using System;
using System.Numerics;

namespace Crystallography
{
    public class DynamicTheory
    {
        //public enum Modes { SAED, CBED }

        //public enum Methods { Bethe, Multislice }

        //private static Complex I = System.Numerics.Complex.ImaginaryOne;
        //private static Complex One = System.Numerics.Complex.One;
        //private static Complex TwoPiI = 2 * Math.PI * I;

        public class MultiSlice
        {
            /*
            /// <summary>
            /// Interaction parameter. 2 * PI/rambda/kV*(m0*c^2 + e0*kV*1000) / (2*m0*c^2 + e0*kV*1000). Kirklandの教科書 p79 (5.6)
            /// </summary>
            public double Sigma = 2 * Math.PI * UniversalConstants.e0 * UniversalConstants.h;

            BackgroundWorker worker = new BackgroundWorker();

            public event ProgressChangedEventHandler ProgressChanged;
            public event RunWorkerCompletedEventHandler Completed;

            public double Voltage { get; set; }
            public Crystal Crystal { get; set; }
            public Matrix3D Rotation { get; set; }
            public double Thickness { get; set; }
            public double Resolution { get; set; }
            public int Dimension { get; set; }
            public double Rambda { get; set; }
            public Modes Mode { get; set; }
            public double AlphaMax { get; set; }
            public double Cs { get; set; }
            public double Defocus { get; set; }

            public MultiSlice(Modes mode, double voltage, Crystal crystal, Matrix3D rotation, double thickness, double resolution, int dimension, double alphaMax = 0, double cs = 0, double defocus = 0)
            {
                worker.DoWork += Run_DoWork;
                worker.ProgressChanged += Run_ProgressChanged;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.WorkerReportsProgress = true;
                worker.WorkerSupportsCancellation = true;

                Voltage = voltage;
                Rotation = rotation;
                Thickness = thickness;
                Resolution = resolution;
                Dimension = dimension;
                Crystal = crystal;
                Mode = mode;
                AlphaMax = alphaMax;
                Cs = cs;
                Defocus = defocus;

                Rambda = UniversalConstants.Convert.EnergyToElectronWaveLength(Voltage);
                Sigma = 2 * Math.PI / Rambda / Voltage / 1000 * (9.1093897 * 2.99792458 * 2.99792458 + 1.602176565E-1 * Voltage) / (2 * 9.1093897 * 2.99792458 * 2.99792458 + 1.602176565E-1 * Voltage);
            }

            private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                Completed?.Invoke(sender, e);
            }
            private void Run_ProgressChanged(object sender, ProgressChangedEventArgs e)
            {
                ProgressChanged?.Invoke(sender, e);
            }

            public void Cancel()
            {
                worker.CancelAsync();
            }
            public void Run()
            {
                worker.RunWorkerAsync();
            }

            private void Run_DoWork(object sender, DoWorkEventArgs e)
            {
                Stopwatch swPotential = new Stopwatch(), swTransmission = new Stopwatch(), swFftForward = new Stopwatch(), swPropagator = new Stopwatch(), swFftBackward = new Stopwatch(), swTotal = new Stopwatch(), swInitialize = new Stopwatch();

                Func<Stopwatch, long> t = new Func<Stopwatch, long>(s => s.ElapsedMilliseconds);

                swTotal.Start();
                swInitialize.Start();

                Vector3DBase aAxis = Rotation * Crystal.A_Axis;
                Vector3DBase bAxis = Rotation * Crystal.B_Axis;
                Vector3DBase cAxis = Rotation * Crystal.C_Axis;

                int pixelHalf = Dimension / 2;
                double deltaZ = Math.Max(Math.Max(Math.Abs(aAxis.Z), Math.Abs(bAxis.Z)), Math.Abs(cAxis.Z)) / 1.0;

                //スライスを生成　
                int sliceNumber = (int)(Thickness / deltaZ) + 1;
                Slice[] slice = new Slice[sliceNumber];
                for (int i = 0; i < slice.Length; i++)
                    slice[i] = new Slice(Dimension, Resolution);

                //n番目のスライスのZ範囲は、  (n - sliceNumber/2) * deltaZ   から (n + 1 - sliceNumber/2) * deltaZ

                #region スライス生成

                double minX = -Resolution * Dimension / 2.0, maxX = Resolution * Dimension / 2.0;
                double minY = -Resolution * Dimension / 2.0, maxY = Resolution * Dimension / 2.0;
                double minZ = -sliceNumber * deltaZ / 2.0, maxZ = sliceNumber * deltaZ / 2.0;

                List<Vector3DBase> baseAtomPosition = new List<Vector3DBase>();//n番目の原子の位置
                List<byte> atomicNumber = new List<byte>();//elements[n] n番目の原子の原子番号
                for (int i = 0; i < Crystal.Atoms.Length; i++)
                    foreach (Vector3D v in Crystal.Atoms[i].Atom)
                    {
                        baseAtomPosition.Add(aAxis * v.X + bAxis * v.Y + cAxis * v.Z);// + new Vector3DBase(0.001, 0.0001, 0.0001));
                        atomicNumber.Add((byte)Crystal.Atoms[i].AtomicNumber);
                    }

                //u,v,w(併進ベクトル)の最大値を算出
                Matrix3D reci = new Matrix3D(aAxis, bAxis, cAxis).Inverse();

                var v1 = reci * new Vector3D(minX, minY, minZ);
                var v2 = reci * new Vector3D(maxX, minY, minZ);
                var v3 = reci * new Vector3D(minX, maxY, minZ);
                var v4 = reci * new Vector3D(minX, minY, maxZ);

                int uMax = (int)Math.Max(Math.Max(Math.Abs(v1.X), Math.Abs(v2.X)), Math.Max(Math.Abs(v3.X), Math.Abs(v4.X))) + 2;
                int vMax = (int)Math.Max(Math.Max(Math.Abs(v1.Y), Math.Abs(v2.Y)), Math.Max(Math.Abs(v3.Y), Math.Abs(v4.Y))) + 2;
                int wMax = (int)Math.Max(Math.Max(Math.Abs(v1.Z), Math.Abs(v2.Z)), Math.Max(Math.Abs(v3.Z), Math.Abs(v4.Z))) + 2;

                //範囲内に存在する原子を探索
                for (int u = -uMax; u <= uMax; u++)
                {
                    var translate1 = u * aAxis;
                    for (int v = -vMax; v <= vMax; v++)
                    {
                        var translate2 = translate1 + v * bAxis;
                        for (int w = -wMax; w <= wMax; w++)
                        {
                            var translete3 = translate2 + w * cAxis;
                            for (int i = 0; i < baseAtomPosition.Count; i++)
                            {
                                var vec = translete3 + baseAtomPosition[i];
                                var temp = -vec.X;
                                vec.X = vec.Y;
                                vec.Y = temp;
                                if (vec.X < maxX && vec.X >= minX && vec.Y < maxY && vec.Y >= minY && vec.Z < maxZ && vec.Z >= minZ)
                                {
                                    int n = (int)((vec.Z - minZ) / deltaZ);

                                    if (n > -1 && n < sliceNumber)
                                    {
                                        slice[n].AtomPosition.Add(vec.ToPointD());
                                        slice[n].ElementNumber.Add(atomicNumber[i]);
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion スライス生成

                //fftwクラスを初期化
                //FFTW fftForward = new FFTW(Dimension, Dimension, FFTWSharp.fftw_direction.Forward);
                //FFTW fftBackward = new FFTW(Dimension, Dimension, FFTWSharp.fftw_direction.Backward);

                //伝播関数(逆空間)を作成
                var propagator = GeneratePropagater(Dimension, Dimension, Resolution, Rambda, 100);

                //波動関数を初期化
                var psi0 = new Complex[Dimension * Dimension];
                var rPsi = new Complex[Dimension * Dimension];

                //var test = new double[Dimension * Dimension];
                //var target = psi0;

                //平面波の場合
                    for (int i = 0; i < Dimension * Dimension; i++)
                    {
                        int x = i % Dimension, y = i / Dimension;

                        psi0[(x + y * Dimension) * 2] = 1;
                        psi0[(x + y * Dimension) * 2 + 1] = 0;
                    }

                //var psi0Temp2 = new double[Dimension * Dimension * 2];
                //Parallel.For(0, Dimension * Dimension, j =>
               // {
                //    psi0Temp2[j * 2] = propagator[j * 2] * psi0Temp1[j * 2] - propagator[j * 2 + 1] * psi0Temp1[j * 2 + 1];
                //    psi0Temp2[j * 2 + 1] = propagator[j * 2 + 1] * psi0Temp1[j * 2] + propagator[j * 2] * psi0Temp1[j * 2 + 1];
                //});
                //for (int j = 0; j < Dimension * Dimension * 2; j++)
               // {
               //     psi0Temp1[j] = psi0Temp2[j];
               // }
               //

                propagator = GeneratePropagater(Dimension, Dimension, Resolution, Rambda, deltaZ);

                //var rPsi2 = new double[Dimension * Dimension * 2];

                //透過関数

                //スライスから出射する波動関数
                var psiExit = new double[Dimension * Dimension * 2];

                double coeff = 1.0 / Dimension;

                swInitialize.Stop(); //初期化ここまで

                for (int i = 0; i < sliceNumber; i++)
                {
                    if (worker.CancellationPending) return;

                    //現在のスライスの投影ポテンシャルを取得
                    swPotential.Start();
                    var p = slice[i].GetProjectedPotential();
                    swPotential.Stop();

                    SaveFile(p, Dimension, i);

                    //透過関数を計算・作用させる
                    swTransmission.Start();
                    Parallel.For(0, Dimension * Dimension, j =>
                    {
                        double tR = Math.Cos(Sigma * p[j]) * coeff, tI = Math.Sin(Sigma * p[j]) * coeff;
                        fftForward.Value[j * 2] = psi0[j * 2] * tR - psi0[j * 2 + 1] * tI;//psiExit[j * 2] = psi0[j * 2] * tR - psi0[j * 2 + 1] * tI;
                        fftForward.Value[j * 2 + 1] = psi0[j * 2 + 1] * tR + psi0[j * 2] * tI;//psiExit[j * 2 + 1] = psi0[j * 2 + 1] * tR + psi0[j * 2] * tI;
                    });
                    swTransmission.Stop();

                    //最後のスライスであれば、これで終了. 続きがあれば、以下
                    //if (i == sliceNumber - 1)
                    //    break;

                    swFftForward.Start();
                    //psi1 をフーリエ変換して、rPsiを生成
                    rPsi = fftForward.Run();//rPsi = fftForward.Run(psiExit);
                    swFftForward.Stop();

                    //if (i % 100 == 0)
                    //    SaveFile(rPsi, Dimension, i);

                    //rPsiと伝搬関数を作用させる
                    swPropagator.Start();
                    Parallel.For(0, Dimension * Dimension, j =>
                    {
                        double p1 = propagator[j * 2] * coeff, p2 = propagator[j * 2 + 1] * coeff;
                        fftBackward.Value[j * 2] = p1 * rPsi[j * 2] - p2 * rPsi[j * 2 + 1];//rPsi2[j * 2] = propagator[j * 2] * rPsi[j * 2] - propagator[j * 2 + 1] * rPsi[j * 2 + 1];
                        fftBackward.Value[j * 2 + 1] = p2 * rPsi[j * 2] + p1 * rPsi[j * 2 + 1];//rPsi2[j * 2 + 1] = propagator[j * 2 + 1] * rPsi[j * 2] + propagator[j * 2] * rPsi[j * 2 + 1];
                    });
                    swPropagator.Stop();

                    //rPsi2を逆フーリエして、psi0に戻す
                    swFftBackward.Start();
                    psi0 = fftBackward.Run();//psi0 = fftBackward.Run(rPsi2);
                    swFftBackward.Stop();

                    if ((i + 1) % 10 == 0 || i == sliceNumber - 1)//10回ごとにReport
                    {
                        worker.ReportProgress(0, new State(rPsi, sliceNumber, i + 1, swTotal, swInitialize, swPotential, swTransmission, swFftForward, swPropagator, swFftBackward));
                    }
                }
                //    worker.ReportProgress(0, new State(rPsi, sliceNumber, sliceNumber, swTotal, swInitialize, swPotential, swTransmission, swFftForward, swPropagator, swFftBackward));
            }

            public class State
            {
                public int Progress;
                public long TimeInitialize, TimeTotal, TimePotential, TimeTransmission, TimeFftForward, TimePropagator, TimeFftBackward;
                public int CurrentSlice, TotalSlice;

                //複素数
                public double[] InversePattern;

                public State(double[] inversePattern, int totalSlice, int currentSlice, Stopwatch total, Stopwatch initialize, Stopwatch potential, Stopwatch transmission, Stopwatch fftForward, Stopwatch propagator, Stopwatch fftBackward)
                {
                    InversePattern = inversePattern;
                    CurrentSlice = currentSlice;
                    TotalSlice = totalSlice;
                    TimeTotal = total.ElapsedMilliseconds;
                    TimeInitialize = initialize.ElapsedMilliseconds;
                    TimePotential = potential.ElapsedMilliseconds;
                    TimeFftForward = fftForward.ElapsedMilliseconds;
                    TimePropagator = propagator.ElapsedMilliseconds;
                    TimeFftBackward = fftBackward.ElapsedMilliseconds;
                    TimeTransmission = transmission.ElapsedMilliseconds;
                }
            }

            public void SaveFile(double[] p, int pixels, int sliceNum)
            {
                var bw = new BinaryWriter(new FileStream("test" + sliceNum.ToString() + ".bin", FileMode.Create));
                if (p.Length == pixels * pixels * 2)
                {
                    var target = Swap2Dimage(p, pixels, pixels);
                    for (int j = 0; j < pixels * pixels; j++)
                        bw.Write(target[j * 2] * target[j * 2] + target[j * 2 + 1] * target[j * 2 + 1]);
                }
                else if (p.Length == pixels * pixels)
                {
                    for (int j = 0; j < pixels * pixels; j++)
                        bw.Write(p[j]);
                }
                bw.Close();
            }

            public double[] Swap2Dimage(double[] pixels, int width, int height)
            {
                var target = new double[pixels.Length];
                for (int h = 0; h < height / 2; h++)
                {
                    Array.Copy(pixels, h * width * 2, target, (h * 2 + height) * width + width, width);
                    Array.Copy(pixels, h * width * 2 + width, target, (h * 2 + height) * width, width);

                    Array.Copy(pixels, (h * 2 + height) * width, target, h * width * 2 + width, width);
                    Array.Copy(pixels, (h * 2 + height) * width + width, target, h * width * 2, width);
                }
                return target;
            }

            public double[] GeneratePropagater(int width, int height, double resolution, double rambda, double deltaZ)
            {
                var propagator = new double[width * height * 2];

                for (int i = 0; i < width * height; i++)
                {   //中心座標を, (pixel/2、pixel2)として計算した後に、象限を入れ替える
                    int x = i % width, y = i / width;
                    double phase = -Math.PI * rambda * deltaZ * ((x - width / 2.0) * (x - width / 2.0) / width / width + (y - height / 2.0) * (y - height / 2.0) / height / height) / resolution / resolution;
                    propagator[(x + y * width) * 2] = Math.Cos(phase);
                    propagator[(x + y * width) * 2 + 1] = Math.Sin(phase);
                }
                propagator = Swap2Dimage(propagator, width, height);
                return propagator;
            }

            /// <summary>
            /// 正方形のスライスを想定.
            /// </summary>
            public class Slice
            {
                public List<PointD> AtomPosition;
                public List<byte> ElementNumber;
                public double Resolution;//ピクセルサイズに相当する量. nm単位
                public int Pixel;
                public Slice(int pixel, double resolution)
                {
                    Pixel = pixel;
                    Resolution = resolution;
                    AtomPosition = new List<PointD>();
                    ElementNumber = new List<byte>();
                }

                public unsafe double[] GetProjectedPotential()
                {
                    double[] potential = new double[Pixel * Pixel];
                    for (int i = 0; i < potential.Length; i++)
                        potential[i] = 0;

                    //i番目のピクセルは、x = i%pixel, y=i/pixelに位置する
                    //x,yのピクセルの中心位置は、実空間では  X = resolution * ( (1- pixel)/2 + x), Y= resolution * ( (1- pixel)/2 + y)に相当する
                    //実空間で X, Yの位置は、x= X/resolution -(1- pixel)/2, y= Y/resolution -(1- pixel)/2

                    Func<PointD, PointD> toPixel = new Func<PointD, PointD>((p) => new PointD(p.X / Resolution - (1 - Pixel) / 2.0, p.Y / Resolution - (1 - Pixel) / 2.0));
                    Func<PointD, PointD> toReal = new Func<PointD, PointD>((p) => new PointD(Resolution * ((1 - Pixel) / 2.0 + p.X), Resolution * ((1 - Pixel) / 2.0 + p.Y)));

                    double step = 0.00001;// 0.0001A単位
                    double range1 = 0.2;

                    //range1の範囲を、stepで分割して予め計算
                    //まず、原子の種類を数える
                    List<int> e = new List<int>();
                    for (int i = 0; i < ElementNumber.Count; i++)
                        if (!e.Contains(ElementNumber[i]))
                            e.Add(ElementNumber[i]);

                    double[][] potentialTemp = new double[e.Count][];
                    int[] toIndex = new int[120];
                    for (int i = 0; i < e.Count; i++)
                    {
                        toIndex[e[i]] = i;
                        double[] temp = new double[(int)(range1 / step + 0.01)];
                        for (int j = 0; j < temp.Length; j++)
                            temp[j] = AtomConstants.ElectronScatteringKirkrand[ElementNumber[i]].ProjectedPotential(j * step * 10);
                        temp[0] = temp[1];
                        potentialTemp[i] = temp;
                    }

                    double[] posX = new double[AtomPosition.Count], posY = new double[AtomPosition.Count];
                    for (int i = 0; i < AtomPosition.Count; i++)
                    {
                        posX[i] = AtomPosition[i].X;
                        posY[i] = AtomPosition[i].Y;
                    }

                    double threshold = range1 * range1 / Resolution / Resolution - 1;
                    //for (int i = 0; i < AtomPosition.Count; i++)
                    //Parallel.For(0, AtomPosition.Count, i =>
                    for (int i = 0; i < AtomPosition.Count; i++)
                    {
                        //ピクセル位置を計算
                        double centerX = posX[i] / Resolution - (-Pixel) / 2.0;
                        double centerY = posY[i] / Resolution - (-Pixel) / 2.0;
                        //このピクセルの周りに、range1 の範囲でポテンシャルを計算
                        int minX = (int)(centerX - range1 / Resolution + 0.5), maxX = (int)(centerX + range1 / Resolution + 0.5);
                        int minY = (int)(centerY - range1 / Resolution + 0.5), maxY = (int)(centerY + range1 / Resolution + 0.5);
                        double[] p = potentialTemp[toIndex[ElementNumber[i]]];

                        for (int y = minY; y < maxY; y++)
                        {
                            double ySquared = (y - centerY) * (y - centerY);
                            for (int x = minX; x < maxX; x++)
                            {
                                double r2 = (x - centerX) * (x - centerX) + ySquared;
                                if (r2 < threshold)
                                {
                                    int x1 = x;
                                    if (x1 < 0) x1 += Pixel;
                                    else if (x1 >= Pixel) x1 -= Pixel;
                                    int y1 = y;
                                    if (y1 < 0) y1 += Pixel;
                                    else if (y1 >= Pixel) y1 -= Pixel;
                                    potential[x1 + y1 * Pixel] -= p[(int)(Math.Sqrt(r2) * Resolution / step)];
                                }
                            }
                        }
                    }
                    //);

                    return potential;
                }
            }*/
        }
    }
}