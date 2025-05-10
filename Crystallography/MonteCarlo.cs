using System;
using System.Collections.Generic;
using System.Linq;
using V3 = OpenTK.Mathematics.Vector3d;
using M3 = OpenTK.Mathematics.Matrix3d;
using ZLinq;
using System.Runtime.InteropServices;

namespace Crystallography;


//Electron beam-specimen interactions and simulation methods in microscopy 2018
//Eqs (2.38), (2.41), (2.42) などを参考
public class MonteCarlo
{
    private readonly Random Rnd = Random.Shared;

    public readonly double Z, A, ρ;
    public readonly double InitialKev, Tilt;
    public readonly double coeff0, coeff1, coeff2, coeff3;
 
    public readonly double k, J, tan, cos, sin;

    public readonly double ThresholdKev;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="z">原子番号 (単位無し)</param>
    /// <param name="a">原子量 (g/mol)</param>
    /// <param name="_ρ">密度 (g/cm^3)</param>
    /// <param name="kev">入射電子エネルギー (kev)</param>
    /// <param name="tilt">試料表面の傾き (rad, X軸で回転)</param>
    /// <param name="thresholdKev">飛程計算を打ち切るエネルギー (kev)</param>
    public MonteCarlo(double z, double a, double _ρ, double kev, double tilt, double thresholdKev = 2)
    {
        Z = z;
        A = a;
        ρ = _ρ;
        InitialKev = kev;
        Tilt = tilt;
        ThresholdKev = thresholdKev;

        //散乱係数の計算中に出てくる定数
        coeff0 = 0.0034 * Math.Pow(Z, 2.0 / 3.0);
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
        cos = Math.Cos(tilt);
        sin = Math.Sin(tilt);
    }

    public (double ScreeningParameter, double CrossSection, double MeanFreePath, double StoppingPower) GetParameters(double kev)
    {
        //電子の質量 (kg) × 電子の速度の2乗 (m^2/s^2)
        var mv2 = UniversalConstants.Convert.EnergyToElectronMass(kev) * UniversalConstants.Convert.EnergyToElectronVelositySquared(kev);
        //散乱係数
        var α = coeff0 / kev;
        //トータル散乱断面積 (nm^2)
        var tmp = 2 * coeff1 / mv2;
        var σ_E = tmp * tmp * Math.PI / α / (α + 1) * 1E18;
        //σ_E = 5.21E-21 * Z * Z / kev / kev * 12.56 / α / (α + 1) * Math.Pow((kev + 511) / (kev + 1022), 2);
        //弾性散乱平均自由行程 (nm) 
        var λ_el = coeff2 / σ_E; //λ_el = A / UniversalConstants.A / ρ / σ_E * 1E7;
        //阻止能 (Joy and Luo 1989) (kev/nm単位)
        var sp = coeff3 / mv2 * Math.Log(1.166 * k + 0.583  / UniversalConstants.eV_joule / J * mv2 );
        return (α, σ_E, λ_el, sp);
    }

    /// <summary>
    /// 電子線の飛程を計算する. 電子は -Z軸に沿って入射し、試料と(0,0,0)の座標で衝突したあと、thresholdで指定したエネルギーまで減衰するか、
    /// 試料表面を脱出するまでの飛程を計算する。返り値は、座標 p (nm単位)と エネルギー e (kev単位) のタプル配列
    /// </summary>
    /// <returns>返り値は、座標 p (µm単位)と エネルギー e (kev単位) のタプル配列</returns>
    public List<(V3 p, double e)> GetTrajectories()
    {
        var trajectory = new List<(V3 p, double e)>(100) { (new V3(0, 0, 0), InitialKev) };
        //var vec = new V3(0, 0, -1);
        double m11, m12, m13, m22, m23;
        double vX = 0, vY = 0, vZ = -1;
        int n = 0;

        //電子エネルギーがThresholdKev以下になるか、試料を脱出するまでループ
        while (trajectory[^1].e > ThresholdKev && trajectory[^1].p.Y * tan >= trajectory[^1].p.Z)
        {
            //パラメーター取得
            var (α, _, λ_el, sp) = GetParameters(trajectory[^1].e);
            //飛行距離 s
            var s = -λ_el * Math.Log(Rnd.NextDouble());
            if (n++ != 0)
            {
                double rnd2 = Rnd.NextDouble(), rnd3 = Rnd.NextDouble();
                double cosθ = 1 - 2 * α * rnd2 / (1 + α - rnd2), sinθ = Math.Sqrt(1 - cosθ * cosθ);
                double φ = 2 * Math.PI * rnd3, sinθcosφ = sinθ * Math.Cos(φ), sinθsinφ = sinθ * Math.Sin(φ);

                //var rot = CreateRotationFromZ(vec);
                //vec = new V3(
                //    rot.M11 * sinθcosφ + rot.M21 * sinθsinφ + rot.M31 * cosθ,
                //    rot.M12 * sinθcosφ + rot.M22 * sinθsinφ + rot.M32 * cosθ,
                //    rot.M13 * sinθcosφ + rot.M23 * sinθsinφ + rot.M33 * cosθ
                //            );

                var vZ1 = vZ + 1;
                if (vZ1 < Th)
                { vX = sinθcosφ; vY = sinθsinφ; vZ = -cosθ; }
                else
                {
                    m11 = 1 - vX * vX / vZ1;
                    m22 = 1 - vY * vY / vZ1;
                    m12 = -vX * vY / vZ1;
                    m13 = vX;
                    m23 = vY;

                    vX = m11 * sinθcosφ + m12 * sinθsinφ + m13 * cosθ;
                    vY = m12 * sinθcosφ + m22 * sinθsinφ + m23 * cosθ;
                    vZ = -m13 * sinθcosφ - m23 * sinθsinφ + vZ * cosθ;

                    if (n % 10 == 0)
                    {
                        var len = Math.Sqrt(vX * vX + vY * vY + vZ * vZ);
                        vX /= len; vY /= len; vZ /= len;
                    }
                }
            }
            trajectory.Add((trajectory[^1].p + s * new V3(vX, vY, vZ), trajectory[^1].e + s * sp));
        }

        trajectory.ForEach(static e => e.p /= 1000.0);
        
        return trajectory;
    }


    /// <summary>
    /// 電子線の飛程を計算する. 電子は -Z軸に沿って入射し、試料と(0,0,0)の座標で衝突したあと、thresholdで指定したエネルギーまで減衰するか、
    /// 試料表面を脱出するまでの飛程を計算する。返り値は、座標 p (nm単位), 出射方向 v (単位ベクトル), エネルギー e (kev単位) のタプル
    /// </summary>
    /// <returns>返り値は、深さ (nm単位), 出射方向 (単位ベクトル), エネルギー e (kev単位) のタプル</returns>
    public (double d, V3 v, double e) GetBackscatteredElectrons()
    {
        double e = InitialKev;
        double vX = 0, vY = 0, vZ = -1;
        double pY = 0, pZ = 0;//X座標は考えなくてよい
        int n = 0;
        //電子エネルギーがThresholdKev以下になるか、試料を脱出するまでループ
        while (e > ThresholdKev)
        {
            //乱数発生
            double rnd1 = Rnd.NextDouble(), rnd2 = Rnd.NextDouble(), rnd3 = Rnd.NextDouble();
            //パラメーター取得
            var (α, _, λ_el, sp) = GetParameters(e);
            //飛行距離 s
            var s = -λ_el * Math.Log(rnd1);
            if (n++ != 0)
            {
                double cosθ = 1 - 2 * α * rnd2 / (1 + α - rnd2), sinθ = Math.Sqrt(1 - cosθ * cosθ);
                double φ = 2 * Math.PI * rnd3, sinθcosφ = sinθ * Math.Cos(φ), sinθsinφ = sinθ * Math.Sin(φ);

                //var rot = CreateRotationFromZ(vec);
                //vec = new V3(
                //    rot.M11 * sinθcosφ + rot.M12 * sinθsinφ + rot.M13 * cosθ,
                //    rot.M12 * sinθcosφ + rot.M22 * sinθsinφ + rot.M23 * cosθ,
                //    rot.M13 * sinθcosφ + rot.M32 * sinθsinφ + rot.M33 * cosθ
                //            );

                var vZ1 = vZ + 1;
                if (vZ1 < Th)
                { vX = sinθcosφ; vY = sinθsinφ; vZ = -cosθ; }
                else
                {
                    var m11 = 1 - vX * vX / vZ1;
                    var m22 = 1 - vY * vY / vZ1;
                    var m12 = -vX * vY / vZ1;
                    var m13 = vX;
                    var m23 = vY;
                    vX = m11 * sinθcosφ + m12 * sinθsinφ + m13 * cosθ;
                    vY = m12 * sinθcosφ + m22 * sinθsinφ + m23 * cosθ;
                    vZ = -m13 * sinθcosφ - m23 * sinθsinφ + vZ * cosθ;

                    if (n % 10 == 0)
                    {
                        var len = Math.Sqrt(vX * vX + vY * vY + vZ * vZ);
                        vX /= len; vY /= len; vZ /= len;
                    }
                }
            }
            double pYtmp = pY + s * vY, pZtmp = pZ + s * vZ;
            if (pYtmp * tan < pZtmp)
                break;
            pY = pYtmp; pZ = pZtmp;

            e += s * sp;
        }
        return (sin * pY - cos * pZ, new V3(vX, vY, vZ), e);
    }
  
    public const double Th = 0.0000001;

}
