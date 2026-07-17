using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ReciPro;

public partial class FormPolycrystallineDiffractionSimulator : FormBase
{
    public List<Crystal> Crystals;

    private Stopwatch Sw = new();
    public FormMain formMain;

    private (byte R, byte G, byte B)[] scale;
    private bool isNegative, isGray;

    private readonly List<DiffractionPatternControl> diffractionPatternControl = [];//260718Cl 現代化: new List<T>() → collection expression
    private readonly List<TabPage> tabPage = [];

    private readonly Stopwatch sw = new();

    public string currentPath = "";

    public string CurrentStatus
    {
        set
        {
            if (this.InvokeRequired)//別スレッドから呼び出されたとき
            {
                this.Invoke(() => CurrentStatus = value);//260718Cl 現代化: callBack delegate → lambda
                return;
            }
            toolStripStatusLabel.Text = value;
            if (value.Contains("film blur") || value.Contains("center position") || value.Contains("background"))
            {
                ProgressBarVisible = false;
                CurrentProgress = "";
            }
        }
        get => toolStripStatusLabel.Text;
    }

    public string CurrentProgress
    {
        set
        {
            if (this.InvokeRequired)//別スレッドから呼び出されたとき
            {
                this.Invoke(() => CurrentProgress = value);
                return;
            }
            toolStripStatusLabelProgress.Text = value;
        }
        get => toolStripStatusLabelProgress.Text;
    }

    public bool ProgressBarVisible
    {
        set
        {
            if (this.InvokeRequired)//別スレッドから呼び出されたとき
            {
                this.Invoke(() => ProgressBarVisible = value);
                return;
            }
            toolStripProgressBar.Visible = value;
        }
        get => toolStripProgressBar.Visible;
    }

    private int processorCount = System.Environment.ProcessorCount;

    public FormPolycrystallineDiffractionSimulator()
    {
        InitializeComponent();
        skipEvent = true;
        comboBoxGradient.SelectedIndex = 0;
        comboBoxScale1.SelectedIndex = 1;
        comboBoxScale2.SelectedIndex = 0;
        skipEvent = false;
        //crystalControl1.DefaultTabNumber = 4;
        setScale();
        diffractionPatternControlSimulation.ProgressChanged += dpc_ProgressChanged;
    }

    private void FormPolycrystallineDiffractionSimulator_Load(object sender, EventArgs e) =>
        //formMainのlistBoxのselectedIndexの変更イベントを登録
        formMain.listBox.SelectedIndexChanged += listBox_SelectedIndexChanged;

    private List<CrystalControl> CrystalContlols = new();

    //formMainのlistBoxのselectedIndexが変更されたとき
    private void listBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        tabControlCrystals.TabPages.Clear();
        CrystalContlols.Clear();
        Crystals = new List<Crystal>();
        int n = 0;
        foreach (Crystal c in formMain.listBox.SelectedItems)
        {
            var tab = new TabPage();
            var crystalControl = new CrystalControl
            {
                Crystal = c,
                Dock = DockStyle.Fill,
                VisibleAtomTab = false,
                VisibleBondsPolyhedraTab = false,
                VisibleEOSTab = false,
                VisiblePolycrystallineTab = true,
                VisibleStressStrainTab = true,
                DefaultTabNumber = 4
            };
            tab.Controls.Add(crystalControl);
            tab.Text = c.Name;
            tab.Padding = new Padding(3, 2, 3, 2);

            tabControlCrystals.TabPages.Add(tab);
            CrystalContlols.Add(crystalControl);

            c.id = n++;
            Crystals.Add(c);

            crystalControl.CrystalChanged += crystalControl_CrystalChanged;
        }

        //listBox.SelectedIndex = 0;
    }

    private void crystalControl_CrystalChanged(object sender, EventArgs e)
    {
        Crystals[(sender as CrystalControl).Crystal.id] = (sender as CrystalControl).Crystal;
    }
    private void FormPolycrystallineDiffractionSimulator_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        formMain.toolStripButtonDiffractionPoly.Checked = false;
        this.Visible = false;
    }


    #region View関連

    private void comboBoxScale_SelectedIndexChanged(object sender, EventArgs e) => setScale();

    private void setScale()
    {
        if (skipEvent) return;
        isNegative = comboBoxGradient.SelectedIndex == 1;

        //スケールをセット
        if (comboBoxScale1.SelectedIndex == 0)//ログスケール
            if (comboBoxScale2.SelectedIndex == 0)//グレー
            {
                scale = PseudoBitmap.ColorScaleGrayLog;
                isGray = true;
            }
            else
            {
                scale = PseudoBitmap.ColorScaleColdWarmLog;
                isGray = false;
            }
        else//リニア
            if (comboBoxScale2.SelectedIndex == 0)//グレー
        {
            scale = PseudoBitmap.ColorScaleGrayLiner;
            isGray = true;
        }
        else//color
        {
            scale = PseudoBitmap.ColorScaleColdWarmLiner;
            isGray = false;
        }
        diffractionPatternControlSimulation.SetScale(scale, isNegative, isGray);
        for (int i = 0; i < diffractionPatternControl.Count; i++)
            diffractionPatternControl[i].SetScale(scale, isNegative, isGray);
    }

    #endregion View関連

    #region リファレンス画像追加/削除関連

    private void buttonAddRefferencePattern_Click(object sender, EventArgs e)
    {
        OpenFileDialog dlg = new OpenFileDialog { Filter = "*.ipa|*.ipa" };
        if (dlg.ShowDialog() == DialogResult.OK)
            if (dlg.FileName.EndsWith("ipa"))
                addRefferencePattern(dlg.FileName);
    }

    private void addRefferencePattern(string fileName)
    {
        var serializer = new XmlSerializer(typeof(ImageIO.IPAImage));
        var fs = new FileStream(fileName, System.IO.FileMode.Open);//ファイルを開く
        var ipa = (ImageIO.IPAImage)serializer.Deserialize(fs);

        int n = 0;
        double[] pixels;
        if (ipa.Intensity != null)
        {
            pixels = new double[ipa.Intensity.Length];
            for (int y = 0; y < ipa.Height; y++)
                for (int x = 0; x < ipa.Width; x++)
                    pixels[n++] = ipa.Intensity[y * ipa.Width + x] * ipa.Scale;
        }
        else//新しいipa形式ではIntensityDoubleを使う (2015/5/13修正)
        {
            pixels = new double[ipa.IntensityDouble.Length];
            for (int y = 0; y < ipa.Height; y++)
                for (int x = 0; x < ipa.Width; x++)
                    pixels[n++] = ipa.IntensityDouble[y * ipa.Width + x];
        }

        tabPage.Add(new TabPage());
        diffractionPatternControl.Add(new DiffractionPatternControl());

        int index = diffractionPatternControl.Count - 1;
        diffractionPatternControl[index].ProgressChanged += dpc_ProgressChanged;
        diffractionPatternControl[index].SetImage(pixels, ipa.Width, ipa.Center, ipa.Resolution, scale);
        diffractionPatternControl[index].IsReferrenceImage = true;
        diffractionPatternControl[index].Center = ipa.Center;
        if (ipa.Center.X == 0 && ipa.Center.Y == 0)
            diffractionPatternControl[index].Center = new PointD(ipa.Width / 2, ipa.Height / 2);

        diffractionPatternControl[index].Cameralength = ipa.CameraLength;
        diffractionPatternControl[index].WaveProperty = ipa.WaveProperty;
        //  waveLengthControl.WaveSource = ipa.WaveProperty.Source;
        //  waveLengthControl.WaveLength = ipa.WaveProperty.XrayWaveLength;
        diffractionPatternControl[index].ImageHeight = ipa.Height;
        diffractionPatternControl[index].ImageWidth = ipa.Width;
        diffractionPatternControl[index].Resolution = ipa.Resolution;

        diffractionPatternControl[index].FilmBlur = ipa.FilmBlur != 0 ? ipa.FilmBlur : 200;

        listBoxReferrence.Items.Add("#" + diffractionPatternControl.Count.ToString("d2") + ": " + Path.GetFileName(fileName));

        diffractionPatternControl[diffractionPatternControl.Count - 1].Dock = DockStyle.Fill;
        tabPage[^1].Controls.Add(diffractionPatternControl[^1]);
        tabPage[^1].Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
        tabPage[^1].Text = "#" + diffractionPatternControl.Count.ToString("d2") + ": " + Path.GetFileName(fileName);

        tabControl1.TabPages.Add(tabPage[^1]);
        tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;

        fs.Close();
        currentPath = Path.GetDirectoryName(fileName);
    }

    private void buttonRemoveReferrencePattern_Click(object sender, EventArgs e)
    {
        if (listBoxReferrence.SelectedIndex > -1)
        {
            diffractionPatternControl.RemoveAt(listBoxReferrence.SelectedIndex);
            tabPage.RemoveAt(listBoxReferrence.SelectedIndex);
            tabControl1.TabPages.RemoveAt(listBoxReferrence.SelectedIndex + 1);
            listBoxReferrence.Items.RemoveAt(listBoxReferrence.SelectedIndex);

            for (int i = 0; i < diffractionPatternControl.Count; i++)
            {
                tabPage[i].Text = "#" + (i + 1).ToString("d2") + tabPage[i].Text.Remove(0, 3);
                listBoxReferrence.Items[i] = "#" + (i + 1).ToString("d2") + tabPage[i].Text.Remove(0, 3);
            }
        }
    }

    private bool skipEvent = false;

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (skipEvent) return;
        if (tabControl1.SelectedIndex > 0)
            listBoxReferrence.SelectedIndex = tabControl1.SelectedIndex - 1;
    }

    private void listBoxReferrence_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (skipEvent) return;
        if (listBoxReferrence.SelectedIndex > -1)
            tabControl1.SelectedIndex = listBoxReferrence.SelectedIndex + 1;
    }

    #endregion リファレンス画像追加/削除関連

    private void buttonSearch_Click(object sender, EventArgs e)
    {
        if (buttonSearch.Text != "Stop!" && backgroundWorkerMain.IsBusy == false)
        {
            sw.Restart();//260718Cl 変更: new Stopwatch()+Start() の再代入を Restart に (readonly 化に伴う)

            if (setting == null || graphControlResidual.Profile == null || graphControlResidual.Profile.Pt.Count == 0)
                graphControlResidual.Profile = new Profile();
            buttonSearch.Text = "Stop!";
            backgroundWorkerMain.RunWorkerAsync();

            for (int i = 0; i < diffractionPatternControl.Count; i++)
                diffractionPatternControl[i].ContolEnabled(false);
        }
        else
        {
            backgroundWorkerMain.CancelAsync();
            buttonSearch.Text = "Search!";
            for (int i = 0; i < diffractionPatternControl.Count; i++)
                diffractionPatternControl[i].ContolEnabled(true);
        }
    }

    private RefinmentSetting setting = null;

    private void backgroundWorkerMain_DoWork(object sender, DoWorkEventArgs e)
    {
        Random rn = new Random(System.Environment.TickCount);

        if (setting == null)
            setRefinmentSetting();

        double residualInitial = 0;
        int totalPixel = 0;

        for (int i = 0; i < diffractionPatternControl.Count; i++)
        {
            diffractionPatternControl[i].Crystals = new List<Crystal>();
            for (int j = 0; j < Crystals.Count; j++)
                diffractionPatternControl[i].Crystals.Add(Crystals[j]);

            for (int y = 0; y < diffractionPatternControl[i].ImageHeight; y++)
                for (int x = 0; x < diffractionPatternControl[i].ImageWidth; x++)
                    if (!diffractionPatternControl[i].Filter[y * diffractionPatternControl[i].ImageWidth + x])
                        totalPixel++;
            residualInitial += diffractionPatternControl[i].InitialResidual();
        }

        CurrentProgress = "";
        for (int i = 0; i < diffractionPatternControl.Count; i++)
        {
            CurrentStatus = "Now initializing (" + (i + 1).Ordinal() + " pattern)";
            diffractionPatternControl[i].Simulate(true, true, true, true);
        }

        double residualCurrent = 0;
        for (int i = 0; i < diffractionPatternControl.Count; i++)
        {
            CurrentStatus = "Now refining background (" + (i + 1).Ordinal() + " pattern)";
            diffractionPatternControl[i].RefineBackGround();

            CurrentStatus = "Now refining center position (" + (i + 1).Ordinal() + " pattern)";
            if (checkBoxRefineCenterOffset.Checked)
                diffractionPatternControl[i].RefineCenterOffset();

            CurrentStatus = "Now refining film blur (" + (i + 1).Ordinal() + " pattern)";
            if (checkBoxRefineFilmBlur.Checked)
                diffractionPatternControl[i].RefineFilmBlur();

            residualCurrent += diffractionPatternControl[i].Residual();
        }

        //backgroundWorkerMain.ReportProgress(-1, new object[] { sw.ElapsedMilliseconds, 0, -1, 0.0, 0.0, polyCrystal });

        List<double> dev = new List<double>();


        int beforeSuccessTime = 0;
        int beforeSuccessStep = -1;

        int[] densityIndex = Array.Empty<int>();
        double[] densityValue = Array.Empty<double>();

        for (; setting.TotalStep < 100000000 && !backgroundWorkerMain.CancellationPending; setting.TotalStep++)
        {
            //setting.RefineStress = checkBoxRefineStress.Checked;
            beforeSuccessTime = setting.LPO_SuccessTime;


            CurrentStatus = "Now refining LPO...";

            //まずターゲットとなる結晶番号を決める
            int targetCrystal = 0;// rn.Next(Crystals.Count);

            setting.Inheritability = numericBoxInheritabiliry.Value / 100;
            setting.DirectionalDensity = numericBoxDirectionalDensity.Value / 180 * Math.PI;
            setting.LPO_Ratio = numericBoxCrystalNumPerStep.Value / 100.0;
            setting.RefineCenterOffset = checkBoxRefineCenterOffset.Checked;
            setting.RefineFilmBlur = checkBoxRefineFilmBlur.Checked;

            //ランダムに一つの方位を選ぶ
            int seed = rn.Next(Crystals[targetCrystal].Crystallites.TotalCrystalline);
            //一定の確率で、遺伝させる
            if (setting.Inheritability > rn.NextDouble())
            {
                double sum = rn.NextDouble() * Crystals[targetCrystal].Crystallites.Density.Sum(), temp = 0;
                for (seed = 0; seed < Crystals[targetCrystal].Crystallites.Density.Length && sum > temp; seed++)
                    temp += Crystals[targetCrystal].Crystallites.Density[seed];
            }//方位選択ここまで

            //新しい方位密度分布を作成
            Crystals[targetCrystal].Crystallites.GetBiasedDirection(seed, ref densityIndex, ref densityValue, setting.DirectionalDensity, setting.LPO_Ratio);

            double residualDifference = 0;
            for (int i = 0; i < diffractionPatternControl.Count; i++)
                residualDifference += diffractionPatternControl[i].PartialDensity
                    (targetCrystal, densityIndex, densityValue, setting.LPO_Ratio, beforeSuccessStep == setting.TotalStep);

            if (residualDifference < 0)//残差に改善があれば
            {
                dev.Add(residualDifference);//改善度を保存
                if (dev.Count > setting.LPO_SuccessTime + 1 || dev.Count > 8)
                {
                    if (dev.Count > 8)
                        dev.RemoveAt(0);
                    if (residualDifference < dev.Average())
                    {
                        for (int i = 0; i < Crystals[targetCrystal].Crystallites.Density.Length; i++)
                        {
                            double newDensity = Crystals[targetCrystal].Crystallites.Density[i] * (1 - setting.LPO_Ratio);
                            if (newDensity > 0)
                                Crystals[targetCrystal].Crystallites.Density[i] = newDensity;
                            else
                                Crystals[targetCrystal].Crystallites.Density[i] = 0;
                        }
                        for (int i = 0; i < densityIndex.Length; i++)
                            Crystals[targetCrystal].Crystallites.Density[densityIndex[i]] += densityValue[i];

                        for (int i = 0; i < diffractionPatternControl.Count; i++)
                            diffractionPatternControl[i].ApplyPartialDensity(targetCrystal, densityIndex, densityValue, setting.LPO_Ratio);
                        residualCurrent += residualDifference;
                        setting.LPO_SuccessTime++;//成功回数を増やす
                        beforeSuccessStep = setting.TotalStep;
                    }
                }
            }


            //LPOの変化が通算50%分増加したらBackGroundを再計算
            if (setting.LPO_SuccessTime != beforeSuccessTime && setting.LPO_SuccessTime % ((int)(0.50 / setting.LPO_Ratio)) == 0)
            {
                residualCurrent = 0;
                for (int i = 0; i < diffractionPatternControl.Count; i++)
                {
                    CurrentStatus = "Now refining background (" + (i + 1).Ordinal() + " pattern)";
                    residualCurrent += diffractionPatternControl[i].RefineBackGround();
                }
            }

            //LPOの変化が通算10%分増加したら ScaleFactorを再計算
            if (setting.LPO_SuccessTime != beforeSuccessTime && setting.LPO_SuccessTime % ((int)(0.10 / setting.LPO_Ratio)) == 0)
            {
                residualCurrent = 0;
                double coeff = Crystals[targetCrystal].Crystallites.TotalCrystalline / Crystals[targetCrystal].Crystallites.Density.Sum();
                for (int i = 0; i < Crystals[targetCrystal].Crystallites.Density.Length; i++)
                    Crystals[targetCrystal].Crystallites.Density[i] *= coeff;

                for (int i = 0; i < diffractionPatternControl.Count; i++)
                {
                    diffractionPatternControl[i].Simulate(false, false, false, true);
                    residualCurrent += diffractionPatternControl[i].RefineScaleFactor();
                }
            }

            //LPOの変化が通算25%分増加したら FilmBlurを再計算
            if (setting.RefineFilmBlur && setting.LPO_SuccessTime != beforeSuccessTime && setting.LPO_SuccessTime % ((int)(0.25 / setting.LPO_Ratio)) == 0)
            {
                residualCurrent = 0;
                for (int i = 0; i < diffractionPatternControl.Count; i++)
                {
                    CurrentStatus = "Now refining film blur (" + (i + 1).Ordinal() + " pattern)";
                    residualCurrent += diffractionPatternControl[i].RefineFilmBlur();
                }
            }

            //LPOの変化が通算200%分増加したら CenterOffsetを再計算
            if (setting.RefineCenterOffset && setting.LPO_SuccessTime != beforeSuccessTime && setting.LPO_SuccessTime % ((int)(2.0 / setting.LPO_Ratio)) == 0)
            {
                residualCurrent = 0;
                for (int i = 0; i < diffractionPatternControl.Count; i++)
                {
                    CurrentStatus = "Now refining center position (" + (i + 1).Ordinal() + " pattern)";
                    residualCurrent += diffractionPatternControl[i].RefineCenterOffset();
                }
            }

            backgroundWorkerMain.ReportProgress(
                setting.TotalStep % 100 == 0 ? 0 : 1,
                new object[] {
                        sw.ElapsedMilliseconds,
                        setting.TotalStep,
                        setting.LPO_SuccessTime,
                    Math.Sqrt(residualCurrent / residualInitial),
                    Math.Sqrt(residualCurrent / totalPixel) });
        }

        backgroundWorkerMain.CancelAsync();
    }

    private int beforeStep = -1;
    private long beforeSecond = 0;
    private long beforeRenewSecond = 0;

    private int beforeParameterChangedStep = 0;

    private double S = 0, residual = 0;

    private List<long> beforeSeconds = new List<long>();

    /// <summary>backGroundWorkerから途中経過報告を受ける</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void backgroundWorkerMain_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        long currentSecond = (long)((object[])e.UserState)[0];
        int step = (int)((object[])e.UserState)[1];
        int successTime = (int)((object[])e.UserState)[2];
        residual = (double)((object[])e.UserState)[3];
        S = (double)((object[])e.UserState)[4];

        if (e.ProgressPercentage == 1)
        {
            beforeSeconds.Add(currentSecond);
            if (beforeSeconds.Count > 30)
                beforeSeconds.RemoveAt(0);
            if (beforeSeconds[^1] - beforeRenewSecond > 500)//500msec以上、経過していたら
            {
                ProgressBarVisible = true;
                toolStripProgressBar.Value = (int)((step % 100 + 1.0) / 100.0 * toolStripProgressBar.Maximum);
                CurrentStatus = "Now refining LPO....";
                CurrentProgress = "Current Step: " + step.ToString() + ".   Average speed of recent " + beforeSeconds.Count.ToString() + " steps: " +
                    ((double)(beforeSeconds[^1] - beforeSeconds[0]) / beforeSeconds.Count).ToString("f0") + " ms/step";
                beforeRenewSecond = currentSecond;
                // 260428Cl Application.DoEvents() を削除 (BackgroundWorker の ProgressChanged は UI スレッドで動作するため不要)
            }
            return;
        }

        textBox1.Text = "Time: " + (currentSecond / 1000.0).ToString("f0") + " s\r\n" +
                       "Speed: " + ((currentSecond - beforeSecond) / (step - beforeStep)).ToString("f0") + " ms/step \r\n" +
                       "Step: " + step.ToString() + "\r\n" +
                       "Success: " + successTime.ToString() + "\r\n" +
                       "Residual: " + (residual * 100).ToString("f2") + "%\r\n" +
                       "S: " + S.ToString("f2");
        beforeStep = step;
        beforeSecond = currentSecond;

        foreach (var crystalControl in CrystalContlols)
            crystalControl.DrawPoleFigure();

        for (int i = 0; i < diffractionPatternControl.Count; i++)
        {
            diffractionPatternControl[i].Simulate(false, false, false, true);
            diffractionPatternControl[i].setSimulatedPixels();
        }
        graphControlResidual.AddPoint(0, new PointD(step, residual * 100));
        graphControlResidual.SetDrawingRangeXandExpandY(new RectangleD(graphControlResidual.Profile.Pt[0].X - 1, 0, graphControlResidual.Profile.Pt[0].X + step + 2, 0));


        //加工率の調整
        double thresholdStep = 1 / numericBoxCrystalNumPerStep.Value * 200;
        //前回の加工率を調整したステップから、1 / (double)numericUpDownCrystalNumPerStep.Value*200以上離れていること
        //0.5% だったら400ステップ, 0.1%だったら2000ステップ
        if (thresholdStep <= step - beforeParameterChangedStep)
        {
            //前回と今回までを二等分して、Rwp値の変化率(傾き)が0.75以下の時はパラメータを変化させる
            PointD[] pt = graphControlResidual.Profile.Pt.Where(p => p.X >= beforeParameterChangedStep).ToArray();
            double slope1 = (pt[pt.Length - 1].Y - pt[pt.Length / 2].Y) / (pt[^1].X - pt[pt.Length / 2].X);//後半の傾き
            double slope2 = (pt[pt.Length / 2].Y - pt[0].Y) / (pt[pt.Length / 2].X - pt[0].X);//前半の傾き
            textBox1.Text += "\r\n 傾き変化 (後半/前半): " + (slope1 / slope2).ToString("f2");
            if (slope1 / slope2 < numericBoxChangeParameterThreshold.Value)                                                                            // 260522Cl 変更: NumericUpDown → NumericBox (Value は double のため (double) キャスト撤去)
            {
                numericBoxCrystalNumPerStep.Value *= 0.8;
                numericBoxDirectionalDensity.Value *= 0.8;
                numericBoxInheritabiliry.Value = 100 - (100 - numericBoxInheritabiliry.Value) * 0.8;

                beforeParameterChangedStep = step;

                if (checkBoxDirectionalDensityThreshold.Checked && numericBoxDirectionalDensity.Value < numericBoxDirectionalDensityThreshold.Value)
                    backgroundWorkerMain.CancelAsync();

                if (checkBoxCrystalNumPerStepThreshold.Checked && numericBoxCrystalNumPerStep.Value < numericBoxCrystalNumPerStepThreshold.Value)
                    backgroundWorkerMain.CancelAsync();

                if (checkBoxInheritabiliryThreshold.Checked && numericBoxInheritabiliry.Value > numericBoxInheritabiliryThreshold.Value)
                    backgroundWorkerMain.CancelAsync();
            }
        }

        // 260428Cl Application.DoEvents() を削除 (BackgroundWorker の ProgressChanged は UI スレッドで動作するため不要)
    }


    private void backgroundWorkerMain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        buttonSearch.Text = "Search!";
        for (int i = 0; i < diffractionPatternControl.Count; i++)
            diffractionPatternControl[i].ContolEnabled(true);
    }

    private void tabControl1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        int imageWidth = 0, currentWitdh = 0;
        int imageHeight = 0, currentHeight = 0;

        if (tabControl1.SelectedIndex == 0 && diffractionPatternControlSimulation.ImageWidth != 0)
        {
            imageWidth = diffractionPatternControlSimulation.ImageWidth;
            imageHeight = diffractionPatternControlSimulation.ImageHeight;
            currentWitdh = diffractionPatternControlSimulation.scalablePictureBox.Width;
            currentHeight = diffractionPatternControlSimulation.scalablePictureBox.Height;
        }
        else if (tabControl1.SelectedIndex > 0 && diffractionPatternControl[tabControl1.SelectedIndex - 1].ImageWidth != 0)
        {
            imageWidth = diffractionPatternControl[tabControl1.SelectedIndex - 1].ImageWidth;
            imageHeight = diffractionPatternControl[tabControl1.SelectedIndex - 1].ImageHeight;
            currentWitdh = diffractionPatternControl[tabControl1.SelectedIndex - 1].scalablePictureBox.Width;
            currentHeight = diffractionPatternControl[tabControl1.SelectedIndex - 1].scalablePictureBox.Height;
        }

        if (imageWidth != 0 && imageHeight != 0)
        {
            this.Size = new Size(this.Width + imageWidth - currentWitdh + 1, this.Height + imageHeight - currentHeight + 1);
            Bitmap bmp;

            if (tabControl1.SelectedIndex == 0 && diffractionPatternControlSimulation.ImageWidth != 0)
                bmp = diffractionPatternControlSimulation.scalablePictureBox.GetBitmapImage();
            else
                bmp = diffractionPatternControl[tabControl1.SelectedIndex - 1].scalablePictureBox.GetBitmapImage();

            Clipboard.SetData(DataFormats.Bitmap, bmp);
        }
    }

    private void buttonSaveCurrentSetting_Click(object sender, EventArgs e)
    {
        SaveFileDialog dlg = new SaveFileDialog { Filter = "*.setting|*.setting" };
        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            saveSettngFile(dlg.FileName);
    }

    private void saveSettngFile(string fileName)
    {
        setRefinmentSetting();
        using Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        var serializer = new XmlSerializer(typeof(RefinmentSetting));
        serializer.Serialize(stream, setting);
    }

    private void buttonLoadSetting_Click(object sender, EventArgs e)
    {
    }

    private void setRefinmentSetting()
    {
        AreaDetector[] detector = new AreaDetector[diffractionPatternControl.Count];
        for (int i = 0; i < diffractionPatternControl.Count; i++)
            detector[i] = diffractionPatternControl[i].DetectorProperty;
        setting = new RefinmentSetting(
        Crystals.ToArray(), detector,
        numericBoxInheritabiliry.Value / 100,
        numericBoxDirectionalDensity.Value / 180 * Math.PI,
        setting != null ? setting.TotalStep : 0,
        setting != null ? setting.LPO_SuccessTime : 0,
        graphControlResidual.Profile ?? new Profile(),
        S, residual);
    }

    private void FormPolycrystallineDiffractionSimulator_DragDrop(object sender, DragEventArgs e)
    {
        string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);

        if (fileName.Count(str => str.EndsWith("ipa")) == 1)//ipaファイルが1つ含まれているとき
        {
            addRefferencePattern(fileName.First(str => str.EndsWith("ipa")));
            if (fileName.Any(str => str.EndsWith("mas") || str.EndsWith("bg")))//さらにmasファイルかbgファイルが含まれているとき
                diffractionPatternControl[tabControl1.SelectedIndex - 1].DiffractionPatternControl_DragDrop(sender, e);
        }
        else if (fileName.Count(str => str.EndsWith("ipa")) > 1)
        {
            foreach (string filename in fileName)
                if (filename.EndsWith("ipa"))
                    addRefferencePattern(filename);
        }
        else
        {
            if (fileName.Any(str => str.EndsWith("mas") || str.EndsWith("bg")))//さらにmasファイルかbgファイルが含まれているとき
                diffractionPatternControl[tabControl1.SelectedIndex - 1].DiffractionPatternControl_DragDrop(sender, e);
        }
    }

    private void FormPolycrystallineDiffractionSimulator_DragEnter(object sender, DragEventArgs e) => e.Effect = (e.Data.GetData(DataFormats.FileDrop) != null) ? DragDropEffects.Copy : DragDropEffects.None;

    #region

    private void buttonSearchUnrelatedOrientations_Click(object sender, EventArgs e)
    {
    }

    #endregion

    private void listView1_DragDrop(object sender, DragEventArgs e)
    {
        Crystal c = (Crystal)e.Data.GetData(typeof(Crystal).ToString(), true);
        if (c != null)
        {
        }
    }

    private void listView1_DragEnter(object sender, DragEventArgs e) => e.Effect = (e.Data.GetDataPresent(typeof(Crystal))) ? DragDropEffects.Copy : DragDropEffects.None;

    private void buttonSimulateDebyeRing_Click(object sender, EventArgs e)
    {
        DiffractionPatternControl dpc;
        if (tabControl1.SelectedIndex == 0)
            dpc = diffractionPatternControlSimulation;
        else
            dpc = diffractionPatternControl[tabControl1.SelectedIndex - 1];
        dpc.Crystals = this.Crystals;

        if (tabControlCrystals.TabPages.Count == 0 || dpc.Crystals == null || dpc.Crystals.Count == 0) return;

        for (int i = 0; i < dpc.Crystals.Count; i++)
            if (dpc.Crystals[i].Crystallites == null || ModifierKeys == Keys.Control)
            {
                dpc.Crystals[i].SetCrystallites();
            }
        foreach (var crystalControl in CrystalContlols)
            crystalControl.DrawPoleFigure();

        YusaGonio gonio = new YusaGonio();
        if (checkBoxYusaGonioScan.Checked)
        {
            gonio.Valid = true;
            gonio.Rx = checkBoxYusaGonio_ValidRx.Checked;
            gonio.Ry_OscillationRange = numericBoxYusaGonioRyOscillation.Value;
            gonio.Rz_OscillationRange = numericBoxYusaGonioRzOscillation.Value;
        }

        sw.Restart();

        dpc.Simulate(true, true, true, true, gonio);

        toolStripStatusLabelProgress.Text = $"{sw.ElapsedMilliseconds / 1000.0:#.000} s";

        sw.Restart();
        if (tabControl1.SelectedIndex != 0)
            dpc.RefineBackGround();

        dpc.ScaleFactor = 100000.0;
        if (tabControl1.SelectedIndex != 0)
            dpc.RefineScaleFactor();

        double max;
        if (dpc.SrcPixels == null)
            max = dpc.DiffractionPixels.Max() * dpc.ScaleFactor;
        else
            max = Math.Max(dpc.DiffractionPixels.Max() * dpc.ScaleFactor, dpc.SrcPixels.Max());

        if (max < 2) max = 2;

        // 260522Cl 変更: DiffractionPatternControl の numericUpDownMaxInt/MinInt → numericBoxMaxInt/MinInt (NumericBox 化, .Value/.Maximum は double)
        dpc.numericBoxMaxInt.Maximum = max;
        dpc.numericBoxMaxInt.Value = max;
        dpc.numericBoxMinInt.Maximum = max - 1;
        dpc.numericBoxMinInt.Value = 1;

        dpc.checkBoxSimulation.Checked = true;
        setScale();
        dpc.setSimulatedPixels();

        dpc.numericBoxMaxInt.Maximum = max;
        dpc.DiffractionInformation();

        toolStripStatusLabelProgress.Text += $"   {sw.ElapsedMilliseconds / 1000.0:#.000} s";
    }

    private void button1_Click(object sender, EventArgs e)
    {
        DiffractionPatternControl dpc;
        if (tabControl1.SelectedIndex == 0)
            dpc = diffractionPatternControlSimulation;
        else
            dpc = diffractionPatternControl[tabControl1.SelectedIndex - 1];

        if (tabControlCrystals.TabPages.Count == 0 || dpc.Crystals == null || dpc.Crystals.Count == 0) return;

        Random rn = new Random();
        for (int i = 0; i < dpc.Crystals.Count; i++)
        //if (dpc.Crystals[i].Crystallites == null)
        {
            double[] density = new double[dpc.Crystals[i].Crystallites.TotalCrystalline];//260718Cl: 冗長なゼロ埋めループを削除 (new で全 0)
            density[rn.Next(density.Length)] = 1;

            //dpc.Crystals[i].SetCrystallites(density);
            dpc.DiffractionPixels = dpc.GetDiffractionPixels(false, false, false, density);
        }

        dpc.setSimulatedPixels();
    }

    private void checkBoxInheritabiliryThreshold_CheckedChanged(object sender, EventArgs e) => numericBoxInheritabiliryThreshold.Visible = checkBoxInheritabiliryThreshold.Checked;

    private void checkBoxDirectionalDensityThreshold_CheckedChanged(object sender, EventArgs e) => numericBoxDirectionalDensityThreshold.Visible = checkBoxDirectionalDensityThreshold.Checked;

    private void checkBoxCrystalNumPerStepThreshold_CheckedChanged(object sender, EventArgs e) => numericBoxCrystalNumPerStepThreshold.Visible = checkBoxCrystalNumPerStepThreshold.Checked;

    public void dpc_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        if (this.InvokeRequired)
        {
            this.Invoke(() => dpc_ProgressChanged(sender, e));//260718Cl 現代化: 専用 delegate → lambda
        }
        else
        {
            double ratio = (double)((object[])e.UserState)[0];
            string str = (string)((object[])e.UserState)[1];
            if (ratio < 1)
            {
                ProgressBarVisible = true;
                toolStripProgressBar.Value = (int)(ratio * toolStripProgressBar.Maximum);
                toolStripStatusLabelProgress.Text = str;
            }
            else
            {
                ProgressBarVisible = false;
                toolStripStatusLabelProgress.Text = "";
            }
            // 260428Cl Application.DoEvents() を削除 (Invoke 経由で UI スレッドで動作するため不要)
        }
    }

    private void FormPolycrystallineDiffractionSimulator_VisibleChanged(object sender, EventArgs e)
    {
        if (this.Visible && formMain.Visible)
            formMain.listBox.SelectedIndex = formMain.listBox.SelectedIndex;
    }

    private void crystalControl1_VisibleChanged(object sender, EventArgs e)
    {
        if (this.Visible)
            listBox_SelectedIndexChanged(sender, e);
    }
}

[Serializable()]
public class RefinmentSetting
{
    public Crystal[] Crystals;
    public AreaDetector[] AreaDetectors;

    public double Inheritability;
    public double DirectionalDensity;
    public double LPO_Ratio;

    public bool RefineCenterOffset = true;
    public bool RefineFilmBlur = true;
    public bool RefineStress = true;

    public int LPO_SuccessTime;
    public int TotalStep;
    public Profile ResidualProfile;
    public double S, Residual;

    public double[] euler1, euler2, euler3;
    public double[] size, density;

    [NonSerialized]
    public Crystallite[] Crystallites;

    public RefinmentSetting()
    {
    }

    public RefinmentSetting
        (Crystal[] crystals, AreaDetector[] areaDetectors, double inheritability, double directionalDensity,
        int totalStep, int successStep, Profile residualProfile, double s, double residual)
    {
        Crystals = crystals;
        AreaDetectors = areaDetectors;

        Inheritability = inheritability;
        DirectionalDensity = directionalDensity;

        TotalStep = totalStep;
        LPO_SuccessTime = successStep;

        ResidualProfile = residualProfile;
        S = s;
        Residual = residual;
    }
}
