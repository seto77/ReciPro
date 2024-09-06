using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using V3 = OpenTK.Vector3d;
using M3 = OpenTK.Matrix3d;

namespace Crystallography
{
    public class MonteCarlo
    {
        public static int seed = 0;

        public double Z, A, ρ;
        public double InitialKev, Tilt;
        public double coeff1, coeff2, coeff3;
     
        public double k, J, tan;

        public double ThresholdKev;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="z">原子番号 (単位無し)</param>
        /// <param name="a">原子量 (g/mol)</param>
        /// <param name="_ρ">密度 (g/cm^3)</param>
        /// <param name="kev">入射電子エネルギー (kev)</param>
        /// <param name="tilt">試料表面の傾き (rad, X軸で回転)</param>
        /// <param name="thresholdKev">飛程計算を打ち切るエネルギー (kev)</param>
        public MonteCarlo( double z, double a, double _ρ, double kev, double tilt, double thresholdKev=2)
        {
            //Electron beam-specimen interactions and simulation methods in microscopy 2018
            //Eqs (2.38), (2.41), (2.42) などを参考

            Z = z;
            A = a;
            Tilt = tilt;
            ρ = _ρ;
            InitialKev = kev;
            Tilt = tilt;
            ThresholdKev = thresholdKev;

            //トータル散乱断面積の計算中に出てくる定数
            coeff1 = Z * UniversalConstants.e0 * UniversalConstants.e0 / (8.0 * Math.PI * UniversalConstants.ε0);
            //平均自由行程のところに出てくる定数
            coeff2 = A / UniversalConstants.A / ρ * 1E21;// / Math.PI;
            //阻止能の計算中に出てくる定数
            coeff3 = -Z * UniversalConstants.A * ρ * 1E3 / (A * 1E-3) * Math.Pow(UniversalConstants.e0, 4)
                / 4 / Math.PI / UniversalConstants.ε0 / UniversalConstants.ε0 / UniversalConstants.eV_joule * 1E-9 * 1E-3;
            
            //阻止能の計算中に出てくる物質依存の定数 k    Joy and Luo (1989)によれば 6C:0.77, 13Al: 0.815, 14Si: 0.822, 28Ni: 0.83, 29Cu: 0.83,  47Ag:0.852, 79Au: 0.851
            //取りあえず対数近似した値を使う
            k = 0.0299 * Math.Log(Z) + 0.7307;
            //阻止能の計算中に出てくる物質依存の定数 J (eV) Z<=12の時は J=11.5evにするらしい (Joy&Luo 1989)
            J = Z <= 12 ? 11.5 * Z : (9.76 * Z + 58.5 / Math.Pow(Z, 0.19));
            tan = Math.Tan(tilt);
        }

        public (double ScreeningParameter, double CrossSection, double MeanFreePath, double StoppingPower) GetParameters(double kev)
        {
            //電子の質量 (kg) × 電子の速度の2乗 (m^2/s^2)
            var mv2 = UniversalConstants.Convert.EnergyToElectronMass(kev) * UniversalConstants.Convert.EnergyToElectronVelositySquared(kev);
            //散乱係数
            var α = 0.0034 * Math.Pow(Z, 2.0 / 3.0) / kev;
            //トータル散乱断面積 (nm^2)
            var t1 = coeff1 / mv2;
            var σ_E = t1 * t1 * 4 * Math.PI / α / (α + 1) * 1E18;
            //弾性散乱平均自由行程 (nm) 
            var λ_el = coeff2 / σ_E;
            //阻止能 (Joy and Luo 1989) (kev/nm単位)
            var sp = coeff3 / mv2 * Math.Log(1.166 * k + 0.583 * mv2 / UniversalConstants.eV_joule / J);
            return (α, σ_E, λ_el, sp);
        }

        /// <summary>
        /// 電子線の飛程を計算する. 電子は -Z軸に沿って入射し、試料と(0,0,0)の座標で衝突したあと、thresholdで指定したエネルギーまで減衰するか、
        /// 試料表面を脱出するまでの飛程を計算する。返り値は、座標 p (µm単位)と エネルギー e (kev単位) のタプル配列
        /// </summary>
        /// <returns>返り値は、座標 p (µm単位)と エネルギー e (kev単位) のタプル配列</returns>
        public (V3 p, double e)[] GetTrajectories()
        {
            var r = new Random(Interlocked.Increment(ref seed));
            var trajectory = new List<(V3 p, double e)>() { (new V3(0, 0, 0), InitialKev) };
            var vec = new V3(0, 0, -1);

            //電子エネルギーがThresholdKev以下になるか、試料を脱出するまでループ
            while (trajectory[^1].e > ThresholdKev && trajectory[^1].p.Y * tan >= trajectory[^1].p.Z)
            {
                var (α, σ_E, λ_el, sp) = GetParameters(trajectory[^1].e);

                //飛行距離 s
                var s = -λ_el * Math.Log(r.NextDouble());

                if (trajectory.Count == 1)
                    trajectory.Add((s * vec, trajectory[^1].e + s * sp));
                else
                {
                    double rnd2 = r.NextDouble(), rnd3 = r.NextDouble();
                    double cosθ = 1 - 2 * α * rnd2 / (1 + α - rnd2), sinθ = Math.Sqrt(1 - cosθ * cosθ);
                    double φ = 2 * Math.PI * rnd3, cosφ = Math.Cos(φ), sinφ = Math.Sin(φ);
                    var rot = CreateRotationFromZ(vec);
                    vec = new V3(
                        rot.M11 * sinθ * cosφ + rot.M12 * sinθ * sinφ + rot.M13 * cosθ,
                        rot.M21 * sinθ * cosφ + rot.M22 * sinθ * sinφ + rot.M23 * cosθ,
                        rot.M31 * sinθ * cosφ + rot.M32 * sinθ * sinφ + rot.M33 * cosθ
                                );
                    trajectory.Add((trajectory[^1].p + s * vec, trajectory[^1].e + s * sp));
                }
            }
            return trajectory.Select(e => (e.p / 1000, e.e)).ToArray();
        }


        /// <summary>
        /// Z軸(001)を引数のベクトルvに回転させる行列を生成する
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static M3 CreateRotationFromZ(V3 v)
        {
            v.Normalize();
            if (Math.Abs(v.Z - 1) < Th)
                return M3.Identity;
            else if (Math.Abs(v.Z + 1) < Th)
                return M3.CreateRotationX(Math.PI);
            else
                return M3.CreateFromAxisAngle(V3.Cross(v, Z_vector), V3.CalculateAngle(Z_vector, v));
        }
        public static readonly V3 Z_vector = new(0, 0, 1);
        public const double Th = 0.0000001;

    }
}
