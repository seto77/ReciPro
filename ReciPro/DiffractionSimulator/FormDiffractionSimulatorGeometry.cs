using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ReciPro;

public partial class FormDiffractionSimulatorGeometry : FormBase
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

    /// <summary>DetectorRotationの逆行列</summary>
    public Matrix3D DetectorRotationInv { get; set; } = new Matrix3D();


    //public double CameraLength1 { set { numericBoxCameraLength1.Value = value; } get { return numericBoxCameraLength1.Value; } }
    public double CameraLength2 { set => numericBoxCameraLength2.Value = value; get => numericBoxCameraLength2.Value; }

    // 260521Cl: numericBoxPixelWidth/Height → sizeControl1 へ置換
    public int DetectorWidth { set => sizeControl1.ImageWidth = value; get => sizeControl1.ImageWidth; }
    public int DetectorHeight { set => sizeControl1.ImageHeight = value; get => sizeControl1.ImageHeight; }
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
        this.ClientSize = new Size(groupBoxDetectorAndOverlappedImage.Width, panelDetectorAreaAndOverlappedImage.Location.Y + panelDetectorAreaAndOverlappedImage.Height);
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
            FormDiffractionSimulator.CameraLength2 = CameraLength2;

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
        FormDiffractionSimulator.SetVector();
        if(FormDiffractionSimulator.IsCenterFixed)
            FormDiffractionSimulator.Foot = FormDiffractionSimulator.FixedCenter;
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

    private void buttonLoadPicture_Click(object sender, EventArgs e)
    {
        //var dlg = new OpenFileDialog { Filter = ImageIO.FilterString + "|All files(*.*)|*.*" }; // 旧: ダイアログを解放していなかった
        using var dlg = new OpenFileDialog { Filter = ImageIO.FilterString + "|All files(*.*)|*.*" }; // (260715Ch)
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
           // if (prm.FootMode == "True")
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
                    //pseudBitmap.Dispose(); pseudBitmap = null; OverlappedImage = null; // (260715Ch) 260716Cl 旧: 差し替え3点セットを直書き
                    SetPseudoBitmap(null); // 260716Cl キャッシュ Bitmap を含む所有リソースを破棄してから参照を外す
                }

                FootX = Convert.ToDouble(prm.FootX);
                FootY = Convert.ToDouble(prm.FootY);
                Tau = Convert.ToDouble(prm.tiltTau) / 180.0 * Math.PI;
                Phi = Convert.ToDouble(prm.tiltPhi) / 180.0 * Math.PI;
                CameraLength2 = Convert.ToDouble(prm.CameraLength2);
            }
            //else
            //{
            //    FootX = Convert.ToDouble(prm.DirectSpotX);
            //    FootY = Convert.ToDouble(prm.DirectSpotY);

            //    FormDiffractionSimulator.waveLengthControl.WaveSource = (WaveSource)Convert.ToInt32(prm.waveSource);
            //    FormDiffractionSimulator.waveLengthControl.XrayWaveSourceElementNumber = Convert.ToInt32(prm.xRayElement);
            //    FormDiffractionSimulator.waveLengthControl.XrayWaveSourceLine = (XrayLine)Convert.ToInt32(prm.xRayLine);
            //    FormDiffractionSimulator.waveLengthControl.WaveLength = Convert.ToDouble(prm.waveLength) * 0.1;

            //}
            //DetectorPixelSize = (Convert.ToDouble(prm.pixSizeX) + Convert.ToDouble(prm.pixSizeX)) / 2.0; // 旧: Y 方向のピクセル寸法を無視
            DetectorPixelSize = (Convert.ToDouble(prm.pixSizeX) + Convert.ToDouble(prm.pixSizeY)) / 2.0; // (260715Ch)

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

    // 260716Cl 追加: pseudBitmap 差し替え時の不変条件 (旧インスタンスの破棄と、旧インスタンス由来 OverlappedImage の無効化) を集約。
    // 引数の新インスタンス生成は呼び出し前に評価されるため、コンストラクタ失敗時も旧画像は有効なまま保たれる。
    private void SetPseudoBitmap(PseudoBitmap value)
    {
        pseudBitmap?.Dispose();
        pseudBitmap = value;
        OverlappedImage = null;
    }

    private void buttonClearPicture_Click(object sender, EventArgs e)
    {
        textBoxFileName.Text = "";
        //pseudBitmap?.Dispose(); pseudBitmap = null; OverlappedImage = null; // (260715Ch) 260716Cl 旧: 差し替え3点セットを直書き
        SetPseudoBitmap(null); // 260716Cl 読み込んだ画像と生成済み Bitmap を解放する
        FormDiffractionSimulator.Draw();
    }

    // 260521Cl: numericBoxPixelHeight/Width.Enabled → sizeControl1.Enabled
    private void textBoxFileName_TextChanged(object sender, EventArgs e) => sizeControl1.Enabled = textBoxFileName.Text.Length == 0;

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
            // 260521Cl: numericBoxPixelWidth/Height → sizeControl1。Detector W/H を入れ替え
            pseudBitmap.Width = height;
            pseudBitmap.Height = width;
            sizeControl1.Value = new Size(height, width);
            OverlappedImage = pseudBitmap.GetImage();
            FormDiffractionSimulator.SkipDrawing = false;
        }
        else
        {
            FormDiffractionSimulator.SkipDrawing = true;
            // 260521Cl: numericBoxPixelWidth/Height → sizeControl1。Detector W/H を入れ替え
            sizeControl1.Value = new Size(sizeControl1.ImageHeight, sizeControl1.ImageWidth);
            FormDiffractionSimulator.SkipDrawing = false;
        }
    }

    private void CheckBoxShowSchematicDiagram_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxSchematicDiagram.Checked)
        {
            panelSchematicDiagram.ClientSize = pictureBoxSchematicDiagram.Size;
            this.ClientSize = new Size(pictureBoxSchematicDiagram.Width, panelDetectorAreaAndOverlappedImage.Location.Y + panelDetectorAreaAndOverlappedImage.Height);
        }
        else
        {
            panelSchematicDiagram.ClientSize = new Size(1, 1);
            this.ClientSize = new Size(groupBoxDetectorAndOverlappedImage.Width, panelDetectorAreaAndOverlappedImage.Location.Y + panelDetectorAreaAndOverlappedImage.Height);
        }
    }

    public bool ReadImage(string filename)
    {
        try
        {
            var ext = System.IO.Path.GetExtension(filename).ToLower()[1..];

            if (ext == "ipa")
            {
                //ImageIO.IPAImage ipa = ImageIO.GetIPA_Object(filename); // 旧: 同じ XML を後続の ReadImage でもう一度解析していた
                // 旧: ここでは Ring を更新せず、後続の汎用画像分岐へ偶然フォールスルーして IPA を再読込していた
                if (!ImageIO.ReadImage(filename))
                    return false; // (260715Ch) PseudoBitmap 作成前に IPA 強度を Ring へ確実に展開する
                //pseudBitmap = new PseudoBitmap(Ring.Intensity.ToArray(), Ring.SrcImgSize.Width); // 旧: 既存画像を解放せず差し替えていた
                //var loadedPseudoBitmap = new PseudoBitmap(...); pseudBitmap?.Dispose(); pseudBitmap = loadedPseudoBitmap; OverlappedImage = null; // (260715Ch) 260716Cl 旧: 差し替え3点セットを直書き
                SetPseudoBitmap(new PseudoBitmap(Ring.Intensity.ToArray(), Ring.SrcImgSize.Width)); // 260716Cl

                //FormDiffractionSimulator.waveLengthControl.Property = ipa.WaveProperty; // 旧: 別途 Deserialize したオブジェクトから取得
                FormDiffractionSimulator.waveLengthControl.Property = Ring.IP.WaveProperty; // (260715Ch) ReadImage が設定した同一メタデータを再利用

                DetectorWidth = pseudBitmap.Width;
                DetectorHeight = pseudBitmap.Height;
                //DetectorPixelSize = ipa.Resolution; FootX = ipa.Center.X; FootY = ipa.Center.Y; CameraLength2 = ipa.CameraLength; // 旧
                DetectorPixelSize = Ring.IP.PixSizeX; FootX = Ring.IP.CenterX; FootY = Ring.IP.CenterY; // (260715Ch)
                CameraLength2 = Ring.IP.FilmDistance; // (260715Ch)
            }
            //if (ext == "dm3" || ext == "dm4") // 旧: 独立 if のため IPA が後続の汎用画像分岐にも入り、二重に PseudoBitmap を生成していた
            else if (ext == "dm3" || ext == "dm4") // (260715Ch)
            {
                //if (ImageIO.ReadImage(filename)) // 旧: 読み込み失敗や非逆空間 DM でも古い画像を再利用して後続処理へ進み得た
                if (!ImageIO.ReadImage(filename))
                    return false; // (260715Ch)

                //DigitalMicroGraphデータであればスケールの情報などを取得
                if (Ring.DigitalMicrographProperty.PixelUnit == LengthUnitEnum.NanoMeterInverse)
                {
                    //pseudBitmap = new PseudoBitmap(Ring.Intensity.ToArray(), Ring.SrcImgSize.Width); // 旧: 既存画像を解放せず差し替えていた
                    //var loadedPseudoBitmap = new PseudoBitmap(...); pseudBitmap?.Dispose(); pseudBitmap = loadedPseudoBitmap; OverlappedImage = null; // (260715Ch) 260716Cl 旧: 差し替え3点セットを直書き
                    SetPseudoBitmap(new PseudoBitmap(Ring.Intensity.ToArray(), Ring.SrcImgSize.Width)); // 260716Cl

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
                else
                    return false; // (260715Ch) 実空間 DM を回折像として扱わず、古い画像の流用を防ぐ
            }
            else if (ext == "bmp" || ext == "jpg" || ext == "tif" || ext == "tiff" || ImageIO.IsReadable(ext))
            {
                //ImageIO.ReadImage(filename); // 旧: 読み込み失敗後も古い Ring データで成功扱いにし得た
                if (!ImageIO.ReadImage(filename))
                    return false; // (260715Ch)

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

                //pseudBitmap = new PseudoBitmap(Ring.Intensity.ToArray(), Ring.SrcImgSize.Width); // 旧: 既存画像を解放せず差し替えていた
                //var loadedPseudoBitmap = new PseudoBitmap(...); pseudBitmap?.Dispose(); pseudBitmap = loadedPseudoBitmap; OverlappedImage = null; // (260715Ch) 260716Cl 旧: 差し替え3点セットを直書き
                SetPseudoBitmap(new PseudoBitmap(Ring.Intensity.ToArray(), Ring.SrcImgSize.Width)); // 260716Cl
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

