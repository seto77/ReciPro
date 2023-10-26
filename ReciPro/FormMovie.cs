using Crystallography.OpenGL;
using ImagingSolution.Control;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ReciPro;
public partial class FormMovie : Form
{
    public FormMain FormMain;
    public Form Caller;//呼び出し元

    public Control Target;

    public Func<Bitmap> Func;
    public Vector3DBase Direction { get; set; } = new Vector3DBase(0, 1, 0);

    public double Speed => numericBoxSpeed.Value;
    public double Duration => numericBoxDuration.Value;

    public Vector3D A => FormMain.Crystal.A_Axis;
    public Vector3D B => FormMain.Crystal.B_Axis;
    public Vector3D C => FormMain.Crystal.C_Axis;

    public Matrix3D Rot => FormMain.Crystal.RotationMatrix;

    public FormMovie()
    {
        InitializeComponent();
    }

    private void buttonDirection_Click(object sender, EventArgs e)
    {
        Direction = (sender as Button).Name switch
        {
            "buttonTopRight" => new Vector3DBase(-1, 1, 0),
            "buttonRight" => new Vector3DBase(0, 1, 0),
            "buttonBottomRight" => new Vector3DBase(1, 1, 0),
            "buttonBottom" => new Vector3DBase(1, 0, 0),
            "buttonBottomLeft" => new Vector3DBase(1, -1, 0),
            "buttonLeft" => new Vector3DBase(0, -1, 0),
            "buttonTopLeft" => new Vector3DBase(-1, -1, 0),
            "buttonTop" => new Vector3DBase(-1, 0, 0),
            "buttonClock" => new Vector3DBase(0, 0, -1),
            "buttonAntiClock" => new Vector3DBase(0, 0, 1),
            _ => new Vector3DBase(0, 0, 1)
        };
        var buttons = new[] { buttonTopRight, buttonRight, buttonBottomRight, buttonBottom, buttonBottomLeft, buttonLeft, buttonTopLeft, buttonTop, buttonClock, buttonAntiClock };

        foreach (var b in buttons)
            b.ForeColor = (sender as Button).Name == b.Name ? Color.Blue : Color.Gray;
    }

    private void radioButtonCurrent_CheckedChanged(object sender, EventArgs e)
    {
        tableLayoutPanelAxis.Enabled = radioButtonAxis.Checked;
        tableLayoutPanelPlane.Enabled = radioButtonPlane.Checked;
        tableLayoutPanelCurrent.Enabled = radioButtonCurrent.Checked;
        numericBoxAxisU_ValueChanged(sender, e);
    }

    private void numericBoxAxisU_ValueChanged(object sender, EventArgs e)
    {
        if (radioButtonAxis.Checked)
            Direction = Rot * (numericBoxAxisU.Value * A + numericBoxAxisV.Value * B + numericBoxAxisW.Value * C);
        else if (radioButtonPlane.Checked)
        {
            var rot = new Matrix3D(A, B, C).Inverse();
            Direction = Rot * (numericBoxPlaneH.Value * rot.Row1 + numericBoxPlaneK.Value * rot.Row2 + numericBoxPlaneL.Value * rot.Row3);
        }
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
        Visible = false;

        if (Direction.X == 0 && Direction.Y == 0 && Direction.Z == 0)
        {
            MessageBox.Show("Please input a valid orientation");
            return;
        }

        var dlg = new SaveFileDialog() { Filter = "*.mp4|*.mp4" };
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            FormMain.Enabled = Caller.Enabled = false;

            //実行パスを取得
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\";

            //pngファイルが残っている場合があるので念のため削除
            foreach (var pathFrom in Directory.EnumerateFiles(path + "ffmpeg\\", "*.png"))
                File.Delete(pathFrom);

            var framerate = 30;
            var bmp = new Bitmap(2, 2);
            for (int i = 0; i < Duration * framerate; i++)
            {
                FormMain.Rotate(Direction, Speed * Math.PI / framerate / 180.0);

                if (Target is GLControlAlpha c)
                    bmp = c.GenerateBitmap();
                else
                    bmp = Func();

                if (bmp.Width % 2 != 0 || bmp.Height % 2 != 0)
                    bmp = bmp.Clone(new Rectangle(0, 0, bmp.Width - bmp.Width % 2, bmp.Height - bmp.Height % 2), bmp.PixelFormat);

                bmp.Save(path + $@"ffmpeg\{i:0000}.png", System.Drawing.Imaging.ImageFormat.Png);
            }

            var p = Process.Start(new ProcessStartInfo()
            {
                WorkingDirectory = path + "ffmpeg",
                FileName = path + "ffmpeg\\ffmpeg.exe",
                Arguments = "-framerate 30 -i %04d.png -c:v libx264 -pix_fmt yuv420p -y out.mp4",
                WindowStyle = ProcessWindowStyle.Minimized,
            });
            p.WaitForExit(120000);
            File.Move(path + "ffmpeg\\out.mp4", dlg.FileName, true);

            //pngファイル削除
            foreach (var pathFrom in Directory.EnumerateFiles(path + "ffmpeg\\", "*.png"))
                File.Delete(pathFrom);

            FormMain.Enabled = Caller.Enabled = true;
        }
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
        Visible = false;
    }

    public void Execute(Control target, Form caller)
    {
        Target = target;
        Caller = caller;
        Location = new Point(caller.Location.X + 10, caller.Location.Y + 10);
        TopMost = true;
        Visible = true;
    }

    public void Execute(Func<Bitmap> func, Form caller)
    {
        Target = null;
        Func = func;
        Caller = caller;
        Location = new Point(caller.Location.X + 10, caller.Location.Y + 10);
        TopMost = true;
        Visible = true;
    }

    private void numericBoxAxisU_ReadOnlyChanged(object sender, EventArgs e)
    {

    }
}
