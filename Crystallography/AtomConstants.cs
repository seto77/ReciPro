using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using Edge = Crystallography.XrayLineEdge;

namespace Crystallography
{
	public static class AtomConstants
	{
		//静的コンストラクタ
		static AtomConstants()
		{
			AtomConstantsSub.LinearAbsorptionCoefficient = new PointD[AtomConstantsSub.MassAbsorptionCoefficient.Length][][];
			for (int i = 0; i < AtomConstantsSub.LinearAbsorptionCoefficient.Length; i++)
			{
				AtomConstantsSub.LinearAbsorptionCoefficient[i] = new PointD[AtomConstantsSub.MassAbsorptionCoefficient[i].Length][];
				for (int j = 0; j < AtomConstantsSub.MassAbsorptionCoefficient[i].Length; j++)
				{
					AtomConstantsSub.LinearAbsorptionCoefficient[i][j] = new PointD[AtomConstantsSub.MassAbsorptionCoefficient[i][j].Length];
					for (int k = 0; k < AtomConstantsSub.LinearAbsorptionCoefficient[i][j].Length; k++)
						AtomConstantsSub.LinearAbsorptionCoefficient[i][j][k] = new PointD(AtomConstantsSub.MassAbsorptionCoefficient[i][j][k]) * AtomConstants.NominalDensity(i);
				}
			}
		}

		public static PointD[] LinearAbsorptionCoefficient(int z)
		{
			List<PointD> pt = new List<PointD>();
			for (int n = 0; n < AtomConstantsSub.LinearAbsorptionCoefficient[z].Length; n++)
				pt.AddRange(AtomConstantsSub.LinearAbsorptionCoefficient[z][n]);
			return pt.ToArray();
		}

		public static PointD[] MassAbsorptionCoefficient(int z)
		{
			List<PointD> pt = new List<PointD>();
			for (int n = 0; n < AtomConstantsSub.MassAbsorptionCoefficient[z].Length; n++)
				pt.AddRange(AtomConstantsSub.MassAbsorptionCoefficient[z][n].Select(e => new PointD(e)));
			return pt.ToArray();
		}

		/// <summary>
		/// 原子番号 z, 線種 line を入力すると エネルギー (kev) を返す。 対応する原子、線種がない場合はNaNを返す
		/// </summary>
		/// <param name="z"></param>
		/// <param name="line"></param>
		/// <returns></returns>
		public static double CharacteristicXrayEnergy(int z, XrayLine line)
		{
			return UniversalConstants.Convert.WavelengthToXrayEnergy(CharacteristicXrayWavelength(z, line) * 0.1) / 1000;
		}

		/// <summary>
		/// 原子番号 z, 線種 line を入力すると エネルギー (kev) を返す。 対応する原子、線種がない場合はNaNを返す
		/// </summary>
		/// <param name="z"></param>
		/// <param name="line"></param>
		/// <returns></returns>
		public static double CharacteristicXrayEnergy(int z, XrayLineEdge line)
		{
			#region
			switch (z)
			{
				case 1:
					return line switch
					{
						Edge.K => 1.36000E-02,
						_ => double.NaN,
					};
				case 2:
					return line switch
					{
						Edge.K => 2.46000E-02,
						_ => double.NaN,
					};
				case 3:
					return line switch
					{
						Edge.K => 5.47500E-02,
						Edge.L1 => 5.34000E-03,
						_ => double.NaN,
					};
				case 4:
					return line switch
					{
						Edge.K => 1.11000E-01,
						Edge.L1 => 8.42000E-03,
						_ => double.NaN,
					};
				case 5:
					return line switch
					{
						Edge.K => 1.88000E-01,
						Edge.L1 => 1.34700E-02,
						Edge.L2 => 4.70000E-03,
						_ => double.NaN,
					};
				case 6:
					return line switch
					{
						Edge.K => 2.83800E-01,
						Edge.L1 => 1.95100E-02,
						Edge.L2 => 6.40000E-03,
						_ => double.NaN,
					};
				case 7:
					return line switch
					{
						Edge.K => 4.01600E-01,
						Edge.L1 => 2.63100E-02,
						Edge.L2 => 9.20000E-03,
						Edge.L3 => 9.20000E-03,
						_ => double.NaN,
					};
				case 8:
					return line switch
					{
						Edge.K => 5.32000E-01,
						Edge.L1 => 2.37000E-02,
						Edge.L2 => 7.10000E-03,
						Edge.L3 => 7.10000E-03,
						_ => double.NaN,
					};
				case 9:
					return line switch
					{
						Edge.K => 6.85400E-01,
						Edge.L1 => 3.10000E-02,
						Edge.L2 => 8.60000E-03,
						Edge.L3 => 8.60000E-03,
						_ => double.NaN,
					};
				case 10:
					return line switch
					{
						Edge.K => 8.66900E-01,
						Edge.L1 => 4.50000E-02,
						Edge.L2 => 1.83000E-02,
						Edge.L3 => 1.83000E-02,
						_ => double.NaN,
					};
				case 11:
					return line switch
					{
						Edge.K => 1.07210E+00,
						Edge.L1 => 6.33000E-02,
						Edge.L2 => 3.11000E-02,
						Edge.L3 => 3.11000E-02,
						_ => double.NaN,
					};
				case 12:
					return line switch
					{
						Edge.K => 1.30500E+00,
						Edge.L1 => 8.94000E-02,
						Edge.L2 => 5.14000E-02,
						Edge.L3 => 5.14000E-02,
						_ => double.NaN,
					};
				case 13:
					return line switch
					{
						Edge.K => 1.55960E+00,
						Edge.L1 => 1.17700E-01,
						Edge.L2 => 7.31000E-02,
						Edge.L3 => 7.31000E-02,
						Edge.M1 => 8.37567E-03,
						_ => double.NaN,
					};
				case 14:
					return line switch
					{
						Edge.K => 1.83890E+00,
						Edge.L1 => 1.48700E-01,
						Edge.L2 => 9.92000E-02,
						Edge.L3 => 9.92000E-02,
						Edge.M1 => 1.13572E-02,
						Edge.M2 => 5.08305E-03,
						_ => double.NaN,
					};
				case 15:
					return line switch
					{
						Edge.K => 2.14550E+00,
						Edge.L1 => 1.89300E-01,
						Edge.L2 => 1.32200E-01,
						Edge.L3 => 1.32200E-01,
						Edge.M1 => 1.44615E-02,
						Edge.M2 => 6.38493E-03,
						Edge.M3 => 6.33669E-03,
						_ => double.NaN,
					};
				case 16:
					return line switch
					{
						Edge.K => 2.47200E+00,
						Edge.L1 => 2.29200E-01,
						Edge.L2 => 1.64800E-01,
						Edge.L3 => 1.64800E-01,
						Edge.M1 => 1.76882E-02,
						Edge.M2 => 7.81363E-03,
						Edge.M3 => 7.73488E-03,
						_ => double.NaN,
					};
				case 17:
					return line switch
					{
						Edge.K => 2.82240E+00,
						Edge.L1 => 2.70200E-01,
						Edge.L2 => 2.01600E-01,
						Edge.L3 => 2.00000E-01,
						Edge.M1 => 1.75000E-02,
						Edge.M2 => 6.80000E-03,
						Edge.M3 => 6.80000E-03,
						_ => double.NaN,
					};
				case 18:
					return line switch
					{
						Edge.K => 3.20290E+00,
						Edge.L1 => 3.20000E-01,
						Edge.L2 => 2.47300E-01,
						Edge.L3 => 2.45200E-01,
						Edge.M1 => 2.53000E-02,
						Edge.M2 => 1.24000E-02,
						Edge.M3 => 1.24000E-02,
						_ => double.NaN,
					};
				case 19:
					return line switch
					{
						Edge.K => 3.60740E+00,
						Edge.L1 => 3.77100E-01,
						Edge.L2 => 2.96300E-01,
						Edge.L3 => 2.93600E-01,
						Edge.M1 => 3.39000E-02,
						Edge.M2 => 1.78000E-02,
						Edge.M3 => 1.78000E-02,
						_ => double.NaN,
					};
				case 20:
					return line switch
					{
						Edge.K => 4.03810E+00,
						Edge.L1 => 4.37800E-01,
						Edge.L2 => 3.50000E-01,
						Edge.L3 => 3.46400E-01,
						Edge.M1 => 4.37000E-02,
						Edge.M2 => 2.54000E-02,
						Edge.M3 => 2.54000E-02,
						_ => double.NaN,
					};
				case 21:
					return line switch
					{
						Edge.K => 4.49280E+00,
						Edge.L1 => 5.00400E-01,
						Edge.L2 => 4.06700E-01,
						Edge.L3 => 4.02200E-01,
						Edge.M1 => 5.38000E-02,
						Edge.M2 => 3.23000E-02,
						Edge.M3 => 3.23000E-02,
						_ => double.NaN,
					};
				case 22:
					return line switch
					{
						Edge.K => 4.96640E+00,
						Edge.L1 => 5.63700E-01,
						Edge.L2 => 4.61500E-01,
						Edge.L3 => 4.55500E-01,
						Edge.M1 => 6.03000E-02,
						Edge.M2 => 3.46000E-02,
						Edge.M3 => 3.46000E-02,
						_ => double.NaN,
					};
				case 23:
					return line switch
					{
						Edge.K => 5.46510E+00,
						Edge.L1 => 6.28200E-01,
						Edge.L2 => 5.20500E-01,
						Edge.L3 => 5.12900E-01,
						Edge.M1 => 6.65000E-02,
						Edge.M2 => 3.78000E-02,
						Edge.M3 => 3.78000E-02,
						Edge.M4 => 2.20000E-03,
						_ => double.NaN,
					};
				case 24:
					return line switch
					{
						Edge.K => 5.98920E+00,
						Edge.L1 => 6.94600E-01,
						Edge.L2 => 5.83700E-01,
						Edge.L3 => 5.74500E-01,
						Edge.M1 => 7.41000E-02,
						Edge.M2 => 4.25000E-02,
						Edge.M3 => 4.25000E-02,
						Edge.M4 => 2.30000E-03,
						Edge.M5 => 2.30000E-03,
						_ => double.NaN,
					};
				case 25:
					return line switch
					{
						Edge.K => 6.53900E+00,
						Edge.L1 => 7.69000E-01,
						Edge.L2 => 6.51400E-01,
						Edge.L3 => 6.40300E-01,
						Edge.M1 => 8.39000E-02,
						Edge.M2 => 4.86000E-02,
						Edge.M3 => 4.86000E-02,
						Edge.M4 => 7.26159E-03,
						Edge.M5 => 7.14378E-03,
						_ => double.NaN,
					};
				case 26:
					return line switch
					{
						Edge.K => 7.11200E+00,
						Edge.L1 => 8.46100E-01,
						Edge.L2 => 7.21100E-01,
						Edge.L3 => 7.08100E-01,
						Edge.M1 => 9.29000E-02,
						Edge.M2 => 5.40000E-02,
						Edge.M3 => 5.40000E-02,
						Edge.M4 => 3.60000E-03,
						Edge.M5 => 3.60000E-03,
						_ => double.NaN,
					};
				case 27:
					return line switch
					{
						Edge.K => 7.70890E+00,
						Edge.L1 => 9.25600E-01,
						Edge.L2 => 7.93600E-01,
						Edge.L3 => 7.78600E-01,
						Edge.M1 => 1.00700E-01,
						Edge.M2 => 5.95000E-02,
						Edge.M3 => 5.95000E-02,
						Edge.M4 => 2.90000E-03,
						Edge.M5 => 2.90000E-03,
						_ => double.NaN,
					};
				case 28:
					return line switch
					{
						Edge.K => 8.33280E+00,
						Edge.L1 => 1.00810E+00,
						Edge.L2 => 8.71900E-01,
						Edge.L3 => 8.54700E-01,
						Edge.M1 => 1.11800E-01,
						Edge.M2 => 6.81000E-02,
						Edge.M3 => 6.81000E-02,
						Edge.M4 => 3.60000E-03,
						Edge.M5 => 3.60000E-03,
						_ => double.NaN,
					};
				case 29:
					return line switch
					{
						Edge.K => 8.97890E+00,
						Edge.L1 => 1.09610E+00,
						Edge.L2 => 9.51000E-01,
						Edge.L3 => 9.31100E-01,
						Edge.M1 => 1.19800E-01,
						Edge.M2 => 7.36000E-02,
						Edge.M3 => 7.36000E-02,
						Edge.M4 => 1.60000E-03,
						Edge.M5 => 1.60000E-03,
						_ => double.NaN,
					};
				case 30:
					return line switch
					{
						Edge.K => 9.65860E+00,
						Edge.L1 => 1.19360E+00,
						Edge.L2 => 1.04280E+00,
						Edge.L3 => 1.01970E+00,
						Edge.M1 => 1.35900E-01,
						Edge.M2 => 8.66000E-02,
						Edge.M3 => 8.66000E-02,
						Edge.M4 => 8.10000E-03,
						Edge.M5 => 8.10000E-03,
						_ => double.NaN,
					};
				case 31:
					return line switch
					{
						Edge.K => 1.03671E+01,
						Edge.L1 => 1.29770E+00,
						Edge.L2 => 1.14230E+00,
						Edge.L3 => 1.11540E+00,
						Edge.M1 => 1.58100E-01,
						Edge.M2 => 1.06800E-01,
						Edge.M3 => 1.02900E-01,
						Edge.M4 => 1.74000E-02,
						Edge.M5 => 1.74000E-02,
						_ => double.NaN,
					};
				case 32:
					return line switch
					{
						Edge.K => 1.11031E+01,
						Edge.L1 => 1.41430E+00,
						Edge.L2 => 1.24780E+00,
						Edge.L3 => 1.21670E+00,
						Edge.M1 => 1.80000E-01,
						Edge.M2 => 1.27900E-01,
						Edge.M3 => 1.20800E-01,
						Edge.M4 => 2.87000E-02,
						Edge.M5 => 2.87000E-02,
						_ => double.NaN,
					};
				case 33:
					return line switch
					{
						 Edge.K=> 1.18667E+01,
						 Edge.L1=> 1.52650E+00,
						 Edge.L2=> 1.35860E+00,
						 Edge.L3=> 1.32310E+00,
						 Edge.M1=> 2.03500E-01,
						 Edge.M2=> 1.46400E-01,
						 Edge.M3=> 1.40500E-01,
						 Edge.M4=> 4.12000E-02,
						 Edge.M5=> 4.12000E-02,
						_=> double.NaN,
					};
				case 34:
					return line switch
					{
						 Edge.K=> 1.26578E+01,
						 Edge.L1=> 1.65390E+00,
						 Edge.L2=> 1.47620E+00,
						 Edge.L3=> 1.43580E+00,
						 Edge.M1=> 2.31500E-01,
						 Edge.M2=> 1.68200E-01,
						 Edge.M3=> 1.61900E-01,
						 Edge.M4=> 5.67000E-02,
						 Edge.M5=> 5.67000E-02,
						_=> double.NaN,
					};
				case 35:
					return line switch
					{
						 Edge.K=> 1.34737E+01,
						 Edge.L1=> 1.78200E+00,
						 Edge.L2=> 1.59600E+00,
						 Edge.L3=> 1.54990E+00,
						 Edge.M1=> 2.56500E-01,
						 Edge.M2=> 1.89300E-01,
						 Edge.M3=> 1.81500E-01,
						 Edge.M4=> 7.01000E-02,
						 Edge.M5=> 6.90000E-02,
						_=> double.NaN,
					};
				case 36:
					return line switch
					{
						 Edge.K=> 1.43256E+01,
						 Edge.L1=> 1.92100E+00,
						 Edge.L2=> 1.72720E+00,
						 Edge.L3=> 1.67490E+00,
						 Edge.M1=> 2.88330E-01,
						 Edge.M2=> 2.22700E-01,
						 Edge.M3=> 2.13800E-01,
						 Edge.M4=> 8.89000E-02,
						 Edge.M5=> 8.89000E-02,
						_=> double.NaN,
					};
				case 37:
					return line switch
					{
						 Edge.K=> 1.51997E+01,
						 Edge.L1=> 2.06510E+00,
						 Edge.L2=> 1.86390E+00,
						 Edge.L3=> 1.80440E+00,
						 Edge.M1=> 3.22100E-01,
						 Edge.M2=> 2.47400E-01,
						 Edge.M3=> 2.38500E-01,
						 Edge.M4=> 1.11800E-01,
						 Edge.M5=> 1.10300E-01,
						_=> double.NaN,
					};
				case 38:
					return line switch
					{
						 Edge.K=> 1.61046E+01,
						 Edge.L1=> 2.21630E+00,
						 Edge.L2=> 2.00680E+00,
						 Edge.L3=> 1.93960E+00,
						 Edge.M1=> 3.57500E-01,
						 Edge.M2=> 2.79800E-01,
						 Edge.M3=> 2.69100E-01,
						 Edge.M4=> 1.35000E-01,
						 Edge.M5=> 1.33100E-01,
						 Edge.N1=> 3.77000E-02,
						 Edge.N2=> 1.99000E-02,
						 Edge.N3=> 1.99000E-02,
						_=> double.NaN,
					};
				case 39:
					return line switch
					{
						 Edge.K=> 1.70384E+01,
						 Edge.L1=> 2.37250E+00,
						 Edge.L2=> 2.15550E+00,
						 Edge.L3=> 2.08000E+00,
						 Edge.M1=> 3.93600E-01,
						 Edge.M2=> 3.12400E-01,
						 Edge.M3=> 3.00300E-01,
						 Edge.M4=> 1.59600E-01,
						 Edge.M5=> 1.57400E-01,
						 Edge.N1=> 4.54000E-02,
						 Edge.N2=> 2.56000E-02,
						 Edge.N3=> 2.56000E-02,
						_=> double.NaN,
					};
				case 40:
					return line switch
					{
						 Edge.K=> 1.79976E+01,
						 Edge.L1=> 2.53160E+00,
						 Edge.L2=> 2.30670E+00,
						 Edge.L3=> 2.22230E+00,
						 Edge.M1=> 4.30300E-01,
						 Edge.M2=> 3.44200E-01,
						 Edge.M3=> 3.30500E-01,
						 Edge.M4=> 1.82400E-01,
						 Edge.M5=> 1.80000E-01,
						 Edge.N1=> 5.13000E-02,
						 Edge.N2=> 2.87000E-02,
						 Edge.N3=> 2.87000E-02,
						 Edge.N4=> 4.02345E-03,
						_=> double.NaN,
					};
				case 41:
					return line switch
					{
						 Edge.K=> 1.89856E+01,
						 Edge.L1=> 2.69770E+00,
						 Edge.L2=> 2.46470E+00,
						 Edge.L3=> 2.37050E+00,
						 Edge.M1=> 4.68400E-01,
						 Edge.M2=> 3.78400E-01,
						 Edge.M3=> 3.63000E-01,
						 Edge.M4=> 2.07400E-01,
						 Edge.M5=> 2.04600E-01,
						 Edge.N1=> 5.81000E-02,
						 Edge.N2=> 3.39000E-02,
						 Edge.N3=> 3.39000E-02,
						 Edge.N4=> 3.20000E-03,
						_=> double.NaN,
					};
				case 42:
					return line switch
					{
						 Edge.K=> 1.99995E+01,
						 Edge.L1=> 2.86550E+00,
						 Edge.L2=> 2.62510E+00,
						 Edge.L3=> 2.52020E+00,
						 Edge.M1=> 5.04600E-01,
						 Edge.M2=> 4.09700E-01,
						 Edge.M3=> 3.92300E-01,
						 Edge.M4=> 2.30300E-01,
						 Edge.M5=> 2.27000E-01,
						 Edge.N1=> 6.18000E-02,
						 Edge.N2=> 3.48000E-02,
						 Edge.N3=> 3.48000E-02,
						 Edge.N4=> 1.80000E-03,
						 Edge.N5=> 1.80000E-03,
						_=> double.NaN,
					};
				case 43:
					return line switch
					{
						 Edge.K=> 2.10440E+01,
						 Edge.L1=> 3.04250E+00,
						 Edge.L2=> 2.79320E+00,
						 Edge.L3=> 2.67690E+00,
						 Edge.M1=> 5.47600E-01,
						 Edge.M2=> 4.44900E-01,
						 Edge.M3=> 4.25000E-01,
						 Edge.M4=> 2.56400E-01,
						 Edge.M5=> 2.52900E-01,
						 Edge.N1=> 6.84000E-02,
						 Edge.N2=> 3.89000E-02,
						 Edge.N3=> 3.89000E-02,
						 Edge.N4=> 7.01158E-03,
						 Edge.N5=> 6.72942E-03,
						_=> double.NaN,
					};
				case 44:
					return line switch
					{
						 Edge.K=> 2.21172E+01,
						 Edge.L1=> 3.22400E+00,
						 Edge.L2=> 2.96690E+00,
						 Edge.L3=> 2.83790E+00,
						 Edge.M1=> 5.85000E-01,
						 Edge.M2=> 4.82800E-01,
						 Edge.M3=> 4.60600E-01,
						 Edge.M4=> 2.83600E-01,
						 Edge.M5=> 2.79400E-01,
						 Edge.N1=> 7.49000E-02,
						 Edge.N2=> 4.31000E-02,
						 Edge.N3=> 4.31000E-02,
						 Edge.N4=> 2.00000E-03,
						 Edge.N5=> 2.00000E-03,
						_=> double.NaN,
					};
				case 45:
					return line switch
					{
						 Edge.K=> 2.32199E+01,
						 Edge.L1=> 3.41190E+00,
						 Edge.L2=> 3.14610E+00,
						 Edge.L3=> 3.00380E+00,
						 Edge.M1=> 6.27100E-01,
						 Edge.M2=> 5.21000E-01,
						 Edge.M3=> 4.96200E-01,
						 Edge.M4=> 3.11700E-01,
						 Edge.M5=> 3.07000E-01,
						 Edge.N1=> 8.10000E-02,
						 Edge.N2=> 4.79000E-02,
						 Edge.N3=> 4.79000E-02,
						 Edge.N4=> 2.50000E-03,
						 Edge.N5=> 2.50000E-03,
						_=> double.NaN,
					};
				case 46:
					return line switch
					{
						 Edge.K=> 2.43503E+01,
						 Edge.L1=> 3.60430E+00,
						 Edge.L2=> 3.33030E+00,
						 Edge.L3=> 3.17330E+00,
						 Edge.M1=> 6.69900E-01,
						 Edge.M2=> 5.59100E-01,
						 Edge.M3=> 5.31500E-01,
						 Edge.M4=> 3.40000E-01,
						 Edge.M5=> 3.34700E-01,
						 Edge.N1=> 8.64000E-02,
						 Edge.N2=> 5.11000E-02,
						 Edge.N3=> 5.11000E-02,
						 Edge.N4=> 5.44663E-03,
						 Edge.N5=> 5.01841E-03,
						_=> double.NaN,
					};
				case 47:
					return line switch
					{
						 Edge.K=> 2.55140E+01,
						 Edge.L1=> 3.80580E+00,
						 Edge.L2=> 3.52370E+00,
						 Edge.L3=> 3.35110E+00,
						 Edge.M1=> 7.17500E-01,
						 Edge.M2=> 6.02400E-01,
						 Edge.M3=> 5.71400E-01,
						 Edge.M4=> 3.72800E-01,
						 Edge.M5=> 3.66700E-01,
						 Edge.N1=> 9.52000E-02,
						 Edge.N2=> 6.26000E-02,
						 Edge.N3=> 5.59000E-02,
						 Edge.N4=> 3.30000E-03,
						 Edge.N5=> 3.30000E-03,
						_=> double.NaN,
					};
				case 48:
					return line switch
					{
						 Edge.K=> 2.67112E+01,
						 Edge.L1=> 4.01800E+00,
						 Edge.L2=> 3.72700E+00,
						 Edge.L3=> 3.53750E+00,
						 Edge.M1=> 7.70200E-01,
						 Edge.M2=> 6.50700E-01,
						 Edge.M3=> 6.16500E-01,
						 Edge.M4=> 4.10500E-01,
						 Edge.M5=> 4.03700E-01,
						 Edge.N1=> 1.07600E-01,
						 Edge.N2=> 6.69000E-02,
						 Edge.N3=> 6.69000E-02,
						 Edge.N4=> 9.30000E-03,
						 Edge.N5=> 9.30000E-03,
						_=> double.NaN,
					};
				case 49:
					return line switch
					{
						 Edge.K=> 2.79399E+01,
						 Edge.L1=> 4.23750E+00,
						 Edge.L2=> 3.93800E+00,
						 Edge.L3=> 3.73010E+00,
						 Edge.M1=> 8.25600E-01,
						 Edge.M2=> 7.02200E-01,
						 Edge.M3=> 6.64300E-01,
						 Edge.M4=> 4.50800E-01,
						 Edge.M5=> 4.43100E-01,
						 Edge.N1=> 1.21900E-01,
						 Edge.N2=> 7.74000E-02,
						 Edge.N3=> 7.74000E-02,
						 Edge.N4=> 1.62000E-02,
						 Edge.N5=> 1.62000E-02,
						_=> double.NaN,
					};
				case 50:
					return line switch
					{
						 Edge.K=> 2.92001E+01,
						 Edge.L1=> 4.46470E+00,
						 Edge.L2=> 4.15610E+00,
						 Edge.L3=> 3.92880E+00,
						 Edge.M1=> 8.83800E-01,
						 Edge.M2=> 7.56400E-01,
						 Edge.M3=> 7.14400E-01,
						 Edge.M4=> 4.93300E-01,
						 Edge.M5=> 4.84800E-01,
						 Edge.N1=> 1.36500E-01,
						 Edge.N2=> 8.86000E-02,
						 Edge.N3=> 8.86000E-02,
						 Edge.N4=> 2.39000E-02,
						 Edge.N5=> 2.39000E-02,
						_=> double.NaN,
					};
				case 51:
					return line switch
					{
						 Edge.K=> 3.04912E+01,
						 Edge.L1=> 4.69830E+00,
						 Edge.L2=> 4.38040E+00,
						 Edge.L3=> 4.13220E+00,
						 Edge.M1=> 9.43700E-01,
						 Edge.M2=> 8.11900E-01,
						 Edge.M3=> 7.65600E-01,
						 Edge.M4=> 5.36900E-01,
						 Edge.M5=> 5.27500E-01,
						 Edge.N1=> 1.52000E-01,
						 Edge.N2=> 9.84000E-02,
						 Edge.N3=> 9.84000E-02,
						 Edge.N4=> 3.14000E-02,
						 Edge.N5=> 3.14000E-02,
						_=> double.NaN,
					};
				case 52:
					return line switch
					{
						 Edge.K=> 3.18138E+01,
						 Edge.L1=> 4.93920E+00,
						 Edge.L2=> 4.61200E+00,
						 Edge.L3=> 4.34140E+00,
						 Edge.M1=> 1.00600E+00,
						 Edge.M2=> 8.69700E-01,
						 Edge.M3=> 8.18700E-01,
						 Edge.M4=> 5.82500E-01,
						 Edge.M5=> 5.72100E-01,
						 Edge.N1=> 1.68300E-01,
						 Edge.N2=> 1.10200E-01,
						 Edge.N3=> 1.10200E-01,
						 Edge.N4=> 3.98000E-02,
						 Edge.N5=> 3.98000E-02,
						_=> double.NaN,
					};
				case 53:
					return line switch
					{
						 Edge.K=> 3.31694E+01,
						 Edge.L1=> 5.18810E+00,
						 Edge.L2=> 4.85210E+00,
						 Edge.L3=> 4.55710E+00,
						 Edge.M1=> 1.07210E+00,
						 Edge.M2=> 9.30500E-01,
						 Edge.M3=> 8.74600E-01,
						 Edge.M4=> 6.31300E-01,
						 Edge.M5=> 6.19400E-01,
						 Edge.N1=> 1.86400E-01,
						 Edge.N2=> 1.22700E-01,
						 Edge.N3=> 1.22700E-01,
						 Edge.N4=> 4.96000E-02,
						 Edge.N5=> 4.96000E-02,
						_=> double.NaN,
					};
				case 54:
					return line switch
					{
						 Edge.K=> 3.45614E+01,
						 Edge.L1=> 5.45280E+00,
						 Edge.L2=> 5.10370E+00,
						 Edge.L3=> 4.78220E+00,
						 Edge.M1=> 1.14460E+00,
						 Edge.M2=> 9.99000E-01,
						 Edge.M3=> 9.37000E-01,
						 Edge.M4=> 6.85400E-01,
						 Edge.M5=> 6.72300E-01,
						 Edge.N1=> 2.08100E-01,
						 Edge.N2=> 1.46700E-01,
						 Edge.N3=> 1.46700E-01,
						 Edge.N4=> 6.40000E-02,
						 Edge.N5=> 6.40000E-02,
						_=> double.NaN,
					};
				case 55:
					return line switch
					{
						 Edge.K=> 3.59846E+01,
						 Edge.L1=> 5.71430E+00,
						 Edge.L2=> 5.35940E+00,
						 Edge.L3=> 5.01190E+00,
						 Edge.M1=> 1.21710E+00,
						 Edge.M2=> 1.06500E+00,
						 Edge.M3=> 9.97600E-01,
						 Edge.M4=> 7.39500E-01,
						 Edge.M5=> 7.25500E-01,
						 Edge.N1=> 2.30800E-01,
						 Edge.N2=> 1.72300E-01,
						 Edge.N3=> 1.61600E-01,
						 Edge.N4=> 7.88000E-02,
						 Edge.N5=> 7.65000E-02,
						 Edge.O1=> 2.27000E-02,
						 Edge.O2=> 1.31000E-02,
						 Edge.O3=> 1.14000E-02,
						_=> double.NaN,
					};
				case 56:
					return line switch
					{
						 Edge.K=> 3.74406E+01,
						 Edge.L1=> 5.98880E+00,
						 Edge.L2=> 5.62360E+00,
						 Edge.L3=> 5.24700E+00,
						 Edge.M1=> 1.29280E+00,
						 Edge.M2=> 1.13670E+00,
						 Edge.M3=> 1.06220E+00,
						 Edge.M4=> 7.96100E-01,
						 Edge.M5=> 7.80700E-01,
						 Edge.N1=> 2.53000E-01,
						 Edge.N2=> 1.91800E-01,
						 Edge.N3=> 1.79700E-01,
						 Edge.N4=> 9.25000E-02,
						 Edge.N5=> 8.99000E-02,
						 Edge.O1=> 3.91000E-02,
						 Edge.O2=> 1.66000E-02,
						 Edge.O3=> 1.46000E-02,
						_=> double.NaN,
					};
				case 57:
					return line switch
					{
						 Edge.K=> 3.89246E+01,
						 Edge.L1=> 6.26630E+00,
						 Edge.L2=> 5.89060E+00,
						 Edge.L3=> 5.48270E+00,
						 Edge.M1=> 1.36130E+00,
						 Edge.M2=> 1.20440E+00,
						 Edge.M3=> 1.12340E+00,
						 Edge.M4=> 8.48500E-01,
						 Edge.M5=> 8.31700E-01,
						 Edge.N1=> 2.70400E-01,
						 Edge.N2=> 2.05800E-01,
						 Edge.N3=> 1.91400E-01,
						 Edge.N4=> 9.89000E-02,
						 Edge.N5=> 9.89000E-02,
						 Edge.O1=> 3.23000E-02,
						 Edge.O2=> 1.44000E-02,
						 Edge.O3=> 1.44000E-02,
						_=> double.NaN,
					};
				case 58:
					return line switch
					{
						 Edge.K=> 4.04430E+01,
						 Edge.L1=> 6.54880E+00,
						 Edge.L2=> 6.16420E+00,
						 Edge.L3=> 5.72340E+00,
						 Edge.M1=> 1.43460E+00,
						 Edge.M2=> 1.27280E+00,
						 Edge.M3=> 1.18540E+00,
						 Edge.M4=> 9.01300E-01,
						 Edge.M5=> 8.83300E-01,
						 Edge.N1=> 2.89600E-01,
						 Edge.N2=> 2.23300E-01,
						 Edge.N3=> 2.07200E-01,
						 Edge.N4=> 1.10000E-01,
						 Edge.N5=> 1.10000E-01,
						 Edge.N6=> 8.59000E-02,
						 Edge.O1=> 3.78000E-02,
						 Edge.O2=> 1.98000E-02,
						 Edge.O3=> 1.98000E-02,
						_=> double.NaN,
					};
				case 59:
					return line switch
					{
						 Edge.K=> 4.19906E+01,
						 Edge.L1=> 6.83480E+00,
						 Edge.L2=> 6.44040E+00,
						 Edge.L3=> 5.96430E+00,
						 Edge.M1=> 1.51100E+00,
						 Edge.M2=> 1.33740E+00,
						 Edge.M3=> 1.24220E+00,
						 Edge.M4=> 9.51100E-01,
						 Edge.M5=> 9.31000E-01,
						 Edge.N1=> 3.04500E-01,
						 Edge.N2=> 2.36300E-01,
						 Edge.N3=> 2.17600E-01,
						 Edge.N4=> 1.13200E-01,
						 Edge.N5=> 1.13200E-01,
						 Edge.N6=> 3.50000E-03,
						 Edge.O1=> 3.74000E-02,
						 Edge.O2=> 2.23000E-02,
						 Edge.O3=> 2.23000E-02,
						_=> double.NaN,
					};
				case 60:
					return line switch
					{
						 Edge.K=> 4.35689E+01,
						 Edge.L1=> 7.12600E+00,
						 Edge.L2=> 6.72150E+00,
						 Edge.L3=> 6.20790E+00,
						 Edge.M1=> 1.57530E+00,
						 Edge.M2=> 1.40280E+00,
						 Edge.M3=> 1.29740E+00,
						 Edge.M4=> 9.99500E-01,
						 Edge.M5=> 9.77700E-01,
						 Edge.N1=> 3.15200E-01,
						 Edge.N2=> 2.43300E-01,
						 Edge.N3=> 2.24600E-01,
						 Edge.N4=> 1.17500E-01,
						 Edge.N5=> 1.17500E-01,
						 Edge.N6=> 3.00000E-03,
						 Edge.O1=> 3.75000E-02,
						 Edge.O2=> 2.11000E-02,
						 Edge.O3=> 2.11000E-02,
						_=> double.NaN,
					};
				case 61:
					return line switch
					{
						 Edge.K=> 4.51840E+01,
						 Edge.L1=> 7.42790E+00,
						 Edge.L2=> 7.01280E+00,
						 Edge.L3=> 6.45930E+00,
						 Edge.M1=> 1.64650E+00,
						 Edge.M2=> 1.47140E+00,
						 Edge.M3=> 1.35690E+00,
						 Edge.M4=> 1.05150E+00,
						 Edge.M5=> 1.02690E+00,
						 Edge.N1=> 3.30400E-01,
						 Edge.N2=> 2.54400E-01,
						 Edge.N3=> 2.36000E-01,
						 Edge.N4=> 1.20400E-01,
						 Edge.N5=> 1.20400E-01,
						 Edge.N6=> 4.00000E-03,
						 Edge.O1=> 3.75000E-02,
						 Edge.O2=> 2.11000E-02,
						 Edge.O3=> 2.11000E-02,
						_=> double.NaN,
					};
				case 62:
					return line switch
					{
						 Edge.K=> 4.68342E+01,
						 Edge.L1=> 7.73680E+00,
						 Edge.L2=> 7.31180E+00,
						 Edge.L3=> 6.71620E+00,
						 Edge.M1=> 1.72280E+00,
						 Edge.M2=> 1.54070E+00,
						 Edge.M3=> 1.41980E+00,
						 Edge.M4=> 1.10600E+00,
						 Edge.M5=> 1.08020E+00,
						 Edge.N1=> 3.45700E-01,
						 Edge.N2=> 2.65600E-01,
						 Edge.N3=> 2.47400E-01,
						 Edge.N4=> 1.29000E-01,
						 Edge.N5=> 1.29000E-01,
						 Edge.N6=> 5.50000E-03,
						 Edge.O1=> 3.74000E-02,
						 Edge.O2=> 2.13000E-02,
						 Edge.O3=> 2.13000E-02,
						_=> double.NaN,
					};
				case 63:
					return line switch
					{
						 Edge.K=> 4.85190E+01,
						 Edge.L1=> 8.05200E+00,
						 Edge.L2=> 7.61710E+00,
						 Edge.L3=> 6.97690E+00,
						 Edge.M1=> 1.80000E+00,
						 Edge.M2=> 1.61390E+00,
						 Edge.M3=> 1.48060E+00,
						 Edge.M4=> 1.16060E+00,
						 Edge.M5=> 1.13090E+00,
						 Edge.N1=> 3.60200E-01,
						 Edge.N2=> 2.83900E-01,
						 Edge.N3=> 2.56600E-01,
						 Edge.N4=> 1.33200E-01,
						 Edge.N5=> 1.33200E-01,
						 Edge.N6=> 2.91151E-03,
						 Edge.O1=> 3.18000E-02,
						 Edge.O2=> 2.20000E-02,
						 Edge.O3=> 2.20000E-02,
						_=> double.NaN,
					};
				case 64:
					return line switch
					{
						 Edge.K=> 5.02391E+01,
						 Edge.L1=> 8.37560E+00,
						 Edge.L2=> 7.93030E+00,
						 Edge.L3=> 7.24280E+00,
						 Edge.M1=> 1.88080E+00,
						 Edge.M2=> 1.68830E+00,
						 Edge.M3=> 1.54400E+00,
						 Edge.M4=> 1.21720E+00,
						 Edge.M5=> 1.18520E+00,
						 Edge.N1=> 3.75800E-01,
						 Edge.N2=> 2.88500E-01,
						 Edge.N3=> 2.70900E-01,
						 Edge.N4=> 1.40500E-01,
						 Edge.N5=> 1.40500E-01,
						 Edge.N6=> 9.27940E-03,
						 Edge.N7=> 8.52419E-03,
						 Edge.O1=> 3.61000E-02,
						 Edge.O2=> 2.03000E-02,
						 Edge.O3=> 2.03000E-02,
						_=> double.NaN,
					};
				case 65:
					return line switch
					{
						 Edge.K=> 5.19957E+01,
						 Edge.L1=> 8.70800E+00,
						 Edge.L2=> 8.25160E+00,
						 Edge.L3=> 7.51400E+00,
						 Edge.M1=> 1.96750E+00,
						 Edge.M2=> 1.76770E+00,
						 Edge.M3=> 1.61130E+00,
						 Edge.M4=> 1.27500E+00,
						 Edge.M5=> 1.24120E+00,
						 Edge.N1=> 3.97900E-01,
						 Edge.N2=> 3.10200E-01,
						 Edge.N3=> 2.85000E-01,
						 Edge.N4=> 1.47000E-01,
						 Edge.N5=> 1.47000E-01,
						 Edge.N6=> 9.40000E-03,
						 Edge.N7=> 8.60000E-03,
						 Edge.O1=> 3.90000E-02,
						 Edge.O2=> 2.54000E-02,
						 Edge.O3=> 2.54000E-02,
						_=> double.NaN,
					};
				case 66:
					return line switch
					{
						 Edge.K=> 5.37885E+01,
						 Edge.L1=> 9.04580E+00,
						 Edge.L2=> 8.58060E+00,
						 Edge.L3=> 7.79010E+00,
						 Edge.M1=> 2.04680E+00,
						 Edge.M2=> 1.84180E+00,
						 Edge.M3=> 1.67560E+00,
						 Edge.M4=> 1.33250E+00,
						 Edge.M5=> 1.29490E+00,
						 Edge.N1=> 4.16300E-01,
						 Edge.N2=> 3.31800E-01,
						 Edge.N3=> 2.92900E-01,
						 Edge.N4=> 1.54200E-01,
						 Edge.N5=> 1.54200E-01,
						 Edge.N6=> 4.20000E-03,
						 Edge.N7=> 4.20000E-03,
						 Edge.O1=> 6.29000E-02,
						 Edge.O2=> 2.63000E-02,
						 Edge.O3=> 2.63000E-02,
						_=> double.NaN,
					};
				case 67:
					return line switch
					{
						 Edge.K=> 5.56177E+01,
						 Edge.L1=> 9.39420E+00,
						 Edge.L2=> 8.91780E+00,
						 Edge.L3=> 8.07110E+00,
						 Edge.M1=> 2.12830E+00,
						 Edge.M2=> 1.92280E+00,
						 Edge.M3=> 1.74120E+00,
						 Edge.M4=> 1.39150E+00,
						 Edge.M5=> 1.35140E+00,
						 Edge.N1=> 4.35700E-01,
						 Edge.N2=> 3.43500E-01,
						 Edge.N3=> 3.06600E-01,
						 Edge.N4=> 1.61000E-01,
						 Edge.N5=> 1.61000E-01,
						 Edge.N6=> 3.70000E-03,
						 Edge.N7=> 3.70000E-03,
						 Edge.O1=> 5.12000E-02,
						 Edge.O2=> 2.03000E-02,
						 Edge.O3=> 2.03000E-02,
						_=> double.NaN,
					};
				case 68:
					return line switch
					{
						 Edge.K=> 5.74855E+01,
						 Edge.L1=> 9.75130E+00,
						 Edge.L2=> 9.26430E+00,
						 Edge.L3=> 8.35790E+00,
						 Edge.M1=> 2.20650E+00,
						 Edge.M2=> 2.00580E+00,
						 Edge.M3=> 1.81180E+00,
						 Edge.M4=> 1.45330E+00,
						 Edge.M5=> 1.40930E+00,
						 Edge.N1=> 4.49100E-01,
						 Edge.N2=> 3.66200E-01,
						 Edge.N3=> 3.20000E-01,
						 Edge.N4=> 1.76700E-01,
						 Edge.N5=> 1.67600E-01,
						 Edge.N6=> 4.30000E-03,
						 Edge.N7=> 4.30000E-03,
						 Edge.O1=> 5.98000E-02,
						 Edge.O2=> 2.94000E-02,
						 Edge.O3=> 2.94000E-02,
						_=> double.NaN,
					};
				case 69:
					return line switch
					{
						Edge.K => 5.93896E+01,
						Edge.L1 => 1.01157E+01,
						Edge.L2 => 9.61690E+00,
						Edge.L3 => 8.64800E+00,
						Edge.M1 => 2.30680E+00,
						Edge.M2 => 2.08980E+00,
						Edge.M3 => 1.88450E+00,
						Edge.M4 => 1.51460E+00,
						Edge.M5 => 1.46770E+00,
						Edge.N1 => 4.71700E-01,
						Edge.N2 => 3.85900E-01,
						Edge.N3 => 3.36600E-01,
						Edge.N4 => 1.79600E-01,
						Edge.N5 => 1.79600E-01,
						Edge.N6 => 5.30000E-03,
						Edge.N7 => 5.30000E-03,
						Edge.O1 => 5.32000E-02,
						Edge.O2 => 3.23000E-02,
						Edge.O3 => 3.23000E-02,
						_ => double.NaN,
					};
				case 70:
					return line switch
					{
						Edge.K => 6.13323E+01,
						Edge.L1 => 1.04864E+01,
						Edge.L2 => 9.97820E+00,
						Edge.L3 => 8.94360E+00,
						Edge.M1 => 2.39810E+00,
						Edge.M2 => 2.17300E+00,
						Edge.M3 => 1.94980E+00,
						Edge.M4 => 1.57630E+00,
						Edge.M5 => 1.52780E+00,
						Edge.N1 => 4.87200E-01,
						Edge.N2 => 3.96700E-01,
						Edge.N3 => 3.43500E-01,
						Edge.N4 => 1.98100E-01,
						Edge.N5 => 1.84900E-01,
						Edge.N6 => 6.30000E-03,
						Edge.N7 => 6.30000E-03,
						Edge.O1 => 5.41000E-02,
						Edge.O2 => 2.34000E-02,
						Edge.O3 => 2.34000E-02,
						_ => double.NaN,
					};
				case 71:
					return line switch
					{
						Edge.K => 6.33138E+01,
						Edge.L1 => 1.08704E+01,
						Edge.L2 => 1.03486E+01,
						Edge.L3 => 9.24410E+00,
						Edge.M1 => 2.49120E+00,
						Edge.M2 => 2.26350E+00,
						Edge.M3 => 2.02360E+00,
						Edge.M4 => 1.63940E+00,
						Edge.M5 => 1.58850E+00,
						Edge.N1 => 5.06200E-01,
						Edge.N2 => 4.10100E-01,
						Edge.N3 => 3.59300E-01,
						Edge.N4 => 2.04800E-01,
						Edge.N5 => 1.95000E-01,
						Edge.N6 => 6.90000E-03,
						Edge.N7 => 6.90000E-03,
						Edge.O1 => 5.68000E-02,
						Edge.O2 => 2.80000E-02,
						Edge.O3 => 2.80000E-02,
						_ => double.NaN,
					};
				case 72:
					return line switch
					{
						Edge.K => 6.53508E+01,
						Edge.L1 => 1.12707E+01,
						Edge.L2 => 1.07394E+01,
						Edge.L3 => 9.56070E+00,
						Edge.M1 => 2.60090E+00,
						Edge.M2 => 2.36540E+00,
						Edge.M3 => 2.10760E+00,
						Edge.M4 => 1.71640E+00,
						Edge.M5 => 1.66170E+00,
						Edge.N1 => 5.38100E-01,
						Edge.N2 => 4.37000E-01,
						Edge.N3 => 3.80400E-01,
						Edge.N4 => 2.23800E-01,
						Edge.N5 => 2.13700E-01,
						Edge.N6 => 1.71000E-02,
						Edge.N7 => 1.71000E-02,
						Edge.O1 => 6.49000E-02,
						Edge.O2 => 3.81000E-02,
						Edge.O3 => 3.06000E-02,
						Edge.O4 => 5.00000E-03,
						_ => double.NaN,
					};
				case 73:
					return line switch
					{
						Edge.K => 6.74164E+01,
						Edge.L1 => 1.16815E+01,
						Edge.L2 => 1.11361E+01,
						Edge.L3 => 9.88110E+00,
						Edge.M1 => 2.70800E+00,
						Edge.M2 => 2.46870E+00,
						Edge.M3 => 2.19400E+00,
						Edge.M4 => 1.79320E+00,
						Edge.M5 => 1.73510E+00,
						Edge.N1 => 5.65500E-01,
						Edge.N2 => 4.64800E-01,
						Edge.N3 => 4.04500E-01,
						Edge.N4 => 2.41300E-01,
						Edge.N5 => 2.29300E-01,
						Edge.N6 => 2.50000E-02,
						Edge.N7 => 2.50000E-02,
						Edge.O1 => 7.11000E-02,
						Edge.O2 => 4.49000E-02,
						Edge.O3 => 3.64000E-02,
						Edge.O4 => 5.70000E-03,
						_ => double.NaN,
					};
				case 74:
					return line switch
					{
						Edge.K => 6.95250E+01,
						Edge.L1 => 1.20998E+01,
						Edge.L2 => 1.15440E+01,
						Edge.L3 => 1.02068E+01,
						Edge.M1 => 2.81960E+00,
						Edge.M2 => 2.57490E+00,
						Edge.M3 => 2.28100E+00,
						Edge.M4 => 1.87160E+00,
						Edge.M5 => 1.80920E+00,
						Edge.N1 => 5.95000E-01,
						Edge.N2 => 4.91600E-01,
						Edge.N3 => 4.25300E-01,
						Edge.N4 => 2.58800E-01,
						Edge.N5 => 2.45400E-01,
						Edge.N6 => 3.65000E-02,
						Edge.N7 => 3.36000E-02,
						Edge.O1 => 7.71000E-02,
						Edge.O2 => 4.68000E-02,
						Edge.O3 => 3.56000E-02,
						Edge.O4 => 6.10000E-03,
						_ => double.NaN,
					};
				case 75:
					return line switch
					{
						Edge.K => 7.16764E+01,
						Edge.L1 => 1.25267E+01,
						Edge.L2 => 1.19587E+01,
						Edge.L3 => 1.05353E+01,
						Edge.M1 => 2.93170E+00,
						Edge.M2 => 2.68160E+00,
						Edge.M3 => 2.36730E+00,
						Edge.M4 => 1.94890E+00,
						Edge.M5 => 1.88290E+00,
						Edge.N1 => 6.25000E-01,
						Edge.N2 => 5.17900E-01,
						Edge.N3 => 4.44400E-01,
						Edge.N4 => 2.73700E-01,
						Edge.N5 => 2.60200E-01,
						Edge.N6 => 4.06000E-02,
						Edge.N7 => 4.06000E-02,
						Edge.O1 => 8.28000E-02,
						Edge.O2 => 4.56000E-02,
						Edge.O3 => 3.46000E-02,
						Edge.O4 => 6.06267E-03,
						Edge.O5 => 5.20913E-03,
						_ => double.NaN,
					};
				case 76:
					return line switch
					{
						Edge.K => 7.38708E+01,
						Edge.L1 => 1.29680E+01,
						Edge.L2 => 1.23850E+01,
						Edge.L3 => 1.08709E+01,
						Edge.M1 => 3.04850E+00,
						Edge.M2 => 2.79220E+00,
						Edge.M3 => 2.45720E+00,
						Edge.M4 => 2.03080E+00,
						Edge.M5 => 1.96010E+00,
						Edge.N1 => 6.54300E-01,
						Edge.N2 => 5.46500E-01,
						Edge.N3 => 4.68200E-01,
						Edge.N4 => 2.89400E-01,
						Edge.N5 => 2.72800E-01,
						Edge.N6 => 4.63000E-02,
						Edge.N7 => 4.63000E-02,
						Edge.O1 => 8.37000E-02,
						Edge.O2 => 5.80000E-02,
						Edge.O3 => 4.54000E-02,
						Edge.O4 => 7.05265E-03,
						Edge.O5 => 6.02794E-03,
						_ => double.NaN,
					};
				case 77:
					return line switch
					{
						 Edge.K=> 7.61110E+01,
						 Edge.L1=> 1.34185E+01,
						 Edge.L2=> 1.28241E+01,
						 Edge.L3=> 1.12152E+01,
						 Edge.M1=> 3.17370E+00,
						 Edge.M2=> 2.90870E+00,
						 Edge.M3=> 2.55070E+00,
						 Edge.M4=> 2.11610E+00,
						 Edge.M5=> 2.04040E+00,
						 Edge.N1=> 6.90100E-01,
						 Edge.N2=> 5.77100E-01,
						 Edge.N3=> 4.94300E-01,
						 Edge.N4=> 3.11400E-01,
						 Edge.N5=> 2.94900E-01,
						 Edge.N6=> 6.34000E-02,
						 Edge.N7=> 6.05000E-02,
						 Edge.O1=> 9.52000E-02,
						 Edge.O2=> 6.30000E-02,
						 Edge.O3=> 5.05000E-02,
						 Edge.O4=> 8.06275E-03,
						 Edge.O5=> 6.85456E-03,
						_=> double.NaN,
					};
				case 78:
					return line switch
					{
						 Edge.K=> 7.83948E+01,
						 Edge.L1=> 1.38799E+01,
						 Edge.L2=> 1.32726E+01,
						 Edge.L3=> 1.15637E+01,
						 Edge.M1=> 3.29600E+00,
						 Edge.M2=> 3.02650E+00,
						 Edge.M3=> 2.64540E+00,
						 Edge.M4=> 2.20190E+00,
						 Edge.M5=> 2.12160E+00,
						 Edge.N1=> 7.22000E-01,
						 Edge.N2=> 6.09200E-01,
						 Edge.N3=> 5.19000E-01,
						 Edge.N4=> 3.30800E-01,
						 Edge.N5=> 3.13300E-01,
						 Edge.N6=> 7.43000E-02,
						 Edge.N7=> 7.11000E-02,
						 Edge.O1=> 1.01700E-01,
						 Edge.O2=> 6.53000E-02,
						 Edge.O3=> 5.17000E-02,
						 Edge.O4=> 7.43991E-03,
						 Edge.O5=> 6.12538E-03,
						_=> double.NaN,
					};
				case 79:
					return line switch
					{
						Edge.K => 8.07249E+01,
						Edge.L1 => 1.43528E+01,
						Edge.L2 => 1.37336E+01,
						Edge.L3 => 1.19187E+01,
						Edge.M1 => 3.42490E+00,
						Edge.M2 => 3.14780E+00,
						Edge.M3 => 2.74300E+00,
						Edge.M4 => 2.29110E+00,
						Edge.M5 => 2.20570E+00,
						Edge.N1 => 7.58800E-01,
						Edge.N2 => 6.43700E-01,
						Edge.N3 => 5.45400E-01,
						Edge.N4 => 3.52000E-01,
						Edge.N5 => 3.33900E-01,
						Edge.N6 => 8.64000E-02,
						Edge.N7 => 8.28000E-02,
						Edge.O1 => 1.07800E-01,
						Edge.O2 => 7.17000E-02,
						Edge.O3 => 5.37000E-02,
						Edge.O4 => 8.30838E-03,
						Edge.O5 => 6.79032E-03,
						_ => double.NaN,
					};
				case 80:
					return line switch
					{
						Edge.K => 8.31023E+01,
						Edge.L1 => 1.48393E+01,
						Edge.L2 => 1.42087E+01,
						Edge.L3 => 1.22839E+01,
						Edge.M1 => 3.56160E+00,
						Edge.M2 => 3.27850E+00,
						Edge.M3 => 2.84710E+00,
						Edge.M4 => 2.38490E+00,
						Edge.M5 => 2.29490E+00,
						Edge.N1 => 8.00300E-01,
						Edge.N2 => 6.76900E-01,
						Edge.N3 => 5.71000E-01,
						Edge.N4 => 3.78300E-01,
						Edge.N5 => 3.59800E-01,
						Edge.N6 => 1.02200E-01,
						Edge.N7 => 9.85000E-02,
						Edge.O1 => 1.20300E-01,
						Edge.O2 => 8.05000E-02,
						Edge.O3 => 5.76000E-02,
						Edge.O4 => 6.40000E-03,
						Edge.O5 => 6.40000E-03,
						Edge.P1 => 7.71361E-03,
						_ => double.NaN,
					};
				case 81:
					return line switch
					{
						Edge.K => 8.55304E+01,
						Edge.L1 => 1.53467E+01,
						Edge.L2 => 1.46979E+01,
						Edge.L3 => 1.26575E+01,
						Edge.M1 => 3.70410E+00,
						Edge.M2 => 3.41570E+00,
						Edge.M3 => 2.95660E+00,
						Edge.M4 => 2.48510E+00,
						Edge.M5 => 2.38930E+00,
						Edge.N1 => 8.45500E-01,
						Edge.N2 => 7.21300E-01,
						Edge.N3 => 6.09000E-01,
						Edge.N4 => 4.06600E-01,
						Edge.N5 => 3.86200E-01,
						Edge.N6 => 1.22800E-01,
						Edge.N7 => 1.18500E-01,
						Edge.O1 => 1.36300E-01,
						Edge.O2 => 9.96000E-02,
						Edge.O3 => 7.54000E-02,
						Edge.O4 => 1.53000E-02,
						Edge.O5 => 1.31000E-02,
						Edge.P1 => 9.66483E-03,
						_ => double.NaN,
					};
				case 82:
					return line switch
					{
						Edge.K => 8.80045E+01,
						Edge.L1 => 1.58608E+01,
						Edge.L2 => 1.52000E+01,
						Edge.L3 => 1.30352E+01,
						Edge.M1 => 3.85070E+00,
						Edge.M2 => 3.55420E+00,
						Edge.M3 => 3.06640E+00,
						Edge.M4 => 2.58560E+00,
						Edge.M5 => 2.48400E+00,
						Edge.N1 => 8.93600E-01,
						Edge.N2 => 7.63900E-01,
						Edge.N3 => 6.44500E-01,
						Edge.N4 => 4.35200E-01,
						Edge.N5 => 4.12900E-01,
						Edge.N6 => 1.42900E-01,
						Edge.N7 => 1.38100E-01,
						Edge.O1 => 1.47300E-01,
						Edge.O2 => 1.04800E-01,
						Edge.O3 => 8.60000E-02,
						Edge.O4 => 2.18000E-02,
						Edge.O5 => 1.92000E-02,
						Edge.P1 => 1.16904E-02,
						Edge.P2 => 4.91166E-03,
						_ => double.NaN,
					};
				case 83:
					return line switch
					{
						Edge.K => 9.05259E+01,
						Edge.L1 => 1.63875E+01,
						Edge.L2 => 1.57111E+01,
						Edge.L3 => 1.34186E+01,
						Edge.M1 => 3.99910E+00,
						Edge.M2 => 3.69630E+00,
						Edge.M3 => 3.17690E+00,
						Edge.M4 => 2.68760E+00,
						Edge.M5 => 2.57960E+00,
						Edge.N1 => 9.38200E-01,
						Edge.N2 => 8.05300E-01,
						Edge.N3 => 6.78900E-01,
						Edge.N4 => 4.63600E-01,
						Edge.N5 => 4.40000E-01,
						Edge.N6 => 1.61900E-01,
						Edge.N7 => 1.57400E-01,
						Edge.O1 => 1.59300E-01,
						Edge.O2 => 1.16800E-01,
						Edge.O3 => 9.28000E-02,
						Edge.O4 => 2.65000E-02,
						Edge.O5 => 2.44000E-02,
						Edge.P1 => 1.42334E-02,
						Edge.P2 => 6.16991E-03,
						_ => double.NaN,
					};
				case 84:
					return line switch
					{
						Edge.K => 9.31050E+01,
						Edge.L1 => 1.69393E+01,
						Edge.L2 => 1.62443E+01,
						Edge.L3 => 1.38138E+01,
						Edge.M1 => 4.14940E+00,
						Edge.M2 => 3.85410E+00,
						Edge.M3 => 3.30190E+00,
						Edge.M4 => 2.79800E+00,
						Edge.M5 => 2.68300E+00,
						Edge.N1 => 9.95300E-01,
						Edge.N2 => 8.51000E-01,
						Edge.N3 => 7.05000E-01,
						Edge.N4 => 5.00200E-01,
						Edge.N5 => 4.73400E-01,
						Edge.N6 => 1.75344E-01,
						Edge.N7 => 1.69362E-01,
						Edge.O1 => 1.70906E-01,
						Edge.O2 => 1.25695E-01,
						Edge.O3 => 9.83141E-02,
						Edge.O4 => 3.14000E-02,
						Edge.O5 => 3.14000E-02,
						Edge.P1 => 1.67777E-02,
						Edge.P2 => 7.55974E-03,
						Edge.P3 => 5.39477E-03,
						_ => double.NaN,
					};
				case 85:
					return line switch
					{
						Edge.K => 9.57299E+01,
						Edge.L1 => 1.74930E+01,
						Edge.L2 => 1.67847E+01,
						Edge.L3 => 1.42135E+01,
						Edge.M1 => 4.31700E+00,
						Edge.M2 => 4.00800E+00,
						Edge.M3 => 3.42600E+00,
						Edge.M4 => 2.90870E+00,
						Edge.M5 => 2.78670E+00,
						Edge.N1 => 1.04200E+00,
						Edge.N2 => 8.86000E-01,
						Edge.N3 => 7.40000E-01,
						Edge.N4 => 5.33200E-01,
						Edge.N5 => 4.75385E-01,
						Edge.N6 => 1.97076E-01,
						Edge.N7 => 1.90577E-01,
						Edge.O1 => 1.85617E-01,
						Edge.O2 => 1.38499E-01,
						Edge.O3 => 1.08426E-01,
						Edge.O4 => 4.15942E-02,
						Edge.O5 => 3.76618E-02,
						Edge.P1 => 1.93390E-02,
						Edge.P2 => 9.03104E-03,
						Edge.P3 => 6.24450E-03,
						_ => double.NaN,
					};
				case 86:
					return line switch
					{
						Edge.K => 9.84040E+01,
						Edge.L1 => 1.80490E+01,
						Edge.L2 => 1.73371E+01,
						Edge.L3 => 1.46194E+01,
						Edge.M1 => 4.48200E+00,
						Edge.M2 => 4.15900E+00,
						Edge.M3 => 3.53800E+00,
						Edge.M4 => 3.02150E+00,
						Edge.M5 => 2.89240E+00,
						Edge.N1 => 1.09700E+00,
						Edge.N2 => 9.29000E-01,
						Edge.N3 => 7.68000E-01,
						Edge.N4 => 5.66600E-01,
						Edge.N5 => 5.37000E-01,
						Edge.N6 => 2.19631E-01,
						Edge.N7 => 2.12588E-01,
						Edge.O1 => 2.00831E-01,
						Edge.O2 => 1.51771E-01,
						Edge.O3 => 1.18817E-01,
						Edge.O4 => 4.86911E-02,
						Edge.O5 => 4.42550E-02,
						Edge.P1 => 2.19397E-02,
						Edge.P2 => 1.05726E-02,
						Edge.P3 => 7.12588E-03,
						_ => double.NaN,
					};
				case 87:
					return line switch
					{
						Edge.K => 1.01137E+02,
						Edge.L1 => 1.86390E+01,
						Edge.L2 => 1.79065E+01,
						Edge.L3 => 1.50312E+01,
						Edge.M1 => 4.65200E+00,
						Edge.M2 => 4.32700E+00,
						Edge.M3 => 3.66300E+00,
						Edge.M4 => 3.13620E+00,
						Edge.M5 => 2.99970E+00,
						Edge.N1 => 1.15300E+00,
						Edge.N2 => 9.80000E-01,
						Edge.N3 => 8.10000E-01,
						Edge.N4 => 6.03300E-01,
						Edge.N5 => 5.77000E-01,
						Edge.N6 => 2.46488E-01,
						Edge.N7 => 2.38863E-01,
						Edge.O1 => 2.20035E-01,
						Edge.O2 => 1.69009E-01,
						Edge.O3 => 1.32957E-01,
						Edge.O4 => 5.95378E-02,
						Edge.O5 => 5.45529E-02,
						Edge.P1 => 2.78679E-02,
						Edge.P2 => 1.51650E-02,
						Edge.P3 => 1.06123E-02,
						_ => double.NaN,
					};
				case 88:
					return line switch
					{
						Edge.K => 1.03922E+02,
						Edge.L1 => 1.92367E+01,
						Edge.L2 => 1.84843E+01,
						Edge.L3 => 1.54444E+01,
						Edge.M1 => 4.82200E+00,
						Edge.M2 => 4.48950E+00,
						Edge.M3 => 3.79180E+00,
						Edge.M4 => 3.24840E+00,
						Edge.M5 => 3.10490E+00,
						Edge.N1 => 1.20840E+00,
						Edge.N2 => 1.05760E+00,
						Edge.N3 => 8.79100E-01,
						Edge.N4 => 6.35900E-01,
						Edge.N5 => 6.02700E-01,
						Edge.N6 => 2.98900E-01,
						Edge.N7 => 2.98900E-01,
						Edge.O1 => 2.54400E-01,
						Edge.O2 => 2.00400E-01,
						Edge.O3 => 1.52800E-01,
						Edge.O4 => 6.72000E-02,
						Edge.O5 => 6.72000E-02,
						Edge.P1 => 4.35000E-02,
						Edge.P2 => 1.88000E-02,
						Edge.P3 => 1.88000E-02,
						_ => double.NaN,
					};
				case 89:
					return line switch
					{
						Edge.K => 1.06755E+02,
						Edge.L1 => 1.98400E+01,
						Edge.L2 => 1.90832E+01,
						Edge.L3 => 1.58710E+01,
						Edge.M1 => 5.00200E+00,
						Edge.M2 => 4.65600E+00,
						Edge.M3 => 3.90900E+00,
						Edge.M4 => 3.37020E+00,
						Edge.M5 => 3.21900E+00,
						Edge.N1 => 1.26900E+00,
						Edge.N2 => 1.08000E+00,
						Edge.N3 => 8.90000E-01,
						Edge.N4 => 6.74900E-01,
						Edge.N5 => 6.37000E-01,
						Edge.N6 => 3.03944E-01,
						Edge.N7 => 2.95067E-01,
						Edge.O1 => 2.61255E-01,
						Edge.O2 => 2.06171E-01,
						Edge.O3 => 1.63235E-01,
						Edge.O4 => 8.31361E-02,
						Edge.O5 => 7.69389E-02,
						Edge.P1 => 4.04636E-02,
						Edge.P2 => 2.51851E-02,
						Edge.P3 => 1.84021E-02,
						_ => double.NaN,
					};
				case 90:
					return line switch
					{
						Edge.K => 1.09651E+02,
						Edge.L1 => 2.04721E+01,
						Edge.L2 => 1.96932E+01,
						Edge.L3 => 1.63003E+01,
						Edge.M1 => 5.18230E+00,
						Edge.M2 => 4.83040E+00,
						Edge.M3 => 4.04610E+00,
						Edge.M4 => 3.49080E+00,
						Edge.M5 => 3.33200E+00,
						Edge.N1 => 1.32950E+00,
						Edge.N2 => 1.16820E+00,
						Edge.N3 => 9.67300E-01,
						Edge.N4 => 7.14100E-01,
						Edge.N5 => 6.76400E-01,
						Edge.N6 => 3.44400E-01,
						Edge.N7 => 3.35200E-01,
						Edge.O1 => 2.90200E-01,
						Edge.O2 => 2.29400E-01,
						Edge.O3 => 1.81800E-01,
						Edge.O4 => 9.43000E-02,
						Edge.O5 => 8.79000E-02,
						Edge.P1 => 5.95000E-02,
						Edge.P2 => 4.90000E-02,
						Edge.P3 => 4.30000E-02,
						_ => double.NaN,
					};
				case 91:
					return line switch
					{
						Edge.K => 1.12601E+02,
						Edge.L1 => 2.11046E+01,
						Edge.L2 => 2.03137E+01,
						Edge.L3 => 1.67331E+01,
						Edge.M1 => 5.36690E+00,
						Edge.M2 => 5.00090E+00,
						Edge.M3 => 4.17380E+00,
						Edge.M4 => 3.61120E+00,
						Edge.M5 => 3.44180E+00,
						Edge.N1 => 1.38710E+00,
						Edge.N2 => 1.22430E+00,
						Edge.N3 => 1.00670E+00,
						Edge.N4 => 7.43400E-01,
						Edge.N5 => 7.08200E-01,
						Edge.N6 => 3.71200E-01,
						Edge.N7 => 3.59500E-01,
						Edge.O1 => 3.09600E-01,
						Edge.O2 => 2.33624E-01,
						Edge.O3 => 1.83050E-01,
						Edge.O4 => 9.66789E-02,
						Edge.O5 => 8.92408E-02,
						Edge.P1 => 4.54585E-02,
						Edge.P2 => 2.85451E-02,
						Edge.P3 => 2.03206E-02,
						_ => double.NaN,
					};
				case 92:
					return line switch
					{
						Edge.K => 1.15606E+02,
						Edge.L1 => 2.17574E+01,
						Edge.L2 => 2.09476E+01,
						Edge.L3 => 1.71663E+01,
						Edge.M1 => 5.54800E+00,
						Edge.M2 => 5.18220E+00,
						Edge.M3 => 4.30340E+00,
						Edge.M4 => 3.72760E+00,
						Edge.M5 => 3.55170E+00,
						Edge.N1 => 1.44080E+00,
						Edge.N2 => 1.27260E+00,
						Edge.N3 => 1.04490E+00,
						Edge.N4 => 7.80400E-01,
						Edge.N5 => 7.37700E-01,
						Edge.N6 => 3.91300E-01,
						Edge.N7 => 3.80900E-01,
						Edge.O1 => 3.23700E-01,
						Edge.O2 => 2.59300E-01,
						Edge.O3 => 1.95100E-01,
						Edge.O4 => 1.05000E-01,
						Edge.O5 => 9.63000E-02,
						Edge.P1 => 7.07000E-02,
						Edge.P2 => 4.23000E-02,
						Edge.P3 => 3.23000E-02,
						_ => double.NaN,
					};
				default: return double.NaN;
			}
			#endregion
		}

		/// <summary>
		/// 原子番号 z, 線種 line を入力すると 波長 (Å) を返す。 対応する原子、線種がない場合はNaNを返す
		/// </summary>
		/// <param name="z"></param>
		/// <param name="line"></param>
		/// <returns></returns>
		public static double CharacteristicXrayWavelength(int z, XrayLine line)
		{
			if (line == XrayLine.Ka)
			{
				double ka1 = CharacteristicXrayWavelength(z, XrayLine.Ka1);
				double ka2 = CharacteristicXrayWavelength(z, XrayLine.Ka2);
				if (double.IsNaN(ka2))
					return ka1;
				else
					return (2 * ka1 + ka2) / 3;
			}

			#region
			switch (z)
			{
				case 0:
					switch (line)
					{
						case XrayLine.Ka1: return double.NaN;
						case XrayLine.Ka2: return double.NaN;
						case XrayLine.Kb1: return double.NaN;
						case XrayLine.Kb3: return double.NaN;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return double.NaN;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 1:
					switch (line)
					{
						case XrayLine.Ka1: return double.NaN;
						case XrayLine.Ka2: return double.NaN;
						case XrayLine.Kb1: return double.NaN;
						case XrayLine.Kb3: return double.NaN;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 918;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 2:
					switch (line)
					{
						case XrayLine.Ka1: return double.NaN;
						case XrayLine.Ka2: return double.NaN;
						case XrayLine.Kb1: return double.NaN;
						case XrayLine.Kb3: return double.NaN;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 504;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 3:
					switch (line)
					{
						case XrayLine.Ka1: return 228;
						case XrayLine.Ka2: return double.NaN;
						case XrayLine.Kb1: return double.NaN;
						case XrayLine.Kb3: return double.NaN;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 226.953;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 4:
					switch (line)
					{
						case XrayLine.Ka1: return 114;
						case XrayLine.Ka2: return double.NaN;
						case XrayLine.Kb1: return double.NaN;
						case XrayLine.Kb3: return double.NaN;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 106.9;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 5:
					switch (line)
					{
						case XrayLine.Ka1: return 67.6;
						case XrayLine.Ka2: return double.NaN;
						case XrayLine.Kb1: return double.NaN;
						case XrayLine.Kb3: return double.NaN;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 64.6;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 6:
					switch (line)
					{
						case XrayLine.Ka1: return 44.7;
						case XrayLine.Ka2: return double.NaN;
						case XrayLine.Kb1: return double.NaN;
						case XrayLine.Kb3: return double.NaN;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 43.767;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 7:
					switch (line)
					{
						case XrayLine.Ka1: return 31.63;
						case XrayLine.Ka2: return double.NaN;
						case XrayLine.Kb1: return double.NaN;
						case XrayLine.Kb3: return double.NaN;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 31.052;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 8:
					switch (line)
					{
						case XrayLine.Ka1: return 23.707;
						case XrayLine.Ka2: return double.NaN;
						case XrayLine.Kb1: return double.NaN;
						case XrayLine.Kb3: return double.NaN;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 23.367;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 9:
					switch (line)
					{
						case XrayLine.Ka1: return 18.307;
						case XrayLine.Ka2: return double.NaN;
						case XrayLine.Kb1: return double.NaN;
						case XrayLine.Kb3: return double.NaN;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 18.05;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 10:
					switch (line)
					{
						case XrayLine.Ka1: return 14.6102;
						case XrayLine.Ka2: return 14.6102;
						case XrayLine.Kb1: return 14.4522;
						case XrayLine.Kb3: return 14.4522;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 14.30201;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 11:
					switch (line)
					{
						case XrayLine.Ka1: return 11.9103;
						case XrayLine.Ka2: return 11.9103;
						case XrayLine.Kb1: return 11.5752;
						case XrayLine.Kb3: return 11.5752;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 11.5692;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 12:
					switch (line)
					{
						case XrayLine.Ka1: return 9.889554;
						case XrayLine.Ka2: return 9.89153;
						case XrayLine.Kb1: return 9.5211;
						case XrayLine.Kb3: return 9.5211;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 9.51234;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 13:
					switch (line)
					{
						case XrayLine.Ka1: return 8.339514;
						case XrayLine.Ka2: return 8.341831;
						case XrayLine.Kb1: return 7.9601;
						case XrayLine.Kb3: return 7.9601;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 7.948249;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 14:
					switch (line)
					{
						case XrayLine.Ka1: return 7.125588;
						case XrayLine.Ka2: return 7.12801;
						case XrayLine.Kb1: return 6.7531;
						case XrayLine.Kb3: return 6.7531;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 6.7381;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return 123.0;
					}
					break;

				case 15:
					switch (line)
					{
						case XrayLine.Ka1: return 6.1571;
						case XrayLine.Ka2: return 6.1601;
						case XrayLine.Kb1: return 5.7961;
						case XrayLine.Kb3: return 5.7961;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 5.7841;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 16:
					switch (line)
					{
						case XrayLine.Ka1: return 5.372200;
						case XrayLine.Ka2: return 5.374960;
						case XrayLine.Kb1: return 5.03168;
						case XrayLine.Kb3: return double.NaN;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 5.01858;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 17:
					switch (line)
					{
						case XrayLine.Ka1: return 4.727818;
						case XrayLine.Ka2: return 4.730693;
						case XrayLine.Kb1: return 4.40347;
						case XrayLine.Kb3: return 4.40347;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 4.39717;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 18:
					switch (line)
					{
						case XrayLine.Ka1: return 4.191938;
						case XrayLine.Ka2: return 4.194939;
						case XrayLine.Kb1: return 3.88606;
						case XrayLine.Kb3: return 3.88606;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 3.870958;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 19:
					switch (line)
					{
						case XrayLine.Ka1: return 3.7412838;
						case XrayLine.Ka2: return 3.7443932;
						case XrayLine.Kb1: return 3.45395;
						case XrayLine.Kb3: return 3.45395;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 3.43655;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 20:
					switch (line)
					{
						case XrayLine.Ka1: return 3.358440;
						case XrayLine.Ka2: return 3.361710;
						case XrayLine.Kb1: return 3.08975;
						case XrayLine.Kb3: return 3.08975;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return 36.331;
						case XrayLine.La1: return 36.331;
						case XrayLine.Lb1: return 35.941;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 3.07035;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return 35.131;
							//case XrayLine.L3abs: return 35.491;
					}
					break;

				case 21:
					switch (line)
					{
						case XrayLine.Ka1: return 3.030854;
						case XrayLine.Ka2: return 3.0344010;
						case XrayLine.Kb1: return 2.77964;
						case XrayLine.Kb3: return 2.77964;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return 31.350;
						case XrayLine.La1: return 31.350;
						case XrayLine.Lb1: return 31.020;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 2.7620;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 22:
					switch (line)
					{
						case XrayLine.Ka1: return 2.7485471;
						case XrayLine.Ka2: return 2.7521950;
						case XrayLine.Kb1: return 2.513960;
						case XrayLine.Kb3: return 2.513960;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return 27.420;
						case XrayLine.La1: return 27.420;
						case XrayLine.Lb1: return 27.050;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 2.497377;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return 27.290;
							//case XrayLine.L3abs: return 27.290;
					}
					break;

				case 23:
					switch (line)
					{
						case XrayLine.Ka1: return 2.503610;
						case XrayLine.Ka2: return 2.507430;
						case XrayLine.Kb1: return 2.284446;
						case XrayLine.Kb3: return 2.284446;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return 24.250;
						case XrayLine.La1: return 24.250;
						case XrayLine.Lb1: return 23.880;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 2.269211;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 24:
					switch (line)
					{
						case XrayLine.Ka1: return 2.2897260;
						case XrayLine.Ka2: return 2.2936510;
						case XrayLine.Kb1: return 2.0848810;
						case XrayLine.Kb3: return 2.0848810;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return 21.640;
						case XrayLine.La1: return 21.640;
						case XrayLine.Lb1: return 21.270;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 2.070193;
							//case XrayLine.L1abs: return 16.70;
							//case XrayLine.L2abs: return 17.90;
							//case XrayLine.L3abs: return 20.70;
					}
					break;

				case 25:
					switch (line)
					{
						case XrayLine.Ka1: return 2.1018540;
						case XrayLine.Ka2: return 2.1058220;
						case XrayLine.Kb1: return 1.9102160;
						case XrayLine.Kb3: return 1.9102160;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return 19.450;
						case XrayLine.La1: return 19.450;
						case XrayLine.Lb1: return 19.110;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 1.8964592;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 26:
					switch (line)
					{
						case XrayLine.Ka1: return 1.9360410;
						case XrayLine.Ka2: return 1.9399730;
						case XrayLine.Kb1: return 1.7566040;
						case XrayLine.Kb3: return 1.7566040;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return 17.590;
						case XrayLine.La1: return 17.590;
						case XrayLine.Lb1: return 17.260;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 1.7436170;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return 17.2023;
							//case XrayLine.L3abs: return 17.5253;
					}
					break;

				case 27:
					switch (line)
					{
						case XrayLine.Ka1: return 1.7889960;
						case XrayLine.Ka2: return 1.7928350;
						case XrayLine.Kb1: return 1.6208260;
						case XrayLine.Kb3: return 1.6208260;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return 15.9722;
						case XrayLine.La1: return 15.9722;
						case XrayLine.Lb1: return 15.666;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 1.6083510;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return 15.6182;
							//case XrayLine.L3abs: return 15.9152;
					}
					break;

				case 28:
					switch (line)
					{
						case XrayLine.Ka1: return 1.6579300;
						case XrayLine.Ka2: return 1.6617560;
						case XrayLine.Kb1: return 1.5001520;
						case XrayLine.Kb3: return 1.5001520;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return 14.5612;
						case XrayLine.La1: return 14.5612;
						case XrayLine.Lb1: return 14.2712;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 1.4881401;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return 14.2422;
							//case XrayLine.L3abs: return 14.5252;
					}
					break;

				case 29:
					switch (line)
					{
						case XrayLine.Ka1: return 1.54059290;
						case XrayLine.Ka2: return 1.54442740;
						case XrayLine.Kb1: return 1.3922340;
						case XrayLine.Kb3: return 1.3922340;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return 13.3362;
						case XrayLine.La1: return 13.3362;
						case XrayLine.Lb1: return 13.0532;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 1.3805971;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return 13.0142;
							//case XrayLine.L3abs: return 13.2882;
					}
					break;

				case 30:
					switch (line)
					{
						case XrayLine.Ka1: return 1.435184;
						case XrayLine.Ka2: return 1.439029;
						case XrayLine.Kb1: return 1.295276;
						case XrayLine.Kb3: return 1.295276;
						case XrayLine.KbII2: return 1.283739;
						case XrayLine.KbI2: return 1.283739;
						case XrayLine.La2: return 12.2542;
						case XrayLine.La1: return 12.2542;
						case XrayLine.Lb1: return 11.9832;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 1.2833798;
							//case XrayLine.L1abs: return 13.060;
							//case XrayLine.L2abs: return 11.8622;
							//case XrayLine.L3abs: return 12.1312;
					}
					break;

				case 31:
					switch (line)
					{
						case XrayLine.Ka1: return 1.3401270;
						case XrayLine.Ka2: return 1.3440260;
						case XrayLine.Kb1: return 1.207930;
						case XrayLine.Kb3: return 1.208390;
						case XrayLine.KbII2: return 1.196018;
						case XrayLine.KbI2: return 1.196018;
						case XrayLine.La2: return 11.2922;
						case XrayLine.La1: return 11.2922;
						case XrayLine.Lb1: return 11.0232;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 1.19582;
							//case XrayLine.L1abs: return 9.5171;
							//case XrayLine.L2abs: return 10.8282;
							//case XrayLine.L3abs: return 11.1002;
					}
					break;

				case 32:
					switch (line)
					{
						case XrayLine.Ka1: return 1.254073;
						case XrayLine.Ka2: return 1.258030;
						case XrayLine.Kb1: return 1.128957;
						case XrayLine.Kb3: return 1.12938;
						case XrayLine.KbII2: return 1.116877;
						case XrayLine.KbI2: return 1.116877;
						case XrayLine.La2: return 10.4363;
						case XrayLine.La1: return 10.4363;
						case XrayLine.Lb1: return 10.1752;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 1.116597;
							//case XrayLine.L1abs: return 8.7731;
							//case XrayLine.L2abs: return 9.9241;
							//case XrayLine.L3abs: return 10.1872;
					}
					break;

				case 33:
					switch (line)
					{
						case XrayLine.Ka1: return 1.17595600;
						case XrayLine.Ka2: return 1.179959;
						case XrayLine.Kb1: return 1.057368;
						case XrayLine.Kb3: return 1.057898;
						case XrayLine.KbII2: return 1.045016;
						case XrayLine.KbI2: return 1.045016;
						case XrayLine.La2: return 9.6710;
						case XrayLine.La1: return 9.6710;
						case XrayLine.Lb1: return 9.4142;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 1.04502;
							//case XrayLine.L1abs: return 8.1071;
							//case XrayLine.L2abs: return 9.1251;
							//case XrayLine.L3abs: return 9.3671;
					}
					break;

				case 34:
					switch (line)
					{
						case XrayLine.Ka1: return 1.104780;
						case XrayLine.Ka2: return 1.108830;
						case XrayLine.Kb1: return 0.992189;
						case XrayLine.Kb3: return 0.992689;
						case XrayLine.KbII2: return 0.979935;
						case XrayLine.KbI2: return 0.979935;
						case XrayLine.La2: return 8.99013;
						case XrayLine.La1: return 8.99013;
						case XrayLine.Lb1: return 8.73593;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 0.979755;
							//case XrayLine.L1abs: return 7.5031;
							//case XrayLine.L2abs: return 8.4071;
							//case XrayLine.L3abs: return 8.6461;
					}
					break;

				case 35:
					switch (line)
					{
						case XrayLine.Ka1: return 1.039756;
						case XrayLine.Ka2: return 1.043836;
						case XrayLine.Kb1: return 0.932804;
						case XrayLine.Kb3: return 0.933284;
						case XrayLine.KbII2: return 0.920474;
						case XrayLine.KbI2: return 0.920474;
						case XrayLine.La2: return 8.37473;
						case XrayLine.La1: return 8.37473;
						case XrayLine.Lb1: return 8.12522;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 0.92041;
							//case XrayLine.L1abs: return 6.9591;
							//case XrayLine.L2abs: return 7.7531;
							//case XrayLine.L3abs: return 7.9841;
					}
					break;

				case 36:
					switch (line)
					{
						case XrayLine.Ka1: return 0.9802670;
						case XrayLine.Ka2: return 0.9843590;
						case XrayLine.Kb1: return 0.8785220;
						case XrayLine.Kb3: return 0.8790110;
						case XrayLine.KbII2: return 0.86611;
						case XrayLine.KbI2: return 0.86611;
						case XrayLine.La2: return 7.82032;
						case XrayLine.La1: return 7.82032;
						case XrayLine.Lb1: return 7.574441;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 0.865533;
							//case XrayLine.L1abs: return 6.470;
							//case XrayLine.L2abs: return 7.1681;
							//case XrayLine.L3abs: return 7.3921;
					}
					break;

				case 37:
					switch (line)
					{
						case XrayLine.Ka1: return 0.925567;
						case XrayLine.Ka2: return 0.929704;
						case XrayLine.Kb1: return 0.828692;
						case XrayLine.Kb3: return 0.829222;
						case XrayLine.KbII2: return 0.816462;
						case XrayLine.KbI2: return 0.816462;
						case XrayLine.La2: return 7.32521;
						case XrayLine.La1: return 7.31841;
						case XrayLine.Lb1: return 7.07601;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 0.815552;
							//case XrayLine.L1abs: return 6.0081;
							//case XrayLine.L2abs: return 6.6441;
							//case XrayLine.L3abs: return 6.8621;
					}
					break;

				case 38:
					switch (line)
					{
						case XrayLine.Ka1: return 0.875273;
						case XrayLine.Ka2: return 0.879443;
						case XrayLine.Kb1: return 0.782932;
						case XrayLine.Kb3: return 0.783462;
						case XrayLine.KbII2: return 0.770822;
						case XrayLine.KbI2: return 0.770822;
						case XrayLine.La2: return 6.86980;
						case XrayLine.La1: return 6.86290;
						case XrayLine.Lb1: return 6.62400;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 0.769742;
							//case XrayLine.L1abs: return 5.5921;
							//case XrayLine.L2abs: return 6.1731;
							//case XrayLine.L3abs: return 6.3871;
					}
					break;

				case 39:
					switch (line)
					{
						case XrayLine.Ka1: return 0.828852;
						case XrayLine.Ka2: return 0.833063;
						case XrayLine.Kb1: return 0.740731;
						case XrayLine.Kb3: return 0.741271;
						case XrayLine.KbII2: return 0.728651;
						case XrayLine.KbI2: return 0.728651;
						case XrayLine.La2: return 6.45590;
						case XrayLine.La1: return 6.44890;
						case XrayLine.Lb1: return 6.21209;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 0.7277514;
							//case XrayLine.L1abs: return 5.2171;
							//case XrayLine.L2abs: return 5.7561;
							//case XrayLine.L3abs: return 5.9621;
					}
					break;

				case 40:
					switch (line)
					{
						case XrayLine.Ka1: return 0.7859579;
						case XrayLine.Ka2: return 0.7901790;
						case XrayLine.Kb1: return 0.7018008;
						case XrayLine.Kb3: return 0.7023554;
						case XrayLine.KbII2: return 0.689940;
						case XrayLine.KbI2: return 0.689940;
						case XrayLine.La2: return 6.0766;
						case XrayLine.La1: return 6.070250;
						case XrayLine.Lb1: return 5.836214;
						case XrayLine.Lb2: return 5.58638;
							//case XrayLine.Kabs: return 0.6889591;
							//case XrayLine.L1abs: return 4.8791;
							//case XrayLine.L2abs: return 5.3781;
							//case XrayLine.L3abs: return 5.5791;
					}
					break;

				case 41:
					switch (line)
					{
						case XrayLine.Ka1: return 0.746211;
						case XrayLine.Ka2: return 0.750451;
						case XrayLine.Kb1: return 0.665770;
						case XrayLine.Kb3: return 0.666350;
						case XrayLine.KbII2: return 0.654170;
						case XrayLine.KbI2: return 0.654170;
						case XrayLine.La2: return 5.73199;
						case XrayLine.La1: return 5.72439;
						case XrayLine.Lb1: return 5.49238;
						case XrayLine.Lb2: return 5.23798;
							//case XrayLine.Kabs: return 0.6531341;
							//case XrayLine.L1abs: return 4.5751;
							//case XrayLine.L2abs: return 5.0311;
							//case XrayLine.L3abs: return 5.2301;
					}
					break;

				case 42:
					switch (line)
					{
						case XrayLine.Ka1: return 0.70931715;
						case XrayLine.Ka2: return 0.713607;
						case XrayLine.Kb1: return 0.632303;
						case XrayLine.Kb3: return 0.632887;
						case XrayLine.KbII2: return 0.620999;
						case XrayLine.KbI2: return 0.620999;
						case XrayLine.La2: return 5.41445;
						case XrayLine.La1: return 5.40663;
						case XrayLine.Lb1: return 5.17716;
						case XrayLine.Lb2: return 4.92327;
							//case XrayLine.Kabs: return 0.61991006;
							//case XrayLine.L1abs: return 4.3041;
							//case XrayLine.L2abs: return 4.7191;
							//case XrayLine.L3abs: return 4.9131;
					}
					break;

				case 43:
					switch (line)
					{
						case XrayLine.Ka1: return 0.675030;
						case XrayLine.Ka2: return 0.679330;
						case XrayLine.Kb1: return 0.601309;
						case XrayLine.Kb3: return 0.601889;
						case XrayLine.KbII2: return 0.590249;
						case XrayLine.KbI2: return 0.590249;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return 5.11488;
						case XrayLine.Lb1: return 4.8874;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 0.589069;
							//case XrayLine.L1abs: return 4.0581;
							//case XrayLine.L2abs: return 4.4361;
							//case XrayLine.L3abs: return 4.6301;
					}
					break;

				case 44:
					switch (line)
					{
						case XrayLine.Ka1: return 0.6430994;
						case XrayLine.Ka2: return 0.6474205;
						case XrayLine.Kb1: return 0.5724966;
						case XrayLine.Kb3: return 0.5730816;
						case XrayLine.KbII2: return 0.561668;
						case XrayLine.KbI2: return 0.561668;
						case XrayLine.La2: return 4.85388;
						case XrayLine.La1: return 4.845823;
						case XrayLine.Lb1: return 4.620649;
						case XrayLine.Lb2: return 4.37187;
							//case XrayLine.Kabs: return 0.560518;
							//case XrayLine.L1abs: return 3.8351;
							//case XrayLine.L2abs: return 4.1801;
							//case XrayLine.L3abs: return 4.3691;
					}
					break;

				case 45:
					switch (line)
					{
						case XrayLine.Ka1: return 0.6132937;
						case XrayLine.Ka2: return 0.6176458;
						case XrayLine.Kb1: return 0.5456189;
						case XrayLine.Kb3: return 0.5462139;
						case XrayLine.KbII2: return 0.535038;
						case XrayLine.KbI2: return 0.535038;
						case XrayLine.La2: return 4.60552;
						case XrayLine.La1: return 4.59750;
						case XrayLine.Lb1: return 4.374206;
						case XrayLine.Lb2: return 4.13106;
							//case XrayLine.Kabs: return 0.5339086;
							//case XrayLine.L1abs: return 3.6291;
							//case XrayLine.L2abs: return 3.94256;
							//case XrayLine.L3abs: return 4.12996;
					}
					break;

				case 46:
					switch (line)
					{
						case XrayLine.Ka1: return 0.5854639;
						case XrayLine.Ka2: return 0.5898351;
						case XrayLine.Kb1: return 0.5205333;
						case XrayLine.Kb3: return 0.5211363;
						case XrayLine.KbII2: return 0.5102357;
						case XrayLine.KbI2: return 0.5102357;
						case XrayLine.La2: return 4.37595;
						case XrayLine.La1: return 4.367736;
						case XrayLine.Lb1: return 4.146282;
						case XrayLine.Lb2: return 3.908929;
							//case XrayLine.Kabs: return 0.5091212;
							//case XrayLine.L1abs: return 3.4371;
							//case XrayLine.L2abs: return 3.72286;
							//case XrayLine.L3abs: return 3.90746;
					}
					break;

				case 47:
					switch (line)
					{
						case XrayLine.Ka1: return 0.55942178;
						case XrayLine.Ka2: return 0.5638131;
						case XrayLine.Kb1: return 0.4970817;
						case XrayLine.Kb3: return 0.4976977;
						case XrayLine.KbII2: return 0.4870393;
						case XrayLine.KbI2: return 0.4870393;
						case XrayLine.La2: return 4.163002;
						case XrayLine.La1: return 4.154492;
						case XrayLine.Lb1: return 3.934789;
						case XrayLine.Lb2: return 3.703406;
							//case XrayLine.Kabs: return 0.4859155;
							//case XrayLine.L1abs: return 3.25645;
							//case XrayLine.L2abs: return 3.51645;
							//case XrayLine.L3abs: return 3.69996;
					}
					break;

				case 48:
					switch (line)
					{
						case XrayLine.Ka1: return 0.5350147;
						case XrayLine.Ka2: return 0.5394358;
						case XrayLine.Kb1: return 0.4751181;
						case XrayLine.Kb3: return 0.4757401;
						case XrayLine.KbII2: return 0.465335;
						case XrayLine.KbI2: return 0.465335;
						case XrayLine.La2: return 3.965020;
						case XrayLine.La1: return 3.956409;
						case XrayLine.Lb1: return 3.738286;
						case XrayLine.Lb2: return 3.514133;
							//case XrayLine.Kabs: return 0.4641293;
							//case XrayLine.L1abs: return 3.08495;
							//case XrayLine.L2abs: return 3.32575;
							//case XrayLine.L3abs: return 3.50475;
					}
					break;

				case 49:
					switch (line)
					{
						case XrayLine.Ka1: return 0.5121251;
						case XrayLine.Ka2: return 0.5165572;
						case XrayLine.Kb1: return 0.4545616;
						case XrayLine.Kb3: return 0.4551966;
						case XrayLine.KbII2: return 0.445007;
						case XrayLine.KbI2: return 0.445007;
						case XrayLine.La2: return 3.780787;
						case XrayLine.La1: return 3.771977;
						case XrayLine.Lb1: return 3.555363;
						case XrayLine.Lb2: return 3.338430;
							//case XrayLine.Kabs: return 0.4437454;
							//case XrayLine.L1abs: return 2.92604;
							//case XrayLine.L2abs: return 3.14735;
							//case XrayLine.L3abs: return 3.32375;
					}
					break;

				case 50:
					switch (line)
					{
						case XrayLine.Ka1: return 0.4906115;
						case XrayLine.Ka2: return 0.4950646;
						case XrayLine.Kb1: return 0.4352421;
						case XrayLine.Kb3: return 0.4358821;
						case XrayLine.KbII2: return 0.425921;
						case XrayLine.KbI2: return 0.425921;
						case XrayLine.La2: return 3.606964;
						case XrayLine.La1: return 3.599994;
						case XrayLine.Lb1: return 3.384921;
						case XrayLine.Lb2: return 3.175098;
							//case XrayLine.Kabs: return 0.4245978;
							//case XrayLine.L1abs: return 2.77694;
							//case XrayLine.L2abs: return 2.98234;
							//case XrayLine.L3abs: return 3.15575;
					}
					break;

				case 51:
					switch (line)
					{
						case XrayLine.Ka1: return 0.4703700;
						case XrayLine.Ka2: return 0.4748391;
						case XrayLine.Kb1: return 0.4170966;
						case XrayLine.Kb3: return 0.4177477;
						case XrayLine.KbII2: return 0.4079791;
						case XrayLine.KbI2: return 0.4079791;
						case XrayLine.La2: return 3.448452;
						case XrayLine.La1: return 3.439462;
						case XrayLine.Lb1: return 3.225718;
						case XrayLine.Lb2: return 3.023395;
							//case XrayLine.Kabs: return 0.4066324;
							//case XrayLine.L1abs: return 2.63884;
							//case XrayLine.L2abs: return 2.82944;
							//case XrayLine.L3abs: return 3.00035;
					}
					break;

				case 52:
					switch (line)
					{
						case XrayLine.Ka1: return 0.4513018;
						case XrayLine.Ka2: return 0.4557908;
						case XrayLine.Kb1: return 0.4000010;
						case XrayLine.Kb3: return 0.4006650;
						case XrayLine.KbII2: return 0.3911079;
						case XrayLine.KbI2: return 0.3911079;
						case XrayLine.La2: return 3.29851;
						case XrayLine.La1: return 3.289249;
						case XrayLine.Lb1: return 3.076816;
						case XrayLine.Lb2: return 2.88221;
							//case XrayLine.Kabs: return 0.389746;
							//case XrayLine.L1abs: return 2.50994;
							//case XrayLine.L2abs: return 2.68794;
							//case XrayLine.L3abs: return 2.85554;
					}
					break;

				case 53:
					switch (line)
					{
						case XrayLine.Ka1: return 0.4333245;
						case XrayLine.Ka2: return 0.437836;
						case XrayLine.Kb1: return 0.3839108;
						case XrayLine.Kb3: return 0.3845698;
						case XrayLine.KbII2: return 0.375236;
						case XrayLine.KbI2: return 0.375236;
						case XrayLine.La2: return 3.157957;
						case XrayLine.La1: return 3.148647;
						case XrayLine.Lb1: return 2.937484;
						case XrayLine.Lb2: return 2.75057;
							//case XrayLine.Kabs: return 0.373816;
							//case XrayLine.L1abs: return 2.38804;
							//case XrayLine.L2abs: return 2.55424;
							//case XrayLine.L3abs: return 2.71964;
					}
					break;

				case 54:
					switch (line)
					{
						case XrayLine.Ka1: return 0.4163508;
						case XrayLine.Ka2: return 0.42088103;
						case XrayLine.Kb1: return 0.3687346;
						case XrayLine.Kb3: return 0.3694051;
						case XrayLine.KbII2: return 0.360265;
						case XrayLine.KbI2: return 0.360265;
						case XrayLine.La2: return 3.025940;
						case XrayLine.La1: return 3.016582;
						case XrayLine.Lb1: return 2.806553;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 0.35841;
							//case XrayLine.L1abs: return 2.27373;
							//case XrayLine.L2abs: return 2.42924;
							//case XrayLine.L3abs: return 2.59264;
					}
					break;

				case 55:
					switch (line)
					{
						case XrayLine.Ka1: return 0.4002960;
						case XrayLine.Ka2: return 0.4048411;
						case XrayLine.Kb1: return 0.354369;
						case XrayLine.Kb3: return 0.3550553;
						case XrayLine.KbII2: return 0.346115;
						case XrayLine.KbI2: return 0.346115;
						case XrayLine.La2: return 2.90204;
						case XrayLine.La1: return 2.89244;
						case XrayLine.Lb1: return 2.68374;
						case XrayLine.Lb2: return 2.51184;
							//case XrayLine.Kabs: return 0.344515;
							//case XrayLine.L1abs: return 2.16733;
							//case XrayLine.L2abs: return 2.31393;
							//case XrayLine.L3abs: return 2.47404;
					}
					break;

				case 56:
					switch (line)
					{
						case XrayLine.Ka1: return 0.38512464;
						case XrayLine.Ka2: return 0.38968378;
						case XrayLine.Kb1: return 0.34082708;
						case XrayLine.Kb3: return 0.3415228;
						case XrayLine.KbII2: return 0.332775;
						case XrayLine.KbI2: return 0.332775;
						case XrayLine.La2: return 2.785572;
						case XrayLine.La1: return 2.775992;
						case XrayLine.Lb1: return 2.568249;
						case XrayLine.Lb2: return 2.404386;
							//case XrayLine.Kabs: return 0.331045;
							//case XrayLine.L1abs: return 2.06783;
							//case XrayLine.L2abs: return 2.20483;
							//case XrayLine.L3abs: return 2.36294;
					}
					break;

				case 57:
					switch (line)
					{
						case XrayLine.Ka1: return 0.3707426;
						case XrayLine.Ka2: return 0.3753186;
						case XrayLine.Kb1: return 0.3279879;
						case XrayLine.Kb3: return 0.3286909;
						case XrayLine.KbII2: return 0.320122;
						case XrayLine.KbI2: return 0.320122;
						case XrayLine.La2: return 2.675383;
						case XrayLine.La1: return 2.665740;
						case XrayLine.Lb1: return 2.458947;
						case XrayLine.Lb2: return 2.303312;
							//case XrayLine.Kabs: return 0.318445;
							//case XrayLine.L1abs: return 1.97803;
							//case XrayLine.L2abs: return 2.10533;
							//case XrayLine.L3abs: return 2.2610;
					}
					break;

				case 58:
					switch (line)
					{
						case XrayLine.Ka1: return 0.3570974;
						case XrayLine.Ka2: return 0.3616884;
						case XrayLine.Kb1: return 0.3158207;
						case XrayLine.Kb3: return 0.3165248;
						case XrayLine.KbII2: return 0.308165;
						case XrayLine.KbI2: return 0.308165;
						case XrayLine.La2: return 2.57059;
						case XrayLine.La1: return 2.56163;
						case XrayLine.Lb1: return 2.35580;
						case XrayLine.Lb2: return 2.20900;
							//case XrayLine.Kabs: return 0.306485;
							//case XrayLine.L1abs: return 1.89343;
							//case XrayLine.L2abs: return 2.01243;
							//case XrayLine.L3abs: return 2.1660;
					}
					break;

				case 59:
					switch (line)
					{
						case XrayLine.Ka1: return 0.3441452;
						case XrayLine.Ka2: return 0.3487542;
						case XrayLine.Kb1: return 0.3042656;
						case XrayLine.Kb3: return 0.3049796;
						case XrayLine.KbII2: return 0.296794;
						case XrayLine.KbI2: return 0.296794;
						case XrayLine.La2: return 2.47294;
						case XrayLine.La1: return 2.46304;
						case XrayLine.Lb1: return 2.25883;
						case XrayLine.Lb2: return 2.11943;
							//case XrayLine.Kabs: return 0.295184;
							//case XrayLine.L1abs: return 1.81413;
							//case XrayLine.L2abs: return 1.92553;
							//case XrayLine.L3abs: return 2.07913;
					}
					break;

				case 60:
					switch (line)
					{
						case XrayLine.Ka1: return 0.33185689;
						case XrayLine.Ka2: return 0.33647921;
						case XrayLine.Kb1: return 0.2933086;
						case XrayLine.Kb3: return 0.2940366;
						case XrayLine.KbII2: return 0.28610;
						case XrayLine.KbI2: return 0.28610;
						case XrayLine.La2: return 2.38079;
						case XrayLine.La1: return 2.370526;
						case XrayLine.Lb1: return 2.167008;
						case XrayLine.Lb2: return 2.035448;
							//case XrayLine.Kabs: return 0.284534;
							//case XrayLine.L1abs: return 1.73903;
							//case XrayLine.L2abs: return 1.84403;
							//case XrayLine.L3abs: return 1.99673;
					}
					break;

				case 61:
					switch (line)
					{
						case XrayLine.Ka1: return 0.3201648;
						case XrayLine.Ka2: return 0.3248079;
						case XrayLine.Kb1: return 0.282904;
						case XrayLine.Kb3: return 0.283634;
						case XrayLine.KbII2: return 0.27590;
						case XrayLine.KbI2: return 0.27590;
						case XrayLine.La2: return 2.29263;
						case XrayLine.La1: return 2.28223;
						case XrayLine.Lb1: return 2.07973;
						case XrayLine.Lb2: return 1.95593;
							//case XrayLine.Kabs: return 0.274314;
							//case XrayLine.L1abs: return 1.66743;
							//case XrayLine.L2abs: return 1.76763;
							//case XrayLine.L3abs: return 1.91913;
					}
					break;

				case 62:
					switch (line)
					{
						case XrayLine.Ka1: return 0.30904506;
						case XrayLine.Ka2: return 0.31369830;
						case XrayLine.Kb1: return 0.273014;
						case XrayLine.Kb3: return 0.273764;
						case XrayLine.KbII2: return 0.26620;
						case XrayLine.KbI2: return 0.26620;
						case XrayLine.La2: return 2.210430;
						case XrayLine.La1: return 2.199873;
						case XrayLine.Lb1: return 1.998432;
						case XrayLine.Lb2: return 1.882206;
							//case XrayLine.Kabs: return 0.264644;
							//case XrayLine.L1abs: return 1.60022;
							//case XrayLine.L2abs: return 1.69533;
							//case XrayLine.L3abs: return 1.84573;
					}
					break;

				case 63:
					switch (line)
					{
						case XrayLine.Ka1: return 0.2984505;
						case XrayLine.Ka2: return 0.3031225;
						case XrayLine.Kb1: return 0.2635810;
						case XrayLine.Kb3: return 0.2643360;
						case XrayLine.KbII2: return 0.256927;
						case XrayLine.KbI2: return 0.256927;
						case XrayLine.La2: return 2.13156;
						case XrayLine.La1: return 2.120673;
						case XrayLine.Lb1: return 1.92053;
						case XrayLine.Lb2: return 1.81215;
							//case XrayLine.Kabs: return 0.255534;
							//case XrayLine.L1abs: return 1.53812;
							//case XrayLine.L2abs: return 1.62712;
							//case XrayLine.L3abs: return 1.77613;
					}
					break;

				case 64:
					switch (line)
					{
						case XrayLine.Ka1: return 0.2883573;
						case XrayLine.Ka2: return 0.2930424;
						case XrayLine.Kb1: return 0.254604;
						case XrayLine.Kb3: return 0.255344;
						case XrayLine.KbII2: return 0.248164;
						case XrayLine.KbI2: return 0.248164;
						case XrayLine.La2: return 2.05783;
						case XrayLine.La1: return 2.04683;
						case XrayLine.Lb1: return 1.84683;
						case XrayLine.Lb2: return 1.74553;
							//case XrayLine.Kabs: return 0.246814;
							//case XrayLine.L1abs: return 1.47842;
							//case XrayLine.L2abs: return 1.56322;
							//case XrayLine.L3abs: return 1.71173;
					}
					break;

				case 65:
					switch (line)
					{
						case XrayLine.Ka1: return 0.2787242;
						case XrayLine.Ka2: return 0.2834273;
						case XrayLine.Kb1: return 0.246084;
						case XrayLine.Kb3: return 0.246834;
						case XrayLine.KbII2: return 0.23970;
						case XrayLine.KbI2: return 0.23970;
						case XrayLine.La2: return 1.98753;
						case XrayLine.La1: return 1.97653;
						case XrayLine.Lb1: return 1.77683;
						case XrayLine.Lb2: return 1.68303;
							//case XrayLine.Kabs: return 0.238414;
							//case XrayLine.L1abs: return 1.42232;
							//case XrayLine.L2abs: return 1.50232;
							//case XrayLine.L3abs: return 1.64972;
					}
					break;

				case 66:
					switch (line)
					{
						case XrayLine.Ka1: return 0.2695370;
						case XrayLine.Ka2: return 0.2742511;
						case XrayLine.Kb1: return 0.237884;
						case XrayLine.Kb3: return 0.238624;
						case XrayLine.KbII2: return 0.23170;
						case XrayLine.KbI2: return 0.23170;
						case XrayLine.La2: return 1.919939;
						case XrayLine.La1: return 1.908839;
						case XrayLine.Lb1: return 1.71065;
						case XrayLine.Lb2: return 1.62371;
							//case XrayLine.Kabs: return 0.230483;
							//case XrayLine.L1abs: return 1.36922;
							//case XrayLine.L2abs: return 1.44452;
							//case XrayLine.L3abs: return 1.59162;
					}
					break;

				case 67:
					switch (line)
					{
						case XrayLine.Ka1: return 0.2607608;
						case XrayLine.Ka2: return 0.26549088;
						case XrayLine.Kb1: return 0.230124;
						case XrayLine.Kb3: return 0.230834;
						case XrayLine.KbII2: return 0.22410;
						case XrayLine.KbI2: return 0.22410;
						case XrayLine.La2: return 1.856472;
						case XrayLine.La1: return 1.845092;
						case XrayLine.Lb1: return 1.647484;
						case XrayLine.Lb2: return 1.567168;
							//case XrayLine.Kabs: return 0.222913;
							//case XrayLine.L1abs: return 1.31902;
							//case XrayLine.L2abs: return 1.39052;
							//case XrayLine.L3abs: return 1.53682;
					}
					break;

				case 68:
					switch (line)
					{
						case XrayLine.Ka1: return 0.25237359;
						case XrayLine.Ka2: return 0.2571133;
						case XrayLine.Kb1: return 0.22269866;
						case XrayLine.Kb3: return 0.2234766;
						case XrayLine.KbII2: return 0.21670;
						case XrayLine.KbI2: return 0.21670;
						case XrayLine.La2: return 1.795701;
						case XrayLine.La1: return 1.784481;
						case XrayLine.Lb1: return 1.587466;
						case XrayLine.Lb2: return 1.51401;
							//case XrayLine.Kabs: return 0.2156801;
							//case XrayLine.L1abs: return 1.27062;
							//case XrayLine.L2abs: return 1.33862;
							//case XrayLine.L3abs: return 1.48352;
					}
					break;

				case 69:
					switch (line)
					{
						case XrayLine.Ka1: return 0.24434486;
						case XrayLine.Ka2: return 0.24910095;
						case XrayLine.Kb1: return 0.21559182;
						case XrayLine.Kb3: return 0.216366;
						case XrayLine.KbII2: return 0.20980;
						case XrayLine.KbI2: return 0.20980;
						case XrayLine.La2: return 1.738003;
						case XrayLine.La1: return 1.7267720;
						case XrayLine.Lb1: return 1.5302410;
						case XrayLine.Lb2: return 1.46402;
							//case XrayLine.Kabs: return 0.208803;
							//case XrayLine.L1abs: return 1.22502;
							//case XrayLine.L2abs: return 1.28922;
							//case XrayLine.L3abs: return 1.43342;
					}
					break;

				case 70:
					switch (line)
					{
						case XrayLine.Ka1: return 0.2366586;
						case XrayLine.Ka2: return 0.2414276;
						case XrayLine.Kb1: return 0.208843;
						case XrayLine.Kb3: return 0.20960;
						case XrayLine.KbII2: return 0.20330;
						case XrayLine.KbI2: return 0.20330;
						case XrayLine.La2: return 1.682875;
						case XrayLine.La1: return 1.671915;
						case XrayLine.Lb1: return 1.475672;
						case XrayLine.Lb2: return 1.415521;
							//case XrayLine.Kabs: return 0.202243;
							//case XrayLine.L1abs: return 1.18182;
							//case XrayLine.L2abs: return 1.24282;
							//case XrayLine.L3abs: return 1.38622;
					}
					break;

				case 71:
					switch (line)
					{
						case XrayLine.Ka1: return 0.2293014;
						case XrayLine.Ka2: return 0.2340845;
						case XrayLine.Kb1: return 0.202313;
						case XrayLine.Kb3: return 0.203093;
						case XrayLine.KbII2: return 0.19690;
						case XrayLine.KbI2: return 0.19690;
						case XrayLine.La2: return 1.630314;
						case XrayLine.La1: return 1.619534;
						case XrayLine.Lb1: return 1.423611;
						case XrayLine.Lb2: return 1.370141;
							//case XrayLine.Kabs: return 0.195853;
							//case XrayLine.L1abs: return 1.14022;
							//case XrayLine.L2abs: return 1.19852;
							//case XrayLine.L3abs: return 1.34052;
					}
					break;

				case 72:
					switch (line)
					{
						case XrayLine.Ka1: return 0.2222303;
						case XrayLine.Ka2: return 0.2270274;
						case XrayLine.Kb1: return 0.196073;
						case XrayLine.Kb3: return 0.196863;
						case XrayLine.KbII2: return 0.19080;
						case XrayLine.KbI2: return 0.19080;
						case XrayLine.La2: return 1.580484;
						case XrayLine.La1: return 1.569604;
						case XrayLine.Lb1: return 1.374121;
						case XrayLine.Lb2: return 1.326410;
							//case XrayLine.Kabs: return 0.189823;
							//case XrayLine.L1abs: return 1.1002640;
							//case XrayLine.L2abs: return 1.1548587;
							//case XrayLine.L3abs: return 1.2971383;
					}
					break;

				case 73:
					switch (line)
					{
						case XrayLine.Ka1: return 0.2155002;
						case XrayLine.Ka2: return 0.2203083;
						case XrayLine.Kb1: return 0.1900919;
						case XrayLine.Kb3: return 0.1908929;
						case XrayLine.KbII2: return 0.185191;
						case XrayLine.KbI2: return 0.185014;
						case XrayLine.La2: return 1.532953;
						case XrayLine.La1: return 1.521993;
						case XrayLine.Lb1: return 1.327000;
						case XrayLine.Lb2: return 1.284559;
							//case XrayLine.Kabs: return 0.183943;
							//case XrayLine.L1abs: return 1.06132;
							//case XrayLine.L2abs: return 1.11372;
							//case XrayLine.L3abs: return 1.25532;
					}
					break;

				case 74:
					switch (line)
					{
						case XrayLine.Ka1: return 0.20901314;
						case XrayLine.Ka2: return 0.21383304;
						case XrayLine.Kb1: return 0.1843768;
						case XrayLine.Kb3: return 0.18518317;
						case XrayLine.KbII2: return 0.179603;
						case XrayLine.KbI2: return 0.179424;
						case XrayLine.La2: return 1.487452;
						case XrayLine.La1: return 1.4763112;
						case XrayLine.Lb1: return 1.281812;
						case XrayLine.Lb2: return 1.2443048;
							//case XrayLine.Kabs: return 0.178373;
							//case XrayLine.L1abs: return 1.024685;
							//case XrayLine.L2abs: return 1.07452;
							//case XrayLine.L3abs: return 1.21552;
					}
					break;

				case 75:
					switch (line)
					{
						case XrayLine.Ka1: return 0.2027840;
						case XrayLine.Ka2: return 0.2076141;
						case XrayLine.Kb1: return 0.1788827;
						case XrayLine.Kb3: return 0.1796997;
						case XrayLine.KbII2: return 0.174253;
						case XrayLine.KbI2: return 0.1740566;
						case XrayLine.La2: return 1.443982;
						case XrayLine.La1: return 1.432922;
						case XrayLine.Lb1: return 1.238599;
						case XrayLine.Lb2: return 1.206618;
							//case XrayLine.Kabs: return 0.173023;
							//case XrayLine.L1abs: return 0.98941;
							//case XrayLine.L2abs: return 1.03712;
							//case XrayLine.L3abs: return 1.17732;
					}
					break;

				case 76:
					switch (line)
					{
						case XrayLine.Ka1: return 0.1967970;
						case XrayLine.Ka2: return 0.2016420;
						case XrayLine.Kb1: return 0.1736136;
						case XrayLine.Kb3: return 0.1744336;
						case XrayLine.KbII2: return 0.169103;
						case XrayLine.KbI2: return 0.1689085;
						case XrayLine.La2: return 1.402361;
						case XrayLine.La1: return 1.391231;
						case XrayLine.Lb1: return 1.197288;
						case XrayLine.Lb2: return 1.16981;
							//case XrayLine.Kabs: return 0.167873;
							//case XrayLine.L1abs: return 0.95581;
							//case XrayLine.L2abs: return 1.00142;
							//case XrayLine.L3abs: return 1.14082;
					}
					break;

				case 77:
					switch (line)
					{
						case XrayLine.Ka1: return 0.1910499;
						case XrayLine.Ka2: return 0.1959069;
						case XrayLine.Kb1: return 0.1685445;
						case XrayLine.Kb3: return 0.1693695;
						case XrayLine.KbII2: return 0.164152;
						case XrayLine.KbI2: return 0.163958;
						case XrayLine.La2: return 1.362520;
						case XrayLine.La1: return 1.351300;
						case XrayLine.Lb1: return 1.157827;
						case XrayLine.Lb2: return 1.135337;
							//case XrayLine.Kabs: return 0.162922;
							//case XrayLine.L1abs: return 0.92361;
							//case XrayLine.L2abs: return 0.96711;
							//case XrayLine.L3abs: return 1.10582;
					}
					break;

				case 78:
					switch (line)
					{
						case XrayLine.Ka1: return 0.1855138;
						case XrayLine.Ka2: return 0.1903839;
						case XrayLine.Kb1: return 0.1636775;
						case XrayLine.Kb3: return 0.1645035;
						case XrayLine.KbII2: return 0.159392;
						case XrayLine.KbI2: return 0.159202;
						case XrayLine.La2: return 1.324340;
						case XrayLine.La1: return 1.313060;
						case XrayLine.Lb1: return 1.119917;
						case XrayLine.Lb2: return 1.102017;
							//case XrayLine.Kabs: return 0.158182;
							//case XrayLine.L1abs: return 0.893213;
							//case XrayLine.L2abs: return 0.9341861;
							//case XrayLine.L3abs: return 1.0722721;
					}
					break;

				case 79:
					switch (line)
					{
						case XrayLine.Ka1: return 0.18019780;
						case XrayLine.Ka2: return 0.18507664;
						case XrayLine.Kb1: return 0.15899527;
						case XrayLine.Kb3: return 0.1598249;
						case XrayLine.KbII2: return 0.154832;
						case XrayLine.KbI2: return 0.154620;
						case XrayLine.La2: return 1.287739;
						case XrayLine.La1: return 1.276419;
						case XrayLine.Lb1: return 1.083546;
						case XrayLine.Lb2: return 1.070236;
							//case XrayLine.Kabs: return 0.1535953;
							//case XrayLine.L1abs: return 0.863683;
							//case XrayLine.L2abs: return 0.9027409;
							//case XrayLine.L3abs: return 1.0401625;
					}
					break;

				case 80:
					switch (line)
					{
						case XrayLine.Ka1: return 0.1750706;
						case XrayLine.Ka2: return 0.1799607;
						case XrayLine.Kb1: return 0.1544893;
						case XrayLine.Kb3: return 0.1553233;
						case XrayLine.KbII2: return 0.150402;
						case XrayLine.KbI2: return 0.150202;
						case XrayLine.La2: return 1.25266;
						case XrayLine.La1: return 1.241219;
						case XrayLine.Lb1: return 1.048696;
						case XrayLine.Lb2: return 1.03977;
							//case XrayLine.Kabs: return 0.149182;
							//case XrayLine.L1abs: return 0.83531;
							//case XrayLine.L2abs: return 0.87221;
							//case XrayLine.L3abs: return 1.00912;
					}
					break;

				case 81:
					switch (line)
					{
						case XrayLine.Ka1: return 0.1701386;
						case XrayLine.Ka2: return 0.1750386;
						case XrayLine.Kb1: return 0.1501443;
						case XrayLine.Kb3: return 0.1509823;
						case XrayLine.KbII2: return 0.146142;
						case XrayLine.KbI2: return 0.145952;
						case XrayLine.La2: return 1.218768;
						case XrayLine.La1: return 1.207408;
						case XrayLine.Lb1: return 1.015145;
						case XrayLine.Lb2: return 1.010325;
							//case XrayLine.Kabs: return 0.144952;
							//case XrayLine.L1abs: return 0.80811;
							//case XrayLine.L2abs: return 0.84341;
							//case XrayLine.L3abs: return 0.97931;
					}
					break;

				case 82:
					switch (line)
					{
						case XrayLine.Ka1: return 0.16537816;
						case XrayLine.Ka2: return 0.17029527;
						case XrayLine.Kb1: return 0.14596836;
						case XrayLine.Kb3: return 0.1468129;
						case XrayLine.KbII2: return 0.142122;
						case XrayLine.KbI2: return 0.141912;
						case XrayLine.La2: return 1.186498;
						case XrayLine.La1: return 1.175028;
						case XrayLine.Lb1: return 0.982925;
						case XrayLine.Lb2: return 0.98222;
							//case XrayLine.Kabs: return 0.1408821;
							//case XrayLine.L1abs: return 0.7818404;
							//case XrayLine.L2abs: return 0.8157395;
							//case XrayLine.L3abs: return 0.9511590;
					}
					break;

				case 83:
					switch (line)
					{
						case XrayLine.Ka1: return 0.1607903;
						case XrayLine.Ka2: return 0.1657183;
						case XrayLine.Kb1: return 0.1419492;
						case XrayLine.Kb3: return 0.142780;
						case XrayLine.KbII2: return 0.138172;
						case XrayLine.KbI2: return 0.137972;
						case XrayLine.La2: return 1.155377;
						case XrayLine.La1: return 1.143877;
						case XrayLine.Lb1: return 0.951992;
						case XrayLine.Lb2: return 0.955194;
							//case XrayLine.Kabs: return 0.136942;
							//case XrayLine.L1abs: return 0.75711;
							//case XrayLine.L2abs: return 0.78871;
							//case XrayLine.L3abs: return 0.92341;
					}
					break;

				case 84:
					switch (line)
					{
						case XrayLine.Ka1: return 0.156362;
						case XrayLine.Ka2: return 0.161302;
						case XrayLine.Kb1: return 0.138072;
						case XrayLine.Kb3: return 0.138922;
						case XrayLine.KbII2: return 0.134382;
						case XrayLine.KbI2: return 0.134182;
						case XrayLine.La2: return 1.125497;
						case XrayLine.La1: return 1.113877;
						case XrayLine.Lb1: return 0.92201;
						case XrayLine.Lb2: return 0.929384;
							//case XrayLine.Kabs: return double.NaN;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 85:
					switch (line)
					{
						case XrayLine.Ka1: return 0.152102;
						case XrayLine.Ka2: return 0.157052;
						case XrayLine.Kb1: return 0.134322;
						case XrayLine.Kb3: return 0.135172;
						case XrayLine.KbII2: return 0.130722;
						case XrayLine.KbI2: return 0.130522;
						case XrayLine.La2: return 1.096726;
						case XrayLine.La1: return 1.085016;
						case XrayLine.Lb1: return 0.89350;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return double.NaN;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 86:
					switch (line)
					{
						case XrayLine.Ka1: return 0.147982;
						case XrayLine.Ka2: return 0.152942;
						case XrayLine.Kb1: return 0.130692;
						case XrayLine.Kb3: return 0.131552;
						case XrayLine.KbII2: return 0.127192;
						case XrayLine.KbI2: return 0.126982;
						case XrayLine.La2: return 1.069006;
						case XrayLine.La1: return 1.057246;
						case XrayLine.Lb1: return 0.86606;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return double.NaN;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 87:
					switch (line)
					{
						case XrayLine.Ka1: return 0.143992;
						case XrayLine.Ka2: return 0.148962;
						case XrayLine.Kb1: return 0.127192;
						case XrayLine.Kb3: return 0.128072;
						case XrayLine.KbII2: return 0.123792;
						case XrayLine.KbI2: return 0.123582;
						case XrayLine.La2: return 1.042316;
						case XrayLine.La1: return 1.030505;
						case XrayLine.Lb1: return 0.83941;
						case XrayLine.Lb2: return 0.8580;
							//case XrayLine.Kabs: return double.NaN;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 88:
					switch (line)
					{
						case XrayLine.Ka1: return 0.140132;
						case XrayLine.Ka2: return 0.145119;
						case XrayLine.Kb1: return 0.123815;
						case XrayLine.Kb3: return 0.124689;
						case XrayLine.KbII2: return 0.120535;
						case XrayLine.KbI2: return 0.120320;
						case XrayLine.La2: return 1.016575;
						case XrayLine.La1: return 1.004745;
						case XrayLine.Lb1: return 0.813762;
						case XrayLine.Lb2: return 0.835383;
							//case XrayLine.Kabs: return double.NaN;
							//case XrayLine.L1abs: return 0.64451;
							//case XrayLine.L2abs: return 0.67071;
							//case XrayLine.L3abs: return 0.80281;
					}
					break;

				case 89:
					switch (line)
					{
						case XrayLine.Ka1: return 0.136419;
						case XrayLine.Ka2: return 0.141412;
						case XrayLine.Kb1: return 0.120552;
						case XrayLine.Kb3: return 0.121432;
						case XrayLine.KbII2: return 0.117322;
						case XrayLine.KbI2: return 0.117112;
						case XrayLine.La2: return 0.991795;
						case XrayLine.La1: return 0.979945;
						case XrayLine.Lb1: return 0.78904;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return double.NaN;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 90:
					switch (line)
					{
						case XrayLine.Ka1: return 0.13282021;
						case XrayLine.Ka2: return 0.13782600;
						case XrayLine.Kb1: return 0.11740759;
						case XrayLine.Kb3: return 0.11828686;
						case XrayLine.KbII2: return 0.114262;
						case XrayLine.KbI2: return 0.114042;
						case XrayLine.La2: return 0.9679082;
						case XrayLine.La1: return 0.9560826;
						case XrayLine.Lb1: return 0.7652610;
						case XrayLine.Lb2: return 0.7935516;
							//case XrayLine.Kabs: return 0.113072;
							//case XrayLine.L1abs: return 0.60591;
							//case XrayLine.L2abs: return 0.62991;
							//case XrayLine.L3abs: return 0.76071;
					}
					break;

				case 91:
					switch (line)
					{
						case XrayLine.Ka1: return 0.1293302;
						case XrayLine.Ka2: return 0.1343516;
						case XrayLine.Kb1: return 0.1143583;
						case XrayLine.Kb3: return 0.1152427;
						case XrayLine.KbII2: return 0.111292;
						case XrayLine.KbI2: return 0.111072;
						case XrayLine.La2: return 0.944834;
						case XrayLine.La1: return 0.932854;
						case XrayLine.Lb1: return 0.742331;
						case XrayLine.Lb2: return 0.77371;
							//case XrayLine.Kabs: return double.NaN;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 92:
					switch (line)
					{
						case XrayLine.Ka1: return 0.12595977;
						case XrayLine.Ka2: return 0.13099111;
						case XrayLine.Kb1: return 0.11140132;
						case XrayLine.Kb3: return 0.11228858;
						case XrayLine.KbII2: return 0.108372;
						case XrayLine.KbI2: return 0.108182;
						case XrayLine.La2: return 0.922572;
						case XrayLine.La1: return 0.910653;
						case XrayLine.Lb1: return 0.719995;
						case XrayLine.Lb2: return 0.754692;
							//case XrayLine.Kabs: return 0.107232;
							//case XrayLine.L1abs: return 0.56951;
							//case XrayLine.L2abs: return 0.59191;
							//case XrayLine.L3abs: return 0.72231;
					}
					break;

				case 93:
					switch (line)
					{
						case XrayLine.Ka1: return 0.1226882;
						case XrayLine.Ka2: return 0.1277287;
						case XrayLine.Kb1: return 0.1085265;
						case XrayLine.Kb3: return 0.1094230;
						case XrayLine.KbII2: return 0.105670;
						case XrayLine.KbI2: return 0.105457;
						case XrayLine.La2: return 0.901059;
						case XrayLine.La1: return 0.889141;
						case XrayLine.Lb1: return 0.698488;
						case XrayLine.Lb2: return 0.736241;
							//case XrayLine.Kabs: return 0.1044605;
							//case XrayLine.L1abs: return 0.55239;
							//case XrayLine.L2abs: return 0.57368;
							//case XrayLine.L3abs: return 0.704136;
					}
					break;

				case 94:
					switch (line)
					{
						case XrayLine.Ka1: return 0.1195140;
						case XrayLine.Ka2: return 0.1245705;
						case XrayLine.Kb1: return 0.1057595;
						case XrayLine.Kb3: return 0.1066611;
						case XrayLine.KbII2: return 0.1029724;
						case XrayLine.KbI2: return 0.1027429;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return double.NaN;
							//case XrayLine.L1abs: return 0.53651;
							//case XrayLine.L2abs: return 0.55721;
							//case XrayLine.L3abs: return 0.68671;
					}
					break;

				case 95:
					switch (line)
					{
						case XrayLine.Ka1: return 0.1164463;
						case XrayLine.Ka2: return 0.1215158;
						case XrayLine.Kb1: return 0.1030803;
						case XrayLine.Kb3: return 0.1039794;
						case XrayLine.KbII2: return 0.1003537;
						case XrayLine.KbI2: return 0.1001357;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return double.NaN;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 96:
					switch (line)
					{
						case XrayLine.Ka1: return 0.1134635;
						case XrayLine.Ka2: return 0.1185427;
						case XrayLine.Kb1: return 0.1004708;
						case XrayLine.Kb3: return 0.1013753;
						case XrayLine.KbII2: return 0.0978355;
						case XrayLine.KbI2: return 0.0975952;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return double.NaN;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 97:
					switch (line)
					{
						case XrayLine.Ka1: return 0.1105745;
						case XrayLine.Ka2: return 0.1156630;
						case XrayLine.Kb1: return 0.0979514;
						case XrayLine.Kb3: return 0.0988598;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return 0.0942501;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return double.NaN;
							//case XrayLine.L1abs: return 0.49060;
							//case XrayLine.L2abs: return 0.50851;
							//case XrayLine.L3abs: return 0.63748;
					}
					break;

				case 98:
					switch (line)
					{
						case XrayLine.Ka1: return 0.1077793;
						case XrayLine.Ka2: return 0.1128799;
						case XrayLine.Kb1: return 0.0954860;
						case XrayLine.Kb3: return 0.0963915;
						case XrayLine.KbII2: return 0.0929715;
						case XrayLine.KbI2: return 0.0927508;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 0.091862;
							//case XrayLine.L1abs: return 0.476569;
							//case XrayLine.L2abs: return 0.493804;
							//case XrayLine.L3abs: return 0.62300;
					}
					break;

				case 99:
					switch (line)
					{
						case XrayLine.Ka1: return 0.1050554;
						case XrayLine.Ka2: return 0.1102072;
						case XrayLine.Kb1: return 0.093090;
						case XrayLine.Kb3: return 0.094036;
						case XrayLine.KbII2: return double.NaN;
						case XrayLine.KbI2: return double.NaN;
						case XrayLine.La2: return double.NaN;
						case XrayLine.La1: return double.NaN;
						case XrayLine.Lb1: return double.NaN;
						case XrayLine.Lb2: return double.NaN;
							//case XrayLine.Kabs: return 0.0895878;
							//case XrayLine.L1abs: return double.NaN;
							//case XrayLine.L2abs: return double.NaN;
							//case XrayLine.L3abs: return double.NaN;
					}
					break;

				case 100:
					switch (line)
					{
						case XrayLine.Ka1: return 0.102386;
						case XrayLine.Ka2: return 0.107514;
						case XrayLine.Kb1: return 0.0907943;
						case XrayLine.Kb3: return 0.091715;
						case XrayLine.KbII2: return 0.0884212;
						case XrayLine.KbI2: return 0.0881872;
						case XrayLine.La2: return 0.76904;
						case XrayLine.La1: return 0.75674;
						case XrayLine.Lb1: return 0.56619;
						case XrayLine.Lb2: return 0.62369;
							//case XrayLine.Kabs: return 0.0873356;
							//case XrayLine.L1abs: return 0.44966;
							//case XrayLine.L2abs: return 0.46534;
							//case XrayLine.L3abs: return 0.59414;
					}
					break;

				default: return double.NaN;
			}
			#endregion

			return 0;
		}

		/// <summary>
		/// 原子量を返す　引数は原子番号
		/// </summary>
		/// <param name="z"></param>
		/// <returns></returns>
		public static double AtomicWeight(int z)
		{
			#region
			switch (z)
			{
				case 0: return 0;
				case 1: return 1.007947;
				case 2: return 4.0026022;
				case 3: return 6.9412;
				case 4: return 9.0121823;
				case 5: return 10.8115;
				case 6: return 12.0111;
				case 7: return 14.006747;
				case 8: return 15.99943;
				case 9: return 18.99840329;
				case 10: return 20.17976;
				case 11: return 22.9897686;
				case 12: return 24.30506;
				case 13: return 26.9815395;
				case 14: return 28.08553;
				case 15: return 30.9737624;
				case 16: return 32.0666;
				case 17: return 35.45279;
				case 18: return 39.9481;
				case 19: return 39.09831;
				case 20: return 40.0784;
				case 21: return 44.9559109;
				case 22: return 47.883;
				case 23: return 50.94151;
				case 24: return 51.99616;
				case 25: return 54.938051;
				case 26: return 55.8473;
				case 27: return 58.933201;
				case 28: return 58.69342;
				case 29: return 63.5463;
				case 30: return 65.392;
				case 31: return 69.7231;
				case 32: return 72.612;
				case 33: return 74.921592;
				case 34: return 78.963;
				case 35: return 79.9041;
				case 36: return 83.801;
				case 37: return 85.46783;
				case 38: return 87.621;
				case 39: return 88.905852;
				case 40: return 91.2242;
				case 41: return 92.906382;
				case 42: return 95.941;
				case 43: return 99;
				case 44: return 101.072;
				case 45: return 102.905503;
				case 46: return 106.421;
				case 47: return 107.86822;
				case 48: return 112.4118;
				case 49: return 114.8183;
				case 50: return 118.7107;
				case 51: return 121.7573;
				case 52: return 127.603;
				case 53: return 126.904473;
				case 54: return 131.292;
				case 55: return 132.905435;
				case 56: return 137.3277;
				case 57: return 138.90552;
				case 58: return 140.1154;
				case 59: return 140.907653;
				case 60: return 144.243;
				case 61: return 145;
				case 62: return 150.363;
				case 63: return 151.9659;
				case 64: return 157.253;
				case 65: return 158.925343;
				case 66: return 162.503;
				case 67: return 164.930323;
				case 68: return 167.263;
				case 69: return 168.934213;
				case 70: return 173.043;
				case 71: return 174.9671;
				case 72: return 178.492;
				case 73: return 180.94791;
				case 74: return 183.841;
				case 75: return 186.2071;
				case 76: return 190.233;
				case 77: return 192.223;
				case 78: return 195.083;
				case 79: return 196.966543;
				case 80: return 200.592;
				case 81: return 204.38332;
				case 82: return 207.21;
				case 83: return 208.980373;
				case 84: return 210;
				case 85: return 210;
				case 86: return 222;
				case 87: return 223;
				case 88: return 226;
				case 89: return 227;
				case 90: return 232.03811;
				case 91: return 231.035882;
				case 92: return 238.02891;
				case 93: return 237;
				case 94: return 239;
				case 95: return 243;
				case 96: return 247;
				case 97: return 247;
				case 98: return 252;
				case 99: return 252;
				case 100: return 257;
				case 101: return 256;
				case 102: return 259;
				case 103: return 260;
				default: return 0;
			}
			#endregion
		}

		/// <summary>
		/// 原子量を返す　引数は原子名
		/// </summary>
		/// <param name="atomicName"></param>
		/// <returns></returns>
		public static double AtomicWeight(string atomicName)
		{
			return AtomicWeight(AtomicNumber(atomicName));
		}

		/// <summary>
		/// イオン半径
		/// </summary>
		/// <param name="z"></param>
		/// <returns></returns>
		public static double AtomicRadius(int z)
		{
			#region
			switch (z)
			{
				case 0: return 0;
				case 1: return 1.007947;
				case 2: return 4.0026022;
				case 3: return 6.9412;
				case 4: return 9.0121823;
				case 5: return 10.8115;
				case 6: return 12.0111;
				case 7: return 14.006747;
				case 8: return 15.99943;
				case 9: return 18.99840329;
				case 10: return 20.17976;
				case 11: return 22.9897686;
				case 12: return 24.30506;
				case 13: return 26.9815395;
				case 14: return 28.08553;
				case 15: return 30.9737624;
				case 16: return 32.0666;
				case 17: return 35.45279;
				case 18: return 39.9481;
				case 19: return 39.09831;
				case 20: return 40.0784;
				case 21: return 44.9559109;
				case 22: return 47.883;
				case 23: return 50.94151;
				case 24: return 51.99616;
				case 25: return 54.938051;
				case 26: return 55.8473;
				case 27: return 58.933201;
				case 28: return 58.69342;
				case 29: return 63.5463;
				case 30: return 65.392;
				case 31: return 69.7231;
				case 32: return 72.612;
				case 33: return 74.921592;
				case 34: return 78.963;
				case 35: return 79.9041;
				case 36: return 83.801;
				case 37: return 85.46783;
				case 38: return 87.621;
				case 39: return 88.905852;
				case 40: return 91.2242;
				case 41: return 92.906382;
				case 42: return 95.941;
				case 43: return 99;
				case 44: return 101.072;
				case 45: return 102.905503;
				case 46: return 106.421;
				case 47: return 107.86822;
				case 48: return 112.4118;
				case 49: return 114.8183;
				case 50: return 118.7107;
				case 51: return 121.7573;
				case 52: return 127.603;
				case 53: return 126.904473;
				case 54: return 131.292;
				case 55: return 132.905435;
				case 56: return 137.3277;
				case 57: return 138.90552;
				case 58: return 140.1154;
				case 59: return 140.907653;
				case 60: return 144.243;
				case 61: return 145;
				case 62: return 150.363;
				case 63: return 151.9659;
				case 64: return 157.253;
				case 65: return 158.925343;
				case 66: return 162.503;
				case 67: return 164.930323;
				case 68: return 167.263;
				case 69: return 168.934213;
				case 70: return 173.043;
				case 71: return 174.9671;
				case 72: return 178.492;
				case 73: return 180.94791;
				case 74: return 183.841;
				case 75: return 186.2071;
				case 76: return 190.233;
				case 77: return 192.223;
				case 78: return 195.083;
				case 79: return 196.966543;
				case 80: return 200.592;
				case 81: return 204.38332;
				case 82: return 207.21;
				case 83: return 208.980373;
				case 84: return 210;
				case 85: return 210;
				case 86: return 222;
				case 87: return 223;
				case 88: return 226;
				case 89: return 227;
				case 90: return 232.03811;
				case 91: return 231.035882;
				case 92: return 238.02891;
				case 93: return 237;
				case 94: return 239;
				case 95: return 243;
				case 96: return 247;
				case 97: return 247;
				case 98: return 252;
				case 99: return 252;
				case 100: return 257;
				case 101: return 256;
				case 102: return 259;
				case 103: return 260;
				default: return 0;
			}
			#endregion
		}

		/// <summary>
		/// イオン半径
		/// </summary>
		/// <param name="AtomicName"></param>
		/// <returns></returns>
		public static double AtomicRadius(string AtomicName)
		{
			#region
			switch (AtomicName)
			{
				case "H": return 1.007947;
				case "He": return 4.0026022;
				case "Li": return 6.9412;
				case "Be": return 9.0121823;
				case "B": return 10.8115;
				case "C": return 12.0111;
				case "N": return 14.006747;
				case "O": return 15.99943;
				case "F": return 18.99840329;
				case "Ne": return 20.17976;
				case "Na": return 22.9897686;
				case "Mg": return 24.30506;
				case "Al": return 26.9815395;
				case "Si": return 28.08553;
				case "P": return 30.9737624;
				case "S": return 32.0666;
				case "Cl": return 35.45279;
				case "Ar": return 39.9481;
				case "K": return 39.09831;
				case "Ca": return 40.0784;
				case "Sc": return 44.9559109;
				case "Ti": return 47.883;
				case "V": return 50.94151;
				case "Cr": return 51.99616;
				case "Mn": return 54.938051;
				case "Fe": return 55.8473;
				case "Co": return 58.933201;
				case "Ni": return 58.69342;
				case "Cu": return 63.5463;
				case "Zn": return 65.392;
				case "Ga": return 69.7231;
				case "Ge": return 72.612;
				case "As": return 74.921592;
				case "Se": return 78.963;
				case "Br": return 79.9041;
				case "Kr": return 83.801;
				case "Rb": return 85.46783;
				case "Sr": return 87.621;
				case "Y": return 88.905852;
				case "Zr": return 91.2242;
				case "Nb": return 92.906382;
				case "Mo": return 95.941;
				case "Tc": return 99;
				case "Ru": return 101.072;
				case "Rh": return 102.905503;
				case "Pd": return 106.421;
				case "Ag": return 107.86822;
				case "Cd": return 112.4118;
				case "In": return 114.8183;
				case "Sn": return 118.7107;
				case "Sb": return 121.7573;
				case "Te": return 127.603;
				case "I": return 126.904473;
				case "Xe": return 131.292;
				case "Cs": return 132.905435;
				case "Ba": return 137.3277;
				case "La": return 138.90552;
				case "Ce": return 140.1154;
				case "Pr": return 140.907653;
				case "Nd": return 144.243;
				case "Pm": return 145;
				case "Sm": return 150.363;
				case "Eu": return 151.9659;
				case "Gd": return 157.253;
				case "Tb": return 158.925343;
				case "Dy": return 162.503;
				case "Ho": return 164.930323;
				case "Er": return 167.263;
				case "Tm": return 168.934213;
				case "Yb": return 173.043;
				case "Lu": return 174.9671;
				case "Hf": return 178.492;
				case "Ta": return 180.94791;
				case "W": return 183.841;
				case "Re": return 186.2071;
				case "Os": return 190.233;
				case "Ir": return 192.223;
				case "Pt": return 195.083;
				case "Au": return 196.966543;
				case "Hg": return 200.592;
				case "Tl": return 204.38332;
				case "Pb": return 207.21;
				case "Bi": return 208.980373;
				case "Po": return 210;
				case "At": return 210;
				case "Rn": return 222;
				case "Fr": return 223;
				case "Ra": return 226;
				case "Ac": return 227;
				case "Th": return 232.03811;
				case "Pa": return 231.035882;
				case "U": return 238.02891;
				case "Np": return 237;
				case "Pu": return 239;
				case "Am": return 243;
				case "Cm": return 247;
				case "Bk": return 247;
				case "Cf": return 252;
				case "Es": return 252;
				case "Fm": return 257;
				case "Md": return 256;
				case "No": return 259;
				case "Lr": return 260;
				default: return 0;
			}
			#endregion
		}

		/// <summary>
		/// 原子名を与えて、原子番号を返す. 文字列textから適宜原子名を抽出する.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static int AtomicNumber2(string text)
		{
			int number = 0;
			//2文字で検索
			for (int i = 0; i < text.Length - 1; i++)
			{
				number = AtomicNumber(text.Substring(i, 2));
				if (number != 0)
					return number;
			}
			for (int i = 0; i < text.Length; i++)
			{
				number = AtomicNumber(text.Substring(i, 1));
				if (number != 0)
					return number;
			}
			return number;
		}

		/// <summary>
		/// 原子名を与えて、原子番号を返す
		/// </summary>
		/// <param name="atomicName"></param>
		/// <returns></returns>
		public static int AtomicNumber(string atomicName, bool caseSensitive = true)
		{
			if (!caseSensitive)
			{
				atomicName = atomicName.ToLower();
				System.Globalization.TextInfo ti = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
				atomicName = ti.ToTitleCase(atomicName);
			}
			#region
			switch (atomicName)
			{
				case "H": return 1;
				case "He": return 2;
				case "Li": return 3;
				case "Be": return 4;
				case "B": return 5;
				case "C": return 6;
				case "N": return 7;
				case "O": return 8;
				case "F": return 9;
				case "Ne": return 10;
				case "Na": return 11;
				case "Mg": return 12;
				case "Al": return 13;
				case "Si": return 14;
				case "P": return 15;
				case "S": return 16;
				case "Cl": return 17;
				case "Ar": return 18;
				case "K": return 19;
				case "Ca": return 20;
				case "Sc": return 21;
				case "Ti": return 22;
				case "V": return 23;
				case "Cr": return 24;
				case "Mn": return 25;
				case "Fe": return 26;
				case "Co": return 27;
				case "Ni": return 28;
				case "Cu": return 29;
				case "Zn": return 30;
				case "Ga": return 31;
				case "Ge": return 32;
				case "As": return 33;
				case "Se": return 34;
				case "Br": return 35;
				case "Kr": return 36;
				case "Rb": return 37;
				case "Sr": return 38;
				case "Y": return 39;
				case "Zr": return 40;
				case "Nb": return 41;
				case "Mo": return 42;
				case "Tc": return 43;
				case "Ru": return 44;
				case "Rh": return 45;
				case "Pd": return 46;
				case "Ag": return 47;
				case "Cd": return 48;
				case "In": return 49;
				case "Sn": return 50;
				case "Sb": return 51;
				case "Te": return 52;
				case "I": return 53;
				case "Xe": return 54;
				case "Cs": return 55;
				case "Ba": return 56;
				case "La": return 57;
				case "Ce": return 58;
				case "Pr": return 59;
				case "Nd": return 60;
				case "Pm": return 61;
				case "Sm": return 62;
				case "Eu": return 63;
				case "Gd": return 64;
				case "Tb": return 65;
				case "Dy": return 66;
				case "Ho": return 67;
				case "Er": return 68;
				case "Tm": return 69;
				case "Yb": return 70;
				case "Lu": return 71;
				case "Hf": return 72;
				case "Ta": return 73;
				case "W": return 74;
				case "Re": return 75;
				case "Os": return 76;
				case "Ir": return 77;
				case "Pt": return 78;
				case "Au": return 79;
				case "Hg": return 80;
				case "Tl": return 81;
				case "Pb": return 82;
				case "Bi": return 83;
				case "Po": return 84;
				case "At": return 85;
				case "Rn": return 86;
				case "Fr": return 87;
				case "Ra": return 88;
				case "Ac": return 89;
				case "Th": return 90;
				case "Pa": return 91;
				case "U": return 92;
				case "Np": return 93;
				case "Pu": return 94;
				case "Am": return 95;
				case "Cm": return 96;
				case "Bk": return 97;
				case "Cf": return 98;
				case "Es": return 99;
				case "Fm": return 100;
				case "Md": return 101;
				case "No": return 102;
				case "Lr": return 103;
				default: return 0;
			}
			#endregion
		}

		/// <summary>
		/// 原子番号を与えて、原子名を返す
		/// </summary>
		/// <param name="z"></param>
		/// <returns></returns>
		public static string AtomicName(int z)
		{
			#region
			switch (z)
			{
				case 1: return "H";
				case 2: return "He";
				case 3: return "Li";
				case 4: return "Be";
				case 5: return "B";
				case 6: return "C";
				case 7: return "N";
				case 8: return "O";
				case 9: return "F";
				case 10: return "Ne";
				case 11: return "Na";
				case 12: return "Mg";
				case 13: return "Al";
				case 14: return "Si";
				case 15: return "P";
				case 16: return "S";
				case 17: return "Cl";
				case 18: return "Ar";
				case 19: return "K";
				case 20: return "Ca";
				case 21: return "Sc";
				case 22: return "Ti";
				case 23: return "V";
				case 24: return "Cr";
				case 25: return "Mn";
				case 26: return "Fe";
				case 27: return "Co";
				case 28: return "Ni";
				case 29: return "Cu";
				case 30: return "Zn";
				case 31: return "Ga";
				case 32: return "Ge";
				case 33: return "As";
				case 34: return "Se";
				case 35: return "Br";
				case 36: return "Kr";
				case 37: return "Rb";
				case 38: return "Sr";
				case 39: return "Y";
				case 40: return "Zr";
				case 41: return "Nb";
				case 42: return "Mo";
				case 43: return "Tc";
				case 44: return "Ru";
				case 45: return "Rh";
				case 46: return "Pd";
				case 47: return "Ag";
				case 48: return "Cd";
				case 49: return "In";
				case 50: return "Sn";
				case 51: return "Sb";
				case 52: return "Te";
				case 53: return "I";
				case 54: return "Xe";
				case 55: return "Cs";
				case 56: return "Ba";
				case 57: return "La";
				case 58: return "Ce";
				case 59: return "Pr";
				case 60: return "Nd";
				case 61: return "Pm";
				case 62: return "Sm";
				case 63: return "Eu";
				case 64: return "Gd";
				case 65: return "Tb";
				case 66: return "Dy";
				case 67: return "Ho";
				case 68: return "Er";
				case 69: return "Tm";
				case 70: return "Yb";
				case 71: return "Lu";
				case 72: return "Hf";
				case 73: return "Ta";
				case 74: return "W";
				case 75: return "Re";
				case 76: return "Os";
				case 77: return "Ir";
				case 78: return "Pt";
				case 79: return "Au";
				case 80: return "Hg";
				case 81: return "Tl";
				case 82: return "Pb";
				case 83: return "Bi";
				case 84: return "Po";
				case 85: return "At";
				case 86: return "Rn";
				case 87: return "Fr";
				case 88: return "Ra";
				case 89: return "Ac";
				case 90: return "Th";
				case 91: return "Pa";
				case 92: return "U";
				case 93: return "Np";
				case 94: return "Pu";
				case 95: return "Am";
				case 96: return "Cm";
				case 97: return "Bk";
				case 98: return "Cf";
				case 99: return "Es";
				case 100: return "Fm";
				case 101: return "Md";
				case 102: return "No";
				case 103: return "Lr";
				default: return "";
			}
			#endregion
		}

		public class ElasticScattering
		{
			/// <summary>
			/// Valence　価数
			/// </summary>
			public int Valence;

			/// <summary>
			/// 散乱因子の計算方法
			/// </summary>
			public string Method;

			/// <summary>
			/// 引数がS2 (単位: nm^-2), 戻り値が振幅 (単位: nm)の関数
			/// </summary>
			public Func<double, double> Factor { get; set; }

			/// <summary>
			/// 引数がS2 (単位: nm^-2), m (単位: nm^2), 戻り値が無次元量の関数
			/// </summary>
			public Func<double, double, double> FactorImaginary { get; set; }

			/// <summary>
			/// 引数が r (原子の中心からの距離)、戻り値(単位: volt * angstrom)が投影ポテンシャルの関数
			/// </summary>
			public Func<double, double> ProjectedPotential;

			//X線用のコンストラクタ
			public ElasticScattering(double a1, double b1, double a2, double b2, double a3, double b3, double a4, double b4, double c, int valence, string methods)
			{
				Valence = valence;
				Method = methods;
			
				var a = new[] { a1, a2, a3, a4 };
				var b = new[] { b1, b2, b3, b4 };
				Factor = new Func<double, double>(s2 =>
				{
					s2 *= 0.01;//単位を修正
					var result = 0.0;
					for (int i = 0; i < a.Length; i++)
						result += a[i] * Math.Exp(-s2 * b[i]);
					return (result + c) * 0.1;
				});
			}

			//電子線用のコンストラクタ (Five gaussian)
			public ElasticScattering(double a1, double a2, double a3, double a4, double a5, double b1, double b2, double b3, double b4, double b5, int valence, string methods)
			{
				Valence = valence;
				Method = methods;

				var a = new[] { a1, a2, a3, a4, a5 };
				var b = new[] { b1, b2, b3, b4, b5 };

				Factor = new Func<double, double>(s2 =>
				{
					s2 *= 0.01;//単位を修正
					var result = 0.0;
					for (int i = 0; i < a.Length; i++)
						result += a[i] * Math.Exp(-s2 * b[i]);
					return result * 0.1;
				});

				FactorImaginary = new Func<double, double, double>((s2, m) =>
				{
					s2 *= 0.01;//単位を修正
					m *= 100;//単位を修正
					var f = 0.0;
					for (int i = 0; i < a.Length; i++)
						for (int j = 0; j < a.Length; j++)
						{
							var sum = b[i] + b[j];
							var product = b[i] * b[j];
							if (sum != 0)
								f += a[i] * a[j] * (Math.Exp(-s2 * product / sum) / sum - Math.Exp(-s2 * (product - m * m) / (sum + 2 * m)) / (sum + 2 * m));
						}
					return Math.PI * f;
				}
				);
			}

			//電子線用のコンストラクタ (Eight gaussian)
			public ElasticScattering(double a1, double a2, double a3, double a4, double a5, double a6, double a7, double a8, double b1, double b2, double b3, double b4, double b5, double b6, double b7, double b8)
			{
				Valence = 0;
				Method = "";

				var a = new[] { a1, a2, a3, a4, a5, a6, a7, a8 };
				var b = new[] { b1, b2, b3, b4, b5, b6, b7, b8 };
				
				Factor = new Func<double, double>(s2 =>
				{
					s2 *= 0.01;//単位を修正
					var result = 0.0;
					for (int i = 0; i < a.Length; i++)
						result += a[i] * Math.Exp(-s2 * b[i]);
					return result * 0.1;
				});


				FactorImaginary = new Func<double, double, double>((s2, m) =>
				{
					s2 *= 100;//単位を修正
					m *= 100;//単位を修正
					var f = 0.0;
					for (int i = 0; i < a.Length; i++)
						for (int j = 0; j < a.Length; j++)
						{
							var sum = b[i] + b[j];
							var product = b[i] * b[j];
							if (sum != 0)
								f += a[i] * a[j] * (Math.Exp(-s2 * product / sum) / sum - Math.Exp(-s2 * (product - m * m) / (sum + 2 * m)) / (sum + 2 * m));
						}
					return Math.PI * f;
				}
				);
			}

			//電子線用のコンストラクタ (3 lorentian , 3 gaussian)
			public ElasticScattering(double a1, double a2, double a3, double b1, double b2, double b3, double c1, double c2, double c3, double d1, double d2, double d3)
			{
				Factor = new Func<double, double>(
					S2 => a1 / (S2 + b1) + a2 / (S2 + b2) + a3 / (S2 + b3) + c1 * Math.Exp(-S2 * d1) + c2 * Math.Exp(-S2 * d2) + c3 * Math.Exp(-S2 * d3));

				double a0 = UniversalConstants.a0 * 1E10, e = UniversalConstants.Ry * 2 * a0;
				double sqrtB1 = Math.Sqrt(b1), sqrtB2 = Math.Sqrt(b2), sqrtB3 = Math.Sqrt(b3);
				double pi2 = Math.PI * Math.PI;
				double d1inv = 1 / d1, d2inv = 1 / d2, d3inv = 1 / d3;
				ProjectedPotential = new Func<double, double>(
					r => r <= 0 ? 0 :
						2 * pi2 * a0 * e * (
					2 * a1 * MathNet.Numerics.SpecialFunctions.BesselK0(2 * Math.PI * r * sqrtB1) +
					2 * a2 * MathNet.Numerics.SpecialFunctions.BesselK0(2 * Math.PI * r * sqrtB2) +
					2 * a3 * MathNet.Numerics.SpecialFunctions.BesselK0(2 * Math.PI * r * sqrtB3) +
					c1 * d1inv * Math.Exp(-pi2 * r * r * d1inv) +
					c2 * d2inv * Math.Exp(-pi2 * r * r * d2inv) +
					c3 * d3inv * Math.Exp(-pi2 * r * r * d3inv)
					));
			}
		}

		/// <summary>
		/// X線による原子散乱因子 XrayScattering[AtomicNumber][SubNumber]
		/// </summary>
		public static readonly ElasticScattering[][] XrayScattering = new ElasticScattering[][]{
		#region
	new ElasticScattering[]{
		new ElasticScattering(0,0,0,0,0,0,0,0,0,0,"Unknown: ")},
	new ElasticScattering[]{
		new ElasticScattering(0.493002,10.5109,0.322912,26.1257,0.140191,3.14236,0.04081,57.7997,0.003038,0,"H: "),
		new ElasticScattering(0.897661,53.1368,0.565616,15.187,0.415815,186.576,0.116973,3.56709,0.002389,-1,"H1-: HF")},
	new ElasticScattering[]{
		new ElasticScattering(0.8734,9.1037,0.6309,3.3568,0.3112,22.9276,0.178,0.9821,0.0064,0,"He: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(1.1282,3.9546,0.7508,1.0524,0.6175,85.3905,0.4653,168.261,0.0377,0,"Li: RHF"),
		new ElasticScattering(0.6968,4.6237,0.7888,1.9557,0.3414,0.6316,0.1563,10.0953,0.0167,+1,"Li1+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(1.5919,43.6427,1.1278,1.8623,0.5391,103.483,0.7029,0.542,0.0385,0,"Be: RHF"),
		new ElasticScattering(6.2603,0.0027,0.8849,0.8313,0.7993,2.2758,0.1647,5.1146,-6.1092,+2,"Be2+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(2.0545,23.2185,1.3326,1.021,1.0979,60.3498,0.7068,0.1403,-0.1932,0,"B: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(2.31,20.8439,1.02,10.2075,1.5886,0.5687,0.865,51.6512,0.2156,0,"C: RHF"),
		new ElasticScattering(2.26069,22.6907,1.56165,0.656665,1.05075,9.75618,0.839259,55.5949,0.286977,0,"C val: HF")},
	new ElasticScattering[]{
		new ElasticScattering(12.2126,0.0057,3.1322,9.8933,2.0125,28.9975,1.1663,0.5826,-11.529,0,"N: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(3.0485,13.2771,2.2868,5.7011,1.5463,0.3239,0.867,32.9089,0.2508,0,"O: RHF"),
		new ElasticScattering(4.1916,12.8573,1.63969,4.17236,1.52673,47.0179,-20.307,-0.01404,21.9412,-1,"O1-: HF")},
	new ElasticScattering[]{
		new ElasticScattering(3.5392,10.2825,2.6412,4.2944,1.517,0.2615,1.0243,26.1476,0.2776,0,"F: RHF"),
		new ElasticScattering(3.6322,5.27756,3.51057,14.7353,1.26064,0.442258,0.940706,47.3437,0.653396,-1,"F1-: HF")},
	new ElasticScattering[]{
		new ElasticScattering(3.9553,8.4042,3.1125,3.4262,1.4546,0.2306,1.1251,21.7184,0.3515,0,"Ne: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(4.7626,3.285,3.1736,8.8422,1.2674,0.3136,1.1128,129.424,0.676,0,"Na: RHF"),
		new ElasticScattering(3.2565,2.6671,3.9362,6.1153,1.3998,0.2001,1.0032,14.039,0.404,+1,"Na1+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(5.4204,2.8275,2.1735,79.2611,1.2269,0.3808,2.3073,7.1937,0.8584,0,"Mg: RHF"),
		new ElasticScattering(3.4988,2.1676,3.8378,4.7542,1.3284,0.185,0.8497,10.1411,0.4853,+2,"Mg2+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(6.4202,3.0387,1.9002,0.7426,1.5936,31.5472,1.9646,85.0886,1.1151,0,"Al: RHF"),
		new ElasticScattering(4.17448,1.93816,3.3876,4.14553,1.20296,0.228753,0.528137,8.28524,0.706786,+3,"Al3+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(6.2915,2.4386,3.0353,32.3337,1.9891,0.6785,1.541,81.6937,1.1407,0,"Si: RHF"),
		new ElasticScattering(4.43918,1.64167,3.20345,3.43757,1.19453,0.2149,0.41653,6.65365,0.746297,+4,"Si4+: HF"),
		new ElasticScattering(5.66269,2.6652,3.07164,38.6634,2.62446,0.916946,1.3932,93.5458,1.24707,0,"Si val: HF")},
	new ElasticScattering[]{
		new ElasticScattering(6.4345,1.9067,4.1791,27.157,1.78,0.526,1.4908,68.1645,1.1149,0,"P: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(6.9053,1.4679,5.2034,22.2151,1.4379,0.2536,1.5863,56.172,0.8669,0,"S: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(11.4604,0.0104,7.1964,1.1662,6.2556,18.5194,1.6455,47.7784,-9.5574,0,"Cl: RHF"),
		new ElasticScattering(18.2915,0.0066,7.2084,1.1717,6.5337,19.5424,2.3386,60.4486,-16.378,-1,"Cl1-: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(7.4845,0.9072,6.7723,14.8407,0.6539,43.8983,1.6442,33.3929,1.4445,0,"Ar: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(8.2186,12.7949,7.4398,0.7748,1.0519,213.187,0.8659,41.6841,1.4228,0,"K: RHF"),
		new ElasticScattering(7.9578,12.6331,7.4917,0.7674,6.359,-0.002,1.1915,31.9128,-4.9978,+1,"K1+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(8.6266,10.4421,7.3873,0.6599,1.5899,85.7484,1.0211,178.437,1.3751,0,"Ca: RHF"),
		new ElasticScattering(15.6348,-0.0074,7.9518,0.6089,8.4372,10.3116,0.8537,25.9905,-14.875,+2,"Ca2+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(9.189,9.0213,7.3679,0.5729,1.6409,136.108,1.468,51.3531,1.3329,0,"Sc: RHF"),
		new ElasticScattering(13.4008,0.29854,8.0273,7.9629,1.65943,-0.28604,1.57936,16.0662,-6.6667,+3,"Sc3+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(9.7595,7.8508,7.3558,0.5,1.6991,35.6338,1.9021,116.105,1.2807,0,"Ti: "),
		new ElasticScattering(9.11423,7.5243,7.62174,0.457585,2.2793,19.5361,0.087899,61.6558,0.897155,+2,"Ti2+: HF"),
		new ElasticScattering(17.7344,0.22061,8.73816,7.04716,5.25691,-0.15762,1.92134,15.9768,-14.652,+3,"Ti3+: HF"),
		new ElasticScattering(19.5114,0.178847,8.23473,6.67018,2.01341,-0.29263,1.5208,12.9464,-13.28,+4,"Ti4+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(10.2971,6.8657,7.3511,0.4385,2.0703,26.8938,2.0571,102.478,1.2199,0,"V: RHF"),
		new ElasticScattering(10.106,6.8818,7.3541,0.4409,2.2884,20.3004,0.0223,115.122,1.2298,+2,"V2+: RHF"),
		new ElasticScattering(9.43141,6.39535,7.7419,0.383349,2.15343,15.1908,0.016865,63.969,0.656565,+3,"V3+: HF"),
		new ElasticScattering(15.6887,0.679003,8.14208,5.40135,2.03081,9.97278,-9.576,0.940464,1.7143,+5,"V5+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(10.6406,6.1038,7.3537,0.392,3.324,20.2626,1.4922,98.7399,1.1832,0,"Cr: RHF"),
		new ElasticScattering(9.54034,5.66078,7.7509,0.344261,3.58274,13.3075,0.509107,32.4224,0.616898,+2,"Cr2+: HF"),
		new ElasticScattering(9.6809,5.59463,7.81136,0.334393,2.87603,12.8288,0.113575,32.8761,0.518275,+3,"Cr3+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(11.2819,5.3409,7.3573,0.3432,3.0193,17.8674,2.2441,83.7543,1.0896,0,"Mn: RHF"),
		new ElasticScattering(10.8061,5.2796,7.362,0.3435,3.5268,14.343,0.2184,41.3235,1.0874,+2,"Mn2+: RHF"),
		new ElasticScattering(9.84521,4.91797,7.87194,0.294393,3.56531,10.8171,0.323613,24.1281,0.393974,+3,"Mn3+: HF"),
		new ElasticScattering(9.96253,4.8485,7.97057,0.283303,2.76067,10.4852,0.054447,27.573,0.251877,+4,"Mn4+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(11.7695,4.7611,7.3573,0.3072,3.5222,15.3535,2.3045,76.8805,1.0369,0,"Fe: RHF"),
		new ElasticScattering(11.0424,4.6538,7.374,0.3053,4.1346,12.0546,0.4399,31.2809,1.0097,+2,"Fe2+: RHF"),
		new ElasticScattering(11.1764,4.6147,7.3863,0.3005,3.3948,11.6729,0.0724,38.5566,0.9707,+3,"Fe3+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(12.2841,4.2791,7.3409,0.2784,4.0034,13.5359,2.3488,71.1692,1.0118,0,"Co: RHF"),
		new ElasticScattering(11.2296,4.1231,7.3883,0.2726,4.7393,10.2443,0.7108,25.6466,0.9324,+2,"Co2+: RHF"),
		new ElasticScattering(10.338,3.90969,7.88173,0.238668,4.76795,8.35583,0.725591,18.3491,118.349,+3,"Co3+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(12.8376,3.8785,7.292,0.2565,4.4438,12.1763,2.38,66.3421,1.0341,0,"Ni: RHF"),
		new ElasticScattering(11.4166,3.6766,7.4005,0.2449,5.3442,8.873,0.9773,22.1626,0.8614,+2,"Ni2+: RHF"),
		new ElasticScattering(10.7806,3.5477,7.75868,0.22314,5.22746,7.64468,0.847114,16.9673,0.386044,+3,"Ni3+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(13.338,3.5828,7.1676,0.247,5.6158,11.3966,1.6735,64.8126,1.191,0,"Cu: RHF"),
		new ElasticScattering(11.9475,3.3669,7.3573,0.2274,6.2455,8.6625,1.5578,25.8487,0.89,+1,"Cu1+: RHF"),
		new ElasticScattering(11.8168,3.37484,7.11181,0.244078,5.78135,7.9876,1.14523,19.897,1.14431,+2,"Cu2+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(14.0743,3.2655,7.0318,0.2333,5.1652,10.3163,2.41,58.7097,1.3041,0,"Zn: RHF"),
		new ElasticScattering(11.9719,2.9946,7.3862,0.2031,6.4668,7.0826,1.394,18.0995,0.7807,+2,"Zn2+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(15.2354,3.0669,6.7006,0.2412,4.3591,10.7805,2.9623,61.4135,1.7189,0,"Ga: RHF"),
		new ElasticScattering(12.692,2.81262,6.69883,0.22789,6.06692,6.36441,1.0066,14.4122,1.53545,+3,"Ga3+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(16.0816,2.8509,6.3747,0.2516,3.7068,11.4468,3.683,54.7625,2.1313,0,"Ge: RHF"),
		new ElasticScattering(12.9172,2.53718,6.70003,0.205855,6.06791,5.47913,0.859041,11.603,1.45572,+4,"Ge4+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(16.6723,2.6345,6.0701,0.2647,3.4313,12.9479,4.2779,47.7972,2.531,0,"As: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(17.0006,2.4098,5.8196,0.2726,3.9731,15.2372,4.3543,43.8163,2.8409,0,"Se: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(17.1789,2.1723,5.2358,16.5796,5.6377,0.2609,3.9851,41.4328,2.9557,0,"Br: RHF"),
		new ElasticScattering(17.1718,2.2059,6.3338,19.3345,5.5754,0.2871,3.7272,58.1535,3.1776,-1,"Br1-: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(17.3555,1.9384,6.7286,16.5623,5.5493,0.2261,3.5375,39.3972,2.825,0,"Kr: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(17.1784,1.7888,9.6435,17.3151,5.1399,0.2748,1.5292,164.934,3.4873,0,"Rb: RHF"),
		new ElasticScattering(17.5816,1.7139,7.6598,14.7957,5.8981,0.1603,2.7817,31.2087,2.0782,+1,"Rb1+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(17.5663,1.5564,9.8184,14.0988,5.422,0.1664,2.6694,132.376,2.5064,0,"Sr: RHF"),
		new ElasticScattering(18.0874,1.4907,8.1373,12.6963,2.5654,24.5651,-34.193,-0.0138,41.4025,+2,"Sr2+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(17.776,1.4029,10.2946,12.8006,5.72629,0.125599,3.26588,104.354,1.91213,0,"Y: *RHF"),
		new ElasticScattering(17.9268,1.35417,9.1531,11.2145,1.76795,22.6599,-33.108,-0.01319,40.2602,+3,"Y3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(17.8765,1.27618,10.948,11.916,5.41732,0.117622,3.65721,87.6627,2.06929,0,"Zr: *RHF"),
		new ElasticScattering(18.1668,1.2148,10.0562,10.1483,1.01118,21.6054,-2.6479,-0.10276,9.41454,+4,"Zr4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(17.6142,1.18865,12.0144,11.766,4.04183,0.204785,3.53346,69.7957,3.75591,0,"Nb: *RHF"),
		new ElasticScattering(19.8812,0.019175,18.0653,1.13305,11.0177,10.1621,1.94715,28.3389,-12.912,+3,"Nb3+: *DS"),
		new ElasticScattering(17.9163,1.12446,13.3417,0.028781,10.799,9.28206,0.337905,25.7228,-6.3934,+5,"Nb5+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(3.7025,0.2772,17.2356,1.0958,12.8876,11.004,3.7429,61.6584,4.3875,0,"Mo: RHF"),
		new ElasticScattering(21.1664,0.014734,18.2017,1.03031,11.7423,9.53659,2.30951,26.6307,-14.421,+3,"Mo3+: *DS"),
		new ElasticScattering(21.0149,0.014345,18.0992,1.02238,11.4632,8.78809,0.740625,23.3452,-14.316,+5,"Mo5+: *DS"),
		new ElasticScattering(17.8871,1.03649,11.175,8.48061,6.57891,0.058881,0,0,0.344941,+6,"Mo6+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(19.1301,0.864132,11.0948,8.14487,4.64901,21.5707,2.71263,86.8472,5.40428,0,"Tc: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(19.2674,0.80852,12.9182,8.43467,4.86337,24.7997,1.56756,94.2928,5.37874,0,"Ru: *RHF"),
		new ElasticScattering(18.5638,0.847329,13.2885,8.37164,9.32602,0.017662,3.00964,22.887,-3.1892,+3,"Ru3+: *DS"),
		new ElasticScattering(18.5003,0.844582,13.1787,8.12534,4.71304,0.036495,2.18535,20.8504,1.42357,+4,"Ru4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(19.2957,0.751536,14.3501,8.21758,4.73425,25.8749,1.28918,98.6062,5.328,0,"Rh: *RHF"),
		new ElasticScattering(18.8785,0.764252,14.1259,7.84438,3.32515,21.2487,-6.1989,-0.01036,11.8678,+3,"Rh3+: *DS"),
		new ElasticScattering(18.8545,0.760825,13.9806,7.62436,2.53464,19.3317,-5.6526,-0.0102,11.2835,+4,"Rh4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(19.3319,0.698655,15.5017,7.98929,5.29537,25.2052,0.605844,76.8986,5.26593,0,"Pd: *RHF"),
		new ElasticScattering(19.1701,0.696219,15.2096,7.55573,4.32234,22.5057,0,0,5.2916,+2,"Pd2+: *DS"),
		new ElasticScattering(19.2493,0.683839,14.79,7.14833,2.89289,17.9144,-7.9492,0.005127,13.0174,+4,"Pd4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(19.2808,0.6446,16.6885,7.4726,4.8045,24.6605,1.0463,99.8156,5.179,0,"Ag: RHF"),
		new ElasticScattering(19.1812,0.646179,15.9719,7.19123,5.27475,21.7326,0.357534,66.1147,5.21572,+1,"Ag1+: *DS"),
		new ElasticScattering(19.1643,0.645643,16.2456,7.18544,4.3709,21.4072,0,0,5.21404,+2,"Ag2+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(19.2214,0.5946,17.6444,6.9089,4.461,24.7008,1.6029,87.4825,5.0694,0,"Cd: RHF"),
		new ElasticScattering(19.1514,0.597922,17.2535,6.80639,4.47128,20.2521,0,0,5.11937,+2,"Cd2+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(19.1624,0.5476,18.5596,6.3776,4.2948,25.8499,2.0396,92.8029,4.9391,0,"In: RHF"),
		new ElasticScattering(19.1045,0.551522,18.1108,6.3247,3.78897,17.3595,0,0,4.99635,+3,"In3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(19.1889,5.8303,19.1005,0.5031,4.4585,26.8909,2.4663,83.9571,4.7821,0,"Sn: RHF"),
		new ElasticScattering(19.1094,0.5036,19.0548,5.8378,4.5648,23.3752,0.487,62.2061,4.7861,+2,"Sn2+: RHF"),
		new ElasticScattering(18.9333,5.764,19.7131,0.4655,3.4182,14.0049,0.0193,-0.7583,3.9182,+4,"Sn4+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(19.6418,5.3034,19.0455,0.4607,5.0371,27.9074,2.6827,75.2825,4.5909,0,"Sb: RHF"),
		new ElasticScattering(18.9755,0.467196,18.933,5.22126,5.10789,19.5902,0.288753,55.5113,4.69626,+3,"Sb3+: *DS"),
		new ElasticScattering(19.8685,5.44853,19.0302,0.467973,2.41253,14.1259,0,0,4.69263,+5,"Sb5+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(19.9644,4.81742,19.0138,0.420885,6.14487,28.5284,2.5239,70.8403,4.352,0,"Te: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(20.1472,4.347,18.9949,0.3814,7.5138,27.766,2.2735,66.8776,4.0712,0,"I: RHF"),
		new ElasticScattering(20.2332,4.3579,18.997,0.3815,7.8069,29.5259,2.8868,84.9304,4.0714,-1,"I1-: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(20.2933,3.9282,19.0298,0.344,8.9767,26.4659,1.99,64.2658,3.7118,0,"Xe: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(20.3892,3.569,19.1062,0.3107,10.662,24.3879,1.4953,213.904,3.3352,0,"Cs: RHF"),
		new ElasticScattering(20.3524,3.552,19.1278,0.3086,10.2821,23.7128,0.9615,59.4565,3.2791,+1,"Cs1+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(20.3361,3.216,19.297,0.2756,10.888,20.2073,2.6959,167.202,2.7731,0,"Ba: RHF"),
		new ElasticScattering(20.1807,3.21367,19.1136,0.28331,10.9054,20.0558,0.773634,51.746,3.02902,+2,"Ba2+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(20.578,2.94817,19.599,0.244475,11.3727,18.7726,3.28719,133.124,2.14678,0,"La: *RHF"),
		new ElasticScattering(20.2489,2.9207,19.3763,0.250698,11.6323,17.8211,0.336048,54.9453,2.4086,+3,"La3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(21.1671,2.81219,19.7695,0.226836,11.8513,17.6083,3.33049,127.113,1.86264,0,"Ce: *RHF"),
		new ElasticScattering(20.8036,2.77691,19.559,0.23154,11.9369,16.5408,0.612376,43.1692,2.09013,+3,"Ce3+: *DS"),
		new ElasticScattering(20.3235,2.65941,19.8186,0.21885,12.1233,15.7992,0.144583,62.2355,1.5918,+4,"Ce4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(22.044,2.77393,19.6697,0.222087,12.3856,16.7669,2.82428,143.644,2.0583,0,"Pr: *RHF"),
		new ElasticScattering(21.3727,2.6452,19.7491,0.214299,12.1329,15.323,0.97518,36.4065,1.77132,+3,"Pr3+: *DS"),
		new ElasticScattering(20.9413,2.54467,20.0539,0.202481,12.4668,14.8137,0.296689,45.4643,1.24285,+4,"Pr4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(22.6845,2.66248,19.6847,0.210628,12.774,15.885,2.85137,137.903,1.98486,0,"Nd: *RHF"),
		new ElasticScattering(21.961,2.52722,19.9339,0.199237,12.12,14.1783,1.51031,30.8717,1.47588,+3,"Nd3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(23.3405,2.5627,19.6095,0.202088,13.1235,15.1009,2.87516,132.721,2.02876,0,"Pm: *RHF"),
		new ElasticScattering(22.5527,2.4174,20.1108,0.185769,12.0671,13.1275,2.07492,27.4491,1.19499,+3,"Pm3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(24.0042,2.47274,19.4258,0.196451,13.4396,14.3996,2.89604,128.007,2.20963,0,"Sm: *RHF"),
		new ElasticScattering(23.1504,2.31641,20.2599,0.174081,11.9202,12.1571,2.71488,24.8242,0.954586,+3,"Sm3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(24.6274,2.3879,19.0886,0.1942,13.7603,13.7546,2.9227,123.174,2.5745,0,"Eu: RHF"),
		new ElasticScattering(24.0063,2.27783,19.9504,0.17353,11.8034,11.6096,3.87243,26.5156,1.36389,+2,"Eu2+: *DS"),
		new ElasticScattering(23.7497,2.22258,20.3745,0.16394,11.8509,11.311,3.26503,22.9966,0.759344,+3,"Eu3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(25.0709,2.25341,19.0798,0.181951,13.8518,12.9331,3.54545,101.398,2.4196,0,"Gd: *RHF"),
		new ElasticScattering(24.3466,2.13553,20.4208,0.155525,11.8708,10.5782,3.7149,21.7029,0.645089,+3,"Gd3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(25.8976,2.24256,18.2185,0.196143,14.3167,12.6648,2.95354,115.362,3.58324,0,"Tb: *RHF"),
		new ElasticScattering(24.9559,2.05601,20.3271,0.149525,12.2471,10.0499,3.773,21.2773,0.691967,+3,"Tb3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(26.507,2.1802,17.6383,0.202172,14.5596,12.1899,2.96577,111.874,4.29728,0,"Dy: *RHF"),
		new ElasticScattering(25.5395,1.9804,20.2861,0.143384,11.9812,9.34972,4.50073,19.581,0.68969,+3,"Dy3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(26.9049,2.07051,17.294,0.19794,14.5583,11.4407,3.63837,92.6566,4.56796,0,"Ho: *RHF"),
		new ElasticScattering(26.1296,1.91072,20.0994,0.139358,11.9788,8.80018,4.93676,18.5908,0.852795,+3,"Ho3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(27.6563,2.07356,16.4285,0.223545,14.9779,11.3604,2.98233,105.703,5.92046,0,"Er: *RHF"),
		new ElasticScattering(26.722,1.84659,19.7748,0.13729,12.1506,8.36225,5.17379,17.8974,1.17613,+3,"Er3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(28.1819,2.02859,15.8851,0.238849,15.1542,10.9975,2.98706,102.961,6.75621,0,"Tm: *RHF"),
		new ElasticScattering(27.3083,1.78711,19.332,0.136974,12.3339,7.96778,5.38348,17.2922,1.63929,+3,"Tm3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(28.6641,1.9889,15.4345,0.257119,15.3087,10.6647,2.98963,100.417,7.56672,0,"Yb: *RHF"),
		new ElasticScattering(28.1209,1.78503,17.6817,0.15997,13.3335,8.18304,5.14657,20.39,3.70983,+2,"Yb2+: *DS"),
		new ElasticScattering(27.8917,1.73272,18.7614,0.13879,12.6072,7.64412,5.47647,16.8153,2.26001,+3,"Yb3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(28.9476,1.90182,15.2208,9.98519,15.1,0.261033,3.71601,84.3298,7.97628,0,"Lu: *RHF"),
		new ElasticScattering(28.4628,1.68216,18.121,0.142292,12.8429,7.33727,5.59415,16.3535,2.97573,+3,"Lu3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(29.144,1.83262,15.1726,9.5999,14.7586,0.275116,4.30013,72.029,8.58154,0,"Hf: *RHF"),
		new ElasticScattering(28.8131,1.59136,18.4601,0.128903,12.7285,6.76232,5.59927,14.0366,2.39699,+4,"Hf4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(29.2024,1.77333,15.2293,9.37046,14.5135,0.295977,4.76492,63.3644,9.24354,0,"Ta: *RHF"),
		new ElasticScattering(29.1587,1.50711,18.8407,0.116741,12.8268,6.31524,5.38695,12.4244,1.78555,+5,"Ta5+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(29.0818,1.72029,15.43,9.2259,14.4327,0.321703,5.11982,57.056,9.8875,0,"W: *RHF"),
		new ElasticScattering(29.4936,1.42755,19.3763,0.104621,13.0544,5.93667,5.06412,11.1972,1.01074,+6,"W6+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(28.7621,1.67191,15.7189,9.09227,14.5564,0.3505,5.44174,52.0861,10.472,0,"Re: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(28.1894,1.62903,16.155,8.97948,14.9305,0.382661,5.67589,48.1647,11.0005,0,"Os: *RHF"),
		new ElasticScattering(30.419,1.37113,15.2637,6.84706,14.7458,0.165191,5.06795,18.003,6.49804,+4,"Os4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(27.3049,1.59279,16.7296,8.86553,15.6115,0.417916,5.83377,45.0011,11.4722,0,"Ir: *RHF"),
		new ElasticScattering(30.4156,1.34323,15.862,7.10909,13.6145,0.204633,5.82008,20.3254,8.27903,+3,"Ir3+: *DS"),
		new ElasticScattering(30.7058,1.30923,15.5512,6.71983,14.2326,0.167252,5.53672,17.4911,6.96824,+4,"Ir4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(27.0059,1.51293,17.7639,8.81174,15.7131,0.424593,5.7837,38.6103,11.6883,0,"Pt: *RHF"),
		new ElasticScattering(29.8429,1.32927,16.7224,7.38979,13.2153,0.263297,6.35234,22.9426,9.85329,+2,"Pt2+: *DS"),
		new ElasticScattering(30.9612,1.24813,15.9829,6.60834,13.7348,0.16864,5.92034,16.9392,7.39534,+4,"Pt4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(16.8819,0.4611,18.5913,8.6216,25.5582,1.4826,5.86,36.3956,12.0658,0,"Au: RHF"),
		new ElasticScattering(28.0109,1.35321,17.8204,7.7395,14.3359,0.356752,6.58077,26.4043,11.2299,+1,"Au1+: *DS"),
		new ElasticScattering(30.6886,1.2199,16.9029,6.82872,12.7801,0.212867,6.52354,18.659,9.0968,+3,"Au3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(20.6809,0.545,19.0417,8.4484,21.6575,1.5729,5.9676,38.3246,12.6089,0,"Hg: RHF"),
		new ElasticScattering(25.0853,1.39507,18.4973,7.65105,16.8883,0.443378,6.48216,28.2262,12.0205,+1,"Hg1+: *DS"),
		new ElasticScattering(29.5641,1.21152,18.06,7.05639,12.8374,0.284738,6.89912,20.7482,10.6268,+2,"Hg2+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(27.5446,0.65515,19.1584,8.70751,15.538,1.96347,5.52593,45.8149,13.1746,0,"Tl: *RHF"),
		new ElasticScattering(21.3985,1.4711,20.4723,0.517394,18.7478,7.43463,6.82847,28.8482,12.5258,+1,"Tl1+: *DS"),
		new ElasticScattering(30.8695,1.1008,18.3841,6.53852,11.9328,0.219074,7.00574,17.2114,9.8027,+3,"Tl3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(31.0617,0.6902,13.0637,2.3576,18.442,8.618,5.9696,47.2579,13.4118,0,"Pb: RHF"),
		new ElasticScattering(21.7886,1.3366,19.5682,0.488383,19.1406,6.7727,7.01107,23.8132,12.4734,+2,"Pb2+: *DS"),
		new ElasticScattering(32.1244,1.00566,18.8003,6.10926,12.0175,0.147041,6.96886,14.714,8.08428,+4,"Pb4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(33.3689,0.704,12.951,2.9238,16.5877,8.7937,6.4692,48.0093,13.5782,0,"Bi: RHF"),
		new ElasticScattering(21.8053,1.2356,19.5026,6.24149,19.1053,0.469999,7.10295,20.3185,12.4711,+3,"Bi3+: *DS"),
		new ElasticScattering(33.5364,0.91654,25.0946,0.039042,19.2497,5.71414,6.91555,12.8285,-6.7994,+5,"Bi5+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(34.6726,0.700999,15.4733,3.55078,13.1138,9.55642,7.02588,47.0045,13.677,0,"Po: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(35.3163,0.68587,19.0211,3.97458,9.49887,11.3824,7.42518,45.4715,13.7108,0,"At: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(35.5631,0.6631,21.2816,4.0691,8.0037,14.0422,7.4433,44.2473,13.6905,0,"Rn: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(35.9299,0.646453,23.0547,4.17619,12.1439,23.1052,2.11253,150.645,13.7247,0,"Fr: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(35.763,0.616341,22.9064,3.87135,12.4739,19.9887,3.21097,142.325,13.6211,0,"Ra: *RHF"),
		new ElasticScattering(35.215,0.604909,21.67,3.5767,7.91342,12.601,7.65078,29.8436,13.5431,+2,"Ra2+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(35.6597,0.589092,23.1032,3.65155,12.5977,18.599,4.08655,117.02,13.5266,0,"Ac: *RHF"),
		new ElasticScattering(35.1736,0.579689,22.1112,3.41437,8.19216,12.9187,7.05545,25.9443,13.4637,+3,"Ac3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(35.5645,0.563359,23.4219,3.46204,12.7473,17.8309,4.80703,99.1722,13.4314,0,"Th: *RHF"),
		new ElasticScattering(35.1007,0.555054,22.4418,3.24498,9.78554,13.4661,5.29444,23.9533,13.376,+4,"Th4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(35.8847,0.547751,23.2948,3.41519,14.1891,16.9235,4.17287,105.251,13.4287,0,"Pa: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(36.0228,0.5293,23.4128,3.3253,14.9491,16.0927,4.188,100.613,13.3966,0,"U: RHF"),
		new ElasticScattering(35.5747,0.52048,22.5259,3.12293,12.2165,12.7148,5.37073,26.3394,13.3092,+3,"U3+: *DS"),
		new ElasticScattering(35.3715,0.516598,22.5326,3.05053,12.0291,12.5723,4.7984,23.4582,13.2671,+4,"U4+: *DS"),
		new ElasticScattering(34.8509,0.507079,22.7584,2.8903,14.0099,13.1767,1.21457,25.2017,13.1665,+6,"U6+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(36.1874,0.511929,23.5964,3.25396,15.6402,15.3622,4.1855,97.4908,13.3573,0,"Np: *RHF"),
		new ElasticScattering(35.7074,0.502322,22.613,3.03807,12.9898,12.1449,5.43227,25.4928,13.2544,+3,"Np3+: *DS"),
		new ElasticScattering(35.5103,0.498626,22.5787,2.96627,12.7766,11.9484,4.92159,22.7502,13.2116,+4,"Np4+: *DS"),
		new ElasticScattering(35.0136,0.48981,22.7286,2.81099,14.3884,12.33,1.75669,22.6581,13.113,+6,"Np6+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(36.5254,0.499384,23.8083,3.26371,16.7707,14.9455,3.47947,105.98,13.3812,0,"Pu: *RHF"),
		new ElasticScattering(35.84,0.484938,22.7169,2.96118,13.5807,11.5331,5.66016,24.3992,13.1991,+3,"Pu3+: *DS"),
		new ElasticScattering(35.6493,0.481422,22.646,2.8902,13.3595,11.316,5.18831,21.8301,13.1555,+4,"Pu4+: *DS"),
		new ElasticScattering(35.1736,0.473204,22.7181,2.73848,14.7635,11.553,2.28678,20.9303,13.0582,+6,"Pu6+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(36.6706,0.483629,24.0992,3.20647,17.3415,14.3136,3.49331,102.273,13.3592,0,"Am: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(36.6488,0.465154,24.4096,3.08997,17.399,13.4346,4.21665,88.4834,13.2887,0,"Cm: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(36.7881,0.451018,24.7736,3.04619,17.8919,12.8946,4.23284,86.003,13.2754,0,"Bk: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(36.9185,0.437533,25.1995,3.00775,18.3317,12.4044,4.24391,83.7881,13.2674,0,"Cf: *RHF")}
		#endregion
		};

		/// <summary>
		/// 電子線による原子散乱因子 ElectronScattering[AtomicNumber][SubNumber]  5 gaussian
		/// a1,a2,a3,a4,a5,b1,b2,b3,b4,b5,valence,method 
		/// </summary>
		public static readonly ElasticScattering[][] ElectronScattering = new ElasticScattering[][]{
		#region
	new ElasticScattering[]{
		new ElasticScattering(0,0,0,0,0,0,0,0,0,0,0,"Unkonwn")},
	new ElasticScattering[]{
		new ElasticScattering(0.0349,0.1201,0.197,0.0573,0.1195,0.5347,3.5867,12.3471,18.9525,38.6269,0,"H: HF"),
		new ElasticScattering(0.14,0.649,1.37,0.337,0.787,0.984,8.67,38.9,111,166,-1,"H1-: HF")},
	new ElasticScattering[]{
		new ElasticScattering(0.0317,0.0838,0.1526,0.1334,0.0164,0.2507,1.4751,4.4938,12.6646,31.1653,0,"He: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.075,0.2249,0.5548,1.4954,0.9354,0.3864,2.9383,15.3829,53.5545,138.7337,0,"Li: RHF"),
		new ElasticScattering(0.0046,0.0165,0.0435,0.0649,0.027,0.0358,0.239,0.879,2.64,7.09,+1,"Li1+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.078,0.221,0.674,1.3867,0.6925,0.3131,2.2381,10.1517,30.9061,78.3273,0,"Be: RHF"),
		new ElasticScattering(0.0034,0.0103,0.0233,0.0325,0.012,0.0267,0.162,0.531,1.48,3.88,+2,"Be2+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.0909,0.2551,0.7738,1.2136,0.4606,0.2995,2.1155,8.3816,24.1292,63.1314,0,"B: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.0893,0.2563,0.757,1.0487,0.3575,0.2465,1.71,6.4094,18.6113,50.2523,0,"C: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.1022,0.3219,0.7982,0.8197,0.1715,0.2451,1.7481,6.1925,17.3894,48.1431,0,"N: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.0974,0.2921,0.691,0.699,0.2039,0.2067,1.3815,4.6943,12.7105,32.4726,0,"O: RHF"),
		new ElasticScattering(0.205,0.628,1.17,1.03,0.29,0.397,2.64,8.8,27.1,91.8,-1,"O1-: HF"),
		new ElasticScattering(0.0421,0.21,0.852,1.82,1.17,0.0609,0.559,2.96,11.5,37.7,-2,"O2-: HF")},
	new ElasticScattering[]{
		new ElasticScattering(0.1083,0.3175,0.6487,0.5846,0.1421,0.2057,1.3439,4.2788,11.3932,28.7881,0,"F: RHF"),
		new ElasticScattering(0.134,0.391,0.814,0.928,0.347,0.228,1.47,4.68,13.2,36,-1,"F1-: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.1269,0.3535,0.5582,0.4674,0.146,0.22,1.3779,4.0203,9.4934,23.1278,0,"Ne: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.2142,0.6853,0.7692,1.6589,1.4482,0.3334,2.3446,10.083,48.3037,138.27,0,"Na: RHF"),
		new ElasticScattering(0.0256,0.0919,0.297,0.514,0.199,0.0397,0.287,1.18,3.75,10.8,+1,"Na1+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.2314,0.6866,0.9677,2.1882,1.1339,0.3278,2.272,10.9241,39.2898,101.9748,0,"Mg: RHF"),
		new ElasticScattering(0.021,0.0672,0.198,0.368,0.174,0.0331,0.222,0.838,2.48,6.75,+2,"Mg2+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(0.239,0.6573,1.2011,2.5586,1.2312,0.3138,2.1063,10.4163,34.4552,98.5344,0,"Al: RHF"),
		new ElasticScattering(0.0192,0.0579,0.163,0.284,0.114,0.0306,0.198,0.713,2.04,5.25,+3,"Al3+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(0.2519,0.6372,1.3795,2.5082,1.05,0.3075,2.0174,9.6746,29.3744,80.4732,0,"Si: RHF"),
		new ElasticScattering(0.192,0.289,0.1,-0.0728,0.0012,0.359,1.96,9.34,11.1,13.4,+4,"Si4+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.2548,0.6106,1.4541,2.3204,0.8477,0.2908,1.874,8.5176,24.3434,63.2996,0,"P: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.2497,0.5628,1.3899,2.1865,0.7715,0.2681,1.6711,7.0267,19.5377,50.3888,0,"S: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.2443,0.5397,1.3919,2.0197,0.6621,0.2468,1.5242,6.1537,16.6687,42.3086,0,"Cl: RHF"),
		new ElasticScattering(0.265,0.596,1.6,2.69,1.23,0.252,1.56,6.21,17.8,47.8,-1,"Cl1-: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.2385,0.5017,1.3428,1.8899,0.6079,0.2289,1.3694,5.2561,14.0928,35.5361,0,"Ar: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.4115,1.4031,2.2784,2.6742,2.2162,0.3703,3.3874,13.1029,68.9592,194.4329,0,"K: RHF"),
		new ElasticScattering(0.199,0.396,0.928,1.45,0.45,0.192,1.1,3.91,9.75,23.4,+1,"K1+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.4054,1.388,2.1602,3.7532,2.2063,0.3499,3.0991,11.9608,53.9353,142.3892,0,"Ca: RHF"),
		new ElasticScattering(0.164,0.327,0.743,1.16,0.307,0.157,0.894,3.15,7.67,17.7,+2,"Ca2+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.3787,1.2181,2.0594,3.2618,2.387,0.3133,2.5856,9.5813,41.7688,116.7282,0,"Se: RHF"),
		new ElasticScattering(0.163,0.307,0.716,0.88,0.139,0.157,0.899,3.06,7.05,16.1,+3,"Sc3+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(0.3825,1.2598,2.0008,3.0617,2.0694,0.304,2.4863,9.2783,39.0751,109.4583,0,"Ti: RHF"),
		new ElasticScattering(0.399,1.04,1.21,-0.0797,0.352,0.376,2.74,8.1,14.2,23.2,+2,"Ti2+: HF"),
		new ElasticScattering(0.364,0.919,1.35,-0.933,0.589,0.364,2.67,8.18,11.8,14.9,+3,"Ti3+: HF"),
		new ElasticScattering(0.116,0.256,0.565,0.772,0.132,0.108,0.655,2.38,5.51,12.3,+4,"Ti4+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(0.3876,1.275,1.9109,2.8314,1.8979,0.2967,2.378,8.7981,35.9528,101.7201,0,"V: RHF"),
		new ElasticScattering(0.317,0.939,1.49,-1.31,1.47,0.269,2.09,7.22,15.2,17.6,+2,"V2+: RHF"),
		new ElasticScattering(0.341,0.805,0.942,0.0783,0.156,0.321,2.23,5.99,13.4,16.9,+3,"V3+: HF"),
		new ElasticScattering(0.0367,0.124,0.244,0.723,0.435,0.033,0.222,0.824,2.8,6.7,+5,"V5+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(0.4046,1.3696,1.8941,2.08,1.2196,0.2986,2.3958,9.1406,37.4701,113.7121,0,"Cr: RHF"),
		new ElasticScattering(0.237,0.634,1.23,0.713,0.0859,0.177,1.35,4.3,12.2,39,+2,"Cr2+: HF"),
		new ElasticScattering(0.393,1.05,1.62,-1.15,0.407,0.359,2.57,8.68,11,15.8,+3,"Cr3+: HF"),
		new ElasticScattering(0.132,0.292,0.703,0.692,0.0959,0.109,0.695,2.39,5.65,14.7,+4,"Cr4+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(0.3796,1.2094,1.7815,2.542,1.5937,0.2699,2.0455,7.4726,31.0604,91.5622,0,"Mn: RHF"),
		new ElasticScattering(0.0576,0.21,0.604,1.32,0.659,0.0398,0.284,1.29,4.23,14.5,+2,"Mn2+: RHF"),
		new ElasticScattering(0.116,0.523,0.881,0.589,0.214,0.0117,0.876,3.06,6.44,14.3,+3,"Mn3+: HF"),
		new ElasticScattering(0.381,1.83,-1.33,0.995,0.0618,0.354,2.72,3.47,5.47,16.1,+4,"Mn4+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(0.3946,1.2725,1.7031,2.314,1.4795,0.2717,2.0443,7.6007,29.9714,86.2265,0,"Fe: RHF"),
		new ElasticScattering(0.307,0.838,1.11,0.28,0.277,0.23,1.62,4.87,10.7,19.2,+2,"Fe2+: RHF"),
		new ElasticScattering(0.198,0.387,0.889,0.709,0.117,0.154,0.893,2.62,6.65,18,+3,"Fe3+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.4118,1.3161,1.6493,2.193,1.283,0.2742,2.0372,7.7205,29.968,84.9383,0,"Co: RHF"),
		new ElasticScattering(0.213,0.488,0.998,0.828,0.23,0.148,0.939,2.78,7.31,20.7,+2,"Co2+: RHF"),
		new ElasticScattering(0.331,0.487,0.729,0.608,0.131,0.267,1.41,2.89,6.45,15.8,+3,"Co3+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(0.386,1.1765,1.5451,2.073,1.3814,0.2478,1.766,6.3107,25.2204,74.3146,0,"Ni: RHF"),
		new ElasticScattering(0.338,0.982,1.32,-3.56,3.62,0.237,1.67,5.73,11.4,12.1,+2,"Ni2+: RHF"),
		new ElasticScattering(0.347,0.877,0.79,0.0538,0.192,0.26,1.71,4.75,7.51,13,+3,"Ni3+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(0.4314,1.3208,1.5236,1.4671,0.8562,0.2694,1.9223,7.3474,28.9892,90.6246,0,"Cu: RHF"),
		new ElasticScattering(0.312,0.812,1.11,0.794,0.257,0.201,1.31,3.8,10.5,28.2,+1,"Cu1+: RHF"),
		new ElasticScattering(0.224,0.544,0.97,0.727,0.182,0.145,0.933,2.69,7.11,19.4,+2,"Cu2+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(0.4288,1.2646,1.4472,1.8294,1.0934,0.2593,1.7998,6.75,25.586,73.5284,0,"Zn: RHF"),
		new ElasticScattering(0.252,0.6,0.917,0.663,0.161,0.161,1.01,2.76,7.08,19,+2,"Zn2+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.4818,1.4032,1.6561,2.4605,1.1054,0.2825,1.9785,8.7546,32.5238,98.5523,0,"Ga: RHF"),
		new ElasticScattering(0.391,0.947,0.69,0.0709,0.0653,0.264,1.65,4.82,10.7,15.2,+3,"Ga3+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(0.4655,1.3014,1.6088,2.6998,1.3003,0.2647,1.7926,7.6071,26.5541,77.5238,0,"Ge: RHF"),
		new ElasticScattering(0.346,0.83,0.599,0.0949,-0.0217,0.232,1.45,4.08,13.2,29.5,+4,"Ge4+: HF")},
	new ElasticScattering[]{
		new ElasticScattering(0.4517,1.2229,1.5852,2.7958,1.2638,0.2493,1.6436,6.8154,22.3681,62.039,0,"As: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.4477,1.1678,1.5843,2.8087,1.1956,0.2405,1.5442,6.3231,19.461,52.0233,0,"Se: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.4798,1.1948,1.8695,2.6953,0.8203,0.2504,1.5963,6.9653,19.8492,50.3233,0,"Br: RHF"),
		new ElasticScattering(0.125,0.563,1.43,3.52,3.22,0.053,0.469,2.15,11.1,38.9,-1,"Br1-: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.4546,1.0993,1.7696,2.7068,0.8672,0.2309,1.4279,5.9449,16.6752,42.2243,0,"Kr: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(1.016,2.8528,3.5466,-7.7804,12.1148,0.4853,5.0925,25.7851,130.4515,138.6775,0,"Rb: RHF"),
		new ElasticScattering(0.368,0.884,1.14,2.26,0.881,0.187,1.12,3.98,10.9,26.6,+1,"Rb1+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.6703,1.4926,3.3368,4.46,3.1501,0.319,2.2287,10.3504,52.3291,151.2216,0,"Sr: RHF"),
		new ElasticScattering(0.346,0.804,0.988,1.89,0.609,0.176,1.04,3.59,9.32,21.4,+2,"Sr2+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.6894,1.5474,3.245,4.2126,2.9764,0.3189,2.2904,10.0062,44.0771,125.012,0,"y: *RHF"),
		new ElasticScattering(0.465,0.923,2.41,-2.31,2.48,0.24,1.43,6.45,9.97,12.2,+2,"Y2+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.6719,1.4684,3.1668,3.9557,2.892,0.3036,2.1249,8.9236,36.8458,108.2049,0,"Zr: *RHF"),
		new ElasticScattering(0.234,0.642,0.747,1.47,0.377,0.113,0.736,2.54,6.72,14.7,+4,"Zr4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.6123,1.2677,3.0348,3.3841,2.3683,0.2709,1.7683,7.2489,27.9465,98.5624,0,"Nb: *RHF"),
		new ElasticScattering(0.377,0.749,1.29,1.61,0.481,0.184,1.02,3.8,9.44,25.7,+3,"Nb3+: *DS"),
		new ElasticScattering(0.0828,0.271,0.654,1.24,0.829,0.0369,0.261,0.957,3.94,9.44,+5,"Nb5+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.6773,1.4798,3.1788,3.0824,1.8384,0.292,2.0606,8.1129,30.5336,100.0658,0,"Mo: *RHF"),
		new ElasticScattering(0.401,0.756,1.38,1.58,0.497,0.191,1.06,3.84,9.38,24.6,+3,"Mo3+: *DS"),
		new ElasticScattering(0.479,0.846,15.6,-15.2,1.6,0.241,1.46,6.79,7.13,10.4,+5,"Mo5+: *DS"),
		new ElasticScattering(0.203,0.567,0.646,1.16,0.171,0.0971,0.647,2.28,5.61,12.4,+6,"Mo6+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.7082,1.6392,3.1993,3.4327,1.8711,0.2976,2.2106,8.5246,33.1456,96.6377,0,"Te: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.6735,1.4934,3.0966,2.7254,1.5597,0.2773,1.9716,7.3249,26.6891,90.5581,0,"Ru: *RHF"),
		new ElasticScattering(0.428,0.773,1.55,1.46,0.486,0.191,1.09,3.82,9.08,21.7,+3,"Ru3+: *DS"),
		new ElasticScattering(0.282,0.653,1.14,1.53,0.418,0.125,0.753,2.85,7.01,17.5,+4,"Ru4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.6413,1.369,2.9854,2.6952,1.5433,0.258,1.7721,6.3854,23.2549,85.1517,0,"Rh: *RHF"),
		new ElasticScattering(0.352,0.723,1.5,1.63,0.499,0.151,0.878,3.28,8.16,20.7,+3,"Rh3+: *DS"),
		new ElasticScattering(0.397,0.725,1.51,1.19,0.251,0.177,1.01,3.62,8.56,18.9,+4,"Rh4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.5904,1.1775,2.6519,2.2875,0.8689,0.2324,1.5019,5.1591,15.5428,46.8213,0,"Pd: *RHF"),
		new ElasticScattering(0.935,3.11,24.6,-43.6,21.1,0.393,4.06,43.1,54,69.8,+2,"Pd2+: *DS"),
		new ElasticScattering(0.348,0.64,1.22,1.45,0.427,0.151,0.832,2.85,6.59,15.6,+4,"Pd4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.6377,1.379,2.8294,2.3631,1.4553,0.2466,1.6974,5.7656,20.0943,76.7372,0,"Ag: RHF"),
		new ElasticScattering(0.503,0.94,2.17,1.99,0.726,0.199,1.19,4.05,11.3,32.4,+1,"Ag1+: *DS"),
		new ElasticScattering(0.431,0.756,1.72,1.78,0.526,0.175,0.979,3.3,8.24,21.4,+2,"Ag2+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.6364,1.4247,2.7802,2.5973,1.7886,0.2407,1.6823,5.6588,20.7219,69.1109,0,"Cd: RHF"),
		new ElasticScattering(0.425,0.745,1.73,1.74,0.487,0.168,0.944,3.14,7.84,20.4,+2,"Cd2+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.6768,1.6589,2.774,3.1835,2.1326,0.2522,1.8545,6.2936,25.1457,84.5448,0,"In: RHF"),
		new ElasticScattering(0.417,0.755,1.59,1.36,0.451,0.164,0.96,3.08,7.03,16.1,+3,"In3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.7224,1.961,2.7161,3.5603,1.8972,0.2651,2.0604,7.3011,27.5493,81.3349,0,"Sn: RHF"),
		new ElasticScattering(0.797,2.13,2.15,-1.64,2.72,0.317,2.51,9.04,24.2,26.4,+2,"Sn2+: RHF"),
		new ElasticScattering(0.261,0.642,1.53,1.36,0.177,0.0957,0.625,2.51,6.31,15.9,+4,"Sn4+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.7106,1.9247,2.6149,3.8322,1.8899,0.2562,1.9646,6.8852,24.7648,68.9168,0,"Sb: RHF"),
		new ElasticScattering(0.552,1.14,1.87,1.36,0.414,0.212,1.42,4.21,12.5,29,+3,"Sb3+: *DS"),
		new ElasticScattering(0.377,0.588,1.22,1.18,0.244,0.151,0.812,2.4,5.27,11.9,+5,"Sb5+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.6947,1.869,2.5356,4.0013,1.8955,0.2459,1.8542,6.4411,22.173,59.2206,0,"Te: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.7047,1.9484,2.594,4.1526,1.5057,0.2455,1.8638,6.7639,21.8007,56.4395,0,"I: RHF"),
		new ElasticScattering(0.901,2.8,5.61,-8.69,12.6,0.312,2.59,14.1,34.4,39.5,+1,"I1+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.6737,1.7908,2.4129,4.21,1.7058,0.2305,1.689,5.8218,18.3928,47.2496,0,"Xe: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(1.2704,3.8018,5.6618,0.9205,4.8105,0.4356,4.2058,23.4342,136.7783,171.7561,0,"Cs: RHF"),
		new ElasticScattering(0.587,1.4,1.87,3.48,1.67,0.2,1.38,4.12,13,31.8,+1,"Cs1+: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.9049,2.6076,4.8498,5.1603,4.7388,0.3066,2.4363,12.1821,54.6135,161.9978,0,"Ba: RHF"),
		new ElasticScattering(0.733,2.05,23,-152,134,0.258,1.96,11.8,14.4,14.9,+2,"Ba2+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.8405,2.3863,4.6139,5.1514,4.7949,0.2791,2.141,10.34,41.9148,132.0204,0,"La: *RHF"),
		new ElasticScattering(0.493,1.1,1.5,2.7,1.08,0.167,1.11,3.11,9.61,21.2,+3,"La3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.8551,2.3915,4.5772,5.0278,4.5118,0.2805,2.12,10.1808,42.0633,130.9893,0,"Ce: *RHF"),
		new ElasticScattering(0.56,1.35,1.59,2.63,0.706,0.19,1.3,3.93,10.7,23.8,+3,"Ce3+: *DS"),
		new ElasticScattering(0.483,1.09,1.34,2.45,0.797,0.165,1.1,3.02,8.85,18.8,+4,"Ce4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.9096,2.5313,4.5266,4.6376,4.369,0.2939,2.2471,10.8266,48.8842,147.602,0,"Pr: *RMF"),
		new ElasticScattering(0.663,1.73,2.35,0.351,1.59,0.226,1.61,6.33,11,16.9,+3,"Pr3+: *DS"),
		new ElasticScattering(0.521,1.19,1.33,2.36,0.69,0.177,1.17,3.28,8.94,19.3,+4,"Pr4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.8807,2.4183,4.4448,4.6858,4.1725,0.2802,2.0836,10.0357,47.4506,146.9976,0,"Nd: *RHF"),
		new ElasticScattering(0.501,1.18,1.45,2.53,0.92,0.162,1.08,3.06,8.8,19.6,+3,"Nd3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.9471,2.5463,4.3523,4.4789,3.908,0.2977,2.2276,10.5762,49.3619,145.358,0,"Pm: *RHF"),
		new ElasticScattering(0.496,1.2,1.47,2.43,0.943,0.156,1.05,3.07,8.56,19.2,+3,"Pm3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.9699,2.5837,4.2778,4.4575,3.5985,0.3003,2.2447,10.6487,50.7994,146.4179,0,"Srn: *RHF"),
		new ElasticScattering(0.518,1.24,1.43,2.4,0.781,0.163,1.08,3.11,8.52,19.1,+3,"Sm3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.8694,2.2413,3.9196,3.9694,4.5498,0.2653,1.859,8.3998,36.7397,125.7089,0,"Eu: RHF"),
		new ElasticScattering(0.613,1.53,1.84,2.46,0.714,0.19,1.27,4.18,10.7,26.2,+2,"Eu2+: *DS"),
		new ElasticScattering(0.496,1.21,1.45,2.36,0.774,0.152,1.01,2.95,8.18,18.5,+3,"Eu3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.9673,2.4702,4.1148,4.4972,3.2099,0.2909,2.1014,9.7067,43.427,125.9474,0,"Gd: *RHF"),
		new ElasticScattering(0.49,1.19,1.42,2.3,0.795,0.148,0.974,2.81,7.78,17.7,+3,"Gd3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.9325,2.3673,3.8791,3.9674,3.7996,0.2761,1.9511,8.9296,41.5937,131.0122,0,"Tb: *RHF"),
		new ElasticScattering(0.503,1.22,1.42,2.24,0.71,0.15,0.982,2.86,7.77,17.7,+3,"Tb3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.9505,2.3705,3.8218,4.0471,3.4451,0.2773,1.9469,8.8862,43.0938,133.1396,0,"Dy: *RHF"),
		new ElasticScattering(0.503,1.24,1.44,2.17,0.643,0.148,0.97,2.88,7.73,17.6,+3,"Dy3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.9248,2.2428,3.6182,3.791,3.7912,0.266,1.8183,7.9655,33.1129,101.8139,0,"Ho: *RHF"),
		new ElasticScattering(0.456,1.17,1.43,2.15,0.692,0.129,0.869,2.61,7.24,16.7,+3,"Ho3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(1.0373,2.4824,3.6558,3.8925,3.0056,0.2944,2.0797,9.4156,45.8056,132.772,0,"Er: *RHF"),
		new ElasticScattering(0.522,1.28,1.46,2.05,0.508,0.15,0.964,2.93,7.72,17.8,+3,"Er3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(1.0075,2.3787,3.544,3.6932,3.1759,0.2816,1.9486,8.7162,41.842,125.032,0,"Tm: *RHF"),
		new ElasticScattering(0.475,1.2,1.42,2.05,0.584,0.132,0.864,2.6,7.09,16.6,+3,"Tm3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(1.0347,2.3911,3.4619,3.6556,3.0052,0.2855,1.9679,8.7619,42.3304,125.6499,0,"Yb: *RHF"),
		new ElasticScattering(0.508,1.37,1.76,2.23,0.584,0.136,0.922,3.12,8.72,23.7,+2,"Yb2+: *DS"),
		new ElasticScattering(0.498,1.22,1.39,1.97,0.559,0.138,0.881,2.63,6.99,16.3,+3,"Yb3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.9927,2.2436,3.3554,3.7813,3.0994,0.2701,1.8073,7.8112,34.4849,103.3526,0,"Lu: *RHF"),
		new ElasticScattering(0.483,1.21,1.41,1.94,0.522,0.131,0.845,2.57,6.88,16.2,+3,"Lu3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(1.0295,2.2911,3.411,3.9497,2.4925,0.2761,1.8625,8.0961,34.2712,98.5295,0,"Hf: *RHF"),
		new ElasticScattering(0.522,1.22,1.37,1.68,0.312,0.145,0.896,2.74,6.91,16.1,+4,"Hf4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(1.019,2.2291,3.4097,3.9252,2.2679,0.2694,1.7962,7.6944,31.0942,91.1089,0,"Ta: *RHF"),
		new ElasticScattering(0.569,1.26,0.979,1.29,0.551,0.161,0.972,2.76,5.4,10.9,+5,"Ta5+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.9853,2.1167,3.357,3.7981,2.2798,0.2569,1.6745,7.0098,26.9234,81.391,0,"W: *RHF"),
		new ElasticScattering(0.181,0.873,1.18,1.48,0.562,0.0118,0.442,1.52,4.35,9.42,+6,"W6+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.9914,2.0858,3.4531,3.8812,1.8526,0.2548,1.6518,6.8845,26.7234,81.7215,0,"Re: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(0.9813,2.0322,3.3665,3.6235,1.9741,0.2487,1.5973,6.4737,23.2817,70.9254,0,"Os: *RHF"),
		new ElasticScattering(0.586,1.31,1.63,1.71,0.54,0.155,0.938,3.19,7.84,19.3,+4,"Os4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(1.0194,2.0645,3.4425,3.4914,1.6976,0.2554,1.6475,6.5966,23.2269,70.0272,0,"Ir: *RHF"),
		new ElasticScattering(0.692,1.37,1.8,1.97,0.804,0.182,1.04,3.47,8.51,21.2,+3,"Ir3+: *DS"),
		new ElasticScattering(0.653,1.29,1.5,1.74,0.683,0.174,0.992,3.14,7.22,17.2,+4,"Ir4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.9148,1.8096,3.2134,3.2953,1.5754,0.2263,1.3813,5.3243,17.5987,60.0171,0,"Pt: *RHF"),
		new ElasticScattering(0.872,1.68,2.63,1.93,0.475,0.223,1.35,4.99,13.6,33,+2,"Pt2+: *DS"),
		new ElasticScattering(0.55,1.21,1.62,1.95,0.61,0.142,0.833,2.81,7.21,17.7,+4,"Pt4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(0.9674,1.8916,3.3993,3.0524,1.2607,0.2358,1.4712,5.6758,18.7119,61.5286,0,"Au: RHF"),
		new ElasticScattering(0.811,1.57,2.63,2.68,0.998,0.201,1.18,4.25,12.1,34.4,+1,"Au1+: *DS"),
		new ElasticScattering(0.722,1.39,1.94,1.94,0.699,0.184,1.06,3.58,8.56,20.4,+3,"Au3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(1.0033,1.9469,3.4396,3.1548,1.418,0.2413,1.5298,5.8009,19.452,60.5753,0,"Hg: RHF"),
		new ElasticScattering(0.796,1.56,2.72,2.76,1.18,0.194,1.14,4.21,12.4,36.2,+1,"Hg1+: *DS"),
		new ElasticScattering(0.773,1.49,2.45,2.23,0.57,0.191,1.12,4,10.8,27.6,+2,"Hg2+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(1.0689,2.1038,3.6039,3.4927,1.8283,0.254,1.6715,6.3509,23.1531,78.7099,0,"TI: *RHF"),
		new ElasticScattering(0.82,1.57,2.78,2.82,1.31,0.197,1.16,4.23,12.7,35.7,+1,"Tl1+: *DS"),
		new ElasticScattering(0.836,1.43,0.394,2.51,1.5,0.208,1.2,2.57,4.86,13.5,+3,"Tl3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(1.0891,2.1867,3.616,3.8031,1.8994,0.2552,1.7174,6.5131,23.917,74.7039,0,"Pb: RHF"),
		new ElasticScattering(0.755,1.44,2.48,2.45,1.03,0.181,1.05,3.75,10.6,27.9,+2,"Pb2+: *DS"),
		new ElasticScattering(0.583,1.14,1.6,2.06,0.662,0.144,0.796,2.58,6.22,14.8,+4,"Pb4+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(1.1007,2.2306,3.5689,4.1549,2.0382,0.2546,1.7351,6.4948,23.6464,70.378,0,"Bi: RHF"),
		new ElasticScattering(0.708,1.35,2.28,2.18,0.797,0.17,0.981,3.44,9.41,23.7,+3,"Bi3+: *DS"),
		new ElasticScattering(0.654,1.18,1.25,1.66,0.778,0.162,0.905,2.68,5.14,11.2,+5,"Bi5+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(1.1568,2.4353,3.6459,4.4064,1.7179,0.2648,1.8786,7.1749,25.1766,69.2821,0,"Po: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(1.0909,2.1976,3.3831,4.67,2.1277,0.2466,1.6707,6.0197,20.7657,57.2663,0,"At: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(1.0756,2.163,3.3178,4.8852,2.0489,0.2402,1.6169,5.7644,19.4568,52.5009,0,"Rn: RHF")},
	new ElasticScattering[]{
		new ElasticScattering(1.4282,3.5081,5.6767,4.1964,3.8946,0.3183,2.6889,13.4816,54.3866,200.8321,0,"Fr: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(1.3127,3.1243,5.2988,5.3891,5.4133,0.2887,2.2897,10.8276,43.5389,145.6109,0,"Ra: *RHF"),
		new ElasticScattering(0.911,1.65,2.53,3.62,1.58,0.204,1.26,4.03,12.6,30,+2,"Ra2+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(1.3128,3.1021,5.3385,5.9611,4.7562,0.2861,2.2509,10.5287,41.7796,128.2973,0,"Ac: *RHF"),
		new ElasticScattering(0.915,1.64,2.26,3.18,1.25,0.205,1.28,3.92,11.3,25.1,+3,"Ac3+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(1.2553,2.9178,5.0862,6.1206,4.7122,0.2701,2.0636,9.3051,34.5977,107.92,0,"Th: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(1.3218,3.1444,5.4371,5.6444,4.0107,0.2827,2.225,10.2454,41.1162,124.4449,0,"Pa: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(1.3382,3.2043,5.4558,5.4839,3.6342,0.2838,2.2452,10.2519,41.7251,124.9023,0,"U: RHF"),
		new ElasticScattering(1.14,2.48,3.61,1.13,0.9,0.25,1.84,7.39,18,22.7,+3,"U3+: *DS"),
		new ElasticScattering(1.09,2.32,12,-9.11,2.15,0.243,1.75,7.79,8.31,16.5,+4,"U4+: *DS"),
		new ElasticScattering(0.687,1.14,1.83,2.53,0.957,0.154,0.861,2.58,7.7,15.9,+6,"U6+: *DS")},
	new ElasticScattering[]{
		new ElasticScattering(1.5193,4.0053,6.5327,-0.1402,6.7489,0.3213,2.8206,14.8878,68.9103,81.7257,0,"Np: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(1.3517,3.2937,5.3213,4.6466,3.5714,0.2813,2.2418,9.9952,42.7939,132.1739,0,"Pu: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(1.2135,2.7962,4.7545,4.5731,4.4786,0.2483,1.8437,7.5421,29.3841,112.4579,0,"Am: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(1.2937,3.11,5.0393,4.7546,3.5031,0.2638,2.0341,8.7101,35.2992,109.4972,0,"Cm: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(1.2915,3.1023,4.9309,4.6009,3.4661,0.2611,2.0023,8.4377,34.1559,105.8911,0,"Bk: *RHF")},
	new ElasticScattering[]{
		new ElasticScattering(1.2089,2.7391,4.3482,4.0047,4.6497,0.2421,1.7487,6.7262,23.2153,80.3108,0,"Cf: *RHF")}
#endregion
		};

		/// <summary>
		/// 電子線による原子散乱因子 ElectronScatteringKirkrand[AtomicNumber]
		/// </summary>
		public static readonly ElasticScattering[] ElectronScatteringKirkrand =
		#region
			new ElasticScattering[]{
new ElasticScattering(  0,0,0,
	0,0,0,
	0,0,0,
	0,0,0),
new ElasticScattering(  0.00420298324,0.0627762505,0.0300907347,
	0.225350888,0.22536695,0.225331756,
	0.0677756695,0.00356609237,0.0276135815,
	4.38854001,0.403884823,1.44490166),
new ElasticScattering(  0.0000187543704,0.0004105958,0.196300059,
	0.212427997,0.332212279,0.517325152,
	0.00836015738,0.0295102022,0.000000465928982,
	0.366668239,1.37171827,37576.8025),
new ElasticScattering(  0.0745843816,0.071538225,0.145315229,
	0.881151424,0.0459142904,0.881301714,
	1.12125769,0.00251736525,0.358434971,
	18.8483665,0.159189995,6.12371),
new ElasticScattering(  0.0611642897,0.125755034,0.200831548,
	0.0990182132,0.0990272412,1.87392509,
	0.787242876,0.0015884785,0.273962031,
	9.32794929,0.0891900236,3.20687658),
new ElasticScattering(  0.125716066,0.173314452,0.184774811,
	0.14825883,0.148257216,3.34227311,
	0.195250221,0.529642075,0.001082305,
	1.97339463,5.70035553,0.0564857237),
new ElasticScattering(  0.212080767,0.199811865,0.168254385,
	0.208605417,0.208610186,5.57870773,
	0.14204836,0.363830672,0.000835012044,
	1.33311887,3.80800263,0.040398262),
new ElasticScattering(  0.533015554,0.0529008883,0.0924159648,
	0.290952515,10.3547896,10.3540028,
	0.261799101,0.000880262108,0.110166555,
	2.76252723,0.0347681236,0.993421736),
new ElasticScattering(  0.339969204,0.307570172,0.130369072,
	0.38157028,0.381571436,19.1919745,
	0.0883326058,0.1965867,0.000996220028,
	0.760635525,2.07401094,0.0303266869),
new ElasticScattering(  0.230560593,0.526889648,0.124346755,
	0.480754213,0.480763895,39.530672,
	0.00124616894,0.0720452555,0.153075777,
	0.0262181803,0.592495593,1.59127671),
new ElasticScattering(  0.408371771,0.454418858,0.144564923,
	0.588228627,0.588288655,121.246013,
	0.0591531395,0.124003718,0.00164986037,
	0.46396354,1.23413025,0.0205869217),
new ElasticScattering(  0.136471662,0.770677865,0.156862014,
	0.0499965301,0.881899664,16.1768579,
	0.996821513,0.038030467,0.127685089,
	20.013261,0.260516254,0.699559329),
new ElasticScattering(  0.304384121,0.756270563,0.101164809,
	0.0842014377,1.64065598,29.7142975,
	0.0345203403,0.971751327,0.120593012,
	0.216596094,12.1236852,0.560865838),
new ElasticScattering(  0.777419424,0.0578312036,0.426386499,
	2.71058227,71.7532098,0.0913331555,
	0.11340722,0.790114035,0.0323293496,
	0.448867451,8.66366718,0.178503463),
new ElasticScattering(  1.06543892,0.120143691,0.180915263,
	1.04118455,68.7113368,0.0887533926,
	1.1206562,0.0305452816,1.59963502,
	3.70062619,0.214097897,9.99096638),
new ElasticScattering(  1.05284447,0.299440284,0.117460748,
	1.3196259,0.12846052,102.190163,
	0.960643452,0.0263555748,1.3805933,
	2.87477555,0.182076844,7.49165526),
new ElasticScattering(  1.01646916,0.441766748,0.121503863,
	1.69181965,0.174180288,167.011091,
	0.82796667,0.0233022533,1.18302846,
	2.3034281,0.15695415,5.85782891),
new ElasticScattering(  0.944221116,0.437322049,0.254547926,
	0.240052374,9.30510439,9.30486346,
	0.0547763323,0.800087488,0.0107488641,
	0.168655688,2.97849774,0.0684240646),
new ElasticScattering(  1.06983288,0.424631786,0.243897949,
	0.287791022,12.4156957,12.4158868,
	0.0479446296,0.764958952,0.00823128431,
	0.136979796,2.43940729,0.0527258749),
new ElasticScattering(  0.692717865,0.965161085,0.148466588,
	7.1084999,0.357532901,0.0393763275,
	0.0264645027,1.80883768,0.543900018,
	0.103591321,32.2845199,1.67791374),
new ElasticScattering(  0.366902871,0.866378999,0.6672033,
	0.0614274129,0.570881727,7.82965639,
	0.487743636,1.82406314,0.0220248453,
	1.32531318,21.0056032,0.091185345),
new ElasticScattering(  0.378871777,0.900022505,0.715288914,
	0.0698910162,0.521061541,7.8770792,
	0.0188640973,0.407945949,1.6178654,
	0.0817512708,1.11141388,18.0840759),
new ElasticScattering(  0.362383267,0.984232966,0.741715642,
	0.0754707114,0.497757309,8.17659391,
	0.362555269,1.4915939,0.0161659509,
	0.955524906,16.2221677,0.0733140839),
new ElasticScattering(  0.352961378,0.746791014,1.08364068,
	0.0819204103,8.81189511,0.510646075,
	1.3901361,0.331273356,0.0140422612,
	14.8901841,0.838543079,0.0657432678),
new ElasticScattering(  1.34348379,0.507040328,0.426358955,
	1.25814353,11.5042811,0.0853660389,
	0.0117241826,0.511966516,0.338285828,
	0.0600177061,1.53772451,0.662418319),
new ElasticScattering(  0.326697613,0.717297,1.33212464,
	0.0888813083,11.1300198,0.582141104,
	0.280801702,1.15499241,0.0111984488,
	0.671583145,12.6825395,0.0532334467),
new ElasticScattering(  0.313454847,0.689290016,1.47141531,
	0.0899325756,13.0366038,0.633345291,
	1.03298688,0.258280285,0.010346069,
	11.6783425,0.609116446,0.0481610627),
new ElasticScattering(  0.315878278,1.60139005,0.656394338,
	0.0946683246,0.699436449,15.6954403,
	0.936746624,0.00977562646,0.238378578,
	10.939241,0.0437446816,0.556286483),
new ElasticScattering(  1.7225463,0.329543044,0.6230072,
	0.776606908,0.10226236,19.4156207,
	0.00943496513,0.854063515,0.221073515,
	0.0398684596,10.4078166,0.51086933),
new ElasticScattering(  0.358774531,1.76181348,0.636905053,
	0.106153463,1.01640995,15.3659093,
	0.00744930667,0.189002347,0.229619589,
	0.0385345989,0.39842779,0.901419843),
new ElasticScattering(  0.570893973,1.98908856,0.306060585,
	0.126534614,2.17781965,37.8619003,
	0.235600223,0.397061102,0.00685657228,
	0.367019041,0.866419596,0.0335778823),
new ElasticScattering(  0.625528464,2.05302901,0.28960812,
	0.11000565,2.41095786,47.8685736,
	0.207910594,0.345079617,0.00655634298,
	0.327807224,0.743139061,0.0309411369),
new ElasticScattering(  0.59095269,0.53998066,2.00626188,
	0.118375976,71.8937433,1.39304889,
	0.749705041,0.183581347,0.00952190743,
	6.8994335,0.364667232,0.026988865),
new ElasticScattering(  0.777875218,0.59384815,1.95918751,
	0.150733157,142.882209,1.74750339,
	0.179880226,0.863267222,0.00959053427,
	0.331800852,5.85490274,0.0233777569),
new ElasticScattering(  0.958390681,0.603851342,1.90828931,
	0.183775557,196.819224,2.15082053,
	0.173885956,0.935265145,0.00862254658,
	0.300006024,4.92471215,0.0212308108),
new ElasticScattering(  1.1413617,0.518118737,1.85731975,
	0.21870871,193.916682,2.65755396,
	0.168217399,0.975705606,0.00724187871,
	0.271719918,4.194825,0.0199325718),
new ElasticScattering(  0.32438697,1.31732163,1.79912614,
	63.1317973,0.254706036,3.23668394,
	0.00429961425,1.00429433,0.162188197,
	0.019896561,3.61094513,0.245583672),
new ElasticScattering(  0.290445351,2.44201329,0.769435449,
	0.0368420227,1.16013332,16.9591472,
	1.58687,0.00281617593,0.12866383,
	2.53082574,0.0188577417,0.210753969),
new ElasticScattering(  0.0137373086,1.97548672,1.59261029,
	0.0187469061,6.3607923,0.221992482,
	0.173263882,4.66280378,0.00161265063,
	0.201624958,25.3027803,0.0153610568),
new ElasticScattering(  0.675302747,0.47028672,2.63497677,
	0.0654331847,106.108709,2.0664354,
	0.109621746,0.960348773,0.00528921555,
	0.193131925,1.63310938,0.0166083821),
new ElasticScattering(  2.64365505,0.554225147,0.761376625,
	2.20202699,178.260107,0.0767218745,
	0.00602946891,0.099163053,0.95678202,
	0.0155143296,0.176175995,1.54330682),
new ElasticScattering(  0.659532875,1.84545854,1.25584405,
	0.086614549,5.94774398,0.640851475,
	0.122253422,0.706638328,0.00262381591,
	0.16664605,1.62853268,0.00826257859),
new ElasticScattering(  0.61016012,1.26544,1.97428762,
	0.0911628054,0.506776025,5.89590381,
	0.648028962,0.00260380817,0.113887493,
	1.46634108,0.00784336311,0.15511434),
new ElasticScattering(  0.855189183,1.66219641,1.45575475,
	0.102962151,7.64907,1.01639987,
	0.105445664,0.771657112,0.00220992635,
	0.142303338,1.34659349,0.00790358976),
new ElasticScattering(  0.470847093,1.58180781,2.02419818,
	0.0933029874,0.452831347,7.11489023,
	0.00197036257,0.626912639,0.10264132,
	0.00756181595,1.25399858,0.133786087),
new ElasticScattering(  0.420051553,1.76266507,2.02735641,
	0.0938882628,0.464441687,8.19346046,
	0.00145487176,0.6228096,0.0991529915,
	0.00782704517,1.17194153,0.124532839),
new ElasticScattering(  2.10475155,2.03884487,0.182067264,
	8.6860647,0.378924449,0.142921634,
	0.0952040948,0.591445248,0.00113328676,
	0.1171259,1.07843808,0.00780252092),
new ElasticScattering(  2.0798139,0.443170726,1.96515215,
	9.92540297,0.104920104,0.640103839,
	0.596130591,0.478016333,0.094645847,
	0.88959479,1.98509407,0.112744464),
new ElasticScattering(  1.63657549,2.17927989,0.77130069,
	12.4540381,1.4513466,0.126695757,
	0.66419388,0.764563285,0.0861126689,
	0.777659202,1.6607521,0.105728357),
new ElasticScattering(  2.24820632,1.64706864,0.788679265,
	1.51913507,13.0113424,0.106128184,
	0.0812579069,0.668280346,0.638467475,
	0.099404562,1.49742063,0.718422635),
new ElasticScattering(  2.1664462,0.688691021,1.92431751,
	11.3174909,0.110131285,0.674464853,
	0.565359888,0.918683861,0.0780542213,
	0.73356461,10.2310312,0.0931104308),
new ElasticScattering(  1.73662114,0.99987138,2.13972409,
	0.884334719,0.138462121,11.9666432,
	0.560566526,0.993772747,0.0737374982,
	0.67267288,8.72330411,0.0878577715),
new ElasticScattering(  2.09383882,1.56940519,1.30941993,
	12.6856869,1.21236537,0.166633292,
	0.0698067804,1.04969537,0.555594354,
	0.0830817576,7.43147857,0.617487676),
new ElasticScattering(  1.60186925,1.98510264,1.482262,
	0.195031538,13.6976183,1.80304795,
	0.553807199,1.11728722,0.0660720847,
	0.56791234,6.40879878,0.0786615429),
new ElasticScattering(  1.60015487,1.71644581,1.84968351,
	2.92913354,15.588299,0.222525983,
	0.0623813648,1.21387555,0.554051946,
	0.0745581223,5.56013271,0.521994521),
new ElasticScattering(  2.95236854,0.428105721,1.89599233,
	6.01461952,46.4151246,0.180109756,
	0.0548012938,4.708386,0.590356719,
	0.0712799633,45.6702799,0.47023631),
new ElasticScattering(  3.19434243,1.98289586,0.155121052,
	9.27352241,0.228741632,0.0382000231,
	0.0673222354,4.48474211,0.542674414,
	0.0730961745,29.5703565,0.408647015),
new ElasticScattering(  2.05036425,0.142114311,3.23538151,
	0.220348417,0.0396438056,9.56979169,
	0.0634683429,3.97960586,0.520116711,
	0.0692443091,25.3178406,0.383614098),
new ElasticScattering(  3.22990759,0.157618307,2.13477838,
	9.94660135,0.0415378676,0.240480572,
	0.501907609,3.8088901,0.0596625028,
	0.366252019,24.3275968,0.0659653503),
new ElasticScattering(  0.158189324,3.18141995,2.2762214,
	0.0391309056,10.4139545,0.281671757,
	3.97705472,0.0558448277,0.485207954,
	26.1872978,0.0630921695,0.354234369),
new ElasticScattering(  0.181379417,3.17616396,2.35221519,
	0.0437324793,10.7842572,0.305571833,
	3.83125763,0.0525889976,0.470090742,
	25.4745408,0.0602676073,0.339017003),
new ElasticScattering(  0.192986811,2.43756023,3.17248504,
	0.043778597,0.329336996,11.1259996,
	3.58105414,0.456529394,0.0494812177,
	24.6709586,0.324990282,0.05765531),
new ElasticScattering(  0.212002595,3.16891754,2.51503494,
	0.0457703608,11.4536599,0.355561054,
	0.444080845,3.36742101,0.0465652543,
	0.311953363,24.0291435,0.0552266819),
new ElasticScattering(  2.59355002,3.16557522,0.229402652,
	0.382452612,11.7675155,0.0476642249,
	0.43225778,3.1726192,0.0437958317,
	0.299719833,23.4462738,0.052944068),
new ElasticScattering(  3.19144939,2.55766431,0.332681934,
	12.0224655,0.408338876,0.0585819814,
	0.041424313,2.61036728,0.420526863,
	0.0506771477,19.9344244,0.28568624),
new ElasticScattering(  0.259407462,3.16177855,2.75095751,
	0.0504689354,12.3140183,0.438337626,
	2.79247686,0.0385931001,0.410881708,
	22.3797309,0.0487920992,0.277622892),
new ElasticScattering(  3.16055396,2.82751709,0.275140255,
	12.5470414,0.467899094,0.0523226982,
	0.40096716,2.63110834,0.0361333817,
	0.267614884,21.9498166,0.0468871497),
new ElasticScattering(  0.288642467,2.90567296,3.15960159,
	0.0540507687,0.497581077,12.7599505,
	0.391280259,2.48596038,0.0337664478,
	0.258151831,21.5400972,0.0450664323),
new ElasticScattering(  3.15573213,0.31151956,2.97722406,
	12.9729009,0.0581399387,0.531213394,
	0.381563854,2.40247532,0.0315224214,
	0.249195776,21.3627616,0.0433253257),
new ElasticScattering(  3.1559197,0.32254471,3.05569053,
	13.1232407,0.0597223323,0.561876773,
	0.02928451,0.372487205,2.27833695,
	0.0416534255,0.240821967,21.0034185),
new ElasticScattering(  3.10794704,3.14091221,0.375660454,
	0.606347847,13.3705269,0.072981474,
	0.361901097,2.45409082,0.027238399,
	0.232652051,21.2695209,0.0399969597),
new ElasticScattering(  3.11446863,0.539634353,3.06460915,
	13.8968881,0.0891708508,0.679919563,
	0.0258563745,2.13983556,0.347788231,
	0.0382808522,18.0078788,0.222706591),
new ElasticScattering(  3.01166899,3.16284788,0.633421771,
	0.710401889,13.8262192,0.0948486572,
	0.341417198,1.53566013,0.0240723773,
	0.214129678,15.5298698,0.036783369),
new ElasticScattering(  3.20236821,0.830098413,2.86552297,
	13.8446369,0.118381581,0.766369118,
	0.0224813887,1.40165263,0.333740596,
	0.0352934622,14.6148877,0.205704486),
new ElasticScattering(  0.924906855,2.75554557,3.3044006,
	0.128663377,0.765826479,13.447117,
	0.329973862,1.09916444,0.0206498883,
	0.198218895,13.5087534,0.0338918459),
new ElasticScattering(  1.96952105,1.21726619,4.10391685,
	49.883062,0.133243809,1.84396916,
	0.0290791978,0.230696669,0.608840299,
	0.0284192813,0.190968784,1.37090356),
new ElasticScattering(  2.06385867,1.29603406,3.96920673,
	40.5671697,0.146559047,1.82561596,
	0.0269835487,0.231083999,0.630466774,
	0.0284172045,0.179765184,1.38911543),
new ElasticScattering(  2.21522726,1.37573155,3.78244405,
	32.446409,0.160920048,1.78756553,
	0.024464324,0.236932016,0.648471412,
	0.0282909938,0.170692368,1.3792839),
new ElasticScattering(  0.98469794,2.73987079,3.61696715,
	0.160910839,0.718971667,12.9281016,
	0.302885602,0.278370726,0.0152124129,
	0.170134854,1.49862703,0.0283510822),
new ElasticScattering(  0.961263398,3.6958103,2.77567491,
	0.170932277,12.9335319,0.68999707,
	0.295414176,0.311475743,0.0143237267,
	0.16352551,1.39200901,0.0271265337),
new ElasticScattering(  1.29200491,2.75161478,3.49387949,
	0.183432865,0.942368371,14.6235654,
	0.277304636,0.43023281,0.0148294351,
	0.155110144,1.2887167,0.0261903834),
new ElasticScattering(  3.7596473,3.21195904,0.647767825,
	13.5041513,0.666330993,0.0922518234,
	0.276123274,0.31883881,0.0131668419,
	0.150312897,1.12565588,0.0248879842),
new ElasticScattering(  1.00795975,3.09796153,3.61296864,
	0.117268427,0.880453235,14.7325812,
	0.262401476,0.405621995,0.0131812509,
	0.143491014,1.04103506,0.0239575415),
new ElasticScattering(  1.59826875,4.38233925,2.06074719,
	0.156897471,2.47094692,57.2438972,
	0.194426023,0.822704978,0.0233226953,
	0.132979109,0.956532528,0.0223038435),
new ElasticScattering(  1.71463223,2.1411596,4.37512413,
	97.9262841,0.210193717,3.66948812,
	0.021621668,0.197843837,0.65204792,
	0.0198456144,0.133758807,0.780432104),
new ElasticScattering(  1.48047794,2.0917463,4.75246033,
	125.943919,0.183803008,4.19890596,
	0.0185643958,0.205859375,0.713540948,
	0.0181383503,0.133035404,0.703031938),
new ElasticScattering(  0.630022295,3.80962881,3.89756067,
	0.140909762,30.851554,0.651559763,
	0.2407551,2.62868577,0.0314285931,
	0.108899672,6.42383261,0.0242346699),
new ElasticScattering(  5.23288135,2.48604205,0.323431354,
	8.60599536,0.304543982,0.0387759096,
	0.255403596,0.553607228,0.00575278889,
	0.128717724,0.536977452,0.012941779),
new ElasticScattering(  1.44192685,3.55291725,3.91259586,
	0.118740873,1.0173975,63.1814783,
	0.216173519,3.94191605,0.0460422605,
	0.0955806441,35.0602732,0.0220850385),
new ElasticScattering(  1.45864127,4.18945405,3.65866182,
	0.107760494,88.9090649,1.05088931,
	0.208479229,3.16528117,0.0523892556,
	0.0909335557,31.3297788,0.0208807697),
new ElasticScattering(  1.19014064,2.55380607,4.68110181,
	0.0773468729,0.659693681,12.8013896,
	0.226121303,0.358250545,0.0078226395,
	0.108632194,0.456765664,0.0162623474),
new ElasticScattering(  4.68537504,2.98413708,0.891988061,
	14.4503632,0.556438592,0.0669512914,
	0.224825384,0.304444846,0.00948162708,
	0.103235396,0.427255647,0.0177730611),
new ElasticScattering(  4.63343606,3.18157056,0.876455075,
	16.3377267,0.569517868,0.0688860012,
	0.221685477,0.2729171,0.0111737298,
	0.098425455,0.409470917,0.018621541),
new ElasticScattering(  4.56773888,3.40325179,0.861841923,
	19.0992795,0.590099634,0.0703204851,
	0.21972887,0.238176903,0.0138306499,
	0.093633428,0.393554882,0.0194437286),
new ElasticScattering(  5.45671123,0.111687906,3.30260343,
	10.189272,0.0398131313,0.314622212,
	0.184568319,0.493644263,3.57484743,
	0.10422086,0.46308054,21.9369542),
new ElasticScattering(  5.38321999,0.123343236,3.4646909,
	10.7289857,0.0415137806,0.339326208,
	0.175437132,3.39800073,0.469459519,
	0.0998932346,21.1601535,0.45199697),
new ElasticScattering(  5.38402377,3.49861264,0.188039547,
	11.1211419,0.35675021,0.0539853583,
	0.169143137,3.19595016,0.464393059,
	0.0960082633,18.0694389,0.436318197),
new ElasticScattering(  3.66090688,0.203054678,5.30697515,
	0.384420906,0.0548547131,11.7150262,
	0.160934046,3.04808401,0.443610295,
	0.0921020329,17.3525367,0.427132359),
new ElasticScattering(  3.9415039,5.16915345,0.161941074,
	0.418246722,12.5201788,0.0481540117,
	0.415299561,2.91761325,0.151474927,
	0.424913856,19.0899693,0.0881568925),
new ElasticScattering(  4.09780623,5.10079393,0.174617289,
	0.446021145,13.1768613,0.0502742829,
	2.76774658,0.144496639,0.402772109,
	18.4815393,0.0846232592,0.4176401),
new ElasticScattering(  4.2493482,5.03556594,0.188920613,
	0.475263933,13.8570834,0.0526975158,
	0.394356058,2.612131,0.138001927,
	0.411193751,17.8537905,0.0812774434),
new ElasticScattering(  0.200942931,4.40119869,4.97250102,
	0.0548366518,0.504248434,14.5721366,
	2.47530599,0.386883197,0.131936095,
	17.2978308,0.405043898,0.0780821071),
new ElasticScattering(  0.216052899,4.91106799,4.5486287,
	0.0583584058,15.3264212,0.53443476,
	2.36114249,0.126277292,0.381364501,
	16.8164803,0.0750304633,0.399305852),
new ElasticScattering(  4.86738014,0.319974401,4.58872425,
	16.032052,0.0670871138,0.577039373,
	0.121482448,2.31639872,0.379258137,
	0.0722275899,14.1279737,0.389973484)};

		#endregion

		/// <summary>
		/// 電子線による原子散乱因子 ElectronScattering[AtomicNumber]  8 gaussian
		/// a1,a2,a3,a4,a5,a6,a7,a8,b1,b2,b3,b4,b5,b6,b7,b8
		/// </summary>
		public static readonly ElasticScattering[] ElectronScatteringEightGaussian =
		#region
			   new ElasticScattering[]{
new ElasticScattering(  0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ,   0   ),
new ElasticScattering(  0.0349, 0.1201, 0.197, 0.0573, 0.1195, 0,0,0,0.5347,3.5867,12.3471,18.9525,38.6269 , 0,0,0  ),
new ElasticScattering(  0.0601325   ,   0.00341956  ,   0.138259    ,   0.0678185   ,   0.0101995   ,   0.0309647   ,   0.106432    ,   0   ,   20.7404 ,   0.0275781   ,   8.68462 ,   1.6257  ,   0.16002 ,   0.554614    ,   3.78602 ,   1   ),
new ElasticScattering(  1.41412 ,   0.0307736   ,   1.17839 ,   0.16857 ,   0.373743    ,   0.0842652   ,   0.00751999  ,   0   ,   109.435 ,   0.278267    ,   39.6401 ,   3.51652 ,   12.1923 ,   1.08424 ,   0.0374873   ,   1   ),
new ElasticScattering(  0.975465    ,   1.28308 ,   0.0145255   ,   0.183618    ,   0.514835    ,   0.069036    ,   0   ,   0   ,   66.1815 ,   25.1312 ,   0.0520308   ,   2.08077 ,   8.26247 ,   0.449133    ,   1   ,   1   ),
new ElasticScattering(  0.592481    ,   0.0089694   ,   0.0262262   ,   0.660394    ,   0.0996126   ,   1.10783 ,   0.0486544   ,   0.242483    ,   55.2092 ,   0.0282234   ,   0.171227    ,   8.85265 ,   1.23989 ,   22.1131 ,   0.500028    ,   3.31089 ),
new ElasticScattering(  0.150116    ,   0.0115343   ,   0.201134    ,   0.0355018   ,   0.847349    ,   0.0856029   ,   0.681441    ,   0.496973    ,   65.9908 ,   0.030316    ,   1.94977 ,   0.187155    ,   12.5341 ,   0.641122    ,   28.6012 ,   5.24728 ),
new ElasticScattering(  0.128526    ,   0.083282    ,   0.751586    ,   0.0124583   ,   0.0369489   ,   0.196675    ,   0.567848    ,   0.47145 ,   51.5491 ,   0.564273    ,   9.84196 ,   0.0281271   ,   0.171534    ,   1.61259 ,   22.5081 ,   4.15893 ),
new ElasticScattering(  0.0846342   ,   0.219761    ,   0.438753    ,   0.0136601   ,   0.479108    ,   0.0936593   ,   0.0412045   ,   0.65829 ,   45.1001 ,   1.55633 ,   19.8785 ,   0.0269163   ,   3.83676 ,   0.559395    ,   0.166811    ,   8.82452 ),
new ElasticScattering(  0.225703    ,   0.356418    ,   0.456033    ,   0.0943058   ,   0.0141999   ,   0.0412259   ,   0.0714511   ,   0.565454    ,   1.37601 ,   16.3971 ,   3.3432  ,   0.499618    ,   0.0250506   ,   0.151783    ,   36.1589 ,   7.51817 ),
new ElasticScattering(  0.417367    ,   0.204523    ,   0.0134987   ,   0.516054    ,   0.0380247   ,   0.0853457   ,   0.315242    ,   0.0617567   ,   2.68491 ,   1.10341 ,   0.0214966   ,   6.10851 ,   0.128173    ,   0.409071    ,   13.4281 ,   29.3833 ),
new ElasticScattering(  0.764282    ,   0.0784888   ,   0.234002    ,   0.854674    ,   1.71381 ,   0.58487 ,   0.525223    ,   0.0238838   ,   175.012 ,   0.228225    ,   0.872519    ,   27.6569 ,   76.3017 ,   7.71327 ,   2.65453 ,   0.0340102   ),
new ElasticScattering(  0.02423 ,   1.92231 ,   0.223814    ,   0.676519    ,   0.478853    ,   1.22336 ,   0.0772897   ,   0.582527    ,   0.0318712   ,   53.3099 ,   0.772604    ,   119.777 ,   2.28625 ,   21.4161 ,   0.209222    ,   6.70426 ),
new ElasticScattering(  0.573184    ,   0.252849    ,   0.0268127   ,   0.716943    ,   0.086738    ,   1.76022 ,   1.98507 ,   0.487736    ,   129.549 ,   0.796944    ,   0.0325583   ,   7.60225 ,   0.216322    ,   21.9064 ,   53.6162 ,   2.37596 ),
new ElasticScattering(  0.535435    ,   0.0858074   ,   0.756086    ,   0.45021 ,   1.91375 ,   0.0271726   ,   1.82501 ,   0.24233 ,   101.143 ,   0.20113 ,   6.6127  ,   2.10357 ,   42.7998 ,   0.0307968   ,   17.8735 ,   0.718982    ),
new ElasticScattering(  0.436681    ,   1.82552 ,   0.0279794   ,   0.236043    ,   0.424065    ,   0.086638    ,   0.806635    ,   1.72377 ,   81.6721 ,   15.1921 ,   0.0297584   ,   0.665415    ,   1.91921 ,   0.191528    ,   5.93326 ,   35.4328 ),
new ElasticScattering(  0.387738    ,   0.224726    ,   0.390066    ,   1.7531  ,   1.57861 ,   0.803698    ,   0.0287966   ,   0.0870822   ,   65.9213 ,   0.612984    ,   1.71302 ,   12.6087 ,   28.9182 ,   5.09472 ,   0.028847    ,   0.182859    ),
new ElasticScattering(  0.310497    ,   1.40134 ,   0.0291018   ,   1.67571 ,   0.0859913   ,   0.816366    ,   0.214821    ,   0.370868    ,   54.5762 ,   24.3579 ,   0.0275415   ,   10.8334 ,   0.171956    ,   4.51913 ,   0.561297    ,   1.55465 ),
new ElasticScattering(  0.300775    ,   1.32967 ,   0.0823986   ,   0.199699    ,   0.0285433   ,   0.337978    ,   1.55222 ,   0.750167    ,   43.6779 ,   19.577  ,   0.157209    ,   0.501614    ,   0.0255918   ,   1.35306 ,   8.82051 ,   3.7702  ),
new ElasticScattering(  1.47019 ,   2.7234  ,   0.158259    ,   1.39744 ,   0.0440372   ,   0.367376    ,   1.85481 ,   0.970109    ,   224.341 ,   96.2712 ,   0.256883    ,   29.5492 ,   0.0367277   ,   0.946809    ,   9.5039  ,   3.45817 ),
new ElasticScattering(  1.46251 ,   1.69751 ,   0.0419927   ,   3.49446 ,   0.14675 ,   1.8111  ,   0.341765    ,   0.917314    ,   161.365 ,   8.06264 ,   0.0335341   ,   71.1012 ,   0.228292    ,   26.0185 ,   0.829513    ,   2.97452 ),
new ElasticScattering(  0.0430217   ,   1.26251 ,   0.147854    ,   1.84028 ,   0.33549 ,   1.61422 ,   3.14118 ,   0.927025    ,   0.0328123   ,   145.9   ,   0.220617    ,   23.5379 ,   0.79404 ,   7.42144 ,   63.6749 ,   2.76893 ),
new ElasticScattering(  0.0440808   ,   1.11011 ,   0.940715    ,   2.84171 ,   0.332837    ,   1.54557 ,   0.149365    ,   1.81486 ,   0.0321716   ,   134.986 ,   2.60364 ,   58.5216 ,   0.766653    ,   6.94773 ,   0.21402 ,   21.7506 ),
new ElasticScattering(  0.99931 ,   0.150499    ,   0.0451729   ,   1.48172 ,   1.75929 ,   0.945224    ,   2.59571 ,   0.330694    ,   125.93  ,   0.207918    ,   0.0316132   ,   6.51425 ,   20.1357 ,   2.44714 ,   54.188  ,   0.740291    ),
new ElasticScattering(  0.852298    ,   1.47074 ,   0.0478603   ,   0.35711 ,   0.160571    ,   1.02913 ,   1.92524 ,   1.55953 ,   149.54  ,   6.7745  ,   0.0320748   ,   0.773752    ,   0.211857    ,   2.50376 ,   60.4323 ,   20.744  ),
new ElasticScattering(  0.83218 ,   0.334958    ,   0.0475373   ,   0.951479    ,   0.153817    ,   1.62553 ,   2.19816 ,   1.36312 ,   111.769 ,   0.703771    ,   0.0307078   ,   2.20849 ,   0.198378    ,   17.7491 ,   47.6686 ,   5.8636  ),
new ElasticScattering(  0.761895    ,   0.156181    ,   1.55828 ,   0.956086    ,   2.03195 ,   0.0487976   ,   1.30668 ,   0.343311    ,   106.194 ,   0.194736    ,   16.901  ,   2.1278  ,   45.21   ,   0.0303425   ,   5.64426 ,   0.694534    ),
new ElasticScattering(  0.696242    ,   1.25878 ,   0.157568    ,   1.89799 ,   0.946072    ,   1.49586 ,   0.0500207   ,   0.347976    ,   102.015 ,   5.38464 ,   0.190981    ,   43.2697 ,   2.03081 ,   16.0915 ,   0.0299866   ,   0.679259    ),
new ElasticScattering(  0.652579    ,   1.77222 ,   0.358137    ,   1.42069 ,   0.158969    ,   0.944046    ,   0.0510257   ,   1.20696 ,   97.5216 ,   41.256  ,   0.66852 ,   15.3841 ,   0.18675 ,   1.96361 ,   0.0295333   ,   5.20828 ),
new ElasticScattering(  0.507521    ,   1.21015 ,   1.15855 ,   0.954343    ,   0.372839    ,   1.18257 ,   0.0526447   ,   0.162063    ,   111.066 ,   44.1963 ,   15.1352 ,   1.93006 ,   0.665471    ,   5.15635 ,   0.029437    ,   0.185268    ),
new ElasticScattering(  0.581589    ,   1.5752  ,   0.162391    ,   0.375787    ,   0.0535365   ,   0.929019    ,   1.27097 ,   1.11683 ,   92.3102 ,   38.9994 ,   0.181031    ,   0.646816    ,   0.0289677   ,   1.83754 ,   14.456  ,   4.8909  ),
new ElasticScattering(  0.634568    ,   0.449034    ,   1.94142 ,   0.177358    ,   1.71183 ,   0.0581547   ,   1.12623 ,   1.00982 ,   121.477 ,   0.714103    ,   47.2232 ,   0.191768    ,   17.8945 ,   0.030376    ,   5.80926 ,   2.03628 ),
new ElasticScattering(  0.656808    ,   0.964258    ,   2.12199 ,   0.17547 ,   1.09397 ,   1.90889 ,   0.0585689   ,   0.442933    ,   101.263 ,   1.91009 ,   41.206  ,   0.185738    ,   5.45682 ,   16.2628 ,   0.0296712   ,   0.682054    ),
new ElasticScattering(  0.625562    ,   0.0586517   ,   2.02186 ,   0.910575    ,   1.05915 ,   0.429051    ,   2.16296 ,   0.171742    ,   83.697  ,   0.0288676   ,   14.3527 ,   1.76602 ,   5.00827 ,   0.643128    ,   35.1063 ,   0.178544    ),
new ElasticScattering(  0.577296    ,   0.0581149   ,   0.859401    ,   2.13442 ,   0.166743    ,   0.411353    ,   2.08973 ,   1.03155 ,   70.6152 ,   0.0278162   ,   1.62289 ,   30.1848 ,   0.170031    ,   0.601086    ,   12.6744 ,   4.57633 ),
new ElasticScattering(  2.10438 ,   0.999795    ,   2.06085 ,   0.81272 ,   0.520807    ,   0.398948    ,   0.0580867   ,   0.163466    ,   11.2012 ,   4.21291 ,   25.8986 ,   1.50793 ,   59.2074 ,   0.568804    ,   0.0270478   ,   0.163763    ),
new ElasticScattering(  0.474008    ,   0.769753    ,   1.98209 ,   0.383929    ,   2.09774 ,   0.159053    ,   0.973677    ,   0.0573899   ,   50.1217 ,   1.39512 ,   22.4279 ,   0.533989    ,   9.95729 ,   0.156202    ,   3.87392 ,   0.0259996   ),
new ElasticScattering(  0.0783725   ,   3.05511 ,   0.725067    ,   2.52761 ,   0.242429    ,   2.12451 ,   1.96045 ,   1.06803 ,   0.0339787   ,   95.4713 ,   0.864288    ,   9.05602 ,   0.225147    ,   25.5222 ,   231.542 ,   2.81044 ),
new ElasticScattering(  0.0780897   ,   0.695933    ,   4.25465 ,   2.46403 ,   2.30039 ,   0.237473    ,   2.04332 ,   1.0352  ,   0.0330299   ,   0.813646    ,   74.3735 ,   8.15645 ,   24.1188 ,   0.21667 ,   172.945 ,   2.62615 ),
new ElasticScattering(  0.0781649   ,   2.41378 ,   0.234419    ,   1.01479 ,   0.674483    ,   1.75716 ,   4.02825 ,   2.46665 ,   0.0322775   ,   7.49478 ,   0.210015    ,   2.49399 ,   0.773645    ,   150.432 ,   64.2138 ,   22.0849 ),
new ElasticScattering(  0.0786167   ,   0.23349 ,   2.55906 ,   0.657725    ,   3.72837 ,   2.38829 ,   1.52491 ,   1.0077  ,   0.0316908   ,   0.205199    ,   20.47   ,   0.742395    ,   57.9151 ,   7.00584 ,   136.834 ,   2.39956 ),
new ElasticScattering(  0.0813555   ,   1.0922  ,   0.243502    ,   2.56378 ,   0.66955 ,   2.48842 ,   2.82518 ,   1.21009 ,   0.0319839   ,   2.51507 ,   0.208615    ,   20.6653 ,   0.747272    ,   7.13588 ,   60.6096 ,   153.783 ),
new ElasticScattering(  0.082582    ,   1.12787 ,   0.659143    ,   2.59448 ,   0.246817    ,   2.48887 ,   1.11461 ,   2.53915 ,   0.0317117   ,   2.48323 ,   0.729548    ,   58.373  ,   0.206917    ,   6.8868  ,   150.282 ,   19.8441 ),
new ElasticScattering(  0.0836483   ,   0.648641    ,   2.4732  ,   2.47799 ,   2.37164 ,   0.24949 ,   1.02395 ,   1.17249 ,   0.0314063   ,   0.711815    ,   19.0694 ,   6.67    ,   56.2802 ,   0.204697    ,   145.979 ,   2.45212 ),
new ElasticScattering(  0.084265    ,   0.635312    ,   0.943609    ,   2.46001 ,   2.16766 ,   0.251067    ,   2.38692 ,   1.20664 ,   0.0309532   ,   0.691763    ,   141.271 ,   6.42237 ,   54.2133 ,   0.201485    ,   18.246  ,   2.39611 ),
new ElasticScattering(  0.88033 ,   1.98794 ,   0.252507    ,   2.28089 ,   0.0851497   ,   2.42402 ,   0.619615    ,   1.22587 ,   135.618 ,   51.6503 ,   0.198729    ,   17.2317 ,   0.0306238   ,   6.12993 ,   0.671919    ,   2.3216  ),
new ElasticScattering(  0.361742    ,   0.518948    ,   0.0767056   ,   2.1483  ,   0.210327    ,   1.43954 ,   0.815274    ,   2.01054 ,   66.0028 ,   0.542778    ,   0.027159    ,   10.2908 ,   0.167691    ,   26.1133 ,   1.62805 ,   4.20768 ),
new ElasticScattering(  0.655547    ,   0.575414    ,   0.245535    ,   1.17472 ,   0.084353    ,   2.30117 ,   1.5791  ,   2.05471 ,   111.384 ,   0.61232 ,   0.186606    ,   2.06273 ,   0.0291366   ,   5.28755 ,   42.1785 ,   14.3031 ),
new ElasticScattering(  0.796488    ,   1.19919 ,   2.05147 ,   0.561283    ,   2.06006 ,   2.23336 ,   0.0849991   ,   0.247267    ,   96.3153 ,   2.00212 ,   39.4769 ,   0.596615    ,   13.9478 ,   5.07397 ,   0.0287861   ,   0.184072    ),
new ElasticScattering(  0.090693    ,   2.30838 ,   0.590709    ,   2.23886 ,   0.276154    ,   1.48369 ,   2.5794  ,   0.866327    ,   0.0300403   ,   17.4441 ,   0.647281    ,   5.80588 ,   0.196922    ,   2.23865 ,   48.2308 ,   124.969 ),
new ElasticScattering(  0.943292    ,   0.583001    ,   2.92417 ,   0.0940465   ,   0.288661    ,   2.52453 ,   1.52144 ,   2.12276 ,   110.689 ,   0.654755    ,   44.7595 ,   0.0305246   ,   0.201178    ,   17.0959 ,   2.20713 ,   5.67254 ),
new ElasticScattering(  0.0903915   ,   3.15296 ,   0.270803    ,   2.67696 ,   0.552035    ,   2.03496 ,   0.97137 ,   1.43361 ,   0.028867    ,   38.8752 ,   0.1868  ,   15.3696 ,   0.599271    ,   5.10221 ,   93.6747 ,   2.00433 ),
new ElasticScattering(  0.089687    ,   1.93016 ,   0.530384    ,   0.928525    ,   0.266096    ,   2.79482 ,   1.38667 ,   3.2273  ,   0.0281358   ,   4.72113 ,   0.57215 ,   80.705  ,   0.180756    ,   14.0066 ,   1.87787 ,   34.1879 ),
new ElasticScattering(  0.868207    ,   0.510541    ,   2.85987 ,   0.261014    ,   3.23826 ,   0.0887844   ,   1.82249 ,   1.33448 ,   68.8654 ,   0.546456    ,   12.655  ,   0.174579    ,   29.8822 ,   0.0273656   ,   4.35173 ,   1.75881 ),
new ElasticScattering(  0.817035    ,   3.22224 ,   0.0884402   ,   2.90535 ,   0.257191    ,   1.72526 ,   0.490576    ,   1.28874 ,   59.1772 ,   26.3832 ,   0.0268065   ,   11.5222 ,   0.169698    ,   4.03484 ,   0.523619    ,   1.65213 ),
new ElasticScattering(  2.62196 ,   0.113822    ,   3.66969 ,   1.98451 ,   3.54655 ,   0.394089    ,   3.31858 ,   0.860833    ,   256.902 ,   0.0336019   ,   103.775 ,   2.7691  ,   26.3463 ,   0.233917    ,   9.78144 ,   0.927052    ),
new ElasticScattering(  2.89017 ,   1.89056 ,   0.112525    ,   3.31879 ,   0.383299    ,   3.48128 ,   5.36148 ,   0.830254    ,   194.975 ,   2.57695 ,   0.0327016   ,   8.95091 ,   0.224944    ,   24.3762 ,   82.9095 ,   0.87897 ),
new ElasticScattering(  2.51134 ,   0.816205    ,   3.6548  ,   0.111944    ,   5.20493 ,   0.375899    ,   3.31591 ,   1.81363 ,   171.351 ,   0.846234    ,   22.8391 ,   0.0320353   ,   72.0509 ,   0.218007    ,   8.35835 ,   2.43799 ),
new ElasticScattering(  2.65282 ,   3.24207 ,   0.383344    ,   5.00026 ,   0.869706    ,   1.82652 ,   0.114399    ,   3.29876 ,   184.995 ,   23.2619 ,   0.218846    ,   78.2514 ,   0.866836    ,   2.48803 ,   0.0321984   ,   8.43374 ),
new ElasticScattering(  2.56016 ,   4.85096 ,   0.382801    ,   1.79635 ,   0.887782    ,   3.25864 ,   0.115283    ,   3.13213 ,   181.021 ,   76.3136 ,   0.21576 ,   2.44553 ,   0.858576    ,   8.18222 ,   0.0319446   ,   22.7016 ),
new ElasticScattering(  2.45839 ,   3.01185 ,   0.116619    ,   0.916636    ,   0.383993    ,   1.77639 ,   4.70664 ,   3.23031 ,   177.684 ,   22.4517 ,   0.0318178   ,   0.856876    ,   0.213783    ,   2.43381 ,   74.9105 ,   8.03688 ),
new ElasticScattering(  2.37285 ,   0.383367    ,   4.57617 ,   3.18457 ,   2.91255 ,   1.75241 ,   0.117484    ,   0.936268    ,   174.364 ,   0.210885    ,   73.3878 ,   7.84164 ,   22.062  ,   2.4035  ,   0.0315682   ,   0.848316    ),
new ElasticScattering(  2.31555 ,   1.73271 ,   0.950844    ,   4.45892 ,   0.382556    ,   2.80906 ,   0.11857 ,   3.12298 ,   171.455 ,   2.36966 ,   0.837569    ,   72.0704 ,   0.208333    ,   21.6764 ,   0.0313869   ,   7.65687 ),
new ElasticScattering(  2.24197 ,   0.964086    ,   0.119164    ,   1.71197 ,   0.380756    ,   2.72974 ,   4.33236 ,   3.07398 ,   167.788 ,   0.824763    ,   0.031085    ,   2.33495 ,   0.205062    ,   21.2498 ,   70.3624 ,   7.46761 ),
new ElasticScattering(  2.01391 ,   2.8884  ,   0.373999    ,   1.65422 ,   0.118747    ,   0.945405    ,   4.35518 ,   2.9907  ,   147.007 ,   20.0396 ,   0.199642    ,   2.22444 ,   0.0305388   ,   0.794273    ,   61.6435 ,   7.02379 ),
new ElasticScattering(  2.08733 ,   2.97811 ,   4.10812 ,   0.995252    ,   2.57306 ,   0.121504    ,   1.68004 ,   0.379775    ,   162.054 ,   7.18585 ,   67.861  ,   0.805474    ,   20.7283 ,   0.0307848   ,   2.29105 ,   0.200751    ),
new ElasticScattering(  2.03641 ,   2.91767 ,   4.00263 ,   0.378024    ,   2.50684 ,   1.00214 ,   0.122267    ,   1.6606  ,   158.894 ,   7.01022 ,   66.3961 ,   0.197978    ,   20.3142 ,   0.791465    ,   0.0305435   ,   2.25354 ),
new ElasticScattering(  1.98246 ,   1.6442  ,   2.43337 ,   1.01379 ,   3.91568 ,   2.8522  ,   0.1237  ,   0.378395    ,   156.68  ,   2.23099 ,   20.0311 ,   0.782342    ,   65.3623 ,   6.8791  ,   0.0304676   ,   0.196524    ),
new ElasticScattering(  1.92979 ,   1.63093 ,   0.376956    ,   2.79512 ,   0.12466 ,   1.02035 ,   3.82166 ,   2.36992 ,   154.11  ,   2.20164 ,   0.19419 ,   6.74738 ,   0.0302828   ,   0.769189    ,   64.2573 ,   19.7652 ),
new ElasticScattering(  1.87528 ,   3.74343 ,   0.125646    ,   2.30345 ,   0.376833    ,   1.62017 ,   1.03016 ,   2.73359 ,   152.292 ,   63.4955 ,   0.0300992   ,   19.6151 ,   0.192304    ,   2.18184 ,   0.759023    ,   6.65081 ),
new ElasticScattering(  1.83488 ,   0.376694    ,   2.2295  ,   1.6085  ,   3.67599 ,   1.0379  ,   0.126866    ,   2.66615 ,   151.063 ,   0.190667    ,   19.4425 ,   2.1597  ,   62.9794 ,   0.748643    ,   0.0299862   ,   6.55295 ),
new ElasticScattering(  1.68073 ,   1.00913 ,   0.369579    ,   2.40911 ,   0.126217    ,   2.59418 ,   3.74481 ,   1.55172 ,   131.551 ,   0.71905 ,   0.185722    ,   18.1921 ,   0.0294531   ,   6.14775 ,   54.935  ,   2.04909 ),
new ElasticScattering(  1.50688 ,   0.987649    ,   0.125834    ,   2.56464 ,   0.363511    ,   1.50227 ,   3.64872 ,   2.5396  ,   117.661 ,   0.693849    ,   0.028982    ,   17.0156 ,   0.181142    ,   1.96033 ,   49.1244 ,   5.81706 ),
new ElasticScattering(  1.35697 ,   2.50625 ,   0.358272    ,   1.45733 ,   0.12566 ,   3.49095 ,   0.969453    ,   2.68067 ,   107.711 ,   5.53813 ,   0.177598    ,   1.88346 ,   0.0285879   ,   44.8209 ,   0.67147 ,   15.9622 ),
new ElasticScattering(  1.22617 ,   3.30214 ,   0.353785    ,   2.76027 ,   0.956201    ,   2.4908  ,   0.125386    ,   1.42146 ,   100.269 ,   41.5031 ,   0.173996    ,   15.0778 ,   0.651772    ,   5.31779 ,   0.0281573   ,   1.82271 ),
new ElasticScattering(  1.11251 ,   2.48187 ,   2.81601 ,   0.350732    ,   3.13778 ,   0.948284    ,   0.125802    ,   1.38745 ,   93.9358 ,   5.12594 ,   14.3043 ,   0.171414    ,   38.7776 ,   0.636108    ,   0.0279032   ,   1.77564 ),
new ElasticScattering(  1.02176 ,   2.84646 ,   0.126307    ,   2.47711 ,   0.347857    ,   1.35046 ,   2.97214 ,   0.938003    ,   88.6713 ,   13.5415 ,   0.027665    ,   4.92709 ,   0.169039    ,   1.72539 ,   36.404  ,   0.620575    ),
new ElasticScattering(  0.560148    ,   2.48421 ,   2.78847 ,   0.91711 ,   2.05191 ,   1.30041 ,   0.125346    ,   0.341863    ,   88.7271 ,   4.69309 ,   12.2934 ,   0.598955    ,   32.9254 ,   1.64866 ,   0.0271212   ,   0.164766    ),
new ElasticScattering(  0.749825    ,   0.352511    ,   2.81397 ,   1.36804 ,   2.14787 ,   0.130073    ,   2.64742 ,   0.967862    ,   101.016 ,   0.169112    ,   13.4342 ,   1.76991 ,   37.572  ,   0.0277689   ,   4.9944  ,   0.614711    ),
new ElasticScattering(  0.617052    ,   2.02087 ,   0.339158    ,   1.26289 ,   0.910999    ,   2.52151 ,   0.12699 ,   2.77587 ,   82.228  ,   31.0556 ,   0.161579    ,   1.60069 ,   0.576964    ,   4.45526 ,   0.0268121   ,   11.5516 ),
new ElasticScattering(  0.723207    ,   2.55706 ,   2.23871 ,   0.923932    ,   2.77508 ,   0.342209    ,   0.129216    ,   1.27809 ,   78.0543 ,   4.44841 ,   31.1589 ,   0.575041    ,   11.6325 ,   0.16219 ,   0.0269384   ,   1.62629 ),
new ElasticScattering(  0.833412    ,   0.138613    ,   2.52903 ,   2.80802 ,   2.90013 ,   1.01448 ,   0.370303    ,   1.51456 ,   111.953 ,   0.0284577   ,   41.3603 ,   5.22326 ,   14.6694 ,   0.618652    ,   0.174242    ,   1.92444 ),
new ElasticScattering(  1.05004 ,   3.04124 ,   0.141646    ,   1.60717 ,   0.377977    ,   2.83127 ,   3.01737 ,   1.02601 ,   112.419 ,   15.5154 ,   0.0287087   ,   1.99005 ,   0.176457    ,   5.36063 ,   43.2165 ,   0.620787    ),
new ElasticScattering(  1.11824 ,   1.60404 ,   0.375999    ,   3.1765  ,   0.141844    ,   3.37565 ,   1.00847 ,   2.76444 ,   100.779 ,   1.94385 ,   0.174222    ,   15.0081 ,   0.028417    ,   40.1355 ,   0.60582 ,   5.15621 ),
new ElasticScattering(  1.08395 ,   1.56433 ,   3.54436 ,   0.370188    ,   3.3025  ,   0.978952    ,   2.67024 ,   0.141423    ,   89.2264 ,   1.85706 ,   36.1285 ,   0.170754    ,   14.0316 ,   0.584589    ,   4.84602 ,   0.0280216   ),
new ElasticScattering(  1.00772 ,   3.39789 ,   0.140446    ,   1.52947 ,   0.363608    ,   2.57019 ,   3.60413 ,   0.951784    ,   77.5183 ,   13.0774 ,   0.0275277   ,   1.77705 ,   0.166713    ,   4.5624  ,   32.2964 ,   0.563673    ),
new ElasticScattering(  1.02581 ,   3.77133 ,   0.137651    ,   1.43308 ,   0.351096    ,   2.45972 ,   0.910437    ,   3.40362 ,   65.6002 ,   28.2164 ,   0.0267045   ,   1.64363 ,   0.160132    ,   4.13989 ,   0.534216    ,   11.771  ),
new ElasticScattering(  2.65977 ,   0.172172    ,   4.51724 ,   2.50913 ,   3.65274 ,   0.506741    ,   3.55679 ,   1.14054 ,   238.173 ,   0.0326053   ,   24.3663 ,   2.64917 ,   91.3621 ,   0.217409    ,   8.64044 ,   0.743785    ),
new ElasticScattering(  3.05872 ,   2.41359 ,   5.39588 ,   3.52866 ,   4.40445 ,   1.09795 ,   0.169566    ,   0.491939    ,   185.576 ,   2.48189 ,   76.6434 ,   7.99618 ,   22.2101 ,   0.708105    ,   0.0317993   ,   0.209942    ),
new ElasticScattering(  2.78084 ,   0.481883    ,   4.51391 ,   3.48024 ,   5.65886 ,   1.06052 ,   0.168121    ,   2.33968 ,   158.118 ,   0.204504    ,   20.6378 ,   7.48688 ,   65.6584 ,   0.679885    ,   0.0312154   ,   2.34627 ),
new ElasticScattering(  2.50952 ,   2.27111 ,   4.64579 ,   0.474065    ,   5.7135  ,   1.02717 ,   3.41627 ,   0.166777    ,   138.467 ,   2.22721 ,   19.2308 ,   0.199705    ,   57.5987 ,   0.655748    ,   7.02943 ,   0.030657    ),
new ElasticScattering(  2.53675 ,   3.57421 ,   5.31964 ,   1.02789 ,   4.32557 ,   2.30386 ,   0.169372    ,   0.487507    ,   148.16  ,   7.07853 ,   61.1398 ,   0.662681    ,   19.3901 ,   2.23385 ,   0.0307912   ,   0.201938    ),
new ElasticScattering(  2.41398 ,   5.15366 ,   0.495547    ,   2.30254 ,   1.02009 ,   3.62932 ,   0.170722    ,   4.20131 ,   145.452 ,   60.0112 ,   0.202176    ,   2.20443 ,   0.662113    ,   6.96063 ,   0.0307049   ,   19.0886 ),
new ElasticScattering(  2.33279 ,   0.501233    ,   4.07849 ,   2.29349 ,   5.00067 ,   0.171923    ,   3.64748 ,   1.00842 ,   142.699 ,   0.201857    ,   18.6348 ,   2.16431 ,   58.6441 ,   0.0306047   ,   6.78789 ,   0.658414    ),
new ElasticScattering(  2.29418 ,   0.513598    ,   3.81356 ,   1.0134  ,   4.37221 ,   0.173822    ,   3.71086 ,   2.32856 ,   154.198 ,   0.203164    ,   18.6614 ,   0.666106    ,   62.4102 ,   0.0306175   ,   6.78808 ,   2.17248 ),
new ElasticScattering(  2.21203 ,   3.68912 ,   0.519496    ,   1.00764 ,   0.174758    ,   2.32593 ,   4.22729 ,   3.70558 ,   151.267 ,   18.2852 ,   0.202615    ,   0.664712    ,   0.0304719   ,   2.14207 ,   61.1538 ,   6.63823 ),
new ElasticScattering(  2.09796 ,   0.981812    ,   4.46676 ,   2.2728  ,   3.72522 ,   3.61889 ,   0.173748    ,   0.513685    ,   133.864 ,   0.646093    ,   54.6756 ,   2.05382 ,   17.3002 ,   6.28014 ,   0.030013    ,   0.198676    ),
new ElasticScattering(  2.02212 ,   2.27939 ,   0.175139    ,   4.37237 ,   0.522102    ,   3.60506 ,   0.981508    ,   3.6132  ,   132.335 ,   2.03768 ,   0.0299544   ,   54.02   ,   0.198898    ,   17.1382 ,   0.649198    ,   6.18619 ),
new ElasticScattering(  1.99427 ,   2.32394 ,   0.536256    ,   3.34023 ,   0.177385    ,   3.8535  ,   0.998375    ,   3.63309 ,   143.337 ,   2.0622  ,   0.200627    ,   17.307  ,   0.0300373   ,   57.8132 ,   0.663035    ,   6.2258  ),
new ElasticScattering(  1.92307 ,   3.74261 ,   0.54091 ,   2.32381 ,   0.997913    ,   3.59963 ,   0.178227    ,   3.2376  ,   140.786 ,   56.7596 ,   0.199773    ,   2.03791 ,   0.662698    ,   6.10247 ,   0.0298985   ,   17.0224 ),
new ElasticScattering(  1.87073 ,   2.32957 ,   0.546155    ,   3.55039 ,   1.00126 ,   3.11376 ,   0.179151    ,   3.64052 ,   138.994 ,   2.02062 ,   0.199071    ,   5.99489 ,   0.664159    ,   16.7971 ,   0.0297778   ,   56.0658 ),
new ElasticScattering(  1.81225 ,   0.549151    ,   3.02284 ,   0.179559    ,   3.53893 ,   1.0018  ,   3.5033  ,   2.32772 ,   136.279 ,   0.197678    ,   16.4929 ,   0.0295692   ,   54.9099 ,   0.662601    ,   5.87184 ,   1.99486 ),
new ElasticScattering(  2.33372 ,   3.44366 ,   0.180399    ,   1.772   ,   0.553094    ,   3.4476  ,   1.0059  ,   2.89837 ,   1.97733 ,   54.4683 ,   0.0294446   ,   135.103 ,   0.196757    ,   5.77124 ,   0.66311 ,   16.2835 ),
new ElasticScattering(  1.59158 ,   2.99874 ,   0.556367    ,   3.41054 ,   0.180994    ,   1.01462 ,   3.81642 ,   2.32862 ,   127.126 ,   16.378  ,   0.195555    ,   5.68881 ,   0.0292783   ,   0.664271    ,   50.9852 ,   1.9598  )};

		#endregion

		public static readonly Complex[][] NeutronCoherentScattering = new Complex[][]{
			#region 単位はfm
					new Complex[]{new Complex(  0   ,   0   )},
					new Complex[]{new Complex(  -3.739  ,   0   ),
					new Complex(    -3.7406 ,   0   ),
					new Complex(    6.671   ,   0   ),
					new Complex(    4.792   ,   0   )},
					new Complex[]{new Complex(  3.26    ,   0   ),
					new Complex(    5.74    ,   -1.483  ),
					new Complex(    3.26    ,   0   )},
					new Complex[]{new Complex(  -1.9    ,   0   ),
					new Complex(    2   ,   -0.261  ),
					new Complex(    -2.22   ,   0   )},
					new Complex[]{new Complex(  7.79    ,   0   ),
					new Complex(    7.79    ,   0   )},
					new Complex[]{new Complex(  5.3 ,   0.213   ),
					new Complex(    -0.1    ,   1.066   ),
					new Complex(    6.65    ,   0   )},
					new Complex[]{new Complex(  6.646   ,   0   ),
					new Complex(    6.6511  ,   0   ),
					new Complex(    6.19    ,   0   )},
					new Complex[]{new Complex(  9.36    ,   0   ),
					new Complex(    9.37    ,   0   ),
					new Complex(    6.44    ,   0   )},
					new Complex[]{new Complex(  5.803   ,   0   ),
					new Complex(    5.803   ,   0   ),
					new Complex(    5.78    ,   0   ),
					new Complex(    5.84    ,   0   )},
					new Complex[]{new Complex(  5.654   ,   0   ),
					new Complex(    5.654   ,   0   )},
					new Complex[]{new Complex(  4.566   ,   0   ),
					new Complex(    4.631   ,   0   ),
					new Complex(    6.66    ,   0   ),
					new Complex(    3.87    ,   0   )},
					new Complex[]{new Complex(  3.63    ,   0   ),
					new Complex(    3.63    ,   0   )},
					new Complex[]{new Complex(  5.375   ,   0   ),
					new Complex(    5.66    ,   0   ),
					new Complex(    3.62    ,   0   ),
					new Complex(    4.89    ,   0   )},
					new Complex[]{new Complex(  3.449   ,   0   ),
					new Complex(    3.449   ,   0   )},
					new Complex[]{new Complex(  4.1491  ,   0   ),
					new Complex(    4.107   ,   0   ),
					new Complex(    4.7 ,   0   ),
					new Complex(    4.58    ,   0   )},
					new Complex[]{new Complex(  5.13    ,   0   ),
					new Complex(    5.13    ,   0   )},
					new Complex[]{new Complex(  2.847   ,   0   ),
					new Complex(    2.804   ,   0   ),
					new Complex(    4.74    ,   0   ),
					new Complex(    3.48    ,   0   ),
					new Complex(    3   ,   0   )},
					new Complex[]{new Complex(  9.577   ,   0   ),
					new Complex(    11.65   ,   0   ),
					new Complex(    3.08    ,   0   )},
					new Complex[]{new Complex(  1.909   ,   0   ),
					new Complex(    24.9    ,   0   ),
					new Complex(    3.5 ,   0   ),
					new Complex(    1.83    ,   0   )},
					new Complex[]{new Complex(  3.67    ,   0   ),
					new Complex(    3.74    ,   0   ),
					new Complex(    3   ,   0   ),
					new Complex(    2.69    ,   0   )},
					new Complex[]{new Complex(  4.7 ,   0   ),
					new Complex(    4.8 ,   0   ),
					new Complex(    3.36    ,   0   ),
					new Complex(    -1.56   ,   0   ),
					new Complex(    1.42    ,   0   ),
					new Complex(    3.6 ,   0   ),
					new Complex(    0.39    ,   0   )},
					new Complex[]{new Complex(  12.29   ,   0   ),
					new Complex(    12.29   ,   0   )},
					new Complex[]{new Complex(  -3.37   ,   0   ),
					new Complex(    4.725   ,   0   ),
					new Complex(    3.53    ,   0   ),
					new Complex(    -5.86   ,   0   ),
					new Complex(    0.98    ,   0   ),
					new Complex(    5.88    ,   0   )},
					new Complex[]{new Complex(  -0.3824 ,   0   ),
					new Complex(    7.6 ,   0   ),
					new Complex(    -0.402  ,   0   )},
					new Complex[]{new Complex(  3.635   ,   0   ),
					new Complex(    -4.5    ,   0   ),
					new Complex(    4.92    ,   0   ),
					new Complex(    -4.2    ,   0   ),
					new Complex(    4.55    ,   0   )},
					new Complex[]{new Complex(  -3.75   ,   0   ),
					new Complex(    -3.75   ,   0   )},
					new Complex[]{new Complex(  9.45    ,   0   ),
					new Complex(    4.2 ,   0   ),
					new Complex(    9.94    ,   0   ),
					new Complex(    2.3 ,   0   ),
					new Complex(    15  ,   0   )},
					new Complex[]{new Complex(  2.49    ,   0   ),
					new Complex(    2.49    ,   0   )},
					new Complex[]{new Complex(  10.3    ,   0   ),
					new Complex(    14.4    ,   0   ),
					new Complex(    2.8 ,   0   ),
					new Complex(    7.6 ,   0   ),
					new Complex(    -8.7    ,   0   ),
					new Complex(    -0.37   ,   0   )},
					new Complex[]{new Complex(  7.718   ,   0   ),
					new Complex(    6.43    ,   0   ),
					new Complex(    10.61   ,   0   )},
					new Complex[]{new Complex(  5.6 ,   0   ),
					new Complex(    5.22    ,   0   ),
					new Complex(    5.97    ,   0   ),
					new Complex(    7.56    ,   0   ),
					new Complex(    6.03    ,   0   ),
					new Complex(    6   ,   0   )},
					new Complex[]{new Complex(  7.288   ,   0   ),
					new Complex(    7.88    ,   0   ),
					new Complex(    6.4 ,   0   )},
					new Complex[]{new Complex(  8.185   ,   0   ),
					new Complex(    10  ,   0   ),
					new Complex(    8.51    ,   0   ),
					new Complex(    5.02    ,   0   ),
					new Complex(    7.58    ,   0   ),
					new Complex(    8.21    ,   0   )},
					new Complex[]{new Complex(  6.58    ,   0   ),
					new Complex(    6.58    ,   0   )},
					new Complex[]{new Complex(  7.97    ,   0   ),
					new Complex(    0.8 ,   0   ),
					new Complex(    12.2    ,   0   ),
					new Complex(    8.25    ,   0   ),
					new Complex(    8.24    ,   0   ),
					new Complex(    7.48    ,   0   ),
					new Complex(    6.34    ,   0   )},
					new Complex[]{new Complex(  6.795   ,   0   ),
					new Complex(    6.8 ,   0   ),
					new Complex(    6.79    ,   0   )},
					new Complex[]{new Complex(  7.81    ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    8.1 ,   0   )},
					new Complex[]{new Complex(  7.09    ,   0   ),
					new Complex(    7.03    ,   0   ),
					new Complex(    7.23    ,   0   )},
					new Complex[]{new Complex(  7.02    ,   0   ),
					new Complex(    7   ,   0   ),
					new Complex(    5.67    ,   0   ),
					new Complex(    7.4 ,   0   ),
					new Complex(    7.15    ,   0   )},
					new Complex[]{new Complex(  7.75    ,   0   ),
					new Complex(    7.75    ,   0   )},
					new Complex[]{new Complex(  7.16    ,   0   ),
					new Complex(    6.4 ,   0   ),
					new Complex(    8.7 ,   0   ),
					new Complex(    7.4 ,   0   ),
					new Complex(    8.2 ,   0   ),
					new Complex(    5.5 ,   0   )},
					new Complex[]{new Complex(  7.054   ,   0   ),
					new Complex(    7.054   ,   0   )},
					new Complex[]{new Complex(  6.715   ,   0   ),
					new Complex(    6.91    ,   0   ),
					new Complex(    6.8 ,   0   ),
					new Complex(    6.91    ,   0   ),
					new Complex(    6.2 ,   0   ),
					new Complex(    7.24    ,   0   ),
					new Complex(    6.58    ,   0   ),
					new Complex(    6.73    ,   0   )},
					new Complex[]{new Complex(  0   ,   0   ),
					new Complex(    6.8 ,   0   )},
					new Complex[]{new Complex(  7.03    ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    6.9 ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    3.3 ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   )},
					new Complex[]{new Complex(  5.88    ,   0   ),
					new Complex(    5.88    ,   0   )},
					new Complex[]{new Complex(  5.91    ,   0   ),
					new Complex(    7.7 ,   0   ),
					new Complex(    7.7 ,   0   ),
					new Complex(    5.5 ,   0   ),
					new Complex(    6.4 ,   0   ),
					new Complex(    4.1 ,   0   ),
					new Complex(    7.7 ,   0   )},
					new Complex[]{new Complex(  5.922   ,   0   ),
					new Complex(    7.555   ,   0   ),
					new Complex(    4.165   ,   0   )},
					new Complex[]{new Complex(  4.87    ,   -0.70   ),
					new Complex(    5   ,   0   ),
					new Complex(    5.4 ,   0   ),
					new Complex(    5.9 ,   0   ),
					new Complex(    6.5 ,   0   ),
					new Complex(    6.4 ,   0   ),
					new Complex(    -8  ,   -5.73   ),
					new Complex(    7.5 ,   0   ),
					new Complex(    6.3 ,   0   )},
					new Complex[]{new Complex(  2.08    ,   -0.0539 ),
					new Complex(    5.39    ,   0   ),
					new Complex(    4.01    ,   -0.0562 )},
					new Complex[]{new Complex(  6.225   ,   0   ),
					new Complex(    6.1 ,   0   ),
					new Complex(    6.2 ,   0   ),
					new Complex(    6   ,   0   ),
					new Complex(    5.93    ,   0   ),
					new Complex(    6.48    ,   0   ),
					new Complex(    6.07    ,   0   ),
					new Complex(    6.12    ,   0   ),
					new Complex(    6.49    ,   0   ),
					new Complex(    5.74    ,   0   ),
					new Complex(    5.97    ,   0   )},
					new Complex[]{new Complex(  5.57    ,   0   ),
					new Complex(    5.71    ,   0   ),
					new Complex(    5.38    ,   0   )},
					new Complex[]{new Complex(  5.8 ,   0   ),
					new Complex(    5.3 ,   0   ),
					new Complex(    3.8 ,   0   ),
					new Complex(    -0.05   ,   -0.116  ),
					new Complex(    7.96    ,   0   ),
					new Complex(    5.02    ,   0   ),
					new Complex(    5.56    ,   0   ),
					new Complex(    5.89    ,   0   ),
					new Complex(    6.02    ,   0   )},
					new Complex[]{new Complex(  5.28    ,   0   ),
					new Complex(    5.28    ,   0   )},
					new Complex[]{new Complex(  4.92    ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   )},
					new Complex[]{new Complex(  5.42    ,   0   ),
					new Complex(    5.42    ,   0   )},
					new Complex[]{new Complex(  5.07    ,   0   ),
					new Complex(    -3.6    ,   0   ),
					new Complex(    7.8 ,   0   ),
					new Complex(    5.7 ,   0   ),
					new Complex(    4.67    ,   0   ),
					new Complex(    4.91    ,   0   ),
					new Complex(    6.83    ,   0   ),
					new Complex(    4.84    ,   0   )},
					new Complex[]{new Complex(  8.24    ,   0   ),
					new Complex(    8   ,   0   ),
					new Complex(    8.24    ,   0   )},
					new Complex[]{new Complex(  4.84    ,   0   ),
					new Complex(    5.8 ,   0   ),
					new Complex(    6.7 ,   0   ),
					new Complex(    4.84    ,   0   ),
					new Complex(    4.75    ,   0   )},
					new Complex[]{new Complex(  4.58    ,   0   ),
					new Complex(    4.58    ,   0   )},
					new Complex[]{new Complex(  7.69    ,   0   ),
					new Complex(    7.7 ,   0   ),
					new Complex(    14.2    ,   0   ),
					new Complex(    2.8 ,   0   ),
					new Complex(    14.2    ,   0   ),
					new Complex(    8.7 ,   0   ),
					new Complex(    5.7 ,   0   ),
					new Complex(    5.3 ,   0   )},
					new Complex[]{new Complex(  0   ,   0   ),
					new Complex(    12.6    ,   0   )},
					new Complex[]{new Complex(  0.8 ,   -1.65   ),
					new Complex(    -3  ,   0   ),
					new Complex(    14  ,   0   ),
					new Complex(    -3  ,   0   ),
					new Complex(    -19.2   ,   -11.7   ),
					new Complex(    14  ,   0   ),
					new Complex(    -5  ,   0   ),
					new Complex(    9.3 ,   0   )},
					new Complex[]{new Complex(  7.22    ,   -1.26   ),
					new Complex(    6.13    ,   -2.53   ),
					new Complex(    8.22    ,   0   )},
					new Complex[]{new Complex(  6.5 ,   -13.82  ),
					new Complex(    10  ,   0   ),
					new Complex(    10  ,   0   ),
					new Complex(    6   ,   -17.0   ),
					new Complex(    6.3 ,   0   ),
					new Complex(    -1.14   ,   -71.9   ),
					new Complex(    9   ,   0   ),
					new Complex(    9.15    ,   0   )},
					new Complex[]{new Complex(  7.38    ,   0   ),
					new Complex(    7.38    ,   0   )},
					new Complex[]{new Complex(  16.9    ,   -0.276  ),
					new Complex(    6.1 ,   0   ),
					new Complex(    6   ,   0   ),
					new Complex(    6.7 ,   0   ),
					new Complex(    10.3    ,   0   ),
					new Complex(    -1.4    ,   0   ),
					new Complex(    5   ,   0   ),
					new Complex(    49.4    ,   -0.79   )},
					new Complex[]{new Complex(  8.01    ,   0   ),
					new Complex(    8.01    ,   0   )},
					new Complex[]{new Complex(  7.79    ,   0   ),
					new Complex(    8.8 ,   0   ),
					new Complex(    8.2 ,   0   ),
					new Complex(    10.6    ,   0   ),
					new Complex(    3   ,   0   ),
					new Complex(    7.4 ,   0   ),
					new Complex(    9.6 ,   0   )},
					new Complex[]{new Complex(  7.07    ,   0   ),
					new Complex(    7.07    ,   0   )},
					new Complex[]{new Complex(  12.43   ,   0   ),
					new Complex(    -4.07   ,   -0.62   ),
					new Complex(    6.77    ,   0   ),
					new Complex(    9.66    ,   0   ),
					new Complex(    9.43    ,   0   ),
					new Complex(    9.56    ,   0   ),
					new Complex(    19.3    ,   0   ),
					new Complex(    8.72    ,   0   )},
					new Complex[]{new Complex(  7.21    ,   0   ),
					new Complex(    7.24    ,   0   ),
					new Complex(    6.1 ,   -0.57   )},
					new Complex[]{new Complex(  7.77    ,   0   ),
					new Complex(    10.9    ,   0   ),
					new Complex(    6.61    ,   0   ),
					new Complex(    0.8 ,   0   ),
					new Complex(    5.9 ,   0   ),
					new Complex(    7.46    ,   0   ),
					new Complex(    13.2    ,   0   )},
					new Complex[]{new Complex(  6.91    ,   0   ),
					new Complex(    7   ,   0   ),
					new Complex(    6.91    ,   0   )},
					new Complex[]{new Complex(  4.86    ,   0   ),
					new Complex(    5   ,   0   ),
					new Complex(    6.97    ,   0   ),
					new Complex(    6.53    ,   0   ),
					new Complex(    7.48    ,   0   ),
					new Complex(    -0.72   ,   0   )},
					new Complex[]{new Complex(  9.2 ,   0   ),
					new Complex(    9   ,   0   ),
					new Complex(    9.3 ,   0   )},
					new Complex[]{new Complex(  10.7    ,   0   ),
					new Complex(    10  ,   0   ),
					new Complex(    11.6    ,   0   ),
					new Complex(    10  ,   0   ),
					new Complex(    7.6 ,   0   ),
					new Complex(    10.7    ,   0   ),
					new Complex(    11  ,   0   ),
					new Complex(    11.5    ,   0   )},
					new Complex[]{new Complex(  10.6    ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   )},
					new Complex[]{new Complex(  9.6 ,   0   ),
					new Complex(    9   ,   0   ),
					new Complex(    9.9 ,   0   ),
					new Complex(    10.55   ,   0   ),
					new Complex(    8.83    ,   0   ),
					new Complex(    9.89    ,   0   ),
					new Complex(    7.8 ,   0   )},
					new Complex[]{new Complex(  7.63    ,   0   ),
					new Complex(    7.63    ,   0   )},
					new Complex[]{new Complex(  12.692  ,   0   ),
					new Complex(    30.3    ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    16.9    ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   ),
					new Complex(    0   ,   0   )},
					new Complex[]{new Complex(  8.776   ,   0   ),
					new Complex(    6.99    ,   0   ),
					new Complex(    9.52    ,   0   )},
					new Complex[]{new Complex(  9.405   ,   0   ),
					new Complex(    9.9 ,   0   ),
					new Complex(    9.22    ,   0   ),
					new Complex(    9.28    ,   0   ),
					new Complex(    9.5 ,   0   )},
					new Complex[]{new Complex(  8.532   ,   0   ),
					new Complex(    8.532   ,   0   )},
					new Complex[]{new Complex(  0   ,   0   ),
					new Complex(    0   ,   0   )},
					new Complex[]{new Complex(  0   ,   0   ),
					new Complex(    0   ,   0   )},
					new Complex[]{new Complex(  0   ,   0   ),
					new Complex(    0   ,   0   )},
					new Complex[]{new Complex(  0   ,   0   ),
					new Complex(    0   ,   0   )},
					new Complex[]{new Complex(  0   ,   0   ),
					new Complex(    10  ,   0   )},
					new Complex[]{new Complex(  0   ,   0   ),
					new Complex(    0   ,   0   )},
					new Complex[]{new Complex(  10.31   ,   0   ),
					new Complex(    10.31   ,   0   )},
					new Complex[]{new Complex(  0   ,   0   ),
					new Complex(    9.1 ,   0   )},
					new Complex[]{new Complex(  8.417   ,   0   ),
					new Complex(    0.1 ,   0   ),
					new Complex(    2.4 ,   0   ),
					new Complex(    0.47    ,   0   ),
					new Complex(    8.402   ,   0   )},
					new Complex[]{new Complex(  0   ,   0   ),
					new Complex(    10.55   ,   0   )},
					new Complex[]{new Complex(  0   ,   0   ),
					new Complex(    14.1    ,   0   ),
					new Complex(    7.7 ,   0   ),
					new Complex(    3.5 ,   0   ),
					new Complex(    8.1 ,   0   )},
					new Complex[]{new Complex(  0   ,   0   ),
					new Complex(    8.3 ,   0   )},
					new Complex[]{new Complex(  0   ,   0   ),
					new Complex(    9.5 ,   0   ),
					new Complex(    9.3 ,   0   ),
					new Complex(    7.7 ,   0   )}

#endregion
			};

		/// <summary>
		/// 同位体存在度. IsotopeAbundance[z][a]: z 原子番号, a 質量数
		/// </summary>
		public static readonly Dictionary<int, double>[] IsotopeAbundance = new Dictionary<int, double>[]{
			#region
			new Dictionary<int,double>{{0,0}},
			new Dictionary<int,double>{
			{1,99.985},
			{2,0.015},
			{3,0}},
			new Dictionary<int,double>{
			{3,0.00014},
			{4,99.99986}},
			new Dictionary<int,double>{
			{6,7.5},
			{7,92.5}},
			new Dictionary<int,double>{
			{9,100}},
			new Dictionary<int,double>{
			{10,19.92},
			{11,80.12}},
			new Dictionary<int,double>{
			{12,98.9},
			{13,1.1}},
			new Dictionary<int,double>{
			{14,99.63},
			{15,0.37}},
			new Dictionary<int,double>{
			{16,99.762},
			{17,0.038},
			{18,0.2}},
			new Dictionary<int,double>{
			{19,100}},
			new Dictionary<int,double>{
			{20,90.51},
			{21,0.27},
			{22,9.22}},
			new Dictionary<int,double>{
			{23,100}},
			new Dictionary<int,double>{
			{24,78.99},
			{25,10},
			{26,11.01}},
			new Dictionary<int,double>{
			{27,100}},
			new Dictionary<int,double>{
			{28,92.23},
			{29,4.67},
			{30,3.1}},
			new Dictionary<int,double>{
			{31,100}},
			new Dictionary<int,double>{
			{32,95.02},
			{33,0.75},
			{34,4.21},
			{36,0.02}},
			new Dictionary<int,double>{
			{35,75.77},
			{37,24.23}},
			new Dictionary<int,double>{
			{36,0.337},
			{38,0.063},
			{40,99.6}},
			new Dictionary<int,double>{
			{39,93.258},
			{40,0.012},
			{41,6.73}},
			new Dictionary<int,double>{
			{40,96.941},
			{42,0.647},
			{43,0.135},
			{44,2.086},
			{46,0.004},
			{48,0.187}},
			new Dictionary<int,double>{
			{45,100}},
			new Dictionary<int,double>{
			{46,8.2},
			{47,7.4},
			{48,73.8},
			{49,5.4},
			{50,5.2}},
			new Dictionary<int,double>{
			{50,0.25},
			{51,99.75}},
			new Dictionary<int,double>{
			{50,4.35},
			{52,83.79},
			{53,9.5},
			{54,2.36}},
			new Dictionary<int,double>{
			{55,100}},
			new Dictionary<int,double>{
			{54,5.8},
			{56,91.7},
			{57,2.2},
			{58,0.3}},
			new Dictionary<int,double>{
			{59,100}},
			new Dictionary<int,double>{
			{58,68.27},
			{60,26.1},
			{61,1.13},
			{62,3.59},
			{64,0.91}},
			new Dictionary<int,double>{
			{63,69.17},
			{65,30.83}},
			new Dictionary<int,double>{
			{64,48.6},
			{66,27.9},
			{67,4.1},
			{68,18.8},
			{70,0.6}},
			new Dictionary<int,double>{
			{69,60.1},
			{71,39.9}},
			new Dictionary<int,double>{
			{70,20.5},
			{72,27.4},
			{73,7.8},
			{74,36.5},
			{76,7.8}},
			new Dictionary<int,double>{
			{75,100}},
			new Dictionary<int,double>{
			{74,0.9},
			{76,9},
			{77,7.6},
			{78,23.5},
			{80,49.6},
			{82,9.4}},
			new Dictionary<int,double>{
			{79,50.69},
			{81,49.31}},
			new Dictionary<int,double>{
			{78,0.35},
			{80,2.25},
			{82,11.6},
			{83,11.5},
			{84,57},
			{86,17.3}},
			new Dictionary<int,double>{
			{85,72.17},
			{87,27.83}},
			new Dictionary<int,double>{
			{84,0.56},
			{86,9.86},
			{87,7},
			{88,82.58}},
			new Dictionary<int,double>{
			{89,100}},
			new Dictionary<int,double>{
			{90,51.45},
			{91,11.32},
			{92,17.19},
			{94,17.28},
			{96,2.76}},
			new Dictionary<int,double>{
			{93,100}},
			new Dictionary<int,double>{
			{92,14.84},
			{94,9.25},
			{95,15.92},
			{96,16.68},
			{97,9.55},
			{98,24.13},
			{100,9.63}},
			new Dictionary<int,double>{
			{99,0}},
			new Dictionary<int,double>{
			{96,5.5},
			{98,1.9},
			{99,12.7},
			{100,12.6},
			{101,17},
			{102,31.6},
			{104,18.7}},
			new Dictionary<int,double>{
			{103,100}},
			new Dictionary<int,double>{
			{102,1.02},
			{104,11.14},
			{105,22.33},
			{106,27.33},
			{108,26.46},
			{110,11.72}},
			new Dictionary<int,double>{
			{107,51.839},
			{109,48.161}},
			new Dictionary<int,double>{
			{106,1.25},
			{108,0.89},
			{110,12.51},
			{111,12.81},
			{112,24.13},
			{113,12.22},
			{114,28.72},
			{116,7.47}},
			new Dictionary<int,double>{
			{113,4.3},
			{115,95.7}},
			new Dictionary<int,double>{
			{112,1},
			{114,0.7},
			{115,0.4},
			{116,14.7},
			{117,7.7},
			{118,24.3},
			{119,8.6},
			{120,32.4},
			{122,4.6},
			{124,5.6}},
			new Dictionary<int,double>{
			{121,57.3},
			{123,42.7}},
			new Dictionary<int,double>{
			{120,0.096},
			{122,2.6},
			{123,0.908},
			{124,4.816},
			{125,7.14},
			{126,18.95},
			{128,31.69},
			{130,33.8}},
			new Dictionary<int,double>{
			{127,100}},
			new Dictionary<int,double>{
			{124,0.1},
			{126,0.09},
			{128,1.91},
			{129,26.4},
			{130,4.1},
			{131,21.2},
			{132,26.9},
			{134,10.4},
			{136,8.9}},
			new Dictionary<int,double>{
			{133,100}},
			new Dictionary<int,double>{
			{130,0.11},
			{132,0.1},
			{134,2.42},
			{135,6.59},
			{136,7.85},
			{137,11.23},
			{138,71.7}},
			new Dictionary<int,double>{
			{138,0.09},
			{139,99.91}},
			new Dictionary<int,double>{
			{136,0.19},
			{138,0.25},
			{140,88.48},
			{142,11.08}},
			new Dictionary<int,double>{
			{141,100}},
			new Dictionary<int,double>{
			{142,27.16},
			{143,12.18},
			{144,23.8},
			{145,8.29},
			{146,17.19},
			{148,5.75},
			{150,5.63}},
			new Dictionary<int,double>{
			{147,0}},
			new Dictionary<int,double>{
			{144,3.1},
			{147,15.1},
			{148,11.3},
			{149,13.9},
			{150,7.4},
			{152,26.6},
			{154,22.6}},
			new Dictionary<int,double>{
			{151,47.8},
			{153,52.2}},
			new Dictionary<int,double>{
			{152,0.2},
			{154,2.1},
			{155,14.8},
			{156,20.6},
			{157,15.7},
			{158,24.8},
			{160,21.8}},
			new Dictionary<int,double>{
			{159,100}},
			new Dictionary<int,double>{
			{156,0.06},
			{158,0.1},
			{160,2.34},
			{161,19},
			{162,25.5},
			{163,24.9},
			{164,28.1}},
			new Dictionary<int,double>{
			{165,100}},
			new Dictionary<int,double>{
			{162,0.14},
			{164,1.56},
			{166,33.4},
			{167,22.9},
			{168,27.1},
			{170,14.9}},
			new Dictionary<int,double>{
			{169,100}},
			new Dictionary<int,double>{
			{168,0.14},
			{170,3.06},
			{171,14.3},
			{172,21.9},
			{173,16.1},
			{174,31.8},
			{176,12.7}},
			new Dictionary<int,double>{
			{175,97.39},
			{176,2.61}},
			new Dictionary<int,double>{
			{174,0.2},
			{176,5.2},
			{177,18.6},
			{178,27.1},
			{179,13.7},
			{180,35.2}},
			new Dictionary<int,double>{
			{180,0.012},
			{181,99.988}},
			new Dictionary<int,double>{
			{180,0.1},
			{182,26.3},
			{183,14.3},
			{184,30.7},
			{186,28.6}},
			new Dictionary<int,double>{
			{185,37.4},
			{187,62.6}},
			new Dictionary<int,double>{
			{184,0.02},
			{186,1.58},
			{187,1.6},
			{188,13.3},
			{189,16.1},
			{190,26.4},
			{192,41}},
			new Dictionary<int,double>{
			{191,37.3},
			{193,62.7}},
			new Dictionary<int,double>{
			{190,0.01},
			{192,0.79},
			{194,32.9},
			{195,33.8},
			{196,25.3},
			{198,7.2}},
			new Dictionary<int,double>{
			{197,100}},
			new Dictionary<int,double>{
			{196,0.2},
			{198,10.1},
			{199,17},
			{200,23.1},
			{201,13.2},
			{202,29.6},
			{204,6.8}},
			new Dictionary<int,double>{
			{203,29.524},
			{205,70.476}},
			new Dictionary<int,double>{
			{204,1.4},
			{206,24.1},
			{207,22.1},
			{208,52.4}},
			new Dictionary<int,double>{
			{209,100}},
			new Dictionary<int,double>{
			{210,0}},
			new Dictionary<int,double>{
			{210,0}},
			new Dictionary<int,double>{
			{222,0}},
			new Dictionary<int,double>{
			{223,0}},
			new Dictionary<int,double>{
			{226,0}},
			new Dictionary<int,double>{
			{227,0}},
			new Dictionary<int,double>{
			{232,100}},
			new Dictionary<int,double>{
			{231,0}},
			new Dictionary<int,double>{
			{233,0},
			{234,0.0051},
			{235,0.7201},
			{238,99.275}},
			new Dictionary<int,double>{
			{237,0}},
			new Dictionary<int,double>{
			{238,0},
			{239,0},
			{240,0},
			{242,0}},
			new Dictionary<int,double>{
			{243,0}},
			new Dictionary<int,double>{
			{244,0},
			{246,0},
			{248,0}}

#endregion
		};

		/// <summary>
		/// 平均イオン化エネルギー(keV), mode 1: Ducumb et al. 1968, mode 2: Berger 1964, mode 3: Pouchou and Pichoir 1991
		/// </summary>
		/// <param name="z"></param>
		/// <returns></returns>
		public static double MeanExcitationEnergy(int z, int mode = 0)
		{
			if (mode == 0)
				return z * (14 * (1 - Math.Exp(-0.1 * z)) + 75.5 / Math.Pow(z, z / 7.5) - z / (z + 100.0)) / 1000;
			else if (mode == 1)
				return z * (9.76 + 58.8 * Math.Pow(z, -1.19)) / 1000;
			else
				return z * (10.04 + 8.25 * Math.Exp(-z / 11.22)) / 1000;
		}

		/// <summary>
		/// Stopping Power Factor
		/// </summary>
		/// <param name="z"></param>
		/// <param name="line"></param>
		/// <param name="incidentEnergy"></param>
		/// <returns></returns>
		public static double StoppingFactor(double ec, double e0, int z)
		{
			return z / AtomicWeight(z) * Math.Log(1.166 * (2 * e0 + ec) / 3 / AtomConstants.MeanExcitationEnergy(z));
		}

		/// <summary>
		/// Back Scattered Factor
		/// </summary> Ec臨界励起エネルギー, E0入射エネルギー, z原子番号
		/// <param name="z"></param>
		/// <param name="line"></param>
		/// <param name="incidentEnergy"></param>
		/// <returns></returns>
		public static double BackScatteredFactor(double ec, double e0, int z)
		{
			double u = ec / e0;
			double r = 1;
			for (int i = 0; i < 5; i++)
			{
				double a = 0;
				for (int j = 0; j < 6; j++)
					a += Ducumb[i][j] * Math.Pow(u, j);
				r += Math.Pow(z * 0.01, i + 1) * a;
			}
			return r;
		}

		private static readonly double[][] Ducumb = new double[][]
		{
			#region
			new double[]{ -0.581,  +2.162, -5.137,     +9.213,   -8.619,  +2.962},
			new double[]{ -1.609,  -8.298, +28.791,   -47.744,  +46.540, -17.676},
			new double[]{ +5.400, +19.184, -75.733,  +120.050, -110.700, +41.792},
			new double[]{ -5.725, -21.645, +88.128,  -136.060, +117.750, -42.445},
			new double[]{ +2.095,  +8.947, -36.510,   +55.694,  -46.079, +15.851}
			#endregion
		};

		private static readonly double[][] Bishop1966 = new double[][]{
			#region
			new double[]{1.0088E2,-7.6070E-1,-3.5702E-3,1.6329E-4,-9.6521E-7},
			new double[]{-6.1134E-1,6.0271E-1,1.6222E2,-4.5936E-4,2.5267E-6},
			new double[]{-9.1447E-1,2.9326E0,-7.636E-1,2.8558E-3,-1.3294E-5},
			new double[]{-7.0753E-1,-4.6855E0,2.9116E-1,-4.6797E-3,2.1597E-5},
			new double[]{1.3735E0,1.9015E0,-1.2703E-1,2.1144E-3,-9.8423E-6}
#endregion
		};

		private static readonly object lockObjForMassAbsorption = new object();

		/// <summary>
		/// 質量吸収係数を覚えておく
		/// </summary>
		private static Dictionary<double, double>[] massAbsorption = new Dictionary<double, double>[100];

		/// <summary>
		/// エネルギー(keV)と吸収体元素を与えて、質量吸収係数を返す
		/// </summary>
		/// <param name="energy"></param>
		/// <param name="z"></param>
		/// <returns></returns>
		public static double MassAbsorption(double energy, int z)
		{
			if (z < 1 || z > AtomConstantsSub.MassAbsorptionCoefficient.Length || energy <= 0) return double.NaN;
			if (massAbsorption[z] != null && massAbsorption[z].ContainsKey(energy))
				return massAbsorption[z][energy];

			//どのセグメントに属するかを決める
			int segNo = 0;
			for (int j = AtomConstantsSub.MassAbsorptionCoefficient[z].Length - 1; j >= 0 && segNo == 0; j--)//上位から順に探す
				if (energy >= AtomConstantsSub.MassAbsorptionCoefficient[z][j][0].X)
					segNo = j;
			PointD[] coef = AtomConstantsSub.MassAbsorptionCoefficient[z][segNo].Select(e=>new PointD(e)).ToArray();

			//点の位置を探す
			int position = int.MinValue;//この値と この値+1 の間のindexにxが存在する
			if (coef[0].X > energy)
				position = -1;
			else if (coef[coef.Length - 1].X < energy)
				position = coef.Length - 1;
			else
				for (int i = 0; i < coef.Length && position == int.MinValue; i++)
					if (energy == coef[i].X)
					{
						if (massAbsorption[z] == null)
							massAbsorption[z] = new Dictionary<double, double>();
						lock (lockObjForMassAbsorption)
							massAbsorption[z].Add(energy, coef[i].Y);
						return coef[i].Y;
					}
					else if (i < coef.Length - 1 && energy > coef[i].X && energy < coef[i + 1].X)
						position = i;

			int pointNum = 3, order = 2;
			List<PointD> pt = new List<PointD>();
			//次に、この点から前後にPointNumだけ近い点を探す
			for (int i = Math.Max(position - pointNum, 0); i < Math.Min(position + pointNum + 1, coef.Length); i++)
				pt.Add(coef[i]);
			while (pt.Count > pointNum)
				pt.RemoveAt(Math.Abs(pt[0].X - energy) > Math.Abs(pt[pt.Count - 1].X - energy) ? 0 : pt.Count - 1);

			if (pt.Count < pointNum)
			{
				pointNum = pt.Count;
				if (order > pointNum - 1)
					order = pointNum - 1;
			}

			//計算精度のため、xの範囲を1から+2に変換する 式は X = c1 x + c2;
			double c1 = 1.0 / (pt[pt.Count - 1].X - pt[0].X);
			double c2 = 1 - pt[0].X * c1;

			var m = new DenseMatrix(pointNum, order + 1);
			var y = new DenseMatrix(pointNum, 1);
			for (int j = 0; j < pointNum; j++)
			{
				y[j, 0] = pt[j].Y;
				for (int i = 0; i < order + 1; i++)
					m[j, i] = Math.Pow(c1 * pt[j].X + c2, i);
			}
			double value = 0;

			if (!(m.Transpose() * m).TryInverse(out Matrix inv))
				return double.NaN;

			var a = inv * m.Transpose() * y;

			for (int j = 0; j < order + 1; j++)
				value += a[j, 0] * Math.Pow(c1 * energy + c2, j);

			if (massAbsorption[z] == null)
				massAbsorption[z] = new Dictionary<double, double>();

			if (massAbsorption[z].Count < 1E4)
				lock (lockObjForMassAbsorption)
					massAbsorption[z].Add(energy, value);

			return value;
		}

		public static double NominalDensity(int z) => AtomConstantsSub.NominalDensity[z];

		public static double AbsorptionJumpRatio(int z, XrayLineEdge edge)
		{
			double ec = CharacteristicXrayEnergy(z, edge);
			for (int k = 1; k < AtomConstantsSub.MassAbsorptionCoefficient[z].Length; k++)
				if (AtomConstantsSub.MassAbsorptionCoefficient[z][k][0].X == ec)
					return AtomConstantsSub.MassAbsorptionCoefficient[z][k][0].Y / AtomConstantsSub.MassAbsorptionCoefficient[z][k - 1][AtomConstantsSub.MassAbsorptionCoefficient[z][k - 1].Length - 1].Y;
			return double.NaN;
		}

		/// <summary>
		/// http://www.nist.gov/pml/data/ffast/index.cfm のデータを読み込んで、コードを吐き出す関数
		/// </summary>
		public static void ReadChantlerData(string[] fileNames)
		{
			#region
			var sbEdgeEnergy = new StringBuilder();
			sbEdgeEnergy.AppendLine("switch (z)");
			sbEdgeEnergy.AppendLine("{");

			var sbAbsorption = new StringBuilder();
			sbAbsorption.AppendLine("public static PointD[][][] MassAbsorptionCoefficient = new PointD[][][]");
			sbAbsorption.AppendLine("{");
			sbAbsorption.AppendLine("new PointD[][]{new PointD[]{}},");

			foreach (string fileName in fileNames)
			{
				List<string> str = new List<string>();
				string strTemp;
				System.IO.StreamReader reader = new System.IO.StreamReader(fileName);
				while ((strTemp = reader.ReadLine()) != null)
					str.Add(strTemp);
				reader.Close();

				int i = 0;
				int z = Convert.ToInt32(str[1].Split(new char[] { ',' })[0].Replace("<b>Z=", ""));//2行目から原子番号を読み取る
				//edgeの値を読み取る
				while (!str[i].Contains("edge")) i++;
				int edgeNo = Convert.ToInt32(str[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
				i += 2;

				List<PointD> edge = new List<PointD>();
				sbEdgeEnergy.AppendLine("case " + z.ToString() + ": switch (line)");
				sbEdgeEnergy.AppendLine("{");
				if (z == 1)
				{
					sbEdgeEnergy.AppendLine("case XrayLineEdge.K : return 1.36000E-02;");
					edge.Add(new PointD(1.36000E-02, double.PositiveInfinity));
					edge.Add(new PointD(1.36000E-02, double.NegativeInfinity));
				}
				else if (z == 2)
				{
					sbEdgeEnergy.AppendLine("case XrayLineEdge.K : return 2.46000E-02;");
					edge.Add(new PointD(2.46000E-02, double.PositiveInfinity));
					edge.Add(new PointD(2.46000E-02, double.NegativeInfinity));
				}
				else
					while (!str[i].Contains("</pre>") && !str[i].Contains("<dd>"))
					{
						str[i] = str[i].Replace(" VII", "7"); str[i] = str[i].Replace(" VI", "6"); str[i] = str[i].Replace(" IV", "4");
						str[i] = str[i].Replace(" V", "5"); str[i] = str[i].Replace(" III", "3"); str[i] = str[i].Replace(" II", "2");
						str[i] = str[i].Replace(" I", "1");
						string[] temp = str[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
						if (temp.Length % 2 == 0)
							for (int j = 0; j < temp.Length; j += 2)
							{
								sbEdgeEnergy.AppendLine("case XrayLineEdge." + temp[j] + ": return " + temp[j + 1] + ";");
								//edge.Add(new PointD(Convert.ToDouble(temp[j + 1]), -((temp[j][0] - 'J') * 10 + (temp[j].Length > 1 ? temp[j][1] - '0' : 0))));
								if (edge.Count == 0 || (edge.Count > 0 && edge[edge.Count - 1].X != Convert.ToDouble(temp[j + 1])))
								{
									edge.Add(new PointD(Convert.ToDouble(temp[j + 1]), double.PositiveInfinity));
									edge.Add(new PointD(Convert.ToDouble(temp[j + 1]), double.NegativeInfinity));
								}
							}
						i++;
					}
				sbEdgeEnergy.AppendLine("default: return double.NaN;");
				sbEdgeEnergy.AppendLine("}");

				while (!str[i].Contains("Photoelectric")) i++;
				i += 2;
				List<PointD> absorp = new List<PointD>(edge.ToArray());
				sbAbsorption.AppendLine("new PointD[][]{");
				for (; i < str.Count - 1; i++)
					absorp.Add(new PointD(Convert.ToDouble(str[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]), Convert.ToDouble(str[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1])));
				//sbAbsorption.AppendLine("new PointD(" + str[i].Replace("  ", ",") + ")" + (i == str.Count - 2 ? "" : ","));
				absorp.Sort();
				var pf = new List<Profile> { new Profile() };
				for (int j = 0; j < absorp.Count; j++)
				{
					if (double.IsPositiveInfinity(absorp[j].Y) && pf[pf.Count - 1].Pt.Count > 0)
						pf.Add(new Profile());
					pf[pf.Count - 1].Pt.Add(absorp[j]);
				}
				for (int j = 0; j < pf.Count; j++)
					if (pf[j].Pt.Count == 1 && double.IsInfinity(pf[pf.Count - 1].Pt[0].Y))
						pf.RemoveAt(j--);
				for (int j = 0; j < pf.Count; j++)
				{
					for (int k = 0; k < pf[j].Pt.Count; k++)
						if (double.IsInfinity(pf[j].Pt[k].Y))
						{
							double x = pf[j].Pt[k].X;
							pf[j].Pt.RemoveAt(k);
							double y = 0;
							for (int n = 3; n >= 1; n--)
							{
								y = pf[j].GetValue(x, n, n - 1);
								if (!double.IsNaN(y) && !double.IsInfinity(y))
									break;
							}
							pf[j].Pt.Add(new PointD(x, y));
							pf[j].Sort();
						}
				}
				for (int j = 0; j < pf.Count; j++)
				{
					sbAbsorption.AppendLine("new PointD[]{");
					for (int k = 0; k < pf[j].Pt.Count; k++)
						sbAbsorption.AppendLine("new PointD(" + pf[j].Pt[k].X.ToString("E6") + "," + pf[j].Pt[k].Y.ToString("E6") + (k == pf[j].Pt.Count - 1 ? ")" : "),"));
					sbAbsorption.AppendLine("}" + (j == pf.Count - 1 ? "" : ","));
				}
				sbAbsorption.AppendLine("}" + (fileName.Contains("92") ? "" : ","));
			}
			sbEdgeEnergy.AppendLine("default: return double.NaN;");
			sbEdgeEnergy.AppendLine("}");

			sbAbsorption.AppendLine("};");
			//Clipboard.SetDataObject(sbEdgeEnergy.ToString(), true);
			Clipboard.SetDataObject(sbAbsorption.ToString(), true);
			#endregion
		}
	}
}