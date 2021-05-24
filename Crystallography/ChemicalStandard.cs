using System;
using System.Collections.Generic;
using System.Linq;

namespace Crystallography
{
    [Serializable()]
    public class Element
    {
        /// <summary>
        /// 原子番号
        /// </summary>
        public int Z;

        /// <summary>
        /// 原子名
        /// </summary>
        public string AtomicName;

        /// <summary>
        /// 存在度
        /// </summary>
        public double MolarAbundance;

        /// <summary>
        /// 重量比
        /// </summary>
        public double WeightRatio;

        /// <summary>
        /// 原子量
        /// </summary>
        public double MolarWeight;

        /// <summary>
        /// 同位体比
        /// </summary>
        //public Dictionary<int, double> Isotopes;
        /// <summary>
        /// X線検出時間
        /// </summary>
        public double XrayCountTime;

        /// <summary>
        /// X線強度
        /// </summary>
        public double XrayCPS;

        /// <summary>
        /// X線種類
        /// </summary>
        public XrayLine Line;

        /// <summary>
        /// 価数
        /// </summary>
        public double Valence;

        /// <summary>
        /// K-ratio
        /// </summary>
        public double Kratio;

        /// <summary>
        /// 電流値
        /// </summary>
        public double BeamCurrent;

        /// <summary>
        /// この元素が100%存在した場合の強度 (マトリックス効果を考慮しない)
        /// </summary>
        public double ApparentFullCPS;

        /// <summary>
        /// この元素が100%存在した場合の強度 (マトリックス効果を考慮)
        /// </summary>
        public double IdealFullCPS;

        public string StandardName;

        public double S;
        public double R;
        public double A;
        public double FchGamma;
        public double FcoGamma;

        public Element Clone()
        {
            return Deep.Copy<Element>(this);
        }

        public Element()
        {
        }

        public Element(int atomicNumber, double valence, double molarAbundance = 1, XrayLine line = XrayLine.Ka, double count = 0, double countTime = 1)
        {
            Z = atomicNumber;
            AtomicName = AtomConstants.AtomicName(Z);
            MolarAbundance = molarAbundance;
            Valence = valence;
            // Isotopes = AtomConstants.IsotopeAbundance[Z];
            MolarWeight = AtomConstants.AtomicWeight(Z);
        }

        public override string ToString()
        {
            string s = AtomicName;
            if (Line == XrayLine.Ka || Line == XrayLine.Ka1 || Line == XrayLine.Ka2)
                s += " Ka";
            else
                s += " La";
            if (StandardName == null || StandardName == "")
                return s;
            else
                return s + " in " + StandardName;
        }
    }

    [Serializable()]
    public class Molecule
    {
        /// <summary>
        /// 価数
        /// </summary>
        public double Valence;

        /// <summary>
        /// 分子量
        /// </summary>
        public double MolarWeight;

        /// <summary>
        /// 構成元素リスト
        /// </summary>
        public List<Element> Elements;

        public string Formula;

        public double MolarAbundance;
        public double WeightRatio;

        public Molecule Clone()
        {
            return Deep.Copy<Molecule>(this);
        }

        public Molecule()
        { }

        /// <summary>
        /// 単原子分子のコンストラクタ
        /// </summary>
        /// <param name="z">原子番号</param>
        /// <param name="valence">価数</param>
        /// <param name="weightRatio">重量比</param>
        public Molecule(int z, double valence = 0, double weightRatio = 0, double molarAbundance = 0)
        {
            if (z != 0)
            {
                Element e = new Element(z, 0);
                Elements = new List<Element>();
                Elements.Add(e);
                e.MolarAbundance = 1;
                MolarWeight = e.MolarWeight;
                Valence = valence;
                WeightRatio = weightRatio;
                MolarAbundance = molarAbundance;
                Formula = AtomConstants.AtomicName(z);
            }
        }

        /// <summary>
        /// 複合原子分子
        /// </summary>
        /// <param name="formula">組成式(文字列)</param>
        /// <param name="valence">価数</param>
        /// <param name="weightRatio">重量比</param>
        /// <param name="molarAbundance">モル存在度</param>
        public Molecule(string formula, double valence = 0, double weightRatio = 0, double molarAbundance = 0)
        {
            Formula = formula;
            Dictionary<int, double> form = GetFormula(formula);
            Elements = new List<Element>();
            foreach (int n in form.Keys)
                Elements.Add(new Element(n, 0, form[n]));
            MolarWeight = 0;
            foreach (Element e in Elements)
                MolarWeight += e.MolarWeight * e.MolarAbundance;
            Valence = valence;
            WeightRatio = weightRatio;
            MolarAbundance = molarAbundance;
        }

        /// <summary>
        /// 複合原子分子
        /// </summary>
        /// <param name="elemenets">Element[] 配列</param>
        /// <param name="valence">価数</param>
        /// <param name="weightRatio">重量比</param>
        /// <param name="molarAbundance">モル存在度</param>
        public Molecule(Element[] elemenets, double valence = 0, double weightRatio = 0, double molarAbundance = 0)
        {
            Formula = "";
            for (int i = 0; i < elemenets.Length; i++)
                Formula += elemenets[i].AtomicName + elemenets[i].MolarAbundance.ToString();
            Elements = new List<Element>();
            Elements.AddRange(elemenets);
            MolarWeight = 0;
            foreach (Element e in Elements)
                MolarWeight += e.MolarWeight * e.MolarAbundance;
            Valence = valence;
            WeightRatio = weightRatio;
            MolarAbundance = molarAbundance;
        }

        /// <summary>
        /// 原子番号Zの元素を含むかどうか。含んでいたらそのindexを、含んでいなかったら-1を返す
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public int Contain(int z)
        {
            for (int i = 0; i < Elements.Count; i++)
                if (Elements[i].Z == z)
                    return i;
            return -1;
        }

        public override string ToString()
        {
            return Formula;
        }

        public static Molecule[] DefinedIon = new Molecule[]{
            new Molecule("O", -2),
            new Molecule("OH", -1),
            new Molecule("Cl", -1),
            new Molecule("F", -1),
            new Molecule("CO3", -2),
            new Molecule("BO3", -3),
            new Molecule("SO4", -2),
            new Molecule("PO4", -2  )};

        /// <summary>
        /// 二つのMoleculeを結合した分子式(文字列)を返す
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static string CombineCationAndAnion(Molecule m1, Molecule m2)
        {
            string formula = "";

            if (m1.Valence * m2.Valence < 0)
            {
                double a = Math.Abs(m2.Valence), b = Math.Abs(m1.Valence);
                for (int j = 1; j < 10; j++)
                {
                    a *= j; b *= j;
                    if (a % 1 == 0 && b % 1 == 0)
                    {
                        for (int i = (int)Math.Max(a, b); i >= 1; i--)
                            if (a / i % 1 == 0 && b / i % 1 == 0)
                            {
                                a = a / i;
                                b = b / i;
                            }
                        break;
                    }
                }
                if (a != 1 && m1.Formula.Length > 2) formula += "(" + m1.Formula + ")";
                else formula += m1.Formula;
                if (a != 1) formula += a.ToString();

                if (b != 1 && m2.Formula.Length > 2) formula += "(" + m2.Formula + ")";
                else formula += m2.Formula;
                if (b != 1) formula += b.ToString();
            }
            return formula;
        }

        /// <summary>
        /// 文字列を分解して、化学組成を返す。戻り値は、Dictionary<Key, Value> で、Keyには原子番号、Valueにはモル比
        /// </summary>
        /// <param name="compound"></param>
        /// <returns></returns>
        public static Dictionary<int, double> GetFormula(string str)
        {
            Dictionary<int, double> formula = new Dictionary<int, double>();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= 'A' && str[i] <= 'Z')//一文字目が大文字のとき
                {
                    int z = 0;
                    if (i + 1 < str.Length && str[i + 1] >= 'a' && str[i + 1] <= 'z')//二文字目が小文字のとき
                        z = AtomConstants.AtomicNumber(str.Substring(i++, 2));
                    else//二文字目が小文字ではないとき
                        z = AtomConstants.AtomicNumber(str.Substring(i, 1));

                    string numStr = "";
                    while (i + 1 < str.Length && str[i + 1] >= '0' && str[i + 1] <= '9')//次に続く文字が数字の時
                        numStr += str[i++ + 1];
                    int num = numStr == "" ? 1 : Convert.ToInt32(numStr);

                    if (formula.ContainsKey(z))
                        formula[z] += num;
                    else
                        formula.Add(z, num);
                }

                if (str[i] == '(')  //かっこの始まりが現れたら
                {
                    int count = 1;
                    for (int j = i + 1; j < str.Length; j++)//対応するかっこの終りをみつける
                    {
                        if (str[j] == '(') count++;
                        else if (str[j] == ')') count--;
                        if (count == 0)//見つかったら
                        {
                            Dictionary<int, double> temp = GetFormula(str.Substring(i + 1, j - i - 1));
                            string numStr = "";
                            while (j + 1 < str.Length && str[j + 1] >= '0' && str[j + 1] <= '9')// ")"のあとが数値だったら
                                numStr += str[j++ + 1];
                            int num = numStr == "" ? 1 : Convert.ToInt32(numStr);

                            foreach (int n in temp.Keys)
                            {
                                if (formula.ContainsKey(n))
                                    formula[n] += num * temp[n];
                                else
                                    formula.Add(n, num * temp[n]);
                            }
                            i = j;
                            break;
                        }
                    }
                }
            }
            return formula;
        }
    }

    //CompoundはMoleculeの集合体である. MoleculeはElementの集合体である。
    [Serializable()]
    public class Compound
    {
        public string Name = "";
        public List<Molecule> Molecules = new List<Molecule>();
        public List<Element> Elements = new List<Element>();

        public Molecule MoleculeTotal = new Molecule();
        public Element ElementTotal = new Element();

        public bool WeightMode = true;
        public bool KratioMode = false;
        public double IncidentEnergy = 20;
        public double TakeoffAngle = 35 / 180.0 * Math.PI;

        private int iterationNumber = 10;
        public int IterationNumber { set { iterationNumber = value; } get { return iterationNumber; } }

        override public string ToString()
        {
            return Name;
        }

        public Compound Clone()
        {
            return Deep.Copy<Compound>(this);
        }

        public Compound()
        {
        }

        public Compound(Compound c)
        {
            Name = c.Name;
            Molecules.AddRange(c.Molecules.ToArray());
            Elements.AddRange(c.Elements.ToArray());
            WeightMode = c.WeightMode;
            KratioMode = c.KratioMode;
            IncidentEnergy = c.IncidentEnergy;
            TakeoffAngle = c.TakeoffAngle;
        }

        public Compound(string name)
        {
            Name = name;
        }

        public Compound(string name, Molecule[] molecules, Element[] elements, bool weightMode, bool kratioMode, double incidentEnergy, double takeoffAngle)
        {
            Name = name;
            Molecules.AddRange(molecules);
            Elements.AddRange(elements);
            WeightMode = weightMode;
            KratioMode = kratioMode;
            IncidentEnergy = incidentEnergy;
            TakeoffAngle = takeoffAngle;
            CalculateElementRatio();
        }

        /// <summary>
        /// Moleculeを追加する. weightモードではないときは、molarモード. molarモードのときは全てのMoleculeのweight値を再計算する.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="WeightMode"></param>
        public void AddMolecule(Molecule e, bool calcZAFParameters = true, bool calcIdealAndApparentIntensity = true)
        {
            Molecules.Add(e);
            CalculateElementRatio();
            if (calcZAFParameters)
                CalculateZAFParameters();
            if (calcIdealAndApparentIntensity)
                CaluculateIdealIntensity();
        }

        public void AddMolecule(Molecule[] e, bool calcZAFParameters = true, bool calcIdealAndApparentIntensity = true)
        {
            Molecules.AddRange(e);
            CalculateElementRatio();
            if (calcZAFParameters)
                CalculateZAFParameters();
            if (calcIdealAndApparentIntensity)
                CaluculateIdealIntensity();
        }

        public void ReplaceMolecules(Molecule m, int p, bool calcZAFParameters = true, bool calcIdealAndApparentIntensity = true)
        {
            if (p < Molecules.Count)
                Molecules[p] = m;
            CalculateElementRatio();
            if (calcZAFParameters)
                CalculateZAFParameters();
            if (calcIdealAndApparentIntensity)
                CaluculateIdealIntensity();
        }

        /// <summary>
        /// Moleculeをすべて削除
        /// </summary>
        public void RemoveMoleculeAll()
        {
            Molecules.Clear();
        }

        /// <summary>
        /// index位置のMoleculeを削除. indexが不正の時は何もしない
        /// </summary>
        /// <param name="index"></param>
        public void RemoveMoleculeAt(int index)
        {
            if (index < Molecules.Count && index >= 0)
                Molecules.RemoveAt(index);
            CalculateElementRatio();
        }

        public void CalculateElementRatio()
        {
            for (int i = 0; i < Elements.Count; i++)
            {
                Elements[i].WeightRatio = 0;
                Elements[i].MolarAbundance = 0;
                Elements[i].StandardName = this.Name;
                if (!Molecules.Any(m => m.Elements.Any(e => e.Z == Elements[i].Z)))
                    Elements.RemoveAt(i--);
            }

            if (WeightMode)
            {
                double totalMol = 0;
                for (int i = 0; i < Molecules.Count; i++)
                    totalMol += Molecules[i].WeightRatio / Molecules[i].MolarWeight;
                if (totalMol > 0)
                    for (int i = 0; i < Molecules.Count; i++)
                        Molecules[i].MolarAbundance = Molecules[i].WeightRatio / Molecules[i].MolarWeight / totalMol;
            }
            else
            {
                double totalWeight = 0;
                for (int i = 0; i < Molecules.Count; i++)
                    totalWeight += Molecules[i].MolarWeight * Molecules[i].MolarAbundance;
                if (totalWeight > 0)
                    for (int i = 0; i < Molecules.Count; i++)
                        Molecules[i].WeightRatio = Molecules[i].MolarWeight * Molecules[i].MolarAbundance / totalWeight;
            }

            foreach (Molecule m in Molecules)
                foreach (Element e in m.Elements)
                {
                    int j = Elements.FindIndex(e1 => e1.Z == e.Z);
                    if (j >= 0)//見つかったら
                    {
                        Elements[j].WeightRatio += m.WeightRatio * e.MolarAbundance * e.MolarWeight / m.MolarWeight;
                        Elements[j].MolarAbundance += m.MolarAbundance * e.MolarAbundance;
                    }
                    else
                    {
                        Element element = Deep.Copy<Element>(e);
                        element.StandardName = this.Name;
                        Elements.Add(element);
                        Elements[^1].WeightRatio = m.WeightRatio * e.MolarAbundance * e.MolarWeight / m.MolarWeight;
                        Elements[^1].MolarAbundance = m.MolarAbundance * e.MolarAbundance;
                    }
                }

            //酸素を含んでいたら、最後に回す
            for (int i = 0; i < Elements.Count; i++)
                if (Elements[i].Z == 8)
                {
                    Elements.Add(Elements[i]);
                    Elements.RemoveAt(i);
                    break;
                }

            ElementTotal.WeightRatio = ElementTotal.MolarAbundance = 0;
            foreach (Element e in Elements)
            {
                ElementTotal.WeightRatio += e.WeightRatio;
                ElementTotal.MolarAbundance += e.MolarAbundance;
            }
            MoleculeTotal.WeightRatio = MoleculeTotal.MolarAbundance = 0;
            foreach (Molecule m in Molecules)
            {
                MoleculeTotal.WeightRatio += m.WeightRatio;
                MoleculeTotal.MolarAbundance += m.MolarAbundance;
            }
        }

        public void CalculateZAFParameters()
        {
            for (int i = 0; i < Elements.Count; i++)
            {
                //原子番号補正
                Elements[i].S = ZAFCorrection.StoppingFactor(Elements.ToArray(), i, IncidentEnergy);//AtomConstants.StoppingFactor(Elements[i].Z, Elements[i].Line, 20);
                Elements[i].R = ZAFCorrection.BackscatteredFactor(Elements.ToArray(), i, IncidentEnergy); //AtomConstants.BackScatteredFactor(Elements[i].Z, Elements[i].Line, 20);

                //吸収補正の計算
                Elements[i].A = ZAFCorrection.AbsorptionCorrectionFunction(Elements.ToArray(), i, IncidentEnergy, TakeoffAngle);

                //蛍光補正
                Elements[i].FchGamma = ZAFCorrection.CharacteristicFluorescenceFactor(Elements.ToArray(), i, IncidentEnergy, TakeoffAngle);
                Elements[i].FcoGamma = ZAFCorrection.ChontinuousFluorescenceFactor(Elements.ToArray(), i, IncidentEnergy, TakeoffAngle);
            }
        }

        public void CaluculateIdealIntensity()
        {
            for (int i = 0; i < Elements.Count; i++)
            {
                Elements[i].ApparentFullCPS = Elements[i].XrayCPS / Elements[i].WeightRatio;
                Elements[i].IdealFullCPS = Elements[i].ApparentFullCPS / (Elements[i].R / Elements[i].S * Elements[i].A * (1 + Elements[i].FchGamma + Elements[i].FcoGamma));
            }
        }

        public void CalculateMatrixCorrection()
        {
            //まず、初期濃度を設定
            for (int i = 0; i < Elements.Count; i++)
                Elements[i].WeightRatio = 0;

            for (int i = 0; i < Elements.Count; i++)
            {
                if (KratioMode)
                    Elements[i].XrayCPS = Elements[i].ApparentFullCPS * Elements[i].Kratio;
                else
                    Elements[i].Kratio = Elements[i].XrayCPS / Elements[i].ApparentFullCPS;
            }

            for (int i = 0; i < Elements.Count; i++)
            {
                if (Elements[i].ApparentFullCPS != 0)//スタンダード登録されている元素であれば
                {
                    for (int j = 0; j < Molecules.Count; j++) //その元素が入力されている分子を探して、
                    {
                        int k = Molecules[j].Contain(Elements[i].Z);
                        if (k >= 0)//見つかったら
                        {
                            Molecules[j].WeightRatio = Molecules[j].MolarWeight / (Molecules[j].Elements[k].MolarWeight * Molecules[j].Elements[k].MolarAbundance) * Elements[i].XrayCPS / Elements[i].ApparentFullCPS;
                            break;
                        }
                    }
                }
            }
            double[] beforeWeight = new double[Molecules.Count];
            for (int i = 0; i < Molecules.Count; i++)
                beforeWeight[i] = Molecules[i].WeightRatio;

            //与えられた重量比に従って、ZAF値を計算
            for (int n = 0; n < IterationNumber; n++)
            {
                CalculateElementRatio();
                //totalを100に規格化
                double weightTotal = 0;
                for (int i = 0; i < Molecules.Count; i++)
                    weightTotal += Molecules[i].WeightRatio;
                for (int i = 0; i < Molecules.Count; i++)
                    Molecules[i].WeightRatio /= weightTotal;

                CalculateZAFParameters();
                //double sum = 0;
                for (int i = 0; i < Elements.Count; i++)
                    if (Elements[i].ApparentFullCPS != 0)//スタンダード登録されている元素であれば
                    {
                        for (int j = 0; j < Molecules.Count; j++) //その元素が入力されている分子を探して、
                        {
                            int k = Molecules[j].Contain(Elements[i].Z);
                            if (k >= 0)//見つかったら
                            {
                                double zaf = Elements[i].R / Elements[i].S * Elements[i].A * (1 + Elements[i].FchGamma + Elements[i].FcoGamma);
                                Molecules[j].WeightRatio = Molecules[j].MolarWeight / (Molecules[j].Elements[k].MolarWeight * Molecules[j].Elements[k].MolarAbundance) * Elements[i].XrayCPS / Elements[i].IdealFullCPS / zaf;
                                //sum+=Molecules[j].WeightRatio;
                                break;
                            }
                        }
                    }

                //変化率がすべての元素で0.1%以下になったら終了
                bool flag = true;
                for (int i = 0; i < Molecules.Count && flag; i++)
                    if (Math.Abs(beforeWeight[i] - Molecules[i].WeightRatio) / Molecules[i].WeightRatio > 0.001)
                        flag = false;
                if (flag)
                    break;
                else
                    for (int i = 0; i < Molecules.Count; i++)
                        beforeWeight[i] = Molecules[i].WeightRatio;
            }
            CalculateElementRatio();
        }
    }

    public static class ZAFCorrection
    {
        /// <summary>
        /// 連続X線による蛍光励起補正
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="index"></param>
        /// <param name="E0"></param>
        /// <param name="TakeoffAngle"></param>
        /// <returns></returns>
        public static double ChontinuousFluorescenceFactor(Element[] elements, int index, double E0, double TakeoffAngle)
        {
            int zA = elements[index].Z;
            XrayLineEdge edgeA = XrayLineEdge.K;
            XrayLine lineA = XrayLine.Ka;
            if (elements[index].Line == XrayLine.La1 || elements[index].Line == XrayLine.La2 || elements[index].Line == XrayLine.Lb1 || elements[index].Line == XrayLine.Lb2)
            {
                edgeA = XrayLineEdge.L3;
                lineA = XrayLine.La1;
            }
            double EcA = AtomConstants.CharacteristicXrayEnergy(zA, edgeA);
            double E = AtomConstants.CharacteristicXrayEnergy(zA, elements[index].Line);

            double c = lineA == XrayLine.Ka ? 4.34E-6 : 3.13E-6;

            double mu_rhoAAe = AtomConstants.MassAbsorption(EcA, zA);
            double mu_rhoUnkAe = 0;
            double mu_rhoUnkA = 0;
            double zAve = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                mu_rhoUnkAe += elements[i].WeightRatio * AtomConstants.MassAbsorption(EcA, elements[i].Z);
                mu_rhoUnkA += elements[i].WeightRatio * AtomConstants.MassAbsorption(E, elements[i].Z);
                zAve += elements[i].WeightRatio * elements[i].Z;
            }
            double gamma = AtomConstants.AbsorptionJumpRatio(zA, edgeA);
            double gammaMinusOnePerGamma = (gamma - 1) / gamma;
            if (lineA == XrayLine.La1)
                gammaMinusOnePerGamma = (gamma - 1) / gamma / AtomConstants.AbsorptionJumpRatio(zA, XrayLineEdge.L2) / AtomConstants.AbsorptionJumpRatio(zA, XrayLineEdge.L1);

            double chi = mu_rhoUnkA / Math.Sin(TakeoffAngle);
            double g = chi / mu_rhoUnkAe;
            double U0A = E0 / EcA;

            return c * elements[index].MolarWeight * zAve * EcA * mu_rhoAAe / mu_rhoUnkAe * gammaMinusOnePerGamma * Math.Log(1 + g * U0A) / g / U0A;
        }

        private static object lockObJforBetaArray = new object();

        /// <summary>
        /// betaの値を保管する一時変数.
        /// </summary>
        private static Dictionary<double, double[]>[] betaArray = new Dictionary<double, double[]>[100];

        /// <summary>
        /// 特性X線による蛍光励起補正
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="index"></param>
        /// <param name="E0"></param>
        /// <param name="takeoff"></param>
        /// <returns></returns>
        public static double CharacteristicFluorescenceFactor(Element[] elements, int index, double E0, double takeoff)
        {
            //まず、被励起X線 A[] と励起X線 B[] の組み合わせを選択

            int zA = elements[index].Z;
            XrayLineEdge edgeA = XrayLineEdge.K;
            XrayLine lineA = XrayLine.Ka;
            double energyA = AtomConstants.CharacteristicXrayEnergy(zA, elements[index].Line);
            if (elements[index].Line == XrayLine.La1 || elements[index].Line == XrayLine.La2 || elements[index].Line == XrayLine.Lb1 || elements[index].Line == XrayLine.Lb2)
            {
                edgeA = XrayLineEdge.L3;
                lineA = XrayLine.La1;
            }
            double EcA = AtomConstants.CharacteristicXrayEnergy(zA, edgeA);

            double fchGamma = 0;
            double U0A = E0 / EcA;
            double gamma = AtomConstants.AbsorptionJumpRatio(zA, edgeA);
            double gammaMinusOnePerGamma = (gamma - 1) / gamma;
            if (lineA == XrayLine.La1)
                gammaMinusOnePerGamma = (gamma - 1) / gamma / AtomConstants.AbsorptionJumpRatio(zA, XrayLineEdge.L2) / AtomConstants.AbsorptionJumpRatio(zA, XrayLineEdge.L1);

            double mu_rhoUnkA = 0;
            for (int k = 0; k < elements.Length; k++)
                mu_rhoUnkA += elements[k].WeightRatio * AtomConstants.MassAbsorption(energyA, elements[k].Z);

            var sigmaA = 4.5E5 / (Math.Pow(E0, 1.65) - Math.Pow(EcA, 1.65));

            for (int i = 0; i < elements.Length; i++)
            {
                if (i != index)
                {
                    var zB = elements[i].Z;
                    var omega = AtomConstantsSub.FluorescentYieldK[zB];
                    double JAwithoutP = 0.5 * gammaMinusOnePerGamma * omega * AtomConstants.AtomicWeight(zA) / AtomConstants.AtomicWeight(zB);

                    //"Kab": beta = 1.1;  "Kb": beta = 0.1; "Lab": beta = 1.4;  "Lb": beta = 0.4;　計算効率を上げるため、一度計算したbetaは再利用する.
                    var beta = Array.Empty<double>();
                    if (betaArray[zB] != null && betaArray[zB].ContainsKey(EcA))
                        beta = betaArray[zB][EcA];
                    else
                    {
                        if (EcA < AtomConstants.CharacteristicXrayEnergy(zB, XrayLine.La1)) beta = new[] { 1.1, 1.4 };
                        else if (EcA < AtomConstants.CharacteristicXrayEnergy(zB, XrayLine.Lb1)) beta = new[] { 1.1, 0.4 };
                        else if (EcA < AtomConstants.CharacteristicXrayEnergy(zB, XrayLine.Ka1)) beta = new[] { 1.1 };
                        else if (EcA < AtomConstants.CharacteristicXrayEnergy(zB, XrayLine.Kb1)) beta = new[] { 0.1 };
                        if (betaArray[zB] == null)
                            betaArray[zB] = new Dictionary<double, double[]>();
                        lock (lockObJforBetaArray)
                            betaArray[zB].Add(EcA, beta);
                    }

                    for (int j = 0; j < beta.Length; j++)//j==0ならK系列, j==1ならL系列
                    {
                        XrayLineEdge edgeB = j == 0 ? XrayLineEdge.K : XrayLineEdge.L3;
                        double EcB = AtomConstants.CharacteristicXrayEnergy(zB, edgeB);
                        double U0B = E0 / EcB;
                        if (E0 > EcB)
                        {
                            double fchCb = elements[i].WeightRatio * beta[j];

                            double P = 1;
                            if (lineA == XrayLine.Ka && j == 1) P = 4.2;
                            else if (lineA == XrayLine.La1 && j == 1) P = 0.24;

                            double JA = JAwithoutP * P;

                            double D = Math.Pow((U0B - 1) / (U0A - 1), 1.67);

                            double energyB = j == 0 ? AtomConstants.CharacteristicXrayEnergy(zB, XrayLine.Ka) : AtomConstants.CharacteristicXrayEnergy(zB, XrayLine.La1);

                            double mu_rhoAB = AtomConstants.MassAbsorption(energyB, zA);
                            double mu_rhoUnkB = 0;
                            for (int k = 0; k < elements.Length; k++)
                                mu_rhoUnkB += elements[k].WeightRatio * AtomConstants.MassAbsorption(energyB, elements[k].Z);

                            double x = mu_rhoUnkA / mu_rhoUnkB / Math.Sin(takeoff);
                            double gx = Math.Log(1 + x) / x;

                            double y = sigmaA / mu_rhoUnkB;
                            double gy = Math.Log(1 + y) / y;

                            fchGamma += 0.5 * fchCb * JA * D * mu_rhoAB / mu_rhoUnkB * (gx + gy);
                        }
                    }
                }
            }
            return fchGamma;
        }

        /// <summary>
        /// 阻止能
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="index"></param>
        /// <param name="incidentEnergy"></param>
        /// <returns></returns>
        public static double StoppingFactor(Element[] elements, int index, double incidentEnergy)
        {
            int z = elements[index].Z;
            XrayLine line = elements[index].Line;
            XrayLineEdge edge;
            if (line == XrayLine.Ka || line == XrayLine.Ka1 || line == XrayLine.Ka2) edge = XrayLineEdge.K;
            else edge = XrayLineEdge.L3;
            double s = 0;
            for (int i = 0; i < elements.Length; i++)
                s += elements[i].WeightRatio * AtomConstants.StoppingFactor(AtomConstants.CharacteristicXrayEnergy(z, edge), incidentEnergy, elements[i].Z);
            return s;
        }

        /// <summary>
        /// 後方散乱係数
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="index"></param>
        /// <param name="incidentEnergy"></param>
        /// <returns></returns>
        public static double BackscatteredFactor(Element[] elements, int index, double incidentEnergy)
        {
            int z = elements[index].Z;
            XrayLine line = elements[index].Line;
            XrayLineEdge edge;
            if (line == XrayLine.Ka || line == XrayLine.Ka1 || line == XrayLine.Ka2) edge = XrayLineEdge.K;
            else edge = XrayLineEdge.L3;
            double r = 0;
            for (int i = 0; i < elements.Length; i++)
                r += elements[i].WeightRatio * AtomConstants.BackScatteredFactor(AtomConstants.CharacteristicXrayEnergy(z, edge), incidentEnergy, elements[i].Z);
            return r;
        }

        /// <summary>
        /// 吸収係数
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="index"></param>
        /// <param name="incidentEnergy"></param>
        /// <param name="theta"></param>
        /// <returns></returns>
        public static double AbsorptionCorrectionFunction(Element[] elements, int index, double incidentEnergy, double theta)
        {
            int z = elements[index].Z;

            XrayLine line = elements[index].Line;
            XrayLineEdge edge;
            if (elements[index].Line == XrayLine.Ka || elements[index].Line == XrayLine.Ka1 || elements[index].Line == XrayLine.Ka2) edge = XrayLineEdge.K;
            else edge = XrayLineEdge.L3;

            double mu_rho = 0;
            double h = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                mu_rho += elements[i].WeightRatio * AtomConstants.MassAbsorption(AtomConstants.CharacteristicXrayEnergy(z, line), elements[i].Z);
                h += elements[i].WeightRatio * 1.2 * AtomConstants.AtomicWeight(elements[i].Z) / elements[i].Z / elements[i].Z;
            }

            double chi = mu_rho / Math.Sin(theta);
            double ec = AtomConstants.CharacteristicXrayEnergy(z, edge);
            double sigma = 4.5E5 / (Math.Pow(incidentEnergy, 1.65) - Math.Pow(ec, 1.65));
            return (1 + h) / (1 + chi / sigma) / (1 + h * (1 + chi / sigma)); ;
        }
    }
}