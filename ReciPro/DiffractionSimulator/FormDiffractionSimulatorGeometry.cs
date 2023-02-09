using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ReciPro;

public partial class FormDiffractionSimulatorGeometry : Form
{
    #region フィールド、プロパティ
    public FormDiffractionSimulator FormDiffractionSimulator;

    public double Tau { set => numericBoxTau.RadianValue = value; get => numericBoxTau.RadianValue; }
    public double Phi { set => numericBoxPhi.RadianValue = value; get => numericBoxPhi.RadianValue; }

    public double CosTau { set; get; } = 1;
    public double CosTauSquare { set; get; } = 1;
    public double SinTau { set; get; } = 0;
    public double SinTauSquare { set; get; } = 0;

    public double CosPhi { set; get; } = 1;
    public double CosPhiSquare { set; get; } = 1;
    public double SinPhi { set; get; } = 0;
    public double SinPhiSquare { set; get; } = 0;

    /// <summary>
    /// (CosPhi, SinPhi, 0) の周りに Tau回転する行列
    ///  Cos2Phi * (1 - CosTau) + CosTau | CosPhi * SinPhi * (1 - CosTau)  |  SinPhi * SinTau
    ///  CosPhi * SinPhi * (1 - CosTau)  | Sin2Phi * (1 - CosTau) + CosTau | -CosPhi * SinTau
    /// -SinPhi * SinTau                 | cosPhi  * sinTau                |  CosTau 
    /// この行列をv＝(X,Y,CL2)に作用させると、検出器座標(X,Y)を実空間座標に変換できる。
    /// </summary>
    public Matrix3D DetectorRotation { get; set; } = new Matrix3D();

    /// <summary>
    /// DetectorRotationの逆行列
    /// </summary>
    public Matrix3D DetectorRotationInv { get; set; } = new Matrix3D();


    //public double CameraLength1 { set { numericBoxCameraLength1.Value = value; } get { return numericBoxCameraLength1.Value; } }
    public double CameraLength2 { set => numericBoxCameraLength2.Value = value; get => numericBoxCameraLength2.Value; }

    public int DetectorWidth { set => numericBoxPixelWidth.Value = value; get => (int)numericBoxPixelWidth.Value; }
    public int DetectorHeight { set => numericBoxPixelHeight.Value = value; get => (int)numericBoxPixelHeight.Value; }
    public double DetectorPixelSize { set => numericBoxPixelSize.Value = value; get => numericBoxPixelSize.Value; }

    public double FootX { set => numericBoxFootX.Value = value; get => numericBoxFootX.Value; }
    public double FootY { set => numericBoxFootY.Value = value; get => numericBoxFootY.Value; }

    public bool ShowDetectorArea { get => checkBoxDetectorSizePosition.Checked; set => checkBoxDetectorSizePosition.Checked = value; }

    //public bool Precession { get { return checkBoxPrecession.Checked; } set { checkBoxPrecession.Checked = value; } }

    public float ImageOpacity => (float)trackBarPictureOpacity1.Value / trackBarPictureOpacity1.Maximum;

    public PseudoBitmap pseudBitmap = null;
    public Bitmap OverlappedImage = null;

    #endregion

    public FormDiffractionSimulatorGeometry() => InitializeComponent();

    private void FormDiffractionSimulatorGeometry_Load(object sender, EventArgs e)
    {
        panelSchematicDiagram.ClientSize = new Size(0, 0);
        this.ClientSize = new Size(groupBoxDetectorAndOverlappedImage.Width, panel2.Location.Y + panel2.Height);
    }

    private void FormDiffractionSimulatorGeometry_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        this.Visible = false;
    }

    private void checkBoxDetectorSizePosition_CheckedChanged(object sender, EventArgs e)
    {
        groupBoxDetectorAndOverlappedImage.Enabled = checkBoxDetectorSizePosition.Checked;
        FormDiffractionSimulator.Draw();

        FormDiffractionSimulator.copyDetectorAreaToolStripMenuItem.Visible = checkBoxDetectorSizePosition.Checked;
        FormDiffractionSimulator.saveDetectorAreaToolStripMenuItem.Visible = checkBoxDetectorSizePosition.Checked;
    }

    private void numericBoxCameraLength2_ValueChanged(object sender, EventArgs e)
    {
        //var cosTau = Math.Cos(numericBoxTau.RadianValue);
        //numericBoxCameraLength1.Value = cosTau > 0.0000001 ? numericBoxCameraLength2.Value / cosTau : double.PositiveInfinity;

        if (this.Visible || !FormDiffractionSimulator.Visible)//このフォームがvisibleの時か、親フォームがvisible出ない時(つまり、最初のロード時)
            FormDiffractionSimulator.numericUpDownCamaraLength2.Value = (decimal)CameraLength2;

        FormDiffractionSimulator.SetVector();

        FormDiffractionSimulator.Draw();
    }

    private void numericBoxTau_ValueChanged(object sender, EventArgs e)
    {
        FormDiffractionSimulator.SetVector();

        CosTau = Math.Cos(Tau);
        CosTauSquare = CosTau * CosTau;
        SinTau = Math.Sin(Tau);
        SinTauSquare = SinTau * SinTau;
        CosPhi = Math.Cos(Phi);
        CosPhiSquare = CosPhi * CosPhi;
        SinPhi = Math.Sin(Phi);
        SinPhiSquare = SinPhi * SinPhi;

        /// (CosPhi, SinPhi, 0) の周りに Tau回転する行列
        ///  CosPhiSquare * (1 - CosTau) + CosTau | CosPhi * SinPhi * (1 - CosTau)  |  SinPhi * SinTau
        ///  CosPhi * SinPhi * (1 - CosTau)  | SinPhiSquare * (1 - CosTau) + CosTau | -CosPhi * SinTau
        /// -SinPhi * SinTau                 | CosPhi  * SinTau                |  CosTau 

        DetectorRotation = new Matrix3D(
            CosPhiSquare * (1 - CosTau) + CosTau, CosPhi * SinPhi * (1 - CosTau), -SinPhi * SinTau,
            CosPhi * SinPhi * (1 - CosTau), SinPhiSquare * (1 - CosTau) + CosTau, CosPhi * SinTau,
            SinPhi * SinTau, -CosPhi * SinTau, CosTau
            );

        DetectorRotationInv = DetectorRotation.Inverse();

        FormDiffractionSimulator.Draw();
    }

    #region View関連

    private void toolStripComboBoxScale_SelectedIndexChanged(object sender, EventArgs e) => setScale();

    private void toolStripComboBoxScale2_SelectedIndexChanged(object sender, EventArgs e) => setScale();

    private void toolStripComboBoxGradient_SelectedIndexChanged(object sender, EventArgs e) => setScale();

    private void setScale()
    {
        if (pseudBitmap != null)
        {
            pseudBitmap.IsNegative = comboBoxGradient.SelectedIndex == 1;


            var linear = comboBoxScale1.SelectedIndex == 1;
            //スケールをセット
            if (comboBoxScale2.SelectedIndex == 0)//グレー
                pseudBitmap.SetScaleGray(linear);
            else if (comboBoxScale2.SelectedIndex == 1)
                pseudBitmap.SetScaleColdWarm(linear);
            else if (comboBoxScale2.SelectedIndex == 2)
                pseudBitmap.SetScaleSpectrum(linear);
            else if (comboBoxScale2.SelectedIndex == 3)
                pseudBitmap.SetScaleFire(linear);

            OverlappedImage = pseudBitmap.GetImage();
            FormDiffractionSimulator.Draw();
        }
    }

    #endregion View関連

    private void buttonReadPicture_Click(object sender, EventArgs e)
    {
        var dlg = new OpenFileDialog { Filter = "*.bmp, *.jpg, *.tif, *.ipa | *.bmp;*.jpg;*.tif;*.ipa" };
        if (dlg.ShowDialog() == DialogResult.OK)
            ReadImage(dlg.FileName);
    }

    private double trackbarConstantA = 0, trackbarConstantB = 1;

    private double convertTrackbarIntensityToRealIntensity(int trackbarPosition) => trackbarConstantA + Math.Exp(trackbarPosition / trackbarConstantB);

    private double convertRealIntensityToTrackbarIntensity(double intensity) => (int)(trackbarConstantB * Math.Log(intensity - trackbarConstantA));

    private void trackBarMaxInt_ValueChanged(object sender, EventArgs e)
    {
        if (pseudBitmap != null)
        {
            pseudBitmap.MaxValue = convertTrackbarIntensityToRealIntensity(trackBarMaxInt.Value);
            pseudBitmap.MinValue = convertTrackbarIntensityToRealIntensity(trackBarMinInt.Value);
            OverlappedImage = pseudBitmap.GetImage();
            FormDiffractionSimulator.Draw();
        }
    }

    public void FormDiffractionSimulatorGeometry_DragDrop(object sender, DragEventArgs e)
    {
        string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        if (fileName.Length == 1 && fileName[0].ToLower().EndsWith("prm"))
        {
            var prm = DiffractionOptics.Read(fileName[0]);
            FormDiffractionSimulator.SkipDrawing = true;
            if (prm.FootMode == "True")
            {
                FormDiffractionSimulator.waveLengthControl.WaveSource = WaveSource.Xray;
                FormDiffractionSimulator.waveLengthControl.XrayWaveSourceElementNumber = 0;
                FormDiffractionSimulator.waveLengthControl.WaveLength = Convert.ToDouble(prm.waveLength) * 0.1;

                if (prm.SACLA_EH5_PixelHeight != null)
                    DetectorHeight = Convert.ToInt32(prm.SACLA_EH5_PixelHeight);
                if (prm.SACLA_EH5_PixelWidth != null)
                    DetectorWidth = Convert.ToInt32(prm.SACLA_EH5_PixelWidth);

                if (pseudBitmap != null && (pseudBitmap.Width != DetectorWidth || pseudBitmap.Height != DetectorHeight))//既に読み込んでいる画像のサイズと異なっていたら
                {
                    textBoxFileName.Text = "";
                    pseudBitmap = null;
                    OverlappedImage = null;
                }

                FootX = Convert.ToDouble(prm.FootX);
                FootY = Convert.ToDouble(prm.FootY);
                Tau = Convert.ToDouble(prm.tiltTau) / 180.0 * Math.PI;
                Phi = Convert.ToDouble(prm.tiltPhi) / 180.0 * Math.PI;
                CameraLength2 = Convert.ToDouble(prm.CameraLength2);
            }
            else
            {
                FootX = Convert.ToDouble(prm.DirectSpotX);
                FootY = Convert.ToDouble(prm.DirectSpotY);

                FormDiffractionSimulator.waveLengthControl.WaveSource = (WaveSource)Convert.ToInt32(prm.waveSource);
                FormDiffractionSimulator.waveLengthControl.XrayWaveSourceElementNumber = Convert.ToInt32(prm.xRayElement);
                FormDiffractionSimulator.waveLengthControl.XrayWaveSourceLine = (XrayLine)Convert.ToInt32(prm.xRayLine);
                FormDiffractionSimulator.waveLengthControl.WaveLength = Convert.ToDouble(prm.waveLength) * 0.1;


            }
            DetectorPixelSize = (Convert.ToDouble(prm.pixSizeX) + Convert.ToDouble(prm.pixSizeX)) / 2.0;

            ShowDetectorArea = true;
            FormDiffractionSimulator.SkipDrawing = false;
        }
        else
        {
            if (ReadImage(fileName[0]))
            {
                ShowDetectorArea = true;
                this.Visible = true;
                trackBarMaxInt_ValueChanged(sender, e);
            }
        }
    }

    public void FormDiffractionSimulatorGeometry_DragEnter(object sender, DragEventArgs e) => e.Effect = (e.Data.GetData(DataFormats.FileDrop) != null) ? DragDropEffects.Copy : DragDropEffects.None;

    private void buttonClearPicture_Click(object sender, EventArgs e)
    {
        textBoxFileName.Text = "";
        pseudBitmap = null;
        OverlappedImage = null;
        FormDiffractionSimulator.Draw();
    }

    private void textBoxFileName_TextChanged(object sender, EventArgs e) => numericBoxPixelHeight.Enabled = numericBoxPixelWidth.Enabled = textBoxFileName.Text.Length == 0;

    private void trackBarPictureOpacity1_ValueChanged(object sender, EventArgs e) => FormDiffractionSimulator.Draw();

    private void buttonRot90_Click(object sender, EventArgs e)
    {
        if (pseudBitmap != null)
        {
            FormDiffractionSimulator.SkipDrawing = true;
            int width = pseudBitmap.Width, height = pseudBitmap.Height;
            double[] d = new double[pseudBitmap.SrcValuesGray.Length];
            int n = 0;
            for (int x = 0; x < width; x++)
                for (int y = height - 1; y >= 0; y--)
                {
                    d[n++] = pseudBitmap.SrcValuesGray[x + y * width];
                }
            pseudBitmap.SrcValuesGray = d;
            numericBoxPixelWidth.Value = pseudBitmap.Width = height;
            numericBoxPixelHeight.Value = pseudBitmap.Height = width;
            OverlappedImage = pseudBitmap.GetImage();
            FormDiffractionSimulator.SkipDrawing = false;
        }
        else
        {
            FormDiffractionSimulator.SkipDrawing = true;
            int temp = (int)numericBoxPixelWidth.Value;
            numericBoxPixelWidth.Value = numericBoxPixelHeight.Value;
            numericBoxPixelHeight.Value = temp;
            FormDiffractionSimulator.SkipDrawing = false;
        }
    }

    private void CheckBoxShowSchematicDiagram_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxSchematicDiagram.Checked)
        {
            panelSchematicDiagram.ClientSize = pictureBoxSchematicDiagram.Size;
            this.ClientSize = new Size(pictureBoxSchematicDiagram.Width, panel2.Location.Y + panel2.Height);
        }
        else
        {
            panelSchematicDiagram.ClientSize = new Size(1, 1);
            this.ClientSize = new Size(groupBoxDetectorAndOverlappedImage.Width, panel2.Location.Y + panel2.Height);
        }
    }

    public bool ReadImage(string filename)
    {
        try
        {
            if (filename.EndsWith("ipa"))
            {
                ImageIO.IPAImage ipa = ImageIO.GetIPA_Object(filename);
                pseudBitmap = new PseudoBitmap(Ring.Intensity.ToArray(), Ring.SrcImgSize.Width);

                FormDiffractionSimulator.waveLengthControl.Property = ipa.WaveProperty;

                DetectorWidth = pseudBitmap.Width;
                DetectorHeight = pseudBitmap.Height;
                DetectorPixelSize = ipa.Resolution; FootX = ipa.Center.X; FootY = ipa.Center.Y;
                CameraLength2 = ipa.CameraLength;
            }
            if (filename.EndsWith("dm3") || filename.EndsWith("dm4"))
            {
                if (ImageIO.ReadImage(filename))
                {
                    //DigitalMicroGraphデータであればスケールの情報などを取得
                    if (Ring.DigitalMicrographProperty.PixelUnit == PixelUnitEnum.NanoMeterInv)
                    {
                        pseudBitmap = new PseudoBitmap(Ring.Intensity.ToArray(), Ring.SrcImgSize.Width);

                        FormDiffractionSimulator.waveLengthControl.WaveSource = Crystallography.WaveSource.Electron;
                        FormDiffractionSimulator.waveLengthControl.Energy = Ring.DigitalMicrographProperty.AccVoltage / 1000.0;
                        DetectorPixelSize = Ring.DigitalMicrographProperty.PixelSizeInMicron / 1000.0;
                        CameraLength2 = Ring.DigitalMicrographProperty.PixelSizeInMicron / 1000 / Math.Tan(2 * Math.Asin(FormDiffractionSimulator.WaveLength * Ring.DigitalMicrographProperty.PixelScale / 2));
                        Tau = 0;
                        if (DetectorWidth != pseudBitmap.Width || DetectorHeight != pseudBitmap.Height)
                        {
                            DetectorWidth = pseudBitmap.Width;
                            DetectorHeight = pseudBitmap.Height;
                            FootX = DetectorWidth / 2;
                            FootY = DetectorHeight / 2;
                        }
                    }
                }
            }
            else if (filename.ToLower().EndsWith("bmp") || filename.ToLower().EndsWith("jpg") || filename.ToLower().EndsWith("tif") || filename.ToLower().EndsWith("tiff"))
            {
                ImageIO.ReadImage(filename);

                //tifの場合は上下反転(理由は不明)
                /*for (int y = 0; y < Ring.SrcImgSize.Height / 2; y++)
                {
                    for (int x = 0; x < Ring.SrcImgSize.Width; x++)
                    {
                        double temp = Ring.Intensity[x + y * Ring.SrcImgSize.Width];
                        Ring.Intensity[x + y * Ring.SrcImgSize.Width] = Ring.Intensity[x + (Ring.SrcImgSize.Height - y - 1) * Ring.SrcImgSize.Width];
                        Ring.Intensity[x + (Ring.SrcImgSize.Height - y - 1) * Ring.SrcImgSize.Width] = temp;
                    }
                }*/

                pseudBitmap = new PseudoBitmap(Ring.Intensity.ToArray(), Ring.SrcImgSize.Width);
                DetectorWidth = pseudBitmap.Width;
                DetectorHeight = pseudBitmap.Height;
                trackBarMaxInt_ValueChanged(new object(), new EventArgs());
            }
            else
                return false;

            textBoxFileName.Text = filename;

            Ring.CalcFreq();
            trackbarConstantA = Ring.Intensity.Min() - 1;
            trackbarConstantB = trackBarMaxInt.Maximum / Math.Log(Ring.Intensity.Max() - trackbarConstantA);

            var (min, max) = Ring.Intensity.MinMax();
            pseudBitmap.MaxValue = max;
            pseudBitmap.MinValue = min;

            setScale();

            return true;
        }
        catch
        {
            return false;
        }
    }
}
