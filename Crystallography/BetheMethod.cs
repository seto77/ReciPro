#region using
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using DMat = MathNet.Numerics.LinearAlgebra.Complex.DenseMatrix;
using DVec = MathNet.Numerics.LinearAlgebra.Complex.DenseVector;
using static System.Numerics.Complex;
#endregion

namespace Crystallography
{
    /// <summary>
    /// Bethe法による動力学計算を提供するクラス。すべて、単位はnm
    /// </summary>
    [Serializable]
    public class BetheMethod
    {
        #region static readonly field

        private static readonly Complex One = Complex.One;
        private static readonly double TwoPi = 2 * Math.PI;
        private static readonly Complex TwoPiI = TwoPi * ImaginaryOne;
        private static readonly Complex PiI = Math.PI * ImaginaryOne;
        private static readonly double PiSq = Math.PI * Math.PI;
        private static readonly Vector3DBase zNorm = new Vector3DBase(0, 0, 1);

        #endregion

        #region フィールド、プロパティ

        private double AccVoltage { get; set; }
        private Crystal Crystal { get; set; } = null;
        private Matrix3D BaseRotation { get; set; } = null;
        public double AlphaMax { get; set; }
        public double Cs { get; set; }
        public double Defocus { get; set; }
        public Matrix3D[] BeamRotations { get; set; }

        public int RotationArrayValidLength { get; set; } = 0;

        public readonly bool EigenEnabled = true;

        /// <summary>
        /// サンプル表面(から内部への)の法線単位ベクトル. ReciProの座標系は、画面右が+X、上が+Y,手前が+Zなので、初期値は(0,0,-1)
        /// </summary>
        public Vector3D Surface { get; set; } = new Vector3D(0, 0, -1);

        public int MaxNumOfBloch { get; set; }
        public double Thickness { get; set; }
        public double[] Thicknesses { get; set; }
        public enum Solver { Eigen_Managed, Eigen_MKL, Eigen_Eigen, MtxExp_Eigen, Auto }

        public DVec EigenValues { get; set; }
        public DMat EigenVectors { get; set; }
        public Matrix<Complex> EigenVectorsInverse { get; set; }

        public DVec[] EigenValuesPED { get; set; }
        public DMat[] EigenVectorsPED { get; set; }
        public DMat[] EigenVectorsInversePED { get; set; }

        [NonSerialized]
        public Beam[][] BeamsPED;
        public double SemianglePED { get; set; }

        public bool IsBusy => bwCBED == null || bwCBED.IsBusy;

        /// <summary>
        /// Disks[Z_index][G_index]
        /// </summary>
        public CBED_Disk[][] Disks { get; set; }

        [NonSerialized]
        public Beam[] Beams;

        [NonSerialized]
        private readonly BackgroundWorker bwCBED = new BackgroundWorker();

        public event ProgressChangedEventHandler CbedProgressChanged;

        public event RunWorkerCompletedEventHandler CbedCompleted;

        private readonly object lockObj = new object();

        #endregion

        #region コンストラクタ
        public BetheMethod(Crystal crystal)
        {
            Crystal = crystal;

            bwCBED = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            bwCBED.RunWorkerCompleted += Cbed_RunWorkerCompleted;
            bwCBED.ProgressChanged += Cbed_ProgressChanged;
            bwCBED.DoWork += cbed_DoWork;

            EigenEnabled = NativeWrapper.Enabled;
        }
        #endregion

        #region CBED
        private void Cbed_ProgressChanged(object sender, ProgressChangedEventArgs e) => CbedProgressChanged?.Invoke(sender, e);

        private void Cbed_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => CbedCompleted?.Invoke(sender, e);

        public void CancelCBED()
        {
            if (bwCBED.IsBusy)
                bwCBED.CancelAsync();
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="maxNumOfBloch"></param>
        /// <param name="voltage">加速電圧(kV)</param>
        /// <param name="rotation">基準となる方位</param>
        /// <param name="thickness">厚みの配列</param>
        /// <param name="beamRotations">基準となる方位に乗算する方位配列</param>
        public void RunCBED(int maxNumOfBloch, double voltage, Matrix3D rotation, double[] thickness, Matrix3D[] beamRotations, Solver solver = Solver.Auto, int thread = 1)
        {
            MaxNumOfBloch = maxNumOfBloch;
            AccVoltage = voltage;
            //Wavelength = UniversalConstants.Convert.EnergyToElectronWaveLength(voltage);
            BaseRotation = new Matrix3D(rotation);
            BeamRotations = beamRotations;
            Thicknesses = thickness;
            MathNet.Numerics.Control.TryUseNativeMKL();
            bwCBED.RunWorkerAsync(new object[] { solver, thread });
        }

        private void cbed_DoWork(object sender, DoWorkEventArgs e)
        {
            //波数を計算
            var kvac = UniversalConstants.Convert.EnergyToElectronWaveNumber(AccVoltage);
            //U0を計算
            var u0 = getU(AccVoltage, (0, 0, 0), 0).Real.Real;
            //k0ベクトルを計算
            var vecK0 = getVecK0(kvac, u0);
            //計算対象のg-Vectorsを決める。indexが小さく、かつsg(励起誤差)の小さいg-vectorを抽出する
            Beams = Find_gVectors(BaseRotation, vecK0);

            //入射面での波動関数を定義
            var psi0 = DVec.OfArray(Enumerable.Range(0, Beams.Length).ToList().Select(g => g == 0 ? One : 0).ToArray());
            //ポテンシャルマトリックスを取得
            uDictionary = new Dictionary<int, (Complex, Complex)>();
            var factorMatrix = getPotentialMatrix(Beams);
            //有効なRotationだけを選択
            var beamRotationsValid = BeamRotations.Where(rot => rot != null).ToList();

            RotationArrayValidLength = beamRotationsValid.Count;

            //進捗状況報告用の各種定数を初期化
            int count = 0;

            #region solver, thread の設定
            var solver = (Solver)((object[])e.Argument)[0];
            var thread = (int)((object[])e.Argument)[1];

            if(solver== Solver.Auto)
            {
                if (NativeWrapper.Enabled)
                {
                    solver = Solver.MtxExp_Eigen;
                    thread = Math.Max(1, (int)(Environment.ProcessorCount * 0.75));
                }
                else if(MathNet.Numerics.Control.TryUseNativeMKL())
                {
                    solver = Solver.Eigen_MKL;
                    thread = Math.Max(1, (int)(Environment.ProcessorCount * 0.25));
                }
                else 
                {
                    solver = Solver.Eigen_Managed;
                    thread = Math.Max(1, (int)(Environment.ProcessorCount * 0.75));
                }
            }
            else if (solver == Solver.Eigen_MKL)
                solver = MathNet.Numerics.Control.TryUseNativeMKL() ? Solver.Eigen_MKL : Solver.Eigen_Managed;
            else if (solver == Solver.Eigen_Managed)
                MathNet.Numerics.Control.UseManaged();
            #endregion

            var reportString = solver.ToString() + thread.ToString();

            var beamRotationsP = beamRotationsValid.AsParallel().WithDegreeOfParallelism(thread);

            //ここからdiskValid[t][g]を計算.
            
            var diskValid = beamRotationsP.Select(beamRotation =>
            {
                if (bwCBED.CancellationPending) return null;
                var rotZ = beamRotation * zNorm;
                var coeff = 1.0 / rotZ.Z; // = 1/cosTau

                var vecK0 = getVecK0(kvac, u0, beamRotation);

                var beams = reset_gVectors(Beams, BaseRotation, vecK0);//BeamsのPやQをリセット
                var potentialMatrix = getEigenProblemMatrix(beams, factorMatrix);//ポテンシャル行列をセット
                Complex[][] result;

                //ポテンシャル行列の固有値、固有ベクトルを取得し、resultに格納

                //Eigen＿Eigenの場合
                if (solver == Solver.Eigen_Eigen)
                {
                    result = NativeWrapper.CBEDSolver_Eigen(potentialMatrix, psi0.ToArray(), Thicknesses, coeff);
                }
                //Eigen_MKL あるいは Eigen_Managedの場合    
                else if (solver == Solver.Eigen_Managed || solver == Solver.Eigen_MKL)
                {
                    var evd = DMat.OfArray(potentialMatrix).Evd(Symmetricity.Asymmetric);
                    var alpha = evd.EigenVectors.Inverse() * psi0;
                    result = Thicknesses.Select(t =>
                    {
                        //ガンマの対称行列×アルファを作成
                        var gammmaAlpha = DVec.OfArray(evd.EigenValues.Select((ev, i) => Exp(TwoPiI * ev * t * coeff) * alpha[i]).ToArray());
                        //深さtにおけるψを求める
                        return (evd.EigenVectors * gammmaAlpha).ToArray();
                    }).ToArray();
                }
                else //MtxExp_Eigenの場合
                {
                    result = NativeWrapper.CBEDSolver_MatExp(potentialMatrix, psi0.ToArray(), Thicknesses, coeff);

                    //var matExp = NativeWrapper.MatrixExponential_Cuda(TwoPiI * coeff * Thicknesses[0] * DMat.OfArray(potentialMatrix));
                    //var vec = psi0;
                    //result = new Complex[Thicknesses.Length][];
                    //for (int i = 0; i < Thicknesses.Length; i++)
                    //{
                    //    vec = matExp * vec;
                    //    result[i] = vec.ToArray();
                    //}
                }
                bwCBED.ReportProgress(Interlocked.Increment(ref count), reportString);//進捗状況を報告
                return result;
            }).ToArray();


            //無効なRotationも考慮してdisk[RotationIndex][Z_index][G_index]を構築
            var disk = new List<Complex[][]>();
            for (int i = 0, j = 0; i < BeamRotations.Length; i++)
                disk.Add(BeamRotations[i] != null ? diskValid[j++] : null);

            //diskをコンパイルする
            Disks = new CBED_Disk[Thicknesses.Length][];
            Parallel.For(0, Thicknesses.Length, t =>
            {
                Disks[t] = new CBED_Disk[Beams.Length];
                for (int g = 0; g < Beams.Length; g++)
                {
                    var intensity = new double[BeamRotations.Length];
                    for (int r = 0; r < BeamRotations.Length; r++)
                        if (disk[r] != null)
                            intensity[r] = disk[r][t][g].Magnitude2();

                    Disks[t][g] = new CBED_Disk(new[] { Beams[g].H, Beams[g].K, Beams[g].L }, Beams[g].Vec, Thicknesses[t], intensity);
                }
            });

            if (bwCBED.CancellationPending)
                e.Cancel = true;
        }

        #endregion

        #region 平行ビーム電子回折

        /// <summary>
        /// 平行ビームの電子回折計算
        /// </summary>
        /// <param name="maxNumOfBloch"></param>
        /// <param name="voltage"></param>
        /// <param name="rotation"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        public Beam[] GetDifractedBeamAmpriltudes(int maxNumOfBloch, double voltage, Matrix3D rotation, double thickness)
        {

            var useEigen = !MathNet.Numerics.Control.TryUseNativeMKL();

            if (AccVoltage != voltage)
                uDictionary = new Dictionary<int, (Complex, Complex)>();

            //波数を計算
            var k_vac = UniversalConstants.Convert.EnergyToElectronWaveNumber(voltage);
            //U0を計算
            var u0 = getU(voltage, (0, 0, 0), 0).Real.Real;
            var vecK0 = getVecK0(k_vac, u0);


            if (MaxNumOfBloch != maxNumOfBloch || AccVoltage != voltage || EigenValues == null || EigenVectors == null || !rotation.Equals(BaseRotation))
            {
                MaxNumOfBloch = maxNumOfBloch;
                AccVoltage = voltage;
                BaseRotation = new Matrix3D(rotation);
                Thickness = thickness;

                //計算対象のg-Vectorsを決める。
                Beams = Find_gVectors(BaseRotation, vecK0);

                if (Beams == null || Beams.Length == 0) return new Beam[0];

                var potentialMatrix = getEigenProblemMatrix(Beams);

                //A行列に関する固有値、固有ベクトルを取得 
                if (useEigen) { 
                    (EigenValues, EigenVectors) = NativeWrapper.EigenSolver(potentialMatrix);
                    EigenVectorsInverse = NativeWrapper.Inverse(EigenVectors);
                }
                else
                {
                    var evd = DMat.OfArray(potentialMatrix).Evd(Symmetricity.Asymmetric);
                    EigenValues = evd.EigenValues.AsArray();
                    EigenVectors = (DMat)evd.EigenVectors;
                    EigenVectorsInverse = EigenVectors.Inverse();
                }
            }

            int len = Beams.Length;

            var psi0 = DVec.OfArray(new Complex[len]);//入射面での波動関数を定義
            psi0[0] = 1;

            var alpha = EigenVectorsInverse * psi0;//アルファベクトルを求める

            //ガンマの対称行列×アルファを作成
            var gamma_alpha = new DVec(Enumerable.Range(0, len).Select(n => Exp(TwoPiI * EigenValues[n] * thickness) * alpha[n]).ToArray());

            //出射面での境界条件を考慮した位相にするため、以下の1行を追加 (20190827)
            var p = new DiagonalMatrix(len, len, Beams.Select(b => Exp(PiI * (b.P - 2 * k_vac * Surface.Z) * thickness)).ToArray());

            //深さZにおけるψを求める
            var psi_atZ = p * EigenVectors * gamma_alpha;
          

            


            for (int i = 0; i < Beams.Length && i < len; i++)
                Beams[i].Psi = psi_atZ[i];

            return Beams;
        }

        #endregion

        #region Precession electron diffraction

        /// <summary>
        /// PEDの計算
        /// </summary>
        /// <param name="maxNumOfBloch"></param>
        /// <param name="voltage"></param>
        /// <param name="baseRotation"></param>
        /// <param name="thickness"></param>
        /// <param name="semiangle"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public Beam[] GetPrecessionElectronDiffraction(int maxNumOfBloch, double voltage, Matrix3D baseRotation, double thickness, double semiangle, int step)
        {

            //波数を計算
            var kvac = UniversalConstants.Convert.EnergyToElectronWaveNumber(voltage);
            //U0を計算
            var u0 = getU(voltage, (0, 0, 0), 0).Real.Real;

            if (AccVoltage != voltage)
                uDictionary = new Dictionary<int, (Complex, Complex)>();

            var useEigen = EigenEnabled && maxNumOfBloch < 400;
            if (!MathNet.Numerics.Control.TryUseNativeMKL())
                useEigen = true;

            var stepP = Enumerable.Range(0, step).ToList().AsParallel().WithDegreeOfParallelism(useEigen ? Environment.ProcessorCount : Math.Max(1, Environment.ProcessorCount / 4));
            if (MaxNumOfBloch != maxNumOfBloch || AccVoltage != voltage || EigenValuesPED == null || EigenValuesPED.Length != step
                || EigenVectorsPED == null || EigenVectorsPED.Length != step || semiangle != SemianglePED || !baseRotation.Equals(BaseRotation))
            {
                MaxNumOfBloch = maxNumOfBloch;
                AccVoltage = voltage;
                BaseRotation = new Matrix3D(baseRotation);
                Thickness = thickness;
                SemianglePED = semiangle;

                EigenValuesPED = new DVec[step];
                EigenVectorsPED = new DMat[step];
                EigenVectorsInversePED = new DMat[step];
                BeamsPED = new Beam[step][];

                stepP.ForAll(k =>
                   {
                       var rotAngle = 2.0 * Math.PI * k / step;
                       var beamRotation = Matrix3D.Rot(new Vector3DBase(Math.Cos(rotAngle), Math.Sin(rotAngle), 0), SemianglePED);
                       //計算対象のg-Vectorsを決める。
                       var potentialMatrix = new Complex[0, 0];
                       var vecK0 = getVecK0(kvac, u0, beamRotation);
                       lock (lockObj)
                       {
                           BeamsPED[k] = Find_gVectors(BaseRotation, vecK0);
                           potentialMatrix = getEigenProblemMatrix(BeamsPED[k]);
                       }

                       //A行列に関する固有値、固有ベクトルを取得 
                       if (useEigen)
                       {//Eigenを使う場合
                           (EigenValuesPED[k], EigenVectorsPED[k]) = NativeWrapper.EigenSolver(potentialMatrix);
                           EigenVectorsInversePED[k] = NativeWrapper.Inverse(EigenVectorsPED[k]);
                       }
                       else
                       {//MKLを使う場合
                           var evd = DMat.OfArray(potentialMatrix).Evd(Symmetricity.Asymmetric);
                           EigenValuesPED[k] = evd.EigenValues.AsArray();
                           EigenVectorsPED[k] = (DMat)evd.EigenVectors;
                           EigenVectorsInversePED[k] = (DMat)EigenVectorsPED[k].Inverse();
                       }
                   });
            }

            //各方向でのbeamの振幅を求める
            stepP.ForAll(k =>
            {
                if (EigenValuesPED[k] != null)
                {
                    var len = EigenValuesPED[k].Count();
                    var psi0 = DVec.OfArray(new Complex[len]);//入射面での波動関数を定義
                    psi0[0] = 1;
                    var alpha = EigenVectorsInversePED[k] * psi0;//アルファベクトルを求める
                    //ガンマの対称行列×アルファを作成
                    var gamma_alpha = new DVec(Enumerable.Range(0, len).Select(n => Exp(TwoPiI * EigenValuesPED[k][n] * thickness) * alpha[n]).ToArray());
                    //深さZにおけるψを求める
                    var psi_atZ = EigenVectorsPED[k] * gamma_alpha;
                    for (int i = 0; i < BeamsPED[k].Length && i < len; i++)
                        BeamsPED[k][i].Psi = psi_atZ[i];
                }
            });

            //最後に全てのビームをまとめる
            var compiled = new Dictionary<(int h, int k, int l), Beam>();
            foreach (var beamsEach in BeamsPED.Where(beams => beams != null))
                foreach (var beam in beamsEach)
                {
                    if (!compiled.ContainsKey(beam.Index))
                    {
                        compiled.Add(beam.Index, beam);
                        compiled[beam.Index].intensity = beam.Psi.Magnitude2() / step;
                    }
                    else
                        compiled[beam.Index].intensity += beam.Psi.Magnitude2() / step;
                }

            //基準の方位でP,Q,Sなどを再セット
            var mat = BaseRotation * Crystal.MatrixInverse.Transpose();
            var beams = compiled.Values.ToList();
            for (int i = 0; i < beams.Count; i++)
            {
                var g = mat * beams[i].Index;
                var (Q, P) = getQP(g, kvac, u0);
                var psi = new Complex(Math.Sqrt(beams[i].intensity), 0);
                beams[i] = new Beam(beams[i].Index, g, (beams[i].Freal, beams[i].Fimag), (Q, P)) { Psi = psi };
            }

            //並び替え
            beams.Sort((a, b) =>
            {
                var c = a.Rating - b.Rating;
                return (c > 0) ? 1 : (c < 0) ? -1 : 0;
            });
            Beams = beams.ToArray();
            return Beams;
        }

        #endregion

        #region Image Simulation
        public double[][] GetPotentialImage(IEnumerable<Beam> beams, Size size, double res, bool phase = true)
        {
            int width = size.Width, height = size.Height;
            //gList[gNUm]を全て計算
            var gList = beams.Select(b => (b.Freal, b.Fimag, b.Vec.ToPointD())).ToList();
            //imagesを初期化
            var images = Enumerable.Range(0, 4).ToList().Select(d => new double[width * height]).ToArray();
            //各ピクセルの計算
            double cX = width / 2.0, cY = height / 2.0;
            Parallel.For(0, width * height, n =>
            {
                var r = new PointD(n % width - cX, height - n / width - 1 - cY) * res;
                var sums = new Complex[2];
                foreach (var (uCry, uTher, vec) in gList)
                {
                    var exp = Exp(vec * r * TwoPiI);
                    sums[0] += uCry * exp;
                    sums[1] += uTher * exp;
                }
                for (var i = 0; i < sums.Length; i++)
                {
                    images[i * 2][n] = phase ? sums[i].Magnitude : sums[i].Real;
                    images[i * 2 + 1][n] = phase ? sums[i].Phase : sums[i].Imaginary;
                }
            });
            return images;
        }



        /// <summary>
        /// HRTEMイメージをシミュレーションする
        /// </summary>
        /// <param name="beams"></param>
        /// <param name="size"></param>
        /// <param name="res"></param>
        /// <param name="cs">球面収差</param>
        /// <param name="aper"></param>
        /// <param name="beta">単位は rad</param>
        /// <param name="delta">Cc * ΔV/V 単位は nm</param>
        /// <param name="defocus">配列で与える. 単位はnm</param>
        /// <param name="quasiMode">trueの時はQuasi-coherent mode, falseの時はTransmission cross coefficient </param>
        /// <returns>double[defocusNum][pixels]</returns>
        public double[][] GetHRTEMImage(Beam[] beams, Size size, double res, double cs, double beta, double delta, double[] defocusses,
                                       bool quasiMode = true, bool native = true)
        {
            int width = size.Width, height = size.Height;
            double rambda = UniversalConstants.Convert.EnergyToElectronWaveLength(AccVoltage), rambdaSq = rambda * rambda;
            double deltaSq = delta * delta, betaSq = beta * beta;
            var k = new PointD(0, 0);//入射ベクトルKのXY成分
            var defLen = defocusses.Length;

            //gList[gNUm]を全て計算
            var gList = new List<(Complex Psi, PointD Vec, Complex[] Lenz)>();
            if (quasiMode)//Quasi-coherent modeの時
                foreach (var g in beams)
                {
                    var qSq = (k + g.Vec.ToPointD()).Length2;
                    gList.Add((g.Psi, k + g.Vec.ToPointD(), defocusses.Select(defocus =>
                        Exp(-PiI * rambda * qSq * (cs * rambdaSq * qSq / 2.0 + defocus))//球面収差
                        * Math.Exp(-PiSq * betaSq * qSq * (defocus + rambdaSq * cs * qSq) * (defocus + rambdaSq * cs * qSq))//時間的インコヒーレンス
                        * Math.Exp(-PiSq * rambdaSq * deltaSq * qSq * qSq / 2)//空間的インコヒーレンス
                         ).ToArray()));
                }

            else//Transmission cross coefficient modelの時
            {
                var vecDic = new Dictionary<(int H, int K, int L), PointD>();
                for (var gNum = 0; gNum < beams.Length; gNum++)
                    for (var hNum = gNum; hNum < beams.Length; hNum++)
                    {
                        Beam g = beams[gNum], h = beams[hNum];
                        PointD q1 = k + g.Vec.ToPointD(), q2 = k + h.Vec.ToPointD();
                        double q1Sq = q1.Length2, q2Sq = q2.Length2;
                        //gNum==hNumの時は、g.Psi.Magnitude2() が画素に伝わるだけなので、最後に強度を0~2^16に規格化する場合は、あってもなくても関係ない
                        var psi = gNum == hNum ? g.Psi.Magnitude2() : 2 * g.Psi * Conjugate(h.Psi);

                        //indexが同じものがあるかどうかを検索し、無い場合のみvecを計算する
                        var index = (g.H - h.H, g.K - h.K, g.L - h.L);
                        if (!vecDic.TryGetValue(index, out var vec))
                        {
                            vec = (g.Vec - h.Vec).ToPointD();
                            vecDic.Add(index, vec);
                        }

                        var lenz = defocusses.Select(defocus =>
                            Exp(-PiI * rambda * q1Sq * (cs * rambdaSq * q1Sq / 2.0 + defocus)) *  //Kai1
                            Exp(PiI * rambda * q2Sq * (cs * rambdaSq * q2Sq / 2.0 + defocus)) *  //kai2
                            Math.Exp(-PiSq * betaSq * (defocus * (q1 - q2) + rambdaSq * cs * (q1Sq * q1 - q2Sq * q2)).Length2) *  //eb
                            Math.Exp(-PiSq * rambdaSq * deltaSq * (q1Sq - q2Sq) * (q1Sq - q2Sq) / 2.0) //ed
                            ).ToArray();

                        gList.Add((psi, vec, lenz));
                    }
            }
            //gListを並び替え
            gList.Sort((g1, g2) =>
            {
                double x = g1.Vec.X - g2.Vec.X, y = g1.Vec.Y - g2.Vec.Y;
                return (y > 0 || (y == 0 && x > 0)) ? 1 : (y < 0 || (y == 0 && x < 0)) ? -1 : 0;
            });

            //imagesを初期化
            var images = defocusses.Select(d => new double[width * height]).ToArray();

            //各ピクセルの計算
            double cX = width / 2.0, cY = height / 2.0;
            if (native && NativeWrapper.Enabled)//ネイティブC++で実行 3倍速い
            {
                var (gPsi, gVec, gLenz) = NativeWrapper.HRTEM_Helper(gList);
                int divTotal = Environment.ProcessorCount * 4, step = width * height / divTotal;
                Parallel.For(0, divTotal, div =>
                {
                    int start = step * div, count = div == divTotal - 1 ? width * height - start : step;
                    var rVec = Enumerable.Range(start, count).SelectMany(n => new[] { res * (n % width - cX), res * (height - n / width - 1 - cY) }).ToArray();
                    var results = NativeWrapper.HRTEM_Solver(gPsi, gVec, gLenz, rVec, quasiMode);
                    for (var i = 0; i < defLen; i++)
                        Array.Copy(results, i * count, images[i], start, count);
                });
            }
            else//Managed
            {
                Parallel.For(0, width * height, n =>
                {
                    PointD r = new PointD(n % width - cX, height - n / width - 1 - cY) * res, _vec = new PointD(double.NaN, double.NaN);
                    var sums = new Complex[defLen];
                    var exp = new Complex(0, 0);
                    foreach (var (Psi, Vec, Lenz) in gList)
                    {
                        if (_vec != Vec)
                            exp = Exp(Vec * r * TwoPiI);

                        for (var (i, f) = (0, Psi * exp); i < defLen; i++)
                            sums[i] += f * Lenz[i];

                        _vec = Vec;
                    }
                    for (var i = 0; i < defLen; i++)
                        images[i][n] = quasiMode ? sums[i].Magnitude2() : Math.Abs(sums[i].Real);
                });
            }
            return images;
        }
        #endregion Image Simulation

        #region 構造因子 F
        /// <summary>
        /// 構造因子を求める. s2の単位は nm^-2
        /// </summary>
        /// <param name="h"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        private (Complex Real, Complex Imag) getF((int H, int K, int L) index, double s2)
        {
            Complex fReal = 0, fImag = 0;
            foreach (var atoms in Crystal.Atoms)
            {
                //var real = AtomConstants.ElectronScatteringEightGaussian[atoms.AtomicNumber].Factor(s2);
                var real = AtomConstants.ElectronScattering[atoms.AtomicNumber][atoms.SubNumberElectron].Factor(s2);
                // 等方散乱因子の時 あるいは非等方でg=0の時
                if (atoms.Dsf.UseIso || (index == (0, 0, 0)))
                {
                    var m =  atoms.Dsf.Biso;//Bisoの単位はnm^2
                    if (!atoms.Dsf.UseIso && double.IsNaN(m) && index == (0, 0, 0))// 非等方でg = 0、かつmがNaNの時 Acta Cryst. (1959). 12, 609 , Hamilton の式に従って、Bisoを計算
                    {
                        double a = Crystal.A, b = Crystal.B, c = Crystal.C;
                        m = (atoms.Dsf.B11 * a * a + atoms.Dsf.B22 * b * b + atoms.Dsf.B33 * c * c + 2 * atoms.Dsf.B12 * a * b + 2 * atoms.Dsf.B23 * b * c + 2 * atoms.Dsf.B31 * c * a) * 4.0 / 3.0;
                    }

                    var t = double.IsNaN(m) ? 1 : Math.Exp(-m * s2);
                    //var imag = AtomConstants.ElectronScatteringEightGaussian[atoms.AtomicNumber].FactorImaginary(s2, m);//答えは無次元
                    var imag = AtomConstants.ElectronScattering[atoms.AtomicNumber][atoms.SubNumberElectron].FactorImaginary(s2, m);//答えは無次元
                    foreach (var atom in atoms.Atom)
                    {
                        var d = t * Exp(TwoPiI * (atom * index)) * atoms.Occ;
                        fReal += real * d;
                        fImag += imag * d;
                    }
                }
                //非等方散乱因子一般
                else
                {
                    foreach (var atom in atoms.Atom)
                    {
                        var (H, K, L) = atom.Operation.ConvertPlaneIndex(index);
                        var m = atoms.Dsf.B11 * H * H + atoms.Dsf.B22 * K * K + atoms.Dsf.B33 * L * L + 2 * atoms.Dsf.B12 * H * K + 2 * atoms.Dsf.B23 * K * L + 2 * atoms.Dsf.B31 * L * H;
                        //var imag = AtomConstants.ElectronScatteringEightGaussian[atoms.AtomicNumber].FactorImaginary(s2, m / s2);//答えは無次元

                        if (double.IsNaN(m)) m = 0;
                        var imag = AtomConstants.ElectronScattering[atoms.AtomicNumber][atoms.SubNumberElectron].FactorImaginary(s2, m / s2);//答えは無次元
                        var d = Math.Exp(-m) * Exp(TwoPiI * (atom * index)) * atoms.Occ;
                        fReal += real * d;
                        fImag += imag * d;
                    }
                }
            }
            return (fReal, fImag);
        }
        #endregion

        #region ポテンシャル U
        /// <summary>
        /// ポテンシャルを求める. s2の単位は nm^-2
        /// </summary>
        /// <param name="kV"></param>
        /// <param name="h"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        private (Complex Real, Complex Imag) getU(double kV, (int H, int K, int L) index, double s2)
        {
            var key = index.H * 1024 * 1024 + index.K * 1024 + index.L;
            if (!uDictionary.TryGetValue(key, out (Complex real, Complex imag) u))
            {
                var (fReal, fImag) = getF(index, s2);
                //Kirklandの教科書のp120参照
                var coeff1 = 1 / Math.PI / Crystal.Volume;
                //相対論補正
                var gamma = 1 + UniversalConstants.e0 * kV * 1E3 / UniversalConstants.m0 / UniversalConstants.c2;
                var beta = Math.Sqrt(1 - 1 / gamma / gamma);
                var coeff2 = 2 * UniversalConstants.h / UniversalConstants.m0 / beta / UniversalConstants.c * 1E9;
                u = (fReal * coeff1 * gamma, fImag * coeff1 * coeff2 * gamma);
                uDictionary.Add(key, u);
            }
            if(double.IsNaN(u.real.Real))
            {


            }


            return u;
        }
        private (Complex Real, Complex Imag) getU((int H, int K, int L) index, double s2) => getU(AccVoltage, index, s2);

        private Dictionary<int, (Complex, Complex)> uDictionary = new Dictionary<int, (Complex, Complex)>();
        #endregion

        #region ポテンシャルのマトリックス
        /// <summary>
        /// ポテンシャルマトリックスを求める. k0の単位はnm^-1. 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private Complex[,] getPotentialMatrix(Beam[] b)
        {
            var potentialMatrix = new Complex[b.Length, b.Length];//A行列確保
            //A行列を決定
            for (int i = 0; i < b.Length; i++)
                for (int j = 0; j < b.Length; j++)
                {
                    var (Real, Imag) = getU((b[i].H - b[j].H, b[i].K - b[j].K, b[i].L - b[j].L), (b[i].Vec - b[j].Vec).Length2 / 4);
                    potentialMatrix[i, j] = i == j ? ImaginaryOne * Imag : Real + ImaginaryOne * Imag;
                }
            return potentialMatrix;
        }
        #endregion

        #region 固有値問題対象のマトリックス
        /// <summary>
        /// 固有値問題マトリックスを求める. k0の単位はnm^-1
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private Complex[,] getEigenProblemMatrix(Beam[] b, Complex[,] potentialMatrix = null)
        {
            if (potentialMatrix == null || potentialMatrix.GetLength(0) != b.Length)
                potentialMatrix = getPotentialMatrix(b);

            //A行列を決定
            var eigenProblemMatrix = new Complex[b.Length, b.Length];//A行列確保
            for (int i = 0; i < b.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                    eigenProblemMatrix[i, j] = potentialMatrix[i, j] / b[i].P;

                eigenProblemMatrix[i, i] += b[i].Q / b[i].P;

            }
            return eigenProblemMatrix;
        }
        #endregion

        #region 候補となるg vectorsの検索
        /// <summary>
        /// 候補となるgVectorを検索する.
        /// </summary>
        /// <param name="baseRotation"></param>
        /// <param name="k0"></param>
        /// <returns></returns>
        public Beam[] Find_gVectors(Matrix3D baseRotation, Vector3DBase vecK0, int maxNumOfBloch = -1)
        {
            //if (double.IsNaN(vecK0.X))
            //    return null;

            if (maxNumOfBloch > 0)
                MaxNumOfBloch = maxNumOfBloch;

            var threshold = 0.8;//逆空間でエワルド球からこの値(nm^-1)より離れていたら、無条件に棄却

            var mat = baseRotation * Crystal.MatrixInverse.Transpose();

            var direction = new List<(int h, int k, int l)>();

            #region directionを初期化
            if (Crystal.Symmetry.LatticeTypeStr == "F")
                direction.AddRange(new (int h, int k, int l)[] { (1, 1, 1), (1, 1, -1), (1, -1, 1), (1, -1, -1), (-1, 1, 1), (-1, 1, -1), (-1, -1, 1), (-1, -1, -1) });
            else if (Crystal.Symmetry.LatticeTypeStr == "A")
                direction.AddRange(new (int h, int k, int l)[] { (0, 1, 1), (0, 1, -1), (0, -1, 1), (0, -1, -1), (1, 0, 0), (-1, 0, 0) });
            else if (Crystal.Symmetry.LatticeTypeStr == "B")
                direction.AddRange(new (int h, int k, int l)[] { (1, 0, 1), (1, 0, -1), (-1, 0, 1), (-1, 0, -1), (0, 1, 0), (0, -1, 0) });
            else if (Crystal.Symmetry.LatticeTypeStr == "C")
                direction.AddRange(new (int h, int k, int l)[] { (1, 1, 0), (1, -1, 0), (-1, 1, 0), (-1, -1, 0), (0, 0, 1), (0, 0, -1) });
            else if (Crystal.Symmetry.LatticeTypeStr == "I")
                direction.AddRange(new (int h, int k, int l)[] { (1, 1, 0), (1, -1, 0), (-1, 1, 0), (-1, -1, 0), (0, 1, 1), (0, 1, -1), (0, -1, 1), (0, -1, -1), (1, 0, 1), (1, 0, -1), (-1, 0, 1), (-1, 0, -1) });
            else if (Crystal.Symmetry.LatticeTypeStr == "R" && Crystal.Symmetry.SpaceGroupHMsubStr == "H")
                direction.AddRange(new (int h, int k, int l)[] { (1, 0, 1), (0, -1, 1), (-1, 1, 1), (-1, 0, -1), (0, 1, -1), (1, -1, -1) });
            else if (Crystal.Symmetry.CrystalSystemStr == "trigonal" || Crystal.Symmetry.CrystalSystemStr == "hexagonal")
                direction.AddRange(new (int h, int k, int l)[] { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (1, -1, 0), (-1, 1, 0), (0, 0, 1), (0, 0, -1) });
            else
                direction.AddRange(new (int h, int k, int l)[] { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1) });

            #endregion directionを初期化

            var (q0, p0) = getQP(new Vector3DBase(0, 0, 0), vecK0);
            var beams = new List<Beam> { { new Beam((0, 0, 0), new Vector3DBase(0, 0, 0), getU(AccVoltage, (0, 0, 0), 0), (q0, p0)) } };
            var outer = new Dictionary<(int h, int k, int l), double> { { (0, 0, 0), 0 } };
            var whole = new HashSet<int> { 0 };

            var shift = direction.Select(dir => (mat * dir).Length).Max() * 2;

            var k0 = vecK0.Length;
            double minR = (k0 - threshold) * (k0 - threshold) - k0 * k0, maxR = (k0 + threshold) * (k0 + threshold) - k0 * k0;
            double minR2 = (k0 - threshold - shift) * (k0 - threshold - shift) - k0 * k0, maxR2 = (k0 + threshold + shift) * (k0 + threshold + shift) - k0 * k0;

            const int coeff = 1024;
            var zero = (new Complex(0, 0), new Complex(0, 0));
            while (beams.Count < MaxNumOfBloch * 4 && whole.Count < 1000000)
            {
                var min = outer.Min(c => c.Value);
                var keyList = outer.Where(c => c.Value - min < shift).Select(c => c.Key).ToList();
                foreach ((int h1, int k1, int l1) in keyList)
                    foreach ((int h2, int k2, int l2) in direction)
                    {
                        int h = h1 + h2, k = k1 + k2, l = l1 + l2, key = h * coeff * coeff + k * coeff + l;
                        if (!whole.Contains(key))
                        {
                            var g = mat * (h, k, l);
                            var (q, p) = getQP(g, vecK0);
                            if (q > minR2 && q < maxR2)
                            {
                                if (q > minR && q < maxR)
                                    beams.Add(new Beam((h, k, l), g, zero, (q, p)));
                                whole.Add(key);
                                outer.Add((h, k, l), g.Length);
                            }
                        }
                    }
                keyList.ForEach(key => outer.Remove(key));
            }

            //indexが小さく、かつQg(励起誤差)の小さいg-vectorを抽出する
            beams.Sort((a, b) =>
            {
                var c = a.Rating - b.Rating;
                return (c > 0) ? 1 : (c < 0) ? -1 : 0;
            });

            if (beams.Count > MaxNumOfBloch + 1)
                beams.RemoveRange(MaxNumOfBloch + 1, beams.Count - MaxNumOfBloch - 1);

            for (int i = 0; i < beams.Count; i++)
            {
                var bi = beams[i];
                for (int j = i + 1; j < beams.Count; j++)
                {
                    var bj = beams[j];
                    if ((bi.Vec.X - bj.Vec.X) * (bi.Vec.X - bj.Vec.X) < 1E-10 && (bi.Vec.Y - bj.Vec.Y) * (bi.Vec.Y - bj.Vec.Y) < 1E-10)
                    {
                        if (Math.Abs(bi.S) > Math.Abs(bj.S))
                        {
                            beams.RemoveAt(i--);//iの方を除去
                            break;
                        }
                        else
                            beams.RemoveAt(j--);
                    }
                }
            }

            int n = beams.Count - 1;
            for (int i = beams.Count - 1; i >= 1; i--)
                if (Math.Abs(beams[i].Rating - beams[i - 1].Rating) > 1E-10)
                {
                    n = i;
                    break;
                }
            beams.RemoveRange(n, beams.Count - n);

            //最後にポテンシャルを計算
            beams.ForEach(b => (b.Freal, b.Fimag) = getU(AccVoltage, b.Index, b.Vec.Length2 / 4));

            return beams.ToArray();
        }

        #endregion

        #region 絞りの内部にあるビームのみ選び取る (HRTEM シミュレータから呼ばれる)
        public Beam[] ExtractInsideBeams(Beam[] beams, double acc, double radius, double shiftX, double shiftY)
        {
            if (double.IsInfinity(radius))
                return beams.ToArray();
            else
            {
                var rambda = UniversalConstants.Convert.EnergyToElectronWaveLength(acc);
                var center = new PointD(2 * Math.Sin(shiftX / 2) / rambda, 2 * Math.Sin(shiftY / 2) / rambda);
                var r = 2 * Math.Sin(radius / 2) / rambda;
                return beams.Where(b => (b.Vec.ToPointD() - center).Length2 < r * r).ToArray();
            }
        }
        #endregion

        #region P, Q のリセットやゲット

        /// <summary>
        /// 引数のBeamsとrotationをもとに、PとQだけセットして返す。ほかのパラメータは放置. CBEDの時にみ呼ばれる。
        /// </summary>
        /// <param name="baseRotation"></param>
        /// <param name="k0"></param>
        /// <returns></returns>
        private Beam[] reset_gVectors(Beam[] beams, Matrix3D baseRotation, Vector3DBase vecK0)
        {
            var mat = baseRotation * Crystal.MatrixInverse.Transpose();
            var newBeams = new List<Beam>();
            for (int i = 0; i < beams.Length; i++)
            {
                var g = mat * beams[i].Index;
                var prms = getQP(g, vecK0);
                newBeams.Add(new Beam(prms));
            }
            return newBeams.ToArray();
        }

        private (double Q, double P) getQP(Vector3DBase g, Vector3DBase vecK0)
            => (vecK0.Length2 - (vecK0 + g).Length2, 2 * Surface * (vecK0 + g));

        private (double Q, double P) getQP(Vector3DBase g, double kvac, double u0, Matrix3D beamRotation = null)
            => getQP(g, getVecK0(kvac, u0, beamRotation));

        #endregion

        #region K0ベクトルを求める
        /// <summary>
        /// K0ベクトルを求める。K0ベクトルは、XY方向を保存したままZ方向のみ変化する。
        /// </summary>
        /// <param name="beamRotation"></param>
        /// <param name="kvac"></param>
        /// <param name="u0"></param>
        /// <returns></returns>
        private Vector3DBase getVecK0(double kvac, double u0, Matrix3D beamRotation = null)
        {
            // |k0|^2 - |kvac|^2 = u0
            // vecK0 = vecKvac + x * vecSurface
            //   =>   x^2 + 2 vecSurface * vecKvac - u0 = 0
            // を満たすxを求めれば良い。
            var vecKvac = (beamRotation == null) ? new Vector3DBase(0, 0, -kvac) : beamRotation * new Vector3DBase(0, 0, -kvac);
            var b = Surface * vecKvac;
            var x = Math.Sqrt(b * b + u0) - b;
            return vecKvac + x * Surface;
        }

        #endregion

        #region Beamクラス

        public class Beam
        {
            /// <summary>
            /// 指数
            /// </summary>
            public int H => Index.H;
            public int K => Index.K;
            public int L => Index.L;

            /// <summary>
            /// 指数
            /// </summary>
            public (int H, int K, int L) Index;

            /// <summary>
            /// 逆格子ベクトル
            /// </summary>
            public Vector3DBase Vec;

            /// <summary>
            /// 励起誤差
            /// </summary>
            public double S => Math.Sqrt(P * P / 4 + Q) - P / 2;

            public Complex Freal;

            public Complex Fimag;

            /// <summary>
            /// k0^2 - (k0 + g)^2 = - g (2 k0 +g) (2 k0 S 励起誤差とわずかに定義が違う)
            /// </summary>
            public double Q;

            /// <summary>
            /// 2 n (k0 + g) 大塚さんの資料参考
            /// </summary>
            public double P;

            /// <summary>
            /// 振幅
            /// </summary>
            public Complex Psi;

            /// <summary>
            /// 強度を保存する(PED計算の時のみ使用)
            /// </summary>
            public double intensity = 0;


            /// <summary>
            /// レンズ関数 
            /// 球面収差関数 × 時間的インコヒーレンス包絡関数 × 空間的インコヒーレンス包絡関数 
            /// </summary>
            public Complex Lenz = new Complex(1, 0);


            /// <summary>
            /// 評価値
            /// </summary>
            public double Rating;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="hkl">指数</param>
            /// <param name="vec">逆格子ベクトル</param>
            /// <param name="s">励起誤差</param>
            public Beam((int H, int K, int L) index, Vector3DBase vec, (Complex Real, Complex Imag) f, (double Q, double P) prms)
            {
                Index = index;
                Vec = vec;
                Freal = f.Real;
                Fimag = f.Imag;
                Q = prms.Q;
                P = prms.P;
                Rating = Math.Sqrt(Vec.Length2) * Math.Abs(Q) * Math.Abs(Q);
            }


            public Beam(double q, double p)
            {
                Q = q;
                P = p;
            }
            public Beam((double Q, double P) prms) : this(prms.Q, prms.P) { }

            public Vector3D ConvertToVector3D()
            {
                Vector3D g = new Vector3D(Vec.X, Vec.Y, Vec.Z);
                g.d = 1 / g.Length;
                g.Text = $"{H} {K} {L}";
                g.Index = (H, K, L);
                g.F = Psi;
                g.RawIntensity = Psi.Magnitude2();
                g.Tag = S;
                g.Flag = true;
                g.Argb = Color.White.ToArgb();
                return g;
            }
        }

        #endregion

        #region CBED_Diskクラス
        public class CBED_Disk
        {
            /// <summary>
            /// 指数
            /// </summary>
            public int H, K, L;

            /// <summary>
            /// 厚み
            /// </summary>
            public double Thickness;

            public Vector3DBase G;

            /// <summary>
            /// 強度を格納した配列
            /// </summary>
            public double[] Intensity;

            public CBED_Disk(int[] hkl, Vector3DBase vec, double thickness, double[] intensity)
            {
                H = hkl[0];
                K = hkl[1];
                L = hkl[2];
                G = vec;
                Thickness = thickness;
                Intensity = intensity;
            }
        } 
        #endregion

    }
}