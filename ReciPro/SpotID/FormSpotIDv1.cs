using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ReciPro;

public partial class FormSpotIDv1 : Form
{
    public FormMain formMain;
    private PhotoInformation photo1, photo2, photo3;
    public FormSpotIDv1Results formTEMIDResults;

    public FormSpotIDv1()
    {
        InitializeComponent();
        photo1 = new PhotoInformation(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, true, 0, 0);
        photo2 = new PhotoInformation(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, true, 0, 0);
        photo3 = new PhotoInformation(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, true, 0, 0);

        numericBoxP1Theta.ReadOnly = numericBoxP2Theta.ReadOnly = numericBoxP3Theta.ReadOnly = true;
    }

    //フォームクローズ時
    private void FormTEMID_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        formMain.toolStripButtonSpotIDv1.Checked = false;
        this.Visible = false;
    }

    private void checkBoxPhoto2_CheckedChanged(object sender, EventArgs e)
    {
        panelPhoto2.Visible = groupBoxPhoto2.Enabled = checkBoxPhoto3.Enabled = checkBoxPhoto2.Checked;
        textBoxAngleBetween31.Visible = panel4.Visible = panelPhoto3.Visible = groupBoxPhoto3.Enabled = checkBoxPhoto3.Checked && checkBoxPhoto2.Checked;
        checkBoxEquivalentPhoto1L2Photo2L2.Enabled = checkBoxEquivalentPhoto1L1Photo2L1.Enabled = checkBoxPhoto2.Checked;
        checkBoxEquivalentPhoto2L2Photo3L2.Enabled = checkBoxEquivalentPhoto2L1Photo3L1.Enabled = checkBoxPhoto3.Checked && checkBoxPhoto2.Checked;
        textBox_TextChanged(new object(), new EventArgs());
    }

    //  private bool IsTextChangedEventSkiped = false;
    private void textBox_TextChanged(object sender, EventArgs e)
    {
        //if (IsTextChangedEventSkiped) return;
        //IsTextChangedEventSkiped = true;

        double waveLength = 1.2264262862108010441350327657997 / Math.Sqrt((double)numericUpDownAccVol.Value * 1000 * (1 + (double)numericUpDownAccVol.Value * 0.9784753725226711491437618236159 / 1000));
        double cameraLength = (double)numericUpDownCamaraLength.Value;

        inputBoxP1L1.CameraLength = inputBoxP1L2.CameraLength = inputBoxP1L3.CameraLength =
        inputBoxP2L1.CameraLength = inputBoxP2L2.CameraLength = inputBoxP2L3.CameraLength =
        inputBoxP3L1.CameraLength = inputBoxP3L2.CameraLength = inputBoxP3L3.CameraLength = cameraLength;

        inputBoxP1L1.WaveLength = inputBoxP1L2.WaveLength = inputBoxP1L3.WaveLength =
        inputBoxP2L1.WaveLength = inputBoxP2L2.WaveLength = inputBoxP2L3.WaveLength =
        inputBoxP3L1.WaveLength = inputBoxP3L2.WaveLength = inputBoxP3L3.WaveLength = waveLength;

        checkBoxEquivalentPhoto1L1Photo2L1_CheckedChanged(new object(), new EventArgs());

        photo1 = new PhotoInformation(inputBoxP1L1.Length, inputBoxP1L2.Length, inputBoxP1L3.Length, numericBoxP1Theta.RadianValue,
            (double)numericUpDownPhoto1L1Err.Value / 100, (double)numericUpDownPhoto1L2Err.Value / 100, (double)numericUpDownPhoto1L3Err.Value / 100, (double)numericUpDownPhoto1ThetaErr.Value / 180 * Math.PI,
            numericBoxP1Tilt1.RadianValue, numericBoxP1Tilt2.RadianValue, (double)numericUpDownPhoto1Tilt1Err.Value / 180 * Math.PI, (double)numericUpDownPhoto1Tilt2Err.Value / 180 * Math.PI,
            radioButtonPhoto1Mode1.Checked, waveLength, cameraLength);

        if (photo1.IsTriangleMode)
            numericBoxP1Theta.RadianValue = photo1.Paintable ? photo1.Theta : 0;
        else
            inputBoxP1L3.Length = photo1.Paintable ? photo1.L3 : 0;

        photo2 = new PhotoInformation(inputBoxP2L1.Length, inputBoxP2L2.Length, inputBoxP2L3.Length, numericBoxP2Theta.RadianValue,
            (double)numericUpDownPhoto2L1Err.Value / 100, (double)numericUpDownPhoto2L2Err.Value / 100, (double)numericUpDownPhoto2L3Err.Value / 100, (double)numericUpDownPhoto2ThetaErr.Value / 180 * Math.PI,
            numericBoxP2Tilt1.RadianValue, numericBoxP2Tilt2.RadianValue, (double)(numericUpDownPhoto2Tilt1Err.Value) / 180 * Math.PI, (double)(numericUpDownPhoto2Tilt2Err.Value) / 180 * Math.PI,
            radioButtonPhoto2Mode1.Checked, waveLength, cameraLength);

        if (photo2.IsTriangleMode)
            numericBoxP2Theta.RadianValue = photo2.Paintable ? photo2.Theta : 0;
        else
            inputBoxP2L3.Length = photo2.Paintable ? photo2.L3 : 0;

        photo3 = new PhotoInformation(inputBoxP3L1.Length, inputBoxP3L2.Length, inputBoxP2L3.Length, numericBoxP3Theta.RadianValue,
            (double)numericUpDownPhoto3L1Err.Value / 100, (double)numericUpDownPhoto3L2Err.Value / 100, (double)numericUpDownPhoto3L3Err.Value / 100, (double)numericUpDownPhoto3ThetaErr.Value / 180 * Math.PI,
            numericBoxP3Tilt1.RadianValue, numericBoxP3Tilt2.RadianValue, (double)(numericUpDownPhoto3Tilt1Err.Value) / 180 * Math.PI, (double)(numericUpDownPhoto3Tilt2Err.Value) / 180 * Math.PI,
            radioButtonPhoto3Mode1.Checked, waveLength, cameraLength);

        if (photo3.IsTriangleMode)
            numericBoxP3Theta.RadianValue = photo3.Paintable ? photo3.Theta : 0;
        else
            inputBoxP3L3.Length = photo3.Paintable ? photo3.L3 : 0;

        buttonSearchPhoto1.Enabled = photo1.Paintable;
        buttonSearchPhoto2.Enabled = photo2.Paintable;
        buttonSearchPhoto3.Enabled = photo3.Paintable;

        //二つ以上がPanitableなときAllボタンをEnableにする
        buttonSearchAll.Enabled = (photo1.Paintable && photo2.Paintable && checkBoxPhoto2.Checked)
            || (photo1.Paintable && photo2.Paintable && checkBoxPhoto2.Checked && checkBoxPhoto3.Checked && photo3.Paintable);

        if (photo1.Paintable && photo2.Paintable && checkBoxPhoto3.Checked && photo3.Paintable)
            buttonSearchAll.Text = "Search zone axes from three patterns";
        else
            buttonSearchAll.Text = "Search zone axes from two patterns";

        //最大の長さを調べて、その値で50を割ったときの値がmagになる
        double maxlength = double.NegativeInfinity;

        if (photo1.Paintable) maxlength = Math.Max(photo1.L1, photo1.L2);
        if (photo2.Paintable) maxlength = Math.Max(maxlength, Math.Max(photo2.L1, photo2.L2));
        if (photo3.Paintable) maxlength = Math.Max(maxlength, Math.Max(photo3.L1, photo3.L2));
        double mag;
        if (maxlength > 0) mag = 40 / maxlength;
        else mag = 50;

        pictureBoxPhoto1.Image = DrawPic(photo1, pictureBoxPhoto1.Size, mag);

        double rot = 0;//回転を決める
        if (checkBoxEquivalentPhoto1L2Photo2L2.Checked)
            rot = Math.Atan2(photo2.P2.Y, photo2.P2.X) - Math.Atan2(photo1.P2.Y, photo1.P2.X);
        photo2.Rot(rot);

        pictureBoxPhoto2.Image = DrawPic(photo2, pictureBoxPhoto2.Size, mag);

        if (checkBoxEquivalentPhoto2L1Photo3L1.Checked) rot = Math.Atan2(photo2.P1.Y, photo2.P1.X) - Math.Atan2(photo3.P1.Y, photo3.P1.X);
        else if (checkBoxEquivalentPhoto2L2Photo3L2.Checked) rot = Math.Atan2(photo2.P2.Y, photo2.P2.X) - Math.Atan2(photo3.P2.Y, photo3.P2.X);
        else rot = 0;
        photo3.Rot(rot);
        pictureBoxPhoto3.Image = DrawPic(photo3, pictureBoxPhoto3.Size, mag);

        //IsTextChangedEventSkiped = false;
    }

    private static Bitmap DrawPic(PhotoInformation photo, Size size, double mag)
    {
        var bmp = new Bitmap(size.Width, size.Height);
        var g = Graphics.FromImage(bmp);
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        if (photo.Paintable)
            g.Clear(Color.White);
        else
            g.Clear(Color.LightGray);
        PointF p1, p2;
        if (photo.Paintable)//ベクトルの位置を決める
        {
            p1 = new PointF((float)(mag * photo.P1.X), (float)(mag * photo.P1.Y));
            p2 = new PointF((float)(mag * photo.P2.X), (float)(mag * photo.P2.Y));
        }
        else
        {
            p1 = new PointF(50, 0);
            p2 = new PointF(10, -70);
        }
        var offset = new PointF(-(p1.X + p2.X) / 3f + bmp.Width / 2f, -(p1.Y + p2.Y) / 3f + bmp.Height / 2f);

        //補助線を書く
        var penArc = new Pen(Brushes.Gray, 1 / 10f);
        var penLine = new Pen(Brushes.Gray, 1 / 10f);

        string str;
        //p1
        double angle = Math.Atan2(p1.Y, p1.X);
        float length = (float)Math.Sqrt(p1.X * p1.X + p1.Y * p1.Y);
        g.Transform = new System.Drawing.Drawing2D.Matrix((float)Math.Cos(angle), (float)Math.Sin(angle), -(float)Math.Sin(angle), (float)Math.Cos(angle), offset.X, offset.Y);
        g.DrawArc(penArc, new RectangleF(0, -length / 4, length, length / 2), 0, 180);
        g.DrawLine(penLine, 0, 0, length, 0);

        //p2
        angle = Math.Atan2(p2.Y, p2.X);
        length = (float)Math.Sqrt(p2.X * p2.X + p2.Y * p2.Y);
        g.Transform = new System.Drawing.Drawing2D.Matrix((float)Math.Cos(angle), (float)Math.Sin(angle), -(float)Math.Sin(angle), (float)Math.Cos(angle), offset.X, offset.Y);
        g.DrawArc(penArc, new RectangleF(0, -length / 4, length, length / 2), 180, 180);
        g.DrawLine(penLine, 0, 0, length, 0);

        //p3
        if (photo.IsTriangleMode)
        {
            var p3 = new PointF(p2.X - p1.X, p2.Y - p1.Y);
            angle = Math.Atan2(p3.Y, p3.X);
            length = (float)Math.Sqrt(p3.X * p3.X + p3.Y * p3.Y);
            g.Transform = new System.Drawing.Drawing2D.Matrix((float)Math.Cos(angle), (float)Math.Sin(angle), -(float)Math.Sin(angle), (float)Math.Cos(angle),
                p1.X + offset.X, p1.Y + offset.Y);
            g.DrawArc(penArc, new RectangleF(0, -length / 4, length, length / 2), 0, 180);
            g.DrawLine(penLine, 0, 0, length, 0);
        }
        else //theta
        {
            g.Transform = new System.Drawing.Drawing2D.Matrix(1, 0, 0, 1, offset.X, offset.Y);
            float r = (float)(Math.Min(Math.Sqrt(p1.X * p1.X + p1.Y * p1.Y), Math.Sqrt(p2.X * p2.X + p2.Y * p2.Y)) / 2);
            g.DrawArc(penArc, new RectangleF(-r / 2, -r / 2, r, r), (float)(Math.Atan2(p1.Y, p1.X) / Math.PI * 180), (float)((Math.Atan2(p2.Y, p2.X) - Math.Atan2(p1.Y, p1.X)) / Math.PI * 180));
        }

        //次に点を描く
        g.Transform = new System.Drawing.Drawing2D.Matrix(1, 0, 0, 1, offset.X, offset.Y);
        float ptSize = 8;
        for (int n = 1; n < double.PositiveInfinity; n++)
        {
            bool flag = true;
            for (int i = -n; i <= n; i++)
                for (int j = Math.Abs(i) - n; j <= -Math.Abs(i) + n; j += -Math.Abs(i) + n != 0 ? 2 * (-Math.Abs(i) + n) : 1)
                {
                    var p = new PointF(p1.X * i + p2.X * j, p1.Y * i + p2.Y * j);
                    if (g.IsVisible(p))
                    {
                        g.FillEllipse(new SolidBrush(Color.Black), p.X - ptSize / 2, p.Y - ptSize / 2, ptSize, ptSize);
                        flag = false;
                    }
                }
            if (flag)
            {
                g.FillEllipse(new SolidBrush(Color.Red), -ptSize / 2, -ptSize / 2, ptSize, ptSize);
                break;
            }
        }

        //最後に字を書く
        var strFont = new Font("tahoma", 7.5f, FontStyle.Regular);
        var brush = new SolidBrush(Color.SlateBlue);
        g.Transform = new System.Drawing.Drawing2D.Matrix(1, 0, 0, 1, 0, 0);
        if (!photo.Paintable)
        {
            if (photo.IsTriangleMode)
                g.DrawString("Input values of l1, l2, & l3", strFont, brush, 0, 0);
            else
                g.DrawString("Input values of l1, l2, & θ", strFont, brush, 0, 0);
        }
        g.Transform = new System.Drawing.Drawing2D.Matrix(1, 0, 0, 1, offset.X, offset.Y);

        //p1
        str = photo.Paintable ? "L1 = " + photo.L1.ToString() + "mm" : "L1";
        g.DrawString(str, strFont, brush, p1.X / 2f - p1.Y / 4f, p1.Y / 2f + p1.X / 4f);
        //p2
        str = photo.Paintable ? "L2 = " + photo.L2.ToString() + "mm" : "L2";
        if (photo.Paintable)
            g.DrawString(str, strFont, brush, p2.X / 2f + p2.Y / 4f - 50, p2.Y / 2f - p2.X / 4f);
        else
            g.DrawString(str, strFont, brush, p2.X / 2f + p2.Y / 4f - 15, p2.Y / 2f - p2.X / 4f);
        //p3
        if (photo.IsTriangleMode)
        {
            str = photo.Paintable ? "L3 = " + photo.L3.ToString() + "mm" : "L3";
            g.DrawString(str, strFont, brush, (p2.X + p1.X) / 2f - (p2.Y - p1.Y) / 4f + 5, (p1.Y + p2.Y) / 2f + (p2.X - p1.X) / 4f);
        }
        else //theta
        {
            float r = (float)(Math.Min(Math.Sqrt(p1.X * p1.X + p1.Y * p1.Y), Math.Sqrt(p2.X * p2.X + p2.Y * p2.Y)) / 2);
            angle = (Math.Atan2(p2.Y, p2.X) + Math.Atan2(p1.Y, p1.X)) / 2;
            str = photo.Paintable ? "θ = " + (photo.Theta / Math.PI * 180).ToString("f2") + "°" : "θ";
            g.DrawString(str, strFont, brush, (float)(r * Math.Cos(angle) / 2) + 5, (float)(r * Math.Sin(angle) / 2) - 10);
        }
        return bmp;
    }

    private void checkBoxEquivalentPhoto1L1Photo2L1_CheckedChanged(object sender, EventArgs e)
    {
        inputBoxP2L1.Enabled = !checkBoxEquivalentPhoto1L1Photo2L1.Checked;
        inputBoxP2L2.Enabled = !checkBoxEquivalentPhoto1L2Photo2L2.Checked;
        inputBoxP3L1.Enabled = !checkBoxEquivalentPhoto2L1Photo3L1.Checked;
        inputBoxP3L2.Enabled = !checkBoxEquivalentPhoto2L2Photo3L2.Checked;
        if (checkBoxEquivalentPhoto1L1Photo2L1.Checked) inputBoxP2L1.Length = inputBoxP1L1.Length;
        if (checkBoxEquivalentPhoto1L2Photo2L2.Checked) inputBoxP2L2.Length = inputBoxP1L2.Length;
        if (checkBoxEquivalentPhoto2L1Photo3L1.Checked) inputBoxP3L1.Length = inputBoxP2L1.Length;
        if (checkBoxEquivalentPhoto2L2Photo3L2.Checked) inputBoxP3L2.Length = inputBoxP2L2.Length;
    }

    private void radioButtonPhoto1Mode1_CheckedChanged(object sender, EventArgs e)
    {
        inputBoxP1L3.Enabled = numericUpDownPhoto1L3Err.Enabled = numericBoxP1Theta.ReadOnly = !radioButtonPhoto1Mode2.Checked;
        
      numericUpDownPhoto1ThetaErr.Enabled = radioButtonPhoto1Mode2.Checked;
       
        inputBoxP2L3.Enabled = numericUpDownPhoto2L3Err.Enabled = numericBoxP2Theta.ReadOnly = !radioButtonPhoto2Mode2.Checked;
         numericUpDownPhoto2ThetaErr.Enabled = radioButtonPhoto2Mode2.Checked;
        
        inputBoxP3L3.Enabled = numericUpDownPhoto3L3Err.Enabled = numericBoxP3Theta.ReadOnly = !radioButtonPhoto3Mode2.Checked;
         numericUpDownPhoto3ThetaErr.Enabled = radioButtonPhoto3Mode2.Checked;
        textBox_TextChanged(new object(), new EventArgs());
    }

    private void FormTEMID_Load(object sender, EventArgs e)
    {
        textBox_TextChanged(new object(), new EventArgs());
        numericUpDownAccVol_ValueChanged(new object(), new EventArgs());
    }

    private void buttonSearch_Click(object sender, EventArgs e)
    {
        if (formMain.Crystal == null || formMain.Crystal.A * formMain.Crystal.B * formMain.Crystal.C == 0) return;
        var za = new List<ZoneAxis>();

        PhotoInformation photo;

        if (((Button)sender).Name == "buttonSearchPhoto1" && photo1.Paintable) photo = photo1;
        else if (((Button)sender).Name == "buttonSearchPhoto2" && photo2.Paintable) photo = photo2;
        else if (((Button)sender).Name == "buttonSearchPhoto3" && photo3.Paintable) photo = photo3;
        else return;

        for (int i = 0; i < formMain.Crystals.Length; i++)
            za.AddRange(FindZoneAxis.GetZoneAxis(formMain.Crystals[i], photo, true));

        if (za.Count > 1000)
            MessageBox.Show("Number of candidates are over 1000. Set more limeted conditions.");
        else if (za.Count > 0)
        {
            formTEMIDResults?.Close();
            formTEMIDResults = new FormSpotIDv1Results(this);
            formTEMIDResults.Show();
            formTEMIDResults.SetDataSet(photo, za);
        }
        else
        {
            MessageBox.Show("No candidate is found");
        }
    }

    private void numericUpDownAccVol_ValueChanged(object sender, EventArgs e)
    {
        //m0 = 9.1093897*10^-31   //e0 = 1.60217733*10^-19  //h = 6.6260755*10^-34
        //c = 2.99792458*10^+8     //U =voltage
        //WaveLength = h / Math.Sqrt ( 2 * m0 * e0 * U * ( 1 + e0 * U / 2 / m0 / c^2 ) )
        double WaveLength = 1.2264262862108010441350327657997 / Math.Sqrt((double)numericUpDownAccVol.Value * 1000 * (1 + (double)numericUpDownAccVol.Value * 0.9784753725226711491437618236159 / 1000));
        textBoxWaveLength.Text = WaveLength.ToString();
    }

    #region L3のInputBoxか角度をクリックされたときの動作
    private void inputBoxP1L3_Click(object sender, EventArgs e) => radioButtonPhoto1Mode1.Checked = true;
    private void numericBoxP1Theta_Click2(object sender, EventArgs e) => radioButtonPhoto1Mode2.Checked = true;
    private void inputBoxP2L3_Click2(object sender, EventArgs e) => radioButtonPhoto2Mode1.Checked = true;
    private void numericBoxP2Theta_Click2(object sender, EventArgs e) => radioButtonPhoto2Mode2.Checked = true;
    private void inputBoxP3L1_Click2(object sender, EventArgs e) => radioButtonPhoto3Mode1.Checked = true;
    private void numericBoxP3Theta_Click2(object sender, EventArgs e) => radioButtonPhoto3Mode2.Checked = true;
    #endregion


    private void textBoxTilt_TextChanged(object sender, EventArgs e)
    {
        textBox_TextChanged(new object(), new EventArgs());
        try
        {
            textBoxAngleBetween12.Text = (FindZoneAxis.GetAngleBetweenHolders(photo1.Tilt1, photo1.Tilt2, photo2.Tilt1, photo2.Tilt2) / Math.PI * 180).ToString("f3");
            textBoxAngleBetween23.Text = (FindZoneAxis.GetAngleBetweenHolders(photo2.Tilt1, photo2.Tilt2, photo3.Tilt1, photo3.Tilt2) / Math.PI * 180).ToString("f3");
            textBoxAngleBetween31.Text = (FindZoneAxis.GetAngleBetweenHolders(photo3.Tilt1, photo3.Tilt2, photo1.Tilt1, photo1.Tilt2) / Math.PI * 180).ToString("f3");
        }
        catch { }
    }

    private void buttonSearchAll_Click(object sender, EventArgs e)
    {
        textBox_TextChanged(new object(), new EventArgs());
        //photo3が選択されているかどうかで分岐

        var candidate = new List<ZoneAxes>();
        if (!checkBoxPhoto3.Checked)
        {//1,2だけ選択されている場合
            for (int i = 0; i < formMain.Crystals.Length; i++)
                candidate.AddRange(FindZoneAxis.ZoneAxisFromTwoZoneAxis(formMain.Crystals[i], photo1, photo2, true));
        }
        else
        {//1,2,3が選択されている場合
            for (int i = 0; i < formMain.Crystals.Length; i++)
                candidate.AddRange(FindZoneAxis.ZoneAxisFromThreeZoneAxis(formMain.Crystals[i], photo1, photo2, photo3, true));
        }

        if (candidate.Count > 1000)
            MessageBox.Show("Number of candidates are over 1000. Set more limeted conditions.");
        else if (candidate.Count > 0)
        {
            double[] obsAngle = new double[]{
                    FindZoneAxis.GetAngleBetweenHolders(photo1.Tilt1, photo1.Tilt2, photo2.Tilt1, photo2.Tilt2),
                    FindZoneAxis.GetAngleBetweenHolders(photo2.Tilt1, photo2.Tilt2, photo3.Tilt1, photo3.Tilt2),
                    FindZoneAxis.GetAngleBetweenHolders(photo3.Tilt1, photo3.Tilt2, photo1.Tilt1, photo1.Tilt2)};

            formTEMIDResults?.Close();
            formTEMIDResults = new FormSpotIDv1Results(this);
            formTEMIDResults.Show();
            formTEMIDResults.SetDataSet(obsAngle, candidate);
        }
        else
        {
            MessageBox.Show("No candidate is found");
        }
    }
}
