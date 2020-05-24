using System;
using System.Collections.Generic;
using System.Linq;
//using ProtoBuf;
using MessagePack;


namespace Crystallography
{
    //Saveのために必要最小限の情報だけを保存するクラス
    //[ProtoContract]
    [Serializable()]
    [MessagePackObject]
    public class Crystal2Array
    {
        [Key(0)]
        public Crystal2[] Array;

        public Crystal2Array(Crystal2[] array)
        {
            Array = array;
        }
    }

    //必要最小限の情報だけを保存するクラス
    //[ProtoContract]
    [Serializable()]
    [MessagePackObject]
    public class Crystal2
    {
        [Key(0)]
        public double a;
        [Key(1)]
        public double b;
        [Key(2)]
        public double c;
        [Key(3)]
        public double alpha;
        [Key(4)]
        public double beta;
        [Key(5)]
        public double gamma;
        [Key(6)]
        public int argb;
        [Key(7)]
        public double density;
        [Key(8)]
        public string name;
        [Key(9)]
        public string note;
        [Key(10)]
        public string jour;
        [Key(11)]
        public string auth;
        [Key(12)]
        public string sect;
        [Key(13)]
        public string formula;//計算可能な場合は。
        [Key(14)]
        public Int16 sym;
        [Key(15)]
        public List<Atoms2> atoms;
        //[Key(16)]
        //public List<Bonds> bonds;
        [Key(17)]
        public double[] d;//強度8位までのd値
        [Key(18)]
        public string fileName;

        public Crystal2()
        {
            atoms = new List<Atoms2>();
            //bonds = new List<Bonds>();
        }

        public static Crystal GetCrystal(Crystal2 c)
        {
           var atom = new List<Atoms>();
            foreach (Atoms2 a in c.atoms)
            {
                double[] X_err = a.X_err != null ? new double[] { a.X_err[0], a.X_err[1], a.X_err[2] } : new double[] { 0, 0, 0 };
                double[] Occ = a.Occ.Length == 2 ? new double[] { a.Occ[0], a.Occ[1] } : new double[] { a.Occ[0], 0 };
                atom.Add(new Atoms(a.Label, a.AtomNo, a.SubXray, a.SubElectron, a.Isotope, c.sym,
                    new Vector3D(a.X[0], a.X[1], a.X[2], false), new Vector3D(X_err[0], X_err[1], X_err[2]), Occ[0], Occ[1],
                    new DiffuseScatteringFactor(a.IsIso, a.Biso, a.Baniso)));

                atom[atom.Count - 1].ResetVesta();
            }

            var bonds = Bonds.GetVestaBonds(atom.Select(a => a.AtomicNumber));

            Crystal crystal = new Crystal(
                (c.a, c.b, c.c, c.alpha, c.beta, c.gamma), null,
                c.sym, c.name, System.Drawing.Color.FromArgb(c.argb), new Matrix3D(),
                atom.ToArray(), (c.note, c.auth, GetFullJournal(c.jour), GetFullTitle(c.sect)), bonds);

            return crystal;
        }

        public static Crystal2 FromCrystal(Crystal c)
        {
            if (c == null) return null;
            Crystal2 c2 = new Crystal2
            {
                a = c.A,
                b = c.B,
                c = c.C,
                alpha = c.Alpha,
                beta = c.Beta,
                gamma = c.Gamma,
                sym = (short)c.SymmetrySeriesNumber,
                name = c.Name,
                note = c.Note,
                argb = c.Argb,
                auth = c.PublAuthorName,
                sect = GetShortTitle(c.PublSectionTitle),
                jour = GetShortJournal(c.Journal),
                formula = c.ChemicalFormulaSum,
                density = c.Density
            };

            try { c2.d = c.GetDspacingList(0.1, 0.1); }
            catch { return null; }

            c2.atoms = new List<Atoms2>();
            foreach (Atoms a in c.Atoms)
                c2.atoms.Add(new Atoms2(a.Label, a.AtomicNumber, a.SubNumberXray, a.SubNumberElectron,
                    new Vector3D(a.X, a.Y, a.Z, false), new Vector3D(a.X_err, a.Y_err, a.Z_err), a.Occ, a.Occ_err, a.Dsf));

            return c2;
        }

        public static string GetFullJournal(string shortJournal)
        {
            if (shortJournal != null && shortJournal.Contains("##"))
            {
                string number = shortJournal.Substring(shortJournal.IndexOf("##"), 4);
                string journal = "";
                switch (number)
                {
                    case "##01": journal = "American Mineralogist"; break;
                    case "##02": journal = "Canadian Mineralogist"; break;
                    case "##03": journal = "Acta Crystallographica"; break;
                    case "##04": journal = "Bulletin de la Societe Francaise de Mineralogie et de Cristallographie"; break;
                    case "##05": journal = "Bulletin of the Chemical Society of Japan"; break;
                    case "##06": journal = "Canadian Journal of Chemistry"; break;
                    case "##07": journal = "Chemische Berichte"; break;
                    case "##08": journal = "Clays and Clay Minerals"; break;
                    case "##09": journal = "Comptes Rendus Hebdomadaires des Seances de l'Academie des Sciences"; break;
                    case "##10": journal = "Contributions to Mineralogy and Petrology"; break;
                    case "##11": journal = "Doklady Akademii Nauk SSSR"; break;
                    case "##12": journal = "Dopovidi Akademii Nauk Ukrains'koi RSR Seriya B: Geologichni Khimichni ta Biologichni Nauki"; break;
                    case "##13": journal = "European Journal of Mineralogy"; break;
                    case "##14": journal = "Gazzetta Chimica Italiana"; break;
                    case "##15": journal = "Inorganic Chemistry"; break;
                    case "##16": journal = "Inorganica Chimica Acta"; break;
                    case "##17": journal = "Izvestiya Akademii Nauk SSSR Neorganicheskie Materialy"; break;
                    case "##18": journal = "Journal of Chemical Physics"; break;
                    case "##19": journal = "Journal of Inorganic and Nuclear Chemistry"; break;
                    case "##20": journal = "Journal of Physical Chemistry"; break;
                    case "##21": journal = "Journal of Solid State Chemistry"; break;
                    case "##22": journal = "Journal of the American Ceramic Society"; break;
                    case "##23": journal = "Journal of the American Chemical Society"; break;
                    case "##24": journal = "Journal of the Chemical Society"; break;
                    case "##25": journal = "Journal of the Less-Common Metals"; break;
                    case "##26": journal = "Kristallografiya"; break;
                    case "##27": journal = "Materials Research Bulletin"; break;
                    case "##28": journal = "Mineralogical Magazine"; break;
                    case "##29": journal = "Nature"; break;
                    case "##30": journal = "Naturwissenschaften"; break;
                    case "##31": journal = "Neues Jahrbuch fuer Mineralogie. Monatshefte"; break;
                    case "##32": journal = "Neues Jahrbuch fur Mineralogie, Monatshefte"; break;
                    case "##33": journal = "Physics and Chemistry of Minerals"; break;
                    case "##34": journal = "Zeitschrift fuer Anorganische und Allgemeine Chemie"; break;
                    case "##35": journal = "Zeitschrift fuer Kristallographie"; break;
                    case "##36": journal = "Zeitschrift fur Kristallographie"; break;
                    case "##37": journal = "Comptes Rendus Hebdomadaires des Seances de lAcademie des Sciences"; break;
                    case "##38": journal = "Dalton transactions"; break;
                    case "##39": journal = "Journal of Organic Chemistry"; break;
                }
                return shortJournal.Replace(number, journal);
            }
            return shortJournal;
        }

        public static string GetShortJournal(string fullJournal)
        {
            string journal = "";
            if (fullJournal != null)
                journal = fullJournal;
            journal = journal.Replace("American Mineralogist", "##01");
            journal = journal.Replace("Canadian Mineralogist", "##02");
            journal = journal.Replace("Acta Crystallographica", "##03");
            journal = journal.Replace("Bulletin de la Societe Francaise de Mineralogie et de Cristallographie", "##04");
            journal = journal.Replace("Bulletin of the Chemical Society of Japan", "##05");
            journal = journal.Replace("Canadian Journal of Chemistry", "##06");
            journal = journal.Replace("Chemische Berichte", "##07");
            journal = journal.Replace("Clays and Clay Minerals", "##08");
            journal = journal.Replace("Comptes Rendus Hebdomadaires des Seances de l'Academie des Sciences", "##09");
            journal = journal.Replace("Contributions to Mineralogy and Petrology", "##10");
            journal = journal.Replace("Doklady Akademii Nauk SSSR", "##11");
            journal = journal.Replace("Dopovidi Akademii Nauk Ukrains'koi RSR Seriya B: Geologichni Khimichni ta Biologichni Nauki", "##12");
            journal = journal.Replace("European Journal of Mineralogy", "##13");
            journal = journal.Replace("Gazzetta Chimica Italiana", "##14");
            journal = journal.Replace("Inorganic Chemistry", "##15");
            journal = journal.Replace("Inorganica Chimica Acta", "##16");
            journal = journal.Replace("Izvestiya Akademii Nauk SSSR Neorganicheskie Materialy", "##17");
            journal = journal.Replace("Journal of Chemical Physics", "##18");
            journal = journal.Replace("Journal of Inorganic and Nuclear Chemistry", "##19");
            journal = journal.Replace("Journal of Physical Chemistry", "##20");
            journal = journal.Replace("Journal of Solid State Chemistry", "##21");
            journal = journal.Replace("Journal of the American Ceramic Society", "##22");
            journal = journal.Replace("Journal of the American Chemical Society", "##23");
            journal = journal.Replace("Journal of the Chemical Society", "##24");
            journal = journal.Replace("Journal of the Less-Common Metals", "##25");
            journal = journal.Replace("Kristallografiya", "##26");
            journal = journal.Replace("Materials Research Bulletin", "##27");
            journal = journal.Replace("Mineralogical Magazine", "##28");
            journal = journal.Replace("Nature", "##29");
            journal = journal.Replace("Naturwissenschaften", "##30");
            journal = journal.Replace("Neues Jahrbuch fuer Mineralogie. Monatshefte", "##31");
            journal = journal.Replace("Neues Jahrbuch fur Mineralogie, Monatshefte", "##32");
            journal = journal.Replace("Physics and Chemistry of Minerals", "##33");
            journal = journal.Replace("Zeitschrift fuer Anorganische und Allgemeine Chemie", "##34");
            journal = journal.Replace("Zeitschrift fuer Kristallographie", "##35");
            journal = journal.Replace("Zeitschrift fur Kristallographie", "##36");
            journal = journal.Replace("Comptes Rendus Hebdomadaires des Seances de lAcademie des Sciences", "##37");
            journal = journal.Replace("Dalton transactions", "##38");
            journal = journal.Replace("Journal of Organic Chemistry", "##39");
            return journal;

        }

        public static string GetFullTitle(string shortTitle)
        {
            string title = shortTitle;
            if (title.Contains("##"))
            {
                title = title.Replace("##01", "The crystal structure");
                title = title.Replace("##02", "Crystal structure of");
                title = title.Replace("##03", "crystal structure of");
                title = title.Replace("##04", "powder diffraction");
                title = title.Replace("##05", "Rietveld refinement of");
                title = title.Replace("##06", "Second edition. Interscience Publishers, New York, New York Note:");
                title = title.Replace("##07", "Single-crystal structure refinements");
                title = title.Replace("##08", "Structural variation");
                title = title.Replace("##09", "Structure refinement of");
                title = title.Replace("##10", "Structure refinements of");
                title = title.Replace("##11", "structure refinement of");
                title = title.Replace("##12", "structure refinements of");
                title = title.Replace("##13", "_cod_database_code");
                title = title.Replace("##14", "_database_code_amcsd");
            }
            return title;
        }

        public static string GetShortTitle(string fullTitle)
        {
            string title = fullTitle;

            title = title.Replace("The crystal structure", "##01");
            title = title.Replace("Crystal structure of", "##02");
            title = title.Replace("crystal structure of", "##03");
            title = title.Replace("powder diffraction", "##04");
            title = title.Replace("Rietveld refinement of", "##05");
            title = title.Replace("Second edition. Interscience Publishers, New York, New York Note:", "##06");
            title = title.Replace("Single-crystal structure refinements", "##07");
            title = title.Replace("Structural variation", "##08");
            title = title.Replace("Structure refinement of", "##09");
            title = title.Replace("Structure refinements of", "##10");
            title = title.Replace("structure refinement of", "##11");
            title = title.Replace("structure refinements of", "##12");
            title = title.Replace("_cod_database_code", "##13");
            title = title.Replace("_database_code_amcsd", "##14");
            return title;
        }
    }

    [Serializable()]
    [MessagePackObject]
    public class Atoms2
    {
        [Key(0)]
        public string Label;
        [Key(1)]
        public float[] X;
        [Key(2)]
        public float[] X_err;
        [Key(3)]
        public float[] Occ;//Occ, Occ_errの順番
        [Key(4)]
        public byte SubXray;
        [Key(5)]
        public byte SubElectron;
        [Key(6)]
        public byte AtomNo;
        [Key(7)]
        public bool IsIso;
        [Key(8)]
        public float[] Biso;//Biso, Biso_errの順番
        [Key(9)]
        public float[] Baniso;//B11, B22, B33, B12, B23, B31, B11_err, B22_err, B33_err, B12_err, B23_err, B31_errの順番
        [Key(13)]
        public double[] Isotope;

        public Atoms2()
        {
            
        }
        public Atoms2(string label, int atomNo, int sfx, int sfe, Vector3D pos, Vector3D pos_err, double occ, double occ_err,
            DiffuseScatteringFactor dsf)
        {
             X = new float[] {  (float)pos.X, (float)pos.Y,  (float)pos.Z };

            if (pos_err.X != 0 || pos_err.Y != 0 || pos_err.Z != 0)
                X_err = new float[] { (float)pos_err.X , (float)pos_err.Y, (float)pos_err.Z };

            Label = label;

            if (occ_err == 0)
                Occ = new float[] { (float)occ };
            else
                Occ = new float[] { (float)occ, (float)occ_err };

            SubXray = (byte)sfx;
            SubElectron = (byte)sfe;
            AtomNo = (byte)atomNo;
            IsIso = dsf.IsIso;

            if (IsIso && dsf.Biso != 0)
            {
                if (dsf.Biso_err != 0)
                    Biso = new float[] { (float)dsf.Biso, (float)dsf.Biso_err };
                else
                    Biso = new float[] { (float)dsf.Biso };
            }
            else if (!IsIso && (dsf.B11 != 0 || dsf.B22 != 0 || dsf.B33 != 0 || dsf.B12 != 0 || dsf.B23 != 0 || dsf.B31 != 0))
            {
                if (dsf.B11_err != 0 || dsf.B22_err != 0 || dsf.B33_err != 0 || dsf.B12_err != 0 || dsf.B23_err != 0 || dsf.B31_err != 0)
                    Baniso = new float[] { (float)dsf.B11, (float)dsf.B22, (float)dsf.B33, (float)dsf.B12, (float)dsf.B23, (float)dsf.B31
                        , (float)dsf.B11_err, (float)dsf.B22_err, (float)dsf.B33_err, (float)dsf.B12_err, (float)dsf.B23_err, (float)dsf.B31_err };
                else
                    Baniso = new float[] { (float)dsf.B11, (float)dsf.B22, (float)dsf.B33, (float)dsf.B12, (float)dsf.B23, (float)dsf.B31 };
            }

            //Rad = radius;
            //Argb = mat.Argb;//色
            //Mat = new[] { 
            //    (byte)(mat.Ambient * 100), 
            //    (byte)(mat.Diffuse * 100), 
            //    (byte)(mat.Specular * 100), 
            //    (byte)(mat.SpecularPower*10), 
            //    (byte)(mat.Emission * 100), 
            //};
        }
    }
}