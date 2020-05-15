using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;
using System.IO;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using MathNet.Numerics.LinearAlgebra.Complex;
using OpenTK;
using System.Linq.Dynamic.Core;
using V3 = OpenTK.Vector3d;

namespace Crystallography
{
	public class ConvertCrystalData
	{
		static public long a1 = 0;
		static public long a2 = 0;
		static public long a3 = 0;
		static public long a4 = 0;

		public static bool SaveCrystalListXml(Crystal[] crystals, string filename)
		{
			try
			{
				System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Crystal[]));
				System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create);
				serializer.Serialize(fs, crystals);
				fs.Close();
				return true;
			}
			catch { return false; }
		}

		//CrystalListを読み込むとき
		public static Crystal[] ConvertToCrystalList(string filename)
		{
			Crystal[] cry = new Crystal[0];
			if (filename.ToLower().EndsWith("xml"))//XML形式のリストを読み込んだとき
			{
				#region old code
				//プロパティ文字列が変更にたいする対処
				/*    try
					{
						StreamReader reader = new StreamReader(filename, Encoding.GetEncoding("Shift_JIS"));
						List<string> strList = new List<string>();
						string tempstr;
						while ((tempstr = reader.ReadLine()) != null)
						{
							// "<" あるいは "</" の直後を大文字に変換
							int index = 0;

							index = tempstr.IndexOf("<");
							if (index >= 0 && tempstr.Length > index + 1)
							{
								string targetString = tempstr.Substring(index + 1, 1);
								tempstr = tempstr.Replace("<" + targetString, "<" + targetString.ToUpper());
							}

							index = tempstr.IndexOf("</");
							if (index >= 0 && tempstr.Length > index + 2)
							{
								string targetString = tempstr.Substring(index + 2, 1);
								tempstr = tempstr.Replace("</" + targetString, "</" + targetString.ToUpper());
							}

							tempstr = tempstr.Replace("Alfa", "Alpha");

							//if(tempstr.IndexOf("")>0)

							strList.Add(tempstr);
						}

						reader.Close();

						//filename = filename + "_";//検証のためファイルネーム変更

						StreamWriter writer = new StreamWriter(filename, false, Encoding.GetEncoding("Shift_JIS"));
						for (int i = 0; i < strList.Count; i++)
							writer.WriteLine(strList[i]);
						writer.Flush();
						writer.Close();
					}
					catch { return null; };*/
				//プロパティ文字列が変更にたいする対処　ここまで
				#endregion old code
				try
				{
					using (var fs = new System.IO.FileStream(filename, System.IO.FileMode.Open))
					{
						System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Crystal[]));
						cry = (Crystal[])serializer.Deserialize(fs);
						#region //Bondクラスの単位を オングストロームからnmに変更したための対処
						foreach(var c in cry)
						{
							foreach(var b in c.Bonds)
								if(!b.NanometerUnit)
								{
									b.MaxLength *= 0.1f;
									b.MinLength *= 0.1f;
									b.Radius *= 0.1f;
									b.NanometerUnit = true;
								}
						}    

						#endregion


					}
				}
				catch { }
			}
			else if (filename.EndsWith("out"))//SMAP形式を読み込んだとき
			{
				var stringList = new List<string>();
				string strTemp;
				var reader = new System.IO.StreamReader(filename);
				while ((strTemp = reader.ReadLine()) != null)
					stringList.Add(strTemp);
				reader.Close();
				cry = ConvertFromSMAP(stringList.ToArray());
			}

			for (int i = 0; i < cry.Length; i++)
			{
				cry[i].Reset();
				cry[i].SaveInitialCellConstants();
			}

			return cry;
		}

		#region SMAPの出力ファイル(*.out)読込

		/// <summary>
		/// SMAPのoutファイル読み込み
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static Crystal[] ConvertFromSMAP(string[] str)
		{
			string wavelength = "";
			string ChemicalFormula = "";
			double a = 0, b = 0, c = 0, alpha = 0, beta = 0, gamma = 0;
			int spaceGroupSeriesNum = 0;
			//空間群
			int line = 0;

			for (; line < str.Length; line++)
				if (str[line].StartsWith(" wave length"))
				{
					wavelength = str[line];
					break;
				}
			for (; line < str.Length; line++)
				if (str[line].StartsWith(" Chemical Formula"))
				{
					ChemicalFormula = str[line];
					break;
				}
			for (; line < str.Length; line++)
				if (str[line].StartsWith(" Cell Constants"))
				{
					string[] cellconstants = str[line + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					a = Convert.ToDouble(cellconstants[0]) / 10.0;
					b = Convert.ToDouble(cellconstants[1]) / 10.0;
					c = Convert.ToDouble(cellconstants[2]) / 10.0;
					alpha = Convert.ToDouble(cellconstants[3]) / 180 * Math.PI;
					beta = Convert.ToDouble(cellconstants[4]) / 180 * Math.PI;
					gamma = Convert.ToDouble(cellconstants[5]) / 180 * Math.PI;
					break;
				}

			for (; line < str.Length; line++)
				if (str[line].StartsWith(" Space Group Number"))
				{
					string s = (str[line].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[1]).Trim();
					int num = Convert.ToInt32(s.Substring(1, 3));
					int sub = Convert.ToInt32(s.Substring(4, 1));
					spaceGroupSeriesNum = SymmetryStatic.GetSeriesNumber(num, sub);
					break;
				}

			int n = 0;
			//ここから原始座標読み取り
			List<Crystal> cry = new List<Crystal>();
			while (line < str.Length)
			{
				for (; line < str.Length; line++)
					if (str[line].StartsWith("No ="))
					{
						Crystal tempCrystal = new Crystal(
							(a, b, c, alpha, beta, gamma),
							null, spaceGroupSeriesNum,
							ChemicalFormula.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim() + "-" + (n++).ToString()
							, Color.Blue);
						tempCrystal.Note = wavelength + "  " + ChemicalFormula + "\r\n" + str[line];
						line++;
						for (; line < str.Length; line++)
						{
							if (str[line].Length == 0)
								break;
							string[] s = str[line].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
							string label = s[0];
							//ラベル名から元素を決める
							string temp;

							int atomicNumber = 0;
							for (int q = label.Length; q > 0; q--)
							{
								temp = label.Substring(0, q);
								for (int k = 0; k <= 96; k++)
								{
									if (AtomConstants.AtomicName(k) == temp)
									{
										atomicNumber = k;
										break;
									}
								}
							}
							double x = ZeroToOne(Convert.ToDouble(s[2]));
							double y = ZeroToOne(Convert.ToDouble(s[3]));
							double z = ZeroToOne(Convert.ToDouble(s[4]));

							tempCrystal.AddAtoms(label, atomicNumber, 0, 0, null, x, y, z, 1, new DiffuseScatteringFactor(), true);
						}
						SetOpenGL_property(tempCrystal);
						cry.Add(tempCrystal);
					}
			}
			return cry.ToArray();
		}

		/// <summary>
		/// ConvertFromSMAPから呼び出される
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static double ZeroToOne(double x)
		{
			if (x < 0)
				while (x < 0)
					x++;
			else if (x > 1)
				while (x > 1)
					x--;
			return x;
		}

		#endregion SMAPの出力ファイル(*.out)読込

		public static Crystal2 ConvertToCrystal2(string fileName)
		{
			var c = ConvertToCrystal(fileName);

			if (c != null)
			{
				var c2 = c.ToCrystal2();
				c2.fileName = Path.GetFileNameWithoutExtension(fileName);
				return c2;
			}
			else
				return null;
		}

		public static Crystal ConvertToCrystal(string fileName)
		{
			List<string> stringList = new List<string>();
			string strTemp;
			using (var reader = new System.IO.StreamReader(fileName))
			{
				Crystal crystal = null;
				try
				{
					if (fileName.EndsWith("amc"))
					{
						while ((strTemp = reader.ReadLine()) != null)
							if (strTemp != "")
								stringList.Add(strTemp);
						crystal = ConvertFromAmc(stringList.ToArray());
					}
					else if (fileName.EndsWith("cif"))
					{
						while ((strTemp = reader.ReadLine()) != null)
							stringList.Add(strTemp);
						crystal = ConvertFromCIF(stringList.ToArray());
					}
					return crystal;
				}
				catch (Exception e)
				{
					return null;
				}
			}
	}

		#region amcファイルの読み込み

		/// <summary>
		/// amcファイルの読み込み
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		private static Crystal ConvertFromAmc(string[] str)
		{
			int n = 0;

			string Name;
			string AuthorName = "";
			string Reference = "";
			string Title = "";
			double xShift, yShift, zShift;
			xShift = yShift = zShift = 0;
			Crystal crystal;
			if (str[n] == "")
				n++;

			Name = str[n];//結晶の名前

			n++; if (str.Length <= n) return null;

			AuthorName = str[n];//著者の名前
			n++; if (str.Length <= n) return null;
			while (str[n][str[n].Length - 1] < '.' || str[n][str[n].Length - 1] > '9')
			{
				AuthorName += ", " + str[n];
				n++; if (str.Length <= n) return null;
			}

			Reference = str[n];//引用文献
			n++; if (str.Length <= n) return null;

			Title = str[n];//文献タイトル
			n++; if (str.Length <= n) return null;

			//ここで格子定数、対称性とタイトル
			while ((crystal = CellParamForAmc(str[n])) == null)
			{
				if (str[n].Length > 0 && Char.IsLower(str[n][0]))
					Title += " " + str[n];
				else
					Title += "\r\n" + str[n];
				n++;
				//if (str.Length <= n)
				//    return null;
			}
			if (Title.Contains("_cod_database_code"))
				Title = Title.Replace("_cod_database_code", "\r\n_cod_database_code");
			else if (Title.Contains("_database_code_amcsd"))
				Title = Title.Replace("_database_code_amcsd", "\r\n_database_code_amcsd");

			n++; if (str.Length <= n) return null;

			if (!str[n].StartsWith("atom") && str[n].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length == 3)
			{
				xShift = ConvertToDouble(str[n].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
				yShift = ConvertToDouble(str[n].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
				zShift = ConvertToDouble(str[n].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2]);
				n++; if (str.Length <= n) return null;
			}

			//ここから原子座標の読み取り
			bool IsOcc = false;
			bool IsisoUsed = false;
			bool IsanisoUsed = false;
			bool IsUtypeUsed = false;
			if (str[n].IndexOf("occ") > 0)
				IsOcc = true;
			if (str[n].IndexOf("Uiso") > 0 || str[n].IndexOf("uiso") > 0)
			{
				IsisoUsed = true;
				IsUtypeUsed = true;
			}
			else if (str[n].IndexOf("Biso") > 0 || str[n].IndexOf("biso") > 0)
			{
				IsisoUsed = true;
				IsUtypeUsed = false;
			}

			if (str[n].IndexOf("U(1,1)") > 0 || str[n].IndexOf("u(1,1)") > 0)
			{
				IsanisoUsed = true;
				IsUtypeUsed = true;
			}
			else if (str[n].IndexOf("B(1,1)") > 0 || str[n].IndexOf("b(1,1)") > 0)
			{
				IsanisoUsed = true;
				IsUtypeUsed = false;
			}

			string[] tempStr = str[n].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			int[] l = new int[tempStr.Length];

			for (int i = 0; i < l.Length; i++)//各入力値の文字位置を決める。
				l[i] = str[n].IndexOf(tempStr[i]) + tempStr[i].Length;
			for (int i = n + 1; i < str.Length; i++)//最初のatomラベルだけ例外があるようなのでそれに対処
				if (l[0] < str[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0].Length)
					l[0] = str[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0].Length;

			crystal.SetAxis();
			double aStar = 1 / crystal.A;
			double bStar = 1 / crystal.B;
			double cStar = 1 / crystal.C;

			//三方あるいは六方
			bool isHex = crystal.Symmetry.SeriesNumber >= 430 && crystal.Symmetry.SeriesNumber <= 488;

			for (int i = n + 1; i < str.Length; i++)
			{//原子座標読み取りループ開始
				str[i] = str[i].PadRight(str[n].Length, ' ');
				double x, y, z, occ;

				int j = 0;
				double Xiso, X11, X22, X33, X12, X13, X23;
				string label;

				label = str[i].Substring(0, l[j]);
				j++;

				x = ConvertToDouble(str[i].Substring(l[j - 1], l[j] - l[j - 1]), isHex) + xShift; j++;
				y = ConvertToDouble(str[i].Substring(l[j - 1], l[j] - l[j - 1]), isHex) + yShift; j++;
				z = ConvertToDouble(str[i].Substring(l[j - 1], l[j] - l[j - 1]), isHex) + zShift; j++;
				if (IsOcc)
				{
					occ = ConvertToDouble(str[i].Substring(l[j - 1], l[j] - l[j - 1]));
					j++;
					if (occ == 0) occ = 1;
				}
				else
					occ = 1;

				if (IsisoUsed)
				{
					Xiso = ConvertToDouble(str[i].Substring(l[j - 1], l[j] - l[j - 1])); j++;
				}
				else
					Xiso = 0;

				if (IsanisoUsed)
				{
					X11 = ConvertToDouble(str[i].Substring(l[j - 1], l[j] - l[j - 1])); j++;
					X22 = ConvertToDouble(str[i].Substring(l[j - 1], l[j] - l[j - 1])); j++;
					X33 = ConvertToDouble(str[i].Substring(l[j - 1], l[j] - l[j - 1])); j++;
					X12 = ConvertToDouble(str[i].Substring(l[j - 1], l[j] - l[j - 1])); j++;
					X13 = ConvertToDouble(str[i].Substring(l[j - 1], l[j] - l[j - 1])); j++;
					X23 = ConvertToDouble(str[i].Substring(l[j - 1], l[j] - l[j - 1])); j++;
				}
				else
					X11 = X22 = X33 = X12 = X13 = X23 = 0;

				DiffuseScatteringFactor dsf;
				if (IsUtypeUsed)
					dsf = new DiffuseScatteringFactor(!IsanisoUsed, Xiso, X11, X22, X33, X12, X13, X23, aStar, bStar, cStar);
				else
					dsf = new DiffuseScatteringFactor(!IsanisoUsed, Xiso, X11, X22, X33, X12, X13, X23);

				//ラベル名から元素を決める
				string temp;
				int atomicNumber = 0;
				//AtomicScatteringFactor asf= new AtomicScatteringFactor();

				for (int q = label.Length; q > 0 && atomicNumber == 0; q--)
				{
					temp = label.Substring(0, q);
					for (int k = 0; k <= 96 && atomicNumber == 0; k++)
					{
						// asf.SetCoefficientForXray(k);

						if (AtomConstants.AtomicName(k) == temp)
						{
							atomicNumber = k;
							q = -1;
							break;
						}
					}
					if (temp == "OH") //OH基のとき
						atomicNumber = -1;
					else if (temp == "D") //重水素のとき
						atomicNumber = 1;
					else if (temp == "Wat" || temp == "WAT" || temp == "wat") //水のとき
						atomicNumber = -2;
				}

				if (atomicNumber > 0)
				{
					/*String element;
					if (asf.AtomicNumber > 10)
						element = asf.AtomicNumber.ToString() + ":  " + asf.AtomicName;
					else
						element = asf.AtomicNumber.ToString() + ":   " + asf.AtomicName;
					*/
					crystal.AddAtoms(new Atoms(label, atomicNumber, 0, 0, null, crystal.SymmetrySeriesNumber, new Vector3D(x, y, z), occ, dsf));
				}
				else if (atomicNumber == -1)//"OH"のときの対処
				{
					/*sfn = 1;
					AtomicScatteringFactor asf = AtomicScatteringFactor.GetCoefficientForXray(sfn);
					String element = asf.AtomicNumber.ToString() + ":   " + asf.AtomicName;*/
					crystal.AddAtoms(new Atoms(label, 1, 0, 0, null, crystal.SymmetrySeriesNumber, new Vector3D(x, y, z), occ, dsf));

					/*sfn = 12;
					asf = AtomicScatteringFactor.GetCoefficientForXray(sfn);
					element = asf.AtomicNumber.ToString() + ":   " + asf.AtomicName;*/
					crystal.AddAtoms(new Atoms(label, 8, 0, 0, null, crystal.SymmetrySeriesNumber, new Vector3D(x, y, z), occ, dsf));
				}
				else if (atomicNumber == -2)//"Wat"水のときの対処
				{
					/*sfn = 1;
					AtomicScatteringFactor asf = AtomicScatteringFactor.GetCoefficientForXray(sfn);
					String element = asf.AtomicNumber.ToString() + ":   " + asf.AtomicName;
					crystal.AddAtoms(new Atoms(label, sfn, crystal.SymmetrySeriesNumber, new Vector3D(x, y, z), occ, dsf));*/

					crystal.AddAtoms(new Atoms(label, 1, 0, 0, null, crystal.SymmetrySeriesNumber, new Vector3D(x, y, z), occ, dsf));
					crystal.AddAtoms(new Atoms(label, 1, 0, 0, null, crystal.SymmetrySeriesNumber, new Vector3D(x, y, z), occ, dsf));

					/*sfn = 12;
					asf = AtomicScatteringFactor.GetCoefficientForXray(sfn);
					element = asf.AtomicNumber.ToString() + ":   " + asf.AtomicName;*/
					crystal.AddAtoms(new Atoms(label, 8, 0, 0, null, crystal.SymmetrySeriesNumber, new Vector3D(x, y, z), occ, dsf));
				}
			}

			SetOpenGL_property(crystal);

			crystal.Name = Name;
			crystal.Journal = Reference;
			crystal.PublAuthorName = AuthorName;
			crystal.PublSectionTitle = Title;

			return crystal;
		}

		private static Crystal CellParamForAmc(string str)
		{
			double A, B, C, Alfa, Beta, Gamma;
			int symmetrySeriesNumber = -1;
			string[] s;
			if (str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length != 7)
				return null;
			s = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			try
			{
				if (Miscellaneous.IsDecimalPointComma)
				{
					s[0] = s[0].Replace('.', ',');
					s[1] = s[1].Replace('.', ',');
					s[2] = s[2].Replace('.', ',');
					s[3] = s[3].Replace('.', ',');
					s[4] = s[4].Replace('.', ',');
					s[5] = s[5].Replace('.', ',');
				}
				A = Convert.ToDouble(s[0]);
				B = Convert.ToDouble(s[1]);
				C = Convert.ToDouble(s[2]);
				Alfa = Convert.ToDouble(s[3]);
				Beta = Convert.ToDouble(s[4]);
				Gamma = Convert.ToDouble(s[5]);
			}
			catch { return null; }
			string SgName = s[6];

			SgName = SgName.Replace("_", "sub");
			bool isAsterisk = SgName.Contains("*");
			SgName = SgName.Replace("*", "");


			#region
			

			if (SgName == "Pm3") SgName = "Pm-3";
			else if (SgName == "Pn3") SgName = "Pn-3";
			else if (SgName == "Fm3") SgName = "Fm-3";
			else if (SgName == "Fd3") SgName = "Fd-3";
			else if (SgName == "Im3") SgName = "Im-3";
			else if (SgName == "Pa3") SgName = "Pa-3";
			else if (SgName == "Ia3") SgName = "Ia-3";

			else if (SgName == "Pm3m") SgName = "Pm-3m";
			else if (SgName == "Pn3n") SgName = "Pm-3m";
			else if (SgName == "Pm3n") SgName = "Pm-3n";
			else if (SgName == "Pn3m") SgName = "Pn-3m";
			else if (SgName == "Fm3m") SgName = "Fm-3m";
			else if (SgName == "Fm3c") SgName = "Fm-3c";
			else if (SgName == "Fd3m") SgName = "Fd-3m";
			else if (SgName == "Fd3c") SgName = "Fd-3c";
			else if (SgName == "Im3m") SgName = "Im-3m";
			else if (SgName == "Ia3d") SgName = "Ia-3d";

			else if (SgName == "I2sub1/a-3") SgName = "Ia3";

			else if (SgName == "R-32/c") SgName = "R-3c";

			else if (SgName == "P2" && Alfa == 90.0 && Gamma == 90.0) SgName = "P121";
			else if (SgName == "P2" && Alfa == 90.0 && Beta == 90.0) SgName = "P112";
			else if (SgName == "P2" && Beta == 90.0 && Gamma == 90.0) SgName = "P211";
			else if (SgName == "P2sub1" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12sub11";
			else if (SgName == "P2sub1" && Alfa == 90.0 && Beta == 90.0) SgName = "P112sub1";
			else if (SgName == "P2sub1" && Beta == 90.0 && Gamma == 90.0) SgName = "P2sub111";
			else if (SgName == "C2" && Alfa == 90.0 && Gamma == 90.0) SgName = "C121";
			else if (SgName == "A2" && Alfa == 90.0 && Gamma == 90.0) SgName = "A121";
			else if (SgName == "I2" && Alfa == 90.0 && Gamma == 90.0) SgName = "I121";
			else if (SgName == "A2" && Alfa == 90.0 && Beta == 90.0) SgName = "A112";
			else if (SgName == "B2" && Alfa == 90.0 && Beta == 90.0) SgName = "B112";
			else if (SgName == "I2" && Alfa == 90.0 && Beta == 90.0) SgName = "I112";
			else if (SgName == "B2" && Beta == 90.0 && Gamma == 90.0) SgName = "B211";
			else if (SgName == "C2" && Beta == 90.0 && Gamma == 90.0) SgName = "C211";
			else if (SgName == "I2" && Beta == 90.0 && Gamma == 90.0) SgName = "I211";
			else if (SgName == "Pm" && Alfa == 90.0 && Gamma == 90.0) SgName = "P1m1";
			else if (SgName == "Pm" && Alfa == 90.0 && Beta == 90.0) SgName = "P11m";
			else if (SgName == "Pm" && Beta == 90.0 && Gamma == 90.0) SgName = "Pm11";
			else if (SgName == "Pc" && Alfa == 90.0 && Gamma == 90.0) SgName = "P1c1";
			else if (SgName == "Pn" && Alfa == 90.0 && Gamma == 90.0) SgName = "P1n1";
			else if (SgName == "Pa" && Alfa == 90.0 && Gamma == 90.0) SgName = "P1a1";
			else if (SgName == "Pa" && Alfa == 90.0 && Beta == 90.0) SgName = "P11a";
			else if (SgName == "Pn" && Alfa == 90.0 && Beta == 90.0) SgName = "P11n";
			else if (SgName == "Pb" && Alfa == 90.0 && Beta == 90.0) SgName = "P11b";
			else if (SgName == "Pb" && Beta == 90.0 && Gamma == 90.0) SgName = "Pb11";
			else if (SgName == "Pn" && Beta == 90.0 && Gamma == 90.0) SgName = "Pn11";
			else if (SgName == "Pc" && Beta == 90.0 && Gamma == 90.0) SgName = "Pc11";
			else if (SgName == "Cm" && Alfa == 90.0 && Gamma == 90.0) SgName = "C1m1";
			else if (SgName == "Am" && Alfa == 90.0 && Gamma == 90.0) SgName = "A1m1";
			else if (SgName == "Im" && Alfa == 90.0 && Gamma == 90.0) SgName = "I1m1";
			else if (SgName == "Am" && Alfa == 90.0 && Beta == 90.0) SgName = "A11m";
			else if (SgName == "Bm" && Alfa == 90.0 && Beta == 90.0) SgName = "B11m";
			else if (SgName == "Im" && Alfa == 90.0 && Beta == 90.0) SgName = "I11m";
			else if (SgName == "Bm" && Beta == 90.0 && Gamma == 90.0) SgName = "Bm11";
			else if (SgName == "Cm" && Beta == 90.0 && Gamma == 90.0) SgName = "Cm11";
			else if (SgName == "Im" && Beta == 90.0 && Gamma == 90.0) SgName = "Im11";
			else if (SgName == "Cc" && Alfa == 90.0 && Gamma == 90.0) SgName = "C1c1";
			else if (SgName == "An" && Alfa == 90.0 && Gamma == 90.0) SgName = "A1n1";
			else if (SgName == "Ia" && Alfa == 90.0 && Gamma == 90.0) SgName = "I1a1";
			else if (SgName == "Aa" && Alfa == 90.0 && Gamma == 90.0) SgName = "A1a1";
			else if (SgName == "Cn" && Alfa == 90.0 && Gamma == 90.0) SgName = "C1n1";
			else if (SgName == "Ic" && Alfa == 90.0 && Gamma == 90.0) SgName = "I1c1";
			else if (SgName == "Aa" && Alfa == 90.0 && Beta == 90.0) SgName = "A11a";
			else if (SgName == "Bn" && Alfa == 90.0 && Beta == 90.0) SgName = "B11n";
			else if (SgName == "Ib" && Alfa == 90.0 && Beta == 90.0) SgName = "I11b";
			else if (SgName == "Bb" && Alfa == 90.0 && Beta == 90.0) SgName = "B11b";
			else if (SgName == "An" && Alfa == 90.0 && Beta == 90.0) SgName = "A11n";
			else if (SgName == "Ia" && Alfa == 90.0 && Beta == 90.0) SgName = "I11a";
			else if (SgName == "Bb" && Beta == 90.0 && Gamma == 90.0) SgName = "Bb11";
			else if (SgName == "Cn" && Beta == 90.0 && Gamma == 90.0) SgName = "Cn11";
			else if (SgName == "Ic" && Beta == 90.0 && Gamma == 90.0) SgName = "Ic11";
			else if (SgName == "Cc" && Beta == 90.0 && Gamma == 90.0) SgName = "Cc11";
			else if (SgName == "Bn" && Beta == 90.0 && Gamma == 90.0) SgName = "Bn11";
			else if (SgName == "Ib" && Beta == 90.0 && Gamma == 90.0) SgName = "Ib11";
			else if (SgName == "P2/m" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12/m1";
			else if (SgName == "P2/m" && Alfa == 90.0 && Beta == 90.0) SgName = "P112/m";
			else if (SgName == "P2/m" && Beta == 90.0 && Gamma == 90.0) SgName = "P2/m11";
			else if (SgName == "P2sub/m" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12sub1/m1";
			else if (SgName == "P2sub/m" && Alfa == 90.0 && Beta == 90.0) SgName = "P112sub1/m";
			else if (SgName == "P2sub/m" && Beta == 90.0 && Gamma == 90.0) SgName = "P2sub1/m11";
			else if (SgName == "C2/m" && Alfa == 90.0 && Gamma == 90.0) SgName = "C12/m1";
			else if (SgName == "A2/m" && Alfa == 90.0 && Gamma == 90.0) SgName = "A12/m1";
			else if (SgName == "I2/m" && Alfa == 90.0 && Gamma == 90.0) SgName = "I12/m1";
			else if (SgName == "A2/m" && Alfa == 90.0 && Beta == 90.0) SgName = "A112/m";
			else if (SgName == "B2/m" && Alfa == 90.0 && Beta == 90.0) SgName = "B112/m";
			else if (SgName == "I2/m" && Alfa == 90.0 && Beta == 90.0) SgName = "I112/m";
			else if (SgName == "B2/m" && Beta == 90.0 && Gamma == 90.0) SgName = "B2/m11";
			else if (SgName == "C2/m" && Beta == 90.0 && Gamma == 90.0) SgName = "C2/m11";
			else if (SgName == "I2/m" && Beta == 90.0 && Gamma == 90.0) SgName = "I2/m11";
			else if (SgName == "P2/c" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12/c1";
			else if (SgName == "P2/n" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12/n1";
			else if (SgName == "P2/a" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12/a1";
			else if (SgName == "P2/a" && Alfa == 90.0 && Beta == 90.0) SgName = "P112/a";
			else if (SgName == "P2/n" && Alfa == 90.0 && Beta == 90.0) SgName = "P112/n";
			else if (SgName == "P2/b" && Alfa == 90.0 && Beta == 90.0) SgName = "P112/b";
			else if (SgName == "P2/b" && Beta == 90.0 && Gamma == 90.0) SgName = "P2/b11";
			else if (SgName == "P2/n" && Beta == 90.0 && Gamma == 90.0) SgName = "P2/n11";
			else if (SgName == "P2/c" && Beta == 90.0 && Gamma == 90.0) SgName = "P2/c11";
			else if (SgName == "P2sub1/c" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12sub1/c1";
			else if (SgName == "P2sub1/n" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12sub1/n1";
			else if (SgName == "P2sub1/a" && Alfa == 90.0 && Gamma == 90.0) SgName = "P12sub1/a1";
			else if (SgName == "P2sub1/a" && Alfa == 90.0 && Beta == 90.0) SgName = "P112sub1/a";
			else if (SgName == "P2sub1/n" && Alfa == 90.0 && Beta == 90.0) SgName = "P112sub1/n";
			else if (SgName == "P2sub1/b" && Alfa == 90.0 && Beta == 90.0) SgName = "P112sub1/b";
			else if (SgName == "P2sub1/b" && Beta == 90.0 && Gamma == 90.0) SgName = "P2sub1/b11";
			else if (SgName == "P2sub1/n" && Beta == 90.0 && Gamma == 90.0) SgName = "P2sub1/n11";
			else if (SgName == "P2sub1/c" && Beta == 90.0 && Gamma == 90.0) SgName = "P2sub1/c11";
			else if (SgName == "C2/c" && Alfa == 90.0 && Gamma == 90.0) SgName = "C12/c1";
			else if (SgName == "A2/n" && Alfa == 90.0 && Gamma == 90.0) SgName = "A12/n1";
			else if (SgName == "I2/a" && Alfa == 90.0 && Gamma == 90.0) SgName = "I12/a1";
			else if (SgName == "A2/a" && Alfa == 90.0 && Gamma == 90.0) SgName = "A12/a1";
			else if (SgName == "C2/n" && Alfa == 90.0 && Gamma == 90.0) SgName = "C12/n1";
			else if (SgName == "I2/c" && Alfa == 90.0 && Gamma == 90.0) SgName = "I12/c1";
			else if (SgName == "A2/a" && Alfa == 90.0 && Beta == 90.0) SgName = "A112/a";
			else if (SgName == "B2/n" && Alfa == 90.0 && Beta == 90.0) SgName = "B112/n";
			else if (SgName == "I2/b" && Alfa == 90.0 && Beta == 90.0) SgName = "I112/b";
			else if (SgName == "B2/b" && Alfa == 90.0 && Beta == 90.0) SgName = "B112/b";
			else if (SgName == "A2/n" && Alfa == 90.0 && Beta == 90.0) SgName = "A112/n";
			else if (SgName == "I2/a" && Alfa == 90.0 && Beta == 90.0) SgName = "I112/a";
			else if (SgName == "B2/b" && Beta == 90.0 && Gamma == 90.0) SgName = "B2/b11";
			else if (SgName == "C2/n" && Beta == 90.0 && Gamma == 90.0) SgName = "C2/n11";
			else if (SgName == "I2/c" && Beta == 90.0 && Gamma == 90.0) SgName = "I2/c11";
			else if (SgName == "C2/c" && Beta == 90.0 && Gamma == 90.0) SgName = "C2/c11";
			else if (SgName == "B2/n" && Beta == 90.0 && Gamma == 90.0) SgName = "B2/n11";
			else if (SgName == "I2/b" && Beta == 90.0 && Gamma == 90.0) SgName = "I2/b11";
			#endregion amcファイルの読み込み

			//文字列を含んでいて、かつ、文字数が少ない空間群を選択する (C1とC121などを見分けるため)
			int length = int.MaxValue;

			var sg = SymmetryStatic.SpaceGroupListWithoutSpace;
			for (int i = 0; i < SymmetryStatic.TotalSpaceGroupNumber; i++)
			{
				if (sg[i].Length < length)
				{
					var sg_list = new List<string>(new[] { sg[i] });

					if(sg[i].Contains("e"))
					{
						sg_list.Add(sg[i].Replace('e', 'a'));
						sg_list.Add(sg[i].Replace('e', 'b'));
						sg_list.Add(sg[i].Replace('e', 'c'));
					}

					if ( sg_list.Any(s => s.IndexOf(SgName, 0) != -1))
					{
						symmetrySeriesNumber = i;
						length = sg[i].Length;
					}
				}
			}
			if (symmetrySeriesNumber == -1)
				return null;
			//Rhombohedoralのときの処置
			if (A == B && B == C && Alfa == Beta && Beta == Gamma && SymmetryStatic.Get_Symmetry(symmetrySeriesNumber).SpaceGroupHMStr.IndexOf("Hex") >= 0)
				symmetrySeriesNumber++;

			//Asteriskの時(2nd setting)の処理
			if(isAsterisk && sg[symmetrySeriesNumber].EndsWith("(1)"))
			 symmetrySeriesNumber++;


			if (symmetrySeriesNumber >= 0)
			{
				var r = new Random();
				return new Crystal((A / 10, B / 10, C / 10, Alfa * Math.PI / 180, Beta * Math.PI / 180, Gamma * Math.PI / 180),null,
					symmetrySeriesNumber, "",  Color.FromArgb(r.Next(255), r.Next(255), r.Next(255)));
			}
			else
				return null;
		}

		#endregion

		#region OpenGLのためのBond設定
		public static void SetOpenGL_property(Crystal c)
		{
			foreach (Atoms a in c.Atoms)
				a.ResetVesta();
			c.Bonds = Bonds.GetVestaBonds(c.Atoms.Select(a => a.ElementName).Distinct());

			#region お蔵入り
			/*
			//先ず原子の色、半径を設定
			foreach (Atoms a in c.Atoms)
			{
				switch (a.ElementName)
				{
					#region イオン半径、色を設定
					case "1: H": a.Radius = 0.005f; a.Argb = Color.FromArgb(98, 11, 181).ToArgb(); break;
					case "2: He": a.Radius = 0.05f; a.Argb = Color.FromArgb(137, 250, 106).ToArgb(); break;
					case "3: Li": a.Radius = 0.38f; a.Argb = Color.FromArgb(43, 200, 157).ToArgb(); break;
					case "4: Be": a.Radius = 0.135f; a.Argb = Color.FromArgb(161, 34, 128).ToArgb(); break;
					case "5: B": a.Radius = 0.055f; a.Argb = Color.FromArgb(249, 251, 174).ToArgb(); break;
					case "6: C": a.Radius = 0.075f; a.Argb = Color.FromArgb(211, 45, 173).ToArgb(); break;
					case "7: N": a.Radius = 0.08f; a.Argb = Color.FromArgb(248, 240, 241).ToArgb(); break;
					case "8: O": a.Radius = 0.71f; a.Argb = Color.FromArgb(255, 0, 0).ToArgb(); break;
					case "9: F": a.Radius = 0.665f; a.Argb = Color.FromArgb(153, 165, 227).ToArgb(); break;
					case "10: Ne": a.Radius = 0.005f; a.Argb = Color.FromArgb(65, 223, 157).ToArgb(); break;
					case "11: Na": a.Radius = 0.59f; a.Argb = Color.FromArgb(187, 195, 36).ToArgb(); break;
					case "12: Mg": a.Radius = 0.445f; a.Argb = Color.FromArgb(193, 162, 107).ToArgb(); break;
					case "13: Al": a.Radius = 0.195f; a.Argb = Color.FromArgb(153, 46, 158).ToArgb(); break;
					case "14: Si": a.Radius = 0.20f; a.Argb = Color.FromArgb(0, 0, 255).ToArgb(); break;
					case "15: P": a.Radius = 0.145f; a.Argb = Color.FromArgb(101, 199, 183).ToArgb(); break;
					case "16: S": a.Radius = 0.92f; a.Argb = Color.FromArgb(87, 191, 62).ToArgb(); break;
					case "17: Cl": a.Radius = 0.905f; a.Argb = Color.FromArgb(23, 36, 16).ToArgb(); break;
					case "18: Ar": a.Radius = 0.05f; a.Argb = Color.FromArgb(203, 82, 236).ToArgb(); break;
					case "19: K": a.Radius = 0.755f; a.Argb = Color.FromArgb(216, 96, 67).ToArgb(); break;
					case "20: Ca": a.Radius = 0.56f; a.Argb = Color.FromArgb(35, 159, 207).ToArgb(); break;
					case "21: Sc": a.Radius = 0.435f; a.Argb = Color.FromArgb(165, 191, 130).ToArgb(); break;
					case "22: Ti": a.Radius = 0.25f; a.Argb = Color.FromArgb(64, 112, 111).ToArgb(); break;
					case "23: V": a.Radius = 0.29f; a.Argb = Color.FromArgb(158, 54, 69).ToArgb(); break;
					case "24: Cr": a.Radius = 0.3075f; a.Argb = Color.FromArgb(43, 48, 188).ToArgb(); break;
					case "25: Mn": a.Radius = 0.29f; a.Argb = Color.FromArgb(69, 122, 233).ToArgb(); break;
					case "26: Fe": a.Radius = 0.400f; a.Argb = Color.FromArgb(23, 239, 57).ToArgb(); break;
					case "27: Co": a.Radius = 0.305f; a.Argb = Color.FromArgb(119, 8, 111).ToArgb(); break;
					case "28: Ni": a.Radius = 0.3f; a.Argb = Color.FromArgb(190, 121, 182).ToArgb(); break;
					case "29: Cu": a.Radius = 0.285f; a.Argb = Color.FromArgb(215, 226, 61).ToArgb(); break;
					case "30: Zn": a.Radius = 0.37f; a.Argb = Color.FromArgb(122, 74, 64).ToArgb(); break;
					case "31: Ga": a.Radius = 0.31f; a.Argb = Color.FromArgb(22, 97, 30).ToArgb(); break;
					case "32: Ge": a.Radius = 0.265f; a.Argb = Color.FromArgb(60, 177, 233).ToArgb(); break;
					case "33: As": a.Radius = 0.23f; a.Argb = Color.FromArgb(159, 16, 53).ToArgb(); break;
					case "34: Se": a.Radius = 0.99f; a.Argb = Color.FromArgb(250, 56, 55).ToArgb(); break;
					case "35: Br": a.Radius = 0.98f; a.Argb = Color.FromArgb(221, 111, 159).ToArgb(); break;
					case "36: Kr": a.Radius = 0.05f; a.Argb = Color.FromArgb(225, 104, 113).ToArgb(); break;
					case "37: Rb": a.Radius = 0.805f; a.Argb = Color.FromArgb(51, 135, 205).ToArgb(); break;
					case "38: Sr": a.Radius = 0.63f; a.Argb = Color.FromArgb(158, 31, 36).ToArgb(); break;
					case "39: Y": a.Radius = 0.45f; a.Argb = Color.FromArgb(131, 105, 94).ToArgb(); break;
					case "40: Zr": a.Radius = 0.36f; a.Argb = Color.FromArgb(237, 15, 134).ToArgb(); break;
					case "41: Nb": a.Radius = 0.32f; a.Argb = Color.FromArgb(236, 148, 12).ToArgb(); break;
					case "42: Mo": a.Radius = 0.295f; a.Argb = Color.FromArgb(246, 92, 178).ToArgb(); break;
					case "43: Tc": a.Radius = 0.3f; a.Argb = Color.FromArgb(103, 200, 138).ToArgb(); break;
					case "44: Ru": a.Radius = 0.18f; a.Argb = Color.FromArgb(227, 4, 84).ToArgb(); break;
					case "45: Rh": a.Radius = 0.275f; a.Argb = Color.FromArgb(191, 235, 99).ToArgb(); break;
					case "46: Pd": a.Radius = 0.3075f; a.Argb = Color.FromArgb(150, 156, 76).ToArgb(); break;
					case "47: Ag": a.Radius = 0.47f; a.Argb = Color.FromArgb(55, 51, 185).ToArgb(); break;
					case "48: Cd": a.Radius = 0.475f; a.Argb = Color.FromArgb(84, 100, 150).ToArgb(); break;
					case "49: In": a.Radius = 0.4f; a.Argb = Color.FromArgb(47, 80, 243).ToArgb(); break;
					case "50: Sn": a.Radius = 0.345f; a.Argb = Color.FromArgb(180, 73, 238).ToArgb(); break;
					case "51: Sb": a.Radius = 0.38f; a.Argb = Color.FromArgb(210, 79, 249).ToArgb(); break;
					case "52: Te": a.Radius = 0.485f; a.Argb = Color.FromArgb(92, 27, 100).ToArgb(); break;
					case "53: I": a.Radius = 1.1f; a.Argb = Color.FromArgb(224, 110, 46).ToArgb(); break;
					case "54: Xe": a.Radius = 0.2f; a.Argb = Color.FromArgb(169, 220, 216).ToArgb(); break;
					case "55: Cs": a.Radius = 0.87f; a.Argb = Color.FromArgb(244, 226, 13).ToArgb(); break;
					case "56: Ba": a.Radius = 0.71f; a.Argb = Color.FromArgb(177, 2, 109).ToArgb(); break;
					case "57: La": a.Radius = 0.58f; a.Argb = Color.FromArgb(194, 79, 196).ToArgb(); break;
					case "58: Ce": a.Radius = 0.5715f; a.Argb = Color.FromArgb(88, 221, 142).ToArgb(); break;
					case "59: Pr": a.Radius = 0.563f; a.Argb = Color.FromArgb(247, 156, 100).ToArgb(); break;
					case "60: Nd": a.Radius = 0.4915f; a.Argb = Color.FromArgb(252, 189, 121).ToArgb(); break;
					case "61: Pm": a.Radius = 0.5465f; a.Argb = Color.FromArgb(209, 104, 146).ToArgb(); break;
					case "62: Sm": a.Radius = 0.479f; a.Argb = Color.FromArgb(133, 7, 117).ToArgb(); break;
					case "63: Eu": a.Radius = 0.625f; a.Argb = Color.FromArgb(226, 218, 141).ToArgb(); break;
					case "64: Gd": a.Radius = 0.5265f; a.Argb = Color.FromArgb(26, 83, 227).ToArgb(); break;
					case "65: Tb": a.Radius = 0.52f; a.Argb = Color.FromArgb(160, 216, 140).ToArgb(); break;
					case "66: Dy": a.Radius = 0.485f; a.Argb = Color.FromArgb(13, 71, 184).ToArgb(); break;
					case "67: Ho": a.Radius = 0.5075f; a.Argb = Color.FromArgb(104, 109, 153).ToArgb(); break;
					case "68: Er": a.Radius = 0.502f; a.Argb = Color.FromArgb(21, 168, 23).ToArgb(); break;
					case "69: Tm": a.Radius = 0.497f; a.Argb = Color.FromArgb(221, 220, 184).ToArgb(); break;
					case "70: Yb": a.Radius = 0.434f; a.Argb = Color.FromArgb(23, 151, 178).ToArgb(); break;
					case "71: Lu": a.Radius = 0.4305f; a.Argb = Color.FromArgb(108, 190, 142).ToArgb(); break;
					case "72: Hf": a.Radius = 0.355f; a.Argb = Color.FromArgb(84, 253, 37).ToArgb(); break;
					case "73: Ta": a.Radius = 0.34f; a.Argb = Color.FromArgb(160, 247, 238).ToArgb(); break;
					case "74: W": a.Radius = 0.21f; a.Argb = Color.FromArgb(4, 203, 171).ToArgb(); break;
					case "75: Re": a.Radius = 0.275f; a.Argb = Color.FromArgb(87, 31, 14).ToArgb(); break;
					case "76: Os": a.Radius = 0.245f; a.Argb = Color.FromArgb(218, 170, 3).ToArgb(); break;
					case "77: Ir": a.Radius = 0.3125f; a.Argb = Color.FromArgb(245, 219, 180).ToArgb(); break;
					case "78: Pt": a.Radius = 0.3125f; a.Argb = Color.FromArgb(43, 17, 113).ToArgb(); break;
					case "79: Au": a.Radius = 0.425f; a.Argb = Color.FromArgb(77, 63, 211).ToArgb(); break;
					case "80: Hg": a.Radius = 0.48f; a.Argb = Color.FromArgb(62, 50, 156).ToArgb(); break;
					case "81: Tl": a.Radius = 0.375f; a.Argb = Color.FromArgb(80, 119, 140).ToArgb(); break;
					case "82: Pb": a.Radius = 0.645f; a.Argb = Color.FromArgb(243, 58, 152).ToArgb(); break;
					case "83: Bi": a.Radius = 0.515f; a.Argb = Color.FromArgb(123, 248, 40).ToArgb(); break;
					case "84: Po": a.Radius = 0.335f; a.Argb = Color.FromArgb(213, 176, 49).ToArgb(); break;
					case "85: At": a.Radius = 0.31f; a.Argb = Color.FromArgb(230, 228, 32).ToArgb(); break;
					case "86: Rn": a.Radius = 0.05f; a.Argb = Color.FromArgb(115, 62, 60).ToArgb(); break;
					case "87: Fr": a.Radius = 0.9f; a.Argb = Color.FromArgb(155, 85, 177).ToArgb(); break;
					case "88: Ra": a.Radius = 0.85f; a.Argb = Color.FromArgb(199, 47, 152).ToArgb(); break;
					case "89: Ac": a.Radius = 0.56f; a.Argb = Color.FromArgb(214, 227, 230).ToArgb(); break;
					case "90: Th": a.Radius = 0.525f; a.Argb = Color.FromArgb(153, 241, 22).ToArgb(); break;
					case "91: Pa": a.Radius = 0.45f; a.Argb = Color.FromArgb(80, 156, 227).ToArgb(); break;
					case "92: U": a.Radius = 0.365f; a.Argb = Color.FromArgb(44, 59, 209).ToArgb(); break;
					case "93: Np": a.Radius = 0.49f; a.Argb = Color.FromArgb(10, 253, 55).ToArgb(); break;
					case "94: Pu": a.Radius = 0.48f; a.Argb = Color.FromArgb(95, 160, 240).ToArgb(); break;
					case "95: Am": a.Radius = 0.425f; a.Argb = Color.FromArgb(252, 12, 39).ToArgb(); break;
					case "96: Cm": a.Radius = 0.425f; a.Argb = Color.FromArgb(217, 174, 152).ToArgb(); break;
					case "97: Bk": a.Radius = 0.415f; a.Argb = Color.FromArgb(54, 244, 120).ToArgb(); break;
					case "98: Cf": a.Radius = 0.4105f; a.Argb = Color.FromArgb(161, 45, 164).ToArgb(); break;
						#endregion イオン半径、色を設定
				}
				if (!elementList.Contains(a.ElementName))
					elementList.Add(a.ElementName);
			}
			*/
			#endregion


		}





		#endregion
		private static double ConvertToDouble(string str)
		{
			return ConvertToDouble(str, false);
		}

		private static double ConvertToDouble(string str, bool IsHex)
		{
			try
			{
				if (str.Trim().Length == 0)
					return 0;
				else if (str.IndexOf('/') > 0)
				{
					string[] temp = str.Split('/');
					if (temp.Length == 2)
						return Convert.ToDouble(temp[0]) / Convert.ToDouble(temp[1]);
					else
						return 0;
				}
				else if (IsHex)
				{
					if (str.Contains(".0833")) return 1.0 / 12.0;
					else if (str.Contains(".1667")) return 1.0 / 6.0;
					else if (str.Contains(".33333")) return 1.0 / 3.0;
					else if (str.Contains(".3333")) return 1.0 / 3.0;
					else if (str.Contains(".4167")) return 5.0 / 12.0;
					else if (str.Contains(".5833")) return 7.0 / 12.0;
					else if (str.Contains(".66667")) return 2.0 / 3.0;
					else if (str.Contains(".6667")) return 2.0 / 3.0;
					else if (str.Contains(".8333")) return 5.0 / 6.0;
					else if (str.Contains(".9167")) return 11.0 / 12.0;

					if (str.Contains(",0833")) return 1.0 / 12.0;
					else if (str.Contains(",1667")) return 1.0 / 6.0;
					else if (str.Contains(",33333")) return 1.0 / 3.0;
					else if (str.Contains(",3333")) return 1.0 / 3.0;
					else if (str.Contains(",4167")) return 5.0 / 12.0;
					else if (str.Contains(",5833")) return 7.0 / 12.0;
					else if (str.Contains(",66667")) return 2.0 / 3.0;
					else if (str.Contains(",6667")) return 2.0 / 3.0;
					else if (str.Contains(",8333")) return 5.0 / 6.0;
					else if (str.Contains(",9167")) return 11.0 / 12.0;
				}

				return Convert.ToDouble(str);
			}
			catch
			{
				return 0;
			}
		}

		#region CIFファイルの読み込み

		/// <summary>
		/// CIFファイルを読み込む
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		private static Crystal ConvertFromCIF(string[] str)
		{
			//System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			//まず ;と; で囲まれている複数にわたる行を一行にする
			List<string> tempStr = new List<string>();
			string note = "";
			if (str[0].StartsWith("data"))
				note = str[0];
			for (int n = 0; n < str.Length; n++)
			{
				while ((str[n].StartsWith("#") || str[n].Trim().Length == 0) && n < str.Length - 1)
					n++;
				//全ての先頭行の空白あるいはタブを削除する
				str[n] = str[n].Replace("\t", " ");
				str[n] = str[n].TrimEnd(' ').TrimStart(' ');

				if (str[n].Trim().StartsWith(";"))//;で始まる行を見つけたら
				{
					StringBuilder temp = new StringBuilder();
					//次に;が出てくるところまですすめてまとめて一行にする
					while (true)
					{
						temp.Append(str[n] + " ");
						n++;
						if (n > str.Length - 1 || str[n].TrimEnd(' ').StartsWith(";"))
							break;
					}
					tempStr.Add("'" + (temp.ToString()).Trim().TrimStart(';').TrimEnd(';').TrimStart(' ').TrimEnd(' ') + "'");
				}
				else
					tempStr.Add(str[n]);
			}

			//次に'あるいは"で囲まれている文字列中の空白を偶然出てこないような文字列に変換する
			for (int n = 0; n < tempStr.Count; n++)
			{
				if (tempStr[n].IndexOf("''") > -1)//''という文字列が含まれていたら
				{
					string temp = tempStr[n].Remove(0, tempStr[n].IndexOf("'"));
					temp = temp.Replace("''", "ここにはスペースがはいります。");
					tempStr[n] = tempStr[n].Remove(tempStr[n].IndexOf("'")) + temp;
				}

				if (tempStr[n].IndexOf("'") > -1)//'が含まれていたら
				{
					//string temp = tempStr[n].Remove(0, tempStr[n].IndexOf("'"));
					//temp = temp.Replace("'", "").TrimEnd(' ').TrimStart(' ').Replace(" ", "ここにはスペースがはいります。");
					//tempStr[n] = tempStr[n].Remove(tempStr[n].IndexOf("'")) + temp;

					while (tempStr[n].IndexOf("'") > -1)
					{
						int firstIndex = tempStr[n].IndexOf("'");
						int next = tempStr[n].IndexOf("'", firstIndex + 1);
						if (next == -1)
							break;
						string substring = tempStr[n].Substring(firstIndex, next - firstIndex + 1);
						substring = substring.Replace(" ", "ここにはスペースがはいります。");
						substring = substring.Replace("'", "");

						tempStr[n] = tempStr[n].Substring(0, firstIndex) + substring + tempStr[n].Substring(next + 1);
					}
				}

				if (tempStr[n].IndexOf("\"") > -1)//#\"が含まれていたら
				{
					string temp = tempStr[n].Remove(0, tempStr[n].IndexOf("\""));
					temp = temp.Replace("\"", "").TrimEnd(' ').TrimStart(' ').Replace(" ", "ここにはスペースがはいります。");
					tempStr[n] = tempStr[n].Remove(tempStr[n].IndexOf("\"")) + temp;
				}
			}

			//次にloop_に続く行が　"label  data"だった時に対応
			for (int n = 0; n < tempStr.Count - 1; n++)
			{
				if (tempStr[n].StartsWith("loop_") && tempStr[n + 1].IndexOf(" ") > -1)
				{
					string[] temp = tempStr[n + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					tempStr[n + 1] = temp[0];
					tempStr.Insert(n + 2, temp[1]);
				}
			}
			str = tempStr.ToArray();

			List<CIF_Group> CIF = new List<CIF_Group>();
			string tempLabel, tempData;
			CIF_Group cif_Group;
			List<string> tempLoopLabels = new List<string>();
			List<string> tempLoopDatas = new List<string>();

			for (int n = 0; n < str.Length; n++)
			{
				cif_Group = new CIF_Group();
				cif_Group.items = new List<CIF_Item>();

				if (str[n].Trim().StartsWith("_"))
				{//単体アイテムのとき
					string[] temp;
					temp = str[n].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					tempLabel = temp[0];
					if (temp.Length == 2)
						tempData = temp[1];
					else if (temp.Length == 1)
					{
						n++;
						tempData = str[n];
					}
					else
						tempData = "";
					cif_Group.AddItem(new CIF_Item(tempLabel, tempData.Replace("ここにはスペースがはいります。", " ")));
					CIF.Add(cif_Group);
				}
				else if (str[n].Trim().StartsWith("loop_"))
				{//ループのとき
					string[] temp;
					tempLoopLabels = new List<string>();
					tempLoopDatas = new List<string>();
					n++;
					//"_"で始まるラベルを数える
					while (n < str.Length && str[n].Trim().StartsWith("_"))
					{
						tempLoopLabels.Add(str[n].Trim());
						n++;
					}
					//次に"_"か"loop_"で始まる行が出てくるまでループ
					while (n < str.Length && !str[n].Trim().StartsWith("_") && !str[n].Trim().StartsWith("loop_"))
					{
						temp = str[n].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
						for (int i = 0; i < temp.Length; i++)
							tempLoopDatas.Add(temp[i]);
						n++;
					}
					n--;
					if (tempLoopDatas.Count % tempLoopLabels.Count == 0)
					{
						for (int i = 0; i < tempLoopDatas.Count / tempLoopLabels.Count; i++)
						{
							cif_Group.items = new List<CIF_Item>();
							for (int j = 0; j < tempLoopLabels.Count; j++)
							{
								cif_Group.AddItem(new CIF_Item(tempLoopLabels[j], tempLoopDatas[i * tempLoopLabels.Count + j].Replace("ここにはスペースがはいります。", " ")));
							}
							CIF.Add(cif_Group);
						}
					}
				}
			}
			//ここまででCIF_Groupクラスのリストが完成

			//重複するCIFのlabelがあったときは、最初を残して、あとは削除する => 消す必要なし. 
			//for (int i = 0; i < CIF.Count; i++)
			//    for (int j = i + 1; j < CIF.Count; j++)
			//        if (CIF[i].items.Count == 1 && CIF[j].items.Count == 1 && CIF[i].items[0].label == CIF[j].items[0].label)
			//        {
			//            CIF.RemoveAt(j);
			//            j--;
			//        }

			double a = 0, b = 0, c = 0, alpha = 90, beta = 90, gamma = 90;
			double a_err = 0, b_err = 0, c_err = 0, alpha_err = 0, beta_err = 0, gamma_err = 0;
			string name = "", sectionTitle = "", journalNameFull = "", journalCodenASTM = "", label = "", data = "";
			string volume = "", year = "", pageFirst = "", pageLast = "", issue = "";
			string journal = "", spaceGroupNameHM = "", spaceGroupNameHall = "", chemical_formula_sum = "", chemical_formula_structural = "";
			int symmetry_Int_Tables_number = -1;
			List<string> author = new List<string>();
			List<string> operations = new List<string>();


			for (int i = 0; i < CIF.Count; i++)
				for (int j = 0; j < CIF[i].items.Count; j++)
				{
					label = CIF[i].items[j].label;
					data = CIF[i].items[j].data;
					if (label.StartsWith("_chemical_name"))
					{
						if (name == "")
							name = data;
						else
							name += " " + data;
					}

					switch (label)
					{
						//ここから格子定数
						case "_cell_length_a":
							a = ConvertToDoubleForCIF(data);
							a_err = ConvertErrForCIF(data);
							break;

						case "_cell_length_b":
							b = ConvertToDoubleForCIF(data);
							b_err = ConvertErrForCIF(data);
							break;

						case "_cell_length_c":
							c = ConvertToDoubleForCIF(data);
							c_err = ConvertErrForCIF(data);
							break;

						case "_cell_angle_alpha":
							alpha = ConvertToDoubleForCIF(data);
							alpha_err = ConvertErrForCIF(data);
							break;

						case "_cell_angle_beta":
							beta = ConvertToDoubleForCIF(data);
							beta_err = ConvertErrForCIF(data);
							break;

						case "_cell_angle_gamma":
							gamma = ConvertToDoubleForCIF(data);
							gamma_err = ConvertErrForCIF(data);
							break;
						//ここからジャーナル情報
						case "_publ_author_name": author.Add(data); break;
						case "_publ_section_title": sectionTitle = data; break;
						case "_journal_name_full": journalNameFull = data; break;
						case "_journal_coden_ASTM": journalCodenASTM = data; break;
						case "_journal_year": year = data; break;
						case "_journal_volume": volume = data; break;
						case "_journal_page_first": pageFirst = data; break;
						case "_journal_page_last": pageLast = data; break;
						case "_journal_issue": issue = data; break;
						//ここから対称性
						case "_symmetry_space_group_name_H-M": spaceGroupNameHM = data; break;
						case "_symmetry_space_group_name_Hall": spaceGroupNameHall = data; break;
						case "_symmetry_Int_Tables_number": int.TryParse(data, out symmetry_Int_Tables_number); break;
						case "_chemical_formula_sum": chemical_formula_sum = data; break;
						case "_chemical_formula_structural": chemical_formula_structural = data; break;
						case "_space_group_symop_operation_xyz": operations.Add(data);break;
						case "_symmetry_equiv_pos_as_xyz": operations.Add(data);break;
					}
				}


			if (name == "" || name == "?" || name == "? ?" || name.Trim() == "")
				name = chemical_formula_sum;



			#region 空間群を調べる部分
			//空間群を検索
			int sgnum = 0;
			if (spaceGroupNameHM == "" && spaceGroupNameHall == "")
				sgnum = 0;
			else
				sgnum = SearchSGseriesNumberForCIF(spaceGroupNameHM, spaceGroupNameHall, symmetry_Int_Tables_number, a, b, c, alpha, beta, gamma);
			if (sgnum == -1)
				return null;

			#region 対象操作がCIFファイル中に記載されている場合は、本当に現在の空間群でよいかどうかをチェック
			var p = new V3(0.111, 0.234, 0.457);//適当な一般位置
			var tempAtom = WyckoffPosition.GetEquivalentAtomsPosition((p.X, p.Y, p.Z), sgnum).Atom;
			var shift = new V3(0, 0, 0);
			if (operations.Count != 0 && operations.Count == tempAtom.Count)
			{
				var th = 0.0000001;
				var prms = new[] { "x", "y", "z" }.Select(s => Expression.Parameter(typeof(double), s)).ToArray();
				//文字列からラムダ式を返すローカル関数
				Func<double, double, double, V3> func(string sExpr)
				{
					try
					{
						sExpr = sExpr.Replace(",+", ",").TrimStart(new[] { '+' });
						sExpr = "new [] {" + sExpr.Replace("/", ".0/").Replace(".0.0", ".0") + "}";//分子に小数点を加える
					   
						var f = DynamicExpressionParser.ParseLambda(prms, typeof(double[]), sExpr).Compile() as Func<double, double, double, double[]>;
						return (x, y, z) => { var d = f(x, y, z); return new V3(d[0], d[1], d[2]); };
					}
					catch (Exception e)
					{
						return null;
					}
				}

				var funcs = operations.Select(s => func(s)).ToArray();
				if (funcs.All(f => f != null))
				{
					var shiftCandidates = new[] { 0, 0.125, 0.25, -0.125, -0.25 };

					var temp = tempAtom.Select(a => norm(new V3(a.X, a.Y, a.Z))).ToList();
					temp.Sort((o1, o2) => Math.Abs(o1.X-o2.X)> th ? o1.X.CompareTo(o2.X) : (Math.Abs(o1.Y - o2.Y)>th ? o1.Y.CompareTo(o2.Y) : o1.Z.CompareTo(o2.Z)));
					var source = temp.Select((pos, i) => (pos, i)).ToList();

					var match = false;
					foreach (var sX in shiftCandidates)
						foreach (var sY in shiftCandidates)
							foreach (var sZ in shiftCandidates)
								if (!match)
								{
									var target = funcs.Select(f => f(p.X + sX, p.Y + sY, p.Z + sZ)).Select(r => norm(new V3(r.X - sX, r.Y - sY, r.Z - sY))).ToList();
									target.Sort((o1, o2) => Math.Abs(o1.X - o2.X) > th ? o1.X.CompareTo(o2.X) : (Math.Abs(o1.Y - o2.Y) > th ? o1.Y.CompareTo(o2.Y) : o1.Z.CompareTo(o2.Z)));
									if (source.All(o => (o.pos - target[o.i]).Length < th))
									{
										match = true;
										shift = new V3(sX, sY, sZ);
									}

								}
				}
			}
			#endregion

			#endregion

			bool isHex = (sgnum >= 430 && sgnum <= 488);

			Crystal crystalTemp = new Crystal(
				(a / 10.0, b / 10.0, c / 10.0, alpha / 180 * Math.PI, beta / 180 * Math.PI, gamma / 180 * Math.PI), sgnum, "", Color.AliceBlue);
			crystalTemp.SetAxis();
			double aStar = crystalTemp.A_Star.Length / 10;
			double bStar = crystalTemp.B_Star.Length / 10;
			double cStar = crystalTemp.C_Star.Length / 10;

			//原子の情報
			string atomLabel, atomSymbol, thermalDisplaceType;
			double x, y, z, occ, x_err, y_err, z_err, occ_err;
			double Uiso = 0, u11 = 0, u22 = 0, u33 = 0, u12 = 0, u13 = 0, u23 = 0, Uiso_err = 0, u11_err = 0, u22_err = 0, u33_err = 0, u12_err = 0, u13_err = 0, u23_err = 0;
			double Biso = 0, b11 = 0, b22 = 0, b33 = 0, b12 = 0, b13 = 0, b23 = 0, Biso_err = 0, b11_err = 0, b22_err = 0, b33_err = 0, b12_err = 0, b13_err = 0, b23_err = 0;
			List<Atoms> atoms = new List<Atoms>();

			//"_atom_site_label"というラベルを含むCIF番号をリストする。 (最初の連続した番号のみを使う)
			List<CIF_Group> atomCIF = new List<CIF_Group>();
			bool flag = false;
			for (int i = 0; i < CIF.Count; i++)
			{
				if (flag || atomCIF.Count == 0)
				{
					if (CIF[i].items.Exists(item => item.label == "_atom_site_label"))
					{
						atomCIF.Add(CIF[i]);
						flag = true;
					}
					else
						flag = false;
				}
			}

			foreach (CIF_Group cif in atomCIF)
			{
				//まず基本的な原子位置や占有率などの情報を探す
				atomLabel = "";
				atomSymbol = "";
				thermalDisplaceType = "";
				x = y = z = 0;
				occ = 1;
				x_err = y_err = z_err = occ_err = Uiso = Uiso_err = 0;
				for (int j = 0; j < cif.items.Count; j++)
				{
					label = cif.items[j].label;
					data = cif.items[j].data;
					switch (label)
					{
						case "_atom_site_type_symbol": atomSymbol = data; break;
						case "_atom_site_label": atomLabel = data; break;
						case "_atom_site_fract_x":
							x = ConvertToDoubleForCIF(data, isHex)+ shift.X;
							x_err = ConvertErrForCIF(data);
							break;

						case "_atom_site_fract_y":
							y = ConvertToDoubleForCIF(data, isHex) + shift.Y;
							y_err = ConvertErrForCIF(data);
							break;

						case "_atom_site_fract_z":
							z = ConvertToDoubleForCIF(data, isHex) + shift.Z;
							z_err = ConvertErrForCIF(data);
							break;

						case "_atom_site_occupancy":
							if (data == "?" || data == ".")
							{
								occ = 1;
								occ_err = 0;
							}
							else
							{
								occ = ConvertToDoubleForCIF(data);
								occ_err = ConvertErrForCIF(data);
							}
							break;

						case "_atom_site_U_iso_or_equiv":
							Uiso = ConvertToDoubleForCIF(data);
							Uiso_err = ConvertErrForCIF(data);
							break;

						case "_atom_site_B_iso_or_equiv":
							Biso = ConvertToDoubleForCIF(data);
							Biso_err = ConvertErrForCIF(data);
							break;

						case "_atom_site_thermal_displace_type":
							thermalDisplaceType = data; break;
					}
				}

				//次に異方性の温度散乱因子をさがす (等方性の温度因子は既に上のループで読み込まれている)
				for (int k = 0; k < CIF.Count; k++)
				{
					if (CIF[k].items.Exists(item => item.label == "_atom_site_aniso_label" && item.data == atomLabel))
					{
						for (int l = 0; l < CIF[k].items.Count; l++)
						{
							label = CIF[k].items[l].label;
							data = CIF[k].items[l].data;
							switch (label)
							{
								case "_atom_site_aniso_U_11":
									u11 = ConvertToDoubleForCIF(data);
									u11_err = ConvertErrForCIF(data);
									break;

								case "_atom_site_aniso_U_22":
									u22 = ConvertToDoubleForCIF(data);
									u22_err = ConvertErrForCIF(data);
									break;

								case "_atom_site_aniso_U_33":
									u33 = ConvertToDoubleForCIF(data);
									u33_err = ConvertErrForCIF(data);
									break;

								case "_atom_site_aniso_U_12":
									u12 = ConvertToDoubleForCIF(data);
									u12_err = ConvertErrForCIF(data);
									break;

								case "_atom_site_aniso_U_13":
									u13 = ConvertToDoubleForCIF(data);
									u13_err = ConvertErrForCIF(data);
									break;

								case "_atom_site_aniso_U_23":
									u23 = ConvertToDoubleForCIF(data);
									u23_err = ConvertErrForCIF(data);
									break;

								case "_atom_site_aniso_B_11":
									b11 = ConvertToDoubleForCIF(data);
									b11_err = ConvertErrForCIF(data);
									break;

								case "_atom_site_aniso_B_22":
									b22 = ConvertToDoubleForCIF(data);
									b22_err = ConvertErrForCIF(data);
									break;

								case "_atom_site_aniso_B_33":
									b33 = ConvertToDoubleForCIF(data);
									b33_err = ConvertErrForCIF(data);
									break;

								case "_atom_site_aniso_B_12":
									b12 = ConvertToDoubleForCIF(data);
									b12_err = ConvertErrForCIF(data);
									break;

								case "_atom_site_aniso_B_13":
									b13 = ConvertToDoubleForCIF(data);
									b13_err = ConvertErrForCIF(data);
									break;

								case "_atom_site_aniso_B_23":
									b23 = ConvertToDoubleForCIF(data);
									b23_err = ConvertErrForCIF(data);
									break;
							}
						}
						break;
					}
				}
				//ラベル名から元素を探す
				string temp;
				int atomicNumber = 0;
				string atomName = atomSymbol == "" ? atomLabel : atomSymbol;

				//AtomicScatteringFactor asf = new AtomicScatteringFactor();
				for (int q = atomName.Length; q > 0 && atomicNumber == 0; q--)
				{
					temp = atomName.Substring(0, q);
					for (int k = 0; k <= 96 && atomicNumber == 0; k++)
					{
						// asf.SetCoefficientForXray(k);
						if (AtomConstants.AtomicName(k).ToLower() == temp.ToLower())
							atomicNumber = k;
					}

					if (temp == "OH")
					{
						atomicNumber = -1;
						break;
					}
				}

				DiffuseScatteringFactor dsf;

				if (Uiso == 0 && u11 == 0 && u12 == 0 && u13 == 0 && u22 == 0 && u23 == 0 && u33 == 0)//全てのUがゼロの時は、Bと判定
					dsf = new DiffuseScatteringFactor(b11 == 0 && b12 == 0 && b13 == 0 && b22 == 0 && b23 == 0 && b33 == 0,
						Biso, b11, b22, b33, b12, b23, b13, Biso_err, b11_err, b22_err, b33_err, b12_err, b23_err, b13_err);
				else//Uの場合
					dsf = new DiffuseScatteringFactor(u11 == 0 && u12 == 0 && u13 == 0 && u22 == 0 && u23 == 0 && u33 == 0,
						Uiso, u11, u22, u33, u12, u23, u13, Uiso_err, u11_err, u22_err, u33_err, u12_err, u23_err, u13_err, aStar, bStar, cStar);

				if (atomicNumber > 0)
					atoms.Add(new Atoms(atomLabel, atomicNumber, 0, 0, null, sgnum, new Vector3D(x, y, z), new Vector3D(x_err, y_err, z_err), occ, occ_err, dsf, null, 0));
				else if (atomicNumber == -1)//"OH"のときの対処
				{
					atomicNumber = 1;
					atoms.Add(new Atoms(atomLabel, atomicNumber, 0, 0, null, sgnum, new Vector3D(x, y, z), new Vector3D(x_err, y_err, z_err), occ, occ_err, dsf, null, 0));
					atomicNumber = 8;
					atoms.Add(new Atoms(atomLabel, atomicNumber, 0, 0, null, sgnum, new Vector3D(x, y, z), new Vector3D(x_err, y_err, z_err), occ, occ_err, dsf, null, 0));
				}
			}



			if (journalNameFull != "" || journalCodenASTM != "") journal += journalNameFull + " " + journalCodenASTM;
			if (issue != "") journal += ", " + issue;
			if (volume != "") journal += ", " + volume;
			if (year != "") journal += "(" + year + ")";
			if (pageFirst != "") journal += ", " + pageFirst;
			if (pageLast != "") journal += "-" + pageLast;

			string authours = "";
			foreach (string s in author)
				authours += s + "; ";

			var r = new Random();

			Crystal crystal = new Crystal(
				(a / 10, b / 10, c / 10, alpha * Math.PI / 180, beta * Math.PI / 180, gamma * Math.PI / 180),
				(a_err / 10, b_err / 10, c_err / 10, alpha_err / 10, beta_err / 10, gamma_err / 10),
				sgnum,
				name,
				Color.FromArgb(r.Next(255), r.Next(255), r.Next(255)),
				new Matrix3D(),
				atoms.ToArray(),
				("", authours, journal, sectionTitle),
				null);

			crystal.JournalName = journalNameFull;
			crystal.JournalPageFirst = pageFirst;
			crystal.JournalPageLast = pageLast;
			crystal.JournalVolume = volume;
			crystal.JournalYear = year;
			crystal.JournalIssue = issue;

			crystal.ChemicalFormulaStructural = chemical_formula_structural;

			SetOpenGL_property(crystal);

			return crystal;
		}

		private static V3 norm(V3 v)
		{
			var d = new[] { v.X, v.Y, v.Z };
			for (int i = 0; i < 3; i++)
			{
				while (d[i] > 0.9999999)
					d[i]--;
				while (d[i] < -0.0000001)
					d[i]++;
			}
			return new V3(d[0],d[1],d[2]);
		}

		private static double ConvertToDoubleForCIF(string x)
		{
			return ConvertToDoubleForCIF(x, false);
		}

		private static double ConvertToDoubleForCIF(string x, bool isHex)
		{
			if (Miscellaneous.IsDecimalPointComma)
				x = x.Replace('.', ',');

			if (x.IndexOf("(") > -1)
				x = x.Remove(x.IndexOf("("));

			double X = 0;
			double.TryParse(x, out X);

			if (isHex)
				return ConvertToDoubleForHexagonalSetting(X);
			else
				return X;
		}

		private static double ConvertErrForCIF(string x)
		{
			if (Miscellaneous.IsDecimalPointComma)
				x = x.Replace('.', ',');

			if (x.IndexOf("(") > -1)
			{
				string y = x.Remove(x.IndexOf("("));
				int n = (y.Length - y.IndexOf(".") - 1);

				x = x.Remove(0, x.IndexOf("(")).Trim('(').Trim(')');
				double X;
				double.TryParse(x, out X);

				return Math.Pow(10, -n) * X;
			}
			else
				return 0;
		}

		private static int SearchSGseriesNumberForCIF(string SgNameHM, string SgNameHall, int SpaceGroupNumber, double A, double B, double C, double Alfa, double Beta, double Gamma)
		{
			int symmetrySeriesNumber = -1;
			if (SgNameHall != "")
			{
				for (int i = 0; i < SymmetryStatic.TotalSpaceGroupNumber; i++)
				{
					var hall = SymmetryStatic.Get_Symmetry(i).SpaceGroupHallStr;
					if (hall != "")
						if ((hall.IndexOf(SgNameHall.Trim(), 0) != -1) ||
						(hall.IndexOf(SgNameHall.Replace(" ", ""), 0) != -1) ||
						(hall.Replace("\"", "").IndexOf(SgNameHall, 0) != -1))
						{
							symmetrySeriesNumber = i;
							break;
						}
				}
				if (symmetrySeriesNumber != -1)
					return symmetrySeriesNumber;
			}
			SgNameHM = SgNameHM.Replace("_", " ");
			SgNameHM = SgNameHM.Replace("{hexagonalal axes}", " ");
			SgNameHM = SgNameHM.Replace("{rhombohedral axes}", " ");

			SgNameHM = SgNameHM.TrimStart(' ').TrimEnd(' ');
			if (SgNameHM.EndsWith("RS") || SgNameHM.EndsWith("HR"))
				SgNameHM = SgNameHM.Remove(SgNameHM.Length - 2, 2).TrimEnd(' ');

			if (SgNameHM.EndsWith("H") || SgNameHM.EndsWith("h") || SgNameHM.EndsWith("R") || SgNameHM.EndsWith("r"))
				SgNameHM = SgNameHM.Remove(SgNameHM.Length - 1, 1).TrimEnd(' ');

			if (SgNameHM.EndsWith(":"))
				SgNameHM = SgNameHM.Remove(SgNameHM.Length - 1, 1).TrimEnd(' ');

			bool IsOrigineChoice2 = false;
			if (SgNameHM.EndsWith("Z"))//最後にZがついていたらOriginChoice2
			{
				IsOrigineChoice2 = true;
				SgNameHM = SgNameHM.TrimEnd('Z').TrimEnd();
			}
			if (SgNameHM.EndsWith("S"))//最後がSだったらOriginChoice1
				SgNameHM = SgNameHM.TrimEnd('S').TrimEnd();

			if (SgNameHM.EndsWith("S1"))//最後がS1だったらOriginChoice1
				SgNameHM = SgNameHM.Replace("S1", "").TrimEnd();
			if (SgNameHM.EndsWith("Z1"))//最後がZ1だったらOriginChoice1
				SgNameHM = SgNameHM.Replace("Z1", "").TrimEnd();
			if (SgNameHM.EndsWith(":S1"))//最後が:S1だったらOriginChoice1
				SgNameHM = SgNameHM.Replace(":S1", "").TrimEnd();
			if (SgNameHM.EndsWith(":1"))//最後が:1だったらOriginChoice1
				SgNameHM = SgNameHM.Replace(":1", "").TrimEnd();

			if (SgNameHM.EndsWith("S2"))//最後がS2だったらOriginChoice2
			{
				SgNameHM = SgNameHM.Replace("S2", "").TrimEnd();
				IsOrigineChoice2 = true;
			}
			if (SgNameHM.EndsWith("Z2"))//最後がZ2だったらOriginChoice2
			{
				SgNameHM = SgNameHM.Replace("Z2", "").TrimEnd();
				IsOrigineChoice2 = true;
			}
			if (SgNameHM.EndsWith(":S2"))//最後が:S2だったらOriginChoice2
			{
				SgNameHM = SgNameHM.Replace(":S2", "").TrimEnd();
				IsOrigineChoice2 = true;
			}
			if (SgNameHM.EndsWith(":2"))//最後が:S2だったらOriginChoice2
			{
				SgNameHM = SgNameHM.Replace(":2", "").TrimEnd();
				IsOrigineChoice2 = true;
			}
			if (SgNameHM.EndsWith("O2"))//最後が:S2だったらOriginChoice2
			{
				SgNameHM = SgNameHM.Replace("O2", "").TrimEnd();
				IsOrigineChoice2 = true;
			}

			SgNameHM = SgNameHM.Replace("~", "");

			//一文字目以降の英字は全て小文字に
			SgNameHM = SgNameHM[0] + SgNameHM.Remove(0, 1).ToLower();

			SgNameHM = SgNameHM.Replace("P(-1)", "P-1");

			SgNameHM = SgNameHM.Replace("R 32", "R32");

			SgNameHM = SgNameHM.Replace("P3(1)21", "P 3sub1 2 1");
			SgNameHM = SgNameHM.Replace("P3(2)21", "P 3sub2 2 1");
			SgNameHM = SgNameHM.Replace("P3121", "P 3sub1 2 1");
			SgNameHM = SgNameHM.Replace("P3221", "P 3sub2 2 1");
			SgNameHM = SgNameHM.Replace("P 31 21", "P 3sub1 2 1");
			SgNameHM = SgNameHM.Replace("P 32 21", "P 3sub2 2 1");
			SgNameHM = SgNameHM.Replace("P321", "P 3 2 1");

			SgNameHM = SgNameHM.Replace("P61", "P 6sub1");
			SgNameHM = SgNameHM.Replace("P62", "P 6sub2");
			SgNameHM = SgNameHM.Replace("P63", "P 6sub3");
			SgNameHM = SgNameHM.Replace("P64", "P 6sub4");
			SgNameHM = SgNameHM.Replace("P65", "P 6sub5");

			SgNameHM = SgNameHM.Replace("I41", "I 4sub1");
			SgNameHM = SgNameHM.Replace("P42", "P 4sub2");

			SgNameHM = SgNameHM.Replace("P42/m b c", "P 4sub2/m b c");

			SgNameHM = SgNameHM.Replace("P4322", "P 4sub3 2 2");

			SgNameHM = SgNameHM.Replace(" 61", " 6sub1");
			SgNameHM = SgNameHM.Replace(" 62", " 6sub2");
			SgNameHM = SgNameHM.Replace(" 63", " 6sub3");
			SgNameHM = SgNameHM.Replace(" 64", " 6sub4");
			SgNameHM = SgNameHM.Replace(" 65", " 6sub5");
			SgNameHM = SgNameHM.Replace(" 41", " 4sub1");
			SgNameHM = SgNameHM.Replace(" 42", " 4sub2");
			SgNameHM = SgNameHM.Replace(" 43", " 4sub3");
			SgNameHM = SgNameHM.Replace(" 31", " 3sub1");
			SgNameHM = SgNameHM.Replace(" 32", " 3sub2");
			SgNameHM = SgNameHM.Replace(" 21", " 2sub1");

			SgNameHM = SgNameHM.Replace("2(1)", " 2sub1");
			SgNameHM = SgNameHM.Replace("3(1)", " 3sub1");
			SgNameHM = SgNameHM.Replace("3(2)", " 3sub2");
			SgNameHM = SgNameHM.Replace("4(1)", " 4sub1");
			SgNameHM = SgNameHM.Replace("4(2)", " 4sub2");
			SgNameHM = SgNameHM.Replace("4(3)", " 4sub3");
			SgNameHM = SgNameHM.Replace("6(1)", " 6sub1");
			SgNameHM = SgNameHM.Replace("6(2)", " 6sub2");
			SgNameHM = SgNameHM.Replace("6(3)", " 6sub3");
			SgNameHM = SgNameHM.Replace("6(4)", " 6sub4");
			SgNameHM = SgNameHM.Replace("6(5)", " 6sub5");

			SgNameHM = SgNameHM.Replace("21", " 2sub1");

			SgNameHM = SgNameHM.Replace("  ", " ");

			string temp = SgNameHM.Replace(" ", "");
			#region
			if (temp == "Pm3") SgNameHM = "P m -3";
			else if (temp == "Pn3") SgNameHM = "P n -3";
			else if (temp == "Fm3") SgNameHM = "F m -3";
			else if (temp == "Fd3") SgNameHM = "F d -3";
			else if (temp == "Im3") SgNameHM = "I m -3";
			else if (temp == "Pa3") SgNameHM = "P a -3";
			else if (temp == "Ia3") SgNameHM = "I a -3";

			else if (temp == "Pm3m") SgNameHM = "P m -3 m";
			else if (temp == "Pn3n") SgNameHM = "P m -3 m";
			else if (temp == "Pm3n") SgNameHM = "P m -3 n";
			else if (temp == "Pn3m") SgNameHM = "P n -3 m";
			else if (temp == "Fm3m") SgNameHM = "F m -3 m";
			else if (temp == "Fm3c") SgNameHM = "F m -3 c";
			else if (temp == "Fd3m") SgNameHM = "F d -3 m";
			else if (temp == "Fd3c") SgNameHM = "F d -3 c";
			else if (temp == "Im3m") SgNameHM = "I m -3 m";
			else if (temp == "Ia3d") SgNameHM = "I a -3 d";
			else if (temp == "I2sub1/a-3") SgNameHM = "I a 3";


			else if (temp == "R-32/c") SgNameHM = "R -3 c";

			else if (temp == "P2" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "P 1 2 1";
			else if (temp == "P2" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "P 1 1 2";
			else if (temp == "P2" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "P 2 1 1";
			else if (temp == "P2sub1" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "P 1 2sub1 1";
			else if (temp == "P2sub1" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "P 1 1 2sub1";
			else if (temp == "P2sub1" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "P 2sub1 1 1";
			else if (temp == "C2" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "C 1 2 1";
			else if (temp == "A2" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "A 1 2 1";
			else if (temp == "I2" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "I 1 2 1";
			else if (temp == "A2" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "A 1 1 2";
			else if (temp == "B2" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "B 1 1 2";
			else if (temp == "I2" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "I 1 1 2";
			else if (temp == "B2" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "B 2 1 1";
			else if (temp == "C2" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "C 2 1 1";
			else if (temp == "I2" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "I 2 1 1";
			else if (temp == "Pm" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "P 1 m 1";
			else if (temp == "Pm" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "P 1 1 m";
			else if (temp == "Pm" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "P m 1 1";
			else if (temp == "Pc" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "P 1 c 1";
			else if (temp == "Pn" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "P 1 n 1";
			else if (temp == "Pa" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "P 1 a 1";
			else if (temp == "Pa" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "P 1 1 a";
			else if (temp == "Pn" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "P 1 1 n";
			else if (temp == "Pb" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "P 1 1 b";
			else if (temp == "Pb" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "P b 1 1";
			else if (temp == "Pn" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "P n 1 1";
			else if (temp == "Pc" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "P c 1 1";
			else if (temp == "Cm" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "C 1 m 1";
			else if (temp == "Am" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "A 1 m 1";
			else if (temp == "Im" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "I 1 m 1";
			else if (temp == "Am" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "A 1 1 m";
			else if (temp == "Bm" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "B 1 1 m";
			else if (temp == "Im" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "I 1 1 m";
			else if (temp == "Bm" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "B m 1 1";
			else if (temp == "Cm" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "C m 1 1";
			else if (temp == "Im" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "I m 1 1";
			else if (temp == "Cc" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "C 1 c 1";
			else if (temp == "An" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "A 1 n 1";
			else if (temp == "Ia" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "I 1 a 1";
			else if (temp == "Aa" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "A 1 a 1";
			else if (temp == "Cn" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "C 1 n 1";
			else if (temp == "Ic" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "I 1 c 1";
			else if (temp == "Aa" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "A 1 1 a";
			else if (temp == "Bn" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "B 1 1 n";
			else if (temp == "Ib" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "I 1 1 b";
			else if (temp == "Bb" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "B 1 1 b";
			else if (temp == "An" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "A 1 1 n";
			else if (temp == "Ia" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "I 1 1 a";
			else if (temp == "Bb" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "B b 1 1";
			else if (temp == "Cn" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "C n 1 1";
			else if (temp == "Ic" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "I c 1 1";
			else if (temp == "Cc" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "C c 1 1";
			else if (temp == "Bn" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "B n 1 1";
			else if (temp == "Ib" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "I b 1 1";
			else if (temp == "P2/m" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "P 1 2/m 1";
			else if (temp == "P2/m" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "P 1 1 2/m";
			else if (temp == "P2/m" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "P 2/m 1 1";
			else if (temp == "P2sub/m" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "P 1 2sub1/m 1";
			else if (temp == "P2sub/m" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "P 1 1 2sub1/m";
			else if (temp == "P2sub/m" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "P 2sub1/m 1 1";
			else if (temp == "C2/m" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "C 1 2/m 1";
			else if (temp == "A2/m" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "A 1 2/m 1";
			else if (temp == "I2/m" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "I 1 2/m 1";
			else if (temp == "A2/m" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "A 1 1 2/m";
			else if (temp == "B2/m" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "B 1 1 2/m";
			else if (temp == "I2/m" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "I 1 1 2/m";
			else if (temp == "B2/m" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "B 2/m 1 1";
			else if (temp == "C2/m" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "C 2/m 1 1";
			else if (temp == "I2/m" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "I 2/m 1 1";
			else if (temp == "P2/c" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "P 1 2/c 1";
			else if (temp == "P2/n" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "P 1 2/n 1";
			else if (temp == "P2/a" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "P 1 2/a 1";
			else if (temp == "P2/a" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "P 1 1 2/a";
			else if (temp == "P2/n" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "P 1 1 2/n";
			else if (temp == "P2/b" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "P 1 1 2/b";
			else if (temp == "P2/b" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "P 2/b 1 1";
			else if (temp == "P2/n" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "P 2/n 1 1";
			else if (temp == "P2/c" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "P 2/c 1 1";
			else if (temp == "P2sub1/c" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "P 1 2sub1/c 1";
			else if (temp == "P2sub1/n" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "P 1 2sub1/n 1";
			else if (temp == "P2sub1/a" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "P 1 2sub1/a 1";
			else if (temp == "P2sub1/a" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "P 1 1 2sub1/a";
			else if (temp == "P2sub1/n" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "P 1 1 2sub1/n";
			else if (temp == "P2sub1/b" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "P 1 1 2sub1/b";
			else if (temp == "P2sub1/b" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "P 2sub1/b 1 1";
			else if (temp == "P2sub1/n" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "P 2sub1/n 1 1";
			else if (temp == "P2sub1/c" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "P 2sub1/c 1 1";
			else if (temp == "C2/c" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "C 1 2/c 1";
			else if (temp == "A2/n" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "A 1 2/n 1";
			else if (temp == "I2/a" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "I 1 2/a 1";
			else if (temp == "A2/a" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "A 1 2/a 1";
			else if (temp == "C2/n" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "C 1 2/n 1";
			else if (temp == "I2/c" && Alfa == 90.0 && Gamma == 90.0) SgNameHM = "I 1 2/c 1";
			else if (temp == "A2/a" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "A 1 1 2/a";
			else if (temp == "B2/n" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "B 1 1 2/n";
			else if (temp == "I2/b" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "I 1 1 2/b";
			else if (temp == "B2/b" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "B 1 1 2/b";
			else if (temp == "A2/n" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "A 1 1 2/n";
			else if (temp == "I2/a" && Alfa == 90.0 && Beta == 90.0) SgNameHM = "I 1 1 2/a";
			else if (temp == "B2/b" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "B 2/b 1 1";
			else if (temp == "C2/n" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "C 2/n 1 1";
			else if (temp == "I2/c" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "I 2/c 1 1";
			else if (temp == "C2/c" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "C 2/c 1 1";
			else if (temp == "B2/n" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "B 2/n 1 1";
			else if (temp == "I2/b" && Beta == 90.0 && Gamma == 90.0) SgNameHM = "I 2/b 1 1";
			#endregion

			//文字列を含んでいて、かつ、文字数が少ない空間群を選択する (C1とC121などを見分けるため)

			int length = int.MaxValue;
			var sg = SymmetryStatic.SpaceGroupListWithoutSpace;
			SgNameHM = SgNameHM.Replace(" ", "");
			for (int i = 0; i < SymmetryStatic.TotalSpaceGroupNumber; i++)
			{
				if (sg[i].Length < length)
				{
					var sg_list = new List<string>(new[] { sg[i] });

					if (sg[i].Contains("e"))
					{
						sg_list.Add(sg[i].Replace('e', 'a'));
						sg_list.Add(sg[i].Replace('e', 'b'));
						sg_list.Add(sg[i].Replace('e', 'c'));
					}

					if (sg_list.Any(s => s.IndexOf(SgNameHM, 0) != -1))
					{
						symmetrySeriesNumber = i;
						length = sg[i].Length;
					}
				}
			}

			//Rhombohedoralのときの処置
			if (A == B && B == C && Alfa == Beta && Beta == Gamma && SymmetryStatic.Get_Symmetry(symmetrySeriesNumber).SpaceGroupHMStr.IndexOf("Hex") >= 0)
				symmetrySeriesNumber++;

			//originChoiceが2のときの対処
			if (IsOrigineChoice2 && SymmetryStatic.Get_Symmetry(symmetrySeriesNumber).SpaceGroupHMStr.IndexOf("(1)") > -1)
				symmetrySeriesNumber++;

			if (SpaceGroupNumber >= 1 && SpaceGroupNumber <= 230)
				if (symmetrySeriesNumber>=1 || SpaceGroupNumber != SymmetryStatic.Get_Symmetry(symmetrySeriesNumber).SpaceGroupNumber)
					for (int i = 0; i < SymmetryStatic.TotalSpaceGroupNumber; i++)
						if (SymmetryStatic.Get_Symmetry(i).SpaceGroupNumber == SpaceGroupNumber)
						{
							symmetrySeriesNumber = i;
							break;
						}

			return symmetrySeriesNumber;
		}

		public struct CIF_Item
		{
			public string label;
			public string data;

			public CIF_Item(string Label, string Data)
			{
				label = Label;
				data = Data;
			}
		}

		public struct CIF_Group
		{
			public List<CIF_Item> items;

			public void AddItem(CIF_Item item)
			{
				items.Add(item);
			}
		}

		#endregion

		/// <summary>
		/// 原子情報から自動的にBondsを検索
		/// </summary>
		/// <param name="atoms"></param>
		/// <returns></returns>
		public static List<Bonds> GetBonds(List<Atoms> atoms)
		{
			List<Bonds> bonds = new List<Bonds>();

			return bonds;
		}

		public static double ConvertToDoubleForHexagonalSetting(double x)
		{
			if (Math.Abs(x - 1.0 / 12.0) < 0.001) return 1.0 / 12.0;
			else if (Math.Abs(x - 1.0 / 6.0) < 0.001) return 1.0 / 6.0;
			else if (Math.Abs(x - 1.0 / 3.0) < 0.001) return 1.0 / 3.0;
			else if (Math.Abs(x - 5.0 / 12.0) < 0.001) return 5.0 / 12.0;
			else if (Math.Abs(x - 7.0 / 12.0) < 0.001) return 7.0 / 12.0;
			else if (Math.Abs(x - 2.0 / 3.0) < 0.001) return 2.0 / 3.0;
			else if (Math.Abs(x - 5.0 / 6.0) < 0.001) return 5.0 / 6.0;
			else if (Math.Abs(x - 11.0 / 12.0) < 0.001) return 11.0 / 12.0;
			else return x;
		}

		public static string ConvertToCIF(Crystal crystal)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("# This file is exported from \"" + System.Diagnostics.Process.GetCurrentProcess().ProcessName + "\"");
			sb.AppendLine("# http://pmsl.planet.sci.kobe-u.ac.jp/~seto");

			sb.AppendLine("data_global");
			sb.AppendLine("_chemical_name '" + crystal.Name + "'");

			sb.AppendLine("loop_");
			sb.AppendLine("_publ_author_name");
			foreach (string str in crystal.PublAuthorName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				sb.AppendLine("'" + str.Trim() + "'");

			sb.AppendLine("_journal_name '" + crystal.Journal + "'");

			#region 論文タイトル
			sb.AppendLine("_publ_section_title");
			sb.AppendLine(";");
			string title = "";
			foreach (string t in crystal.PublSectionTitle.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
			{
				if ((title + " " + t).Length > 80)
				{
					sb.AppendLine(title);
					title = "";
				}
				title += " " + t;
			}
			if (title != "")
				sb.AppendLine(title);
			sb.AppendLine(";");
			#endregion

			sb.AppendLine("_chemical_formula_sum '" + crystal.ChemicalFormulaSum + "'");
			sb.AppendLine("_cell_length_a " + (crystal.A * 10).ToString("f6"));
			sb.AppendLine("_cell_length_b " + (crystal.B * 10).ToString("f6"));
			sb.AppendLine("_cell_length_c " + (crystal.C * 10).ToString("f6"));
			sb.AppendLine("_cell_angle_alpha " + (crystal.Alpha / Math.PI * 180).ToString("f6"));
			sb.AppendLine("_cell_angle_beta " + (crystal.Beta / Math.PI * 180).ToString("f6"));
			sb.AppendLine("_cell_angle_gamma " + (crystal.Gamma / Math.PI * 180).ToString("f6"));
			sb.AppendLine("_cell_volume " + (crystal.Volume * 1000).ToString("f6"));
			sb.AppendLine("_exptl_crystal_density_diffrn " + crystal.Density.ToString("f6"));

			Symmetry sym = crystal.Symmetry;
			sb.AppendLine("_space_group_IT_number " + sym.SpaceGroupNumber);
			sb.AppendLine("_symmetry_cell_setting '" + sym.CrystalSystemStr + "'");
			string hm = sym.SpaceGroupHMStr;
			hm = hm.Replace("Hex", "");
			hm = hm.Replace("Rho", "");
			sb.AppendLine("_symmetry_space_group_name_H-M '" + hm + "'");
			sb.AppendLine("_symmetry_space_group_name_Hall '" + sym.SpaceGroupHallStr + "'");

			#region 原子の等価位置
			sb.AppendLine("loop_");
			sb.AppendLine("_symmetry_equiv_pos_as_xyz");
			bool[][] flag = new bool[0][];
			if (sym.LatticeTypeStr == "P") flag = new[] { new[] { false, false, false } };
			else if (sym.LatticeTypeStr == "A") flag = new[] { new[] { false, false, false }, new[] { false, true, true } };
			else if (sym.LatticeTypeStr == "B") flag = new[] { new[] { false, false, false }, new[] { true, false, true } };
			else if (sym.LatticeTypeStr == "C") flag = new[] { new[] { false, false, false }, new[] { false, true, true } };
			else if (sym.LatticeTypeStr == "I") flag = new[] { new[] { false, false, false }, new[] { true, true, true } };
			else if (sym.LatticeTypeStr == "F") flag = new[] { new[] { false, false, false }, new[] { false, true, true }, new[] { true, false, true }, new[] { false, true, true } };

			foreach (string wp in SymmetryStatic.WyckoffPositions[crystal.SymmetrySeriesNumber][0].PositionStr)
			{
				if (sym.SpaceGroupHMsubStr != "H")
				{
					for (int i = 0; i < flag.Length; i++)
					{
						string[] xyz = wp.Split(new char[] { ',' });
						for (int j = 0; j < flag[i].Length; j++)
						{
							if (flag[i][j])
							{
								if (xyz[j].EndsWith("+1/2")) xyz[j] = xyz[j].Replace("+1/2", "");
								else if (xyz[j].EndsWith("+1/4")) xyz[j] = xyz[j].Replace("+1/4", "+3/4");
								else if (xyz[j].EndsWith("+3/4")) xyz[j] = xyz[j].Replace("+3/4", "+1/4");
								else xyz[j] += "+1/2";
							}
						}
						sb.AppendLine("  '" + xyz[0] + "," + xyz[1] + "," + xyz[2] + "'");
					}
				}
				else//R格子のHexaセッティングのとき
				{
					sb.AppendLine("  '" + wp + "'");//(0,0,0)
					//(1/3,2/3,2/3)
					string[] xyz = wp.Split(new char[] { ',' });
					xyz[0] += "+1/3";
					xyz[1] += "+2/3";
					if (xyz[2].EndsWith("+1/2")) xyz[2] = xyz[2].Replace("+1/2", "+1/6");
					else xyz[2] += "+2/3";
					sb.AppendLine("  '" + xyz[0] + "," + xyz[1] + "," + xyz[2] + "'");
					//(2/3,1/3,1/3)
					xyz = wp.Split(new char[] { ',' });
					xyz[0] += "+2/3";
					xyz[1] += "+1/3";
					if (xyz[2].EndsWith("+1/2")) xyz[2] = xyz[2].Replace("+1/2", "+5/6");
					else xyz[2] += "+1/3";
					sb.AppendLine("  '" + xyz[0] + "," + xyz[1] + "," + xyz[2] + "'");
				}
			}
			#endregion

			#region 各原子の情報
			sb.AppendLine("loop_");
			sb.AppendLine("_atom_site_label");
			sb.AppendLine("_atom_site_type_symbol");
			sb.AppendLine("_atom_site_fract_x");
			sb.AppendLine("_atom_site_fract_y");
			sb.AppendLine("_atom_site_fract_z");
			sb.AppendLine("  _atom_site_occupancy");
			sb.AppendLine("_atom_site_U_iso_or_equiv");

			var aStar = crystal.A_Star.Length / 10;
			var bStar = crystal.B_Star.Length / 10;
			var cStar = crystal.C_Star.Length / 10;
			var pi2 = Math.PI * Math.PI;

			foreach (Atoms atom in crystal.Atoms)
			{
				sb.AppendLine(atom.Label + " " + AtomConstants.AtomicName(atom.AtomicNumber)
					+ " " + atom.X.ToString("f5") + " " + atom.Y.ToString("f5") + " " + atom.Z.ToString("f5")
					+ " " + atom.Occ.ToString("f5") + " " + (atom.Dsf.Biso / 8.0 / pi2).ToString("f5"));
			}

			{
				sb.AppendLine("loop_");
				sb.AppendLine("_atom_site_aniso_label");
				sb.AppendLine("_atom_site_aniso_U_11");
				sb.AppendLine("_atom_site_aniso_U_22");
				sb.AppendLine("_atom_site_aniso_U_33");
				sb.AppendLine("_atom_site_aniso_U_23");
				sb.AppendLine("_atom_site_aniso_U_13");
				sb.AppendLine("_atom_site_aniso_U_12");
				foreach (Atoms atom in crystal.Atoms)
				{
					if (!atom.Dsf.IsIso)
						sb.AppendLine(atom.Label + " " +
							(atom.Dsf.B11 / 2 / pi2 / aStar / aStar).ToString("f5") + " " +
							(atom.Dsf.B22 / 2 / pi2 / bStar / bStar).ToString("f5") + " " +
							(atom.Dsf.B33 / 2 / pi2 / cStar / cStar).ToString("f5") + " " +
							(atom.Dsf.B23 / 2 / pi2 / bStar / cStar).ToString("f5") + " " +
							(atom.Dsf.B31 / 2 / pi2 / cStar / aStar).ToString("f5") + " " +
							(atom.Dsf.B12 / 2 / pi2 / aStar / bStar).ToString("f5")
							);
				}
			}
			#endregion

			return sb.ToString();
		}
	}
}