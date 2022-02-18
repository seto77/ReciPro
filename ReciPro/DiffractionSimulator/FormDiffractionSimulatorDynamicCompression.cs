using Crystallography;
using Crystallography.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ReciPro;

public partial class FormDiffractionSimulatorDynamicCompression : Form
{
    public FormDiffractionSimulator FormDiffractionSimulator;
    public Crystal OriginalCrystal = null;

    public CrystalControl CrystalControl;
    public Crystal Crystal;

    private Dictionary<double, Profile> Profiles;

    public FormDiffractionSimulatorDynamicCompression() => InitializeComponent();

    private void FormDiffractionSimulatorDynamicCompression_Load(object sender, EventArgs e) => CrystalControl = FormDiffractionSimulator.formMain.crystalControl;

    /// <summary>
    /// Pasteボタンを押したときの動作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonPaste_Click(object sender, EventArgs e)
    {
        if (Clipboard.GetDataObject().GetDataPresent(typeof(string)))
        {
            try
            {
                graphControl.Profile = new Profile();
                graphControl.LineList = null;

                var str1 = (string)Clipboard.GetDataObject().GetData(typeof(string));

                var str2 = str1.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                var profile = new Profile();
                for (int i = 0; i < str2.Length; i++)
                {
                    var str3 = str2[i].Split(new[] { '\t' });
                    profile.Pt.Add(new Crystallography.PointD(Convert.ToDouble(str3[0]), Convert.ToDouble(str3[1])));
                }
                graphControl.LineList = new PointD[] { new PointD(trackBarAdvancedFront.Value, double.NaN), new PointD(trackBarAdvancedBack.Value, double.NaN) };

                graphControl.Profile = profile;

                trackBarAdvancedFront.Minimum = profile.Pt[0].X;
                trackBarAdvancedBack.Minimum = profile.Pt[0].X;
                trackBarAdvancedFront.Maximum = profile.Pt[^1].X;
                trackBarAdvancedBack.Maximum = profile.Pt[^1].X;
            }
            catch
            {
                return;
            }
        }
    }

    private bool skipEvent = false;

    private bool trackBarAdvancedBack_ValueChanged(object sender, double value)
    {
        if (skipEvent)
            return true;

        if (graphControl.Profile != null && graphControl.Profile.Pt.Count > 2)
        {
            skipEvent = true;

            graphControl.LineList = new PointD[] { new PointD(trackBarAdvancedFront.Value, double.NaN), new PointD(trackBarAdvancedBack.Value, double.NaN) };
            graphControl.Draw();
            skipEvent = false;
        }

        return true;
    }

    private void graphControl1_LinePositionChanged()
    {
        if (skipEvent) return;

        skipEvent = true;
        trackBarAdvancedFront.Value = graphControl.LineList[0].X;
        trackBarAdvancedBack.Value = graphControl.LineList[1].X;
        skipEvent = false;
    }

    public void SetCrystal()
    {
        CrystalControl.GenerateFromInterface();
        OriginalCrystal = Deep.Copy(CrystalControl.Crystal);
        OriginalCrystal.SetAxis();

        CrystalControl.SymmetrySeriesNumber = 1;
        CrystalControl.Crystal.Atoms = Array.Empty<Atoms>();
        for (int i = 0; i < OriginalCrystal.Atoms.Length; i++)
            for (int j = 0; j < OriginalCrystal.Atoms[i].Atom.Count; j++)
            {
                var atom = Deep.Copy(OriginalCrystal.Atoms[i]);
                atom.X = OriginalCrystal.Atoms[i].Atom[j].X;
                atom.Y = OriginalCrystal.Atoms[i].Atom[j].Y;
                atom.Z = OriginalCrystal.Atoms[i].Atom[j].Z;
                CrystalControl.Crystal.AddAtoms(atom);
            }
        CrystalControl.SetToInterface();
    }

    public void RevertCrystal()
    {
        CrystalControl.Crystal = OriginalCrystal;

        CrystalControl.SetToInterface();
        FormDiffractionSimulator.SetVector();
        FormDiffractionSimulator.Draw();
    }

    private readonly Stopwatch sw = new Stopwatch();

    /// <summary>
    /// 実行ボタン
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonExecute_Click(object sender, EventArgs e)
    {
        if (graphControl.Profile == null)
            return;
        //各種パラメータをセット
        setParameter();
        //まず、初期状態の結晶の軸ベクトルなどをセット
        SetCrystal();

        int count = checkBoxOmegaStep.Checked ? numericBoxOmegaTimes.ValueInteger : 1;
        var initialRot = new Matrix3D(FormDiffractionSimulator.formMain.Crystal.RotationMatrix);
        var path = (folderBrowserDialog.SelectedPath.Length == 0 ? "" : folderBrowserDialog.SelectedPath + "\\") + textBoxFileName.Text;
        sw.Restart();

        for (int i = 0; i < count; i++)
        {
            var filename = "";
            if (checkBoxSaveSimulatedPattern.Checked)
                filename = count == 1 ? path + ".tif" : path + "(omega=" + (numericBoxOmegaStep.Value * i).ToString() + ").tif";

            FormDiffractionSimulator.formMain.Crystal.RotationMatrix = Matrix3D.Rot(new Vector3DBase(1, 0, 0), numericBoxOmegaStep.RadianValue * i) * initialRot;
            execute(filename);
        }
        FormDiffractionSimulator.formMain.Crystal.RotationMatrix = initialRot;

        RevertCrystal();
        toolStripStatusLabel1.Text = "Elapsed time: " + (sw.ElapsedMilliseconds / 1000.0).ToString("f1") + " sec., Completed!";
    }

    private void execute(string filename = "")
    {
        var n = 0;
        var initialRot = new Matrix3D(FormDiffractionSimulator.formMain.Crystal.RotationMatrix);
        var originalAxes = new Matrix3D(OriginalCrystal.A_Axis * 10, OriginalCrystal.B_Axis * 10, OriginalCrystal.C_Axis * 10);

        //データ数を減らす
        var numMax = 4;
        var ptTemp = graphControl.Profile.Pt;
        var pt = new List<PointD>();
        for (int i = 0; i < ptTemp.Count; i += numMax)
        {
            var num = Math.Min(numMax, ptTemp.Count - i);
            var p = new PointD();
            for (int j = i; j < i + num; j++)
                p += ptTemp[j];
            pt.Add(p / num);
        }

        //var pt = graphControl.Profile.Pt;

        var states = new List<(double depth, double pressure, double thickness)>();
        for (int i = 0; i < pt.Count - 1; i++)
            states.Add(((pt[i].X + pt[i + 1].X) / 2, (pt[i].Y + pt[i + 1].Y) / 2, pt[i + 1].X - pt[i].X));

        int count = checkBoxOmegaStep.Checked ? numericBoxOmegaTimes.ValueInteger : 1;
        toolStripProgressBar.Maximum = states.Count * count;
        toolStripProgressBar.Value = 0;


        //無回転の時の圧縮面の法線ベクトル
        int cH = numericBoxShockedPlaneH.ValueInteger, cK = numericBoxShockedPlaneK.ValueInteger, cL = numericBoxShockedPlaneL.ValueInteger;
        var compressedPlane = (cH * OriginalCrystal.A_Star + cK * OriginalCrystal.B_Star + cL * OriginalCrystal.C_Star).Normarize();
        //無回転の時の圧縮面の法線ベクトルを(1,0,0)方向に向ける回転ベクトル
        var m1 = Matrix3D.Rot(Vector3DBase.VectorProduct(compressedPlane, new Vector3DBase(1, 0, 0)), Math.Acos(compressedPlane.X));
        //無回転時の圧縮面と平行なベクトル
        (int h, int k, int l, Vector3DBase vec) v1 = (-cK, cH, 0, (-cK * OriginalCrystal.A_Axis + cH * OriginalCrystal.B_Axis).Normarize());
        (int h, int k, int l, Vector3DBase vec) v2 = (0, -cL, cK, (-cL * OriginalCrystal.B_Axis + cK * OriginalCrystal.C_Axis).Normarize());
        if (v1.vec.Length2 < 0.1)
            v1 = (-cL, 0, cH, (-cL * OriginalCrystal.A_Axis + cH * OriginalCrystal.C_Axis).Normarize());
        if (v2.vec.Length2 < 0.1)
            v2 = (-cL, 0, cH, (-cL * OriginalCrystal.A_Axis + cH * OriginalCrystal.C_Axis).Normarize());

        //回転状態の圧縮面の法線ベクトル
        var shockDirection = initialRot * compressedPlane;

        //無回転の時のスリップ面の法線ベクトル
        var slipPlane = (numericBoxSlipPlaneH.Value * OriginalCrystal.A_Star + numericBoxSlipPlaneK.Value * OriginalCrystal.B_Star + numericBoxSlipPlaneL.Value * OriginalCrystal.C_Star).Normarize();
        //回転時のスリップ面と圧縮面の外積ベクトル
        var rotationAxis2019 = Vector3DBase.AngleBetVectors(slipPlane, compressedPlane) < Math.PI / 2 ?
            initialRot * Vector3DBase.VectorProduct(slipPlane, compressedPlane) :
            initialRot * Vector3DBase.VectorProduct(compressedPlane, slipPlane);
        //(0,0,1)方向をrotationAxis2019に向ける回転ベクトル
        var m2 = Matrix3D.Rot(Vector3DBase.VectorProduct(new Vector3DBase(0, 0, 1), rotationAxis2019), Math.Acos(rotationAxis2019.Z));

        //2018モデル。結晶は圧縮軸に垂直な方向を回転軸として回転すると仮定。その回転軸のリストを設定
        List<Vector3DBase> rotationAxes2018 = new List<Vector3DBase>();
        var div1 = numericBoxDivisionOfRotationAxis.ValueInteger;//回転軸を分割する数
        var initialNormalDirection = Math.Abs(shockDirection.Z) < 0.5 ? new Vector3DBase(shockDirection.Y, -shockDirection.X, 0) : new Vector3DBase(0, -shockDirection.Z, shockDirection.Y);
        for (int rot = 0; rot < div1; rot++)
            rotationAxes2018.Add(Matrix3D.Rot(shockDirection, 2 * Math.PI * rot / div1) * initialNormalDirection);

        var div2 = numericBoxDivisionOfRotationAngle.ValueInteger;//回転量を分割する数

        double front = graphControl.LineList[0].X, back = graphControl.LineList[1].X;
        double omegaC = numericBoxCompressedOmega.RadianValue, sigmaOmegaC = numericBoxCompressedOmegaSigma.RadianValue;
        double omegaR = numericBoxReleasedOmega.RadianValue, sigmaOmegaR = numericBoxReleasedOmegaSigma.RadianValue;
        double thetaC_A = numericBoxCompressedThetaA.RadianValue, thetaC_B = numericBoxCompressedThetaB.RadianValue;
        double thetaR_A = numericBoxReleasedThetaA.RadianValue, thetaR_B = numericBoxReleasedThetaB.RadianValue;
        var absorption = 1.0;
        var compiledImage = new double[FormDiffractionSimulator.FormDiffractionSimulatorGeometry.DetectorWidth * FormDiffractionSimulator.FormDiffractionSimulatorGeometry.DetectorHeight];
        var spots = new List<SpotInformation>();
        long beforeSec = 0;
        //メインループ開始
        for (int i = 0; i < states.Count; i++)
        {
            FormDiffractionSimulator.SkipDrawing = FormDiffractionSimulator.formMain.SkipDrawing = checkBoxSkipDrawing.Checked;
            var rotationList = new List<(Matrix3D rot, double weight)>();

            if (states[i].depth < front)//Uncompressedの時
                rotationList.Add((Matrix3D.IdentityMatrix, 1));
            else
            {
                var time1 = states[i].depth < back ? (states[i].depth - front) / numericBoxUp.Value : (back - front) / numericBoxUp.Value;
                var time2 = states[i].depth < back ? 0.0 : (states[i].depth - back) / numericBoxUr.Value;
                var omega = omegaC * time1 + omegaR * time2;
                var range = 2.0;

                if (radioButton2018Model.Checked)//2018モデルの時
                {
                    var sigmaOmega = Math.Sqrt(time1 * sigmaOmegaC * time1 * sigmaOmegaC + time2 * sigmaOmegaR * time2 * sigmaOmegaR);

                    double start = Math.Max(0, omega - range * sigmaOmega), end = omega + range * sigmaOmega, step = (end - start) / div2;
                    var rotationAngles = new List<PointD>();
                    for (var speed = start; speed <= end + step / 2; speed += step)
                        rotationAngles.Add(new PointD(speed, Math.Exp(-(speed - omega) * (speed - omega) / 2 / sigmaOmega / sigmaOmega)));
                    foreach (var axis in rotationAxes2018)
                        foreach (var angle in rotationAngles)
                            rotationList.Add((Matrix3D.Rot(axis, angle.X), angle.Y));
                }
                else//2019モデルの時
                {
                    double sigmaTheta = states[i].depth < back ? thetaC_A + thetaC_B * time1 : thetaC_A * time1 + thetaR_A + thetaR_B * time2, sigmaTheta2 = sigmaTheta * sigmaTheta;
                    double sigmaOmega = sigmaOmegaC * time1 + sigmaOmegaR * time2, sigmaOmega2 = sigmaOmega * sigmaOmega;

                    double stepTheta = (range * sigmaTheta) / ((int)Math.Sqrt(div1) + 1);
                    double startR = Math.Max(0, omega - range * sigmaOmega), endR = omega + range * sigmaOmega, stepR = (endR - startR) / div2;
                    double stepPhi = (2 * Math.PI) / ((int)Math.Sqrt(div1) + 1);
                    if (stepTheta == 0)
                        stepTheta = double.PositiveInfinity;
                    if (stepR == 0)
                        stepR = double.PositiveInfinity;

                    for (var theta = stepTheta / 2; theta <= range * sigmaTheta; theta += stepTheta)
                        for (var phi = 0.0; phi < 2 * Math.PI; phi += stepPhi)
                            for (var r = startR; r <= endR; r += stepR)
                            {
                                var v = new Vector3DBase(Math.Sin(theta) * Math.Cos(phi), Math.Sin(theta) * Math.Sin(phi), Math.Cos(theta));
                                rotationList.Add((Matrix3D.Rot(m2 * v, r), Math.Exp(-(r - omega) * (r - omega) / 2 / sigmaOmega2 - theta * theta / 2 / sigmaTheta2) * r * r * Math.Sin(theta)));
                            }
                }
            }
            var sum = rotationList.Sum(r => r.weight);
            rotationList = rotationList.Select(r => (r.rot, r.weight / sum)).ToList();

            //ここから、圧縮による格子定数の変化
            Matrix3D newAxes = originalAxes;
            if (states[i].depth >= front)
            {
                var volume = EOS.InverseThirdBirchMurnaghan(numericBoxEOS_K0.Value, numericBoxEOS_Kprime.Value, states[i].pressure * 100);
                if ((states[i].depth < back && radioButtonCompressedUniaxial.Checked) || (states[i].depth >= back && radioButtonReleasedUniaxial.Checked))
                    newAxes = m1.Inverse() * new Matrix3D(1 / volume, 0, 0, 0, 1, 0, 0, 0, 1) * m1 * originalAxes;
                else
                    newAxes = new Matrix3D(Math.Pow(1 / volume, 1.0 / 3.0), 0, 0, 0, Math.Pow(1 / volume, 1.0 / 3.0), 0, 0, 0, Math.Pow(1 / volume, 1.0 / 3.0)) * originalAxes;
            }
            var a = new Vector3DBase(newAxes.E11, newAxes.E21, newAxes.E31);
            var b = new Vector3DBase(newAxes.E12, newAxes.E22, newAxes.E32);
            var c = new Vector3DBase(newAxes.E13, newAxes.E23, newAxes.E33);
            FormDiffractionSimulator.SkipDrawing = FormDiffractionSimulator.formMain.SkipDrawing = true;
            CrystalControl.CellConstants = (a.Length, b.Length, c.Length, Vector3DBase.AngleBetVectors(b, c), Vector3DBase.AngleBetVectors(c, a), Vector3DBase.AngleBetVectors(a, b));

            var corrRot = newAxes * new Matrix3D(CrystalControl.Crystal.A_Axis * 10, CrystalControl.Crystal.B_Axis * 10, CrystalControl.Crystal.C_Axis * 10).Inverse();

            // ここまで

            //候補となるgの絞り込み
            FormDiffractionSimulator.SetVector(true);
            List<Vector3D> gVector = new List<Vector3D>();
            foreach (var g in FormDiffractionSimulator.formMain.Crystal.VectorOfG.Where(g => g.Flag1 && g.RelativeIntensity > 1E-6))
            {
                var vec = initialRot * corrRot * g;
                vec.Y = -vec.Y; vec.Z = -vec.Z;//ここでベクトルのY,Zの符号を反転
                if (-sinTau * vec.Y + cosTau * (vec.Z + ewaldRadius) > 0)//(vec.X, vec.Y, vec.Z + EwaldRadius) と(0, -sinTau, cosTau) の内積が0より大きい)
                {
                    //2020/02/03の変更対処済み
                    var point = Geometriy.GetCrossPoint(0, -sinTau, cosTau, cameraLength2, new Vector3D(0, 0, 0), new Vector3D(vec.X, vec.Y, vec.Z + ewaldRadius));
                    if (IsDetectorArea(new PointD(point.X, point.Y * cosTau + point.Z * sinTau)))
                        gVector.Add(g);
                }
            }//ここまで

            //ここから揺動のループ
            var effectiveThickness = states[i].thickness / (shockDirection * new Vector3DBase(0, 0, 1));
            absorption *= Math.Exp(-numericBoxMassAbsorption.Value * FormDiffractionSimulator.formMain.Crystal.Density * effectiveThickness / 10000);
            foreach (var (rot, weight) in rotationList)
            {
                var temp = GetDiffractionSpots(rot * initialRot * corrRot, gVector, states[i].thickness * absorption * absorption * weight);//入射と出射で二回absorptionをかける
                spots.AddRange(temp);
                if (n++ % 100 == 0 && !checkBoxSkipDrawing.Checked)
                    FormDiffractionSimulator.formMain.SetRotation(rot * initialRot * corrRot);
            }

            // if (spots.Count > 100 || i == states.Count - 1)
            {
                var image = getImageData(spots);
                for (int j = 0; j < compiledImage.Length; j++)
                    compiledImage[j] += image[j];
                spots.Clear();
            }

            toolStripProgressBar.Value++;
            toolStripStatusLabel1.Text =
                "Time elapsed: " + (sw.ElapsedMilliseconds / 1000.0).ToString("f1") + " sec., per slice: " + (sw.ElapsedMilliseconds - beforeSec).ToString() + " msec., wait for more " +
                (sw.ElapsedMilliseconds / 1000.0 / toolStripProgressBar.Value * (toolStripProgressBar.Maximum - toolStripProgressBar.Value)).ToString("f1") + " sec.";
            beforeSec = sw.ElapsedMilliseconds;
            Application.DoEvents();
        }//メインループここまで

        compiledImage = compiledImage.Select(intensity => intensity * 1E6).ToArray();
        PseudoBitmap pseud = new PseudoBitmap(compiledImage, FormDiffractionSimulator.FormDiffractionSimulatorGeometry.DetectorWidth);

        Tiff.Writer("temp.tif", compiledImage, 2, FormDiffractionSimulator.FormDiffractionSimulatorGeometry.DetectorWidth);
        FormDiffractionSimulator.FormDiffractionSimulatorGeometry.ReadImage("temp.tif");

        if (filename != "") File.Copy("temp.tif", filename, true);

        FormDiffractionSimulator.SkipDrawing = FormDiffractionSimulator.formMain.SkipDrawing = false;
    }

    private double[] getImageData(List<SpotInformation> spots)
    {
        //配列作成
        var fdsg = FormDiffractionSimulator.FormDiffractionSimulatorGeometry;
        //Src (mm)をピクセル座標に変換
        var convSrcToPixelInt = new Func<PointD, Point>(p => new Point((int)(p.X / fdsg.DetectorPixelSize + fdsg.FootX + 0.5), (int)(p.Y / fdsg.DetectorPixelSize + fdsg.FootY + 0.5)));
        //Pixel座標をSrc座標に変換
        var convPixelToSrc = new Func<int, int, PointD>((x, y) => new PointD((x - fdsg.FootX) * fdsg.DetectorPixelSize, (y - fdsg.FootY) * fdsg.DetectorPixelSize));

        int width = fdsg.DetectorWidth, height = fdsg.DetectorHeight;
        double[] data = new double[width * height];
        if (spots.Count == 0)
            return data;

        var sigma = spots[0].Sigma;
        int range = 2;// (int)(s.Sigma * / fdsg.DetectorPixelSize + 0.5);//時間がかかりすぎる。どうするか。。。
        int range2 = range * range;
        var coeff1 = 1 / 2 / sigma / sigma;

        foreach (var s in spots)
        {
            var center = new PointD(s.X, s.Y);//ピクセル位置
            var centerPix = convSrcToPixelInt(center);
            int centerX = centerPix.X, centerY = centerPix.Y;
            int yMin = Math.Max(0, centerY - range), yMax = Math.Min(height, centerY + range + 1);
            int xMin = Math.Max(0, centerX - range), xMax = Math.Min(width, centerX + range + 1);
            for (int y = yMin; y < yMax; y++)
                for (int x = xMin; x < xMax; x++)
                {
                    if ((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY) <= range2)
                    {
                        double dev2 = (center - convPixelToSrc(x, y)).Length2;
                        data[y * width + x] += s.Intensity * Math.Exp(-dev2 * coeff1);
                    }
                }
        }

        data = ImageProcess.GaussianBlurFast(data, width, sigma / fdsg.DetectorPixelSize / 2);//半値幅なので、ちゃんと計算しなければ

        return data;
    }

    private double cosTau, sinTau, excitationError, ewaldRadius, cameraLength2, waveLength;
    private double spotRadiusOnDetector, error2, error3, sqrt2PI = Math.Sqrt(2 * Math.PI);

    private void buttonSetFolder_Click(object sender, EventArgs e) => folderBrowserDialog.ShowDialog();

    private void checkBoxSaveSimulatedPattern_CheckedChanged(object sender, EventArgs e) => flowLayoutPanelSavePatterns.Enabled = checkBoxSaveSimulatedPattern.Checked;

    private void checkBoxOmegaStep_CheckedChanged(object sender, EventArgs e) => flowLayoutPanelOmegaStep.Enabled = checkBoxOmegaStep.Checked;

    private void radioButton2019Model_CheckedChanged(object sender, EventArgs e) => groupBoxSlipPlane.Enabled = numericBoxCompressedThetaA.Enabled = numericBoxCompressedThetaB.Enabled =
            numericBoxReleasedThetaA.Enabled = numericBoxReleasedThetaB.Enabled = radioButton2019Model.Checked;

    private void FormDiffractionSimulatorDynamicCompression_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        this.Visible = false;
    }

    private void FormDiffractionSimulatorDynamicCompression_DragEnter(object sender, DragEventArgs e) => e.Effect = (e.Data.GetData(DataFormats.FileDrop) != null) ? DragDropEffects.Copy : DragDropEffects.None;

    private void FormDiffractionSimulatorDynamicCompression_DragDrop(object sender, DragEventArgs e)
    {
        string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        if (fileName.Length == 1 && fileName[0].ToLower().EndsWith("txt"))
        {
            var sr = new StreamReader(fileName[0]);
            string line;
            Profiles = new Dictionary<double, Profile>();

            while ((line = sr.ReadLine()) != null)
            {
                var values = line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length == 3 &&
                    double.TryParse(values[0], out double pos) &&
                    double.TryParse(values[1], out double time) &&
                    double.TryParse(values[2], out double pressure))
                {
                    if (!Profiles.ContainsKey(time))
                        Profiles.Add(time, new Profile());
                    Profiles[time].Pt.Add(new PointD(pos, pressure));
                }
            }
            if (Profiles.Count > 1)
            {
                trackBarAdvancedTime.Minimum = Profiles.Keys.Min();
                trackBarAdvancedTime.Maximum = Profiles.Keys.Max();
                trackBarAdvancedTime.Value = Profiles.Keys.First();
            }
        }
    }

    private bool trackBarAdvancedTime_ValueChanged(object sender, double value)
    {
        if (Profiles == null || Profiles.Count == 0)
            return false;

        if (!Profiles.ContainsKey(trackBarAdvancedTime.Value))
        {
            var dev = double.MaxValue;
            var time = Profiles.Keys.First();
            foreach (var t in Profiles.Keys)
            {
                if (Math.Abs(t - trackBarAdvancedTime.Value) < dev)
                {
                    dev = Math.Abs(t - trackBarAdvancedTime.Value);
                    time = t;
                }
            }

            bool flag = trackBarAdvancedTime.SkipNumericBoxEvent;
            trackBarAdvancedTime.SkipNumericBoxEvent = false;
            trackBarAdvancedTime.Value = time;
            trackBarAdvancedTime.SkipNumericBoxEvent = flag;
        }

        graphControl.Profile = Profiles[trackBarAdvancedTime.Value];

        trackBarAdvancedFront.Minimum = graphControl.Profile.Pt[0].X;
        trackBarAdvancedBack.Minimum = graphControl.Profile.Pt[0].X;
        trackBarAdvancedFront.Maximum = graphControl.Profile.Pt[^1].X;
        trackBarAdvancedBack.Maximum = graphControl.Profile.Pt[^1].X;

        return false;
    }

    private void setParameter()
    {
        excitationError = FormDiffractionSimulator.ExcitationError;
        ewaldRadius = FormDiffractionSimulator.EwaldRadius;
        cameraLength2 = FormDiffractionSimulator.CameraLength2;
        waveLength = FormDiffractionSimulator.WaveLength;
        cosTau = FormDiffractionSimulator.CosTau;
        sinTau = FormDiffractionSimulator.SinTau;
        spotRadiusOnDetector = cameraLength2 * Math.Tan(2 * Math.Asin(waveLength * excitationError / 2));
        error2 = excitationError * excitationError;
        error3 = excitationError * excitationError * excitationError;
    }

    /// <summary>
    /// 回折スポットおよび指数ラベルの描画
    /// </summary>
    /// <param name="graphics">描画対象のグラフィックオブジェクト</param>
    /// <param name="drawLabel">ラベルを描画するかどうか</param>
    /// <param name="outputOnlySpotInformation">このフラグがTrueの場合は、画面描画は行わずにspotの情報だけを返す</param>
    public List<SpotInformation> GetDiffractionSpots(Matrix3D rotation, IEnumerable<Vector3D> gVector, double coeff)
    {
        var spotInformation = new List<SpotInformation>();
        double sigma = spotRadiusOnDetector;
        //描画するスポットを決める
        foreach (Vector3D g in gVector)
        {
            Vector3DBase vec = rotation * g;
            if (vec.Z > -3 * excitationError)
            {
                vec.Y = -vec.Y; vec.Z = -vec.Z;//ここでベクトルのY,Zの符号を反転
                double L2 = vec.X * vec.X + vec.Y * vec.Y;
                double dev = ewaldRadius - Math.Sqrt(L2 + (vec.Z + ewaldRadius) * (vec.Z + ewaldRadius)), dev2 = dev * dev;
                if (dev2 < 9 * error2)
                {
                    if (-sinTau * vec.Y + cosTau * (vec.Z + ewaldRadius) > 0)//(vec.X, vec.Y, vec.Z + EwaldRadius) と(0, -sinTau, cosTau) の内積が0より大きい)
                    {
                        //2020/02/03の変更対処済み
                        var point = Geometriy.GetCrossPoint(0, sinTau, -cosTau, cameraLength2, new Vector3D(0, 0, 0), new Vector3D(vec.X, vec.Y, vec.Z + ewaldRadius));
                        PointD pt = new PointD(point.X, point.Y * cosTau + point.Z * sinTau);

                        //if (IsDetectorArea(pt))
                        {
                            double intensity = g.RelativeIntensity / (sigma * sqrt2PI) * Math.Exp(-dev2 / error2 / 2);
                            if (intensity > 1E-3)
                                spotInformation.Add(new SpotInformation(pt.X, pt.Y, intensity * coeff, sigma));
                        }
                    }
                }
            }
        }
        return spotInformation;
    }

    private PointD startArea = new PointD(double.NaN, double.NaN), endArea;

    /// <summary>
    /// フィルム座標で与えられたptが、検出器内に含まれるかどうかを返す。OverlapPivture機能がOFFの場合は、常に検出器設定が
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    public bool IsDetectorArea(PointD pt)
    {
        if (startArea.IsNaN)
        {
            var fdsg = FormDiffractionSimulator.FormDiffractionSimulatorGeometry;
            startArea = new PointD(-fdsg.DetectorPixelSize * fdsg.FootX, -fdsg.DetectorPixelSize * fdsg.FootY);
            endArea = new PointD(fdsg.DetectorPixelSize * (fdsg.DetectorWidth - fdsg.FootX), fdsg.DetectorPixelSize * (fdsg.DetectorHeight - fdsg.FootY));
        }
        return pt.X > startArea.X && pt.Y > startArea.Y && pt.X < endArea.X && pt.Y < endArea.Y;
    }

    public struct SpotInformation
    {
        public double X, Y, Intensity, Sigma;

        public SpotInformation(double x, double y, double intensity, double sigma)
        { X = x; Y = y; Intensity = intensity; Sigma = sigma; }
    }
}
