using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using DMat = MathNet.Numerics.LinearAlgebra.Complex.DenseMatrix;
using DVec = MathNet.Numerics.LinearAlgebra.Complex.DenseVector;
using static System.Numerics.Complex;

namespace Crystallography
{
	public class IntegralProperty
	{
		//光学系全体に関係するところ
		/// <summary>
		/// 線源の波長
		/// </summary>
		public double WaveLength;

		public WaveProperty WaveProperty;

		/// <summary>
		/// カメラ長1 (サンプルからダイレクトスポットまでの距離)
		/// </summary>
		public double FilmDistance;//カメラ長

		/// <summary>
		/// カメラ長1 (サンプルからダイレクトスポットまでの距離)
		/// </summary>
		public double CameraLength1 => FilmDistance;//カメラ長

		public enum CameraEnum { FlatPanel, Gandolfi }

		public CameraEnum Camera { get; set; } = CameraEnum.FlatPanel;

		//IPの性質に関するところ

		/// <summary>
		/// ソース画像の幅
		/// </summary>
		public int SrcWidth;

		/// <summary>
		/// ソース画像の高さ
		/// </summary>
		public int SrcHeight;

		/// <summary>
		/// センター(Direct Spot)のx位置
		/// </summary>
		public double CenterX;

		/// <summary>
		/// センター(Direct Spot)のy位置
		/// </summary>
		public double CenterY;

		/// <summary>
		/// ピクセルサイズ
		/// </summary>
		public double PixSizeX, PixSizeY;

		/// <summary>
		/// ピクセルの歪み角度
		/// </summary>
		public double ksi = 0;

		/// <summary>
		/// IPの回転角度
		/// </summary>
		public double phi;

		/// <summary>
		/// IPの回転軸の角度;
		/// </summary>
		public double tau;

		public double GandolfiRadius { get; set; }

		/// <summary>
		/// 湾曲パラメータ
		/// </summary>
		public double SpericalRadiusInverse { get; set; } = 0;

		//一次元化の方法

		/// <summary>
		/// trueの場合はコンセントリックモード　falseの場合はラディアルモード
		/// </summary>
		public bool ConcentricMode;

		/// <summary>
		/// コンセントリックモードの場合、start, end ,stepがアングルモードかd値モードか。　ラディアルモードの場合, RadiusとRadiusRangeがアングルモードか角度モードか
		/// </summary>
		public HorizontalAxis Mode;

		/// <summary>
		/// コンセントリック、ラディアルモード両方で、積分対象となる角度範囲を表示するかどうか
		/// </summary>

		public double StepAngle, StartAngle, EndAngle;
		public double StepLength, StartLength, EndLength;
		public double StepDspacing, StartDspacing, EndDspacing;

		public double RadialSectorAngle;
		public double RadialRadiusAngle, RadialRadiusAngleRange;
		public double RadialRadiusDspacing, RadialRadiusDspacingRange;

		/// <summary>
		/// trueの場合は矩形モード falseの場合はセクターモード
		/// </summary>
		public bool IsRectangle;

		public int ThresholdMax, ThresholdMin;//閾値

		public int Edge;
		public bool DoesExcludeEdge;

		public bool IsTiltCorrection;

		public bool IsBraggBrentanoMode;//ブラッグブレンターノモードで出力するかどうか

		//Rectangleモードのとき
		public bool IsFull;

		public double RectangleBand;//バンドの太さ
		public double RectangleAngle;//角度
		public bool RectangleIsBothSide;//半直線かどうか

										//Sectorモードのとき
		public double SectorStartAngle;//開始角度

		public double SectorEndAngle;//終了角度

		public IntegralProperty()
		{
		}
	}

	/// <summary>
	/// DebyeScherrer の概要の説明です。
	/// </summary>
	public static class Ring
	{
		#region フィールド、メソッド、enum

		public enum Rotation { Clockwise, Counterclockwise }
		public enum Direction { Right, Left, Top, Bottom }

		private static readonly object lockObj = new();


		public enum ImageTypeEnum
		{
			#region 画像タイプ

			Unknown,
			Rigaku_RAxis_IV,
			Rigaku_RAxis_V,
			Fuji_BAS2000,
			Fuji_BAS2500,
			Brucker_CCD,
			Fuji_FDL,
			IPAImage,
			ITEX, //中身は単なるtiff
			RayonixSX200,//
			FLA7000, //GEヘルスケアのgel形式 (tiff)
			Rigaku_RAxis_IV_Osc,//RAxisIVの揺動
			Tiff,//Tiff

			/// <summary>
			/// MarresearchのMARファイル
			/// </summary>
			MAR,

			/// <summary>
			/// MarresearchのCCD
			/// </summary>
			MCCD,

			/// <summary>
			/// Perkin Elmer社のフラットパネル
			/// </summary>
			HIS,

			/// <summary>
			/// HDF5形式 (SACLAのbl3)
			/// </summary>
			HDF5,

			/// <summary>
			/// DigitalMicrograph
			/// </summary>
			DM,

			/// <summary>
			/// ADSC社
			/// </summary>
			ADSC,

			/// <summary>
			/// RadIcon社 (拡張子raw)
			/// </summary>
			RadIcon,

			/// <summary>
			/// Dexela社, (拡張子SMV)
			/// </summary>
			SMV,

			/// <summary>
			/// RadIcon社、PFで使用されている (references\ImageExsample\BL18c 柴咲さん )
			/// </summary>
			RadIconPF,

			/// <summary>
			/// MRC2014ファイル. FEI社のTEMのカメラで使われる. http://www.ccpem.ac.uk/mrc_format/mrc_format.php および \references\TalosF200\Manual を参照
			/// </summary>
			MRC



			#endregion 画像タイプ
		}

		//public static WaitDlg wd;
		public static Size SrcImgSize = new Size();

		public static double[] R2;
		public static List<double> Intensity = new List<double>();
		public static List<double> IntensityOriginal = new List<double>();
		public static Size SrcImgSizeOriginal = new Size();

		//バックグラウンド演算で使用
		public static List<double> Background = new List<double>();

		public static double BackgroundCoeff = 1;

		//SequentialImage(*.his,*.h5)で利用する変数
		public static List<List<double>> SequentialImageIntensities = new List<List<double>>();

		public static List<string> SequentialImageNames = new List<string>();

		//HDF *h5ファイルでのみに使用する
		public static List<double> SequentialImageEnergy = new List<double>();

		public static List<double> SequentialImagePulsePower = new List<double>();
		public static bool PulsePowerNormarized = false;
		public static List<List<double>> SequentialImageEnergySpectrum = new List<List<double>>();

		public static SortedList<uint, int> Frequency = new SortedList<uint, int>();

		public static ImageTypeEnum ImageType = ImageTypeEnum.Unknown;

		public static List<bool> IsValid = new List<bool>();//有効な(マスクされていない点かどうか)
		//public static ParallelQuery<bool> IsValidP;

		public static List<bool> IsSpots = new List<bool>();//スポット状の点かどうか
		//public static ParallelQuery<bool> IsSpotsP;

		public static List<bool> IsThresholdOver = new List<bool>();
		//public static ParallelQuery<bool> IsThresholdOverP;

		public static List<bool> IsThresholdUnder = new List<bool>();//飽和しているかどうか
		//public static ParallelQuery<bool> IsThresholdUnderP;


		/// <summary>
		/// 指定された積分領域(矩形、セクター)の範囲外の場合はtrue
		/// </summary>
		public static List<bool> IsOutsideOfIntegralRegion = new List<bool>();//積分エリアの外(或いは選択領域の外)
		//public static ParallelQuery<bool> IsOutsideOfIntegralRegionP;


		/// <summary>
		/// 指定された積分対象角度の範囲外の場合はtrue
		/// </summary>
		public static List<bool> IsOutsideOfIntegralProperty = new List<bool>();//エリアの外(或いは選択領域の外)
		//public static ParallelQuery<bool> IsOutsideOfIntegralPropertyP;


		public static string Comments = "";

		//  public static double ScaleFactor = 1; //強度に乗ずる因子 (入射X線強度でノーマライズする場合)

		public static bool[] IsCalcPosition;

		public static Rotation ChiRotation = Rotation.Clockwise;
		public static Direction ChiDirection = Direction.Right;

		public static int xMax, xMin;
		public static int[] yThreadMin;
		public static int[] yThreadMax;
		public static IntegralProperty IP;

		/*
		/// <summary>
		/// 1ピクセルが相当する実空間(あるいは逆空間)の長さ (単位は PixelUnitで指定)
		/// </summary>
		public static double PixelScale;
		/// <summary>
		/// 1ピクセルが相当する実空間(あるいは逆空間)の単位
		/// </summary>
		public static PixelUnitEnum PixelUnit;
		*/

		public static DigitalMicrograph.Property DigitalMicrographProperty;

		public static MRC MRC;

		/// <summary>
		/// 生データのピクセルあたりのビット数
		/// </summary>
		public static int BitsPerPixels = 4;

		private static double TanKsi, SinTau, CosTau, SinPhi, CosPhi, Numer1, Numer2, Numer3, Denom1, Denom2;
		#endregion

		static Ring()
		{
			//IsValidP = IsValid.AsParallel();
			//IsSpotsP = IsSpots.AsParallel();
			//IsThresholdOverP = IsThresholdOver.AsParallel();
			//IsThresholdUnderP = IsThresholdUnder.AsParallel();

			//IsOutsideOfIntegralPropertyP = IsOutsideOfIntegralProperty.AsParallel();
			//IsOutsideOfIntegralRegionP = IsOutsideOfIntegralRegion.AsParallel();

		}


		#region CalcFreq　Frequencyを計算
		//Frequencyを計算
		public static void CalcFreq()
		{
			if (Intensity == null || Intensity.Count == 0) return;
			Frequency.Clear();
			double unit = 1.2;
			var thread = Environment.ProcessorCount;

			Parallel.For(0, thread, i =>
			{
				int start = Intensity.Count / thread * i;
				int end = Math.Min(Intensity.Count / thread * (i + 1), Intensity.Count);
				SortedList<uint, int> freq = new SortedList<uint, int>();
				for (int j = start; j < end; j++)
				{
					uint value = (uint)Math.Pow(unit, (uint)Math.Log(Intensity[j], unit));

					if (freq.ContainsKey(value))
						freq[value] += 1;
					else
						freq.Add(value, 1);
				}
				lock (lockObj)
				{
					foreach (uint j in freq.Keys)
						if (Frequency.ContainsKey(j))
							Frequency[j] += freq[j];
						else
							Frequency.Add(j, freq[j]);
				}
			});
		}
		#endregion

		//傾き補正係数を計算
		public static void SetTiltParameter()
		{
			TanKsi = Math.Tan(IP.ksi);
			SinTau = Math.Sin(IP.tau);
			CosTau = Math.Cos(IP.tau);
			SinPhi = Math.Sin(IP.phi);
			CosPhi = Math.Cos(IP.phi);
			Numer1 = CosPhi * SinPhi - CosPhi * CosTau * SinPhi;
			Numer2 = CosPhi * CosPhi + CosTau * SinPhi * SinPhi;
			Numer3 = CosPhi * CosPhi * CosTau + SinPhi * SinPhi;
			Denom1 = CosPhi * SinTau;
			Denom2 = -SinPhi * SinTau;
		}

		#region Find Spot
		//ピクセルステップの平均値と標準偏差をもとめてスポットを検出する
		public static void FindSpots(IntegralProperty iP, double DeviationFactor)
		{
			int i;

			IP = iP;
			int w = iP.SrcWidth;
			int h = iP.SrcHeight;
			var thread = Environment.ProcessorCount;

			//各スレッドの上限と下限を決める
			yThreadMin = new int[thread];
			yThreadMax = new int[thread];
			int yStep = IP.SrcHeight / thread;
			for (i = 0; i < thread; i++)
			{
				yThreadMin[i] = i * yStep;
				yThreadMax[i] = (i + 1) * yStep;
			}
			yThreadMax[thread - 1] = IP.SrcHeight;

			//まずどのピクセルがどのステップに属するかを決める
			var r = new int[IP.SrcHeight * IP.SrcWidth];
			var rMax = int.MinValue;
			var tempRMax = new int[thread];
			SetTiltParameter();

			Parallel.For(0, thread, i => FindSpotsThread0(yThreadMin[i], yThreadMax[i], ref r, ref tempRMax[i]));


			//rMaxの最大値をきめる
			rMax = tempRMax.Max();

			if (rMax == 0)
				return;

			rMax++;

			//Profile(各ステップごとの強度)とPixels(各ステップに寄与したピクセル数)を作成
			double[][] tempSumOfIntensity = new double[thread][];
			double[][] tempSumOfIntensitySquare = new double[thread][];
			double[][] tempContributedPixels = new double[thread][];
			for (i = 0; i < thread; i++)
			{
				tempSumOfIntensity[i] = new double[rMax];
				tempSumOfIntensitySquare[i] = new double[rMax];
				tempContributedPixels[i] = new double[rMax];
			}
			//ここからスレッド1起動
			Parallel.For(0, thread, i => FindSpotsThread1(yThreadMin[i], yThreadMax[i], r, ref tempSumOfIntensity[i], ref tempSumOfIntensitySquare[i], ref tempContributedPixels[i]));

			//Thread1の結果をまとめる
			double[] ContributedPixels = new double[rMax];
			double[] SumOfIntensity = new double[rMax];
			double[] SumOfIntensitySquare = new double[rMax];
			for (i = 0; i < rMax; i++)
				for (int t = 0; t < thread; t++)
				{
					SumOfIntensity[i] += tempSumOfIntensity[t][i];
					SumOfIntensitySquare[i] += tempSumOfIntensitySquare[t][i];
					ContributedPixels[i] += tempContributedPixels[t][i];
				}

			//標準偏差をもとめる
			double tempDeviation, tempAverage;
			double[] OverLimit = new double[rMax];
			double[] UnderLimit = new double[rMax];
			for (i = 0; i < rMax; i++)
				if (ContributedPixels[i] < 2.0)
				{
					OverLimit[i] = 0;
					UnderLimit[i] = 0;
				}
				else
				{
					tempDeviation = DeviationFactor * Math.Sqrt(
						(ContributedPixels[i] * SumOfIntensitySquare[i] - SumOfIntensity[i] * SumOfIntensity[i])
						/ ContributedPixels[i] / (ContributedPixels[i] - 1)
						);
					tempAverage = SumOfIntensity[i] / ContributedPixels[i];
					OverLimit[i] = tempAverage + tempDeviation;
					UnderLimit[i] = tempAverage - tempDeviation;
				}
			int n = 0;
			int total = w * h;
			int maskArea = 3;
			for (int p = 0; p < total; p++)
			{
				if (!IsSpots[n] && !IsOutsideOfIntegralRegion[n])
					if (Intensity[n] > OverLimit[r[n]])
					{
						IsSpots[n] = true;

						for (i = -maskArea; i <= maskArea; i++)
							for (int j = -maskArea; j <= maskArea; j++)
							{
								if (i * i + j * j < maskArea * maskArea)
									if (n + i + j * w >= 0 && n + i + j * w < total)
										IsSpots[n + i + j * w] = true;
							}
					}
				n++;
			}
		}

		//private delegate void FindSpotsThread0Delegate(int yMin, int yMax, ref int[] r, ref int rMax);

		public static void FindSpotsThread0(int yMin, int yMax, ref int[] r, ref int rMax)
		{
			double centerX = IP.CenterX;
			double centerY = IP.CenterY;
			rMax = ushort.MinValue;
			double X, Y, tempY2TanKsi, numer4, tempY2numer1, tempY2numer3, tempY2denom1plusFD, tempX, tempY2;
			double FD = IP.FilmDistance;
			double pixX = IP.PixSizeX;
			double pixY = IP.PixSizeY;
			int i, j;
			int w = IP.SrcWidth;
			int n = yMin * IP.SrcWidth; ;
			for (j = yMin; j < yMax; j++)
			{
				tempY2 = (j - centerY) * pixY;
				tempY2TanKsi = tempY2 * TanKsi;
				tempY2numer1 = tempY2 * Numer1;
				tempY2numer3 = tempY2 * Numer3;
				tempY2denom1plusFD = tempY2 * Denom1 + FD;

				for (i = 0; i < w; i++)
				{
					tempX = (i - centerX) * pixX + tempY2TanKsi;
					numer4 = FD / (tempY2denom1plusFD + tempX * Denom2);
					X = tempY2numer1 * numer4 + tempX * Numer2 * numer4;
					Y = tempX * Numer1 * numer4 + tempY2numer3 * numer4;

					r[n] = (ushort)(Math.Sqrt(X * X + Y * Y) / pixX + 0.5);
					if (rMax < r[n])
						rMax = r[n];
					n++;
				}
			}
		}

		private delegate void FindSpotsThread1Delegate(int yMin, int yMax, int[] r, ref double[] SumOfIntensity, ref double[] SumOfIntensitySquare, ref double[] pixels);

		public static void FindSpotsThread1(int yMin, int yMax, int[] r, ref double[] SumOfIntensity, ref double[] SumOfIntensitySquare, ref double[] pixels)
		{
			int i, j;
			int w = IP.SrcWidth;
			int n = yMin * IP.SrcWidth; ;
			for (j = yMin; j < yMax; j++)
				for (i = 0; i < w; i++)
				{
					if (!IsSpots[n] && !IsOutsideOfIntegralRegion[n])
					{
						SumOfIntensity[r[n]] += Intensity[n];
						SumOfIntensitySquare[r[n]] += Intensity[n] * Intensity[n];
						pixels[r[n]]++;
					}
					n++;
				}
		}
		#endregion

		#region 画像の回転・反転

		/// <summary>
		/// 画像を、反転、回転させる. rotateは0: 無回転、1:90度回転, 2: 180度回転、 3: 270度回転
		/// </summary>
		/// <param name="src"></param>
		/// <param name="width"></param>
		/// <param name="flipV"></param>
		/// <param name="flipH"></param>
		/// <param name="rotate"></param>
		/// <returns></returns>
		public static List<double> FlipAndRotate(IEnumerable<double> src, int width, bool flipV, bool flipH, int rotate)
		{
			var srcArray = src.ToArray();
			int height = srcArray.Length / width;
			var flag = (flipV ? 2 : 0) + (flipH ? 1 : 0);
			var convertIndexFlip = flag switch
			{
				0 => new Func<int, int, (int x, int y)>((w, h) => (w, h)),
				1 => new Func<int, int, (int x, int y)>((w, h) => (width - w - 1, h)),
				2 => new Func<int, int, (int x, int y)>((w, h) => (w, height - h - 1)),
				_ => new Func<int, int, (int x, int y)>((w, h) => (width - w - 1, height - h - 1))
			};

			var convertIndexRotate = rotate switch
			{
				0 => new Func<(int x, int y), int>(p => width * p.y + p.x),
				1 => new Func<(int x, int y), int>(p => height * p.x + height - p.y - 1),
				2 => new Func<(int x, int y), int>(p => (height - p.y - 1) * width + (width - p.x - 1)),
				_ => new Func<(int x, int y), int>(p => height * (width - p.x - 1) + p.y)
			};

			var result = new double[srcArray.Length];
			if (flag != 0 || rotate != 0)
				for (int h = 0; h < height; h++)
					for (int w = 0; w < width; w++)
						result[convertIndexRotate(convertIndexFlip(w, h))] = srcArray[h * width + w];
			else
				result = src.ToArray();

			return new List<double>(result);
		}
		#endregion

		#region 偏光補正
		public static List<double> CorrectPolarization(int rotate)
		{
			SetTiltParameter();

			double fd = IP.FilmDistance, fd2 = fd * fd;
			double sizeX = IP.PixSizeX, sizeY = IP.PixSizeY;
			double centX = IP.CenterX, centY = IP.CenterY;

			//補正式は、Icorr = I / (sin(kai)^2 + cos(kai)^2 * cos(2th)^2 )
			// sin(kai)^2 = y^2 /  (x^2 + y^2)
			// cos(2th)^2 = fd^2 / (x^2 + y^2 + fd^2)
			// cos(kai)^2 = x^2 / (x^2 + y^2)
			//まとめると分母の部分は、 1 - x2 / (x2 + y2 + fd2))
			//var coeff1 = rotate == 0 || rotate == 2 ?
			//    new Func<double, double, double>((x2, y2) => (y2+fd2)  / (x2 + y2 + fd2)) :
			//    new Func<double, double, double>((x2, y2) => (x2+fd2) / (x2 + y2 + fd2));

			//20190906追記
			//補正式は、Icorr = I / (sin(kai)^2 + cos(kai)^2 * cos(2th)^2 ) / cos(2th)
			
			Func<double,double,double> coeff1 = rotate == 0 || rotate == 2 ?
				(x2, y2) => 2 * (y2 + fd2) / (x2 + y2 + 2 * fd2) :
				(x2, y2) => 2 * (x2 + fd2) / (x2 + y2 + 2 * fd2);

			//var coeff2 = new Func<double, double, double>((x2, y2) => Math.Sqrt( fd2 / (x2 + y2 + fd2)));

			var result = new double[Intensity.Count];
			//Parallel.Forを使わないほうが早い
			int i = 0;
			for (int pixY = 0; pixY < SrcImgSize.Height; pixY++)
			{
				double tempY = (pixY - centY) * sizeY;
				double temp4 = tempY * Denom1 + fd;
				double temp5 = tempY * Numer1;
				double temp6 = tempY * Numer3;
				double temp7 = tempY * TanKsi;

				for (int pixX = 0; pixX < SrcImgSize.Width; pixX++)
				{
					double tempX = (pixX - centX) * sizeX + temp7;
					double temp8 = fd / (temp4 + tempX * Denom2);
					double x = (temp5 + tempX * Numer2) * temp8;
					double y = (tempX * Numer1 + temp6) * temp8;
					result[i] = Intensity[i] / coeff1(x * x, y * y);// *coeff2(x * x, y * y);
					i++;
				}
			}
			return new List<double>(result);
		}
		#endregion

		#region SetMask

		static bool[] tempArray = new bool[1];

		/// <summary>
		/// スポットや閾値超のピクセルをマスクする関数
		/// このメソッドの前に適切にIsInsideAreaが設定されている必要がある。
		/// </summary>
		/// <param name="OmitSpots"></param>
		/// <param name="OmitTheresholdMin"></param>
		/// <param name="OmitTheresholdMax"></param>
		public static void SetMask(bool OmitSpots, bool OmitTheresholdMin, bool OmitTheresholdMax)
		{
			if (IsValid.Count != IsOutsideOfIntegralRegion.Count)
				return;

			if (tempArray.Length != IsValid.Count)
				tempArray = Enumerable.Repeat(true, IsOutsideOfIntegralRegion.Count).ToArray();

			IsValid.Clear();
			IsValid.AddRange(tempArray);

			if (OmitSpots)
			{
				for (int i = 0; i < IsOutsideOfIntegralRegion.Count; i++)
					if (IsOutsideOfIntegralRegion[i] || IsOutsideOfIntegralProperty[i] || IsSpots[i])
						IsValid[i] = false;
			}
			else
			{
				for (int i = 0; i < IsOutsideOfIntegralRegion.Count; i++)
					if (IsOutsideOfIntegralRegion[i] || IsOutsideOfIntegralProperty[i])
						IsValid[i] = false;
			}

			if (OmitTheresholdMin)
				for (int i = 0; i < IsOutsideOfIntegralRegion.Count; i++)
					if (IsThresholdUnder[i])
						IsValid[i] = false;

			if (OmitTheresholdMax)
				for (int i = 0; i < IsOutsideOfIntegralRegion.Count; i++)
					if (IsThresholdOver[i])
						IsValid[i] = false;
		}

		#endregion

		#region SetInsideArea
		/// <summary>
		/// 積分領域以外をマスクする関数　マスクするのは、次の三点
		/// ・指定した矩形あるいはセクター外の領域　
		/// ・エッジ領域　
		/// ・積分角度範囲に含まれない領域
		/// </summary>
		/// <param name="IP">IPのプロパティ</param>
		/// <param name="calcRegion">指定した矩形あるいはセクター外の領域を計算するかどうか</param>
		/// <param name="calcEdge">エッジ領域を計算するかどうか</param>
		/// <param name="calcProperty">積分角度範囲に含まれない領域を計算するかどうか</param>
		public static void SetInsideArea(IntegralProperty IP, bool calcRegion=true, bool calcEdge=true, bool calcProperty=true)
		{
			var thread = Environment.ProcessorCount;
			if (calcRegion)
			{
				IsOutsideOfIntegralRegion.Clear();
				if (IP.IsRectangle && IP.IsFull)
					IsOutsideOfIntegralRegion.AddRange(new bool[IP.SrcWidth * IP.SrcHeight]);
				else
				{
					IsOutsideOfIntegralRegion.AddRange(Enumerable.Repeat(true, IP.SrcWidth * IP.SrcHeight));

					int Height = IP.SrcHeight;
					int Width = IP.SrcWidth;

					var CenterX = IP.CenterX;
					var CenterY = IP.CenterY;
					var Band = IP.RectangleBand;
					if (IP.IsRectangle)
					#region Rectangleモードのとき
					{
						bool IsXY = false;
						double tan = Math.Tan(IP.RectangleAngle);
						double sin = Math.Sin(IP.RectangleAngle);
						double cos = Math.Cos(IP.RectangleAngle);
						double wx = Math.Abs(Band / sin);
						double wy = Math.Abs(IP.RectangleBand / cos);
						double cx, cy;
						if (Math.Abs(tan) > 1)
							IsXY = true;//縦方向に近い場合はTrue
						int jWidth;
						int startI, endI, midI;
						int startJ, endJ, midJ;
						if (IsXY)
						{
							double MinusCenterYPerTanPlusCenterX = -CenterY / tan + CenterX;

							if (IP.RectangleIsBothSide)
							{//IsXYがTrueで全直線モード
								for (int j = 0; j < Height; j++)
								{
									cx = j / tan + MinusCenterYPerTanPlusCenterX;
									startI = Math.Max(0, (int)(cx - wx + 0.5));
									endI = Math.Min(Width, (int)(cx + wx + 1.5));
									jWidth = j * Width;
									for (int i = startI; i < endI; i++)//バンドの内側のとき
										IsOutsideOfIntegralRegion[i + jWidth] = false;
								}
							}
							else
							{//IsXYがTrueで半直線モード
								double CenterXPerTanPlusCenterY = CenterX / tan + CenterY;
								if (sin > 0)//下に伸びた半直線のときは
								{
									startJ = Math.Max(0, (int)(CenterY - Band * Math.Abs(cos) + 0.5));//スタート地点
									midJ = (int)(CenterY + Band * Math.Abs(cos) + 0.5);//中間地点
									for (int j = startJ; j < Height; j++)
									{
										cx = j / tan + MinusCenterYPerTanPlusCenterX;
										startI = Math.Max(0, (int)(cx - wx + 0.5));
										endI = Math.Min(Width, (int)(cx + wx + 1.5));
										jWidth = j * Width;
										if (j > midJ)
											for (int i = startI; i < endI; i++)//バンドの内側のとき
												IsOutsideOfIntegralRegion[i + jWidth] = false;
										else
											for (int i = startI; i < endI; i++)//バンドの内側のとき
												if (j + i / tan > CenterXPerTanPlusCenterY)
													IsOutsideOfIntegralRegion[i + jWidth] = false;
									}
								}
								else//上に伸びた半直線のときは
								{
									midJ = (int)(CenterY - Band * Math.Abs(cos) + 0.5);
									endJ = Math.Min(Height, (int)(CenterY + Band * Math.Abs(cos) + 0.5));
									for (int j = 0; j < endJ; j++)
									{
										cx = j / tan + MinusCenterYPerTanPlusCenterX;
										startI = Math.Max(0, (int)(cx - wx + 0.5));
										endI = Math.Min(Width, (int)(cx + wx + 1.5));
										jWidth = j * Width;
										if (j < midJ)
											for (int i = startI; i < endI; i++)//バンドの内側のとき
												IsOutsideOfIntegralRegion[i + jWidth] = false;
										else
											for (int i = startI; i < endI; i++)//バンドの内側のとき
												if (j + i / tan < CenterXPerTanPlusCenterY)
													IsOutsideOfIntegralRegion[i + jWidth] = false;
									}
								}
							}
						}
						else//IsXYがFalse
						{
							double CenterYMinusTanCenterX = CenterY - tan * CenterX;
							if (IP.RectangleIsBothSide)
							{
								for (int i = 0; i < Width; i++)
								{
									cy = tan * i + CenterYMinusTanCenterX;
									startJ = Math.Max(0, (int)(cy - wy + 0.5));
									endJ = Math.Min(Height, (int)(cy + wy + 1.5));
									for (int j = startJ; j < endJ; j++)//バンドの内側のとき
										IsOutsideOfIntegralRegion[i + j * Width] = false;
								}
							}
							else
							{//半直線モードのとき
								double CenterYTanPlusCenterX = CenterY * tan + CenterX;

								if (cos > 0)//右に伸びた半直線のときは
								{
									startI = Math.Max(0, (int)(CenterX - Band * Math.Abs(sin) + 0.5));
									midI = (int)(CenterX + Band * Math.Abs(sin) + 0.5);
									for (int i = startI; i < Width; i++)
									{
										cy = tan * i + CenterYMinusTanCenterX;
										startJ = Math.Max(0, (int)(cy - wy + 0.5));
										endJ = Math.Min(Height, (int)(cy + wy + 1.5));
										if (i > midI)
											for (int j = startJ; j < endJ; j++)//バンドの内側のとき
												IsOutsideOfIntegralRegion[i + j * Width] = false;
										else
											for (int j = startJ; j < endJ; j++)
												if (i + j * tan > CenterYTanPlusCenterX)
													IsOutsideOfIntegralRegion[i + j * Width] = false;
									}
								}
								else//左に伸びた半直線のときは
								{
									midI = (int)(CenterX - Band * Math.Abs(sin) + 0.5);
									endI = Math.Max(Width, (int)(CenterX + Band * Math.Abs(sin) + 0.5));
									for (int i = 0; i < endI; i++)
									{
										cy = tan * i + CenterYMinusTanCenterX;
										startJ = Math.Max(0, (int)(cy - wy + 0.5));
										endJ = Math.Min(Height, (int)(cy + wy + 1.5));
										if (i < midI)
											for (int j = startJ; j < endJ; j++)//バンドの内側のとき
												IsOutsideOfIntegralRegion[i + j * Width] = false;
										else
											for (int j = startJ; j < endJ; j++)
												if (i + j * tan < CenterYTanPlusCenterX)
													IsOutsideOfIntegralRegion[i + j * Width] = false;
									}
								}
							}
						}
					}

					#endregion Rectangleモードのとき
					else
					#region Sectorモードのとき

					{
						//カイ角を考慮して、StartAngle, EndAngleを設定
						double startAngle = ChiRotation == Rotation.Clockwise ? IP.SectorStartAngle : -IP.SectorEndAngle;
						double endAngle = ChiRotation == Rotation.Clockwise ? IP.SectorEndAngle : -IP.SectorStartAngle;
						if (ChiDirection == Direction.Bottom)
						{
							startAngle += Math.PI / 2;
							endAngle += Math.PI / 2;
						}
						else if (ChiDirection == Direction.Left)
						{
							startAngle += Math.PI;
							endAngle += Math.PI;
						}
						else if (ChiDirection == Direction.Top)
						{
							startAngle += Math.PI * 3d / 2d;
							endAngle += Math.PI * 3d / 2d;
						}

						//startAngle, endAngleを -piからpiの範囲に変換する
						while (startAngle > Math.PI) startAngle -= 2 * Math.PI;
						while (startAngle < -Math.PI) startAngle += 2 * Math.PI;
						while (endAngle > Math.PI) endAngle -= 2 * Math.PI;
						while (endAngle < -Math.PI || endAngle < startAngle) endAngle += 2 * Math.PI;

						//ピクセル座標x,yを 球面座標に変換するFuncを定義
						double X1 = Math.Cos(startAngle), Y1 = Math.Sin(startAngle);
						double X2 = Math.Cos(endAngle), Y2 = Math.Sin(endAngle);
						Func<double, double, bool> func = endAngle - startAngle < Math.PI ?
							 func = (x, y) => x * Y1 - y * X1 < 0 && x * Y2 - y * X2 > 0 :
							func = (x, y) => x * Y1 - y * X1 < 0 || x * Y2 - y * X2 > 0;

						SetTiltParameter();
						Parallel.For(0, Height, j =>
						{
							for (int i = 0; i < Width; i++)
							{
								var (X, Y, Z) = ConvertCoordinateFromDetectorToRealSpace(i, j);
								if (func(X, Y))
									IsOutsideOfIntegralRegion[i + j * Width] = false;
							}
						});
					}

					#endregion Sectorモードのとき
				}
			}

			if (calcEdge)
			#region エッジを除去するとき

			{
				if (IP.DoesExcludeEdge)
				{
					int n = IP.Edge;
					//上辺
					for (int i = 0; i < Ring.IP.SrcHeight && i < n; i++)
						for (int j = 0; j < Ring.IP.SrcWidth; j++)
                            IsOutsideOfIntegralRegion[(i * Ring.IP.SrcWidth) + j] = true;
					//下辺
					for (int i = Math.Max(0, Ring.IP.SrcHeight - n); i < Ring.IP.SrcHeight; i++)
						for (int j = 0; j < Ring.IP.SrcWidth; j++)
                            IsOutsideOfIntegralRegion[(i * Ring.IP.SrcWidth) + j] = true;
					//左辺
					for (int j = 0; j < Ring.IP.SrcWidth && j < n; j++)
						for (int i = 0; i < Ring.IP.SrcHeight; i++)
                            IsOutsideOfIntegralRegion[(i * Ring.IP.SrcWidth) + j] = true;
					//右辺
					for (int j = Math.Max(0, Ring.IP.SrcWidth - n); j < Ring.IP.SrcWidth; j++)
						for (int i = 0; i < Ring.IP.SrcHeight; i++)
                            IsOutsideOfIntegralRegion[(i * Ring.IP.SrcWidth) + j] = true;
				}
			}
			#endregion エッジを除去するとき

			if (calcProperty)
			#region 積分角度範囲を除去するとき
			{
				IsOutsideOfIntegralProperty.Clear();
				IsOutsideOfIntegralProperty.AddRange(new bool[IP.SrcWidth * IP.SrcHeight]);

				//フラットパネルカメラの場合
				if (IP.Camera == IntegralProperty.CameraEnum.FlatPanel)
				{
					SetTiltParameter();

					double startCos, endCos;
					if (IP.ConcentricMode)
					{
						if (IP.Mode == HorizontalAxis.Angle)
						{
							startCos = Math.Cos(IP.StartAngle);
							endCos = Math.Cos(IP.EndAngle);
						}
						else if (IP.Mode == HorizontalAxis.Length)
						{
							startCos = IP.FilmDistance / Math.Sqrt(IP.StartLength * IP.StartLength + IP.FilmDistance * IP.FilmDistance);
							endCos = IP.FilmDistance / Math.Sqrt(IP.EndLength * IP.EndLength + IP.FilmDistance * IP.FilmDistance);
						}
						else
						{
							if (IP.WaveLength / 2.0 / IP.StartDspacing > 1.0)
								endCos = -1;
							else
								endCos = Math.Cos(2 * Math.Asin(IP.WaveLength / 2.0 / IP.StartDspacing));

							startCos = Math.Cos(2 * Math.Asin(IP.WaveLength / 2.0 / IP.EndDspacing));
						}
					}
					else
					{
						if (IP.Mode == HorizontalAxis.Angle)
						{
							startCos = Math.Cos(IP.RadialRadiusAngle - IP.RadialRadiusAngleRange);
							endCos = Math.Cos(IP.RadialRadiusAngle + IP.RadialRadiusAngleRange);
						}
						else
						{
							startCos = Math.Cos(2 * Math.Asin(IP.WaveLength / 2.0 / (IP.RadialRadiusDspacing + IP.RadialRadiusDspacingRange / 2)));
							endCos = Math.Cos(2 * Math.Asin(IP.WaveLength / 2.0 / (IP.RadialRadiusDspacing - IP.RadialRadiusDspacingRange / 2)));
						}
					}
					double endCos2 = endCos * endCos, startCos2 = startCos * startCos;

					if (startCos < 0) startCos2 = -startCos2;
					if (endCos < 0) endCos2 = -endCos2;

					double centerX = IP.CenterX, centerY = IP.CenterY, pixSizeX = IP.PixSizeX, pixSizeY = IP.PixSizeY,
						startAngle = IP.StartAngle, stepAngle = IP.StepAngle, fd = IP.FilmDistance;

					int width = IP.SrcWidth, height = IP.SrcHeight;
					
					Parallel.For(0, height, j =>
					{
						var tempY = (j - centerY) * pixSizeY;//IP平面上の座標系におけるY位置
						var tempY2TanKsi = tempY * TanKsi;
						var numer1TempY = Numer1 * tempY;
						var numer3TempY = Numer3 * tempY;
						var denom1tempYFD = Denom1 * tempY + fd;
						var jWidth = j * width;

						for (int i = 0; i < width; i++)
						{
							var tempX = (i - centerX) * pixSizeX + tempY2TanKsi;//IP平面上の座標系におけるX位置
							//以下のx,y,zがピクセル中心の空間位置
							var x = Numer2 * tempX + numer1TempY;
							var y = Numer1 * tempX + numer3TempY;
							var z = Denom2 * tempX + denom1tempYFD;

							var cos2 = z > 0 ? z * z / (x * x + y * y + z * z) : -z * z / (x * x + y * y + z * z);

							if (startCos2 < cos2 || cos2 < endCos2)
								IsOutsideOfIntegralProperty[i + jWidth] = true;
						}
					});
                }//フラットパネルモードここまで
				 //Gandolfiモードの時
				else
				{
					SetTiltParameter();
					double centerX = IP.CenterX, centerY = IP.CenterY;
					double pixSizeX = IP.PixSizeX, pixSizeY = IP.PixSizeY;
					int width = IP.SrcWidth;
					double r = IP.GandolfiRadius;
					double CL = IP.FilmDistance;

					Matrix3D m1 = new Matrix3D(CosTau, SinTau * SinPhi, SinTau * CosPhi, 0, CosPhi, -SinPhi, -SinTau, CosTau * SinPhi, CosTau * CosPhi);

					//ピクセル座標を実空間座標に変換するFunc
					var convPixelToReal = new Func<double, double, Vector3DBase>((x, y) => m1 * new Vector3DBase(
									 Math.Sin((x - centerX) * pixSizeX / r) * r,
									 pixSizeY * (-y + centerY),
									 (Math.Cos((x - centerX) * pixSizeX / r) - 1) * r));
					//実空間座標を2Θに変換するFunc
					var convRealToCos = new Func<Vector3DBase, double>((v) => (v.Z + CL) / Math.Sqrt(v.X * v.X + v.Y * v.Y + (v.Z + CL) * (v.Z + CL)));

					double maxCos = Math.Cos(IP.StartAngle);
					double minCos = Math.Cos(IP.EndAngle);

					//角度ステップの区切り位置を設定する
					Parallel.For(0, thread, i =>
					{
						int hUnit = IP.SrcHeight / thread;
						for (int y = hUnit * i; y < Math.Min(hUnit * (i + 1), IP.SrcHeight); y++)
							for (int x = 0; x < IP.SrcWidth; x++)
							{
								var cos = convRealToCos(convPixelToReal(x, y));
								if (cos < minCos || cos > maxCos)
									IsOutsideOfIntegralProperty[x + y * IP.SrcWidth] = true;
							}
					});
				}
			}

			#endregion 積分角度範囲を除去するとき
		}
		/// <summary>
		/// ピクセル座標(detX, detY)を実空間座標(X,Y,Z)に変換する。事前にピクセルサイズや、SetTiltParameterが設定されている必要がある。
		/// </summary>
		/// <param name="detX"></param>
		/// <param name="detY"></param>
		/// <returns></returns>
		public static (double X, double Y, double Z) ConvertCoordinateFromDetectorToRealSpace(double detX, double detY)
		{
			var tempY = (detY - IP.CenterY) * IP.PixSizeY;//IP平面上の座標系におけるY位置
			var tempX = (detX - IP.CenterX) * IP.PixSizeX + tempY * TanKsi;//IP平面上の座標系におけるX位置

			//以下のx,y,zが空間位置
			var realX = Numer2 * tempX + Numer1 * tempY;
			var realY = Numer1 * tempX + Numer3 * tempY;
			var realZ = Denom2 * tempX + Denom1 * tempY + IP.FilmDistance;

			if (IP.SpericalRadiusInverse != 0) //球面補正が必要な場合
			{
				var fd = IP.FilmDistance;
				//IPの法線ベクトル
				(double X, double Y, double Z) detector_normal = (Denom2, Denom1, -CosTau);
				//検出器中心(0,0,FD)からピクセルまでの距離
				double distance2 = realX * realX + realY * realY + (realZ - fd) * (realZ - fd), distance = Math.Sqrt(distance2);
				//検出器のダイレクトスポット方向に縮める割合
				double coeff_detector_palallel = Math.Sin(distance * IP.SpericalRadiusInverse) / distance / IP.SpericalRadiusInverse;
				//検出器の法線方向に進む距離
				double slide_detector_normal = (1 - Math.Cos(distance * IP.SpericalRadiusInverse)) / IP.SpericalRadiusInverse;
				//(0,0,FD)から(X,Y,Z)のベクトルにcoeff_detector_palalleをかけた後、detector_normalの方向にslide_detector_normalだけ進める。
				realX = realX * coeff_detector_palallel + detector_normal.X * slide_detector_normal;
				realY = realY * coeff_detector_palallel + detector_normal.Y * slide_detector_normal;
				realZ = (realZ - fd) * coeff_detector_palallel + fd + detector_normal.Z * slide_detector_normal;
			}
			return (realX, realY, realZ);
		}

		#endregion

		#region CircumferentialBlur 円周方向ににじませた画像を作成
		/// <summary>
		/// 円周方向ににじませた画像を作成(完全に歪んでいない正方形ピクセルを仮定)
		/// </summary>
		/// <param name="angle"></param>
		public static void CircumferentialBlur(double theta)
		{
			double[] pixels = new double[Intensity.Count];
			for (int i = 0; i < pixels.Length; i++)
				pixels[i] = 0;

			double sin3theta = Math.Sin(3 * theta);

			int baseIndex = 0;
			PointD[] pos = new PointD[pixels.Length];
			double[] r = new double[pixels.Length];
			double[] phi = new double[pixels.Length];
			for (int j = 0; j < IP.SrcHeight; j++)
				for (int i = 0; i < IP.SrcWidth; i++)
				{
					pos[baseIndex] = new PointD(i - IP.CenterX, j - IP.CenterY);
					r[baseIndex] = pos[baseIndex].Length;
					phi[baseIndex] = Math.Atan2(pos[baseIndex].Y, pos[baseIndex].X);
					baseIndex++;
				}

			baseIndex = 0;
			for (int j = 0; j < IP.SrcHeight; j++)
			{
				for (int i = 0; i < IP.SrcWidth; i++)
				{
					int rMax = (int)Math.Ceiling(r[baseIndex] * sin3theta);

					List<double> blurRatio = new List<double>();
					List<int> blurIndex = new List<int>();
					for (int y = Math.Max(j - rMax, 0); y <= Math.Min(j + rMax, SrcImgSize.Height - 1); y++)
						for (int x = Math.Max(i - rMax, 0); x <= Math.Min(i + rMax, SrcImgSize.Width - 1); x++)
						{
							int index = y * SrcImgSize.Width + x;
							if ((pos[baseIndex] - pos[index]).Length2 < r[baseIndex] * sin3theta * r[baseIndex] * sin3theta)
							{
								double rDev = Math.Abs(r[index] - r[baseIndex]);
								if (rDev / 0.5 < 3)
								{
									double phiDev = Math.Abs(phi[index] - phi[baseIndex]);
									if (phiDev > theta * 4)
										phiDev = Math.Abs(phiDev - Math.PI * 2);
									if (phiDev / theta < 3)
									{
										blurRatio.Add(Math.Exp(-phiDev / theta * phiDev / theta - rDev * rDev / 0.25));
										blurIndex.Add(index);
									}
								}
							}
						}
					double[] blur = Statistics.Normarize(blurRatio.ToArray());
					for (int k = 0; k < blur.Length; k++)
						pixels[baseIndex] += Intensity[blurIndex[k]] * blur[k];
					baseIndex++;
				}
			}

			for (int i = 0; i < pixels.Length; i++)
				Intensity[i] = pixels[i];
		}

		#endregion

		#region GetCorrectedImageArray 傾き補正やピクセル補正を除去して正確なイメージを作り出す
		/// <summary>
		/// 傾き補正やピクセル補正を除去して正確なイメージを作り出すメソッド
		/// </summary>
		/// <param name="iP"></param>
		/// <param name="newPixelSize"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static double[] GetCorrectedImageArray(IntegralProperty iP, double resolution, Size size, PointD center)
		{
			double[] pixels = new double[size.Height * size.Width];
			for (int i = 0; i < pixels.Length; i++)
				pixels[i] = 0;

			IP = iP;
			SetTiltParameter();

			PointD[] pixelVertex = new PointD[4];

			for (int j = 0; j < iP.SrcHeight; j++)
			{
				double tempY2Upper = (j - IP.CenterY - 0.5) * IP.PixSizeY;
				double tempY2TanKsiUpper = tempY2Upper * TanKsi;
				double tempY2numer1Upper = tempY2Upper * Numer1;
				double tempY2numer3Upper = tempY2Upper * Numer3;
				double tempY2denom1plusFDUpper = tempY2Upper * Denom1 + IP.FilmDistance;

				double tempY2Lower = (j - IP.CenterY + 0.5) * IP.PixSizeY;
				double tempY2TanKsiLower = tempY2Lower * TanKsi;
				double tempY2numer1Lower = tempY2Lower * Numer1;
				double tempY2numer3Lower = tempY2Lower * Numer3;
				double tempY2denom1plusFDLower = tempY2Lower * Denom1 + IP.FilmDistance;

				double tempX = 0, numer4 = 0;

				for (int i = 0; i < iP.SrcWidth; i++)
				//if (IsValid[j * IP.SrcWidth + i])
				{
					//4隅の点を計算
					if (i == 0)
					{
						tempX = (i - IP.CenterX - 0.5) * IP.PixSizeX + tempY2TanKsiUpper;
						numer4 = IP.FilmDistance / (tempY2denom1plusFDUpper + tempX * Denom2);
						pixelVertex[0] = new PointD((tempY2numer1Upper + tempX * Numer2) * numer4, (tempX * Numer1 + tempY2numer3Upper) * numer4);

						tempX = (i - IP.CenterX - 0.5) * IP.PixSizeX + tempY2TanKsiLower;
						numer4 = IP.FilmDistance / (tempY2denom1plusFDLower + tempX * Denom2);
						pixelVertex[3] = new PointD((tempY2numer1Lower + tempX * Numer2) * numer4, (tempX * Numer1 + tempY2numer3Lower) * numer4);
					}
					else
					{
						pixelVertex[0] = pixelVertex[1];
						pixelVertex[3] = pixelVertex[2];
					}
					tempX = (i - IP.CenterX + 0.5) * IP.PixSizeX + tempY2TanKsiUpper;
					numer4 = IP.FilmDistance / (tempY2denom1plusFDUpper + tempX * Denom2);
					pixelVertex[1] = new PointD((tempY2numer1Upper + tempX * Numer2) * numer4, (tempX * Numer1 + tempY2numer3Upper) * numer4);

					tempX = (i - IP.CenterX + 0.5) * IP.PixSizeX + tempY2TanKsiLower;
					numer4 = IP.FilmDistance / (tempY2denom1plusFDLower + tempX * Denom2);
					pixelVertex[2] = new PointD((tempY2numer1Lower + tempX * Numer2) * numer4, (tempX * Numer1 + tempY2numer3Lower) * numer4);

					int maxX = int.MinValue, minX = int.MaxValue, maxY = int.MinValue, minY = int.MaxValue;
					foreach (PointD pt in pixelVertex)
					{
						//求めた4隅の点から、所属する可能性のあるX,Yピクセルの上限、下限を決める
						int pixX = (int)Math.Floor(pt.X / resolution + center.X + 0.5);
						int pixY = (int)Math.Floor(pt.Y / resolution + center.Y + 0.5);
						minX = Math.Min(minX, pixX);
						maxX = Math.Max(maxX, pixX);
						minY = Math.Min(minY, pixY);
						maxY = Math.Max(maxY, pixY);
					}

					if (minX < size.Width && maxX >= 0 && minY < size.Height && maxY >= 0)
					{
						double totalArea = Geometriy.GetPolygonalArea(pixelVertex);
						double intensity = Intensity[j * IP.SrcWidth + i];

						for (int pixX = Math.Max(0, minX); pixX <= Math.Min(size.Width - 1, maxX); pixX++)
							for (int pixY = Math.Max(0, minY); pixY <= Math.Min(size.Height - 1, maxY); pixY++)
							{
								PointD[] tempPixelVertex;
								tempPixelVertex = Geometriy.GetPolygonDividedByLine(pixelVertex, 1, 0, (pixX - center.X - 0.5) * resolution);
								tempPixelVertex = Geometriy.GetPolygonDividedByLine(tempPixelVertex, -1, 0, -(pixX - center.X + 0.5) * resolution);
								tempPixelVertex = Geometriy.GetPolygonDividedByLine(tempPixelVertex, 0, 1, (pixY - center.Y - 0.5) * resolution);
								tempPixelVertex = Geometriy.GetPolygonDividedByLine(tempPixelVertex, 0, -1, -(pixY - center.Y + 0.5) * resolution);

								//面積比を求めて強度を割り振り
								double currentArea = Geometriy.GetPolygonalArea(tempPixelVertex);
								double ratio = currentArea / totalArea;
								pixels[pixY * size.Width + pixX] += ratio * intensity;
							}
					}
				}
			}

			return pixels;
		}
        #endregion


		/// <summary>
		/// 傾き補正やピクセル補正を除去して切り開き画像を作り出すメソッド
		/// </summary>
		/// <param name="iP"></param>
		/// <param name="sweepDivision"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static double[] GetUnrolledImageArray(IntegralProperty iP, int chiDivision, double startTheta, double endTheta, double stepTheta)
		{
			iP.StartAngle = startTheta;
			iP.EndAngle = endTheta;
			iP.StepAngle = stepTheta;
			IP = iP;
			//傾き補正用パラメータを計算
			SetTiltParameter();
			R2 = new double[(int)((endTheta - startTheta) / stepTheta) + 1];
			for (int i = 0; i < R2.Length; i++)
				R2[i] = (i + 0.5) * stepTheta + startTheta;
			int height = chiDivision;
			int width = R2.Length;
			int thread = Environment.ProcessorCount;
			int yStep = IP.SrcHeight / thread + 1;

			var profiles = new double[height * width];
			var pixels = new double[height * width];

			Parallel.For(0, thread, i =>
			{
				(double[][] Profile, double[][] Pixels) = GetProfileThreadWithTiltCorrectionNew(0, iP.SrcWidth, i * yStep, Math.Min((i + 1) * yStep, iP.SrcHeight), chiDivision);
				lock (lockObj)
					for (int h = 0; h < height; h++)
						for (int w = 0; w < width; w++)
							if (Pixels[h][w] > 0)
							{
								pixels[h * width + w] += Pixels[h][w];
								profiles[h * width + w] += Profile[h][w];
							}
			});
			double[] destPixels = new double[height * width];
			for (int i = 0; i < destPixels.Length; i++)
				if (pixels[i] > 0)
					destPixels[i] = profiles[i] / pixels[i];

			return destPixels;
		}

		#region UnrolledImageArray ガンドルフィ
		private static double[] GetUnrolledImageArrayGandlofi(IntegralProperty iP, int yMin, int yMax, double sectorStep, double startTheta, double endTheta, double stepTheta)
		{
			int unrolledImageHeight = (int)(2 * Math.PI / sectorStep) * 2;
			int unrolledImageWidth = (int)((endTheta - startTheta) / stepTheta);
			double[] pixels = new double[unrolledImageWidth * unrolledImageHeight];
			/*int width = iP.SrcWidth;
			double centerX = IP.CenterX, centerY = IP.CenterY;
			double pixSizeX = IP.PixSizeX, pixSizeY = IP.PixSizeY;
			double r = IP.GandolfiRadius;
			double CL = IP.FilmDistance;
			Matrix3D m1 = new Matrix3D(CosTau, SinTau * SinPhi, SinTau * CosPhi, 0, CosPhi, -SinPhi, -SinTau, CosTau * SinPhi, CosTau * CosPhi);
			//ピクセル座標を実空間座標に変換するFunc
			Func<double, double, Vector3DBase> convPixelToReal;
			if (SinTau == 0 && SinPhi == 0)
				convPixelToReal = new Func<double, double, Vector3DBase>((x, y) => new Vector3DBase(
							 Math.Sin((x - centerX) * pixSizeX / r) * r,
							 pixSizeY * (-y + centerY),
							 (Math.Cos((x - centerX) * pixSizeX / r) - 1) * r));
			else
				convPixelToReal = new Func<double, double, Vector3DBase>((x, y) => m1 * new Vector3DBase(
								 Math.Sin((x - centerX) * pixSizeX / r) * r,
								 pixSizeY * (-y + centerY),
								 (Math.Cos((x - centerX) * pixSizeX / r) - 1) * r));

			//桁落ち回避のための係数
			double c = 1000;

			//実空間座標を、極座標系 (すなわち、2θとχ)に変換
			var Vector3DToPolar = new Func<Vector3DBase, PointD>((v) => new PointD(Math.Acos((v.Z + CL) / Math.Sqrt(v.X * v.X + v.Y * v.Y + (v.Z + CL) * (v.Z + CL))) * c, Math.Atan2(v.Y, v.X) + Math.PI * 2));

			//ピクセル座標=>実空間の変換と、実空間＝＞2Θの変換を先にやっておく
			var realVertex = new Vector3DBase[(width + 1) * (yMax - yMin + 1)];
			for (int y = yMin; y < yMax + 1; y++)
				for (int x = 0; x < width + 1; x++)
					realVertex[x + (y - yMin) * (width + 1)] = convPixelToReal(x - 0.5, y - 0.5);

			
			for (int i = 0; i < pixels.Length; i++)
				pixels[i] = 0;

			var pixelVertexPolar = new PointD[4];//極座標

			for (int y = yMin; y < yMax; y++)
			{
				for (int x = 0; x < IP.SrcWidth; x++)
				{
					//四隅の座標を求める
					var pixelVertex = new[] {
							realVertex[x - xMin + (y - yMin) * (width + 1)],

							realVertex[x - xMin + 1 + (y - yMin) * (width + 1)],
						realVertex[x - xMin + 1 + (y - yMin + 1) * (width + 1)],
							realVertex[x - xMin + (y - yMin + 1) * (width + 1)]
						};
					for (int i = 0; i < 4; i++)
						pixelVertexPolar[i] = Vector3DToPolar(pixelVertex[i]);//極座標に変換

					double max = pixelVertexPolar.Max(p => p.Y), min = pixelVertexPolar.Min(p => p.Y);

					if (max - min > Math.PI)
						for (int i = 0; i < 4; i++)
							if (max - pixelVertexPolar[i].Y > Math.PI)
								pixelVertexPolar[i].Y += Math.PI * 2;

					int maxX = int.MinValue, minX = int.MaxValue, maxY = int.MinValue, minY = int.MaxValue;
					foreach (PointD pt in pixelVertexPolar) //求めた4隅の点から、所属する可能性のあるt,rピクセルの上限、下限を決める
					{
						//ここでpixYに含まれる範囲は (pixY-0.5)*sweepStep から(pixY+0.5)*sweepStepまで
						int pixY = (int)Math.Floor(pt.Y / sectorStep + 0.5);
						minY = Math.Min(minY, pixY - 0);
						maxY = Math.Max(maxY, pixY + 0);

						//ここでpixXに含まれる範囲は (pixX-0.5)*thetaStep から(pixX+0.5)*thetaStepまで
						int pixX = (int)Math.Floor((pt.X / c - startTheta) / stepTheta + 0.5);
						minX = Math.Min(minX, pixX - 0);
						maxX = Math.Max(maxX, pixX + 0);
					}

					if (minX >= 0 && maxX < unrolledImageWidth && minY >= 0 && maxY < unrolledImageHeight)
					{
						double intensity = Intensity[x + y * width];

						double totalArea = Geometriy.GetPolygonalArea(pixelVertexPolar);

						if (minX == maxX && minY == maxY)//すっぽり収まった場合は、
							pixels[minY * unrolledImageWidth + minX] += intensity;
						else
						{
							for (int pixY = Math.Max(minY, 0); pixY <= Math.Min(maxY, unrolledImageHeight - 1); pixY++)
							{
								PointD[] pixelVertexPolarTemp1;
								if (maxY == minY)
									pixelVertexPolarTemp1 = pixelVertexPolar;
								else
								{
									//まずpixY(すなわちχ角)で定義される放射状の2本の直線の定義
									pixelVertexPolarTemp1 = Geometriy.GetPolygonDividedByLine(pixelVertexPolar, 0, 1, (pixY - 0.5) * sectorStep);
									pixelVertexPolarTemp1 = Geometriy.GetPolygonDividedByLine(pixelVertexPolarTemp1, 0, -1, -(pixY + 0.5) * sectorStep);
								}

								if (pixelVertexPolarTemp1.Length > 2)
								{
									if (maxX == minX)
										pixels[pixY * unrolledImageWidth + maxX] += Geometriy.GetPolygonalArea(pixelVertexPolarTemp1) / totalArea * intensity;
									else
									{
										//さらに2θ範囲の上限、下限を表す同心円状の2直線の定義

										PointD[] pixelVertexPolarTemp2;
										for (int pixX = Math.Max(0, minX); pixX <= Math.Min(unrolledImageWidth - 1, maxX); pixX++)
										{
											double maxR = ((pixX + 0.5) * stepTheta + startTheta) * c;
											double minR = ((pixX - 0.5) * stepTheta + startTheta) * c;
											pixelVertexPolarTemp2 = Geometriy.GetPolygonDividedByLine(pixelVertexPolarTemp1, 1, 0, minR);
											pixelVertexPolarTemp2 = Geometriy.GetPolygonDividedByLine(pixelVertexPolarTemp2, -1, 0, -maxR);

											//面積比を求めて強度を割り振り
											pixels[pixY * unrolledImageWidth + pixX] += Geometriy.GetPolygonalArea(pixelVertexPolarTemp2) / totalArea * intensity;
										}
									}
								}
							}
						}
					}
				}
			}*/
			return pixels;
		}
		#endregion

		#region お蔵入り GetUnrolledImageArrayThread
		/// <summary>
		/// 傾き補正やピクセル補正を除去して切り開き画像を作り出すメソッドのスレッド
		/// </summary>
		/// <param name="iP"></param>
		/// <param name="sweepDivision"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		private static double[] GetUnrolledImageArrayThread(IntegralProperty iP, int width, int height, int startH, int endH, double sectorStep, double startTheta, double endTheta, double stepTheta)
		{
			double[] pixels = new double[height * width];
		/*	for (int i = 0; i < pixels.Length; i++)
				pixels[i] = 0;

			PointD[] pixelVertex = new PointD[4];
			PointD[] pixelVertexPolar = new PointD[4];

			for (int j = startH; j < endH; j++)
			{
				double tempY2Upper = (j - IP.CenterY - 0.5) * IP.PixSizeY;
				double tempY2TanKsiUpper = tempY2Upper * TanKsi;
				double tempY2numer1Upper = tempY2Upper * Numer1;
				double tempY2numer3Upper = tempY2Upper * Numer3;
				double tempY2denom1plusFDUpper = tempY2Upper * Denom1 + IP.FilmDistance;

				double tempY2Lower = (j - IP.CenterY + 0.5) * IP.PixSizeY;
				double tempY2TanKsiLower = tempY2Lower * TanKsi;
				double tempY2numer1Lower = tempY2Lower * Numer1;
				double tempY2numer3Lower = tempY2Lower * Numer3;
				double tempY2denom1plusFDLower = tempY2Lower * Denom1 + IP.FilmDistance;

				double tempX = 0, numer4 = 0;
				double x, y, l2, sphericalCorrection;
				for (int i = 0; i < IP.SrcWidth; i++)
				{
					//4隅の点を計算
					if (i == 0)
					{
						tempX = (i - IP.CenterX - 0.5) * IP.PixSizeX + tempY2TanKsiUpper;
						numer4 = IP.FilmDistance / (tempY2denom1plusFDUpper + tempX * Denom2);
						x = (tempY2numer1Upper + tempX * Numer2) * numer4;
						y = (tempX * Numer1 + tempY2numer3Upper) * numer4;
						l2 = x * x + y * y;
						sphericalCorrection = IP.FilmDistance / (IP.FilmDistance - l2 * IP.SpericalRadiusInverse / 2.0);

						pixelVertex[0] = RotateFlip(new PointD(x * sphericalCorrection, y * sphericalCorrection));

						pixelVertexPolar[0] = RectangularToPolarCordinate(pixelVertex[0]);

						tempX = (i - IP.CenterX - 0.5) * IP.PixSizeX + tempY2TanKsiLower;
						numer4 = IP.FilmDistance / (tempY2denom1plusFDLower + tempX * Denom2);
						x = (tempY2numer1Lower + tempX * Numer2) * numer4;
						y = (tempX * Numer1 + tempY2numer3Lower) * numer4;
						l2 = x * x + y * y;
						pixelVertex[3] = RotateFlip(new PointD(x * sphericalCorrection, y * sphericalCorrection));
						pixelVertexPolar[3] = RectangularToPolarCordinate(pixelVertex[3]);
					}
					else
					{
						pixelVertex[0] = pixelVertex[1];
						pixelVertex[3] = pixelVertex[2];
						pixelVertexPolar[0] = pixelVertexPolar[1];
						pixelVertexPolar[3] = pixelVertexPolar[2];
					}

					tempX = (i - IP.CenterX + 0.5) * IP.PixSizeX + tempY2TanKsiUpper;
					numer4 = IP.FilmDistance / (tempY2denom1plusFDUpper + tempX * Denom2);
					x = (tempY2numer1Upper + tempX * Numer2) * numer4;
					y = (tempX * Numer1 + tempY2numer3Upper) * numer4;
					l2 = x * x + y * y;
					sphericalCorrection = IP.FilmDistance / (IP.FilmDistance - l2 * IP.SpericalRadiusInverse / 2.0);
					pixelVertex[1] = RotateFlip(new PointD(x * sphericalCorrection, y * sphericalCorrection));
					pixelVertexPolar[1] = RectangularToPolarCordinate(pixelVertex[1]);

					tempX = (i - IP.CenterX + 0.5) * IP.PixSizeX + tempY2TanKsiLower;
					numer4 = IP.FilmDistance / (tempY2denom1plusFDLower + tempX * Denom2);
					x = (tempY2numer1Lower + tempX * Numer2) * numer4;
					y = (tempX * Numer1 + tempY2numer3Lower) * numer4;
					l2 = x * x + y * y;
					sphericalCorrection = IP.FilmDistance / (IP.FilmDistance - l2 * IP.SpericalRadiusInverse / 2.0);
					pixelVertex[2] = RotateFlip(new PointD(x * sphericalCorrection, y * sphericalCorrection));
					pixelVertexPolar[2] = RectangularToPolarCordinate(pixelVertex[2]);

					double max = Math.Max(Math.Max(pixelVertexPolar[0].X, pixelVertexPolar[1].X), Math.Max(pixelVertexPolar[2].X, pixelVertexPolar[3].X));
					double min = Math.Min(Math.Min(pixelVertexPolar[0].X, pixelVertexPolar[1].X), Math.Min(pixelVertexPolar[2].X, pixelVertexPolar[3].X));
					if (max - min > Math.PI)
						for (int n = 0; n < 4; n++)
							if (max - pixelVertexPolar[n].X > Math.PI)
								pixelVertexPolar[n].X += Math.PI * 2;

					int maxX = int.MinValue, minX = int.MaxValue, maxY = int.MinValue, minY = int.MaxValue;
					foreach (PointD pt in pixelVertexPolar) //求めた4隅の点から、所属する可能性のあるt,rピクセルの上限、下限を決める
					{
						//ここでpixYに含まれる範囲は (pixY-0.5)*sweepStep から(pixY+0.5)*sweepStepまで
						int pixY = (int)Math.Floor(pt.X / sectorStep + 0.5);
						minY = Math.Min(minY, pixY);
						maxY = Math.Max(maxY, pixY);

						//ここでpixXに含まれる範囲は (pixX-0.5)*thetaStep から(pixX+0.5)*thetaStepまで
						int pixX = (int)Math.Floor((Math.Atan(pt.Y / iP.FilmDistance) - startTheta) / stepTheta + 0.5);
						minX = Math.Min(minX, pixX);
						maxX = Math.Max(maxX, pixX);
					}

					if (minX >= 0 && maxX < width && minY >= 0 && maxY < height)
					{
						double intensity = Intensity[j * IP.SrcWidth + i];
						if (minX == maxX && minY == maxY)
							pixels[minY * width + maxX] += intensity;
						else
						{
							double totalArea = Geometriy.GetPolygonalArea(pixelVertex);
							for (int pixY = Math.Max(minY, 0); pixY <= Math.Min(maxY, height - 1); pixY++)
							{
								double a1, b1, a2, b2;
								PointD[] pixelVertex1;
								if (maxY == minY)
									pixelVertex1 = pixelVertex;
								else
								{
									//まずpixXで定義される放射状の2本の直線の定義
									a1 = -Math.Sin((pixY - 0.5) * sectorStep);
									b1 = Math.Cos((pixY - 0.5) * sectorStep);
									a2 = Math.Sin((pixY + 0.5) * sectorStep);
									b2 = -Math.Cos((pixY + 0.5) * sectorStep);
									pixelVertex1 = Geometriy.GetPolygonDividedByLine(pixelVertex, a1, b1, 0);
									pixelVertex1 = Geometriy.GetPolygonDividedByLine(pixelVertex1, a2, b2, 0);
								}

								if (pixelVertex1.Length > 2)
								{
									if (maxX == minX)
										pixels[pixY * width + maxX] += Geometriy.GetPolygonalArea(pixelVertex1) / totalArea * intensity;
									else
									{
										//さらに2θ範囲の上限、下限を表す同心円状の2直線の定義
										a1 = Math.Cos(pixY * sectorStep);
										b1 = Math.Sin(pixY * sectorStep);

										PointD[] pixelVertex2;
										double beforeArea = 0;
										for (int pixX = Math.Max(0, minX); pixX <= Math.Min(width - 1, maxX); pixX++)
										{
											double maxR = Math.Tan((pixX + 0.5) * stepTheta + startTheta) * IP.FilmDistance;
											pixelVertex2 = Geometriy.GetPolygonDividedByLine(pixelVertex1, -a1, -b1, -maxR);

											//面積比を求めて強度を割り振り
											double currentArea = Geometriy.GetPolygonalArea(pixelVertex2);
											double ratio = (currentArea - beforeArea) / totalArea;
											pixels[pixY * width + pixX] += ratio * intensity;
											beforeArea = currentArea;
										}
									}
								}
							}
						}
					}
				}
			}
		*/
			return pixels;
		}

        #endregion

        public static Profile GetProfile(IntegralProperty iP)
		{
			if (iP.ConcentricMode)
				return GetConcenrticProfile(iP);
			else
				return GetRadialProfile(iP);
		}

        #region GetRadialProfile
        private static Profile GetRadialProfile(IntegralProperty iP)
		{
			IP = iP;
			double minR, maxR;
			if (IP.Mode == HorizontalAxis.Angle)
			{
				minR = Math.Tan(IP.RadialRadiusAngle - IP.RadialRadiusAngleRange) * IP.FilmDistance;
				maxR = Math.Tan(IP.RadialRadiusAngle + IP.RadialRadiusAngleRange) * IP.FilmDistance;
			}
			else
			{
				minR = IP.FilmDistance * Math.Tan(2 * Math.Asin(IP.WaveLength / 2.0 / (IP.RadialRadiusDspacing + IP.RadialRadiusDspacingRange)));
				maxR = IP.FilmDistance * Math.Tan(2 * Math.Asin(IP.WaveLength / 2.0 / (IP.RadialRadiusDspacing - IP.RadialRadiusDspacingRange)));
			}

			double step = iP.RadialSectorAngle / 180.0 * Math.PI;
			//Profile(各ステップごとの強度)とPixels(各ステップに寄与したピクセル数)を作成
			int length = (int)(360.0 / iP.RadialSectorAngle + 0.5);
			//のりしろ部分を作るために2倍の配列を確保
			double[] tempProfileIntensity = new double[length * 2];
			//このとき、tempProfileIntensity[i]は (i-0.5)*stepから(i+0.5)*stepの角度範囲内の強度を意味する
			for (int i = 0; i < tempProfileIntensity.Length; i++)
				tempProfileIntensity[i] = 0;

			for (int j = 0; j < IP.SrcHeight; j++)
			{
				for (int i = 0; i < IP.SrcWidth; i++)
					if (IsValid[j * IP.SrcWidth + i])
					{
						double intensity = Intensity[j * IP.SrcWidth + i];

						//4隅の点を計算
						PointD[] pixelVertex = new PointD[4];
						int n = 0;
						for (double yshift = -0.5; yshift < 0.6; yshift++)
						{
							double tempY2 = (j - IP.CenterY + yshift) * IP.PixSizeY;
							double tempY2TanKsi = tempY2 * TanKsi;
							double tempY2numer1 = tempY2 * Numer1;
							double tempY2numer3 = tempY2 * Numer3;
							double tempY2denom1plusFD = tempY2 * Denom1 + IP.FilmDistance;
							for (double xshift = -0.5; xshift < 0.6; xshift++)
							{
								double tempX = (i - IP.CenterX + xshift) * IP.PixSizeX + tempY2TanKsi;
								double numer4 = IP.FilmDistance / (tempY2denom1plusFD + tempX * Denom2);
								pixelVertex[n++] = new PointD((tempY2numer1 + tempX * Numer2) * numer4, (tempX * Numer1 + tempY2numer3) * numer4);
							}
						}
                        (pixelVertex[3], pixelVertex[2]) = (pixelVertex[2], pixelVertex[3]);
                        double totalArea = Geometriy.GetPolygonalArea(pixelVertex);

						//求めた4隅の点から、ステップの指数の上限、下限を設定
						List<double> angles = new List<double>();
						foreach (PointD pt in pixelVertex)
							angles.Add(Math.Atan2(pt.Y, pt.X));
						angles.Sort();
						if (angles[3] - angles[0] > Math.PI * 1.5)
						{
							for (int k = 0; k < angles.Count; k++)
								if (angles[k] < 0)
									angles[k] += Math.PI * 2;
							angles.Sort();
						}

						int startIndex = (int)((angles[0] + Math.PI * 2) / step - 1);
						int endIndex = (int)((angles[3] + Math.PI * 2) / step + 2);
						for (int index = startIndex; index <= endIndex; index++)
						{
							PointD[] tempPixelVertex;
							//まずindexで定義される放射状の2本の直線の定義
							double a1 = -Math.Sin((index - 0.5) * step), b1 = Math.Cos((index - 0.5) * step);
							double a2 = Math.Sin((index + 0.5) * step), b2 = -Math.Cos((index + 0.5) * step);
							tempPixelVertex = Geometriy.GetPolygonDividedByLine(pixelVertex, a1, b1, 0);
							tempPixelVertex = Geometriy.GetPolygonDividedByLine(tempPixelVertex, a2, b2, 0);

							//さらに角度範囲の上限、下限を表す同心円状の2直線
							a1 = Math.Cos(index * step);
							b1 = Math.Sin(index * step);
							tempPixelVertex = Geometriy.GetPolygonDividedByLine(tempPixelVertex, a1, b1, minR);
							tempPixelVertex = Geometriy.GetPolygonDividedByLine(tempPixelVertex, -a1, -b1, -maxR);

							//面積比を求めて強度を割り振り
							double currentArea = Geometriy.GetPolygonalArea(tempPixelVertex);
							double ratio = currentArea / totalArea;
							tempProfileIntensity[index] += ratio * intensity;
						}
					}
			}

			var profile = new Profile();
			for (int i = 0; i < length; i++)
				profile.Pt.Add(new PointD(i * step / Math.PI * 180, tempProfileIntensity[i] + tempProfileIntensity[i + length]));

			double shift = 0;
			if (ChiRotation == Rotation.Counterclockwise)
				for (int i = 0; i < length / 2; i++)
				{
					double temp = profile.Pt[i].X;
					//profile.Pt[i].X = profile.Pt[length - i - 1].X;
					profile.Pt[i] = new PointD(profile.Pt[length - i - 1].X, profile.Pt[i].Y);
					//profile.Pt[length - i - 1].X = temp;
					profile.Pt[length - i - 1] = new PointD(temp, profile.Pt[length - i - 1].Y);
				}

			if (ChiRotation == Rotation.Clockwise)
			{
				if (ChiDirection == Direction.Bottom) shift = -90;
				else if (ChiDirection == Direction.Left) shift = -180;
				else if (ChiDirection == Direction.Top) shift = -270;
			}
			else
			{
				if (ChiDirection == Direction.Bottom) shift = -270;
				else if (ChiDirection == Direction.Left) shift = -180;
				else if (ChiDirection == Direction.Top) shift = -90;
			}

			for (int i = 0; i < length; i++)
			{
				//profile.Pt[i].X += shift;
				profile.Pt[i] += new PointD(shift, 0);

				if (profile.Pt[i].X < 0)
					//profile.Pt[i].X += 360;
					profile.Pt[i] += new PointD(360, 0);
			}
			profile.Sort();

			return profile;
		}
        #endregion

        /// <summary>
        /// 2theta-intensity histogram
        /// </summary>
        /// <param name="iP"></param>
        /// <returns></returns>
        private static Profile GetConcenrticProfile(IntegralProperty iP)
		{
			var thread = Environment.ProcessorCount;


			IP = iP;

			//積分領域全体のx上限,x下限, y上限、y下限を決める
			int xMin = int.MaxValue, yMin = int.MaxValue, xMax = int.MinValue, yMax = int.MinValue;
			for (int i = 0; i < IsValid.Count; i++)
				if (IsValid[i])
				{
					xMin = Math.Min(i % IP.SrcWidth, xMin);
					yMin = Math.Min(i / IP.SrcWidth, yMin);
					xMax = Math.Max(i % IP.SrcWidth, xMax);
					yMax = Math.Max(i / IP.SrcWidth, yMax);
				}

			thread = Environment.ProcessorCount;
#if (DEBUG)
			thread = 1;
#endif

			//各スレッドの上限と下限を決める
			int[] yThreadMin = new int[thread];
			int[] yThreadMax = new int[thread];
			int yStep = (yMax - yMin) / thread;
			for (int i = 0; i < thread; i++)
			{
				yThreadMin[i] = yMin + i * yStep;
				yThreadMax[i] = Math.Min(yMin + (i + 1) * yStep, yMax);
			}

			//フラットパネルモードの時
			if (IP.Camera == IntegralProperty.CameraEnum.FlatPanel)
			{
				//角度、距離、d値モードごとに、ステップの区切り位置(中心からの距離の2乗)を設定する
				if (IP.Mode == HorizontalAxis.Angle)
				{
					R2 = new double[(int)((IP.EndAngle - IP.StartAngle) / IP.StepAngle) + 1];
					for (int i = 0; i < R2.Length; i++)
						R2[i] = (i + 0.5) * IP.StepAngle + IP.StartAngle;//2016/12/27 変更
				}
				else if (IP.Mode == HorizontalAxis.d)//d-spacing modeのとき
				{
					R2 = new double[(int)((IP.EndDspacing - IP.StartDspacing) / IP.StepDspacing) + 1];
					for (int i = R2.Length - 1; i >= 0; i--)
						R2[R2.Length - 1 - i] = 2 * Math.Asin(IP.WaveLength / 2 / (IP.StartDspacing + (i + 0.5) * IP.StepDspacing));
				}
				else if (IP.Mode == HorizontalAxis.Length)
				{
					R2 = new double[(int)((IP.EndLength - IP.StartLength) / IP.StepLength) + 1];
					for (int i = 0; i < R2.Length; i++)
					{
						double temp = IP.StartLength + (i + 0.5) * IP.StepLength;
						R2[i] = temp * temp;
					}
				}
			}
			else//ガンドルフィーモードの時
			{
				//角度ステップの区切り位置を設定する
				R2 = new double[(int)((IP.EndAngle - IP.StartAngle) / IP.StepAngle) + 1];
				for (int i = 0; i < R2.Length; i++)
					R2[i] = (i - 0.5) * IP.StepAngle + IP.StartAngle;
			}
			int length = R2.Length;

			//Profile(各ステップごとの強度)とPixels(各ステップに寄与したピクセル数)を作成
			var tempProfileIntensity = new double[thread][][];
			var tempContibutedPixels = new double[thread][][];
			for (int i = 0; i < thread; i++)
			{
				tempProfileIntensity[i] = new double[1][];
				tempContibutedPixels[i] = new double[1][];
				tempProfileIntensity[i][0] = new double[length];
				tempContibutedPixels[i][0] = new double[length];
			}
			
			//傾き補正用パラメータを計算
			SetTiltParameter();

			double[] ProfileIntensity = new double[R2.Length];
			double[] ContributedPixels = new double[R2.Length];


			if(iP.Mode!= HorizontalAxis.Angle)
            {
				//計算する交点をきめる
				IsCalcPosition = new bool[(IP.SrcHeight + 1) * (IP.SrcWidth + 1)];
				int h = IP.SrcHeight;
				int w = IP.SrcWidth;
				int jw, jw1, j1w1;

				for (int j = yMin; j < yMax; j++)
				{
					jw = j * w;
					jw1 = j * (w + 1);
					j1w1 = (j + 1) * (w + 1);
					for (int i = xMin; i < xMax; i++)
						if (IsValid[jw + i])
							IsCalcPosition[jw1 + i] = IsCalcPosition[jw1 + i + 1] = IsCalcPosition[j1w1 + i] = IsCalcPosition[j1w1 + i + 1] = true;
				}
			}

			//FlatPanelモードの時
			if (iP.Camera == IntegralProperty.CameraEnum.FlatPanel)
			{
				if (IP.Mode == HorizontalAxis.Angle || IP.Mode== HorizontalAxis.d)
				{
					#region ネイティブコードは思ったより早くなかった。
					//if (NativeWrapper.Enabled)
					//{
					//	var intensityArray = Intensity.ToArray();
					//                   var isValidArray = IsValid.Select(val => val ? (byte)1 : (byte)0).ToArray();
					//                   var r2 = R2.ToArray();
					//	Parallel.For(0, ThreadTotal, i =>
					//	{
					//		int yMin = yThreadMin[i], yMax = yThreadMax[i];
					//		var intensity = intensityArray.AsSpan(yMin * IP.SrcWidth, (yMax - yMin) * IP.SrcWidth);
					//		var isValid = isValidArray.AsSpan(yMin * IP.SrcWidth, (yMax - yMin) * IP.SrcWidth);

					//		(tempProfileIntensity[i], tempContibutedPixels[i]) = NativeWrapper.Histogram(
					//		   IP.SrcWidth, IP.SrcHeight,
					//		   IP.CenterX, IP.CenterY,
					//		   IP.PixSizeX, IP.PixSizeY,
					//		   IP.FilmDistance,
					//		   IP.ksi, IP.tau, IP.phi,
					//		   IP.SpericalRadiusInverse,
					//		   intensity.ToArray(), isValid.ToArray(),
					//		   yThreadMin[i], yThreadMax[i],
					//		   IP.StartAngle, IP.StepAngle,
					//		   r2);
					//	});
					//               }
					//else
					#endregion

					Parallel.For(0, thread, i =>
							(tempProfileIntensity[i], tempContibutedPixels[i]) = GetProfileThreadWithTiltCorrectionNew(xMin, xMax, yThreadMin[i], yThreadMax[i]));
				}
				
				else if (IP.Mode == HorizontalAxis.Length)
							Parallel.For(0, thread, i =>
					GetProfileThreadWithTiltCorrection(xMin, xMax, yThreadMin[i], yThreadMax[i], ref tempProfileIntensity[i][0], ref tempContibutedPixels[i][0]));


				for (int i = 0; i < thread; i++)
					for (int j = 0; j < length; j++)
					{
						ProfileIntensity[j] += tempProfileIntensity[i][0][j];
						ContributedPixels[j] += tempContibutedPixels[i][0][j];
					}
			}
			//Gandolfiモードの時
			else
			{
				int[] xThreadMin = new int[thread], xThreadMax = new int[thread];
				//まず、IsValidなピクセル数をカウント
				int average = IsValid.Count(b => b) / thread;
				int y = 0;
				for (int i = 0; i < thread; i++)
				{
					int count = 0;
					for (; y < iP.SrcHeight; y++)
					{
						for (int x = 0; x < IP.SrcWidth; x++)
						{
							if (IsValid[x + y * iP.SrcWidth])
							{
								if (count == 0)
								{
									xThreadMin[i] = xThreadMax[i] = x;
									yThreadMin[i] = yThreadMax[i] = y;
								}
								xThreadMin[i] = Math.Min(xThreadMin[i], x);
								xThreadMax[i] = Math.Max(xThreadMax[i], x);
								yThreadMax[i] = Math.Max(yThreadMax[i], y);
								count++;
							}
						}
						if (i != thread - 1 && count > average)
						{
							y++;
							break;
						}
					}
				}

				Parallel.For(0, thread,i =>
				{
					GetProfileGandlfi(xThreadMin[i], xThreadMax[i] + 1, yThreadMin[i], yThreadMax[i] + 1, ref tempProfileIntensity[i][0], ref tempContibutedPixels[i][0]);
					lock (lockObj)
					{
						for (int j = 0; j < length; j++)
						{
							ProfileIntensity[j] += tempProfileIntensity[i][0][j];
							ContributedPixels[j] += tempContibutedPixels[i][0][j];
						}
					}
				});
			}

			//Profile変数に代入
			Profile profile = new Profile();

			//フラットパネルモードの時
			if (IP.Camera == IntegralProperty.CameraEnum.FlatPanel)
			{
				if (IP.Mode == HorizontalAxis.Angle)//角度モードのとき
					for (int i = 0; i < length; i++)
					{
						//double cosTwoTheta = Math.Cos(i * IP.StepAngle + IP.StartAngle);
						//double temp = ProfileIntensity[i] / ContributedPixels[i] / cosTwoTheta/ cosTwoTheta/ cosTwoTheta; //cos2Θの3乗で割ることによって、BB光学系と一致させる
						//20161227 変更
						var temp = ProfileIntensity[i] / ContributedPixels[i];
						if (double.IsNaN(temp) || double.IsInfinity(temp))
							temp = 0;
						double x = (i * IP.StepAngle + IP.StartAngle) / Math.PI * 180.0;
						profile.Pt.Add(new PointD(x, temp));
						profile.Err.Add(new PointD(x, temp / Math.Sqrt(ProfileIntensity[i])));
					}
				else if(IP.Mode == HorizontalAxis.d)//d値のモードのとき
					for (int i = 0; i < length; i++)
					{
						double tempD = i * IP.StepDspacing + IP.StartDspacing;
						double temp = ProfileIntensity[length - 1 - i] / ContributedPixels[length - 1 - i];
						if (double.IsNaN(temp) || double.IsInfinity(temp))
							temp = 0;
						profile.Pt.Add(new PointD(tempD * 10, temp));
						profile.Err.Add(new PointD(tempD * 10, temp / Math.Sqrt(ProfileIntensity[i])));
					}
				else if (IP.Mode == HorizontalAxis.Length)//Lengthモードのとき
					for (int i = 0; i < length; i++)
					{
						var temp = ProfileIntensity[i] / ContributedPixels[i];
						if (double.IsNaN(temp) || double.IsInfinity(temp))
							temp = 0;
						double x = i * IP.StepLength + IP.StartLength;
						profile.Pt.Add(new PointD(x, temp));
						profile.Err.Add(new PointD(x, temp / Math.Sqrt(ProfileIntensity[i])));
					}
				
			}
			//ガンドルフィーモードの時
			else
			{
				for (int i = 0; i < length; i++)
				{
					double temp = ProfileIntensity[i] / ContributedPixels[i];
					if (double.IsNaN(temp) || double.IsInfinity(temp))
						temp = 0;
					double x = (i * IP.StepAngle + IP.StartAngle) / Math.PI * 180.0;
					profile.Pt.Add(new PointD(x, temp));
					profile.Err.Add(new PointD(x, temp / Math.Sqrt(ProfileIntensity[i])));
				}
			}
			tempProfileIntensity = null;
			tempContibutedPixels = null;
			return profile;
		}

		public static Profile[] GetConcenrticProfilesBySector(IntegralProperty iP, int chiDivision)
		{
			IP = iP;
			var thread = Environment.ProcessorCount;
			int yStep = IP.SrcHeight / thread+1;

			R2 = new double[(int)((IP.EndAngle - IP.StartAngle) / IP.StepAngle) + 1];
			for (int i = 0; i < R2.Length; i++)
				R2[i] = (i + 0.5) * IP.StepAngle + IP.StartAngle;//2016/12/27 変更

			int length = R2.Length;

			//傾き補正用パラメータを計算
			SetTiltParameter();

			var profiles = new double[chiDivision, length];
			var pixels = new double[chiDivision, length];

			Parallel.For(0, thread, i =>
			{
				var (tempProfile, tempPixels) = GetProfileThreadWithTiltCorrectionNew(0, iP.SrcWidth, i * yStep, Math.Min((i + 1) * yStep, iP.SrcHeight), chiDivision);
				lock (lockObj)
					for (int h = 0; h < chiDivision; h++)
						for (int w = 0; w < length; w++)
							if (tempPixels[h][w] > 0)
							{
								pixels[h,w] += tempPixels[h][w];
								profiles[h,w] += tempProfile[h][w];
							}

			});
			var destProfiles = new Profile[chiDivision];
			for(int j=0; j< chiDivision; j++)
            {
				destProfiles[j] = new Profile();
				for (int i = 0; i < length; i++)
				{
					double temp = profiles[j, i] / pixels[j, i];
					if (double.IsNaN(temp) || double.IsInfinity(temp))
						temp = 0;
					double x = (i * IP.StepAngle + IP.StartAngle) / Math.PI * 180.0;
					destProfiles[j].Pt.Add(new PointD(x, temp));
					destProfiles[j].Err.Add(new PointD(x, temp / Math.Sqrt(profiles[j, i])));
				}
			}

			return destProfiles;
		}

		#region GetProfile　Gandlfi用
		/// <summary>
		/// ガンドルフィーカメラ用の計算
		/// </summary>
		/// <param name="xMin"></param>
		/// <param name="xMax"></param>
		/// <param name="yMin"></param>
		/// <param name="yMax"></param>
		/// <param name="profile"></param>
		/// <param name="pixels"></param>
		private static void GetProfileGandlfi(int xMin, int xMax, int yMin, int yMax, ref double[] profile, ref double[] pixels)
		{
			double centerX = IP.CenterX, centerY = IP.CenterY;
			double pixSizeX = IP.PixSizeX, pixSizeY = IP.PixSizeY;
			int width = IP.SrcWidth;
			double r = IP.GandolfiRadius;
			//double phi = IP.phi;
			//double tau = IP.tau;
			double CL = IP.FilmDistance;

			Matrix3D m1 = new Matrix3D(CosTau, SinTau * SinPhi, SinTau * CosPhi, 0, CosPhi, -SinPhi, -SinTau, CosTau * SinPhi, CosTau * CosPhi);

			//ピクセル座標を実空間座標に変換するFunc
			Func<double, double, Vector3DBase> convPixelToReal;
			if (SinTau == 0 && SinPhi == 0)
				convPixelToReal = new Func<double, double, Vector3DBase>((x, y) => new Vector3DBase(
							 Math.Sin((x - centerX) * pixSizeX / r) * r,
							 pixSizeY * (-y + centerY),
							 (Math.Cos((x - centerX) * pixSizeX / r) - 1) * r));
			else
				convPixelToReal = new Func<double, double, Vector3DBase>((x, y) => m1 * new Vector3DBase(
								 Math.Sin((x - centerX) * pixSizeX / r) * r,
								 pixSizeY * (-y + centerY),
								 (Math.Cos((x - centerX) * pixSizeX / r) - 1) * r));
			//実空間座標を2Θに変換するFunc
			var convRealToTwoTheta = new Func<Vector3DBase, double>((v) => Math.Acos((v.Z + CL) / Math.Sqrt(v.X * v.X + v.Y * v.Y + (v.Z + CL) * (v.Z + CL))));

			//角度ステップの区切り位置を設定する
			int length = (int)((IP.EndAngle - IP.StartAngle) / IP.StepAngle) + 1;
			var divisions = new List<double>();
			var tan = new double[length];
			var sec = new double[length];
			for (int i = 0; i < length; i++)
			{
				divisions.Add((i - 0.5) * IP.StepAngle + IP.StartAngle);
				tan[i] = Math.Tan(divisions[i]) * Math.Tan(divisions[i]);
				sec[i] = 1 / Math.Cos(divisions[i]) / Math.Cos(divisions[i]);
			}

			//ピクセル座標=>実空間の変換と、実空間＝＞2Θの変換を先にやっておく
			var realCenter = new Vector3DBase[(xMax - xMin) * (yMax - yMin)];
			var realVertex = new Vector3DBase[(xMax - xMin + 1) * (yMax - yMin + 1)];
			var twoThetaCenter = new double[(xMax - xMin) * (yMax - yMin)];
			var twoThetaVertex = new double[(xMax - xMin + 1) * (yMax - yMin + 1)];
			var isValidVertex = new bool[(xMax - xMin + 1) * (yMax - yMin + 1)];

			for (int y = yMin; y < yMax; y++)
				for (int x = xMin; x < xMax; x++)
					if (IsValid[x + y * width])
					{
						int index = x - xMin + (y - yMin) * (xMax - xMin);
						realCenter[index] = convPixelToReal(x, y);
						twoThetaCenter[index] = convRealToTwoTheta(realCenter[index]);
						isValidVertex[x - xMin + (y - yMin) * (xMax - xMin + 1)] = isValidVertex[x - xMin + 1 + (y - yMin) * (xMax - xMin + 1)] =
						isValidVertex[x - xMin + (y - yMin + 1) * (xMax - xMin + 1)] = isValidVertex[x - xMin + 1 + (y - yMin + 1) * (xMax - xMin + 1)] = true;
					}
			for (int y = yMin; y < yMax + 1; y++)
				for (int x = xMin; x < xMax + 1; x++)
				{
					int index = x - xMin + (y - yMin) * (xMax - xMin + 1);
					if (isValidVertex[index])
					{
						realVertex[index] = convPixelToReal(x - 0.5, y - 0.5);
						twoThetaVertex[index] = convRealToTwoTheta(realVertex[index]);
					}
				}

			for (int y = yMin; y < yMax; y++)
				for (int x = xMin; x < xMax; x++)
				{
					if (IsValid[x + y * width])
					{
						double intensity = Intensity[x + y * width];
						// x, yピクセル座標を実空間に変換

						//四隅の2Θを求める
						var twoTheta = new[] {
							twoThetaVertex[x - xMin + (y - yMin) * (xMax - xMin + 1)],
							twoThetaVertex[x - xMin + 1 + (y - yMin) * (xMax - xMin + 1)],
							twoThetaVertex[x - xMin + 1 + (y - yMin + 1) * (xMax - xMin + 1)],
							twoThetaVertex[x - xMin + (y - yMin + 1) * (xMax - xMin + 1)]
						};
						double maxTwoTheta = twoTheta.Max(), minTwoTheta = twoTheta.Min();

						if (maxTwoTheta > divisions[0] && minTwoTheta < divisions[length - 1])//全区切り内にピクセルが(一部でも)収まっている場合（完全に範囲外の時は除外）
						{
							//最小インデックスと最大インデックスを求める
							int minIndex = Math.Max(0, divisions.FindLastIndex(d => d < minTwoTheta));
							int maxIndex = Math.Min(length - 1, divisions.FindIndex(d => d > maxTwoTheta));

							//最大インデックスと最小インデックスの差が1の場合(ピクセルがすっぽり一つの区切り内に収まるとき)
							if (maxIndex - minIndex == 1 && divisions[minIndex] < minTwoTheta && divisions[maxIndex] > maxTwoTheta)
							{
								profile[minIndex] += intensity;
								pixels[minIndex] += pixSizeX * pixSizeY;
							}
							//最大インデックスと最小インデックスの差が2以上の場合(ピクセルが一つの区切り内に収まらないとき)
							else
							{
								var v = realCenter[x - xMin + (y - yMin) * (xMax - xMin)];
								var v1 = realVertex[x - xMin + (y - yMin) * (xMax - xMin + 1)];
								var v2 = realVertex[x - xMin + 1 + (y - yMin) * (xMax - xMin + 1)];

								var arc = (x - centerX) * pixSizeX;
								double cosAR = Math.Cos(arc / r), sinAR = Math.Sin(arc / r);
								double sinGamma = CosTau * cosAR + SinTau * sinAR, cosGamma = SinTau * cosAR - CosTau * sinAR;

								double CLplusZ = CL + v.Z;
								double A = v.X * CLplusZ, B = v.Y * CLplusZ, C = v.X * v.X + v.Y * v.Y;

								double cosAlpha = -B * CosPhi + C * SinPhi, sinAlpha = (C * CosPhi + B * SinPhi) * cosGamma + A * sinGamma;
								var temp = Math.Sqrt(cosAlpha * cosAlpha + sinAlpha * sinAlpha);
								cosAlpha /= temp;
								sinAlpha /= temp;

								Matrix3D m2 = new Matrix3D(-cosAlpha, sinAlpha, 0, -sinAlpha, -cosAlpha, 0, 0, 0, 1) *
									new Matrix3D(sinGamma, cosGamma * SinPhi, cosGamma * CosPhi, 0, CosPhi, -SinPhi, -cosGamma, sinGamma * SinPhi, sinGamma * CosPhi).Inverse();

								//var p = new PointD[] { (m2 * (v1 - v)).ToPointD(), (m2 * (v2 - v)).ToPointD(), (m2 * (v3 - v)).ToPointD(), (m2 * (v4 - v)).ToPointD() };
								var p1 = (m2 * (v1 - v)).ToPointD;
								var p2 = (m2 * (v2 - v)).ToPointD;
								var p = new PointD[] { p1, p2, -p1, -p2 };

								//var vh = new Vector3DBase(sinGamma, cosGamma * SinPhi, cosGamma * CosPhi);
								//var vv = new Vector3DBase(0, CosPhi, -SinPhi);
								var vq = sinAlpha * new Vector3DBase(sinGamma, cosGamma * SinPhi, cosGamma * CosPhi) - cosAlpha * new Vector3DBase(0, CosPhi, -SinPhi);

								double vqZY2 = vq.X * v.X + vq.Y * v.Y;
								double vqZ2 = vq.Z * vq.Z;
								double totalArea = Geometriy.GetPolygonalArea(p), residualArea = totalArea;
								for (int i = minIndex; i <= maxIndex; i++)
								{
									if (divisions[i] < minTwoTheta)
									{ }
									else if (divisions[i] > maxTwoTheta && i > 0)
									{
										profile[i - 1] += intensity * residualArea / totalArea;
										pixels[i - 1] += residualArea;
									}
									else
									{
										double d1 = vqZY2 - tan[i] * vq.Z * CLplusZ;
										double d3 = sec[i] * vqZ2 - 1;
										double d2 = Math.Sqrt(d1 * d1 + d3 * (C - tan[i] * CLplusZ * CLplusZ));

										var d = Math.Abs((d1 + d2) / d3) < Math.Abs((d1 - d2) / d3) ? (d1 + d2) / d3 : (d1 - d2) / d3;

										p = Geometriy.GetPolygonDividedByLine(p, 0, 1, d);
										var area = Geometriy.GetPolygonalArea(p);
										if (i > 0)
										{
											profile[i - 1] += intensity * (residualArea - area) / totalArea;
											pixels[i - 1] += residualArea - area;
										}
										residualArea = area;
									}
								}
							}
						}
					}
				}
		}
		#endregion

		#region GetProfile 旧バージョン  FindParameterで使う
		/// <summary>
		/// TiltCorrectionの時にのみ呼ばれる 
		/// </summary>
		/// <param name="iP"></param>
		/// <returns></returns>
		public static Profile GetProfileForFindTiltCorrection(IntegralProperty iP)
		{
			var thread = Environment.ProcessorCount;

			int i, j;
			//Profile(各ステップごとの強度)とPixels(各ステップに寄与したピクセル数)を作成
			int length = R2.Length;
			double[] ProfileIntensity = new double[length];
			double[] ContributedPixels = new double[length];

			double[][] tempProfileIntensity = new double[thread][];
			double[][] tempContibutedPixels = new double[thread][];
			for (i = 0; i < thread; i++)
			{
				tempProfileIntensity[i] = new double[length];
				tempContibutedPixels[i] = new double[length];
			}

			//ここからスレッド起動
			Parallel.For(0, thread, i => GetProfileThreadWithTiltCorrection(xMin, xMax, yThreadMin[i], yThreadMax[i], ref tempProfileIntensity[i], ref tempContibutedPixels[i]));

			//各スレッドの結果をまとめる
			for (i = 0; i < R2.Length; i++)
				ContributedPixels[i] = ProfileIntensity[i] = 0;
			for (i = 0; i < thread; i++)
				for (j = 0; j < length; j++)
				{
					ProfileIntensity[j] += tempProfileIntensity[i][j];
					ContributedPixels[j] += tempContibutedPixels[i][j];
				}

			//Profile変数に代入
			Profile p = new Profile();
			double temp;
			for (i = 0; i < length; i++)
			{
				temp = 10 * ProfileIntensity[i] / ContributedPixels[i];
				if (double.IsNaN(temp) || double.IsInfinity(temp))
					temp = 0;
				p.Pt.Add(new PointD(i * IP.StepLength + IP.StartLength, temp));
			}
			return p;
		}

		private delegate void GetProfileThreadDelegateWithTiltCorrection(int xMin, int xMax, int yMin, int yMax, ref double[] profile, ref double[] pixels);

		/// <summary>
		/// 2theta-intensity histgram (旧バージョン)
		/// </summary>
		/// <param name="xMin"></param>
		/// <param name="xMax"></param>
		/// <param name="yMin"></param>
		/// <param name="yMax"></param>
		/// <param name="profile"></param>
		/// <param name="pixels"></param>
		public static void GetProfileThreadWithTiltCorrection(int xMin, int xMax, int yMin, int yMax, ref double[] profile, ref double[] pixels)
		{
			int i, j, k, width, kStart;
			double I, area1, area2;
			width = IP.SrcWidth;
			int jWidth;
			int xMax1 = xMax + 1;
			int width1 = width + 1;
			int width2 = width1 + 1;

			int length = R2.Length;

			double x1, y1, x2, y2, x3, y3, x4, y4;
			double r1, r2, r3, r4;
			double sqrt;
			double Area;

			double x12, y12, x13, y13, x24, y24, x34, y34;
			//x12 = y12 = x13 = y13 = x24 = y24 = x34 = y34 = 0;

			double xx12, yy12, xy12, xx13, yy13, xy13, xx24, yy24, xy24, xx34, yy34, xy34;
			double xx12sq, yy12sq, xy12sq, xx13sq, yy13sq, xy13sq, xx24sq, yy24sq, xy24sq, xx34sq, yy34sq, xy34sq;
			double xxyy12, xxyy13, xxyy24, xxyy34;

			xx12 = yy12 = xy12 = xx13 = yy13 = xy13 = xx24 = yy24 = xy24 = xx34 = yy34 = xy34
				= xx12sq = yy12sq = xy12sq = xx13sq = yy13sq = xy13sq = xx24sq = yy24sq = xy24sq = xx34sq = yy34sq = xy34sq
				= xxyy12 = xxyy13 = xxyy24 = xxyy34 = 0;

			double FD = IP.FilmDistance;
			//傾き補正
			double tempY2TanKsi, numer4;
			double tempY2numer1, tempY2numer3, tempY2denom1plusFD;
			double tempX;
			double[] IntersectionPointX = new double[(width + 1) * 2];
			double[] IntersectionPointY = new double[(width + 1) * 2];
			double[] IntersectionPointR2 = new double[(width + 1) * 2];
			double centerX = IP.CenterX;
			double centerY = IP.CenterY;
			double pixSizeX = IP.PixSizeX;
			double pixSizeY = IP.PixSizeY;

			double tempY2;
			int j1width1;

			Func<double, int> getIndex;
			if (IP.Mode == HorizontalAxis.Angle)
				getIndex = r => { return (int)((Math.Atan(Math.Sqrt(r) / FD) - IP.StartAngle) / IP.StepAngle); };
			else if (IP.Mode == HorizontalAxis.Length)
				getIndex = r => { return (int)((Math.Sqrt(r) - IP.StartLength) / IP.StepLength); };
			else//
				getIndex = r => { return (int)((IP.EndDspacing - IP.WaveLength / 2 / Math.Sin(Math.Atan(Math.Sqrt(r) / FD) / 2)) / IP.StepDspacing - 1); };

			//最初の１行目のInterSectionだけ確保しておく
			j = yMin - 1;
			tempY2 = (j - centerY + 0.5) * pixSizeY;
			tempY2TanKsi = tempY2 * TanKsi;
			tempY2numer1 = tempY2 * Numer1;
			tempY2numer3 = tempY2 * Numer3;
			tempY2denom1plusFD = tempY2 * Denom1 + FD;
			int m;
			for (m = xMin; m < xMax1; m++)
				if (IsCalcPosition[(j + 1) * width1 + m])
				{
					tempX = (m - centerX - 0.5) * pixSizeX + tempY2TanKsi;
					numer4 = FD / (tempY2denom1plusFD + tempX * Denom2);
					double x = (tempY2numer1 + tempX * Numer2) * numer4;
					double y = (tempX * Numer1 + tempY2numer3) * numer4;
					double l2 = x * x + y * y;

					//以下は、l1(見かけのピーク位置mm)としたときの真の l1' = FD * sin(l1/radius) *radius / ( FD- radius * (1-cos(l1/radius)) )を変形
					// sin(l1/radius) => l1/radiu,  1- cos(l1/radius) => 2 sin^2 (l1/2/radius) を利用
					double sphericalCorrection = FD / (FD - l2 * IP.SpericalRadiusInverse / 2.0);

					IntersectionPointX[m + width1] = x * sphericalCorrection;
					IntersectionPointY[m + width1] = y * sphericalCorrection;
					IntersectionPointR2[m + width1] = IntersectionPointX[m + width1] * IntersectionPointX[m + width1] + IntersectionPointY[m + width1] * IntersectionPointY[m + width1];
				}

			//ここから積分開始
			for (j = yMin; j < yMax; j++)
			{
				tempY2 = (j - centerY + 0.5) * pixSizeY;
				tempY2TanKsi = tempY2 * TanKsi;
				tempY2numer1 = tempY2 * Numer1;
				tempY2numer3 = tempY2 * Numer3;
				tempY2denom1plusFD = tempY2 * Denom1 + FD;
				j1width1 = (j + 1) * width1;

				Array.Copy(IntersectionPointX, width1, IntersectionPointX, 0, width1);
				Array.Copy(IntersectionPointY, width1, IntersectionPointY, 0, width1);
				Array.Copy(IntersectionPointR2, width1, IntersectionPointR2, 0, width1);

				for (m = xMin; m < xMax1; m++)
					if (IsCalcPosition[j1width1 + m])
					{
						tempX = (m - centerX - 0.5) * pixSizeX + tempY2TanKsi;
						numer4 = FD / (tempY2denom1plusFD + tempX * Denom2);

						double x = (tempY2numer1 + tempX * Numer2) * numer4;
						double y = (tempX * Numer1 + tempY2numer3) * numer4;
						double l2 = x * x + y * y;

						//以下は、l1(見かけのピーク位置mm)　　l2 = FD * sin(l1/radius) *radius / ( FD- radius * (1-cos(l1/radius)) )を変形
						double sphericalCorrection = FD / (FD - l2 * IP.SpericalRadiusInverse / 2.0);

						IntersectionPointX[m + width1] = x * sphericalCorrection;
						IntersectionPointY[m + width1] = y * sphericalCorrection;
						//IntersectionPointX[m + width1] = tempY2numer1 * numer4 + tempX * numer2 * numer4;
						//IntersectionPointY[m + width1] = tempX * numer1 * numer4 + tempY2numer3 * numer4;
						IntersectionPointR2[m + width1] = IntersectionPointX[m + width1] * IntersectionPointX[m + width1] + IntersectionPointY[m + width1] * IntersectionPointY[m + width1];
					}

				jWidth = j * width;
				for (i = xMin; i < xMax; i++)
				{
					if (IsValid[i + jWidth])//マスクされていないとき
					{
						I = Intensity[i + jWidth];

						//第一象限に持ってくる
						if (IntersectionPointX[i] + IntersectionPointX[i + 1] + IntersectionPointX[i + width1] + IntersectionPointX[i + width2] > 0)
							if (IntersectionPointY[i] + IntersectionPointY[i + 1] + IntersectionPointY[i + width1] + IntersectionPointY[i + width2] > 0)
							{// X>0 Y>0
								x1 = IntersectionPointX[i]; y1 = IntersectionPointY[i]; r1 = IntersectionPointR2[i];
								x2 = IntersectionPointX[i + 1]; y2 = IntersectionPointY[i + 1]; r2 = IntersectionPointR2[i + 1];
								x3 = IntersectionPointX[i + width1]; y3 = IntersectionPointY[i + width1]; r3 = IntersectionPointR2[i + width1];
								x4 = IntersectionPointX[i + width2]; y4 = IntersectionPointY[i + width2]; r4 = IntersectionPointR2[i + width2];
							}
							else
							{// X>0 Y<0
								x3 = IntersectionPointX[i]; y3 = -IntersectionPointY[i]; r3 = IntersectionPointR2[i];
								x4 = IntersectionPointX[i + 1]; y4 = -IntersectionPointY[i + 1]; r4 = IntersectionPointR2[i + 1];
								x1 = IntersectionPointX[i + width1]; y1 = -IntersectionPointY[i + width1]; r1 = IntersectionPointR2[i + width1];
								x2 = IntersectionPointX[i + width2]; y2 = -IntersectionPointY[i + width2]; r2 = IntersectionPointR2[i + width2];
							}
						else
							if (IntersectionPointY[i] + IntersectionPointY[i + 1] + IntersectionPointY[i + width1] + IntersectionPointY[i + width2] > 0)
						{ // X<0 Y>0
							x2 = -IntersectionPointX[i]; y2 = IntersectionPointY[i]; r2 = IntersectionPointR2[i];
							x1 = -IntersectionPointX[i + 1]; y1 = IntersectionPointY[i + 1]; r1 = IntersectionPointR2[i + 1];
							x4 = -IntersectionPointX[i + width1]; y4 = IntersectionPointY[i + width1]; r4 = IntersectionPointR2[i + width1];
							x3 = -IntersectionPointX[i + width2]; y3 = IntersectionPointY[i + width2]; r3 = IntersectionPointR2[i + width2];
						}
						else
						{ // x<0 y<0
							x4 = -IntersectionPointX[i]; y4 = -IntersectionPointY[i]; r4 = IntersectionPointR2[i];
							x3 = -IntersectionPointX[i + 1]; y3 = -IntersectionPointY[i + 1]; r3 = IntersectionPointR2[i + 1];
							x2 = -IntersectionPointX[i + width1]; y2 = -IntersectionPointY[i + width1]; r2 = IntersectionPointR2[i + width1];
							x1 = -IntersectionPointX[i + width2]; y1 = -IntersectionPointY[i + width2]; r1 = IntersectionPointR2[i + width2];
						}

						//中心からの最短距離は

						kStart = getIndex(r1);

						if (kStart < 0)
							kStart = 0;
						if (kStart >= length)
							kStart = length;

						area2 = 0;
						xx12sq = xx13sq = xx34sq = xx24sq = 10000000000;
						xy12 = x2 * y1 - x1 * y2;
						xy13 = x3 * y1 - x1 * y3;
						xy34 = x4 * y3 - x3 * y4;
						xy24 = x4 * y2 - x2 * y4;

						//矩形の面積
						Area = 0.5 * (xy13 - xy12 + xy34 - xy24);

						for (k = kStart; k < length; k++)
						{
							if (R2[k] > r1)//r1よりは外側のとき
							{
								if (R2[k] > r4)//r4より外側のとき
									area1 = Area;
								else if (R2[k] < r2)//Pt2より内側のとき
								{
									//1-2との交点
									if (xx12sq == 10000000000)
									{
										xx12sq = (xx12 = x1 - x2) * xx12;
										yy12sq = (yy12 = y1 - y2) * yy12;
										xy12sq = xy12 * xy12;
										xxyy12 = xx12sq + yy12sq;
									}
									sqrt = Math.Sqrt(R2[k] * xxyy12 - xy12sq);
									x12 = (xy12 * yy12 - xx12 * sqrt) / xxyy12;
									y12 = (-xy12 * xx12 - yy12 * sqrt) / xxyy12;

									if (R2[k] < r3)
									{//1-2 1-3 のとき
										//1-3との交点
										if (xx13sq == 10000000000)
										{
											xx13sq = (xx13 = x1 - x3) * xx13;
											yy13sq = (yy13 = y1 - y3) * yy13;
											xy13sq = xy13 * xy13;
											xxyy13 = xx13sq + yy13sq;
										}
										sqrt = Math.Sqrt(R2[k] * xxyy13 - xy13sq);
										x13 = (xy13 * yy13 - xx13 * sqrt) / xxyy13;
										y13 = (-xy13 * xx13 - yy13 * sqrt) / xxyy13;

										area1 = 0.5 * (x12 * y13 - x12 * y1 + x13 * y1 - x13 * y12 + x1 * y12 - x1 * y13);
									}
									else
									{//1-2 3-4 のとき
										//3-4との交点
										if (xx34sq == 10000000000)
										{
											xx34sq = (xx34 = x3 - x4) * xx34;
											yy34sq = (yy34 = y3 - y4) * yy34;
											xy34sq = xy34 * xy34;
											xxyy34 = xx34sq + yy34sq;
										}
										sqrt = Math.Sqrt(R2[k] * xxyy34 - xy34sq);
										x34 = (xy34 * yy34 - xx34 * sqrt) / xxyy34;
										y34 = (-xy34 * xx34 - yy34 * sqrt) / xxyy34;

										area1 = 0.5 * (x3 * y1 - x12 * y1 - x1 * y3 + x34 * y3 - x3 * y34 + x12 * y34 + x1 * y12 - x34 * y12);
									}
								}
								else if (R2[k] < r3)//Pt3より内側のとき
								{//1-3 2-4
									//1-3
									if (xx13sq == 10000000000)
									{
										xx13sq = (xx13 = x1 - x3) * xx13;
										yy13sq = (yy13 = y1 - y3) * yy13;
										xy13sq = xy13 * xy13;
										xxyy13 = xx13sq + yy13sq;
									}
									sqrt = Math.Sqrt(R2[k] * xxyy13 - xy13sq);
									x13 = (xy13 * yy13 - xx13 * sqrt) / xxyy13;
									y13 = (-xy13 * xx13 - yy13 * sqrt) / xxyy13;
									//2-4との交点
									if (xx24sq == 10000000000)
									{
										xx24sq = (xx24 = x2 - x4) * xx24;
										yy24sq = (yy24 = y2 - y4) * yy24;
										xy24sq = xy24 * xy24;
										xxyy24 = xx24sq + yy24sq;
									}
									sqrt = Math.Sqrt(R2[k] * xxyy24 - xy24sq);
									x24 = (xy24 * yy24 - xx24 * sqrt) / xxyy24;
									y24 = (-xy24 * xx24 - yy24 * sqrt) / xxyy24;

									area1 = 0.5 * (x13 * y1 - x2 * y1 - x1 * y13 + x24 * y13 - x13 * y24 + x2 * y24 + x1 * y2 - x24 * y2);
								}
								else
								{//2-4 3-4で確定
									//2-4との交点
									if (xx24sq == 10000000000)
									{
										xx24sq = (xx24 = x2 - x4) * xx24;
										yy24sq = (yy24 = y2 - y4) * yy24;
										xy24sq = xy24 * xy24;
										xxyy24 = xx24sq + yy24sq;
									}
									sqrt = Math.Sqrt(R2[k] * xxyy24 - xy24sq);
									x24 = (xy24 * yy24 - xx24 * sqrt) / xxyy24;
									y24 = (-xy24 * xx24 - yy24 * sqrt) / xxyy24;
									//3-4との交点
									if (xx34sq == 10000000000)
									{
										xx34sq = (xx34 = x3 - x4) * xx34;
										yy34sq = (yy34 = y3 - y4) * yy34;
										xy34sq = xy34 * xy34;
										xxyy34 = xx34sq + yy34sq;
									}
									sqrt = Math.Sqrt(R2[k] * xxyy34 - xy34sq);
									x34 = (xy34 * yy34 - xx34 * sqrt) / xxyy34;
									y34 = (-xy34 * xx34 - yy34 * sqrt) / xxyy34;

									area1 = Area - 0.5 * (x24 * y4 - x24 * y34 + x4 * y34 - x4 * y24 + x34 * y24 - x34 * y4);
								}
								profile[k] += (area1 - area2) / Area * I;
								pixels[k] += (area1 - area2) / Area;
								if (area1 >= Area) break;
								area2 = area1;
							}
						}
					}
					else
					{
						;
					}
				}
			}

			IntersectionPointX = null;
			IntersectionPointY = null;
			IntersectionPointR2 = null;
			//GC.Collect();
		}

        #endregion

        #region GetProfile新バージョン
        /// <summary>
        ///  2theta-intensity histgram (新バージョン)
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMin"></param>
        /// <param name="yMax"></param>
        /// <param name="profile">profile[sector][2theta] </param>
        /// <param name="pixels">pixels[sector][2theta]</param>
        public static (double[][] Profile, double[][] Pixels) GetProfileThreadWithTiltCorrectionNew(int xMin, int xMax, int yMin, int yMax, int chiDivision = 1)
		{
			var profile = new double[chiDivision][];
			var pixels = new double[chiDivision][];
			var chi = new double[chiDivision];
			for (int i = 0; i < chiDivision; i++)
			{
				profile[i] = new double[R2.Length];
				pixels[i] = new double[R2.Length];
				chi[i] = (2 * i + 1) * Math.PI / chiDivision;
			}

			int width = IP.SrcWidth;
			double centerX = IP.CenterX, centerY = IP.CenterY, pixSizeX = IP.PixSizeX, pixSizeY = IP.PixSizeY, startAngle = IP.StartAngle, stepAngle = IP.StepAngle, fd = IP.FilmDistance;

			//x方向に0.5ピクセル進んだ時の並進ベクトル
			double tX = pixSizeX / 2, tY = 0.0;
			(double X, double Y, double Z) slideX = (Numer2 * tX + Numer1 * tY, Numer1 * tX + Numer3 * tY, Denom2 * tX + Denom1 * tY);

			//Y方向に0.5ピクセル進んだ時の並進ベクトル
			tY = pixSizeY / 2;
			tX = pixSizeY * TanKsi / 2;

			(double X, double Y, double Z) slideY = (Numer2 * tX + Numer1 * tY, Numer1 * tX + Numer3 * tY, Denom2 * tX + Denom1 * tY);

			(double X, double Y, double Z)[] slide = new[] {
				(slideX.X + slideY.X, slideX.Y + slideY.Y, slideX.Z + slideY.Z),
				(slideX.X - slideY.X, slideX.Y - slideY.Y, slideX.Z - slideY.Z) };

			//IPの法線ベクトル
			(double X, double Y, double Z) detector_normal = (Denom2, Denom1, -CosTau);

			var pt0 = new (double X, double Y)[8];
			var pt1 = new (double X, double Y)[8];
			var pt2 = new (double X, double Y)[8];
			var pt3 = new (double X, double Y)[8];
			var pt4 = new (double X, double Y)[8];

			//ここから積分開始
			for (int j = yMin; j < yMax; j++)
			{
				var tempY = (j - centerY) * pixSizeY;//IP平面上の座標系におけるY位置
				var tempY2TanKsi = tempY * TanKsi;
				var numer1TempY = Numer1 * tempY;
				var numer3TempY = Numer3 * tempY;
				var denom1tempYFD = Denom1 * tempY + fd;

				var jWidth = j * width;
				for (int i = xMin; i < xMax; i++)
				{
					if (IsValid[i + jWidth])//マスクされていないとき
					{
						var tempX = (i - centerX) * pixSizeX + tempY2TanKsi;//IP平面上の座標系におけるX位置
						//以下のx,y,zがピクセル中心の空間位置
						var x = Numer2 * tempX + numer1TempY;
						var y = Numer1 * tempX + numer3TempY;
						var z = Denom2 * tempX + denom1tempYFD;

						#region 球面補正が必要な場合
						if (IP.SpericalRadiusInverse != 0) //球面補正が必要な場合
						{
							//検出器中心(0,0,FD)からピクセルまでの距離
							var distance = Math.Sqrt(x * x + y * y + (z - fd) * (z - fd));
							//検出器のダイレクトスポット方向に縮める割合
							var coeff_detector_palallel = Math.Sin(distance * IP.SpericalRadiusInverse) / distance / IP.SpericalRadiusInverse;
							//検出器の法線方向に進む距離
							var slide_detector_normal = (1 - Math.Cos(distance * IP.SpericalRadiusInverse)) / IP.SpericalRadiusInverse;
							//(0,0,FD)から(X,Y,Z)のベクトルにcoeff_detector_palallelをかけた後、detector_normalの方向にslide_detector_normalだけ進める。
							x = x * coeff_detector_palallel + detector_normal.X * slide_detector_normal;
							y = y * coeff_detector_palallel + detector_normal.Y * slide_detector_normal;
							z = (z - fd) * coeff_detector_palallel + fd + detector_normal.Z * slide_detector_normal;
						}
                        #endregion

                        double l2 = x * x + y * y + z * z, q = Math.Sqrt(x * x + y * y), l = Math.Sqrt(l2);

						//四隅の頂点座標を計算
						for (int k = 0; k < 2; k++)
						{
							double a = x + slide[k].X, b = y + slide[k].Y, c = z + slide[k].Z;

							var bNew = (b * x - a * y) / q;
							var p = a * x + b * y + c * z;
							var vxTemp = (a * a + b * b + c * c - bNew * bNew) * l2 / p / p - 1;
							pt0[k].X = vxTemp > 0 ? fd * Math.Sqrt(vxTemp) : 0;
							pt0[k].Y = bNew * l * fd / p;
							if (c * l2 < z * p)
								pt0[k].X = -pt0[k].X;
							pt0[k + 2] = (-pt0[k].X, -pt0[k].Y);
						}
						var n0 = 4;

						var twoTheta = z >= 0 ? Math.Asin(q / l) : Math.PI - Math.Asin(q / l);//2θ算出
						int startIndex;
						if (IP.Mode == HorizontalAxis.Angle)
						{
							var devTwoTheta = Math.Atan(Math.Max(Math.Abs(pt0[0].X), Math.Abs(pt0[1].X)) / fd);//ピクセル内での2θの変動幅
							startIndex = Math.Max(0, (int)((twoTheta - devTwoTheta - startAngle) / stepAngle + 0.5));
						}
						else
						{
							for (startIndex = 0; startIndex < R2.Length - 1; startIndex++)
								if (R2[startIndex + 1] > twoTheta)
									break;
						}
						var intensityPerArea = Intensity[i + jWidth] / getArea(4, pt0);

						var chiAngle = Math.Atan2(y, x); //ピクセル中心のChi角
						if (chiAngle < 0) chiAngle += 2 * Math.PI;
						var devChiAngle = Math.Atan(Math.Max(Math.Abs(pt0[0].Y), Math.Abs(pt0[1].Y)) / fd / q * l); //ピクセル内でのChiの変動幅
						var startChiIndex = Math.Max(0, (int)((chiAngle - devChiAngle) / 2 / Math.PI * chiDivision - 0.5)); //ピクセル中心のChi角

						//矩形を x = c の直線(2シータの分割線) と y = d の直線(セクターの分割線)で切り取り、面積比を計算するループ
						for (int k1 = startIndex; k1 < R2.Length; k1++)
						{
							//x が c1以下の矩形(pt1)と、c1以上の矩形(pt2)を生成
							var c = Math.Tan(R2[k1] - twoTheta) * fd;

							int n1 = 0, n2 = 0;
							
							for (int m = 0; m < n0; m++)//pt1は、現在の2Θ範囲のポリゴン、pt2は次の範囲のポリゴン
							{
								(double X, double Y) p1 = pt0[m], p2 = m == n0 - 1 ? pt0[0] : pt0[m + 1];
								if (p1.X < c)
								{
									pt1[n1++] = p1;
									if (c <= p2.X)
										pt1[n1++] = pt2[n2++] = (c, (c * p2.Y - c * p1.Y - p1.X * p2.Y + p2.X * p1.Y) / (p2.X - p1.X));
								}
								else
								{
									pt2[n2++] = p1;
									if (c > p2.X)
										pt1[n1++] = pt2[n2++] = (c, (c * p2.Y - c * p1.Y - p1.X * p2.Y + p2.X * p1.Y) / (p2.X - p1.X));
								}
							}

							if (chiDivision == 1)//セクター分割をしない場合
							{
								var area = getArea(n1, pt1);
								pixels[0][k1] += area;
								profile[0][k1] += area * intensityPerArea;
							}
							else//セクター分割をする場合
							{
								for (int k2 = startChiIndex; k2< chi.Length; k2++)
								{
									var d = q / l * Math.Tan(chi[k2] - chiAngle) * fd;
									//矩形pt1を更に分割していく
									int n3 = 0, n4 = 0;
									for (int m = 0; m < n1; m++)//pt3は、現在の2Θ範囲のポリゴン、pt4は次の範囲のポリゴン
									{
										(double X, double Y) p1 = pt1[m], p2 = m == n1 - 1 ? pt1[0] : pt1[m + 1];
										if (p1.Y < d)
										{
											pt3[n3++] = p1;
											if (d <= p2.Y)
												pt3[n3++] = pt4[n4++] = ((d * p2.X - d * p1.X - p1.Y * p2.X + p2.Y * p1.X) / (p2.Y - p1.Y), d);
										}
										else
										{
											pt4[n4++] = p1;
											if (d > p2.Y)
												pt3[n3++] = pt4[n4++] = ((d * p2.X - d * p1.X - p1.Y * p2.X + p2.Y * p1.X) / (p2.Y - p1.Y), d);
										}
									}
									var area = getArea(n3, pt3);
									pixels[k2][k1] += area;
									profile[k2][k1] += area * intensityPerArea;
									
									if (n4 == 0)//次のポリゴンが無かったら終了
										break;

									Array.Copy(pt4, pt1, n4);
									n1 = n4;

									if (k2 == chiDivision-1)
										k2 = -1;
								}
							}

							if (n2 == 0)//次のポリゴンが無かったら終了
								break;
							Array.Copy(pt2, pt0, n2);
							n0 = n2;

							#region 20200430 上のコードで十分速度が出るので、お蔵入り 
							/*
							if (R2[k] > twoTheta + devTwoTheta)
							{
								pixels[k] += area - area2;
								profile[k] += (area - area2) * intensityPerArea;
								break;
							}
							var c = Math.Tan(R2[k] - twoTheta) * fd;
							//var pt = new List<(double X, double Y)>();
							int n1 = 0;
							for (int m = 0; m < 4; m++)
							{
								(double X, double Y) p1 = v[m], p2 = m == 3 ? currPolygon[0] : v[m + 1];
								if (p1.X < c)
								{
									pt1[n1++]= p1;
									if (c <= p2.X)
										pt1[n1++] = (c, (c * p2.Y - c * p1.Y - p1.X * p2.Y + p2.X * p1.Y) / (p2.X - p1.X));
								}
								else if (c > p2.X)
									pt1[n1++] = (c, (c * p2.Y - c * p1.Y - p1.X * p2.Y + p2.X * p1.Y) / (p2.X - p1.X));
							}

							var area1 = Math.Abs(n1 switch
							{
								3 => pt1[0].X * (pt1[1].Y - pt1[2].Y) + pt1[1].X * (pt1[2].Y - pt1[0].Y) + pt1[2].X * (pt1[0].Y - pt1[1].Y),//0 - 1 - 2 が作る3角形
								4 => pt1[0].X * (pt1[1].Y - pt1[3].Y) + pt1[1].X * (pt1[2].Y - pt1[0].Y) + pt1[2].X * (pt1[3].Y - pt1[1].Y) + pt1[3].X * (pt1[0].Y - pt1[2].Y),// 0 - 1 - 2 -  3 が作る4角形
								5 => pt1[0].X * (pt1[1].Y - pt1[4].Y) + pt1[1].X * (pt1[2].Y - pt1[0].Y) + pt1[2].X * (pt1[3].Y - pt1[1].Y) + pt1[3].X * (pt1[4].Y - pt1[2].Y) + pt1[4].X * (pt1[0].Y - pt1[3].Y),// 0 - 1 - 2 - 3 - 4 が作る5角形
								_ => 0
							}) * 0.5;

							pixels[k] += area1 - area2;
							profile[k] += (area1 - area2) * intensityPerArea;
							area2 = area1;
							*/
							#endregion
						}
					}
				}
			}

			var mag = 1 / (pixSizeX * pixSizeY);//係数
			for (int i = 0; i < pixels.Length; i++)
				for (int j = 0; j < pixels[i].Length; j++)
					pixels[i][j] *= mag;

			//未実装 最後にChiRotationとChiDirectionを考慮して、行を入れ替え

			return (profile, pixels);
		}

		/// <summary>
		/// ポリゴンの面積を求める
		/// </summary>
		/// <param name="n"></param>
		/// <param name="pt"></param>
		/// <returns></returns>
		private static double getArea(int n, (double X, double Y)[] pt)
        {
			if (n < 3) return 0;
			var result = pt[0].X * (pt[1].Y - pt[n - 1].Y) + pt[n - 1].X * (pt[0].Y - pt[n - 2].Y);
			for (int i = 1; i < n-1; i++)
				result += pt[i].X * (pt[i + 1].Y - pt[i-1].Y);
			return Math.Abs(result) * 0.5;
		}
		#endregion

		#region FindCenter
		public static PointD FindCenter(IntegralProperty iP, int radius, List<bool> mask)
		{
			if (iP.CenterY < radius + 2 || iP.CenterY > iP.SrcHeight - radius - 2 || iP.CenterX < radius + 2 || iP.CenterX > iP.SrcWidth - radius - 2)
				return new PointD(iP.CenterX, iP.CenterY);

			int xStart = (int)(iP.CenterX - radius);
			int yStart = (int)(iP.CenterY - radius);

			double[,] tempIntensity = new double[radius * 2 + 1, radius * 2 + 1];
			for (int x = 0; x < radius * 2 + 1; x++)
				for (int y = 0; y < radius * 2 + 1; y++)
				{
					int pos = (yStart + y) * iP.SrcWidth + xStart + x;
					if (mask != null && mask[pos] == true)
						tempIntensity[x, y] = double.NaN;
					else
						tempIntensity[x, y] = Intensity[pos];
				}
			PointD offset = FittingPeak.FitPeakAsPseudoVoigtByMarcal2D(tempIntensity);
			if (double.IsNaN(offset.X))
				return new PointD(iP.CenterX, iP.CenterY);

			return new PointD(offset.X + xStart, offset.Y + yStart);
		}
		#endregion

		#region SetFindTiltParameter
		//FindTiltCorrection用の定数を先に決めておくメソッド
		public static void SetFindTiltParameter(IntegralProperty iP, double[] peaks, double serchRange)
		{
			if (double.IsNaN(IP.EndLength))
				return;

			var thread = Environment.ProcessorCount;

			IP = iP;
			int i, j, k;
			int h = IP.SrcHeight;
			int w = IP.SrcWidth;

			//積分領域全体のy上限、y下限を決める
			int YMin, YMax;
			YMin = YMax = 0;
			for (i = 0; i < IsValid.Count; i++)
				if (IsValid[i])
				{//マスクされていないポイントが見つかったら
					YMin = i / w;
					break;
				}
			for (i = IsValid.Count - 1; i > -1; i--)
				if (IsValid[i])
				{//マスクされていないポイントが見つかったら
					YMax = i / w + 1;
					break;
				}

			xMax = xMin = 0;
			bool flag = true;
			for (i = 0; i < w && flag; i++)
				for (j = YMin; j < YMax; j++)
					if (IsValid[i + j * w])
					{
						xMin = i;
						flag = false;
						break;
					}
			flag = true;
			for (i = w - 1; i > -1 && flag; i--)
				for (j = YMin; j < YMax; j++)
					if (IsValid[i + j * w])
					{
						xMax = i + 1;
						flag = false;
						break;
					}

			//各スレッドの上限と下限を決める
			yThreadMin = new int[thread];
			yThreadMax = new int[thread];
			int yStep = (YMax - YMin) / thread;
			for (i = 0; i < thread; i++)
			{
				yThreadMin[i] = YMin + i * yStep;
				yThreadMax[i] = YMin + (i + 1) * yStep;
			}
			yThreadMax[thread - 1] = YMax;

			//フィッティングに必要のない範囲はマスクする(スレッドを利用)
			serchRange *= 2;//実際のサーチレンジよりちょっと多めに
			double[] peaksPlusRange2 = new double[peaks.Length];
			double[] peaksMinusRange2 = new double[peaks.Length];
			for (k = 0; k < peaks.Length; k++)
			{
				peaksPlusRange2[k] = (peaks[k] + serchRange) * (peaks[k] + serchRange);
				peaksMinusRange2[k] = (peaks[k] - serchRange) * (peaks[k] - serchRange);
			}

			//計算する交点をきめる
			IsCalcPosition = new bool[(h + 1) * (w + 1)];

			//傾き補正係数を計算
			SetTiltParameter();
			
			Parallel.For(0, thread, i => SetFindTiltParameterThread(xMin, xMax, yThreadMin[i], yThreadMax[i], peaksPlusRange2, peaksMinusRange2));

			if (double.IsNaN(IP.EndLength))
				return;

			//配列Rを作成　各ステップごとの中心からの距離を格納する配列
			R2 = new double[(int)((IP.EndLength - IP.StartLength) / IP.StepLength) + 1];
			double temp;
			for (i = 0; i < R2.Length; i++)
			{
				temp = IP.StartLength + (i + 0.5) * IP.StepLength;
				R2[i] = temp * temp;
			}
		}

		public static void SetFindTiltParameterThread(int xMin, int xMax, int YMin, int YMax, double[] peaksPlusRange2, double[] peaksMinusRange2)
		{
			int i, j, k;
			int h = IP.SrcHeight;
			int w = IP.SrcWidth;
			double X, Y, R, tempY2TanKsi, numer4, tempY2numer1, tempY2numer3, tempY2denom1plusFD, tempX, tempY2;
			double pixX = IP.PixSizeX;
			double pixY = IP.PixSizeY;
			double FD = IP.FilmDistance;
			double centerX = IP.CenterX;
			double centerY = IP.CenterY;
			int jw, jw1, j1w1;
			int peakLength = peaksMinusRange2.Length;
			for (j = YMin; j < YMax; j++)
			{
				jw = j * w;
				jw1 = j * (w + 1);
				j1w1 = (j + 1) * (w + 1);

				tempY2 = (j - centerY) * pixY;
				tempY2TanKsi = tempY2 * TanKsi;
				tempY2numer1 = tempY2 * Numer1;
				tempY2numer3 = tempY2 * Numer3;
				tempY2denom1plusFD = tempY2 * Denom1 + FD;

				for (i = xMin; i < xMax; i++)
					if (IsValid[jw + i])
					{
						tempX = (i - centerX) * pixX + tempY2TanKsi;
						numer4 = FD / (tempY2denom1plusFD + tempX * Denom2);
						X = tempY2numer1 * numer4 + tempX * Numer2 * numer4;
						Y = tempX * Numer1 * numer4 + tempY2numer3 * numer4;
						R = X * X + Y * Y;

						IsValid[jw + i] = false;

						for (k = 0; k < peakLength; k++)
							if (R < peaksPlusRange2[k] && R > peaksMinusRange2[k])
							{
								IsValid[jw + i] = true;
								IsCalcPosition[jw1 + i] = true;
								IsCalcPosition[jw1 + i + 1] = true;
								IsCalcPosition[j1w1 + i] = true;
								IsCalcPosition[j1w1 + i + 1] = true;
								break;
							}
					}
			}
		}

        #endregion

        #region バッググラウンド減算
        public static List<double> SubtractBackground(IEnumerable<double> src,
		IEnumerable<double> bg, double coeff = 1)
		{
			if (src.Count() != bg.Count())
				return new List<double>(src);
			else
			{
				var bgArray = bg.ToArray();
				return src.ToArray().Select((s, i) => s - bgArray[i] * coeff).ToList();
			}
		}
		#endregion

		#region バックグランド関数。 未完成
		public static double[] GetBackground(double lower, double upper)
		{
			return Intensity.ToArray();

			/*
			//バックグラウンド形状を探し出す。
			List<PointD> pt = new List<PointD>();
			List<double> I = new List<double>();
			double diaX = Math.Min(IP.CenterX, IP.SrcWidth - IP.CenterX);
			double diaY = Math.Min(IP.CenterY, IP.SrcHeight - IP.CenterY);
			double maxX = Math.Sqrt(diaX * diaX + diaY * diaY) * upper;
			if (maxX <= 0)
				return RawData.ToArray();
			double maxI = double.NegativeInfinity;
			int length = RawData.Count / IP.SrcWidth;
			for (int i = 0; i < length; i++)
			{
				double r = maxX * ((1 - lower) * i / length + lower);

				double angle = i / 180.0 * Math.PI;
				int PosX, PosY;

				for (int j = 0; j < 360; j++)
				{
					angle += Math.PI / 180;
					PosX = (int)(IP.CenterX + r * Math.Cos(angle) + 0.5);
					PosY = (int)(IP.CenterY + r * Math.Sin(angle) + 0.5);
					bool flag = true;
					for (int k = pt.Count - 20; k < pt.Count && flag; k++)
					{
						if (k >= 0 && pt[k].X == Math.Abs(PosX - IP.CenterX) && pt[k].Y == Math.Abs(PosY - IP.CenterY))
							flag = false;
					}

					if (flag && PosX >= IP.SrcWidth * (1 - upper) & PosX < IP.SrcWidth * upper && PosY >= IP.SrcHeight * (1 - upper) & PosY < IP.SrcHeight * upper)
					{
						int counter = 0;
						double temp = 0;
						for (int h = -1; h <= 1; h++)
							for (int w = -1; w <= 1; w++)
								if (PosX + w >= IP.SrcWidth * (1 - upper) & PosX + w < IP.SrcWidth * upper && PosY + h >= IP.SrcHeight * (1 - upper) & PosY + h < IP.SrcHeight * upper)
								{
									temp += RawData[(PosX + w) + (PosY + h) * IP.SrcWidth];
									counter++;
								}
						I.Add(temp / counter);
						pt.Add(new PointD(Math.Abs(PosX - IP.CenterX), Math.Abs(PosY - IP.CenterY)));
						break;
					}
				}
			}

			//Iを「ならす」
			for (int j = 0; j < 5; j++)
			{
				double[] tempI = I.ToArray();
				for (int i = 20; i < I.Count - 20; i++)
				{
					//もしプラスマイナス5の点が50%ずれていたら
					if (Math.Abs(tempI[i - 20] + tempI[i + 20] - 2 * tempI[i]) / 2 / tempI[i] > 0.5)
						I[i] = -1;
				}

				for (int i = 0; i < I.Count; i++)
					if (I[i] == -1)
					{
						I.RemoveAt(i);
						pt.RemoveAt(i);
						i--;
					}
			}
			for (int i = 0; i < I.Count; i++)
				if (maxI < I[i])
					maxI = I[i];

			I.RemoveRange(0, 20);
			I.RemoveRange(I.Count - 20, 20);
			pt.RemoveRange(0, 20);
			pt.RemoveRange(pt.Count - 20, 20);

			//関数形は F0 = Ia * exp ( - 4 Log2 ( (x-X)^2 + C(y-Y)^2 ) / Ha^2 ) + Ib * exp ( - 4 Log2 ( (x-X)^2 + C(y-Y)^2 ) / Hb^2 )
			//評価値は Σ1/(F-F0)^2

			double Ra, Ca, Ia, Ha, Rb, Cb, Ib, Hb, Rc, Cc, Ic, Hc;
			double Ra_new, Ca_new, Ia_new, Ha_new, Rb_new, Cb_new, Ib_new, Hb_new, Rc_new, Cc_new, Ic_new, Hc_new;

			Ra = 0;
			Rb = maxX / 12;
			Rc = maxX / 6;
			Ca = Cb = Cc = 1;
			Ia = maxI * 0.5;
			Ib = maxI * 0.3;
			Ic = maxI * 0.2;
			Ha = maxX / 4;
			Hb = maxX;
			Hc = maxX * 4;

			double[,] diff = new double[12, pt.Count];
			Matrix Alpha = new Matrix(12, 12);
			Matrix Beta = new Matrix(12, 1);
			double[] ResidualCurrent = new double[pt.Count];
			double[] ResidualNew = new double[pt.Count];
			double ResidualSquareCurrent;
			double ResidualSquareNew = 0;
			int count = 0;
			double Ln2 = Math.Log(2);

			//現在の残差を計算
			ResidualSquareCurrent = 0;
			for (int i = 0; i < pt.Count; i++)
			{
				double x = pt[i].X, y = pt[i].Y;
				double x2 = x * x, y2 = y * y;
				double Ca2 = Ca * Ca, Cb2 = Cb * Cb, Cc2 = Cc * Cc;
				double Ra2 = Ra * Ra, Rb2 = Rb * Rb, Rc2 = Rc * Rc;
				double Aa = Math.Sqrt(x2 + Ca2 * y2);
				double Ab = Math.Sqrt(x2 + Cb2 * y2);
				double Ac = Math.Sqrt(x2 + Cc2 * y2);

				ResidualCurrent[i] = I[i] - (
					Ia * Math.Pow(2, (-4 * (-2 * Aa * Ra + Ra2 + x2 + Ca2 * y2)) / Ha / Ha) +
					Ib * Math.Pow(2, (-4 * (-2 * Ab * Rb + Rb2 + x2 + Cb2 * y2)) / Hb / Hb) +
					Ic * Math.Pow(2, (-4 * (-2 * Ac * Rc + Rc2 + x2 + Cc2 * y2)) / Hc / Hc));
				ResidualSquareCurrent += ResidualCurrent[i] * ResidualCurrent[i];
			}
			double ramda = 1;

			while (ramda < 1000000000000 && count < 600)
			{
				count++;
				for (int i = 0; i < pt.Count; i++)//偏微分を作る
				{
					double x = pt[i].X, y = pt[i].Y;
					double x2 = x * x, y2 = y * y;
					double Ha2 = Ha * Ha, Hb2 = Hb * Hb, Hc2 = Hc * Hc;
					double Ha3 = Ha * Ha * Ha, Hb3 = Hb * Hb * Hb, Hc3 = Hc * Hc * Hc;
					double Ca2 = Ca * Ca, Cb2 = Cb * Cb, Cc2 = Cc * Cc;
					double Ra2 = Ra * Ra, Rb2 = Rb * Rb, Rc2 = Rc * Rc;
					double Aa = Math.Sqrt(x2 + Ca2 * y2);
					double Ab = Math.Sqrt(x2 + Cb2 * y2);
					double Ac = Math.Sqrt(x2 + Cc2 * y2);

					//∂F/∂Ia
					diff[0, i] = Math.Pow(2, (-4 * (-2 * Aa * Ra + Ra2 + x2 + Ca2 * y2)) / Ha2);
					//∂F/∂Ca
					diff[1, i] = -(Math.Pow(2, 3 - (4 * (-2 * Aa * Ra + Ra2 + x2 + Ca2 * y2)) / Ha2) * Ca * Ia * Ln2 * (1 - Ra / Aa) * y2) / Ha2;
					//∂F/∂Ra
					diff[2, i] = -(Math.Pow(2, 2 - (4 * (-2 * Aa * Ra + Ra2 + x2 + Ca2 * y2)) / Ha2) * Ia * Ln2 * (-2 * Aa + 2 * Ra)) / Ha2;
					//∂F/∂Ha
					diff[3, i] = (Math.Pow(2, 3 - (4 * (-2 * Aa * Ra + Ra2 + x2 + Ca2 * y2)) / Ha2) * Ia * Ln2 * (-2 * Aa * Ra + Ra2 + x2 + Ca2 * y2)) / Ha3;

					//∂F/∂Ib
					diff[4, i] = Math.Pow(2, (-4 * (-2 * Ab * Rb + Rb2 + x2 + Cb2 * y2)) / Hb2);
					//∂F/∂Cb
					diff[5, i] = -(Math.Pow(2, 3 - (4 * (-2 * Ab * Rb + Rb2 + x2 + Cb2 * y2)) / Hb2) * Cb * Ib * Ln2 * (1 - Rb / Ab) * y2) / Hb2;
					//∂F/∂Rb
					diff[6, i] = -(Math.Pow(2, 2 - (4 * (-2 * Ab * Rb + Rb2 + x2 + Cb2 * y2)) / Hb2) * Ib * Ln2 * (-2 * Ab + 2 * Rb)) / Hb2;
					//∂F/∂Hc
					diff[7, i] = (Math.Pow(2, 3 - (4 * (-2 * Ab * Rb + Rb2 + x2 + Cb2 * y2)) / Hb2) * Ib * Ln2 * (-2 * Ab * Rb + Rb2 + x2 + Cb2 * y2)) / Hb3;

					//∂F/∂Ic
					diff[8, i] = Math.Pow(2, (-4 * (-2 * Ac * Rc + Rc2 + x2 + Cc2 * y2)) / Hc2);
					//∂F/∂Cc
					diff[9, i] = -(Math.Pow(2, 3 - (4 * (-2 * Ac * Rc + Rc2 + x2 + Cc2 * y2)) / Hc2) * Cc * Ic * Ln2 * (1 - Rc / Ac) * y2) / Hc2;
					//∂F/∂Rc
					diff[10, i] = -(Math.Pow(2, 2 - (4 * (-2 * Ac * Rc + Rc2 + x2 + Cc2 * y2)) / Hc2) * Ic * Ln2 * (-2 * Ac + 2 * Rc)) / Hc2;
					//∂F/∂Hc
					diff[11, i] = (Math.Pow(2, 3 - (4 * (-2 * Ac * Rc + Rc2 + x2 + Cc2 * y2)) / Hc2) * Ic * Ln2 * (-2 * Ac * Rc + Rc2 + x2 + Cc2 * y2)) / Hc3;
				}

				//行列Alpha, Betaを作る
				for (int k = 0; k < 12; k++)
				{
					for (int l = 0; l < 12; l++)
					{
						Alpha.E[k, l] = 0;
						for (int i = 0; i < pt.Count; i++)
							Alpha.E[k, l] += diff[k, i] * diff[l, i];

						if (k == l)
							Alpha.E[k, l] *= (1 + ramda);
					}

					Beta.E[k, 0] = 0;
					for (int i = 0; i < pt.Count; i++)
						Beta.E[k, 0] += ResidualCurrent[i] * diff[k, i];
				}

				Matrix delta = Alpha.Inv() * Beta;
				if (delta.E.Length == 0)
					return RawData.ToArray();

				Ia_new = Ia + delta.E[0, 0];
				Ca_new = Ca + delta.E[1, 0];
				Ra_new = Ra + delta.E[2, 0];
				Ha_new = Ha + delta.E[3, 0];

				Ib_new = Ib + delta.E[4, 0];
				Cb_new = Cb + delta.E[5, 0];
				Rb_new = Rb + delta.E[6, 0];
				Hb_new = Hb + delta.E[7, 0];

				Ic_new = Ic + delta.E[8, 0];
				Cc_new = Cc + delta.E[9, 0];
				Rc_new = Rc + delta.E[10, 0];
				Hc_new = Hc + delta.E[11, 0];

				//あたらしいパラメータでの残差を計算
				ResidualSquareNew = 0;
				for (int i = 0; i < pt.Count; i++)
				{
					double x = pt[i].X, y = pt[i].Y;
					double x2 = x * x, y2 = y * y;
					double Ca2 = Ca_new * Ca_new, Cb2 = Cb_new * Cb_new, Cc2 = Cc_new * Cc_new;
					double Ra2 = Ra_new * Ra_new, Rb2 = Rb_new * Rb_new, Rc2 = Rc_new * Rc_new;
					double Aa = Math.Sqrt(x2 + Ca2 * y2);
					double Ab = Math.Sqrt(x2 + Cb2 * y2);
					double Ac = Math.Sqrt(x2 + Cc2 * y2);

					ResidualNew[i] = I[i] - (
					Ia_new * Math.Pow(2, (-4 * (-2 * Aa * Ra_new + Ra2 + x2 + Ca2 * y2)) / Ha_new / Ha_new) +
					Ib_new * Math.Pow(2, (-4 * (-2 * Ab * Rb_new + Rb2 + x2 + Cb2 * y2)) / Hb_new / Hb_new) +
					Ic_new * Math.Pow(2, (-4 * (-2 * Ac * Rc_new + Rc2 + x2 + Cc2 * y2)) / Hc_new / Hc_new));
					ResidualSquareNew += ResidualNew[i] * ResidualNew[i];
				}
				if (ResidualSquareCurrent > ResidualSquareNew)
					if (Math.Abs(ResidualSquareCurrent - ResidualSquareNew) / ResidualSquareCurrent > 0.0000000001)
					{
						ResidualSquareCurrent = ResidualSquareNew;
						ramda *= 0.3;
						for (int i = 0; i < pt.Count; i++)
							ResidualCurrent[i] = ResidualNew[i];
						Ia = Ia_new; Ca = Ca_new; Ra = Ra_new; Ha = Ha_new;
						Ib = Ib_new; Cb = Cb_new; Rb = Rb_new; Hb = Hb_new;
						Ic = Ic_new; Cc = Cc_new; Rc = Rc_new; Hc = Hc_new;
					}
					else
					{
						Ia = Ia_new; Ca = Ca_new; Ra = Ra_new; Ha = Ha_new;
						Ib = Ib_new; Cb = Cb_new; Rb = Rb_new; Hb = Hb_new;
						Ic = Ic_new; Cc = Cc_new; Rc = Rc_new; Hc = Hc_new;
						break;
					}
				else
					ramda *= 5;
			}

			ushort[] tempInt = new ushort[RawData.Count];
			int n = 0;
			for (int h = 0; h < IP.SrcHeight; h++)

				for (int w = 0; w < IP.SrcWidth; w++)
				{
					double x = Math.Abs(w - IP.CenterX);
					double y = Math.Abs(h - IP.CenterY);
					double x2 = x * x, y2 = y * y;
					double Ca2 = Ca * Ca, Cb2 = Cb * Cb, Cc2 = Cc * Cc;
					double Ra2 = Ra * Ra, Rb2 = Rb * Rb, Rc2 = Rc * Rc;
					double Aa = Math.Sqrt(x2 + Ca2 * y2);
					double Ab = Math.Sqrt(x2 + Cb2 * y2);
					double Ac = Math.Sqrt(x2 + Cc2 * y2);

					double bg =
				   Ia * Math.Pow(2, (-4 * (-2 * Aa * Ra + Ra2 + x2 + Ca2 * y2)) / Ha / Ha) +
				   Ib * Math.Pow(2, (-4 * (-2 * Ab * Rb + Rb2 + x2 + Cb2 * y2)) / Hb / Hb) +
				   Ic * Math.Pow(2, (-4 * (-2 * Ac * Rc + Rc2 + x2 + Cc2 * y2)) / Hc / Hc);

					if (x * x + y * y > 2500)
					{
						if (ConvertTable[(int)RawData[n]] - bg + 100 > 1)
							tempInt[n] = (ushourt)(ConvertTable[(int)RawData[n]] - bg + 100);
						else
							tempInt[n] = 1;
					}
					else
						tempInt[n] = ConvertTable[(int)RawData[n]];
					n++;
				}

			return tempInt;*/
		}
		#endregion

	}
}