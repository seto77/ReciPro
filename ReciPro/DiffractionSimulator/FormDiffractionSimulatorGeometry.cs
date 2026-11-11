using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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


    public double CameraLength2 { set => numericBoxCameraLength2.Value = value; get => numericBoxCameraLength2.Value; }

    // 260521Cl: numericBoxPixelWidth/Height → sizeControl1 へ置換
    public int DetectorWidth { set => sizeControl1.ImageWidth = value; get => sizeControl1.ImageWidth; }
    public int DetectorHeight { set => sizeControl1.ImageHeight = value; get => sizeControl1.ImageHeight; }
    public double DetectorPixelSize { set => numericBoxPixelSize.Value = value; get => numericBoxPixelSize.Value; }

    public double FootX { set => numericBoxFootX.Value = value; get => numericBoxFootX.Value; }
    public double FootY { set => numericBoxFootY.Value = value; get => numericBoxFootY.Value; }

    public bool ShowDetectorArea { get => checkBoxDetectorSizePosition.Checked; set => checkBoxDetectorSizePosition.Checked = value; }

    public float ImageOpacity => (float)trackBarPictureOpacity1.Value / trackBarPictureOpacity1.Maximum;

    public PseudoBitmap pseudBitmap = null;
    public Bitmap OverlappedImage = null;

    #endregion

    //260801Cl 変更: HelpPage を設定 (未設定だと F1 がマニュアルのトップページを開いてしまう)。旧: => InitializeComponent();
    public FormDiffractionSimulatorGeometry()
    {
        InitializeComponent();
        HelpPage = "7-diffraction-simulator";
    }

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
        //if (this.Visible || !FormDiffractionSimulator.Visible)//このフォームがvisibleの時か、親フォームがvisible出ない時(つまり、最初のロード時) // 261105Cl 変更前
        //    FormDiffractionSimulator.CameraLength2 = CameraLength2;
        // 261105Cl 変更 (監査 D15): この窓が隠れているときの変更 (.prm・.ipa のドロップ、マクロ) も親の「カメラ長 2」の欄へ返す。
        //   以前は返さず、計算はこの窓の値 (親の CameraLength2 の getter) なのに、親の欄は古い値のまま見え、次に親の欄を触るとドロップした値が消えていた。
        //   親の欄から来た変更は同じ値なので、親の欄は動かない (NumericBox は同じ値では ValueChanged を出さない)
        FormDiffractionSimulator.CameraLength2 = CameraLength2;

        FormDiffractionSimulator.SetVector();

        FormDiffractionSimulator.Draw();
    }

    /// <summary>261105Cl 追加 (監査 D11): 検出器の大きさ・Foot・画素サイズの変更。以前はどこにも配線されておらず、親 (回折シミュレータ) の検出器の枠・重ねた画像・
    /// 「検出器の中心」に固定した中心が、次に別の操作で再描画されるまで古いままだった</summary>
    private void detectorArea_ValueChanged(object sender, EventArgs e)
    {
        if (FormDiffractionSimulator == null || FormDiffractionSimulator.SkipEvent) return; // 親のマウス操作 (Ctrl + 中ボタンで検出器を動かす) は親が自分で 1 回描く
        if (FormDiffractionSimulator.IsCenterFixed)
            FormDiffractionSimulator.Foot = FormDiffractionSimulator.FixedCenter;
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
        //if (dlg.ShowDialog() == DialogResult.OK) // 261105Cl 変更前
        //    ReadImage(dlg.FileName);
        if (dlg.ShowDialog() == DialogResult.OK && !ReadImage(dlg.FileName)) // 261105Cl 変更 (監査 D18): 読めなかったら知らせる
            showCannotRead(dlg.FileName);
    }

    private double trackbarConstantA = 0, trackbarConstantB = 1;

    private double convertTrackbarIntensityToRealIntensity(int trackbarPosition) => trackbarConstantA + Math.Exp(trackbarPosition / trackbarConstantB);
    //260717Cl 削除: 逆変換 convertRealIntensityToTrackbarIntensity は未使用だった

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
        if (fileName.Length == 1 && fileName[0].EndsWith("prm", StringComparison.OrdinalIgnoreCase))//260718Cl ToLower→OrdinalIgnoreCase
        {
            var prm = DiffractionOptics.Read(fileName[0]);
            // 261105Cl 追加: .prm の数値は DiffractionOptics.ParseNumber で読む。CurrentCulture の Convert.ToDouble では、IPAnalyzer の
            //   新しい .prm ("0.1", InvariantCulture) を de-DE が桁区切りとみなして黙って 1 と読み、fr-FR / sv-SE は例外になる。
            //   旧形式 ("0,1" や U+2212 の負号) も読める。要素が無い (null) ときは従来の Convert.ToDouble(null) と同じく 0 にする
            static double num(string s) => s == null ? 0 : DiffractionOptics.ParseNumber(s); // 261105Cl 追加
            // 261105Cl 追加 (監査 D16): 読めない .prm (Read は null を返す) で、下の SkipDrawing = true のまま例外で抜け、以後この回折シミュレータが一切描画しなくなっていた
            if (prm == null)
            {
                showCannotRead(fileName[0]);
                return;
            }
            FormDiffractionSimulator.SkipDrawing = true;
            try // 261105Cl 追加 (監査 D16): 値の読み取りで例外になっても描画を戻す
            {
                //FormDiffractionSimulator.waveLengthControl.WaveSource = WaveSource.Xray; // 261105Cl 変更前: .prm の線源 (waveSource・xRayElement・xRayLine) を無視して常に X 線 (波長の直接指定) にし、
                //FormDiffractionSimulator.waveLengthControl.XrayWaveSourceElementNumber = 0; //   波長を保存していない .prm では波長 0 にしていた
                //FormDiffractionSimulator.waveLengthControl.WaveLength = Convert.ToDouble(prm.waveLength) * 0.1;
                // 261105Cl 変更 (監査 D16): IPAnalyzer の .prm の読み込み (FormMain の「Read checked parameters」) と同じく、保存されている項目だけを、線源も含めて取り込む
                if (prm.waveLength != null)
                {
                    var wlc = FormDiffractionSimulator.waveLengthControl;
                    wlc.WaveSource = prm.waveSource != null ? (WaveSource)Convert.ToInt32(prm.waveSource) : WaveSource.Xray;
                    if (wlc.WaveSource == WaveSource.Xray)
                    {
                        wlc.XrayWaveSourceElementNumber = prm.xRayElement != null ? Convert.ToInt32(prm.xRayElement) : 0;
                        if (prm.xRayLine != null)
                            wlc.XrayWaveSourceLine = (XrayLine)Convert.ToInt32(prm.xRayLine);
                    }
                    if (wlc.WaveSource != WaveSource.Xray || wlc.XrayWaveSourceElementNumber == 0) // 特性 X 線を選んでいるときは、元素と線で波長が決まる
                        wlc.WaveLength = num(prm.waveLength) * 0.1; // .prm の波長は Å // 261105Cl 変更: ParseNumber で読む
                }

                if (prm.SACLA_EH5_PixelHeight != null)
                    //DetectorHeight = Convert.ToInt32(prm.SACLA_EH5_PixelHeight); // 261105Cl 変更前
                    DetectorHeight = (int)num(prm.SACLA_EH5_PixelHeight); // 261105Cl 変更
                if (prm.SACLA_EH5_PixelWidth != null)
                    //DetectorWidth = Convert.ToInt32(prm.SACLA_EH5_PixelWidth); // 261105Cl 変更前
                    DetectorWidth = (int)num(prm.SACLA_EH5_PixelWidth); // 261105Cl 変更

                if (pseudBitmap != null && (pseudBitmap.Width != DetectorWidth || pseudBitmap.Height != DetectorHeight))//既に読み込んでいる画像のサイズと異なっていたら
                {
                    textBoxFileName.Text = "";
                    //pseudBitmap.Dispose(); pseudBitmap = null; OverlappedImage = null; // (260715Ch) 260716Cl 旧: 差し替え3点セットを直書き
                    SetPseudoBitmap(null); // 260716Cl キャッシュ Bitmap を含む所有リソースを破棄してから参照を外す
                }

                //FootX = Convert.ToDouble(prm.FootX); // 261105Cl 変更前: 保存されていない項目 (null) は 0 になっていた (Convert.ToDouble(null) = 0)
                //FootY = Convert.ToDouble(prm.FootY);
                //Tau = Convert.ToDouble(prm.tiltTau) / 180.0 * Math.PI;
                //Phi = Convert.ToDouble(prm.tiltPhi) / 180.0 * Math.PI;
                //CameraLength2 = Convert.ToDouble(prm.CameraLength2);
                if (prm.FootX != null && prm.FootY != null) // 261105Cl 変更 (監査 D16): 保存されている項目だけ。数値は ParseNumber で読む
                {
                    FootX = num(prm.FootX);
                    FootY = num(prm.FootY);
                }
                if (prm.tiltTau != null && prm.tiltPhi != null)
                {
                    Tau = num(prm.tiltTau) / 180.0 * Math.PI;
                    Phi = num(prm.tiltPhi) / 180.0 * Math.PI;
                }
                if (prm.CameraLength2 != null)
                    CameraLength2 = num(prm.CameraLength2);
                //DetectorPixelSize = (Convert.ToDouble(prm.pixSizeX) + Convert.ToDouble(prm.pixSizeX)) / 2.0; // 旧: Y 方向のピクセル寸法を無視
                //DetectorPixelSize = (Convert.ToDouble(prm.pixSizeX) + Convert.ToDouble(prm.pixSizeY)) / 2.0; // (260715Ch) // 261105Cl 変更前: 画素サイズの無い .prm で 0
                if (prm.pixSizeX != null) // 261105Cl 変更 (監査 D16): 古い形式 (pixSize と aspectRatio) も IPAnalyzer と同じく読む
                    DetectorPixelSize = (num(prm.pixSizeX) + num(prm.pixSizeY ?? prm.pixSizeX)) / 2.0;
                else if (prm.pixSize != null)
                    DetectorPixelSize = num(prm.pixSize) * (1 + (prm.aspectRatio != null ? num(prm.aspectRatio) : 1)) / 2.0;

                ShowDetectorArea = true;
            }
            catch (Exception ex) // 261105Cl 追加 (監査 D16)
            {
                showCannotRead(fileName[0], ex.Message);
            }
            finally // 261105Cl 追加 (監査 D16)
            {
                FormDiffractionSimulator.SkipDrawing = false;
            }
        }
        else
        {
            if (ReadImage(fileName[0]))
            {
                ShowDetectorArea = true;
                this.Visible = true;
                trackBarMaxInt_ValueChanged(sender, e);
            }
            else // 261105Cl 追加 (監査 D18): 読めない画像を落としても何も起きず、理由が分からなかった
                showCannotRead(fileName[0]);
        }
    }

    /// <summary>261105Cl 追加 (監査 D16・D18): .prm や画像を読めなかったことを知らせる</summary>
    private void showCannotRead(string fileName, string detail = "")
        => MessageBox.Show(this, Localization.Loc(
            en: "Could not read this file as a detector parameter file (.prm) or a diffraction image:",
            ja: "このファイルを検出器パラメータ (.prm) または回折像として読み込めませんでした:",
            de: "Diese Datei konnte weder als Detektorparameterdatei (.prm) noch als Beugungsbild gelesen werden:",
            fr: "Impossible de lire ce fichier comme fichier de paramètres du détecteur (.prm) ou comme image de diffraction :",
            es: "No se pudo leer este archivo como archivo de parámetros del detector (.prm) ni como imagen de difracción:",
            pt: "Não foi possível ler este arquivo como arquivo de parâmetros do detector (.prm) nem como imagem de difração:",
            it: "Impossibile leggere questo file come file dei parametri del rivelatore (.prm) o come immagine di diffrazione:",
            ru: "Не удалось прочитать этот файл как файл параметров детектора (.prm) или как дифракционное изображение:",
            zhHans: "无法将此文件读取为探测器参数文件 (.prm) 或衍射图像：",
            zhHant: "無法將此檔案讀取為偵測器參數檔 (.prm) 或繞射影像：",
            ko: "이 파일을 검출기 파라미터 파일 (.prm) 또는 회절 이미지로 읽을 수 없습니다:")
            + "\r\n" + fileName + (detail.Length > 0 ? "\r\n" + detail : ""), "ReciPro", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
                Tau = 0; // 261105Cl 追加 (監査 D18): .ipa は傾きを持たない (中心 = 直接ビームの位置とカメラ長だけ) ので、上の Foot = 中心・カメラ長 2 = カメラ長は τ = 0 のときだけ正しい。以前は前の τ を残していた (dm3 の分岐と同じく 0 にする)
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

                //pseudBitmap = new PseudoBitmap(Ring.Intensity.ToArray(), Ring.SrcImgSize.Width); // 旧: 既存画像を解放せず差し替えていた
                //var loadedPseudoBitmap = new PseudoBitmap(...); pseudBitmap?.Dispose(); pseudBitmap = loadedPseudoBitmap; OverlappedImage = null; // (260715Ch) 260716Cl 旧: 差し替え3点セットを直書き
                SetPseudoBitmap(new PseudoBitmap(Ring.Intensity.ToArray(), Ring.SrcImgSize.Width)); // 260716Cl
                DetectorWidth = pseudBitmap.Width;
                DetectorHeight = pseudBitmap.Height;
                trackBarMaxInt_ValueChanged(this, EventArgs.Empty);
            }
            else
                return false;

            textBoxFileName.Text = filename;

            Ring.CalcFreq();
            // var (min, max) = Ring.Intensity.MinMax();//260717Cl 変更: Min()/Max()/MinMax() の 3 走査を MinMax() 1 回に // 261111Cl 変更前 (SimdLinq)
            var (min, max) = Ring.Intensity.MinMaxIgnoreNaN(); // 261111Cl 変更: NaN を除いた最小・最大 (作者判断。SimdLinq を外したため)
            trackbarConstantA = min - 1;
            trackbarConstantB = trackBarMaxInt.Maximum / Math.Log(max - trackbarConstantA);

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

