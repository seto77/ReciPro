using Crystallography;
using Crystallography.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MQ = Crystallography.Marquardt;

namespace ReciPro
{
    public partial class FormSpotID : Form
    {
        #region プロパティ、フィールド
        public object tagObsSpot = "ObsCross";
        public object tagCalcSpot = "Calc";

        public FormSpotDetails FormSpotDetails;

        public FormMain FormMain;

        public AreaDetector Detector;

        private bool skipEvent = false;
        public PointD DirectSpot => dataSet.DataTableSpot.DirectSpotPosition;

        public double PixelSize { set => numericBoxPixelSize.Value = value; get => numericBoxPixelSize.Value; }
        public double CameraLength { set => numericBoxCameraLength.Value = value; get => numericBoxCameraLength.Value; }

        public double ToleranceLength => numericBoxAcceptableError.Value * 0.01;
        public double ToleranceAngle => Math.Asin(ToleranceLength) / 2;

        public double FittingRange => numericBoxFittingRange.Value;

        private readonly object lockObj = new object();

        private readonly Stopwatch sw = new Stopwatch();

        #endregion プロパティ、フィールド

        #region ロード, クローズ関連

        public FormSpotID()
        {
            InitializeComponent();
            scalablePictureBoxAdvanced.Symbols = new List<ScalablePictureBox.Symbol>();
        }

        private void FormSpotID_Load(object sender, EventArgs e)
        {
            FormSpotDetails = new FormSpotDetails { FormSpotID = this };
        }

        private void FormSpotID_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            FormMain.toolStripButtonSpotID.Checked = false;
        }

        #endregion ロード, クローズ関連


        private void clearStatusLabel() => toolStripStatusLabelFindSpot.Text = toolStripStatusLabelIdentifySpot.Text =
                toolStripStatusLabelImageFilter.Text = toolStripStatusLabelRefine.Text = "";

        #region 画像読み込み関連

        /// <summary>
        /// 画像読み込み
        /// </summary>
        /// <param name="fileName"></param>
        private void readImage(string fileName)
        {
            dataSet.DataTableSpot.Clear();
            scalablePictureBoxAdvanced.ReadImage(fileName);

            if (fileName.EndsWith("dm3") || fileName.EndsWith("dm4"))
            {//DigitalMicroGraphデータであればスケールの情報などを取得
                if (Ring.DigitalMicrographProperty.PixelUnit == Crystallography.PixelUnitEnum.NanoMeterInv)
                {
                    waveLengthControl1.WaveSource = WaveSource.Electron;
                    waveLengthControl1.Energy = Ring.DigitalMicrographProperty.AccVoltage / 1000.0;
                    numericBoxPixelSize.Value = Ring.DigitalMicrographProperty.PixelSizeInMicron / 1000.0;
                    CameraLength = Ring.DigitalMicrographProperty.PixelSizeInMicron / 1000 / Math.Tan(2 * Math.Asin(waveLengthControl1.WaveLength * Ring.DigitalMicrographProperty.PixelScale / 2));
                    double pixelSize = Ring.DigitalMicrographProperty.PixelSizeInMicron;
                    double scale = Ring.DigitalMicrographProperty.PixelScale;
                }
            }
            if (fileName.EndsWith("ipa"))
            {
                waveLengthControl1.WaveSource = Ring.IP.WaveProperty.Source;
                waveLengthControl1.WaveLength = Ring.IP.WaveProperty.WaveLength;
                CameraLength = Ring.IP.FilmDistance;
                PixelSize = Ring.IP.PixSizeX;
                var direct = new PointD(Ring.IP.CenterX, Ring.IP.CenterY);
                var area = numericBoxFittingRange.Value;
                var r = fit(direct);
                dataSet.DataTableSpot.Add(true, area, r.PrmsPv, r.PrmsBg, r.R);
            }

            var p = scalablePictureBoxAdvanced.PseudoBitmap;

            FormSpotDetails.SetData();

            dataSet.DataTableSpot.AreaDetector = new AreaDetector(p.Width, p.Height, PixelSize, DirectSpot, waveLengthControl1.Property, CameraLength);
        }

        /// <summary>
        /// ScalablePictureBoxAdvancedのフィルターが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScalablePictureBoxAdvanced_FilterChanged(object sender, EventArgs e)
        {
            if (FormSpotDetails.Visible)
                FormSpotDetails.SetData();
        }

        #region DragDrop関連

        private void FormSpotID_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (fileName.Length > 0)
            {
                if (fileName[0].EndsWith(".csv"))
                    readCSV(fileName[0]);
                else
                    readImage(fileName[0]);
            }
        }

        private void FormSpotID_DragEnter(object sender, DragEventArgs e) => e.Effect = (e.Data.GetData(DataFormats.FileDrop) != null) ? DragDropEffects.Copy : DragDropEffects.None;

        #endregion DragDrop関連
        #endregion 画像読み込み関連

        private bool scalablePictureBoxAdvanced1_MouseDown2(object sender, MouseEventArgs e, Crystallography.PointD pt)
        {
            skipEvent = false;
            var p = scalablePictureBoxAdvanced.PseudoBitmap;
            if (p == null)
                return true;

            if (e.Button == MouseButtons.Left && e.Clicks == 2)//左ダブルクリック　スポット追加
            {
                skipEvent = true;
                sw.Restart();
                var r = fit(pt, FittingRange);

                if ((new PointD(r.PrmsPv[0], r.PrmsPv[1]) - pt).Length < 10)
                {
                    bindingSourceObsSpots.DataMember = "";
                    dataSet.DataTableSpot.Add(ModifierKeys == Keys.Control, FittingRange, r.PrmsPv, r.PrmsBg, r.R);
                    bindingSourceObsSpots.DataMember = "DataTableSpot";
                }

                bindingSourceObsSpots.Position = dataSet.DataTableSpot.Rows.Count - 1;
                skipEvent = false;
                bindingSourceObsSpots_ListChanged(sender, new ListChangedEventArgs(ListChangedType.ItemAdded, dataSet.DataTableSpot.Rows.Count - 1));
                toolStripStatusLabelIdentifySpot.Text = $"  Fitting time (1 spot): {sw.ElapsedMilliseconds} ms.";
                return true;
            }
            else if (e.Button == MouseButtons.Left && e.Clicks == 1)//左クリック スポット選択
            {
                var spots = dataSet.DataTableSpot.Spots;
                if (spots != null && spots.Any())
                {
                    var min = spots.Min(s => (s.X - pt.X) * (s.X - pt.X) + (s.Y - pt.Y) * (s.Y - pt.Y));
                    if (Math.Sqrt(min) * scalablePictureBoxAdvanced.ZoomAndCenter.Zoom < 10)
                    {
                        var num = spots.First(s => (s.X - pt.X) * (s.X - pt.X) + (s.Y - pt.Y) * (s.Y - pt.Y) == min).No;
                        scalablePictureBoxAdvanced.FixZoomAndCenter = true;
                        bindingSourceObsSpots.Position = bindingSourceObsSpots.Find("No", num);
                        scalablePictureBoxAdvanced.FixZoomAndCenter = false;

                        return true;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right && ModifierKeys == Keys.Control)//右クリック
            {
                skipEvent = true;
                var spots = dataSet.DataTableSpot.Spots;
                if (spots != null && spots.Count!=0)
                {
                    var min = spots.Min(s => (s.X - pt.X) * (s.X - pt.X) + (s.Y - pt.Y) * (s.Y - pt.Y));
                    if (Math.Sqrt(min) * scalablePictureBoxAdvanced.ZoomAndCenter.Zoom < 10)
                    {
                        bindingSourceObsSpots.DataMember = "";
                        dataSet.DataTableSpot.Remove(spots.First(s => (s.X - pt.X) * (s.X - pt.X) + (s.Y - pt.Y) * (s.Y - pt.Y) == min).No);
                        bindingSourceObsSpots.DataMember = "DataTableSpot";
                    }
                }

                if (dataSet.DataTableSpot.Rows.Count > 0)
                    bindingSourceObsSpots.Position = 0;
                skipEvent = false;
                bindingSourceObsSpots_ListChanged(sender, new ListChangedEventArgs(ListChangedType.ItemDeleted, 0));
                return true;
            }
            return false;
        }

        #region 画像からスポットを検索、クリア、フィッティング

        private void buttonFindSpots_Click(object sender, EventArgs e)
        {
            var p = scalablePictureBoxAdvanced.PseudoBitmap;
            if (p == null || p.SrcValuesGray == null) return;

            clearStatusLabel();
            sw.Restart();
            Enabled = false;
            var spots = ImageProcess.FindSpots(p.SrcValuesGray, p.Width, numericBoxNearestNeighbor.Value, (int)numericBoxNumberOfSpots.Value, dataSet.DataTableSpot.SpotPositions);
            if (DirectSpot.IsNaN)//ダイレクトが未決定の場合
                spots = spots.OrderBy(s => 1 / s.Int).ToList(); //優先順に並び替える。　条件は　①強度が大きい。(③鋭いピークである)
            else//ダイレクトが既に決められている場合
                spots = spots.Where(s => (new PointD(s.X, s.Y) - DirectSpot).Length > numericBoxNearestNeighbor.Value)//ダイレクトに近いものを除去
                .OrderBy(s => 1 / s.Int * (new PointD(s.X, s.Y) - DirectSpot).Length2).ToList(); //優先順に並び替える。　条件は　①なるべく逆格子原点に近い。②強度が大きい。(③鋭いピークである)

            toolStripStatusLabelIdentifySpot.Text = $" Search time ({spots.Count} spots): {sw.ElapsedMilliseconds} ms.   ";
            sw.Restart();
            #region テストコード
            /*
            var coeff1 = new double[] { 0.5, 0.002, 0.004, 0.006, 0.008, 0.01, 0.012, 0.014, 0.016};
            var coeff2 = new double[] { 1.5, 2 , 3, 4, 5, 6, 7, 8, 9, 10};
            var sb = new StringBuilder();
            for (int k = 0; k < coeff2.Length; k++)
                sb.Append("\t" + coeff2[k].ToString());

            for (int j = 0; j < coeff1.Length; j++)
            {
                sb.Append("\r\n");
                for (int k = 0; k < coeff2.Length; k++)
                {
                    Marquardt.RambdaCoeff1 = coeff1[j];
                    Marquardt.RambdaCoeff2 = coeff2[k];
                    sw.Restart();
                    for (int i = 0; i < spots.Count; i++)
                    {
                        var r = fit(new PointD(spots[i].X, spots[i].Y));
                    }
                    sw.Stop();

                    if (k == 0)
                        sb.Append(coeff1[j].ToString());
                    sb.Append("\t" + sw.ElapsedMilliseconds.ToString());
                }
            }
            Clipboard.SetDataObject(sb.ToString());
            MessageBox.Show("OK");
            */
            #endregion テストコード

            bindingSourceObsSpots.DataMember = "";

            //spots.RemoveRange(0,13);
            //spots.RemoveRange(1,spots.Count-1);


            int n = 0;
            var results = spots.Select(s =>
            {
                toolStripProgressBar.Value = n++ * toolStripProgressBar.Maximum / spots.Count;
                Application.DoEvents();
                var val = fit(new PointD(s.X, s.Y), FittingRange);
                return val;
            }).ToList();

            for (int i = 0; i < results.Count; i++)
            {
                double x = results[i].PrmsPv[0], y = results[i].PrmsPv[1];
                for (int j = i + 1; j < results.Count; j++)
                {
                    double x1 = results[j].PrmsPv[0], y1 = results[j].PrmsPv[1];

                    if ((x1 - x) * (x1 - x) + (y1 - y) * (y1 - y) < numericBoxNearestNeighbor.Value * numericBoxNearestNeighbor.Value)
                        results.RemoveAt(j--);
                }
            }
          
            for (int i = 0; i < results.Count; i++)
                if (!double.IsPositiveInfinity(results[i].R))
                    dataSet.DataTableSpot.Add(DirectSpot.IsNaN, FittingRange, results[i].PrmsPv, results[i].PrmsBg, results[i].R);
            toolStripProgressBar.Value = toolStripProgressBar.Maximum;
            bindingSourceObsSpots.DataMember = "DataTableSpot";
            toolStripStatusLabelIdentifySpot.Text += $" Fitting time ({spots.Count} spots): {sw.ElapsedMilliseconds} ms.";
            Enabled = true;
        }

        /// <summary>
        /// スポットをフィッティングする。スポット位置のみが指定された場合。
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private (double[] PrmsPv, double[] PrmsBg, double R) fit(PointD pt, double radius = 0)
        {
            //画像が読み込まれているか、チェック。
            if (scalablePictureBoxAdvanced.PseudoBitmap == null || scalablePictureBoxAdvanced.PseudoBitmap.SrcValuesGray == null)
                return (new[] { pt.X, pt.Y, 3, 3, 0, 0, 0 }, new[] { 0.0, 0, 0 }, double.PositiveInfinity);

            if (radius == 0)
                radius = numericBoxFittingRange.Value;

            //外れすぎていないかをチェックするfunc
            var exclude = new Func< double, double,bool>((x, y) => Math.Abs(pt.X - x) > 1000 || Math.Abs(pt.Y - y) > 1000 ||
                    (pt.X - x) * (pt.X - x) + (pt.Y - y) * (pt.Y - y) > radius * radius);


            //検索対象のピクセルを粗く設定
            int width = scalablePictureBoxAdvanced.PseudoBitmap.Width, height = scalablePictureBoxAdvanced.PseudoBitmap.Height;
            var srcValues = scalablePictureBoxAdvanced.PseudoBitmap.SrcValuesGray;
            var margin = 5.0;
            var src = new List<(double[] x, double y)>();
            for (int y = (int)Math.Max(0, pt.Y - radius - margin + 0.5); y < (int)Math.Min(height, pt.Y + radius + margin + 0.5); y++)
                for (int x = (int)Math.Max(0, pt.X - radius - margin + 0.5); x < (int)Math.Min(width, pt.X + radius + margin + 0.5); x++)
                    if ((x - pt.X) * (x - pt.X) + (y - pt.Y) * (y - pt.Y) <= (radius + margin) * (radius + margin))
                        src.Add((new double[] { x, y }, srcValues[y * width + x]));
            var srcP = src.AsParallel();

            //1回目. 粗く検索
            var pixels = getPixels(srcP, radius, pt.X, pt.Y);
            var funcs = new List<MQ.Function>
            {
                new MQ.Function(MQ.FuncType.PV2E, pt.X, pt.Y, 3.1, 2.9, 0, 0.5, pixels.Sum(p => p.y)),//疑フォークト
                new MQ.Function(MQ.FuncType.Plane, 0, 0, 0)//バックグラウンド
            };
            var r = MQ.Solve(pixels, funcs, MQ.Precision.Low);
            if (double.IsInfinity(r.R) || exclude(r.Prms[0][0], r.Prms[0][1]))
                return (new[] { pt.X, pt.Y, 3.0, 3.0, 0, 0.0, 0 }, new[] { 0.0, 0, 0 }, double.PositiveInfinity);

            //2回目. 普通に検索
            pixels = getPixels(srcP, radius, r.Prms[0][0], r.Prms[0][1]);
            r = MQ.Solve(pixels, funcs, MQ.Precision.Medium);
            if (double.IsInfinity(r.R) || exclude(r.Prms[0][0], r.Prms[0][1]))
                return (new[] { pt.X, pt.Y, 3.0, 3.0, 0, 0.0, 0 }, new[] { 0.0, 0, 0 }, double.PositiveInfinity);

            return (r.Prms[0], r.Prms[1], r.R);
        }

        /// <summary>
        /// 検索対象のピクセルを設定する
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private (double[] x, double y, double w)[] getPixels(IEnumerable<(double[] x, double y)> src, double radius, double cX, double cY)
        {
            var rMax = (radius + Math.Sqrt(0.5)) * (radius + Math.Sqrt(0.5));
            var rMin = (radius - Math.Sqrt(0.5)) * (radius - Math.Sqrt(0.5));
            return src.Select(p =>
            {
                double x = p.x[0] - cX, y = p.x[1] - cY, l2 = x * x + y * y;
                if (l2 < rMin)
                    return (p.x, p.y, 1);
                else if (l2 > rMax)
                    return (p.x, p.y, 0);
                else
                {
                    var vertices = new[] { (x - .5, y - .5), (x + .5, y - .5), (x + .5, y + .5), (x - .5, y + .5) };
                    var l = Math.Sqrt(l2);
                    vertices = Geometriy.GetPolygonDividedByLine(vertices, -x / l, -y / l, -radius);
                    return (p.x, p.y, w: vertices.Length > 2 ? Geometriy.GetPolygonalArea(vertices) : 0);
                }
            }).Where(p => p.w != 0).ToArray();
        }

        /// <summary>
        /// スポットをフィッティングする。初期値を指定。
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        private (double X0, double Y0, double H1, double H2, double T, double Eta, double A, double B0, double Bx, double By, double R)
            fit((double[] x, double y, double w)[] pixels, double x0, double y0, double h1, double h2, double t, double eta, double a, double b0, double bx, double by,
            MQ.Precision precision = MQ.Precision.Medium)
        {
            var r = MQ.Solve(pixels, new[] { new MQ.Function(MQ.FuncType.PV2E, x0, y0, h1, h2, t, eta, a), new MQ.Function(MQ.FuncType.Plane, b0, bx, by) }, precision);

            if (double.IsInfinity(r.R) || Math.Abs(x0 - r.Prms[0][0]) > 1000 || Math.Abs(y0 - r.Prms[0][1]) > 1000)
                return (x0, y0, h1, h2, t, eta, a, b0, bx, by, double.PositiveInfinity);
            else
                return (r.Prms[0][0], r.Prms[0][1], r.Prms[0][2], r.Prms[0][3], r.Prms[0][4], r.Prms[0][5], r.Prms[0][6], r.Prms[1][0], r.Prms[1][1], r.Prms[1][2], r.R);
        }

        private void buttonClearSpots_Click(object sender, EventArgs e)
        {
            dataSet.DataTableSpot.Clear();
            scalablePictureBoxAdvanced.Symbols?.RemoveAll(s => s.Tag == tagObsSpot);
            scalablePictureBoxAdvanced.Refresh();
        }

        private void buttonRefit_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Application.DoEvents();
            sw.Restart();
            bindingSourceObsSpots.DataMember = "";

            int n = 0;
            var results = dataSet.DataTableSpot.Spots.Select((s, i) =>
            {
                toolStripProgressBar.Value = n++ * toolStripProgressBar.Maximum / dataSet.DataTableSpot.Spots.Count;
                Application.DoEvents();
                var area = dataSet.DataTableSpot.GetPrms(i).Range;
                return fit(new PointD(s.X, s.Y), area);
            }).ToArray();

            for (int i = 0; i < results.Length; i++)
                if (!double.IsPositiveInfinity(results[i].R))
                {
                    var area = dataSet.DataTableSpot.GetPrms(i).Range;
                    dataSet.DataTableSpot.SetPrms(i, area, results[i].PrmsPv, results[i].PrmsBg, results[i].R);
                }

            bindingSourceObsSpots.DataMember = "DataTableSpot";
            toolStripStatusLabelIdentifySpot.Text = $" Fitting time ({ dataSet.DataTableSpot.Rows.Count} spots): { sw.ElapsedMilliseconds} ms.";
            Enabled = true;
        }

        private void ButtonGlobalFit_Click(object sender, EventArgs e)
        {
            if (dataSet.DataTableSpot.Spots.Count == 0) return;
            Enabled = false;
            Application.DoEvents();
            sw.Restart();
            bindingSourceObsSpots.DataMember = "";

            //まず、現在のスポットのパラメータを取得 && 検索対象のエリアの合計を抽出
            int width = scalablePictureBoxAdvanced.PseudoBitmap.Width, height = scalablePictureBoxAdvanced.PseudoBitmap.Height;
            var srcValues = scalablePictureBoxAdvanced.PseudoBitmap.SrcValuesGray;
            var prmsList = new List<(bool Direct, int No, double Range, double X0, double Y0, double H1, double H2, double Theta, double Eta, double A, double B0, double Bx, double By, double R)>();
            var functions = new List<MQ.Function>();
            var excludedArea = new List<int>();
            for (int i = 0; i < dataSet.DataTableSpot.Spots.Count; i++)
            {
                var prms = dataSet.DataTableSpot.GetPrms(i);
                prmsList.Add(prms);
                for (int y = Math.Max(0, (int)(prms.Y0 - prms.Range - 1)); y < Math.Min(height, (int)(prms.Y0 + prms.Range + 2)); y++)
                    for (int x = Math.Max(0, (int)(prms.X0 - prms.Range - 1)); x < Math.Min(width, (int)(prms.X0 + prms.Range + 2)); x++)
                        if ((x - prms.X0) * (x - prms.X0) + (y - prms.Y0) * (y - prms.Y0) < prms.Range * prms.Range)
                            excludedArea.Add(x + y * width);

                if (prms.Direct)
                {
                    var func1 = new MQ.Function(MQ.FuncType.PV2E, prms.X0, prms.Y0, width * 0.5, height * 0.51, 0.5, 0.5, 100000000);
                    var func2 = new MQ.Function(MQ.FuncType.PV2E, prms.X0, prms.Y0, width * 1.0, height * 1.1, 0.5, 0.5, 100000000);
                    var func3 = new MQ.Function(MQ.FuncType.PV2E, prms.X0, prms.Y0, width * 2.0, height * 2.1, 0.5, 0.5, 100000000);
                    func1.Constraints = func2.Constraints = func3.Constraints = p => new[] {
                        Math.Min(Math.Max(p[0], prms.X0 - 100), prms.X0 + 100),//X0
                        Math.Min(Math.Max(p[1], prms.Y0 - 100), prms.Y0 + 100),//Y0
                        Math.Max(p[2], 30),//H1
                        Math.Max(p[3], 30),//H2
                        p[4],//Theta
                        Math.Min(Math.Max(p[5], 0), 1.5),//Eta
                        Math.Max(p[6], 1E-10)//A
                    };
                    functions.AddRange(new[] { func1, func2, func3 });
                }
            }
            var includedArea = Enumerable.Range(0, width * height).Except(excludedArea).ToList();

            //ここまで
            var pixelsBG = new List<(double[] x, double y, double w)>();
            foreach (var index in includedArea)
            {
                int x = index % width, y = index / width;
                double range = numericBoxFittingRange.Value;
                if (x > range && x < width - range && y > range && y < height - range)
                    pixelsBG.Add((new double[] { x, y }, srcValues[index], 1));
            }

            //pixelsBGの数が大きすぎる場合は時間がかかるので、減らす
            while (pixelsBG.Count > 50000)
            {
                pixelsBG = pixelsBG.Where((b, i) => i % 2 == 0).ToList();

            }

            var r = MQ.Solve(pixelsBG, functions, MQ.Precision.Low);

            for (int i = 0; i < prmsList.Count; i++)
            {
                var intensity = 0.0;
                var prms = prmsList[i];
                var h = (prms.H1 + prms.H2) / 2.0 * 8.0;
                for (int y = Math.Max(0, (int)(prms.Y0 - h - 1)); y < Math.Min(height, (int)(prms.Y0 + h + 2)); y++)
                    for (int x = Math.Max(0, (int)(prms.X0 - h - 1)); x < Math.Min(width, (int)(prms.X0 + h + 2)); x++)
                        if ((x - prms.X0) * (x - prms.X0) + (y - prms.Y0) * (y - prms.Y0) < h * h)
                        {
                            var temp = srcValues[x + y * width];
                            foreach (var p in r.Prms)
                                temp -= MQ.PseudoVoigt(new double[] { x, y }, p[0], p[1], p[2], p[3], p[4], p[5], p[6]);
                            intensity += temp;
                        }

                dataSet.DataTableSpot.SetPrms(i, prms.Range, new[] { prms.X0, prms.Y0, 0.0, 0.0, 0.0, 0.0, intensity }, new[] { 0.0, 0.0, 0.0 }, 0);

            }
            bindingSourceObsSpots.DataMember = "DataTableSpot";

            toolStripStatusLabelIdentifySpot.Text = $" Fitting time ({ dataSet.DataTableSpot.Rows.Count} spots): { sw.ElapsedMilliseconds} ms.";
            Enabled = true;
        }

        #endregion 画像からスポットを検索、クリア、フィッティング

        #region スポット情報テーブルに関連するメソッド

        /// <summary>
        /// スポット情報テーブルの内容が変更したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bindingSourceObsSpots_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (skipEvent) return;
            scalablePictureBoxAdvanced.Symbols?.RemoveAll(s => s.Tag == tagObsSpot);
            if (dataSet.DataTableSpot.Spots != null)
            {
                var current = -1;
                if (bindingSourceObsSpots.Current is DataRowView view)
                    current = (int)view.Row["No"];

                var direct = dataSet.DataTableSpot.DirectSpotPosition;
                for (int i = 0; i < dataSet.DataTableSpot.Spots.Count; i++)
                {
                    var p = dataSet.DataTableSpot.GetPrms(i);
                    var symbol = !p.Direct ?
                        new ScalablePictureBox.Symbol(i.ToString(), new PointD(p.X0, p.Y0), p.Range, Color.LightBlue, 5, Color.LightBlue, Color.DarkBlue) :
                        new ScalablePictureBox.Symbol(i.ToString(), new PointD(p.X0, p.Y0), p.Range, Color.Pink, 5, Color.Pink, Color.DarkRed);
                    symbol.Tag = tagObsSpot;
                    symbol.Bold = i == current;
                    scalablePictureBoxAdvanced.Symbols.Add(symbol);
                }
            }
            scalablePictureBoxAdvanced.Refresh();
            if (FormSpotDetails != null && FormSpotDetails.Visible)
                FormSpotDetails.SetData(false);
        }


        /// <summary>
        /// スポット情報テーブルのカレントが変更したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bindingSourceSpot_CurrentChanged(object sender, EventArgs e)
        {
            if (skipEvent) return;

            if (bindingSourceObsSpots.Current is DataRowView view)
            {
                var index = ((int)view.Row["No"]);
                if (index < 0 || index >= dataSet.DataTableSpot.Spots.Count)
                    return;

                //選択スポットをボールドに変更
                foreach (var s in scalablePictureBoxAdvanced.Symbols)
                    if (s.Tag == tagObsSpot)
                        s.Bold = s.Label == index.ToString();

                //中心位置をシフト
                var spot = dataSet.DataTableSpot.Spots[index];
                var center = new PointD(spot.X, spot.Y);
                scalablePictureBoxAdvanced.ZoomAndCenter = (scalablePictureBoxAdvanced.ZoomAndCenter.Zoom, center);


                scalablePictureBoxAdvanced.Refresh();
                if (FormSpotDetails.Visible)
                    FormSpotDetails.SetData(false);
            }
        }

        /// <summary>
        /// セルコンテンツがクリックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewSpots_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataSet.DataTableSpot.Rows.Count > 0 && e.RowIndex >= 0)
            {
                var pos = bindingSourceObsSpots.Position;
                var view = (DataRowView)bindingSourceObsSpots.Current;
                var currentIndex = (int)view.Row[1];
                if (!view.IsNew)
                {
                    //Directチェックボックスを押したとき
                    if (e.ColumnIndex == 0)
                    {
                        skipEvent = true;
                        dataSet.DataTableSpot.SetDirectNo(currentIndex);
                        bindingSourceObsSpots.Position = pos;
                        skipEvent = false;
                        bindingSourceObsSpots_ListChanged(sender, new ListChangedEventArgs(ListChangedType.ItemChanged, pos));
                    }
                    //Fitボタンを押したとき
                    else if (dataGridViewSpots.Columns[e.ColumnIndex].HeaderText.Contains("Fit"))
                    {
                        sw.Restart();
                        skipEvent = true;
                        var p = dataSet.DataTableSpot.GetPrms(currentIndex);
                        var range = p.Range;
                        var r = fit(new PointD(p.X0, p.Y0), range);
                        dataSet.DataTableSpot.SetPrms(currentIndex, range, r.PrmsPv, r.PrmsBg, r.R);
                        skipEvent = false;
                        bindingSourceObsSpots_ListChanged(sender, new ListChangedEventArgs(ListChangedType.ItemChanged, pos));
                        toolStripStatusLabelIdentifySpot.Text = $"  Fitting time (1 spot): {sw.ElapsedMilliseconds} ms.";
                    }
                }
            }
        }

        /// <summary>
        /// 行のヘッダーがダブルクリックされたとき画像を拡大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void DataGridViewSpots_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataSet.DataTableSpot.Rows.Count > 0 && e.RowIndex >= 0)
            {
                var index = (int)((DataRowView)bindingSourceObsSpots.Current)["No"];
                var center = new PointD(dataSet.DataTableSpot.SpotPositions[index].X, dataSet.DataTableSpot.SpotPositions[index].Y);
                scalablePictureBoxAdvanced.ZoomAndCenter = (scalablePictureBoxAdvanced.ZoomAndCenter.Zoom * 2, center);
            }
        }


        private void ButtonResetRangeForAllSpots_Click(object sender, EventArgs e)
        {
            bindingSourceObsSpots.DataMember = "";
            for (int i = 0; i < dataSet.DataTableSpot.Rows.Count; i++)
            {
                (_, _, _, double X0, double Y0, double H1, double H2, double Theta, double Eta, double A, double B0, double Bx, double By, double R)
                    = dataSet.DataTableSpot.GetPrms(i);
                dataSet.DataTableSpot.SetPrms(i, numericBoxFittingRange.Value, new[] { X0, Y0, H1, H2, Theta, Eta, A }, new[] { B0, Bx, By }, R);

            }
            bindingSourceObsSpots.DataMember = "DataTableSpot";
        }

        #endregion スポット情報テーブルに関連するメソッド

        #region　スポット情報のコピー、保存、読み込み

        private void buttonCopyToClipboad_Click(object sender, EventArgs e)
        {
            if (dataSet.DataTableSpot.Rows.Count > 1)
            {
                var sb = new StringBuilder();
                sb.Append("Direct\t");
                for (int i = 1; i < dataGridViewSpots.Columns.Count - 1; i++)
                    if (dataGridViewSpots.Columns[i].Visible)
                        sb.Append(dataGridViewSpots.Columns[i].HeaderText + "\t");
                sb.Append("\r\n");

                for (int j = 0; j < dataGridViewSpots.Rows.Count - 1; j++)
                {
                    sb.Append(((bool)dataGridViewSpots[0, j].Value ? "*" : "") + "\t");
                    for (int i = 1; i < dataGridViewSpots.ColumnCount - 1; i++)
                        if (dataGridViewSpots.Columns[i].Visible)
                            sb.Append(dataGridViewSpots[i, j].Value.ToString() + "\t");
                    sb.Append("\r\n");
                }

                if ((sender as Button).Name.Contains("Clipboad"))
                    Clipboard.SetDataObject(sb.ToString());
                else
                {
                    var dlg = new SaveFileDialog { Filter = "*.csv|*.csv" };
                    if (dlg.ShowDialog() == DialogResult.OK)
                        using (var sw = new StreamWriter(dlg.FileName, false, Encoding.GetEncoding("shift_jis")))
                            sw.Write(sb.ToString().Replace('\t', ','));
                }
            }
        }

        private void readCSV(string filename)
        {
            if (scalablePictureBoxAdvanced.PseudoBitmap == null || scalablePictureBoxAdvanced.PseudoBitmap.SrcValuesGray == null)
                return;
            try
            {
                bindingSourceObsSpots.DataMember = "";
                var f = new Func<string, double>(x => Convert.ToDouble(x));

                var dataList = new List<string[]>();
                using (var sr = new StreamReader(filename))
                {
                    if (sr.ReadLine().StartsWith("Direct,"))
                        while (sr.Peek() > -1)
                            dataList.Add(sr.ReadLine().Split(new[] { ',' }));
                    else
                        return;
                }

                dataList.Sort((a, b) =>
                {
                    var c = Convert.ToInt32(a[1]) - Convert.ToInt32(b[1]);
                    return (c > 0) ? 1 : (c < 0) ? -1 : 0;
                });

                for (int i = 0; i < dataList.Count; i++)
                {
                    var d = dataList[i];
                    (double Range, double X0, double Y0, double H1, double H2, double Theta, double Eta, double A, double B0, double Bx, double By, double R) =
                    dataSet.DataTableSpot.ConvertPrmsToOriginalValues(f(d[2]), f(d[3]), f(d[4]), f(d[5]), f(d[6]), f(d[7]), f(d[8]), f(d[9]), f(d[10]), f(d[11]), f(d[12]), f(d[13]));

                    dataSet.DataTableSpot.Add(d[0] == "*", Range, X0, Y0, H1, H2, Theta, Eta, A, B0, Bx, By, R);
                    if (d.Length == 17 && d[15] != "")
                        dataSet.DataTableSpot.SetHKL(i, d[15]);

                }

                bindingSourceObsSpots.DataMember = "DataTableSpot";
                scalablePictureBoxAdvanced.Refresh();
            }
            catch { MessageBox.Show("File open error"); }
        }

        #endregion

        #region スポット同定

        private void buttonIdentifySpots_Click(object sender, EventArgs e)
        {
            sw.Restart();
            clearStatusLabel();
            FormMain.Enabled = false;
            buttonIdentifySpots.Visible = false;
            buttonStop.Visible = true;
            var crystals = new List<Crystal>();
            for (int j = 0; j < FormMain.listBox.SelectedItems.Count; j++)
                crystals.Add((Crystal)FormMain.listBox.SelectedItems[j]);

            backgroundWorkerSpotID.RunWorkerAsync(crystals);
        }

        private void buttonStop_Click(object sender, EventArgs e)
            => backgroundWorkerSpotID.CancelAsync();

        private void backgroundWorkerSpotID_DoWork(object sender, DoWorkEventArgs e)
            => e.Result = identifySpots((List<Crystal>)(e.Argument));

        private void backgroundWorkerSpotID_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null && e.Result is List<List<Grain>> candidates)
            {
                dataSet.DataTableCandidate.Clear();
                for (int i = 0; i < candidates.Count; i++)
                    dataSet.DataTableCandidate.Add(i, candidates[i]);
                toolStripProgressBar.Value = toolStripProgressBar.Maximum;
                toolStripStatusLabelFindSpot.Text = $"Completed! Total time: {sw.ElapsedMilliseconds / 1000.0:f2}sec.";
            }
            buttonIdentifySpots.Visible = true;
            buttonStop.Visible = false;
            FormMain.Enabled = true;
        }


        private void backgroundWorkerSpotID_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (skipProgressChangedEvent) return;
            skipProgressChangedEvent = true;
            try
            {
                double progress = (double)e.ProgressPercentage / 1000000;
                toolStripStatusLabelFindSpot.Text = $"Ellapsed time: {(double)sw.ElapsedMilliseconds / 1000:f2} sec.";
                //+" Wait about: " + ((1 - progress) / progress * (sw.ElapsedMilliseconds / 1000.0)).ToString("f2") + "sec.";
                toolStripProgressBar.Value = (int)(toolStripProgressBar.Maximum * progress);
                Application.DoEvents();
            }
            catch { }
            skipProgressChangedEvent = false;
        }

       

        private List<List<Grain>> identifySpots(List<Crystal> crystals)
        {
            var p = scalablePictureBoxAdvanced.PseudoBitmap;
            Detector = new AreaDetector(p.Width, p.Height, numericBoxPixelSize.Value, dataSet.DataTableSpot.DirectSpotPosition, waveLengthControl1.Property, CameraLength);
            Detector.setMaxReciprocalZ();

            var crystalCount = crystals.Count;
            var vec = new List<List<Vector3D>>();
            for (int j = 0; j < crystalCount; j++)
            {
                var c = crystals[j];
                c.AngleResolution = 180;
                c.SetCrystallites();
                c.Crystallites.SpotShapesAngleDivision = 1;

                Detector.WaveSource = WaveSource.None;//SetGvectorの中で強度計算させないようにNoneにしておく
                if (waveLengthControl1.Property.Source == WaveSource.Electron)
                {
                    c.GrainSize = 6;
                    c.Crystallites.SetGVector(Detector, false, checkBoxIgnoreMultipleDiffraction.Checked, false);
                }
                else if (waveLengthControl1.Property.Source == WaveSource.Xray)
                {
                    c.GrainSize = 6;
                    c.Crystallites.SetGVector(Detector, false, true, false);
                }
                Detector.WaveSource = waveLengthControl1.Property.Source;//Noneから元に戻す

                vec.Add(new List<Vector3D>());
                for (int i = 0; i < c.VectorOfG.Count; i++)
                    vec[j].Add(Deep.Copy(c.VectorOfG[i]));
            }
            var obsSpots = dataSet.DataTableSpot.Spots;

            var candidates = new List<List<Grain>>();

            var func = new Func<int, List<Vector3DBase>, int[], List<Grain>>((id, v2, exceptedIndices) =>
                {
                    var cand = getRotationCandidatesFrom2Spots2(vec[id].ToArray(), v2.ToArray(), exceptedIndices)
                                    .OrderByDescending(c => c.Indices.Length).ToList();
                    for (int j = 0; j < cand.Count; j++)
                    {
                        for (int k = j + 1; k < cand.Count; k++)
                            if (Math.Cos(ToleranceAngle) < Math.Abs(((cand[k].Rotation * cand[j].Rotation.Inverse()).SumOfDiagonalCompenent() - 1) / 2))
                                cand.RemoveAt(k--);
                        cand[j].ID = id;
                        cand[j].CrystalName = ((Crystal)FormMain.listBox.SelectedItems[id]).Name;
                        cand[j].Spots = ((Crystal)FormMain.listBox.SelectedItems[id]).Crystallites.GetSpotPosition(Detector, cand[j].Rotation);
                    }
                    return cand;
                });

            //まず、最初の1回チャレンジ
            for (int i = 0; i < crystalCount; i++)
            {
                var cand = func(i, dataSet.DataTableSpot.ReciprocalVectors, null);
                for (int j = 0; j < cand.Count; j++)
                    candidates.Add(new List<Grain>(new[] { cand[j] }));
            }

            if (radioButtonMultiGrain.Checked)
            {
                if (candidates.Count > 5)
                    candidates.RemoveRange(5, candidates.Count - 5);
                for (int n = 1; n < numericBoxMaxGrainNum.Value; n++)
                {
                    var candidates2 = new List<List<Grain>>();
                    for (int i = 0; i < candidates.Count; i++)
                        for (int j = 0; j < crystalCount; j++)
                        {
                            var exceptedIndices = new List<int>();
                            foreach (Grain g in candidates[i])
                                exceptedIndices.AddRange(g.Indices.Select(o => o.No).ToArray());

                            var cand = func(j, dataSet.DataTableSpot.ReciprocalVectors, exceptedIndices.ToArray());
                            if (cand.Count == 0)
                            { candidates2.Add(candidates[i]); }
                            else
                                for (int k = 0; k < cand.Count; k++)
                                {
                                    List<Grain> temp = new List<Grain>();
                                    for (int l = 0; l < candidates[i].Count; l++)
                                        temp.Add(candidates[i][l]);
                                    temp.Add(cand[k]);
                                    candidates2.Add(temp);
                                }
                        }
                    candidates = candidates2;
                }
            }
            return candidates.OrderByDescending(c1 =>
            {
                //var indices = new List<int>();
                //foreach (var g in c1)
                //    indices = indices.Union(g.Indices).ToList();
                //return indices.Count();
                return c1.Sum(g => g.Indices.Count());
            }).ToList();
        }

        /// <summary>
        /// 観察されたスポットの内、2つのスポットを説明しうる回転を探索し、その回転行列リストを返す。
        /// </summary>
        /// <param name="gVectors"></param>
        /// <param name="obsSpotsReciprocal"></param>
        /// <returns></returns>
        private List<Grain> getRotationCandidatesFrom2Spots2(Vector3D[] vectorOfG, Vector3DBase[] obsSpotsReciprocal, int[] exceptedIndices = null)
        {
            //maxNum個数のobsSpotに対する候補となるgVectorsを定義しておく
            var gVectors = new List<List<Vector3D>>();
            for (int i = 0; i < obsSpotsReciprocal.Length; i++)
            {
                gVectors.Add(new List<Vector3D>());
                //indexで指定されたd_spacingに近いg_vectorを探す
                var d = dataSet.DataTableSpot.Dscacing[i];
                gVectors[i].AddRange(vectorOfG.Where(g => g.d > d * (1 - ToleranceLength) && g.d < d * (1 + ToleranceLength)).ToArray());
            }

            if (exceptedIndices == null)
                exceptedIndices = new int[0];
            var obsSpotsReciprocal2 = new List<Vector3DBase>();
            var gVectors2 = new List<List<Vector3D>>();
            for (int i = 0; i < obsSpotsReciprocal.Count(); i++)
                if (!exceptedIndices.Contains(i))
                {
                    obsSpotsReciprocal2.Add(obsSpotsReciprocal[i]);
                    gVectors2.Add(gVectors[i]);
                }

            var mList = new List<Matrix3D>();
            for (int max = 1; max < obsSpotsReciprocal2.Count(); max++)
            {
                for (int i = 0; i < max; i++)
                {
                    foreach (var vec1 in gVectors2[i].Where(g => FormMain.Crystal.Symmetry.IsPlaneRootIndex(g.h, g.k, g.l)))
                    {
                        var angle = Vector3DBase.AngleBetVectors(obsSpotsReciprocal2[i], obsSpotsReciprocal2[max]);//i番目のスポットと、j番目のスポットのなす角度
                        if (angle > 30.0 / 180.0 * Math.PI && angle < 150.0 / 180.0 * Math.PI)
                        {
                            var vX1 = Vector3DBase.VectorProduct(obsSpotsReciprocal2[i], obsSpotsReciprocal2[max]).Normarize();
                            var vY1 = (obsSpotsReciprocal2[i].Normarize() + obsSpotsReciprocal2[max].Normarize()).Normarize();
                            var m1 = new Matrix3D(vX1, vY1, Vector3DBase.VectorProduct(vX1, vY1));
                            foreach (Vector3D vec2 in gVectors2[max].Where(g => Math.Abs(angle - Vector3D.AngleBetVectors(g, vec1)) < ToleranceAngle))
                            {
                                var vX2 = Vector3DBase.VectorProduct(vec1, vec2).Normarize();
                                var vY2 = (vec1.Normarize() + vec2.Normarize()).Normarize();
                                mList.Add(new Matrix3D(vX2, vY2, Vector3DBase.VectorProduct(vX2, vY2)) * m1.Inverse());
                            }

                            if (backgroundWorkerSpotID.CancellationPending)
                                return null;
                        }
                    }
                }
                if (max > 10 && mList.Any())
                    break;
            }

            var candidates = new List<Grain>();
            int counter = 0;
            Parallel.ForEach(mList, rot =>
            {
                var result = new Matrix3D( rot);
                var residual = 0.0;
                var indices = new List<(int No, int H, int K, int L)>();
                for (int n = 0; n < 4; n++)
                {
                    if (backgroundWorkerSpotID.CancellationPending)
                        break;

                    var obsList = new List<Vector3DBase>();
                    var refList = new List<Vector3DBase>();
                    int beforeCount = 0;
                    indices = new List<(int No, int H, int K, int L)>();

                    for (int k = 0; k < obsSpotsReciprocal.Length; k++)
                        if (gVectors[k].Any())
                        {
                            if (backgroundWorkerSpotID.CancellationPending) break;

                            var obsV = rot * obsSpotsReciprocal[k];
                            //最も近いgVectorを探す
                            var min = double.PositiveInfinity;
                            Vector3D v2 = null;
                            for (int l = 0; l < gVectors[k].Count; l++)
                            {
                                double temp = (gVectors[k][l] - obsV).Length2;
                                if (min > temp && !refList.Contains(gVectors[k][l]))
                                {
                                    v2 = gVectors[k][l];
                                    min = temp;
                                }
                            }

                            if (v2 != null && Vector3DBase.AngleBetVectors(obsV, v2) < ToleranceAngle * 2)//許容角度であれば、リストに追加
                            {
                                indices.Add((k, v2.h, v2.k, v2.l));
                                obsList.Add(obsSpotsReciprocal[k]);
                                refList.Add(v2);
                            }
                        }
                    if (indices.Count > 1)
                        (result, residual) = GetRotationMatrix2(obsList.ToArray(), refList.ToArray(), result);
                    if (obsList.Count == beforeCount)
                        break;
                    beforeCount = obsList.Count;
                }

                lock (lockObj)
                {
                    if (indices.Count > 1)
                        candidates.Add(new Grain(result.Inverse(), residual, indices.ToArray()));
                    if (counter++ % 10 == 0)
                        backgroundWorkerSpotID.ReportProgress((int)(1000000.0 / mList.Count * counter));
                }
            }
            );

            return candidates;

            // 3次元ベクトル集合 v1をなるべくv2に近づけるような回転行列を求めるローカル関数。戻り値は、残差の二乗和を個数で割ったもの
            (Matrix3D Rotation, double Residual) GetRotationMatrix2(Vector3DBase[] v1, Vector3DBase[] v2, Matrix3D initialRotation)
            {
                var func = new Func<double, double, double, double>((phi, theta, psi) =>
                {
                    double cosTheta = Math.Cos(theta), sinTheta = Math.Sin(theta), cosPsi = Math.Cos(psi), sinPsi = Math.Sin(psi), cosPhi = Math.Cos(phi), sinPhi = Math.Sin(phi);
                    double e11 = cosPhi * cosPsi - cosTheta * sinPhi * sinPsi, e12 = -cosPhi * sinPsi - cosTheta * sinPhi * cosPsi, e13 = sinTheta * sinPhi;
                    double e21 = sinPhi * cosPsi + cosTheta * cosPhi * sinPsi, e22 = -sinPhi * sinPsi + cosTheta * cosPhi * cosPsi, e23 = -sinTheta * cosPhi;
                    double e31 = sinPsi * sinTheta, e32 = cosPsi * sinTheta, e33 = cosTheta;

                    return v1.Select((v, i) =>
                    {
                        var xDev = e11 * v.X + e12 * v.Y + e13 * v.Z - v2[i].X;
                        var yDev = e21 * v.X + e22 * v.Y + e23 * v.Z - v2[i].Y;
                        var zDev = e31 * v.X + e32 * v.Y + e33 * v.Z - v2[i].Z;
                        return xDev * xDev + yDev * yDev + zDev * zDev;
                    }).Sum();
                });

                var euler = Euler.GetEulerAngle(initialRotation);
                var r = MathNet.Numerics.FindMinimum.OfFunction(func, euler.Phi, euler.Theta, euler.Psi);

                return (Euler.SetEulerAngle(r.Item1, r.Item2, r.Item3), func(r.Item1, r.Item2, r.Item3) / v1.Length);
            }
        }

        #endregion

        public class Grain : IComparable
        {
            public Matrix3D Rotation;
            public double Residual;

            /// <summary>
            /// 観測された回折スポット配列に対して、このGrainで説明可能であったindicesのリスト
            /// </summary>
            public (int No, int H, int K, int L)[] Indices;

            /// <summary>
            /// このGrainによって出現する全てのスポットの位置(x,y)と強度(z)
            /// </summary>
            public Vector3D[] Spots;

            public int ID;
            public string CrystalName;

            public int CompareTo(object obj) => Residual.CompareTo(((Grain)obj).Residual);

            public Grain(Matrix3D rotation, double residual, (int No, int H, int K, int L)[] indices, int id = -1)
            {
                Rotation = rotation;
                Residual = residual;
                Indices = indices;
                ID = id;
            }
        }

        #region お蔵入り

        /// <summary>
        /// 観察されたスポットの内、2つのスポットを説明しうる回転を探索し、その回転行列リストを返す。
        /// </summary>
        /// <param name="gVectors"></param>
        /// <param name="obsSpotsReciprocal"></param>
        /// <returns></returns>
        private List<Matrix3D> getRotationCandidatesFrom2Spots(Vector3DBase[] obsSpotsReciprocal, double toleranceLength, double toleranceAngle)
        {
            //maxNum個数のobsSpotに対する候補となるgVectorsを定義しておく
            int maxNum = 10;
            List<List<Vector3D>> gVectors = new List<List<Vector3D>>();
            for (int i = 0; i < maxNum && i < obsSpotsReciprocal.Length; i++)
            {
                gVectors.Add(new List<Vector3D>());
                //indexで指定されたd_spacingに近いg_vectorを探す
                var d = dataSet.DataTableSpot.Dscacing[i];
                gVectors[i].AddRange(FormMain.Crystal.VectorOfG.Where(g => g.d > d * (1 - toleranceLength) && g.d < d * (1 + toleranceLength)).ToArray());
            }

            List<Matrix3D> mList = new List<Matrix3D>();
            for (int i = 0; i < gVectors.Count - 1; i++)
            {
                foreach (Vector3D vec1 in gVectors[i].Where(g => FormMain.Crystal.Symmetry.IsPlaneRootIndex(g.h, g.k, g.l)))
                {
                    for (int j = i + 1; j < gVectors.Count; j++)
                    {
                        double angle = Vector3D.AngleBetVectors(obsSpotsReciprocal[i], obsSpotsReciprocal[j]);//i番目のスポットと、j番目のスポットのなす角度
                        if (angle > 60.0 / 180.0 * Math.PI && angle < 120.0 / 180.0 * Math.PI)
                        {
                            var vX1 = Vector3DBase.VectorProduct(obsSpotsReciprocal[i], obsSpotsReciprocal[j]).Normarize();
                            var vY1 = (obsSpotsReciprocal[i].Normarize() + obsSpotsReciprocal[j].Normarize()).Normarize();
                            Matrix3D m1 = new Matrix3D(vX1, vY1, Vector3DBase.VectorProduct(vX1, vY1));
                            foreach (Vector3D vec2 in gVectors[j].Where(g => Math.Abs(angle - Vector3D.AngleBetVectors(g, vec1)) < toleranceAngle))
                            {
                                var vX2 = Vector3DBase.VectorProduct(vec1, vec2).Normarize();
                                var vY2 = (vec1.Normarize() + vec2.Normarize()).Normarize();
                                mList.Add(m1 * new Matrix3D(vX2, vY2, Vector3DBase.VectorProduct(vX2, vY2)).Inverse());
                            }
                        }
                    }
                }
            }
            return mList;
        }

        #endregion

        #region お蔵入り

        private double evaluate(AreaDetector detector, Matrix3D initialRot, Vector3DBase[] obsSpots, Vector3DBase[] calSpots, double toleranceLength, double toleranceAngle, ref Matrix3D optimizedRot)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            List<double> residual = new List<double>();
            var obsPoints = obsSpots.Select(s => (new PointD(s.X, s.Y) - DirectSpot)).ToArray();
            var calPoints = calSpots.Select(s => (new PointD(s.X, s.Y) - DirectSpot)).ToArray();
            double bestRot = double.NaN, bestScale = double.NaN, bestResidual = double.PositiveInfinity;
            //まず最初に拡大量と回転角度の微調節量を計算する
            for (double rot = -toleranceAngle; rot < toleranceAngle; rot += toleranceAngle / 3.0)
                for (double scale = 1 - toleranceLength; scale < 1 + toleranceLength; scale += toleranceLength / 3.0)
                {
                    dic = new Dictionary<int, int>();
                    residual = new List<double>();
                    var obsPoints2 = obsPoints.Select(p => new PointD(Math.Cos(rot) * p.X - Math.Sin(rot) * p.Y, Math.Sin(rot) * p.X + Math.Cos(rot) * p.Y) * scale).ToArray();

                    for (int i = 0; i < obsPoints2.Length; i++)
                    {
                        PointD obs = obsPoints2[i];
                        int index = -1;
                        double dev = double.MaxValue;
                        for (int j = 0; j < calPoints.Length; j++)
                        {
                            if (!dic.ContainsValue(j))
                            {
                                double tempDev = (obs - calPoints[j]).Length / obs.Length;
                                if (tempDev < toleranceLength && tempDev < dev)
                                {
                                    index = j;
                                    dev = tempDev;
                                }
                            }
                        }
                        if (index != -1)
                        {
                            dic.Add(i, index);
                            residual.Add(dev * Math.Pow(1.01, i));
                        }
                        else
                            residual.Add(toleranceLength * 2 * Math.Pow(1.01, i));
                    }
                    if (bestResidual > residual.Sum())
                    {
                        bestRot = rot;
                        bestScale = scale;
                        bestResidual = residual.Sum();
                    }
                }

            optimizedRot = 1 / bestScale * Matrix3D.Rot(new Vector3DBase(0, 0, 1), bestRot) * initialRot;
            // optimizedRot = initialRot;
            return bestResidual;
        }

        #endregion

        #region 候補情報テーブルのイベント

        private void bindingSourceCandidates_CurrentChanged(object sender, EventArgs e)
        {
            try
            {
                if (bindingSourceCandidates.Current != null)
                {
                    var c = (List<Grain>)((DataRowView)bindingSourceCandidates.Current).Row["Candidate"];
                    dataSet.DataTableGrain.Clear();
                    for (int i = 0; i < c.Count; i++)
                        dataSet.DataTableGrain.Add(c[i]);
                }
            }
            catch { }
        }

        private void bindingSourceGrains_CurrentChanged(object sender, EventArgs e)
        {
            try
            {
                scalablePictureBoxAdvanced.Symbols.RemoveAll(s => s.Tag == tagCalcSpot);//まず、全部削除

                List<ScalablePictureBox.Symbol> calcSpots = new List<ScalablePictureBox.Symbol>();
                if (dataGridViewGrains.SelectedRows.Count > 1)
                {
                    foreach (DataGridViewRow r in dataGridViewGrains.Rows)
                    {
                        if (r.Selected && r.Cells != null && r.Cells.Count > 0 && r.Cells[0] != null && r.Cells[0].Value != null)
                        {
                            var n = (int)r.Cells[0].Value;
                            var g = (Grain)dataSet.DataTableGrain.Rows[n]["Grain"];
                            var name = (string)dataSet.DataTableGrain.Rows[n]["CrystalName"];
                            //シンボルを更新
                            foreach (var spot in g.Spots)
                            {
                                var s = new ScalablePictureBox.Symbol(
                                    $"{name}{n}: {spot.h} {spot.k} {spot.l}",
                                    new PointD(spot.X, spot.Y),
                                    Color.LightGreen, Color.DarkGreen, 5);
                                s.Tag = tagCalcSpot;

                                calcSpots.Add(s);
                            }
                        }
                    }
                }
                else if (bindingSourceGrains.Current != null)
                {
                    var g = (Grain)((DataRowView)bindingSourceGrains.Current).Row["Grain"];

                    //hklを書き換え
                    skipEvent = true;
                    for (int i = 0; i < dataSet.DataTableSpot.Count; i++)
                    {
                        var index = g.Indices.Where(index => index.No == i).ToArray();
                        dataSet.DataTableSpot.SetHKL(i, index.Length != 1 ? "" : $"{index[0].H} {index[0].K} {index[0].L}");
                    }
                    dataSet.DataTableSpot.SetHKL(dataSet.DataTableSpot.DirectSpotNo, " 0 0 0");
                    foreach (var (No, H, K, L) in g.Indices)
                        dataSet.DataTableSpot.SetHKL(No, $" {H} {K} {L}");
                    skipEvent = false;

                    //シンボルを更新
                    foreach (var spot in g.Spots)
                    {
                        var s = new ScalablePictureBox.Symbol(
                           spot.h + " " + spot.k + " " + spot.l,
                           new PointD(spot.X, spot.Y),
                            Color.LightGreen, Color.DarkGreen, 5);
                        s.Tag = tagCalcSpot;
                        calcSpots.Add(s);
                    }

                    FormMain.SetRotation(g.Rotation);
                }

                scalablePictureBoxAdvanced.Symbols.AddRange(calcSpots);

                checkBoxShowObsSpots_CheckedChanged(sender, e);
                //scalablePictureBoxAdvanced.Refresh(); //上のイベントで呼ばれるので、コメントアウト
            }
            catch { }
        }

        #endregion

        /// <summary>
        /// シンボル、ラベルの表示のチェックボックスイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxShowObsSpots_CheckedChanged(object sender, EventArgs e)
        {

            checkBoxShowObsSpotLabel.Enabled = checkBoxShowObsSpotSymbol.Checked;
            checkBoxShowCalcSpotLabel.Enabled = checkBoxShowCalcSpotSymbol.Checked;


            for (int i = 0; i < scalablePictureBoxAdvanced.Symbols.Count; i++)
            {
                var s = scalablePictureBoxAdvanced.Symbols[i];
                if (s.Tag == tagObsSpot)
                {
                    s.SymbolVisible = checkBoxShowObsSpotSymbol.Checked;
                    s.LabelVisible = checkBoxShowObsSpotLabel.Checked;
                }
                if (s.Tag == tagCalcSpot)
                {
                    s.SymbolVisible = checkBoxShowCalcSpotSymbol.Checked;
                    s.LabelVisible = checkBoxShowCalcSpotLabel.Checked;
                }
            }
            scalablePictureBoxAdvanced.Refresh();
        }

        private void scalablePictureBoxAdvanced_StatusChanged(object sender, EventArgs e) => toolStripStatusLabelImageFilter.Text = scalablePictureBoxAdvanced.StatusLabel;

        private void radioButtonSingleGrain_CheckedChanged(object sender, EventArgs e) => numericBoxMaxGrainNum.Enabled = radioButtonMultiGrain.Checked;

        private void buttonPixelToPixel_Click(object sender, EventArgs e)
        {
            if (scalablePictureBoxAdvanced.PseudoBitmap != null && scalablePictureBoxAdvanced.PseudoBitmap.Width != 0)
            {
                this.Size = new Size(
                    this.Size.Width - scalablePictureBoxAdvanced.PictureSize.Width + scalablePictureBoxAdvanced.PseudoBitmap.Width,
                    this.Size.Height - scalablePictureBoxAdvanced.PictureSize.Height + scalablePictureBoxAdvanced.PseudoBitmap.Height
                    );
            }
        }

        private void checkBoxShowDebyeRing_CheckedChanged(object sender, EventArgs e)
        {
            SetCrystal();
            for (int i = 0; i < scalablePictureBoxAdvanced.Symbols.Count; i++)
            {
                if (scalablePictureBoxAdvanced.Symbols[i].Shape == ScalablePictureBox.SymbolShape.Circle)
                {
                    scalablePictureBoxAdvanced.Symbols[i].SymbolVisible = checkBoxShowDebyeRing.Checked;
                }
            }
            scalablePictureBoxAdvanced.Refresh();
        }

        internal void SetCrystal()
        {
            if (scalablePictureBoxAdvanced.PseudoBitmap == null || scalablePictureBoxAdvanced.PseudoBitmap.Width == 0)
                return;
            /*
            for (int i = 0; i < scalablePictureBoxAdvanced.Symbols.Count; i++)
                if (scalablePictureBoxAdvanced.Symbols[i].Shape == ScalablePictureBox.SymbolShape.Circle)
                    scalablePictureBoxAdvanced.Symbols.RemoveAt(i--);
            var c = FormMain.Crystal;

            //最小のd値を決める
            double width = scalablePictureBoxAdvanced.PseudoBitmap.Width;
            double height = scalablePictureBoxAdvanced.PseudoBitmap.Height;
            double maxL = new double[] { DirectSpot.Length(), (DirectSpot - new PointD(width, height)).Length(), (DirectSpot - new PointD(0, height)).Length(), (DirectSpot - new PointD(width, 0)).Length() }.Max() * PixelSize;
            double minD = waveLengthControl1.WaveLength / 2.0 / Math.Sin(Math.Atan(maxL / CameraLength) / 2);
            FormMain.Crystal.SetPlanes(double.MaxValue, minD, true, true, true, true, HorizontalAxis.d, 0.0001, 0);
            foreach ( var p in FormMain.Crystal.Plane)
            {
                var radius = CameraLength * Math.Tan(2 * Math.Asin(waveLengthControl1.WaveLength / 2 / p.d)) / PixelSize;
                var symbol = new ScalablePictureBox.Symbol(p.strHKL, DirectSpot, radius, Color.Yellow);
                symbol.SymbolVisible = checkBoxShowDebyeRing.Checked;
                scalablePictureBoxAdvanced.Symbols.Add(symbol);
            }
            scalablePictureBoxAdvanced.Refresh();
            */
        }

        #region メタファイル関連

        private void buttonCopyMetafile_Click(object sender, EventArgs e)
        {
            Graphics grfx = CreateGraphics();
            IntPtr ipHdc = grfx.GetHdc();
            MemoryStream ms = new MemoryStream();
            Metafile mf = new Metafile(ms, ipHdc, EmfType.EmfPlusDual);
            grfx.ReleaseHdc(ipHdc);
            grfx.Dispose();
            DrawMetafile(mf);
            ClipboardMetafileHelper.PutEnhMetafileOnClipboard(this.Handle, mf);
        }

        //MetaFileに描画
        public void DrawMetafile(Metafile mf)
        {
            if (scalablePictureBoxAdvanced.PseudoBitmap == null || scalablePictureBoxAdvanced.PseudoBitmap.Width <= 0 || scalablePictureBoxAdvanced.PseudoBitmap.Height <= 0) return;
            Graphics g = Graphics.FromImage(mf);
            //gMain.Clear(colorControlBack.Color);
            //gMain.SmoothingMode = SmoothingMode.AntiAlias;
            //this.DoubleBuffered = true;
            //gMain.FillRectangle(new SolidBrush(colorControlBack.Color), new Rectangle(0, 0, pictureBoxMain.Width, pictureBoxMain.Height));
            //gMain.Clear(colorControlBack.Color);

            float radius = 5f;
            foreach (var s in scalablePictureBoxAdvanced.Symbols.Where(symbol => symbol.Tag == tagObsSpot))
            {
                g.DrawEllipse(new Pen(Color.LightGreen, 1f), new RectangleF((float)s.CrossPosition.X - radius, (float)s.CrossPosition.Y - radius, radius * 2, radius * 2));
            }

            g.Dispose();
        }

        #endregion

        private void checkBoxDetailsSpot_CheckedChanged(object sender, EventArgs e) => FormSpotDetails.Visible = checkBoxDetailsSpot.Checked;

        #region Refine thickness and direction機能
        private void ButtonRefineThicknessAndDirection_Click(object sender, EventArgs e)
        {
            if (FormMain.Crystal.Bethe.IsBusy) return;
            clearStatusLabel();

            splitContainer1.Enabled = false;

            //イベント追加
            FormMain.Crystal.Bethe.CbedCompleted += Bethe_CbedCompleted;
            FormMain.Crystal.Bethe.CbedProgressChanged += Bethe_CbedProgressChanged;

            //ローテーション配列を作る //半径1の円の中に一辺1/Nの長さの正方形を詰め込み、一つの正方形の中心は、円の中心と一致するような問題を考える
            var rotations = new List<Matrix3D>();
            var side = 2.0 / 30;//30分割
            var alphaMax = numericBoxSemiangle.Value * Math.PI / 1000;// 3 mrad;
            var max = (int)(1 / side) + 1;

            for (int h = -max; h <= max; h++)
                for (int w = max; w >= -max; w--)
                    if (h * h + w * w <= max * max)
                        rotations.Add(Matrix3D.Rot(new Vector3DBase(h, -w, 0), Math.Sqrt(w * side * w * side + h * side * h * side) * alphaMax));
                    else
                        rotations.Add(null);
            var rotArray = rotations.ToArray();
            var thicknessArray = Enumerable.Range(50, 500).Select(v => (double)v).ToArray();

            var g = (Grain)((DataRowView)bindingSourceGrains.Current).Row["Grain"];
            FormMain.Crystal.Bethe.RunCBED(numericBoxMaxNumOfG.ValueInteger, 200, g.Rotation, thicknessArray, rotArray, BetheMethod.Solver.Auto);
        }



        private void Bethe_CbedCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //イベント削除
            FormMain.Crystal.Bethe.CbedCompleted -= Bethe_CbedCompleted;
            FormMain.Crystal.Bethe.CbedProgressChanged -= Bethe_CbedProgressChanged;

            //検出しているスポットの強度を取得
            var spots = dataSet.DataTableSpot.Spots;

            //現在選択しているグレインと対応させる
            var grain = (Grain)((DataRowView)bindingSourceGrains.Current).Row["Grain"];

            //Bethe.Disk中のインデックスと、g.Indicesの対応付け
            var disks = FormMain.Crystal.Bethe.Disks;
            var tempDisk = disks[0].Select((disk, index) => (disk, index));
            var corrTable = new List<(int index, double intensity)>();
            foreach (var (No, H, K, L) in grain.Indices)
            {
                var temp = tempDisk.Where(o => H == o.disk.H && K == o.disk.K && L == o.disk.L).ToArray();
                if (temp.Length == 1)
                    corrTable.Add((temp[0].index, spots[No].A / spots.Max(s => s.A)));
            }

            var bestResidual = double.PositiveInfinity;
            var bestThickness = 0.0;
            var bestDirection = new Matrix3D();

            // R = (oI_1 - a * cI_1)^2 + (oI_2 - a * cI_2)^2 + .... + (oI_n - a * cI_n)^2 の最小化問題
            //この時 a = (oI_1 * cI_1 + oI_2 * cI_2 + ... oI_n * cI_n) / (cI_1^2 + cI_2^2 + ... cI_n^2)
            for (int t = 0; t < FormMain.Crystal.Bethe.Thicknesses.Length; t++)
            {
                for (int r = 0; r < FormMain.Crystal.Bethe.BeamRotations.Length; r++)
                {
                    var numer = corrTable.Sum(c => c.intensity * disks[t][c.index].Intensity[r]);
                    var denom = corrTable.Sum(c => Math.Pow(disks[t][c.index].Intensity[r], 2));
                    if (denom != 0)
                    {
                        var a = numer / denom;
                        var residual = corrTable.Sum(c => Math.Pow(c.intensity - a * disks[t][c.index].Intensity[r], 2));
                        if (bestResidual > residual)
                        {
                            bestResidual = residual;
                            bestThickness = FormMain.Crystal.Bethe.Thicknesses[t];
                            bestDirection = FormMain.Crystal.Bethe.BeamRotations[r];
                        }
                    }
                }
            }

            splitContainer1.Enabled = true;
            FormMain.SetRotation(bestDirection * grain.Rotation);
            FormMain.FormDiffractionSimulator.Thickness = bestThickness;
        }

        bool skipProgressChangedEvent = false;
        private void Bethe_CbedProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
            if (skipProgressChangedEvent) return;
            skipProgressChangedEvent = true;
            var current = e.ProgressPercentage;
            var solver = (string)e.UserState;var sec = sw.ElapsedMilliseconds / 1000.0;
            var divisionNumber = FormMain.Crystal.Bethe.RotationArrayValidLength;
            var progress = (int)(100.0 * current / divisionNumber);
            if (progress <= 100)
                toolStripProgressBar.Value = toolStripProgressBar.Maximum * current / divisionNumber;
            toolStripStatusLabelRefine.Text = $"Ellapsed time : {sec:f2} s.,  time/pixel: ";
            toolStripStatusLabelRefine.Text += sec / current > 0.9 ? $"{sec / current:f2} s.,  " : $"{sec / current * 1000:f2} ms., ";
            toolStripStatusLabelRefine.Text += $"{100.0 * current / divisionNumber:f1} % completed,  wait for {sec * (divisionNumber - current) / current:f2} s.";

            Application.DoEvents();
            skipProgressChangedEvent = false;
        }

        #endregion

        private void numericBoxFittingRange_Load(object sender, EventArgs e)
        {

        }

        private void buttonDonut_Click(object sender, EventArgs e)
        {
            if (dataSet.DataTableSpot.Spots.Count == 0) return;
            Enabled = false;
            Application.DoEvents();
            sw.Restart();
            bindingSourceObsSpots.DataMember = "";


            int width = scalablePictureBoxAdvanced.PseudoBitmap.Width, height = scalablePictureBoxAdvanced.PseudoBitmap.Height;
            var srcValues = scalablePictureBoxAdvanced.PseudoBitmap.SrcValuesGray;
            for (int i = 0; i < dataSet.DataTableSpot.Spots.Count; i++)
            {
                //現在のスポットのパラメータを取得
                var prms = dataSet.DataTableSpot.GetPrms(i);

                List<double> core = new List<double>(), mantle = new List<double>();
                double range1 = prms.Range, range2 = range1 + numericBoxDonut.Value;
                for (int y = Math.Max(0, (int)(prms.Y0 - range2 - 2)); y < Math.Min(height, (int)(prms.Y0 + range2 + 2)); y++)
                    for (int x = Math.Max(0, (int)(prms.X0 - range2 - 2)); x < Math.Min(width, (int)(prms.X0 + range2 + 2)); x++)
                    {
                        var r = (x - prms.X0) * (x - prms.X0) + (y - prms.Y0) * (y - prms.Y0);

                        if (r < range1 * range1)
                            core.Add(srcValues[x + y * width]);
                        else if (r < range2 * range2)
                            mantle.Add(srcValues[x + y * width]);
                    }
                var intensity = core.Sum() - (mantle.Sum() * core.Count / mantle.Count);
                dataSet.DataTableSpot.SetPrms(i, prms.Range, new[] { prms.X0, prms.Y0, 0.0, 0.0, 0.0, 0.0, intensity }, new[] { 0.0, 0.0, 0.0 }, 0);
            }
            bindingSourceObsSpots.DataMember = "DataTableSpot";
            toolStripStatusLabelIdentifySpot.Text = $" Fitting time ({dataSet.DataTableSpot.Rows.Count} spots): {sw.ElapsedMilliseconds} ms.";
            Enabled = true;
        }
    }
}