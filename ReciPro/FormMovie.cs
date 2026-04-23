using Crystallography.OpenGL;
using FFMediaToolkit; //260405Cl 追加
using FFMediaToolkit.Encoding; //260405Cl 追加
using FFMediaToolkit.Graphics; //260405Cl 追加
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic; //260405Cl 追加
using System.IO;
using System.Reflection;
using System.Threading.Tasks; //260405Cl 追加
using System.Windows.Forms;

namespace ReciPro;
public partial class FormMovie : CaptureFormBase
{
    public FormMain FormMain;
    public Form Caller;//呼び出し元

    public Control Target;

    public Func<Bitmap> Func;
    public Vector3DBase Direction { get; set; } = new Vector3DBase(0, 1, 0);

    public double Speed => numericBoxSpeed.Value;
    public double Duration => numericBoxDuration.Value;

    public Vector3DBase A => FormMain.Crystal.A_Axis;
    public Vector3DBase B => FormMain.Crystal.B_Axis;
    public Vector3DBase C => FormMain.Crystal.C_Axis;

    public Matrix3D Rot => FormMain.Crystal.RotationMatrix;

    private static bool ffmpegLoaded = false; //260405Cl 追加
    private bool encoding = false; //260405Cl 追加: エンコード中フラグ

    public FormMovie()
    {
        InitializeComponent();
        comboBoxSpeed.SelectedIndex = 7;
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
            //260422Cl HKLControl revert
            Direction = Rot * (numericBoxPlaneH.Value * rot.Row1 + numericBoxPlaneK.Value * rot.Row2 + numericBoxPlaneL.Value * rot.Row3);
        }
    }

    //260405Cl 変更: ffmpeg.exe Process.Start → FFMediaToolkit API, async化
    //private void buttonOK_Click(object sender, EventArgs e) // 旧実装: ffmpeg.exe を Process.Start で呼び出し
    private async void buttonOK_Click(object sender, EventArgs e)
    {
        if (encoding) return; //260405Cl エンコード中は無視
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

            //FFmpegライブラリの初期化 (初回のみ) 260405Cl
            if (!ffmpegLoaded)
            {
                var ffmpegDir = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "ffmpeg");
                foreach (var name in new[] { "libwinpthread-1", "libgcc_s_seh-1", "libstdc++-6", "zlib1",
                    "libx264-165", "libx265", "avutil-59", "swresample-5", "avcodec-61",
                    "avformat-61", "swscale-8" })
                    System.Runtime.InteropServices.NativeLibrary.Load(Path.Combine(ffmpegDir, name + ".dll"));
                FFmpegLoader.FFmpegPath = ffmpegDir;
                ffmpegLoaded = true;
            }

            var framerate = 30;
            var codec = radioButtonH264.Checked ? VideoCodec.H264 : VideoCodec.H265; //260405Cl 変更
            var speed = (string)comboBoxSpeed.SelectedItem;

            //UIスレッドで全フレームのビットマップデータを収集 260405Cl
            var frames = new List<byte[]>();
            int width = 0, height = 0;
            for (int i = 0; i < Duration * framerate; i++)
            {
                FormMain.Rotate(Direction, Speed * Math.PI / framerate / 180.0);
                var bmp = Target is GLControlAlpha c ? c.GenerateBitmap() : Func();
                if (bmp.Width % 2 != 0 || bmp.Height % 2 != 0)
                    bmp = bmp.Clone(new Rectangle(0, 0, bmp.Width - bmp.Width % 2, bmp.Height - bmp.Height % 2), bmp.PixelFormat);
                width = bmp.Width;
                height = bmp.Height;
                //Bitmapのピクセルデータをbyte[]にコピー
                var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                var bits = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                var stride = bits.Stride;
                var data = new byte[stride * bmp.Height];
                System.Runtime.InteropServices.Marshal.Copy(bits.Scan0, data, 0, data.Length);
                bmp.UnlockBits(bits);
                frames.Add(data);
            }

            //バックグラウンドスレッドでエンコード 260405Cl
            var fileName = dlg.FileName;
            var preset = speed switch
            {
                "ultrafast" => EncoderPreset.UltraFast,
                "superfast" => EncoderPreset.SuperFast,
                "veryfast" => EncoderPreset.VeryFast,
                "faster" => EncoderPreset.Faster,
                "fast" => EncoderPreset.Fast,
                "medium" => EncoderPreset.Medium,
                "slow" => EncoderPreset.Slow,
                "slower" => EncoderPreset.Slower,
                "veryslow" => EncoderPreset.VerySlow,
                _ => EncoderPreset.Medium,
            };

            encoding = true; //260405Cl

            //進捗ダイアログを表示 260405Cl
            var progressForm = new Form
            {
                Text = "Encoding...",
                AutoScaleMode = AutoScaleMode.Dpi,
                ClientSize = new System.Drawing.Size(500, 25),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false, MaximizeBox = false, ControlBox = false,
            };
            var codecName = codec == VideoCodec.H264 ? "H.264" : "H.265"; //260405Cl
            var progressBar = new ProgressBar { Dock = DockStyle.Fill, Maximum = frames.Count, Style = ProgressBarStyle.Continuous };
            progressForm.Text = $"{codecName} / {speed} - 0.0% - 00:00:00 / --:--:--";
            progressForm.Controls.Add(progressBar);
            progressForm.Show(this);

            var sw = System.Diagnostics.Stopwatch.StartNew(); //260405Cl
            var progress = new Progress<int>(v =>
            {
                progressBar.Value = v;
                var percent = 100.0 * v / frames.Count;
                var elapsed = sw.Elapsed;
                var remaining = v > 0 ? TimeSpan.FromTicks(elapsed.Ticks * (frames.Count - v) / v) : TimeSpan.Zero;
                progressForm.Text = $"{codecName} / {speed} - {percent:0.0}% - {elapsed:hh\\:mm\\:ss} / {remaining:hh\\:mm\\:ss}";
            });
            await Task.Run(() =>
            {
                var settings = new VideoEncoderSettings(width, height, framerate, codec)                {                    EncoderPreset = preset                };
                using var file = MediaBuilder.CreateContainer(fileName).WithVideo(settings).Create();
                var size = new System.Drawing.Size(width, height);
                for (int i = 0; i < frames.Count; i++)
                {
                    file.Video.AddFrame(ImageData.FromArray(frames[i], ImagePixelFormat.Bgr24, size));
                    ((IProgress<int>)progress).Report(i + 1);
                }
            });

            progressForm.Close();
            encoding = false; //260405Cl

            FormMain.Enabled = Caller.Enabled = true;
        }
    }

    private void buttonCancel_Click(object sender, EventArgs e) => Visible = false;

    public void Execute(Control target, Form caller)
    {
        if (encoding) return; //260405Cl エンコード中は開かない
        Target = target;
        Caller = caller;
        Location = new Point(caller.Location.X + 10, caller.Location.Y + 10);
        TopMost = true;
        Visible = true;
    }

    public void Execute(Func<Bitmap> func, Form caller)
    {
        if (encoding) return; //260405Cl エンコード中は開かない
        Target = null;
        Func = func;
        Caller = caller;
        Location = new Point(caller.Location.X + 10, caller.Location.Y + 10);
        TopMost = true;
        Visible = true;
    }

 
}

