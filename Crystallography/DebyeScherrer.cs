using System;
using System.Windows.Forms;

namespace Crystallography
{
	public class IntegralProperty{
		public int SrcWidth;//ソース画像の幅
		public int SrcHeight;//ソース画像の高さ
		public double CenterX;//センターのx位置
		public double CenterY;//センターのy位置
		public double PixSizeX,PixSizeY;//ピクセルサイズ
		public bool IsAngleMode;//アングルモードかピクセルモードか
		public double AspectRatio;//縦横比 y/x xに対するyの比
		public double StepAngle,StepPixel;//角度もしくはピクセルのステップ
		public double StartAngle,EndAngle,StartPixel,EndPixel;//角度もしくはピクセルの上限値、下限値
		public double CameraLength;//カメラ長
		public double phi;//IPの角度Tilt
		public double beta;//IPの角度Azumuth;
		public bool IsRectangle;//矩形モードかセクターモードか
		
		//Rectangleモードのとき
        public bool IsFull;
		public double RectangleBand;//バンドの太さ
		public double RectangleAngle;//角度
		public bool RectangleIsBothSide;//半直線かどうか 
		//Sectorモードのとき
		public double SectorStartAngle;//開始角度
		public double SectorEndAngle;//終了角度

		//Rectangleモードのコンストラクタ
		public IntegralProperty(){
		}
	}

	/// <summary>
	/// DebyeScherrer の概要の説明です。
	/// </summary>
	public class DebyeScherrer
	{
		public DebyeScherrer()
		{
		}

        public static bool[] FindSpots(IntegralProperty iP, uint[] intensity, double DeviationFactor)
        {
            bool[] IsSpots = new bool[iP.SrcHeight * iP.SrcWidth];
            //ピクセルステップの平均値と標準偏差をもとめる
            int i, j, width, height;
            double AspectRatio, x, y, centerX, centerY;
            IP = iP;
            //Intensity = intensity;
            width = IP.SrcWidth;
            height = IP.SrcHeight;
            centerX = IP.CenterX;
            centerY = IP.CenterY;
            AspectRatio = IP.AspectRatio;
            double A2 = AspectRatio * AspectRatio;
            int length = (int)Math.Max(Math.Max(Math.Sqrt(centerX * centerX + centerY * centerY * A2),
                Math.Sqrt((width - centerX) * (width - centerX) + centerY * centerY) * A2),
                Math.Max(Math.Sqrt(centerX * centerX + (height - centerY) * (height - centerY) * A2),
                Math.Sqrt((width - centerX) * (width - centerX) + (height - centerY) * (height - centerY) * A2))) + 1;
            ushort[] r = new ushort[height * width];
            int n = 0;
            double y2;
            double[] I = new double[intensity.Length];
            for (j = 0; j < height; j++)
            {
                y = (j - centerY) * AspectRatio;
                y2 = y * y + 0.5;
                for (i = 0; i < width; i++)
                {
                    x = i - centerX;
                    r[n] = (ushort)(Math.Sqrt(x * x + y2));
                    I[n] = intensity[n];
                    n++;
                }
            }
            double[] ContributedPixelNumber;
            double[] SumOfIntensity;
            double[] SumOfIntensitySquare;
            double[] OverLimit = new double[length];
            double[] UnderLimit = new double[length];
            double tempDeviation, tempAverage;
            
            int l;
            for (int k = 0; k < 2; k++)
            {
                SumOfIntensity = new double[length];
                ContributedPixelNumber = new double[length];
                SumOfIntensitySquare = new double[length];
                for (l = 0; l < n; l++)
                    if (!IsSpots[l])
                    {
                        SumOfIntensity[r[l]] += I[l];
                        SumOfIntensitySquare[r[l]] += I[l] * I[l];
                        ContributedPixelNumber[r[l]]++;
                    }

                for (i = 0; i < length; i++)
                    if (ContributedPixelNumber[i] < 2.0)
                    {
                        OverLimit[i] = 0;
                        UnderLimit[i] = 0;
                    }
                    else
                    {
                        tempDeviation = DeviationFactor * Math.Sqrt(
                            (ContributedPixelNumber[i] * SumOfIntensitySquare[i] - SumOfIntensity[i] * SumOfIntensity[i])
                            / ContributedPixelNumber[i] / (ContributedPixelNumber[i] - 1)
                            );
                        tempAverage = SumOfIntensity[i] / ContributedPixelNumber[i];
                        OverLimit[i] = tempAverage + tempDeviation;
                        UnderLimit[i] = tempAverage - tempDeviation;
                    }
                for (l = 0; l < n; l++)
                    if (!IsSpots[l])
                        if (I[l] < UnderLimit[r[l]] || I[l] > OverLimit[r[l]])
                            IsSpots[l] = true;
            }
            
            return IsSpots;
        }





        public static double[] R2;
		public static uint[] Intensity;
		public static bool[] IsMask;
		public static IntegralProperty IP;
		public static Profile GetProfile(IntegralProperty iP,uint[] intensity,bool[] isMask) {

			Intensity=intensity;
			IP=iP;
            IsMask = isMask;
			
			int ThreadTotal=16;
			int i,j,length;
			
			//積分領域全体のy上限、y下限を決める
			int YMin,YMax;
			YMin=YMax=0;
			for(i=0;i<IsMask.Length;i++)
				if(!IsMask[i]){//マスクされていないポイントが見つかったら
					YMin=i/IP.SrcWidth;
					break;
				}
			for(i=IsMask.Length-1;i>-1;i--)
				if(!IsMask[i]){//マスクされていないポイントが見つかったら
					YMax=i/IP.SrcWidth;
					break;
				}
			
			//各スレッドの上限と下限を決める
			int[] yMin=new int[ThreadTotal];
			int[] yMax=new int[ThreadTotal];
			int yStep=(YMax-YMin)/ThreadTotal;
			for(i= 0 ; i<ThreadTotal; i++){
				yMin[i]=YMin+i*yStep;
				yMax[i]=YMin+(i+1)*yStep;
			}
			yMax[ThreadTotal-1]=YMax+1;		
     	
			//配列R2を作成　各ステップごとの中心からの距離を格納する配列
			if(IP.IsAngleMode)
				R2=new double[(int)((IP.EndAngle-IP.StartAngle)/IP.StepAngle)+1];
			else 
				R2=new double[(int)((IP.EndPixel-IP.StartPixel)/IP.StepPixel)+1];
			length=R2.Length;
			
            //R2に格納するステップ
			if(IP.IsAngleMode)//角度モードのときは
				for(i=0; i<length ; i++)
					R2[i]=Math.Pow((Math.Tan(i*IP.StepAngle+IP.StartAngle)*IP.CameraLength),2);
			else//ピクセルモードのときは
				for(i=0; i<length ; i++)
					R2[i]=(IP.StartPixel+i*IP.StepPixel)*(IP.StartPixel+i*IP.StepPixel);
			
			//Profile(各ステップごとの強度)とPixels(各ステップに寄与したピクセル数)を作成
			double[][] tempProfileIntensity = new double[ThreadTotal][];
			double[][] tempContibutedPixels = new double[ThreadTotal][];
			for(i=0 ; i<ThreadTotal ; i++){
				tempProfileIntensity[i]=new double[length];
				tempContibutedPixels[i]=new double[length];
			}

			//ここからスレッド起動
			GetProfileThreadDelegate[] d = new GetProfileThreadDelegate[ThreadTotal];
			IAsyncResult[] ar = new IAsyncResult[ThreadTotal];
			for(i = 0; i<ThreadTotal ; i++)
				d[i] = new GetProfileThreadDelegate(GetProfileThread);
			for(i=0 ; i<ThreadTotal ; i++)
				ar[i]=d[i].BeginInvoke(yMin[i],yMax[i],ref tempProfileIntensity[i],ref tempContibutedPixels[i],null,null);//各スレッド起動転送
			for(i=0 ; i<ThreadTotal ; i++)//スレッド終了待ち
				d[i].EndInvoke(ref tempProfileIntensity[i],ref tempContibutedPixels[i], ar[i]);
			double[] ProfileIntensity=new double[R2.Length];
			double[] ContributedPixels=new double[R2.Length];
			for(i=0; i<R2.Length; i++)
				ContributedPixels[i]=ProfileIntensity[i]=0;
			for(i=0 ; i<ThreadTotal ; i++){
				for(j=0; j<length; j++){
					ProfileIntensity[j]+=tempProfileIntensity[i][j];
					ContributedPixels[j]+=tempContibutedPixels[i][j];
				}
			}
			
            //Profile変数に代入
            Profile p =new Profile();
			double temp;
			if(IP.IsAngleMode)
				for(i=0; i<length; i++)
				{
					temp=ProfileIntensity[i]/ContributedPixels[i];
                    //temp2 += ContributedPixels[i];
					if(double.IsNaN(temp) || double.IsInfinity(temp))
						temp=0;
					p.AddPt((i*IP.StepAngle+IP.StartAngle)/Math.PI*180.0,temp,false);
				}
			else
				for(i=0; i<length; i++)
				{
					temp=ProfileIntensity[i]/ContributedPixels[i];
                    //temp2 += ContributedPixels[i];
					if(double.IsNaN(temp) || double.IsInfinity(temp))
						temp=0;
					p.AddPt(i*IP.StepPixel+IP.StartPixel,temp,false);
				}
			Clipboard.SetDataObject(p);
			return p;
		}


		//積分領域以外をマスクする関数
		public static bool[] SetMask(IntegralProperty IP) {
            if(IP.IsFull)
                return new bool[IP.SrcWidth * IP.SrcHeight];
            int i, j;
			int Height=IP.SrcHeight;
			int Width =IP.SrcWidth;
            IsMask = new bool[IP.SrcWidth*IP.SrcHeight];
			double CenterX=IP.CenterX;
			double CenterY=IP.CenterY;
			double Band		=IP.RectangleBand;
            int n = 0;
            if (IP.IsRectangle)
            {//Rectangleモードのとき
                bool IsXY = false;
                double tan = Math.Tan(IP.RectangleAngle);
                double sin = Math.Sin(IP.RectangleAngle);
                double cos = Math.Cos(IP.RectangleAngle);
                double wx = Math.Abs(Band / sin);
                double wy = Math.Abs(IP.RectangleBand / cos);
                double cx, cy;
                if (Math.Abs(tan) > 1)
                    IsXY = true;//縦方向に近い場合はTrue
                
                

                if (IsXY)
                {
                    double MinusCenterYPerTanPlusCenterX = -CenterY / tan + CenterX;
                    
                    if (IP.RectangleIsBothSide){//IsXYがTrueで全直線モード
                        for (j = 0; j < Height; j++){
                            cx = j / tan + MinusCenterYPerTanPlusCenterX;
                            for (i = 0; i < Width; i++){
                                if (i < cx - wx || i > cx + wx)//バンドの外にはみ出しているとき
                                    IsMask[n] = true;
                                n++;
                            }
                        }
                    }
                    else
                    {//IsXYがTrueで半直線モード
                        double CenterXPerTanPlusCenterY =CenterX/ tan + CenterY;
                        for (j = 0; j < Height; j++){
                            cx = j / tan + MinusCenterYPerTanPlusCenterX;
                            for (i = 0; i < Width; i++){
                                if (i < cx - wx || i > cx + wx)//バンドの外にはみ出しているとき
                                    IsMask[n] = true;
                                else{
                                    cy = -i / tan + CenterXPerTanPlusCenterY;
                                    if ((j < cy && sin > 0) || (sin < 0 && j > cy))
                                        IsMask[n] = true;
                                }
                                n++;
                            }
                            
                        }
                    }
                }
                else//IsXYがFalse
                {

                    double CenterYMinusTanCenterX= CenterY - tan * CenterX;
                    if (IP.RectangleIsBothSide)
                    {
                        for (i = 0; i < Width; i++)
                        {
                            cy = tan * i + CenterYMinusTanCenterX;
                            for (j = 0; j < Height; j++)
                            {
                                if (j < cy - wy || j > cy + wy)//バンドの外にはみ出しているとき
                                    IsMask[i+j*Width] = true;
                            }
                        }
                    }
                    else{//半直線モードのとき
                        double CenterYTanPlusCenterX = CenterY * tan + CenterX;
                        for (i = 0; i < Width; i++){
                            cy = tan * i + CenterYMinusTanCenterX;
                            for (j = 0; j < Height; j++){
                                if (j < cy - wy || j > cy + wy)//バンドの外にはみ出しているとき
                                    IsMask[i + j * Width] = true;
                                else
                                {
                                    cx = -j * tan + CenterYTanPlusCenterX;
                                    if ((cos > 0 && i < cx) || (cos < 0 && i > cx))
                                        IsMask[i + j * Width] = true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {//Sectorモードのとき
               
                double End = IP.SectorEndAngle;
                double Start = IP.SectorStartAngle;
                double a;
                for (j = 0; j < Height; j++)
                    for (i = 0; i < Width; i++)
                    {
                        a = Math.Atan2(j - CenterY, i - CenterX);
                        if (!((a > Start && a < End) || ((a + 2 * Math.PI) > Start && (a + 2 * Math.PI) < End) || ((a + 4 * Math.PI) > Start && (a + 4 * Math.PI) < End)))
                            IsMask[n] = true;
                        n++;
                    }
            }
			return IsMask;
		}
			
		delegate  void GetProfileThreadDelegate(int yMin,int yMax,ref double[] profile,ref double[] pixels);
		public static void GetProfileThread(int yMin,int yMax,ref double[] profile,ref double[] pixels){
			int i,j,k,width,kStart;
			double I;
			double Step,AspectRatio,x,y,temp1,temp2,centerX,centerY,Top,Bottom,Right,Left,Top2,Bottom2,Right2,Left2,LB2,LT2,RB2,RT2;
			double Start;
					
			width=IP.SrcWidth;
			centerX=IP.CenterX;		
			centerY=IP.CenterY;
			AspectRatio=IP.AspectRatio;
			
			//ピクセルモードのとき
			if(!IP.IsAngleMode){
				Start=IP.StartPixel;
				Step=IP.StepPixel;
				int length=R2.Length;
				double StartPerStep=-Start/Step+1;
				double Step2=1/Step/Step;
                bool flag = true;
                int n = yMin * width;
				for ( j = yMin; j< yMax ; j++){
					y=(j-centerY)*AspectRatio;
					if(y<0)y=-y;
					Top=y+AspectRatio/2.0;
					Top2=Top*Top;
					Bottom=y-AspectRatio/2.0;
					Bottom2=Bottom*Bottom;
					for ( i = 0; i< width ; i++) {
						if(!IsMask[n]){//マスクされていないとき
							x=i-centerX;
							//第一象限かつx>yに変換
							if(x<0)x=-x;
							Right=x+0.5; Left=x-0.5;	 Right2=Right*Right; Left2=Left*Left;
							LB2=Bottom2+Left2;	LT2=Top2+Left2;		RB2=Right2+Bottom2;		RT2=Right2+Top2;
							I=Intensity[n];
                            if (i < 0)
                                return;
							kStart=(int)(Math.Sqrt(LB2*Step2)+StartPerStep);
							temp2=0;
							flag=true;
							for(k=kStart;flag && k<length && k>-1;k++){
								if(R2[k] > LB2){//rの少しでも内側にピクセルがあるとき
									if(R2[k]>RT2){//rの完全に内側にピクセルがあるとき
											temp1=AspectRatio;
											flag=false;
										}
									else if(R2[k] < LT2){
										if(R2[k] < RB2)//TopLeft,BottomRightの内側にあるとき
											temp1=(Math.Sqrt(R2[k]-Bottom2)-Left)*(Math.Sqrt(R2[k]-Left2)-Bottom)*0.5;
										else					//TopLeftの内側、BottomRightの外側にあるとき
                                            temp1 = (Math.Sqrt(R2[k] - Left2) - Bottom + Math.Sqrt(R2[k] - Right2) - Bottom) * 0.5;
									}
									else{
										if(R2[k] < RB2)//TopLeftの外側、BottomRightの内側にあるとき
                                            temp1 = (Math.Sqrt(R2[k] - Top2) - Left + Math.Sqrt(R2[k] - Bottom2) - Left) * AspectRatio * 0.5;
										else //TopLeftの外側、BottomRightの外側、TopRightの内側
											temp1=AspectRatio-(Right-Math.Sqrt(R2[k]-Top2))*(Top-Math.Sqrt(R2[k]-Right2))*0.5;
										
									}
									profile[k] += (temp1-temp2)/AspectRatio*I;
                                    pixels[k] += (temp1 - temp2)/AspectRatio;
									temp2=temp1;
								}
							}
						}
						n++;
					}
				}
			}//ピクセルモード終了
			else{
                Start = (int)(IP.StartAngle / IP.StepAngle);
                Step = IP.StepAngle;
                double PixSizeX=IP.PixSizeX/2.0;
				double PixSizeY=IP.PixSizeY/2.0;
                double PixArea = PixSizeX * PixSizeY;
				double CameraLength=IP.CameraLength;
				int length=R2.Length;
                bool flag = true;
                int n = yMin * width;
				for ( j = yMin; j< yMax ; j++){
					y=(j-centerY)*PixSizeY;
					if(y<0)y=-y;
					Top=y+PixSizeY;
					Top2=Top*Top;
					Bottom=y-PixSizeY;
					Bottom2=Bottom*Bottom;
					for ( i = 0; i< width ; i++) {
                        if (!IsMask[n])
                        {//マスクされていないとき
							x=(i-centerX)*PixSizeX;
							//第一象限かつx>yに変換
							if(x<0)x=-x;
							Right=x+PixSizeX; Left=x-PixSizeX;	 Right2=Right*Right; Left2=Left*Left;
							LB2=Bottom2+Left2;	LT2=Top2+Left2;		RB2=Right2+Bottom2;		RT2=Right2+Top2;
							I=Intensity[n];
							kStart=(int)(Math.Atan2(Math.Sqrt(LB2),CameraLength)/Step-Start);
							temp2=0;
							flag=true;
							for(k=kStart;flag && k<length && k>-1;k++){
								if(R2[k] > LB2){//rの少しでも内側にピクセルがあるとき
									if(R2[k]>RT2){//rの完全に内側にピクセルがあるとき
                                        temp1 = PixArea;
											flag=false;
										}
									else if(R2[k] < LT2){
										if(R2[k] < RB2)//TopLeft,BottomRightの内側にあるとき
											temp1=(Math.Sqrt(R2[k]-Bottom2)-Left)*(Math.Sqrt(R2[k]-Left2)-Bottom)*0.5;
										else					//TopLeftの内側、BottomRightの外側にあるとき
                                            temp1 = (Math.Sqrt(R2[k] - Left2) - Bottom + Math.Sqrt(R2[k] - Right2) - Bottom) * PixSizeY * 0.5;
									}
									else{
										if(R2[k] < RB2)//TopLeftの外側、BottomRightの内側にあるとき
                                            temp1 = (Math.Sqrt(R2[k] - Top2) - Left + Math.Sqrt(R2[k] - Bottom2) - Left) * PixSizeX * 0.5;
										else //TopLeftの外側、BottomRightの外側、TopRightの内側
                                            temp1 = PixArea - (Right - Math.Sqrt(R2[k] - Top2)) * (Top - Math.Sqrt(R2[k] - Right2)) * 0.5;
										
									}
                                    profile[k] += (temp1 - temp2)/PixArea  * I;
                                    pixels[k] += (temp1 - temp2) / PixArea;
									temp2=temp1;
								}
							}
						}
						n++;
					}
				}
			}////アングルモード終了
		}




        public static Pt FindCenter(IntegralProperty iP, uint[] intensity, bool[] isMask, bool IsNearCenter)
        {
            //荒く中心決め
            IP = iP;
            

            int i, j, k, l;
            double temp;
            double tempX = 0;
            double tempY = 0;
            double tempInt = 0;
            if (IsNearCenter)
            {
                for (j = (int)(IP.CenterY - 2.5); j < (int)(IP.CenterY + 4.5); j++)
                    for (i = (int)(IP.CenterX - 2.5); i < (int)(IP.CenterX + 4.5); i++)
                    {
                        temp = intensity[j * IP.SrcWidth + i];
                        temp = temp * temp;
                        tempX += temp * i;
                        tempY += temp * j;
                        tempInt += temp;
                    }
                return new Pt(tempX / tempInt, tempY / tempInt);
            }
            
            
            //まず画像を平滑化
            Intensity = new uint[intensity.Length];
            for(i=0 ; i<IP.SrcWidth ; i++)
                for (j = 0; j < IP.SrcHeight; j++)
                {
                    if (i == 0 || i == IP.SrcWidth - 1 || j == 0 || j == iP.SrcHeight - 1)
                        Intensity[i + j * IP.SrcWidth] = 0;
                    else{
                        for(k=-1;k<2;k++)
                            for (l = -1; l < 2; l++)
                            {
                                Intensity[i + j * IP.SrcWidth] = intensity[i + k + (j + l) * IP.SrcWidth] / 9;
                            }
                    }
                }
            
            IP.StartPixel = 50;

            double startX, startY, endX, endY, stepX, stepY, bestX, bestY;
            double residual, bestResidual;
            double x, y;

            int bandHalfWidth = (int)(IP.SrcWidth / 60);

             //まず各xMax,xMin,yMax,yMinを定義
            int[] yMin,yMax, xMin,xMax;
            yMin=new int[4]; yMax=new int[4];  xMin=new int[4];  xMax=new int[4];

            double[][] profile=new double[4][];
            double[][] SecondDifferentialProfile = new double[4][];
            
            GetProfileThreadDelegateForFindCenter[] d = new GetProfileThreadDelegateForFindCenter[4];
            IAsyncResult[] ar = new IAsyncResult[4];
            for (i = 0; i < 4; i++)d[i] = new GetProfileThreadDelegateForFindCenter(GetProfileThreadForFindCenter);

            int Range = (int)(Math.Min(IP.SrcHeight,IP.SrcWidth)*0.3);
            //各ピクセルまでの距離を格納する配列R2の設定
            IP.StepPixel = 2;
            R2 = new double[(int)((Range-IP.StartPixel)/IP.StepPixel)];
            for (i = 0; i < R2.Length; i++)
                R2[i] = (IP.StartPixel + i * IP.StepPixel) * (IP.StartPixel + i * IP.StepPixel);

            for (i = 0; i < 4; i++)profile[i] = new double[R2.Length];

            //まずXとYを独立に動かす
            bestX = IP.CenterX;
            bestY = IP.CenterY;
            stepX = stepY = 2;
            startX = bestX - 50; endX = bestX + 50;
            startY = bestY - 50; endY = bestY + 50;
            
            for (int n = 0; n < 5; n++)
            {
                //まず荒くY位置決め
                IP.CenterX = bestX;
                xMin[0] = (int)bestX - bandHalfWidth; xMax[0] = (int)bestX + bandHalfWidth;
                xMin[1] = (int)bestX - bandHalfWidth; xMax[1] = (int)bestX + bandHalfWidth;
                bestResidual = double.PositiveInfinity;
                for (y = startY; y <= endY; y+=stepY)
                {
                    for (i = 0; i < 2; i++) profile[i] = new double[R2.Length];
                    residual = 0;  IP.CenterY = y;
                    int Y = (int)y;
                    yMin[0] = Y - Range; yMax[0] = Y;//Top
                    yMin[1] = Y + 1; yMax[1] = Y + Range + 1; //bottom
                    //スレッド起動
                    for (i = 0; i < 2; i++)
                        ar[i] = d[i].BeginInvoke(xMin[i], xMax[i], yMin[i], yMax[i], ref profile[i], null, null);//各スレッド起動転送
                    //スレッド終了待ち
                    for (i = 0; i < 2; i++)
                    {
                        d[i].EndInvoke(ref profile[i], ar[i]);
                        //SecondDifferentialProfile[i] = GetSecondDifferential(profile[i]);
                    }
                    for (i = 0; i < profile[0].Length - 2; i++)
                    {
                        //temp = SecondDifferentialProfile[0][i] - SecondDifferentialProfile[1][i];
                        temp = profile[0][i] - profile[1][i];
                        residual += temp * temp;
                    }
                    if (residual < bestResidual)
                    {
                        bestY = y; bestResidual = residual;
                    }
                }
                startY = bestY - stepY * 10; endY = bestY + stepY * 10; stepY = 0.6;
                //次に荒くX位置決め
                IP.CenterY = bestY;
                yMin[2] = (int)bestY - bandHalfWidth; yMax[2] = (int)bestY + bandHalfWidth;
                yMin[3] = (int)bestY - bandHalfWidth; yMax[3] = (int)bestY + bandHalfWidth;
                bestResidual = double.PositiveInfinity;
                for (x = startX; x <= endX; x += stepX)
                {
                    for (i = 2; i < 4; i++) profile[i] = new double[R2.Length];
                    residual = 0;
                    IP.CenterX = x;
                    int X = (int)x;
                    xMin[2] = X - Range; xMax[2] = X;//Left
                    xMin[3] = X + 1; xMax[3] = X + Range + 1;//Right
                    for (i = 2; i < 4; i++) //スレッド起動
                        ar[i] = d[i].BeginInvoke(xMin[i], xMax[i], yMin[i], yMax[i], ref profile[i], null, null);//各スレッド起動転送
                    for (i = 2; i < 4; i++)//スレッド終了待ち
                    {
                        d[i].EndInvoke(ref profile[i], ar[i]);
                        //SecondDifferentialProfile[i] = GetSecondDifferential(profile[i]);
                    }
                    for (i = 0; i < profile[2].Length - 2; i++)
                    {
                        //temp = SecondDifferentialProfile[2][i] - SecondDifferentialProfile[3][i];
                        temp = profile[2][i] - profile[3][i];
                        residual += temp * temp;
                    }
                    if (residual < bestResidual)
                    {
                        bestX = x; bestResidual = residual;
                    }
                }
                startX = bestX - stepX * 10; endX = bestX + stepX * 10; stepX *= 0.6;
            }

            //各ピクセルまでの距離を格納する配列R2の設定
            IP.StepPixel = 1;
            R2 = new double[(int)((Range - IP.StartPixel) / IP.StepPixel)];
            for (i = 0; i < R2.Length; i++)
                R2[i] = (IP.StartPixel + i * IP.StepPixel) * (IP.StartPixel + i * IP.StepPixel);

            //XYを同時に動かして精密化する
            stepX = stepY = 1;
            startX = bestX - 1.5 * stepX; endX = bestX + 1.5 * stepX;
            startY = bestY - 1.5 * stepY; endY = bestY + 1.5 * stepY;
            bestResidual = double.PositiveInfinity;
            for (int n = 0; n < 10; n++)
            {//繰り返し計算回数
                for (x = startX; x <= endX; x += stepX)
                {
                    for (y = startY; y <= endY; y += stepY)
                    {
                        for (i = 0; i < 4; i++) profile[i] = new double[R2.Length];
                        residual = 0;
                        IP.CenterX = x;
                        IP.CenterY = y;
                        int X = (int)x;
                        int Y = (int)y;
                        //Top
                        xMin[0] = X - bandHalfWidth; xMax[0] = X + bandHalfWidth; yMin[0] = Y - Range; yMax[0] = Y;
                        //bottom
                        xMin[1] = X - bandHalfWidth; xMax[1] = X + bandHalfWidth; yMin[1] = Y + 1; yMax[1] = Y + Range + 1;
                        //Left
                        xMin[2] = X - Range; xMax[2] = X; yMin[2] = Y - bandHalfWidth; yMax[2] = Y + bandHalfWidth;
                        //Right
                        xMin[3] = X + 1; xMax[3] = X + Range + 1; yMin[3] = Y - bandHalfWidth; yMax[3] = Y + bandHalfWidth;

                        //スレッド起動
                        for (i = 0; i < 4; i++)
                            ar[i] = d[i].BeginInvoke(xMin[i], xMax[i], yMin[i], yMax[i], ref profile[i], null, null);//各スレッド起動転送
                        //スレッド終了待ち
                        for (i = 0; i < 4; i++)
                        {
                            d[i].EndInvoke(ref profile[i], ar[i]);
                            SecondDifferentialProfile[i] = GetSecondDifferential(profile[i]);
                        }

                        for (i = 0; i < profile[0].Length - 2; i++)
                        {
                            temp = SecondDifferentialProfile[0][i] - SecondDifferentialProfile[1][i];
                            residual += temp * temp;
                            temp = SecondDifferentialProfile[2][i] - SecondDifferentialProfile[3][i];
                            residual += temp * temp;
                        }

                        if (residual < bestResidual)
                        {
                            bestX = x;
                            bestY = y;
                            bestResidual = residual;
                        }

                    }
                }
                startX = bestX - stepX * 0.6; endX = bestX + stepX * 0.6; stepX *= 0.4;
                startY = bestY - stepY * 0.6; endY = bestY + stepY * 0.6; stepY *= 0.4;
            }
            return new Pt(bestX, bestY);

        }

        delegate void GetProfileThreadDelegateForFindCenter(int xMin, int xMax, int yMin, int yMax, ref double[] profile);
        public static void GetProfileThreadForFindCenter(int xMin, int xMax, int yMin, int yMax, ref double[] profile)
        {
            int i, j, k, jwidth, width, kStart;
            uint I;
            double Step, AspectRatio, x, y, temp1, temp2, centerX, centerY, Top, Bottom, Right, Left, Top2, Bottom2, Right2, Left2, LB2, LT2, RB2, RT2;
            double Start;

            width = IP.SrcWidth;
            centerX = IP.CenterX;
            centerY = IP.CenterY;
            AspectRatio = IP.AspectRatio;

            Start = IP.StartPixel;
            Step = IP.StepPixel;
            int length = R2.Length;
            double StartPerStep = -Start / Step + 1;
            double Step2 = 1 / Step / Step;
            bool flag = true;
            for (j = yMin; j < yMax; j++)
            {
                y = (j - centerY) * AspectRatio;
                if (y < 0) y = -y;
                Top = y + AspectRatio / 2.0;
                Top2 = Top * Top;
                Bottom = y - AspectRatio / 2.0;
                Bottom2 = Bottom * Bottom;
                jwidth = j * width;
                for (i = xMin; i < xMax; i++)
                {
                    x = i - centerX;
                    //第一象限かつx>yに変換
                    if (x < 0) x = -x;
                    Right = x + 0.5; Left = x - 0.5; Right2 = Right * Right; Left2 = Left * Left;
                    LB2 = Bottom2 + Left2; LT2 = Top2 + Left2; RB2 = Right2 + Bottom2; RT2 = Right2 + Top2;
                    I = Intensity[i + jwidth];
                    kStart = (int)(Math.Sqrt(LB2 * Step2) + StartPerStep);
                    temp2 = 0;
                    flag = true;
                    for (k = kStart; flag && k < length && k > -1; k++)
                    {
                        if (R2[k] > LB2)
                        {//rの少しでも内側にピクセルがあるとき
                            if (R2[k] > RT2)
                            {//rの完全に内側にピクセルがあるとき
                                temp1 = AspectRatio;
                                flag = false;
                            }
                            else if (R2[k] < LT2)
                            {
                                if (R2[k] < RB2)//TopLeft,BottomRightの内側にあるとき
                                    temp1 = (Math.Sqrt(R2[k] - Bottom2) - Left) * (Math.Sqrt(R2[k] - Left2) - Bottom) * 0.5;
                                else					//TopLeftの内側、BottomRightの外側にあるとき
                                    temp1 = (Math.Sqrt(R2[k] - Left2) - Bottom + Math.Sqrt(R2[k] - Right2) - Bottom) * 0.5;
                            }
                            else
                            {
                                if (R2[k] < RB2)//TopLeftの外側、BottomRightの内側にあるとき
                                    temp1 = (Math.Sqrt(R2[k] - Top2) - Left + Math.Sqrt(R2[k] - Bottom2) - Left) * AspectRatio * 0.5;
                                else //TopLeftの外側、BottomRightの外側、TopRightの内側
                                    temp1 = AspectRatio - (Right - Math.Sqrt(R2[k] - Top2)) * (Top - Math.Sqrt(R2[k] - Right2)) * 0.5;

                            }
                            profile[k] += (temp1 - temp2) / AspectRatio * I;
                            temp2 = temp1;
                        }
                    }
                }
            }
          }


        public static double[] GetSecondDifferential(double[] p)
        {
            double[] pp = new double[p.Length - 1];
            double[] ppp = new double[p.Length - 2];
            for (int i = 0; i < pp.Length; i++)
                pp[i] = p[i+1] - p[i];
            for (int i = 0; i < ppp.Length; i++)
                ppp[i] = pp[i + 1] - pp[i];
            return ppp;



        }



    }
}
