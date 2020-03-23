using Crystallography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReciPro
{
    public partial class FormDiffractionSimulatorCBED : Form
    {
        public FormDiffractionSimulator FormDiffractionSimulator;

        private double Voltage => FormDiffractionSimulator.waveLengthControl.Energy;
        private double WaveLength => FormDiffractionSimulator.WaveLength;
        private double Thickness => numericBoxWholeThicknessStart.Value;

        private Crystal Crystal => FormDiffractionSimulator.formMain.Crystal;

        public int DivisionNumber
        {
            get
            {
                int count = 0;
                var side = 2.0 / Division;
                var max = (int)(1 / side) + 1;
                for (int h = -max; h <= max; h++)
                    for (int w = max; w >= -max; w--)
                        if (h * h + w * w <= max * max)
                            count++;
                return count;

            }
        }

        public double ResolutionInverse { get; set; }
        public double ResolutionReal { get; set; }

        //public double Defocus { get { return numericBoxDefocus.Value; } }
        //public double Cs { get { return numericBoxCs.Value; } }
        public double AlphaMax => trackBarAdvancedAlphaMax.Value / 1000.0;

        public bool DrawGuideCircles => checkBoxDrawGuideCircles.Checked;
        public int MaxNumOfBloch => numericBoxMaxNumOfG.ValueInteger;

        public int Division => numericBoxDivision.ValueInteger;

        public double[] ThicknessArray
        {
            get
            {
                List<double> thicknessArray = new List<double>();
                for (double thickness = numericBoxWholeThicknessStart.Value; thickness <= numericBoxThicknessEnd.Value; thickness += numericBoxThicknessStep.Value)
                    thicknessArray.Add(thickness);
                return thicknessArray.ToArray();
            }
        }

        public double[] SrcImage = null;
        public double[] DestImage = null;

        public int ImageWidth => numericBoxImageSize.ValueInteger;
        public int ImageHeight => numericBoxImageSize.ValueInteger;
        public double ImageCenterX => numericBoxImageSize.ValueInteger / 2.0;
        public double ImageCenterY => numericBoxImageSize.ValueInteger / 2.0;

        public double ImagePixelSize { get; set; } = 0;
        public double AngleResolution { get; set; }

        public Bitmap CBED_Image;
        private PseudoBitmap pseudoBmp;

        private Matrix3D[] Rotations;

        public FormDiffractionSimulatorCBED()
        {
            InitializeComponent();
            NumericBoxDivision_ValueChanged(new object(), new EventArgs());
            comboBoxSolver.SelectedIndex = 0;
            comboBoxScale.SelectedIndex = 0;
            comboBoxGradient.SelectedIndex = 0;
        }

        private Stopwatch sw = new Stopwatch();

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            if (Crystal.Bethe.IsBusy) return;

            buttonStop.Visible = true;
            sw.Reset();
            sw.Start();
            FormDiffractionSimulator.SkipDrawing = true;
            Crystal.Bethe.CbedCompleted += Bethe_CbedCompleted;
            Crystal.Bethe.CbedProgressChanged += Bethe_CbedProgressChanged;

            //ローテーション配列を作る //半径1の円の中に一辺1/Nの長さの正方形を詰め込み、一つの正方形の中心は、円の中心と一致するような問題を考える
            var rotations = new List<Matrix3D>();
            var side = 2.0 / Division;
            var max = (int)(1 / side) + 1;

            for (int h = -max; h <= max; h++)
                for (int w = max; w >= -max; w--)
                    if (h * h + w * w <= max * max)
                        rotations.Add(Matrix3D.Rot(new Vector3DBase(h, -w, 0), Math.Sqrt(w * side * w * side + h * side * h * side) * AlphaMax));
                    else
                        rotations.Add(null);
            Rotations = rotations.ToArray();

            var solver = comboBoxSolver.SelectedIndex switch
            {
                1 => BetheMethod.Solver.MKL,
                2 => BetheMethod.Solver.Eigen,
                3 => BetheMethod.Solver.Managed,
                _ => BetheMethod.Solver.Auto
            };

            Crystal.Bethe.RunCBED(MaxNumOfBloch, Voltage, Crystal.RotationMatrix, ThicknessArray, Rotations,
                solver, numericBoxThread.ValueInteger);


        }
        private void buttonStop_Click(object sender, EventArgs e)
        {
            Crystal.Bethe.CancelCBED();
            buttonExecute.Text = "Execute";
            pseudoBmp = null;
            CBED_Image = null;
            buttonStop.Visible = false;

        }


        private bool skipProgressChangedEvent = false;

        private void Bethe_CbedProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (skipProgressChangedEvent) return;
            skipProgressChangedEvent = true;
            var report = (BetheMethod.Report)e.UserState;
            var sec = sw.ElapsedMilliseconds / 1000.0;
            var progress = (int)(100.0 * report.Current / DivisionNumber);
            if (progress <= 100)
                toolStripProgressBar.Value = (int)(100.0 * report.Current / DivisionNumber);
            toolStripStatusLabel2.Text = report.Solver;
            toolStripStatusLabel1.Text = "Ellapsed time : " + sec.ToString("f2") + " s.,  time/pixel: ";
            toolStripStatusLabel1.Text += sec / report.Current > 0.9 ? (sec / report.Current).ToString("f2") + " s.,  " : (sec / report.Current * 1000).ToString("f2") + " ms., ";
            toolStripStatusLabel1.Text += (100.0 * report.Current / DivisionNumber).ToString("f1") + " % completed,  wait for "
                + (sec * (DivisionNumber - report.Current) / report.Current).ToString("f2") + " s.";

            Application.DoEvents();
            skipProgressChangedEvent = false;
        }

        private void Bethe_CbedCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonStop.Visible = false;
            FormDiffractionSimulator.SkipDrawing = false;
            Crystal.Bethe.CbedCompleted -= Bethe_CbedCompleted;
            Crystal.Bethe.CbedProgressChanged -= Bethe_CbedProgressChanged;
            var sec = sw.ElapsedMilliseconds / 1000.0;

            if (!e.Cancelled)
            {
                toolStripStatusLabel1.Text = "Ellapsed time : " + sec.ToString("f2") + " s.,  time/pixel: ";
                toolStripStatusLabel1.Text += sec / DivisionNumber > 0.9 ? (sec / DivisionNumber).ToString("f2") + " s.,  " : (sec / DivisionNumber * 1000).ToString("f2") + " ms., ";
                toolStripStatusLabel1.Text += "100 % completed.";
                groupBoxOutput.Enabled = true;
                generateImage();

                FormDiffractionSimulator.FormDiffractionBeamTable.SetTable(FormDiffractionSimulator.waveLengthControl.Energy);
            }
            else
            {
                toolStripStatusLabel1.Text = "Time ellapsed: " + sec.ToString("f2") + " sec.,  Manually interupted.";
                groupBoxOutput.Enabled = false;
            }

            FormDiffractionSimulator.Draw();
            Application.DoEvents();
        }

        public void setImagePixelSize()
        {
            AngleResolution = Math.Acos((Matrix3D.SumOfDiagonalCompenent(Rotations[Rotations.Length / 2] * Rotations[Rotations.Length / 2 + 1].Inverse()) - 1) / 2);
            ImagePixelSize = FormDiffractionSimulator.CameraLength2 * Math.Tan(AngleResolution);
        }

        private void generateImage(bool resetSrc = true, bool resetDest = true)
        {
            if (FormDiffractionSimulator == null || Crystal == null || Crystal.Bethe.Disks == null)
                return;

            setImagePixelSize();

            if (resetSrc)
                SrcImage = getSrcImage();

            if (SrcImage == null)
                return;

            if (resetDest)
                DestImage = getDestImage(SrcImage);

            if (DestImage == null)
                return;

            pseudoBmp = new PseudoBitmap(DestImage, numericBoxImageSize.ValueInteger);
            pseudoBmp.MaxValue = pseudoBmp.SrcValuesGrayOriginal.Max() * trackBarIntensityBrightnessMax.Value / trackBarIntensityBrightnessMax.Maximum;// * 1.5;
            pseudoBmp.MinValue = pseudoBmp.SrcValuesGrayOriginal.Max() * trackBarIntensityBrightnessMin.Value / trackBarIntensityBrightnessMin.Maximum;
            if (comboBoxScale.SelectedIndex == 0)
                pseudoBmp.SetScaleGray();
            else
                pseudoBmp.SetScaleColdWarm();

            pseudoBmp.IsNegative = comboBoxGradient.SelectedIndex == 1;


            CBED_Image = pseudoBmp.GetImage();

            FormDiffractionSimulator.Draw();
            Application.DoEvents();
        }

        private double[] getSrcImage()
        {
            if (Crystal.Bethe.Disks == null || trackBarOutputThickness.Value >= Crystal.Bethe.Disks.Length)
                return null;

            var image = new double[ImageWidth * ImageHeight];
            var disks = Crystal.Bethe.Disks[trackBarOutputThickness.Value];
            var center = new PointD(ImageWidth / 2.0, ImageHeight / 2.0);
            //中心付近の1ピクセル当たりの角度

            foreach (var disk in disks.Where(disk => disk.Intensity.Max() > 1E-20))
            {
                var v = new { x = disk.G.X, y = -disk.G.Y, z = -disk.G.Z };//ここでベクトルのY,Zの符号を反転
                var point = Geometriy.GetCrossPoint(0, 0, 1, FormDiffractionSimulator.CameraLength2, new Vector3D(0, 0, 0), new Vector3D(v.x, v.y, v.z + FormDiffractionSimulator.EwaldRadius));
                var pt = new PointD(point.X, point.Y);
                //ptをピクセル座標に変換する
                var diskCenter = new Point((int)(pt.X / ImagePixelSize + center.X + 0.5), (int)(pt.Y / ImagePixelSize + center.Y + 0.5));

                if (diskCenter.X > -ImageWidth * 0.5 && diskCenter.Y > -ImageHeight * 0.5 && diskCenter.X < ImageWidth * 1.5 && diskCenter.Y < ImageHeight * 1.5)
                {
                    var side = (int)Math.Sqrt(disk.Intensity.Length);
                    int n = 0;
                    for (int y = diskCenter.Y + side / 2; y >= diskCenter.Y - side / 2; y--)
                        for (int x = diskCenter.X + side / 2; x >= diskCenter.X - side / 2; x--)
                        {
                            if (x > -1 && x < ImageWidth && y > -1 && y < ImageHeight && (diskCenter.X - x) * (diskCenter.X - x) + (diskCenter.Y - y) * (diskCenter.Y - y) < (side * side / 4))
                                image[y * ImageWidth + x] += disk.Intensity[n];
                            n++;
                        }
                }
            }
            return image;
        }

        private double[] getDestImage(double[] srcImage)
        {
            var gamma = trackBarGamma.Value * AngleResolution;
            var destImage = new double[srcImage.Length];
            Parallel.For(0, ImageHeight, y =>
            {
                for (int x = 0; x < ImageWidth; x++)
                {
                    var index = y * ImageWidth + x;
                    if (srcImage[index] != 0)
                    {
                        var r2 = (x - ImageCenterX) * (x - ImageCenterX) + (y - ImageCenterY) * (y - ImageCenterY);
                        var r = Math.Sqrt(r2);
                        destImage[index] = srcImage[index] * Math.Exp(r * gamma);
                    }
                }
            });
            return destImage;
        }

        private void FormDiffractionSimulatorMultislice_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (sender == this)
                FormDiffractionSimulator.radioButtonIntensityBethe.Checked = true;
        }

        private bool trackBarAdvancedAlphaMax_ValueChanged(object sender, double value)
        {
            FormDiffractionSimulator.Draw();
            return false;
        }

        private void CheckBoxDrawGuideCircles_CheckedChanged(object sender, EventArgs e) => FormDiffractionSimulator.Draw();

        private void NumericBoxDivision_ValueChanged(object sender, EventArgs e) => labelDivisionNumber.Text = "disk is divided\r\ninto " + DivisionNumber.ToString() + " points";

        private void NumericBoxWholeThicknessStart_ValueChanged(object sender, EventArgs e)
        {
            trackBarOutputThickness.Maximum = ThicknessArray.Length - 1;
            trackBarOutputThickness.Value = 0;
        }

        private void TrackBarOutputThickness_Scroll(object sender, EventArgs e)
        {
            if (Crystal.Bethe.Disks == null || trackBarOutputThickness.Value >= Crystal.Bethe.Disks.Length)
                return;
            if (trackBarOutputThickness.Value < 0)
                return;
            textBoxThickness.Text = ThicknessArray[trackBarOutputThickness.Value].ToString();
            generateImage();
            FormDiffractionSimulator.FormDiffractionBeamTable.SetTable(FormDiffractionSimulator.waveLengthControl.Energy);
        }

        private void numericBoxImageSize_ValueChanged(object sender, EventArgs e)
        {
            trackBarGamma.Maximum = numericBoxImageSize.ValueInteger;
            generateImage();
        }

        private void trackBarSigma_ValueChanged(object sender, EventArgs e) => generateImage(false, true);


        private void trackBarIntensityBrightnessMax_ValueChanged(object sender, EventArgs e) => generateImage(false, true);

        private void numericBoxMaxNumOfG_ValueChanged(object sender, EventArgs e) => FormDiffractionSimulator.Draw();

        private void ComboBoxSolver_SelectedIndexChanged(object sender, EventArgs e) => numericBoxThread.Enabled = comboBoxSolver.SelectedIndex != 0;
    }
}