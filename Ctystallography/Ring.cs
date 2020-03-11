using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Crystallography
{
	public class IntegralProperty
	{
		//���w�n�S�̂Ɋ֌W����Ƃ���
		/// <summary>
		/// �����̔g��
		/// </summary>
		public double WaveLength;

		public WaveProperty WaveProperty;

		/// <summary>
		/// �J������ (�T���v������_�C���N�g�X�|�b�g�܂ł̋���)
		/// </summary>
		public double FilmDistance;//�J������

		public enum CameraEnum { FlatPanel, Gandolfi }

		public CameraEnum Camera { get; set; } = CameraEnum.FlatPanel;

		//IP�̐����Ɋւ���Ƃ���

		/// <summary>
		/// �\�[�X�摜�̕�
		/// </summary>
		public int SrcWidth;

		/// <summary>
		/// �\�[�X�摜�̍���
		/// </summary>
		public int SrcHeight;

		/// <summary>
		/// �Z���^�[��x�ʒu
		/// </summary>
		public double CenterX;

		/// <summary>
		/// �Z���^�[��y�ʒu
		/// </summary>
		public double CenterY;

		/// <summary>
		/// �s�N�Z���T�C�Y
		/// </summary>
		public double PixSizeX, PixSizeY;

		/// <summary>
		/// �s�N�Z���̘c�݊p�x
		/// </summary>
		public double ksi = 0;

		/// <summary>
		/// IP�̉�]�p�x
		/// </summary>
		public double phi;

		/// <summary>
		/// IP�̉�]���̊p�x;
		/// </summary>
		public double tau;

		public double GandolfiRadius { get; set; }

		/// <summary>
		/// �p�ȃp�����[�^
		/// </summary>
		public double SpericalRadiusInverse { get; set; } = 0;

		//�ꎟ�����̕��@

		/// <summary>
		/// true�̏ꍇ�̓R���Z���g���b�N���[�h�@false�̏ꍇ�̓��f�B�A�����[�h
		/// </summary>
		public bool ConcentricMode;

		/// <summary>
		/// �R���Z���g���b�N���[�h�̏ꍇ�Astart, end ,step���A���O�����[�h��d�l���[�h���B�@���f�B�A�����[�h�̏ꍇ, Radius��RadiusRange���A���O�����[�h���p�x���[�h��
		/// </summary>
		public HorizontalAxis Mode;

		/// <summary>
		/// �R���Z���g���b�N�A���f�B�A�����[�h�����ŁA�ϕ��ΏۂƂȂ�p�x�͈͂�\�����邩�ǂ���
		/// </summary>

		public double StepAngle, StartAngle, EndAngle;
		public double StepLength, StartLength, EndLength;
		public double StepDspacing, StartDspacing, EndDspacing;

		public double RadialSectorAngle;
		public double RadialRadiusAngle, RadialRadiusAngleRange;
		public double RadialRadiusDspacing, RadialRadiusDspacingRange;

		/// <summary>
		/// true�̏ꍇ�͋�`���[�h false�̏ꍇ�̓Z�N�^�[���[�h
		/// </summary>
		public bool IsRectangle;

		public int ThresholdMax, ThresholdMin;//臒l

		public int Edge;
		public bool DoesExcludeEdge;

		public bool IsTiltCorrection;

		public bool IsBraggBrentanoMode;//�u���b�O�u�����^�[�m���[�h�ŏo�͂��邩�ǂ���

		//Rectangle���[�h�̂Ƃ�
		public bool IsFull;

		public double RectangleBand;//�o���h�̑���
		public double RectangleAngle;//�p�x
		public bool RectangleIsBothSide;//���������ǂ���

										//Sector���[�h�̂Ƃ�
		public double SectorStartAngle;//�J�n�p�x

		public double SectorEndAngle;//�I���p�x

		public IntegralProperty()
		{
		}
	}

	/// <summary>
	/// DebyeScherrer �̊T�v�̐����ł��B
	/// </summary>
	public static class Ring
	{
		public enum ImageTypeEnum
		{
			#region �摜�^�C�v

			Unknown,
			Rigaku_RAxis_IV,
			Rigaku_RAxis_V,
			Fuji_BAS2000,
			Fuji_BAS2500,
			Brucker_CCD,
			Fuji_FDL,
			IPAImage,
			ITEX, //���g�͒P�Ȃ�tiff
			RayonixSX200,//
			FLA7000, //GE�w���X�P�A��gel�`�� (tiff)
			Rigaku_RAxis_IV_Osc,//RAxisIV�̗h��
			Tiff,//Tiff

			/// <summary>
			/// Marresearch��MAR�t�@�C��
			/// </summary>
			MAR,

			/// <summary>
			/// Marresearch��CCD
			/// </summary>
			MCCD,

			/// <summary>
			/// Perkin Elmer�Ђ̃t���b�g�p�l��
			/// </summary>
			HIS,

			/// <summary>
			/// HDF5�`�� (SACLA��bl3)
			/// </summary>
			HDF5,

			/// <summary>
			/// DigitalMicrograph
			/// </summary>
			DM,

			/// <summary>
			/// ADSC��
			/// </summary>
			ADSC,

			/// <summary>
			/// RadIcon�� (�g���qraw)
			/// </summary>
			RadIcon,

			/// <summary>
			/// Dexela��, (�g���qSMV)
			/// </summary>
			SMV,

			#endregion �摜�^�C�v
		}

		//public static WaitDlg wd;
		public static Size SrcImgSize = new Size();

		public static double[] R2;
		public static List<double> Intensity = new List<double>();
		public static List<double> IntensityOriginal = new List<double>();
		public static Size SrcImgSizeOriginal = new Size();

		//�o�b�N�O���E���h���Z�Ŏg�p
		public static List<double> Background = new List<double>();

		public static double BackgroundCoeff = 1;

		//SequentialImage(*.his,*.h5)�ŗ��p����ϐ�
		public static List<List<double>> SequentialImageIntensities = new List<List<double>>();

		public static List<string> SequentialImageNames = new List<string>();

		//HDF *h5�t�@�C���ł݂̂Ɏg�p����
		public static List<double> SequentialImageEnergy = new List<double>();

		public static List<double> SequentialImagePulsePower = new List<double>();
		public static bool PulsePowerNormarized = false;
		public static List<List<double>> SequentialImageEnergySpectrum = new List<List<double>>();

		public static SortedList<uint, int> Frequency = new SortedList<uint, int>();

		public static ImageTypeEnum ImageType = ImageTypeEnum.Unknown;

		public static List<bool> IsValid = new List<bool>();//�L����(�}�X�N����Ă��Ȃ��_���ǂ���)
		public static List<bool> IsSpots = new List<bool>();//�X�|�b�g��̓_���ǂ���
		public static List<bool> IsThresholdOver = new List<bool>(), IsThresholdUnder = new List<bool>();//�O�a���Ă��邩�ǂ���

		/// <summary>
		/// �w�肳�ꂽ�ϕ��̈�(��`�A�Z�N�^�[)�͈̔͊O�̏ꍇ��true
		/// </summary>
		public static List<bool> IsOutsideOfIntegralRegion = new List<bool>();//�ϕ��G���A�̊O(�����͑I��̈�̊O)

		/// <summary>
		/// �w�肳�ꂽ�ϕ��Ώۊp�x�͈̔͊O�͈̔͊O�̏ꍇ��true
		/// </summary>
		public static List<bool> IsOutsideOfIntegralProperty = new List<bool>();//�G���A�̊O(�����͑I��̈�̊O)

		public static string Comments = "";

		//  public static double ScaleFactor = 1; //���x�ɏ悸����q (����X�����x�Ńm�[�}���C�Y����ꍇ)

		public static bool[] IsCalcPosition;

		public static Rotation ChiRotation = Rotation.Clockwise;
		public static Direction ChiDirection = Direction.Right;

		public static int xMax, xMin;
		public static int[] yThreadMin;
		public static int[] yThreadMax;
		public static IntegralProperty IP;

		/*
		/// <summary>
		/// 1�s�N�Z����������������(���邢�͋t���)�̒��� (�P�ʂ� PixelUnit�Ŏw��)
		/// </summary>
		public static double PixelScale;
		/// <summary>
		/// 1�s�N�Z����������������(���邢�͋t���)�̒P��
		/// </summary>
		public static PixelUnitEnum PixelUnit;
		*/

		public static DigitalMicrograph.Property DigitalMicrographProperty;

		/// <summary>
		/// ���f�[�^�̃s�N�Z��������̃r�b�g��
		/// </summary>
		public static int BitsPerPixels = 4;

		public static int ThreadTotal = System.Environment.ProcessorCount;

		private static ParallelOptions p = new ParallelOptions();

		static Ring()
		{
			p.MaxDegreeOfParallelism = Environment.ProcessorCount;
		}

		//Frequency���v�Z
		public static void CalcFreq()
		{
			if (Intensity == null || Intensity.Count == 0) return;
			Frequency.Clear();
			double unit = 1.2;

			Parallel.For(0, p.MaxDegreeOfParallelism, p, i =>
			{
				int start = Intensity.Count / ThreadTotal * i;
				int end = Math.Min(Intensity.Count / ThreadTotal * (i + 1), Intensity.Count);
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

		private static double TanKsi, SinTau, CosTau, SinPhi, CosPhi, Numer1, Numer2, Numer3, Denom1, Denom2;

		//�X���␳�W�����v�Z
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

		//�s�N�Z���X�e�b�v�̕��ϒl�ƕW���΍������Ƃ߂ăX�|�b�g�����o����
		public static void FindSpots(IntegralProperty iP, double DeviationFactor)
		{
			int i;

			IP = iP;
			int w = iP.SrcWidth;
			int h = iP.SrcHeight;

			//�e�X���b�h�̏���Ɖ��������߂�
			yThreadMin = new int[ThreadTotal];
			yThreadMax = new int[ThreadTotal];
			int yStep = IP.SrcHeight / ThreadTotal;
			for (i = 0; i < ThreadTotal; i++)
			{
				yThreadMin[i] = i * yStep;
				yThreadMax[i] = (i + 1) * yStep;
			}
			yThreadMax[ThreadTotal - 1] = IP.SrcHeight;

			//�܂��ǂ̃s�N�Z�����ǂ̃X�e�b�v�ɑ����邩�����߂�
			var r = new int[IP.SrcHeight * IP.SrcWidth];
			var rMax = int.MinValue;
			var tempRMax = new int[ThreadTotal];
			SetTiltParameter();

			FindSpotsThread0Delegate[] d0 = new FindSpotsThread0Delegate[ThreadTotal];
			IAsyncResult[] ar0 = new IAsyncResult[ThreadTotal];
			for (i = 0; i < ThreadTotal; i++)
				d0[i] = new FindSpotsThread0Delegate(FindSpotsThread0);
			for (i = 0; i < ThreadTotal; i++)
				ar0[i] = d0[i].BeginInvoke(yThreadMin[i], yThreadMax[i], ref r, ref tempRMax[i], null, null);//�e�X���b�h�N���]��
			for (i = 0; i < ThreadTotal; i++)//�X���b�h�I���҂�
				d0[i].EndInvoke(ref r, ref tempRMax[i], ar0[i]);

			//rMax�̍ő�l�����߂�
			rMax = Statistics.Max(tempRMax);

			if (rMax == 0)
				return;

			rMax++;

			//Profile(�e�X�e�b�v���Ƃ̋��x)��Pixels(�e�X�e�b�v�Ɋ�^�����s�N�Z����)���쐬
			double[][] tempSumOfIntensity = new double[ThreadTotal][];
			double[][] tempSumOfIntensitySquare = new double[ThreadTotal][];
			double[][] tempContributedPixels = new double[ThreadTotal][];
			for (i = 0; i < ThreadTotal; i++)
			{
				tempSumOfIntensity[i] = new double[rMax];
				tempSumOfIntensitySquare[i] = new double[rMax];
				tempContributedPixels[i] = new double[rMax];
			}
			//��������X���b�h1�N��
			FindSpotsThread1Delegate[] d1 = new FindSpotsThread1Delegate[ThreadTotal];
			IAsyncResult[] ar1 = new IAsyncResult[ThreadTotal];
			for (i = 0; i < ThreadTotal; i++)
				d1[i] = new FindSpotsThread1Delegate(FindSpotsThread1);
			for (i = 0; i < ThreadTotal; i++)
				ar1[i] = d1[i].BeginInvoke(yThreadMin[i], yThreadMax[i], r, ref tempSumOfIntensity[i], ref tempSumOfIntensitySquare[i], ref tempContributedPixels[i], null, null);//�e�X���b�h�N���]��
			for (i = 0; i < ThreadTotal; i++)//�X���b�h�I���҂�
				d1[i].EndInvoke(ref tempSumOfIntensity[i], ref tempSumOfIntensitySquare[i], ref tempContributedPixels[i], ar1[i]);

			//Thread1�̌��ʂ��܂Ƃ߂�
			double[] ContributedPixels = new double[rMax];
			double[] SumOfIntensity = new double[rMax];
			double[] SumOfIntensitySquare = new double[rMax];
			for (i = 0; i < rMax; i++)
				for (int t = 0; t < ThreadTotal; t++)
				{
					SumOfIntensity[i] += tempSumOfIntensity[t][i];
					SumOfIntensitySquare[i] += tempSumOfIntensitySquare[t][i];
					ContributedPixels[i] += tempContributedPixels[t][i];
				}

			//�W���΍������Ƃ߂�
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

		private delegate void FindSpotsThread0Delegate(int yMin, int yMax, ref int[] r, ref int rMax);

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

		/// <summary>
		/// �摜���A���]�A��]������. rotate��0: ����]�A1:90�x��], 2: 180�x��]�A 3: 270�x��]
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

		public static List<double> CorrectPolarization(int rotate)
		{
			SetTiltParameter();

			/*
			var test = new double[3000 * 3000];
			int width = 3000, height = 3000;
			double cX = width / 2.0, cY = height / 2.0;
			var pixSize = 0.1;
			var camera = 150;
			for (var h = 0; h < height; h++)
				for (var w = 0; w < width; w++)
				{
					double x = (w - cX) * pixSize, y = (h - cY) * pixSize;
					double sinKai2 = y * y / (x * x + y * y), cosKai2 = x * x / (x * x + y * y);
					double cos2th2 = camera * camera / (camera * camera + x * x + y * y);
					//test[h * width + w] = 1000 * (sinKai2 + cosKai2 * cos2th2);
					test[h * width + w] = 1000 * 0.5 * (1 +  cos2th2);
				}
			Tiff.Writer(@"d:\testUnpolarized.tif", test, 3, width);
			*/

			double fd = IP.FilmDistance, fd2 = fd * fd;
			double sizeX = IP.PixSizeX, sizeY = IP.PixSizeY;
			double centX = IP.CenterX, centY = IP.CenterY;

			//�␳���́AIcorr = I / (sin(kai)^2 + cos(kai)^2 * cos(2th)^2 )
			// sin(kai)^2 = y^2 /  (x^2 + y^2)
			// cos(2th)^2 = fd^2 / (x^2 + y^2 + fd^2)
			// cos(kai)^2 = x^2 / (x^2 + y^2)
			//�܂Ƃ߂�ƕ���̕����́A 1 - x2 / (x2 + y2 + fd2))
			//var coeff1 = rotate == 0 || rotate == 2 ?
			//    new Func<double, double, double>((x2, y2) => (y2+fd2)  / (x2 + y2 + fd2)) :
			//    new Func<double, double, double>((x2, y2) => (x2+fd2) / (x2 + y2 + fd2));

			//20190906�ǋL
			//�␳���́AIcorr = I / (sin(kai)^2 + cos(kai)^2 * cos(2th)^2 ) / cos(2th)
			
			var coeff1 = rotate == 0 || rotate == 2 ?
				new Func<double, double, double>((x2, y2) => 2 * (y2 + fd2) / (x2 + y2 + 2 * fd2)) :
				new Func<double, double, double>((x2, y2) => 2 * (x2 + fd2) / (x2 + y2 + 2 * fd2));

			//var coeff2 = new Func<double, double, double>((x2, y2) => Math.Sqrt( fd2 / (x2 + y2 + fd2)));

			var result = new double[Intensity.Count];
			//Parallel.For���g��Ȃ��ق�������
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

		/// <summary>
		/// �X�|�b�g��臒l���̃s�N�Z�����}�X�N����֐�
		/// ���̃��\�b�h�̑O�ɓK�؂�IsInsideArea���ݒ肳��Ă���K�v������B
		/// </summary>
		/// <param name="OmitSpots"></param>
		/// <param name="OmitTheresholdMin"></param>
		/// <param name="OmitTheresholdMax"></param>
		public static void SetMask(bool OmitSpots, bool OmitTheresholdMin, bool OmitTheresholdMax)
		{
			if (Ring.IsValid.Count != IsOutsideOfIntegralRegion.Count)
				return;
			IsValid.Clear();
			for (int i = 0; i < IsOutsideOfIntegralRegion.Count; i++)
				if (IsOutsideOfIntegralRegion[i])
					IsValid.Add(false);
				else if (IsOutsideOfIntegralProperty[i])
					IsValid.Add(false);
				else
					IsValid.Add(true);

			if (OmitSpots)
				Parallel.For(0, IP.SrcHeight * IP.SrcWidth, i =>
					{ if (IsSpots[i]) IsValid[i] = false; });
			if (OmitTheresholdMin)
				Parallel.For(0, IP.SrcHeight * IP.SrcWidth, i =>
					{ if (IsThresholdUnder[i]) IsValid[i] = false; });
			if (OmitTheresholdMax)
				Parallel.For(0, IP.SrcHeight * IP.SrcWidth, i =>
					{ if (IsThresholdOver[i]) IsValid[i] = false; });
		}

		public static void SetInsideArea(IntegralProperty IP)
		{
			SetInsideArea(IP, true, true, true);
		}

		/// <summary>
		/// �ϕ��̈�ȊO���}�X�N����֐��@�}�X�N����̂́A���̎O�_
		/// �E�w�肵����`���邢�̓Z�N�^�[�O�̗̈�@
		/// �E�G�b�W�̈�@
		/// �E�ϕ��p�x�͈͂Ɋ܂܂�Ȃ��̈�
		/// </summary>
		/// <param name="IP">IP�̃v���p�e�B</param>
		/// <param name="calcRegion">�w�肵����`���邢�̓Z�N�^�[�O�̗̈���v�Z���邩�ǂ���</param>
		/// <param name="calcEdge">�G�b�W�̈���v�Z���邩�ǂ���</param>
		/// <param name="calcProperty">�ϕ��p�x�͈͂Ɋ܂܂�Ȃ��̈���v�Z���邩�ǂ���</param>
		public static void SetInsideArea(IntegralProperty IP, bool calcRegion, bool calcEdge, bool calcProperty)
		{
			if (calcRegion)
			{
				bool flag = IP.IsRectangle && IP.IsFull ? false : true;
				if (flag == false)
					IsOutsideOfIntegralRegion = new List<bool>(new bool[IsOutsideOfIntegralRegion.Count]);
				if (IsOutsideOfIntegralRegion.Count == IP.SrcWidth * IP.SrcHeight)
				{
					for (int n = 0; n < IP.SrcWidth * IP.SrcHeight; n++)
					{
						if (IsOutsideOfIntegralRegion[n] != flag)
							IsOutsideOfIntegralRegion[n] = flag;
					}
				}
				else
				{
					IsOutsideOfIntegralRegion.Clear();
					for (int n = 0; n < IP.SrcWidth * IP.SrcHeight; n++)
						IsOutsideOfIntegralRegion.Add(flag);
				}
				if (flag)
				{
					int Height = IP.SrcHeight;
					int Width = IP.SrcWidth;

					var CenterX = IP.CenterX;
					var CenterY = IP.CenterY;
					var Band = IP.RectangleBand;
					if (IP.IsRectangle)

					#region Rectangle���[�h�̂Ƃ�

					{//
						bool IsXY = false;
						double tan = Math.Tan(IP.RectangleAngle);
						double sin = Math.Sin(IP.RectangleAngle);
						double cos = Math.Cos(IP.RectangleAngle);
						double wx = Math.Abs(Band / sin);
						double wy = Math.Abs(IP.RectangleBand / cos);
						double cx, cy;
						if (Math.Abs(tan) > 1)
							IsXY = true;//�c�����ɋ߂��ꍇ��True
						int jWidth;
						int startI, endI, midI;
						int startJ, endJ, midJ;
						if (IsXY)
						{
							double MinusCenterYPerTanPlusCenterX = -CenterY / tan + CenterX;

							if (IP.RectangleIsBothSide)
							{//IsXY��True�őS�������[�h
								for (int j = 0; j < Height; j++)
								{
									cx = j / tan + MinusCenterYPerTanPlusCenterX;
									startI = (int)(cx - wx + 0.5);
									if (startI < 0) startI = 0;
									endI = (int)(cx + wx + 1.5);
									if (endI > Width) endI = Width;
									jWidth = j * Width;
									for (int i = startI; i < endI; i++)//�o���h�̓����̂Ƃ�
										IsOutsideOfIntegralRegion[i + jWidth] = false;
								}
							}
							else
							{//IsXY��True�Ŕ��������[�h
								double CenterXPerTanPlusCenterY = CenterX / tan + CenterY;
								if (sin > 0)//���ɐL�т��������̂Ƃ���
								{
									startJ = (int)(CenterY - Band * Math.Abs(cos) + 0.5);//�X�^�[�g�n�_
									midJ = (int)(CenterY + Band * Math.Abs(cos) + 0.5);//���Ԓn�_
									for (int j = startJ; j < Height; j++)
									{
										cx = j / tan + MinusCenterYPerTanPlusCenterX;
										startI = (int)(cx - wx + 0.5);
										if (startI < 0) startI = 0;
										endI = (int)(cx + wx + 1.5);
										if (endI > Width) endI = Width;
										jWidth = j * Width;
										if (j > midJ)
											for (int i = startI; i < endI; i++)//�o���h�̓����̂Ƃ�
												IsOutsideOfIntegralRegion[i + jWidth] = false;
										else
											for (int i = startI; i < endI; i++)//�o���h�̓����̂Ƃ�
												if (j + i / tan > CenterXPerTanPlusCenterY)
													IsOutsideOfIntegralRegion[i + jWidth] = false;
									}
								}
								else//��ɐL�т��������̂Ƃ���
								{
									midJ = (int)(CenterY - Band * Math.Abs(cos) + 0.5);
									endJ = (int)(CenterY + Band * Math.Abs(cos) + 0.5);
									for (int j = 0; j < endJ; j++)
									{
										cx = j / tan + MinusCenterYPerTanPlusCenterX;
										startI = (int)(cx - wx + 0.5);
										if (startI < 0) startI = 0;
										endI = (int)(cx + wx + 1.5);
										if (endI > Width) endI = Width;
										jWidth = j * Width;
										if (j < midJ)
											for (int i = startI; i < endI; i++)//�o���h�̓����̂Ƃ�
												IsOutsideOfIntegralRegion[i + jWidth] = false;
										else
											for (int i = startI; i < endI; i++)//�o���h�̓����̂Ƃ�
												if (j + i / tan < CenterXPerTanPlusCenterY)
													IsOutsideOfIntegralRegion[i + jWidth] = false;
									}
								}
							}
						}
						else//IsXY��False
						{
							double CenterYMinusTanCenterX = CenterY - tan * CenterX;
							if (IP.RectangleIsBothSide)
							{
								for (int i = 0; i < Width; i++)
								{
									cy = tan * i + CenterYMinusTanCenterX;
									startJ = (int)(cy - wy + 0.5);
									if (startJ < 0) startJ = 0;
									endJ = (int)(cy + wy + 1.5);
									if (endJ > Height) endJ = Height;
									for (int j = startJ; j < endJ; j++)//�o���h�̓����̂Ƃ�
										IsOutsideOfIntegralRegion[i + j * Width] = false;
								}
							}
							else
							{//���������[�h�̂Ƃ�
								double CenterYTanPlusCenterX = CenterY * tan + CenterX;

								if (cos > 0)//�E�ɐL�т��������̂Ƃ���
								{
									startI = (int)(CenterX - Band * Math.Abs(sin) + 0.5);
									midI = (int)(CenterX + Band * Math.Abs(sin) + 0.5);
									for (int i = startI; i < Width; i++)
									{
										cy = tan * i + CenterYMinusTanCenterX;
										startJ = (int)(cy - wy + 0.5);
										if (startJ < 0) startJ = 0;
										endJ = (int)(cy + wy + 1.5);
										if (endJ > Height) endJ = Height;
										if (i > midI)
											for (int j = startJ; j < endJ; j++)//�o���h�̓����̂Ƃ�
												IsOutsideOfIntegralRegion[i + j * Width] = false;
										else
											for (int j = startJ; j < endJ; j++)
												if (i + j * tan > CenterYTanPlusCenterX)
													IsOutsideOfIntegralRegion[i + j * Width] = false;
									}
								}
								else//���ɐL�т��������̂Ƃ���
								{
									midI = (int)(CenterX - Band * Math.Abs(sin) + 0.5);
									endI = (int)(CenterX + Band * Math.Abs(sin) + 0.5);
									for (int i = 0; i < endI; i++)
									{
										cy = tan * i + CenterYMinusTanCenterX;
										startJ = (int)(cy - wy + 0.5);
										if (startJ < 0) startJ = 0;
										endJ = (int)(cy + wy + 1.5);
										if (endJ > Height) endJ = Height;

										if (i < midI)
											for (int j = startJ; j < endJ; j++)//�o���h�̓����̂Ƃ�
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

					#endregion Rectangle���[�h�̂Ƃ�

					else

					#region Sector���[�h�̂Ƃ�

					{
						//�J�C�p���l�����āAStartAngle, EndAngle��ݒ�
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

						//startAngle, endAngle�� -pi����pi�͈̔͂ɕϊ�����
						while (startAngle > Math.PI) startAngle -= 2 * Math.PI;
						while (startAngle < -Math.PI) startAngle += 2 * Math.PI;
						while (endAngle > Math.PI) endAngle -= 2 * Math.PI;
						while (endAngle < -Math.PI || endAngle < startAngle) endAngle += 2 * Math.PI;

						//�s�N�Z�����Wx,y�� ���ʍ��W�ɕϊ�����Func���`
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

					#endregion Sector���[�h�̂Ƃ�
				}
			}

			if (calcEdge)

			#region �G�b�W����������Ƃ�

			{
				if (IP.DoesExcludeEdge)

				{
					int n = IP.Edge;
					//���
					for (int i = 0; i < Ring.IP.SrcHeight && i < n; i++)
						for (int j = 0; j < Ring.IP.SrcWidth; j++)
							Ring.IsOutsideOfIntegralRegion[i * Ring.IP.SrcWidth + j] = true;
					//����
					for (int i = Math.Max(0, Ring.IP.SrcHeight - n); i < Ring.IP.SrcHeight; i++)
						for (int j = 0; j < Ring.IP.SrcWidth; j++)
							Ring.IsOutsideOfIntegralRegion[i * Ring.IP.SrcWidth + j] = true;
					//����
					for (int j = 0; j < Ring.IP.SrcWidth && j < n; j++)
						for (int i = 0; i < Ring.IP.SrcHeight; i++)
							Ring.IsOutsideOfIntegralRegion[i * Ring.IP.SrcWidth + j] = true;
					//�E��
					for (int j = Math.Max(0, Ring.IP.SrcWidth - n); j < Ring.IP.SrcWidth; j++)
						for (int i = 0; i < Ring.IP.SrcHeight; i++)
							Ring.IsOutsideOfIntegralRegion[i * Ring.IP.SrcWidth + j] = true;
				}
			}

			#endregion �G�b�W����������Ƃ�

			if (calcProperty)

			#region �ϕ��p�x�͈͂���������Ƃ�

			{
				if (IsOutsideOfIntegralProperty.Count == IP.SrcWidth * IP.SrcHeight)
				{
					for (int n = 0; n < IP.SrcWidth * IP.SrcHeight; n++)
					{
						if (!IsOutsideOfIntegralProperty[n])
							IsOutsideOfIntegralProperty[n] = true;
					}
				}
				else
				{
					IsOutsideOfIntegralProperty.Clear();
					for (int n = 0; n < IP.SrcWidth * IP.SrcHeight; n++)
						IsOutsideOfIntegralProperty.Add(true);
				}

				//�t���b�g�p�l���J�����̏ꍇ
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

					//ThreadTotal = p.MaxDegreeOfParallelism = 1;
					int hUnit = IP.SrcHeight / ThreadTotal + 1;

					Parallel.For(0, ThreadTotal, p, k =>
					{
						for (int j = hUnit * k; j < Math.Min(hUnit * (k + 1), IP.SrcHeight); j++)
						{
							double tempY = (j - centerY) * pixSizeY;
							double tempY2TanKsi = tempY * TanKsi;
							double numer1TempY = Numer1 * tempY;
							double numer3TempY = Numer3 * tempY;
							double denom1tempYFD = Denom1 * tempY + fd;

							for (int i = 0; i < IP.SrcWidth; i++)
							{
								double tempX = (i - centerX) * pixSizeX + tempY2TanKsi;//IP���ʏ�̍��W�n�ɂ�����X�ʒu
								//�ȉ���x,y,z���s�N�Z�����S�̋�Ԉʒu
								double x = Numer2 * tempX + numer1TempY;
								double y = Numer1 * tempX + numer3TempY;
								double z = Denom2 * tempX + denom1tempYFD;
								double z2 = z * z;
								double l2 = x * x + y * y + z2;

								double cos2 = z2 / l2;
								if (z < 0) cos2 = -cos2;

								if (cos2 < startCos2 && cos2 > endCos2)
									IsOutsideOfIntegralProperty[i + j * IP.SrcWidth] = false;
							}
						}
					});
				}//�t���b�g�p�l�����[�h�����܂�
				 //Gandolfi���[�h�̎�
				else
				{
					SetTiltParameter();
					double centerX = IP.CenterX, centerY = IP.CenterY;
					double pixSizeX = IP.PixSizeX, pixSizeY = IP.PixSizeY;
					int width = IP.SrcWidth;
					double r = IP.GandolfiRadius;
					double CL = IP.FilmDistance;

					Matrix3D m1 = new Matrix3D(CosTau, SinTau * SinPhi, SinTau * CosPhi, 0, CosPhi, -SinPhi, -SinTau, CosTau * SinPhi, CosTau * CosPhi);

					//�s�N�Z�����W������ԍ��W�ɕϊ�����Func
					var convPixelToReal = new Func<double, double, Vector3DBase>((x, y) => m1 * new Vector3DBase(
									 Math.Sin((x - centerX) * pixSizeX / r) * r,
									 pixSizeY * (-y + centerY),
									 (Math.Cos((x - centerX) * pixSizeX / r) - 1) * r));
					//����ԍ��W��2���ɕϊ�����Func
					var convRealToCos = new Func<Vector3DBase, double>((v) => (v.Z + CL) / Math.Sqrt(v.X * v.X + v.Y * v.Y + (v.Z + CL) * (v.Z + CL)));

					double maxCos = Math.Cos(IP.StartAngle);
					double minCos = Math.Cos(IP.EndAngle);

					//�p�x�X�e�b�v�̋�؂�ʒu��ݒ肷��
					Parallel.For(0, p.MaxDegreeOfParallelism, p, i =>
					{
						int hUnit = IP.SrcHeight / p.MaxDegreeOfParallelism;
						for (int y = hUnit * i; y < Math.Min(hUnit * (i + 1), IP.SrcHeight); y++)
							for (int x = 0; x < IP.SrcWidth; x++)
							{
								var cos = convRealToCos(convPixelToReal(x, y));
								if (cos > minCos && cos < maxCos)
									IsOutsideOfIntegralProperty[x + y * IP.SrcWidth] = false;
							}
					});
				}
			}

			#endregion �ϕ��p�x�͈͂���������Ƃ�
		}

		private delegate void SetInsideArea2Delegate(int xMin, int xMax, int yMin, int yMax);

		/// <summary>
		/// �~�������ɂɂ��܂����摜���쐬(���S�ɘc��ł��Ȃ������`�s�N�Z��������)
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

		/// <summary>
		/// �X���␳��s�N�Z���␳���������Đ��m�ȃC���[�W�����o�����\�b�h
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
					//4���̓_���v�Z
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
						//���߂�4���̓_����A��������\���̂���X,Y�s�N�Z���̏���A���������߂�
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

								//�ʐϔ�����߂ċ��x������U��
								double currentArea = Geometriy.GetPolygonalArea(tempPixelVertex);
								double ratio = currentArea / totalArea;
								pixels[pixY * size.Width + pixX] += ratio * intensity;
							}
					}
				}
			}

			return pixels;
		}

		/// <summary>
		/// �ɍ��W�ɕϊ� �p�x�̓Έȏ� 3�Έȉ�
		/// </summary>
		/// <param name="pt">�������W</param>
		/// <returns>PointD, X:�p�x, Y: ����</returns>
		public static PointD RectangularToPolarCordinate(PointD pt)
		{
			return new PointD(Math.Atan2(pt.Y, pt.X) + Math.PI * 2, Math.Sqrt(pt.X * pt.X + pt.Y * pt.Y));
		}

		public static PointD RotateFlip(PointD pt)
		{
			if (ChiRotation == Rotation.Clockwise)
			{
				if (ChiDirection == Direction.Right)
					return new PointD(pt.X, -pt.Y);
				else if (ChiDirection == Direction.Bottom)
					return new PointD(pt.Y, pt.X);
				else if (ChiDirection == Direction.Left)
					return new PointD(-pt.X, pt.Y);
				else
					return new PointD(-pt.Y, -pt.X);
			}
			else
			{
				if (ChiDirection == Direction.Right)
					return new PointD(pt.X, pt.Y);
				else if (ChiDirection == Direction.Bottom)
					return new PointD(pt.Y, -pt.X);
				else if (ChiDirection == Direction.Left)
					return new PointD(-pt.X, -pt.Y);
				else
					return new PointD(-pt.Y, pt.X);
			}
		}

		public enum Rotation { Clockwise, Counterclockwise }

		public enum Direction { Right, Left, Top, Bottom }

		private static readonly object lockObj = new object();

		/// <summary>
		/// �X���␳��s�N�Z���␳���������Đ؂�J���摜�����o�����\�b�h
		/// </summary>
		/// <param name="iP"></param>
		/// <param name="sweepDivision"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static double[] GetUnrolledImageArray(IntegralProperty iP, double sectorStep, double startTheta, double endTheta, double stepTheta)
		{
			int height = (int)(2 * Math.PI / sectorStep) * 2;
			int width = (int)((endTheta - startTheta) / stepTheta);
			IP = iP;

			int thread = 8;

			int yStep = IP.SrcHeight / thread;

			double[] pixels = new double[height * width];
			for (int i = 0; i < pixels.Length; i++)
				pixels[i] = 0;

			Parallel.For(0, thread, i =>
			{
				double[] temp;
				if (IP.Camera == IntegralProperty.CameraEnum.FlatPanel)//�t���b�g�p�l�����[�h�̎�
				   temp = GetUnrolledImageArrayThread(iP, width, height, i * yStep, Math.Min((i + 1) * yStep, iP.SrcHeight), sectorStep, startTheta, endTheta, stepTheta);
				else
					temp = GetUnrolledImageArrayGandlofi(iP, i * yStep, Math.Min((i + 1) * yStep, iP.SrcHeight), sectorStep, startTheta, endTheta, stepTheta);
				lock (lockObj)
				{
					for (int j = 0; j < temp.Length; j++)
						pixels[j] += temp[j];
				}
			});

			for (int h = 0; h < height / 4 + 1; h++)
				for (int w = 0; w < width; w++)
					pixels[h * width + w] += pixels[(h + height / 2) * width + w];

			double[] destPixels = new double[pixels.Length / 2];
			Array.Copy(pixels, destPixels, pixels.Length / 2);

			double baseTheta = Math.Atan(height / 2 * (IP.PixSizeX + IP.PixSizeY) / 2 / Math.PI / 2 / IP.FilmDistance);

			//if (IP.Camera == IntegralProperty.CameraEnum.FlatPanel)
			{
				for (int w = 0; w < width; w++)
				{
					//�����O�̒�����␳
					double hRatio = Math.Tan(baseTheta) / Math.Tan(startTheta + w * stepTheta);
					//���s�N�Z�����󂯎��p�x�͈͂����摜�ł̃s�N�Z���Ɋ��Z���A�␳
					double wRatio = (IP.PixSizeX + IP.PixSizeY) / 2 / IP.FilmDistance / (Math.Tan(startTheta + (w + 1) * stepTheta) - Math.Tan(startTheta + w * stepTheta));
					//BB�Ɉ�v����悤��1/cos(2theta)��������
					double bbCorection = 1 / Math.Cos(startTheta + w * stepTheta);
					double ratio = hRatio * wRatio * bbCorection;
					if (!double.IsNaN(ratio))
						for (int h = 0; h < height / 2; h++)
							destPixels[h * width + w] *= ratio;
				}
			}

			return destPixels;
		}

		private static double[] GetUnrolledImageArrayGandlofi(IntegralProperty iP, int yMin, int yMax, double sectorStep, double startTheta, double endTheta, double stepTheta)
		{
			int unrolledImageHeight = (int)(2 * Math.PI / sectorStep) * 2;
			int unrolledImageWidth = (int)((endTheta - startTheta) / stepTheta);
			int width = iP.SrcWidth;
			double centerX = IP.CenterX, centerY = IP.CenterY;
			double pixSizeX = IP.PixSizeX, pixSizeY = IP.PixSizeY;
			double r = IP.GandolfiRadius;
			double CL = IP.FilmDistance;
			Matrix3D m1 = new Matrix3D(CosTau, SinTau * SinPhi, SinTau * CosPhi, 0, CosPhi, -SinPhi, -SinTau, CosTau * SinPhi, CosTau * CosPhi);
			//�s�N�Z�����W������ԍ��W�ɕϊ�����Func
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

			//����������̂��߂̌W��
			double c = 1000;

			//����ԍ��W���A�ɍ��W�n (���Ȃ킿�A2�Ƃƃ�)�ɕϊ�
			var Vector3DToPolar = new Func<Vector3DBase, PointD>((v) => new PointD(Math.Acos((v.Z + CL) / Math.Sqrt(v.X * v.X + v.Y * v.Y + (v.Z + CL) * (v.Z + CL))) * c, Math.Atan2(v.Y, v.X) + Math.PI * 2));

			//�s�N�Z�����W=>����Ԃ̕ϊ��ƁA����ԁ���2���̕ϊ����ɂ���Ă���
			var realVertex = new Vector3DBase[(width + 1) * (yMax - yMin + 1)];
			for (int y = yMin; y < yMax + 1; y++)
				for (int x = 0; x < width + 1; x++)
					realVertex[x + (y - yMin) * (width + 1)] = convPixelToReal(x - 0.5, y - 0.5);

			double[] pixels = new double[unrolledImageWidth * unrolledImageHeight];
			for (int i = 0; i < pixels.Length; i++)
				pixels[i] = 0;

			var pixelVertexPolar = new PointD[4];//�ɍ��W

			for (int y = yMin; y < yMax; y++)
			{
				for (int x = 0; x < IP.SrcWidth; x++)
				{
					//�l���̍��W�����߂�
					var pixelVertex = new[] {
							realVertex[x - xMin + (y - yMin) * (width + 1)],

							realVertex[x - xMin + 1 + (y - yMin) * (width + 1)],
						realVertex[x - xMin + 1 + (y - yMin + 1) * (width + 1)],
							realVertex[x - xMin + (y - yMin + 1) * (width + 1)]
						};
					for (int i = 0; i < 4; i++)
						pixelVertexPolar[i] = Vector3DToPolar(pixelVertex[i]);//�ɍ��W�ɕϊ�

					double max = pixelVertexPolar.Max(p => p.Y), min = pixelVertexPolar.Min(p => p.Y);

					if (max - min > Math.PI)
						for (int i = 0; i < 4; i++)
							if (max - pixelVertexPolar[i].Y > Math.PI)
								pixelVertexPolar[i].Y += Math.PI * 2;

					int maxX = int.MinValue, minX = int.MaxValue, maxY = int.MinValue, minY = int.MaxValue;
					foreach (PointD pt in pixelVertexPolar) //���߂�4���̓_����A��������\���̂���t,r�s�N�Z���̏���A���������߂�
					{
						//������pixY�Ɋ܂܂��͈͂� (pixY-0.5)*sweepStep ����(pixY+0.5)*sweepStep�܂�
						int pixY = (int)Math.Floor(pt.Y / sectorStep + 0.5);
						minY = Math.Min(minY, pixY - 0);
						maxY = Math.Max(maxY, pixY + 0);

						//������pixX�Ɋ܂܂��͈͂� (pixX-0.5)*thetaStep ����(pixX+0.5)*thetaStep�܂�
						int pixX = (int)Math.Floor((pt.X / c - startTheta) / stepTheta + 0.5);
						minX = Math.Min(minX, pixX - 0);
						maxX = Math.Max(maxX, pixX + 0);
					}

					if (minX >= 0 && maxX < unrolledImageWidth && minY >= 0 && maxY < unrolledImageHeight)
					{
						double intensity = Intensity[x + y * width];

						double totalArea = Geometriy.GetPolygonalArea(pixelVertexPolar);

						if (minX == maxX && minY == maxY)//�����ۂ���܂����ꍇ�́A
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
									//�܂�pixY(���Ȃ킿�Ԋp)�Œ�`�������ˏ��2�{�̒����̒�`
									pixelVertexPolarTemp1 = Geometriy.GetPolygonDividedByLine(pixelVertexPolar, 0, 1, (pixY - 0.5) * sectorStep);
									pixelVertexPolarTemp1 = Geometriy.GetPolygonDividedByLine(pixelVertexPolarTemp1, 0, -1, -(pixY + 0.5) * sectorStep);
								}

								if (pixelVertexPolarTemp1.Length > 2)
								{
									if (maxX == minX)
										pixels[pixY * unrolledImageWidth + maxX] += Geometriy.GetPolygonalArea(pixelVertexPolarTemp1) / totalArea * intensity;
									else
									{
										//�����2�Ɣ͈͂̏���A������\�����S�~���2�����̒�`

										PointD[] pixelVertexPolarTemp2;
										for (int pixX = Math.Max(0, minX); pixX <= Math.Min(unrolledImageWidth - 1, maxX); pixX++)
										{
											double maxR = ((pixX + 0.5) * stepTheta + startTheta) * c;
											double minR = ((pixX - 0.5) * stepTheta + startTheta) * c;
											pixelVertexPolarTemp2 = Geometriy.GetPolygonDividedByLine(pixelVertexPolarTemp1, 1, 0, minR);
											pixelVertexPolarTemp2 = Geometriy.GetPolygonDividedByLine(pixelVertexPolarTemp2, -1, 0, -maxR);

											//�ʐϔ�����߂ċ��x������U��
											pixels[pixY * unrolledImageWidth + pixX] += Geometriy.GetPolygonalArea(pixelVertexPolarTemp2) / totalArea * intensity;
										}
									}
								}
							}
						}
					}
				}
			}
			return pixels;
		}

		/// <summary>
		/// �X���␳��s�N�Z���␳���������Đ؂�J���摜�����o�����\�b�h�̃X���b�h
		/// </summary>
		/// <param name="iP"></param>
		/// <param name="sweepDivision"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		private static double[] GetUnrolledImageArrayThread(IntegralProperty iP, int width, int height, int startH, int endH, double sectorStep, double startTheta, double endTheta, double stepTheta)
		{
			double[] pixels = new double[height * width];
			for (int i = 0; i < pixels.Length; i++)
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
					//4���̓_���v�Z
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
					foreach (PointD pt in pixelVertexPolar) //���߂�4���̓_����A��������\���̂���t,r�s�N�Z���̏���A���������߂�
					{
						//������pixY�Ɋ܂܂��͈͂� (pixY-0.5)*sweepStep ����(pixY+0.5)*sweepStep�܂�
						int pixY = (int)Math.Floor(pt.X / sectorStep + 0.5);
						minY = Math.Min(minY, pixY);
						maxY = Math.Max(maxY, pixY);

						//������pixX�Ɋ܂܂��͈͂� (pixX-0.5)*thetaStep ����(pixX+0.5)*thetaStep�܂�
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
									//�܂�pixX�Œ�`�������ˏ��2�{�̒����̒�`
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
										//�����2�Ɣ͈͂̏���A������\�����S�~���2�����̒�`
										a1 = Math.Cos(pixY * sectorStep);
										b1 = Math.Sin(pixY * sectorStep);

										PointD[] pixelVertex2;
										double beforeArea = 0;
										for (int pixX = Math.Max(0, minX); pixX <= Math.Min(width - 1, maxX); pixX++)
										{
											double maxR = Math.Tan((pixX + 0.5) * stepTheta + startTheta) * IP.FilmDistance;
											pixelVertex2 = Geometriy.GetPolygonDividedByLine(pixelVertex1, -a1, -b1, -maxR);

											//�ʐϔ�����߂ċ��x������U��
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
			return pixels;
		}

		public static Profile GetProfile(IntegralProperty iP)
		{
			if (iP.ConcentricMode)
				return GetConcenrticProfile(iP, true);
			else
				return GetRadialProfile(iP);
		}

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
			//Profile(�e�X�e�b�v���Ƃ̋��x)��Pixels(�e�X�e�b�v�Ɋ�^�����s�N�Z����)���쐬
			int length = (int)(360.0 / iP.RadialSectorAngle + 0.5);
			//�̂肵�땔������邽�߂�2�{�̔z����m��
			double[] tempProfileIntensity = new double[length * 2];
			//���̂Ƃ��AtempProfileIntensity[i]�� (i-0.5)*step����(i+0.5)*step�̊p�x�͈͓��̋��x���Ӗ�����
			for (int i = 0; i < tempProfileIntensity.Length; i++)
				tempProfileIntensity[i] = 0;

			for (int j = 0; j < IP.SrcHeight; j++)
			{
				for (int i = 0; i < IP.SrcWidth; i++)
					if (IsValid[j * IP.SrcWidth + i])
					{
						double intensity = Intensity[j * IP.SrcWidth + i];

						//4���̓_���v�Z
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
						PointD temp = pixelVertex[2]; pixelVertex[2] = pixelVertex[3]; pixelVertex[3] = temp;//���v���ɂȂ�悤��3�߂�4�߂����ւ���
						double totalArea = Geometriy.GetPolygonalArea(pixelVertex);

						//���߂�4���̓_����A�X�e�b�v�̎w���̏���A������ݒ�
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
							//�܂�index�Œ�`�������ˏ��2�{�̒����̒�`
							double a1 = -Math.Sin((index - 0.5) * step), b1 = Math.Cos((index - 0.5) * step);
							double a2 = Math.Sin((index + 0.5) * step), b2 = -Math.Cos((index + 0.5) * step);
							tempPixelVertex = Geometriy.GetPolygonDividedByLine(pixelVertex, a1, b1, 0);
							tempPixelVertex = Geometriy.GetPolygonDividedByLine(tempPixelVertex, a2, b2, 0);

							//����Ɋp�x�͈͂̏���A������\�����S�~���2����
							a1 = Math.Cos(index * step);
							b1 = Math.Sin(index * step);
							tempPixelVertex = Geometriy.GetPolygonDividedByLine(tempPixelVertex, a1, b1, minR);
							tempPixelVertex = Geometriy.GetPolygonDividedByLine(tempPixelVertex, -a1, -b1, -maxR);

							//�ʐϔ�����߂ċ��x������U��
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

		/// <summary>
		/// 2theta-intensity histogram
		/// </summary>
		/// <param name="iP"></param>
		/// <param name="DoesDlgShow"></param>
		/// <returns></returns>
		private static Profile GetConcenrticProfile(IntegralProperty iP, bool DoesDlgShow)
		{
			IP = iP;

			//�ϕ��̈�S�̂�x���,x����, y����Ay���������߂�
			int xMin = int.MaxValue, yMin = int.MaxValue, xMax = int.MinValue, yMax = int.MinValue;
			for (int i = 0; i < IsValid.Count; i++)
				if (IsValid[i])
				{
					xMin = Math.Min(i % IP.SrcWidth, xMin);
					yMin = Math.Min(i / IP.SrcWidth, yMin);
					xMax = Math.Max(i % IP.SrcWidth, xMax);
					yMax = Math.Max(i / IP.SrcWidth, yMax);
				}

			ThreadTotal = Environment.ProcessorCount;
#if DEBUG
			ThreadTotal=1;
#endif

			p.MaxDegreeOfParallelism = ThreadTotal;

			//�e�X���b�h�̏���Ɖ��������߂�
			int[] yThreadMin = new int[ThreadTotal];
			int[] yThreadMax = new int[ThreadTotal];
			int yStep = (yMax - yMin) / ThreadTotal;
			for (int i = 0; i < ThreadTotal; i++)
			{
				yThreadMin[i] = yMin + i * yStep;
				yThreadMax[i] = Math.Min(yMin + (i + 1) * yStep, yMax);
			}

			//�t���b�g�p�l�����[�h�̎�
			if (IP.Camera == IntegralProperty.CameraEnum.FlatPanel)
			{
				//�p�x�A�����Ad�l���[�h���ƂɁA�X�e�b�v�̋�؂�ʒu(���S����̋�����2��)��ݒ肷��
				if (IP.Mode == HorizontalAxis.Angle)
				{
					R2 = new double[(int)((IP.EndAngle - IP.StartAngle) / IP.StepAngle) + 1];
					for (int i = 0; i < R2.Length; i++)
					{
						//double temp = Math.Tan((i + 0.5) * IP.StepAngle + IP.StartAngle) * IP.FilmDistance;
						//R2[i] = temp * temp;

						//2016/12/27 �ύX
						double temp = (i + 0.5) * IP.StepAngle + IP.StartAngle;
						R2[i] = temp;
					}
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
				else //d-spacing mode�̂Ƃ�
				{
					R2 = new double[(int)((IP.EndDspacing - IP.StartDspacing) / IP.StepDspacing) + 1];
					for (int i = 0; i < R2.Length; i++)
					{
						double temp = Math.Tan(Math.Asin(IP.WaveLength / 2 / (IP.StartDspacing + (R2.Length - 1 - i - 0.5) * IP.StepDspacing)) * 2) * IP.FilmDistance;
						R2[i] = temp * temp;
					}
				}
			}
			//�K���h���t�B�[���[�h�̎�
			else
			{
				//�p�x�X�e�b�v�̋�؂�ʒu��ݒ肷��
				R2 = new double[(int)((IP.EndAngle - IP.StartAngle) / IP.StepAngle) + 1];
				for (int i = 0; i < R2.Length; i++)
					R2[i] = (i - 0.5) * IP.StepAngle + IP.StartAngle;
			}

			int length = R2.Length;

			//Profile(�e�X�e�b�v���Ƃ̋��x)��Pixels(�e�X�e�b�v�Ɋ�^�����s�N�Z����)���쐬
			double[][] tempProfileIntensity = new double[ThreadTotal][];
			double[][] tempContibutedPixels = new double[ThreadTotal][];
			for (int i = 0; i < ThreadTotal; i++)
			{
				tempProfileIntensity[i] = new double[length];
				tempContibutedPixels[i] = new double[length];
			}

			//�v�Z�����_�����߂�
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

			//�X���␳�p�p�����[�^���v�Z
			SetTiltParameter();

			double[] ProfileIntensity = new double[R2.Length];
			double[] ContributedPixels = new double[R2.Length];
			for (int i = 0; i < R2.Length; i++)
				ContributedPixels[i] = ProfileIntensity[i] = 0;

			//FlatPanel���[�h�̎�
			if (iP.Camera == IntegralProperty.CameraEnum.FlatPanel)
			{
				//��������X���b�h�N��
				Parallel.For(0, ThreadTotal, p, i =>
				{
					//GetProfileThreadWithTiltCorrection(xMin, xMax, yThreadMin[i], yThreadMax[i], ref tempProfileIntensity[i], ref tempContibutedPixels[i]);

					//2016/12/27 �ύX
					if (IP.Mode == HorizontalAxis.Angle)
						GetProfileThreadWithTiltCorrectionNew(xMin, xMax, yThreadMin[i], yThreadMax[i], ref tempProfileIntensity[i], ref tempContibutedPixels[i]);
					else
						GetProfileThreadWithTiltCorrection(xMin, xMax, yThreadMin[i], yThreadMax[i], ref tempProfileIntensity[i], ref tempContibutedPixels[i]);
				});
				Parallel.For(0, ThreadTotal, p, i =>
				{
					lock (lockObj)
					{
						for (int j = 0; j < length; j++)
						{
							ProfileIntensity[j] += tempProfileIntensity[i][j];
							ContributedPixels[j] += tempContibutedPixels[i][j];
						}
					}
				});
			}
			//Gandolfi���[�h�̎�
			else
			{
				int[] xThreadMin = new int[ThreadTotal], xThreadMax = new int[ThreadTotal];
				//�܂��AIsValid�ȃs�N�Z�������J�E���g
				int average = IsValid.Count(b => b) / ThreadTotal;
				int y = 0;
				for (int i = 0; i < ThreadTotal; i++)
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
						if (i != ThreadTotal - 1 && count > average)
						{
							y++;
							break;
						}
					}
				}

				Parallel.For(0, ThreadTotal, p, i =>
				{
					GetProfileGandlfi(xThreadMin[i], xThreadMax[i] + 1, yThreadMin[i], yThreadMax[i] + 1, ref tempProfileIntensity[i], ref tempContibutedPixels[i]);
					lock (lockObj)
					{
						for (int j = 0; j < length; j++)
						{
							ProfileIntensity[j] += tempProfileIntensity[i][j];
							ContributedPixels[j] += tempContibutedPixels[i][j];
						}
					}
				});
			}

			//Profile�ϐ��ɑ��
			Profile profile = new Profile();

			//�t���b�g�p�l�����[�h�̎�
			if (IP.Camera == IntegralProperty.CameraEnum.FlatPanel)
			{
				if (IP.Mode == HorizontalAxis.Angle)//�p�x���[�h�̂Ƃ�
					for (int i = 0; i < length; i++)
					{
						//double cosTwoTheta = Math.Cos(i * IP.StepAngle + IP.StartAngle);
						//double temp = ProfileIntensity[i] / ContributedPixels[i] / cosTwoTheta/ cosTwoTheta/ cosTwoTheta; //cos2����3��Ŋ��邱�Ƃɂ���āABB���w�n�ƈ�v������

						//20161227 �ύX
						double temp = ProfileIntensity[i] / ContributedPixels[i];
						if (double.IsNaN(temp) || double.IsInfinity(temp))
							temp = 0;
						double x = (i * IP.StepAngle + IP.StartAngle) / Math.PI * 180.0;
						profile.Pt.Add(new PointD(x, temp));
						profile.Err.Add(new PointD(x, temp / Math.Sqrt(ProfileIntensity[i])));
					}
				else if (IP.Mode == HorizontalAxis.Length)//Length���[�h�̂Ƃ�
					for (int i = 0; i < length; i++)
					{
						double temp = ProfileIntensity[i] / ContributedPixels[i];
						if (double.IsNaN(temp) || double.IsInfinity(temp))
							temp = 0;
						double x = i * IP.StepLength + IP.StartLength;
						profile.Pt.Add(new PointD(x, temp));
						profile.Err.Add(new PointD(x, temp / Math.Sqrt(ProfileIntensity[i])));
					}
				else //d�l�̃��[�h�̂Ƃ�
					for (int i = 0; i < length; i++)
					{
						double tempD = ((length - 1 - i) * IP.StepDspacing + IP.StartDspacing);
						double tempTwoTheta = Math.Asin(iP.WaveLength / 2 / tempD);
						double temp = ProfileIntensity[i] / ContributedPixels[i] / Math.Cos(tempTwoTheta);
						if (double.IsNaN(temp) || double.IsInfinity(temp))
							temp = 0;
						profile.Pt.Add(new PointD(tempD * 10, temp));
						profile.Pt.Add(new PointD(tempD * 10, temp / Math.Sqrt(ProfileIntensity[i])));
					}
			}
			//�K���h���t�B�[���[�h�̎�
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

		/// <summary>
		/// �K���h���t�B�[�J�����p�̌v�Z
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

			//�s�N�Z�����W������ԍ��W�ɕϊ�����Func
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
			//����ԍ��W��2���ɕϊ�����Func
			var convRealToTwoTheta = new Func<Vector3DBase, double>((v) => Math.Acos((v.Z + CL) / Math.Sqrt(v.X * v.X + v.Y * v.Y + (v.Z + CL) * (v.Z + CL))));

			//�p�x�X�e�b�v�̋�؂�ʒu��ݒ肷��
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

			//�s�N�Z�����W=>����Ԃ̕ϊ��ƁA����ԁ���2���̕ϊ����ɂ���Ă���
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
						// x, y�s�N�Z�����W������Ԃɕϊ�

						//�l����2�������߂�
						var twoTheta = new[] {
							twoThetaVertex[x - xMin + (y - yMin) * (xMax - xMin + 1)],
							twoThetaVertex[x - xMin + 1 + (y - yMin) * (xMax - xMin + 1)],
							twoThetaVertex[x - xMin + 1 + (y - yMin + 1) * (xMax - xMin + 1)],
							twoThetaVertex[x - xMin + (y - yMin + 1) * (xMax - xMin + 1)]
						};
						double maxTwoTheta = twoTheta.Max(), minTwoTheta = twoTheta.Min();

						if (maxTwoTheta > divisions[0] && minTwoTheta < divisions[length - 1])//�S��؂���Ƀs�N�Z����(�ꕔ�ł�)���܂��Ă���ꍇ�i���S�ɔ͈͊O�̎��͏��O�j
						{
							//�ŏ��C���f�b�N�X�ƍő�C���f�b�N�X�����߂�
							int minIndex = Math.Max(0, divisions.FindLastIndex(d => d < minTwoTheta));
							int maxIndex = Math.Min(length - 1, divisions.FindIndex(d => d > maxTwoTheta));

							//�ő�C���f�b�N�X�ƍŏ��C���f�b�N�X�̍���1�̏ꍇ(�s�N�Z���������ۂ��̋�؂���Ɏ��܂�Ƃ�)
							if (maxIndex - minIndex == 1 && divisions[minIndex] < minTwoTheta && divisions[maxIndex] > maxTwoTheta)
							{
								profile[minIndex] += intensity;
								pixels[minIndex] += pixSizeX * pixSizeY;
							}
							//�ő�C���f�b�N�X�ƍŏ��C���f�b�N�X�̍���2�ȏ�̏ꍇ(�s�N�Z������̋�؂���Ɏ��܂�Ȃ��Ƃ�)
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
								var p1 = (m2 * (v1 - v)).ToPointD();
								var p2 = (m2 * (v2 - v)).ToPointD();
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

		/// <summary>
		/// TiltCorrection�̎��ɂ̂݌Ă΂��
		/// </summary>
		/// <param name="iP"></param>
		/// <returns></returns>
		public static Profile GetProfileForFindTiltCorrection(IntegralProperty iP)
		{
			int i, j;
			//Profile(�e�X�e�b�v���Ƃ̋��x)��Pixels(�e�X�e�b�v�Ɋ�^�����s�N�Z����)���쐬
			int length = R2.Length;
			double[] ProfileIntensity = new double[length];
			double[] ContributedPixels = new double[length];

			double[][] tempProfileIntensity = new double[ThreadTotal][];
			double[][] tempContibutedPixels = new double[ThreadTotal][];
			for (i = 0; i < ThreadTotal; i++)
			{
				tempProfileIntensity[i] = new double[length];
				tempContibutedPixels[i] = new double[length];
			}

			//��������X���b�h�N��
			GetProfileThreadDelegateWithTiltCorrection[] d = new GetProfileThreadDelegateWithTiltCorrection[ThreadTotal];
			IAsyncResult[] ar = new IAsyncResult[ThreadTotal];
			for (i = 0; i < ThreadTotal; i++)
			{
				d[i] = new GetProfileThreadDelegateWithTiltCorrection(GetProfileThreadWithTiltCorrection);
				ar[i] = d[i].BeginInvoke(xMin, xMax, yThreadMin[i], yThreadMax[i], ref tempProfileIntensity[i], ref tempContibutedPixels[i], null, null);//�e�X���b�h�N���]��
			}
			for (i = 0; i < ThreadTotal; i++)//�X���b�h�I���҂�
				d[i].EndInvoke(ref tempProfileIntensity[i], ref tempContibutedPixels[i], ar[i]);

			//�e�X���b�h�̌��ʂ��܂Ƃ߂�
			for (i = 0; i < R2.Length; i++)
				ContributedPixels[i] = ProfileIntensity[i] = 0;
			for (i = 0; i < ThreadTotal; i++)
				for (j = 0; j < length; j++)
				{
					ProfileIntensity[j] += tempProfileIntensity[i][j];
					ContributedPixels[j] += tempContibutedPixels[i][j];
				}

			//Profile�ϐ��ɑ��
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
		/// 2theta-intensity histgram (���o�[�W����)
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
			x12 = y12 = x13 = y13 = x24 = y24 = x34 = y34 = 0;

			double xx12, yy12, xy12, xx13, yy13, xy13, xx24, yy24, xy24, xx34, yy34, xy34;
			double xx12sq, yy12sq, xy12sq, xx13sq, yy13sq, xy13sq, xx24sq, yy24sq, xy24sq, xx34sq, yy34sq, xy34sq;
			double xxyy12, xxyy13, xxyy24, xxyy34;

			xx12 = yy12 = xy12 = xx13 = yy13 = xy13 = xx24 = yy24 = xy24 = xx34 = yy34 = xy34
				= xx12sq = yy12sq = xy12sq = xx13sq = yy13sq = xy13sq = xx24sq = yy24sq = xy24sq = xx34sq = yy34sq = xy34sq
				= xxyy12 = xxyy13 = xxyy24 = xxyy34 = 0;

			double FD = IP.FilmDistance;
			//�X���␳
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

			//�ŏ��̂P�s�ڂ�InterSection�����m�ۂ��Ă���
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

					//�ȉ��́Al1(�������̃s�[�N�ʒumm)�Ƃ����Ƃ��̐^�� l1' = FD * sin(l1/radius) *radius / ( FD- radius * (1-cos(l1/radius)) )��ό`
					// sin(l1/radius) => l1/radiu,  1- cos(l1/radius) => 2 sin^2 (l1/2/radius) �𗘗p
					double sphericalCorrection = FD / (FD - l2 * IP.SpericalRadiusInverse / 2.0);

					IntersectionPointX[m + width1] = x * sphericalCorrection;
					IntersectionPointY[m + width1] = y * sphericalCorrection;
					IntersectionPointR2[m + width1] = IntersectionPointX[m + width1] * IntersectionPointX[m + width1] + IntersectionPointY[m + width1] * IntersectionPointY[m + width1];
				}

			//��������ϕ��J�n
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

						//�ȉ��́Al1(�������̃s�[�N�ʒumm)�@�@l2 = FD * sin(l1/radius) *radius / ( FD- radius * (1-cos(l1/radius)) )��ό`
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
					if (IsValid[i + jWidth])//�}�X�N����Ă��Ȃ��Ƃ�
					{
						I = Intensity[i + jWidth];

						//���ی��Ɏ����Ă���
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

						//���S����̍ŒZ������

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

						//��`�̖ʐ�
						Area = 0.5 * (xy13 - xy12 + xy34 - xy24);

						for (k = kStart; k < length; k++)
						{
							if (R2[k] > r1)//r1���͊O���̂Ƃ�
							{
								if (R2[k] > r4)//r4���O���̂Ƃ�
									area1 = Area;
								else if (R2[k] < r2)//Pt2�������̂Ƃ�
								{
									//1-2�Ƃ̌�_
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
									{//1-2 1-3 �̂Ƃ�
										//1-3�Ƃ̌�_
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
									{//1-2 3-4 �̂Ƃ�
										//3-4�Ƃ̌�_
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
								else if (R2[k] < r3)//Pt3�������̂Ƃ�
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
									//2-4�Ƃ̌�_
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
								{//2-4 3-4�Ŋm��
									//2-4�Ƃ̌�_
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
									//3-4�Ƃ̌�_
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

		/// <summary>
		/// �s�N�Z�����W(detX, detY)������ԍ��W(X,Y,Z)�ɕϊ�����B���O�Ƀs�N�Z���T�C�Y��ASetTiltParameter�������Ă���Ă���K�v������B
		/// </summary>
		/// <param name="detX"></param>
		/// <param name="detY"></param>
		/// <returns></returns>
		public static (double X, double Y, double Z) ConvertCoordinateFromDetectorToRealSpace(double detX, double detY)
		{
			var tempY = (detY - IP.CenterY) * IP.PixSizeY;//IP���ʏ�̍��W�n�ɂ�����Y�ʒu
			var tempX = (detX - IP.CenterX) * IP.PixSizeX + tempY * TanKsi;//IP���ʏ�̍��W�n�ɂ�����X�ʒu

			//�ȉ���x,y,z����Ԉʒu
			var realX = Numer2 * tempX + Numer1 * tempY;
			var realY = Numer1 * tempX + Numer3 * tempY;
			var realZ = Denom2 * tempX + Denom1 * tempY + IP.FilmDistance;

			if (IP.SpericalRadiusInverse != 0) //���ʕ␳���K�v�ȏꍇ
			{
				var fd = IP.FilmDistance;
				//IP�̖@���x�N�g��
				(double X, double Y, double Z) detector_normal = (Denom2, Denom1, -CosTau);
				//���o�풆�S(0,0,FD)����s�N�Z���܂ł̋���
				double distance2 = realX * realX + realY * realY + (realZ - fd) * (realZ - fd), distance = Math.Sqrt(distance2);
				//���o��̃_�C���N�g�X�|�b�g�����ɏk�߂銄��
				double coeff_detector_palallel = Math.Sin(distance * IP.SpericalRadiusInverse) / distance / IP.SpericalRadiusInverse;
				//���o��̖@�������ɐi�ދ���
				double slide_detector_normal = (1 - Math.Cos(distance * IP.SpericalRadiusInverse)) / IP.SpericalRadiusInverse;
				//(0,0,FD)����(X,Y,Z)�̃x�N�g����coeff_detector_palalle����������Adetector_normal�̕�����slide_detector_normal�����i�߂�B
				realX = realX * coeff_detector_palallel + detector_normal.X * slide_detector_normal;
				realY = realY * coeff_detector_palallel + detector_normal.Y * slide_detector_normal;
				realZ = (realZ - fd) * coeff_detector_palallel + fd + detector_normal.Z * slide_detector_normal;
			}
			return (realX, realY, realZ);
		}

		/// <summary>
		///  2theta-intensity histgram (�V�o�[�W����)
		/// </summary>
		/// <param name="xMin"></param>
		/// <param name="xMax"></param>
		/// <param name="yMin"></param>
		/// <param name="yMax"></param>
		/// <param name="profile"></param>
		/// <param name="pixels"></param>
		public static void GetProfileThreadWithTiltCorrectionNew(int xMin, int xMax, int yMin, int yMax, ref double[] profile, ref double[] pixels)
		{
			int i, j, k, width = IP.SrcWidth, jWidth;
			double area, area1, area2;
			int length = R2.Length;

			double centerX = IP.CenterX, centerY = IP.CenterY, pixSizeX = IP.PixSizeX, pixSizeY = IP.PixSizeY,
				startAngle = IP.StartAngle, stepAngle = IP.StepAngle, fd = IP.FilmDistance;
			double tempX, tempY, tempY2TanKsi;

			//x������0.5�s�N�Z���i�񂾎��̕��i�x�N�g��
			tempX = pixSizeX / 2;
			tempY = 0;
			double slideX_X = Numer2 * tempX + Numer1 * tempY;
			double slideX_Y = Numer1 * tempX + Numer3 * tempY;
			double slideX_Z = Denom2 * tempX + Denom1 * tempY;

			//Y������0.5�s�N�Z���i�񂾎��̕��i�x�N�g��
			tempY = pixSizeY / 2;
			tempX = pixSizeY * TanKsi / 2;
			double slideY_X = Numer2 * tempX + Numer1 * tempY;
			double slideY_Y = Numer1 * tempX + Numer3 * tempY;
			double slideY_Z = Denom2 * tempX + Denom1 * tempY;

			double[] slideX = new double[] { slideX_X + slideY_X, slideX_X - slideY_X };
			double[] slideY = new double[] { slideX_Y + slideY_Y, slideX_Y - slideY_Y };
			double[] slideZ = new double[] { slideX_Z + slideY_Z, slideX_Z - slideY_Z };

			double[] vx = new double[5], vy = new double[5];
			double[] ptx = new double[5], pty = new double[5];

			double x, y, z, q2, q, l2, l;//, qInv, lInv;
			double a, b, bNew, c, r2, p;
			int n, m, startIndex;
			double twoTheta, devTwoTheta;
			double numer1TempY, numer3TempY, denom1tempYFD;
			double intensityPerArea;
			double vxTemp;

			//IP�̖@���x�N�g��
			double detector_normal_X = Denom2;
			double detector_normal_Y = Denom1;
			double detector_normal_Z = -CosTau;

			//��������ϕ��J�n
			for (j = yMin; j < yMax; j++)
			{
				tempY = (j - centerY) * pixSizeY;//IP���ʏ�̍��W�n�ɂ�����Y�ʒu
				tempY2TanKsi = tempY * TanKsi;
				numer1TempY = Numer1 * tempY;
				numer3TempY = Numer3 * tempY;
				denom1tempYFD = Denom1 * tempY + fd;

				jWidth = j * width;
				for (i = xMin; i < xMax; i++)
				{
					if (IsValid[i + jWidth])//�}�X�N����Ă��Ȃ��Ƃ�
					{
						tempX = (i - centerX) * pixSizeX + tempY2TanKsi;//IP���ʏ�̍��W�n�ɂ�����X�ʒu
						//�ȉ���x,y,z���s�N�Z�����S�̋�Ԉʒu
						x = Numer2 * tempX + numer1TempY;
						y = Numer1 * tempX + numer3TempY;
						z = Denom2 * tempX + denom1tempYFD;

						if (IP.SpericalRadiusInverse != 0) //���ʕ␳���K�v�ȏꍇ
						{
							//���o�풆�S(0,0,FD)����s�N�Z���܂ł̋���
							double distance2 = x * x + y * y + (z - fd) * (z - fd), distance = Math.Sqrt(distance2);

							//���o��̃_�C���N�g�X�|�b�g�����ɏk�߂銄��
							double coeff_detector_palallel = Math.Sin(distance * IP.SpericalRadiusInverse) / distance / IP.SpericalRadiusInverse;

							//���o��̖@�������ɐi�ދ���
							double slide_detector_normal = (1 - Math.Cos(distance * IP.SpericalRadiusInverse)) / IP.SpericalRadiusInverse;

							//(0,0,FD)����(X,Y,Z)�̃x�N�g����coeff_detector_palalle����������Adetector_normal�̕�����slide_detector_normal�����i�߂�B
							x = x * coeff_detector_palallel + detector_normal_X * slide_detector_normal;
							y = y * coeff_detector_palallel + detector_normal_Y * slide_detector_normal;
							z = (z - fd) * coeff_detector_palallel + fd + detector_normal_Z * slide_detector_normal;
						}

						q2 = x * x + y * y;
						l2 = q2 + z * z;
						q = Math.Sqrt(q2);
						l = Math.Sqrt(l2);

						//�l���̒��_���W���v�Z
						for (k = 0; k < 2; k++)
						{
							a = x + slideX[k];
							b = y + slideY[k];
							c = z + slideZ[k];

							r2 = a * a + b * b + c * c;
							bNew = (b * x - a * y) / q;
							p = a * x + b * y + c * z;
							vxTemp = (r2 - bNew * bNew) * l2 / p / p - 1;
							vx[k] = vxTemp > 0 ? fd * Math.Sqrt(vxTemp) : 0;
							vy[k] = bNew * l * fd / p;
							if (c * l2 < z * p)
								vx[k] = -vx[k];
							vx[k + 2] = -vx[k];
							vy[k + 2] = -vy[k];
						}
						vx[4] = vx[0];
						vy[4] = vy[0];

						area = vx[0] * (vy[1] - vy[2]) + vx[1] * (vy[2] - vy[0]) + vx[2] * (vy[0] - vy[1]);//��`�̖ʐ�
						if (area < 0) area = -area;

						twoTheta = Math.Asin(q / l);//2�ƎZ�o
						if (z < 0)
							twoTheta = Math.PI - twoTheta;
						devTwoTheta = Math.Atan(Math.Max(Math.Abs(vx[0]), Math.Abs(vx[1])) / fd);

						startIndex = (int)((twoTheta - devTwoTheta - startAngle) / stepAngle + 0.5);
						if (startIndex < 0)
							startIndex = 0;
						intensityPerArea = Intensity[i + jWidth] / area;
						area2 = 0;
						//��`��x=c�̒����Ő؂���A�ʐϔ���v�Z���郋�[�v��������
						for (k = startIndex; k < length; k++)
						{
							if (R2[k] > twoTheta + devTwoTheta)
							{
								pixels[k] += area - area2;
								profile[k] += (area - area2) * intensityPerArea;
								break;
							}
							c = Math.Tan(R2[k] - twoTheta) * fd;
							n = 0;
							for (m = 0; m < 4; m++)
							{
								if (vx[m] < c)
								{
									ptx[n] = vx[m];
									pty[n] = vy[m];
									n++;
								}
								if ((vx[m] < c && c <= vx[m + 1]) || (vx[m] >= c && c > vx[m + 1]))
								{
									ptx[n] = c;
									pty[n] = (c * vy[m + 1] - c * vy[m] - vx[m] * vy[m + 1] + vx[m + 1] * vy[m]) / (vx[m + 1] - vx[m]);
									n++;
								}
							}
							if (n == 3)//0 - 1 - 2 �����3�p�`
								area1 = ptx[0] * (pty[1] - pty[2]) + ptx[1] * (pty[2] - pty[0]) + ptx[2] * (pty[0] - pty[1]);
							else if (n == 4) // 0 - 1 - 2 -  3 �����4�p�`
								area1 = ptx[0] * (pty[1] - pty[3]) + ptx[1] * (pty[2] - pty[0]) + ptx[2] * (pty[3] - pty[1]) + ptx[3] * (pty[0] - pty[2]);
							else if (n == 5)// 0 - 1 - 2 - 3 - 4 �����3�p�`
								area1 = ptx[0] * (pty[1] - pty[4]) + ptx[1] * (pty[2] - pty[0]) + ptx[2] * (pty[3] - pty[1]) + ptx[3] * (pty[4] - pty[2]) + ptx[4] * (pty[0] - pty[3]);
							else
								area1 = 0;
							area1 = area1 > 0 ? area1 * 0.5 : area1 * -0.5;
							pixels[k] += area1 - area2;
							profile[k] += (area1 - area2) * intensityPerArea;
							area2 = area1;
						}
					}
				}
			}

			double mag = 1 / (pixSizeX * pixSizeY);//�W��
			for (i = 0; i < pixels.Length; i++)
				pixels[i] *= mag;
		}

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

		private struct Correspondance
		{
			public int num1, num2;
			public double dev;
			public int[] index;

			public Correspondance(int Num1, int Num2, double Dev)
			{
				num1 = Num1;
				num2 = Num2;
				dev = Dev;
				index = new int[0];
			}
		}

		//FindTiltCorrection�p�̒萔���Ɍ��߂Ă������\�b�h
		public static void SetFindTiltParameter(IntegralProperty iP, double[] peaks, double serchRange)
		{
			IP = iP;
			int i, j, k;
			int h = IP.SrcHeight;
			int w = IP.SrcWidth;

			//�ϕ��̈�S�̂�y����Ay���������߂�
			int YMin, YMax;
			YMin = YMax = 0;
			for (i = 0; i < IsValid.Count; i++)
				if (IsValid[i])
				{//�}�X�N����Ă��Ȃ��|�C���g������������
					YMin = i / w;
					break;
				}
			for (i = IsValid.Count - 1; i > -1; i--)
				if (IsValid[i])
				{//�}�X�N����Ă��Ȃ��|�C���g������������
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

			//�e�X���b�h�̏���Ɖ��������߂�
			yThreadMin = new int[ThreadTotal];
			yThreadMax = new int[ThreadTotal];
			int yStep = (YMax - YMin) / ThreadTotal;
			for (i = 0; i < ThreadTotal; i++)
			{
				yThreadMin[i] = YMin + i * yStep;
				yThreadMax[i] = YMin + (i + 1) * yStep;
			}
			yThreadMax[ThreadTotal - 1] = YMax;

			//�t�B�b�e�B���O�ɕK�v�̂Ȃ��͈͂̓}�X�N����(�X���b�h�𗘗p)
			serchRange *= 2;//���ۂ̃T�[�`�����W��肿����Ƒ��߂�
			double[] peaksPlusRange2 = new double[peaks.Length];
			double[] peaksMinusRange2 = new double[peaks.Length];
			for (k = 0; k < peaks.Length; k++)
			{
				peaksPlusRange2[k] = (peaks[k] + serchRange) * (peaks[k] + serchRange);
				peaksMinusRange2[k] = (peaks[k] - serchRange) * (peaks[k] - serchRange);
			}

			//�v�Z�����_�����߂�
			IsCalcPosition = new bool[(h + 1) * (w + 1)];

			//�X���␳�W�����v�Z
			SetTiltParameter();

			SetFindTiltParameterThreadDelegate[] d = new SetFindTiltParameterThreadDelegate[ThreadTotal];
			IAsyncResult[] ar = new IAsyncResult[ThreadTotal];
			for (i = 0; i < ThreadTotal; i++)
				d[i] = new SetFindTiltParameterThreadDelegate(SetFindTiltParameterThread);
			for (i = 0; i < ThreadTotal; i++)
				ar[i] = d[i].BeginInvoke(xMin, xMax, yThreadMin[i], yThreadMax[i], peaksPlusRange2, peaksMinusRange2, null, null);//�e�X���b�h�N���]��
			for (i = 0; i < ThreadTotal; i++)//�X���b�h�I���҂�
				d[i].EndInvoke(ar[i]);

			//�z��R���쐬�@�e�X�e�b�v���Ƃ̒��S����̋������i�[����z��
			R2 = new double[(int)((IP.EndLength - IP.StartLength) / IP.StepLength) + 1];
			double temp;
			for (i = 0; i < R2.Length; i++)
			{
				temp = IP.StartLength + (i + 0.5) * IP.StepLength;
				R2[i] = temp * temp;
			}
		}

		private delegate void SetFindTiltParameterThreadDelegate(int xMin, int xMax, int YMin, int YMax, double[] peaksPlusRange2, double[] peaksMinusRange2);

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

		public static double[] GetBackground(double lower, double upper)
		{
			return Intensity.ToArray();

			/*
			//�o�b�N�O���E���h�`���T���o���B
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

			//I���u�Ȃ炷�v
			for (int j = 0; j < 5; j++)
			{
				double[] tempI = I.ToArray();
				for (int i = 20; i < I.Count - 20; i++)
				{
					//�����v���X�}�C�i�X5�̓_��50%����Ă�����
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

			//�֐��`�� F0 = Ia * exp ( - 4 Log2 ( (x-X)^2 + C(y-Y)^2 ) / Ha^2 ) + Ib * exp ( - 4 Log2 ( (x-X)^2 + C(y-Y)^2 ) / Hb^2 )
			//�]���l�� ��1/(F-F0)^2

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

			//���݂̎c�����v�Z
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
				for (int i = 0; i < pt.Count; i++)//�Δ��������
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

					//��F/��Ia
					diff[0, i] = Math.Pow(2, (-4 * (-2 * Aa * Ra + Ra2 + x2 + Ca2 * y2)) / Ha2);
					//��F/��Ca
					diff[1, i] = -(Math.Pow(2, 3 - (4 * (-2 * Aa * Ra + Ra2 + x2 + Ca2 * y2)) / Ha2) * Ca * Ia * Ln2 * (1 - Ra / Aa) * y2) / Ha2;
					//��F/��Ra
					diff[2, i] = -(Math.Pow(2, 2 - (4 * (-2 * Aa * Ra + Ra2 + x2 + Ca2 * y2)) / Ha2) * Ia * Ln2 * (-2 * Aa + 2 * Ra)) / Ha2;
					//��F/��Ha
					diff[3, i] = (Math.Pow(2, 3 - (4 * (-2 * Aa * Ra + Ra2 + x2 + Ca2 * y2)) / Ha2) * Ia * Ln2 * (-2 * Aa * Ra + Ra2 + x2 + Ca2 * y2)) / Ha3;

					//��F/��Ib
					diff[4, i] = Math.Pow(2, (-4 * (-2 * Ab * Rb + Rb2 + x2 + Cb2 * y2)) / Hb2);
					//��F/��Cb
					diff[5, i] = -(Math.Pow(2, 3 - (4 * (-2 * Ab * Rb + Rb2 + x2 + Cb2 * y2)) / Hb2) * Cb * Ib * Ln2 * (1 - Rb / Ab) * y2) / Hb2;
					//��F/��Rb
					diff[6, i] = -(Math.Pow(2, 2 - (4 * (-2 * Ab * Rb + Rb2 + x2 + Cb2 * y2)) / Hb2) * Ib * Ln2 * (-2 * Ab + 2 * Rb)) / Hb2;
					//��F/��Hc
					diff[7, i] = (Math.Pow(2, 3 - (4 * (-2 * Ab * Rb + Rb2 + x2 + Cb2 * y2)) / Hb2) * Ib * Ln2 * (-2 * Ab * Rb + Rb2 + x2 + Cb2 * y2)) / Hb3;

					//��F/��Ic
					diff[8, i] = Math.Pow(2, (-4 * (-2 * Ac * Rc + Rc2 + x2 + Cc2 * y2)) / Hc2);
					//��F/��Cc
					diff[9, i] = -(Math.Pow(2, 3 - (4 * (-2 * Ac * Rc + Rc2 + x2 + Cc2 * y2)) / Hc2) * Cc * Ic * Ln2 * (1 - Rc / Ac) * y2) / Hc2;
					//��F/��Rc
					diff[10, i] = -(Math.Pow(2, 2 - (4 * (-2 * Ac * Rc + Rc2 + x2 + Cc2 * y2)) / Hc2) * Ic * Ln2 * (-2 * Ac + 2 * Rc)) / Hc2;
					//��F/��Hc
					diff[11, i] = (Math.Pow(2, 3 - (4 * (-2 * Ac * Rc + Rc2 + x2 + Cc2 * y2)) / Hc2) * Ic * Ln2 * (-2 * Ac * Rc + Rc2 + x2 + Cc2 * y2)) / Hc3;
				}

				//�s��Alpha, Beta�����
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

				//�����炵���p�����[�^�ł̎c�����v�Z
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
	}
}