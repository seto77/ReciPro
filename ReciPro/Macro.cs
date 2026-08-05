#region using
using Crystallography;
using Crystallography.OpenGL; // 260805Cl 追加: StructureViewerClass (GLObject/MeshSnapshot)
using MathNet.Numerics;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.FormattableString; // 260723Cl 追加: SpotInfo() の CSV をカルチャ非依存 (小数点=ピリオド) で出力するため
namespace ReciPro;
#endregion

public class Macro : MacroBase
{
    #region 基底クラス
    private readonly FormMain main;
    public FileClass File;
    public DirectionClass Dir;
    public DifSimClass DifSim;
    public SpotIDClass SpotID; // 260801Cl 追加
    public StructureViewerClass StructureViewer; // 260805Cl 追加 (P1-4)
    public CrystalListClass CrystalList;
    public CrystalClass Crystal;
    public STEMClass STEM;
    public HRTEMClass HRTEM;
    public PotentialClass Potential;

    public Macro(FormMain _main) : base(_main, "ReciPro")
    {
        main = _main;

        //260718Cl 統合: サブクラスごとに繰り返していた GenerateHelpText+ForEach を addHelp に集約 (### 置換は該当テキストが無ければ no-op なので一律適用しても無害)。
        //型付き field への代入だけ個別に残す。
        void addHelp(Type type, string name) => help.AddRange(HelpAttribute.GenerateHelpText(type, name).Select(s => s.Replace("###", name)));

        File = new FileClass(this);
        addHelp(File.GetType(), nameof(File));

        Dir = new DirectionClass(this);
        addHelp(Dir.GetType(), nameof(Dir));

        DifSim = new DifSimClass(this);
        addHelp(DifSim.GetType(), nameof(DifSim));

        SpotID = new SpotIDClass(this); // 260801Cl 追加
        addHelp(SpotID.GetType(), nameof(SpotID));

        StructureViewer = new StructureViewerClass(this); // 260805Cl 追加 (P1-4)
        addHelp(StructureViewer.GetType(), nameof(StructureViewer));

        Crystal = new CrystalClass(this);
        addHelp(Crystal.GetType(), nameof(Crystal));

        CrystalList = new CrystalListClass(this);
        addHelp(CrystalList.GetType(), nameof(CrystalList));

        STEM = new STEMClass(this);
        addHelp(STEM.GetType(), nameof(STEM));

        HRTEM = new HRTEMClass(this);
        addHelp(HRTEM.GetType(), nameof(HRTEM));

        Potential = new PotentialClass(this);
        addHelp(Potential.GetType(), nameof(Potential));
    }

    // (260414Ch) Help text revised throughout this file to match the implementation and improve English.
    [Help("Pauses execution for the specified number of milliseconds.", "int millisec")]
    public static void Sleep(int millisec) => Thread.Sleep(millisec);


    #region マクロサンプル集
    // 260415Cl 改修 英語/日本語を隣接ペア配置、1-2行の短いサンプルを統合、行幅を約1.5倍に拡大 (旧: 並列2配列・15件)
    // 260415Cl 言語判定は ReciPro 全体と統一して FormMain.Language を使用 (旧: 個別の CultureInfo 判定)
    public override (string name, string body)[] SampleMacros => FormMain.Language == Languages.Japanese ? _sampleMacrosJa : _sampleMacrosEn;

    // マスター配列: 各要素で英語版 (nameEn, bodyEn) と日本語版 (nameJa, bodyJa) を並べて定義することで、
    // 英語/日本語の内容対応を見失わずに保守できる。_sampleMacrosEn/Ja はこの配列から Array.ConvertAll で生成。
    private static readonly (string nameEn, string bodyEn, string nameJa, string bodyJa)[] _sampleMacros =
    [
        (
            "01. Basic loop and if",
            """
            # Loop 10 times computing the squares. Inside the loop, an if/else classifies 'i' as "even" or "odd"
            # and an if adds a "big" flag once 'sq' exceeds 25. Run with "Step by step" mode and watch
            # 'i', 'sq', 'kind', 'big' change in the debug panel (print() is not available here).
            for i in range(10):
                sq = i * i
                if i % 2 == 0:
                    kind = "even"
                else:
                    kind = "odd"
                big = sq > 25
            """,
            "01. 基本的なループと条件分岐",
            """
            # 10 回ループして二乗を計算し、ループ内の if/else で 'i' を "even" / "odd" に分類しつつ、
            # 'sq' が 25 を超えたら 'big' フラグを立てます。「Step by step」モードで実行すると、デバッグ
            # パネルで i・sq・kind・big の値の変化を確認できます (print() は使えません)。
            for i in range(10):
                sq = i * i
                if i % 2 == 0:
                    kind = "even"
                else:
                    kind = "odd"
                big = sq > 25
            """
        ),
        (
            "02. Math functions",
            """
            # The math module is pre-imported, so you can use it directly without an explicit import statement.
            # This sample shows pi, trigonometric (sin/cos), sqrt, exponential (exp), and logarithm (log).
            # Run in Step mode to inspect each variable in the debug panel.
            r = 5.0
            area          = math.pi * r * r            # circle area
            circumference = 2 * math.pi * r            # circle circumference
            s   = math.sin(math.pi / 6)                # sin(30°) = 0.5
            c   = math.cos(math.pi / 3)                # cos(60°) = 0.5
            t   = math.tan(math.pi / 4)                # tan(45°) = 1.0
            rt2 = math.sqrt(2)                         # square root of 2
            e2  = math.exp(2)                          # e^2 ≈ 7.389
            ln  = math.log(math.e)                     # natural log of e = 1.0
            lg  = math.log10(1000)                     # base-10 log of 1000 = 3.0
            """,
            "02. 数学関数の使用",
            """
            # math モジュールはあらかじめ import 済みなので、明示的な import 文なしにそのまま使えます。
            # このサンプルでは pi, 三角関数 (sin/cos/tan), sqrt, 指数関数 (exp), 対数関数 (log) を扱います。
            # Step モードで実行して各変数の値をデバッグパネルで確認しましょう。
            r = 5.0
            area          = math.pi * r * r            # 円の面積
            circumference = 2 * math.pi * r            # 円周の長さ
            s   = math.sin(math.pi / 6)                # sin(30°) = 0.5
            c   = math.cos(math.pi / 3)                # cos(60°) = 0.5
            t   = math.tan(math.pi / 4)                # tan(45°) = 1.0
            rt2 = math.sqrt(2)                         # 2 の平方根
            e2  = math.exp(2)                          # e^2 ≒ 7.389
            ln  = math.log(math.e)                     # e の自然対数 = 1.0
            lg  = math.log10(1000)                     # 1000 の常用対数 = 3.0
            """
        ),
        (
            "03. Rotation and alignment",
            """
            # Rotate the crystal around the a-axis [100] by 30 degrees, then align the [001] zone axis to the
            # screen normal, and finally rotate so that the (110) plane becomes parallel to the screen.
            ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 30)
            ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
            ReciPro.Dir.ProjectAlongPlane(1, 1, 0)
            """,
            "03. 回転と方位の整合",
            """
            # 結晶を a軸 [100] 周りに 30° 回転させ、続いて [001] 晶帯軸を画面法線方向に整合させ、
            # 最後に (110) 面が画面と平行になるよう回転させます。Step モードで各操作を順に確認できます。
            ReciPro.Dir.RotateAroundAxisInDeg(1, 0, 0, 30)
            ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
            ReciPro.Dir.ProjectAlongPlane(1, 1, 0)
            """
        ),
        (
            "04. File I/O and crystal info",
            """
            # Load a crystal list (*.xml) or an individual crystal file (CIF / AMC), read the name,
            # chemical formula, and density of the selected crystal, and export it as CIF.
            # Each file operation opens a dialog. Run in Step mode to inspect the variables.
            ReciPro.File.ReadCrystalList()
            ReciPro.File.ReadCrystal()
            name    = ReciPro.Crystal.Name
            formula = ReciPro.Crystal.ChemicalFormula
            density = ReciPro.Crystal.Density
            ReciPro.File.ExportAsCIF()
            """,
            "04. ファイル入出力と結晶情報",
            """
            # 結晶リスト (*.xml) または個別の結晶ファイル (CIF / AMC) を読み込み、選択中の結晶の
            # 名前・化学式・密度を取得し、CIF 形式でエクスポートします。ファイル操作はダイアログで
            # パスを指定します。Step モードで実行して各変数の値を確認しましょう。
            ReciPro.File.ReadCrystalList()
            ReciPro.File.ReadCrystal()
            name    = ReciPro.Crystal.Name
            formula = ReciPro.Crystal.ChemicalFormula
            density = ReciPro.Crystal.Density
            ReciPro.File.ExportAsCIF()
            """
        ),
        (
            "05. Scan crystal list",
            """
            # Scan the crystal list: collect all names, and find the indices of every crystal whose
            # chemical formula is "Mg2SiO4". Use CrystalList.Count to iterate the whole list safely.
            # Run in Step mode to watch 'names' and 'mg2sio4' grow in the debug panel.
            names = []
            mg2sio4 = []
            for i in range(ReciPro.CrystalList.Count):
                ReciPro.CrystalList.SelectedIndex = i
                names.append(ReciPro.Crystal.Name)
                if ReciPro.Crystal.ChemicalFormula == "Mg2SiO4":
                    mg2sio4.append(i)
            """,
            "05. 結晶リストの走査",
            """
            # 結晶リスト全件を走査し、全結晶名を収集しつつ化学式が "Mg2SiO4" のものの index を集めます。
            # CrystalList.Count を使うと全件を安全にループできます。
            # Step モードで実行すると names と mg2sio4 が増えていく様子をデバッグパネルで確認できます。
            names = []
            mg2sio4 = []
            for i in range(ReciPro.CrystalList.Count):
                ReciPro.CrystalList.SelectedIndex = i
                names.append(ReciPro.Crystal.Name)
                if ReciPro.Crystal.ChemicalFormula == "Mg2SiO4":
                    mg2sio4.append(i)
            """
        ),
        (
            "06. Diffraction pattern setup",
            """
            # Open the Diffraction Simulator and display the [001] electron diffraction pattern of the first
            # crystal in the list at 200 keV. This demonstrates the typical simulation setup sequence.
            ReciPro.CrystalList.SelectedIndex = 0
            ReciPro.DifSim.Open()
            ReciPro.DifSim.Source_Electron()
            ReciPro.DifSim.Energy = 200
            ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
            """,
            "06. 回折パターンシミュレーション設定",
            """
            # 回折シミュレーターを開き、リスト最初の結晶の [001] 入射電子回折パターンを 200 keV で表示します。
            # 一般的な回折シミュレーションのセットアップ手順を示したサンプルです。
            ReciPro.CrystalList.SelectedIndex = 0
            ReciPro.DifSim.Open()
            ReciPro.DifSim.Source_Electron()
            ReciPro.DifSim.Energy = 200
            ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
            """
        ),
        (
            "07. Save diffraction image",
            """
            # Simulate the [001] electron diffraction pattern and save it as a PNG file.
            # When SaveAsPng() is called without a filename, a save dialog opens to choose the output path.
            ReciPro.DifSim.Open()
            ReciPro.DifSim.Source_Electron()
            ReciPro.DifSim.Energy = 200
            ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
            ReciPro.DifSim.SaveAsPng()
            """,
            "07. 回折パターン画像の保存",
            """
            # [001] 入射の電子回折パターンをシミュレートして PNG ファイルとして保存します。
            # SaveAsPng() をファイル名なしで呼ぶと、保存ダイアログが開いて出力先を選択できます。
            ReciPro.DifSim.Open()
            ReciPro.DifSim.Source_Electron()
            ReciPro.DifSim.Energy = 200
            ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
            ReciPro.DifSim.SaveAsPng()
            """
        ),
        (
            "07-01. Tilt series (rotate + save)",
            """
            # Variation of 08: rotate around the c-axis in 10° steps and save a PNG at each orientation.
            dir_path = ReciPro.File.GetDirectoryPath()
            ReciPro.DifSim.Open()
            ReciPro.DifSim.Source_Electron()
            ReciPro.DifSim.Energy = 200
            for i in range(36):
                ReciPro.Dir.RotateAroundAxisInDeg(0, 0, 1, 10)
                fname = dir_path + "tilt_" + str(i * 10).zfill(3) + ".png"
                ReciPro.DifSim.SaveAsPng(fname)
            """,
            "07-01. 傾斜シリーズ (回転 + 保存)",
            """
            # 08 のバリエーション: c軸周りに 10° ずつ回転しながら各方位の PNG を連続保存します。
            dir_path = ReciPro.File.GetDirectoryPath()
            ReciPro.DifSim.Open()
            ReciPro.DifSim.Source_Electron()
            ReciPro.DifSim.Energy = 200
            for i in range(36):
                ReciPro.Dir.RotateAroundAxisInDeg(0, 0, 1, 10)
                fname = dir_path + "tilt_" + str(i * 10).zfill(3) + ".png"
                ReciPro.DifSim.SaveAsPng(fname)
            """
        ),
        (
            "07-02. Multi-step rotation loop",
            """
            # Variation of 08: iterate b-axis rotations in Step mode to observe each orientation change.
            ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
            for step in range(18):
                ReciPro.Dir.RotateAroundAxisInDeg(0, 1, 0, 5)
                angle = (step + 1) * 5
            """,
            "07-02. 複数ステップ回転ループ",
            """
            # 08 のバリエーション: b軸周りの回転を Step モードで観察します。
            ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
            for step in range(18):
                ReciPro.Dir.RotateAroundAxisInDeg(0, 1, 0, 5)
                angle = (step + 1) * 5
            """
        ),
        (
            "07-03. Energy series",
            """
            # Variation of 08: save a PNG for each electron energy from 100 to 300 keV in 50 keV steps.
            dir_path = ReciPro.File.GetDirectoryPath()
            ReciPro.DifSim.Open()
            ReciPro.DifSim.Source_Electron()
            ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
            for kev in range(100, 301, 50):
                ReciPro.DifSim.Energy = kev
                fname = dir_path + "energy_" + str(kev) + "keV.png"
                ReciPro.DifSim.SaveAsPng(fname)
            """,
            "07-03. 加速電圧シリーズ",
            """
            # 08 のバリエーション: 加速電圧 100〜300 keV を 50 keV 刻みで変えながら PNG を連続保存します。
            dir_path = ReciPro.File.GetDirectoryPath()
            ReciPro.DifSim.Open()
            ReciPro.DifSim.Source_Electron()
            ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
            for kev in range(100, 301, 50):
                ReciPro.DifSim.Energy = kev
                fname = dir_path + "energy_" + str(kev) + "keV.png"
                ReciPro.DifSim.SaveAsPng(fname)
            """
        ),
        (
            "08. Spot info (dynamical)",
            """
            # Calculate dynamical diffraction for the [001] zone axis with 200 keV electrons, then save the
            # spot information (CSV: hkl, d-spacing, excitation error, amplitudes, intensities) as a text file.
            ReciPro.DifSim.Open()
            ReciPro.DifSim.Source_Electron()
            ReciPro.DifSim.Energy = 200
            ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
            ReciPro.DifSim.Calc_Dynamical()
            csv = ReciPro.DifSim.SpotInfo()
            ReciPro.File.SaveText(csv)
            """,
            "08. スポット情報 (動力学計算)",
            """
            # [001] 晶帯軸に対して 200 keV の電子で動力学回折計算を行い、スポット情報
            # (hkl, 面間隔, 励起誤差, 振幅, 強度などを含む CSV) をテキストファイルとして保存します。
            ReciPro.DifSim.Open()
            ReciPro.DifSim.Source_Electron()
            ReciPro.DifSim.Energy = 200
            ReciPro.Dir.ProjectAlongAxis(0, 0, 1)
            ReciPro.DifSim.Calc_Dynamical()
            csv = ReciPro.DifSim.SpotInfo()
            ReciPro.File.SaveText(csv)
            """
        ),
        (//260805Cl 追加 (P1-3): 結晶の生成・編集 API のサンプル
            "09. Create a crystal and scan the lattice parameter",
            """
            # Build NaCl from scratch (space group, cell in angstroms, atoms in fractional coordinates),
            # then scan the lattice parameter by +-2 %. BeginEdit() starts from the current crystal,
            # so absolute values are set each iteration from the base cell read before the loop.
            ReciPro.Crystal.BeginCreate('NaCl')
            ReciPro.Crystal.SetSpaceGroup('Fm-3m')
            ReciPro.Crystal.SetCellInAng(5.6402)
            ReciPro.Crystal.AddAtom('Na', 'Na', 0, 0, 0)
            ReciPro.Crystal.AddAtom('Cl', 'Cl', 0.5, 0.5, 0.5)
            ReciPro.Crystal.Commit()
            print('density = %.4f g/cm^3' % ReciPro.Crystal.Density)

            base = ReciPro.Crystal.GetCellInAng()
            for k in range(-2, 3):
                ReciPro.Crystal.BeginEdit()
                ReciPro.Crystal.SetCellInAng(base[0] * (1 + 0.01 * k))
                ReciPro.Crystal.Commit()
                print('a = %.4f  density = %.4f' % (ReciPro.Crystal.GetCellInAng()[0], ReciPro.Crystal.Density))
            """,
            "09. 結晶の作成と格子定数スキャン",
            """
            # NaCl をゼロから作り (空間群、Å 単位のセル定数、分率座標の原子)、格子定数を ±2 % スキャンします。
            # BeginEdit() は「現在の結晶」を起点にするので、ループ前に読んだ基準セルから毎回絶対値を設定します。
            ReciPro.Crystal.BeginCreate('NaCl')
            ReciPro.Crystal.SetSpaceGroup('Fm-3m')
            ReciPro.Crystal.SetCellInAng(5.6402)
            ReciPro.Crystal.AddAtom('Na', 'Na', 0, 0, 0)
            ReciPro.Crystal.AddAtom('Cl', 'Cl', 0.5, 0.5, 0.5)
            ReciPro.Crystal.Commit()
            print('density = %.4f g/cm^3' % ReciPro.Crystal.Density)

            base = ReciPro.Crystal.GetCellInAng()
            for k in range(-2, 3):
                ReciPro.Crystal.BeginEdit()
                ReciPro.Crystal.SetCellInAng(base[0] * (1 + 0.01 * k))
                ReciPro.Crystal.Commit()
                print('a = %.4f  density = %.4f' % (ReciPro.Crystal.GetCellInAng()[0], ReciPro.Crystal.Density))
            """
        ),
        (//260805Cl 追加 (P1-4): StructureViewer クラスのサンプル
            "10. Batch 3D-print export",
            """
            # Export the displayed structure for 3D printing: the default solid model, and a variant
            # with the coordination polyhedra as edge frames (the atoms inside stay visible).
            # The file extension picks the format: .stl = single color, .3mf = colored by element.
            d = ReciPro.File.GetDirectoryPath()
            ReciPro.StructureViewer.Export3DModel(d + 'model_60mm.stl', maxSizeInMM=60)
            ReciPro.StructureViewer.Export3DModel(d + 'model_edges_60mm.stl', maxSizeInMM=60, polyhedraAsEdges=True)
            """,
            "10. 3Dプリント用モデルの一括出力",
            """
            # 表示中の構造を 3D プリント用に出力します: 既定のソリッド模型と、配位多面体を稜線枠に
            # した変種 (中の原子が見えます)。拡張子で形式が決まります (.stl = 単色, .3mf = 元素色分け)。
            d = ReciPro.File.GetDirectoryPath()
            ReciPro.StructureViewer.Export3DModel(d + 'model_60mm.stl', maxSizeInMM=60)
            ReciPro.StructureViewer.Export3DModel(d + 'model_edges_60mm.stl', maxSizeInMM=60, polyhedraAsEdges=True)
            """
        ),
    ];

    private static readonly (string name, string body)[] _sampleMacrosEn = Array.ConvertAll(_sampleMacros, m => (name: m.nameEn, body: m.bodyEn));

    private static readonly (string name, string body)[] _sampleMacrosJa = Array.ConvertAll(_sampleMacros, m => (name: m.nameJa, body: m.bodyJa));
    #endregion
    
    #endregion

    #region ファイルクラス
    public class FileClass(Macro _p) : MacroSub(_p.main)
    {
        private FormMain main => _p.main;

        // (260414Ch) Help text revised to describe the actual return value and dialog behavior.
        [Help("Returns a directory path ending with '\\'. If 'filename' is omitted, opens a folder selection dialog; otherwise returns the directory that contains 'filename'.", "string filename")]
        public string GetDirectoryPath(string filename = "") => Execute<string>(new Func<string>(() => getDirectoryPath(filename)));
        private static string getDirectoryPath(string filename = "")
        {
            string path;
            if (filename == "")
            {
                var dlg = new FolderBrowserDialog();
                path = dlg.ShowDialog() == DialogResult.OK ? dlg.SelectedPath : "";
            }
            else
                path = System.IO.Path.GetDirectoryName(filename);
            return path + "\\";
        }

        [Help("Opens a file selection dialog and returns the full path of the selected file, or an empty string if canceled.")]
        // public string GetFileName() => Execute(() => getFileName()); // (260322Ch) 旧実装: 1 回しか使わない短い helper を経由していた
        public string GetFileName() => Execute<string>(new Func<string>(() =>
        {
            var dlg = new OpenFileDialog();
            return dlg.ShowDialog() == DialogResult.OK ? dlg.FileName : "";
        })); // (260322Ch) OpenFileDialog の取得処理をその場でインライン化

        [Help("Opens a file selection dialog, allows multiple selection, and returns the full paths of the selected files.")]
        // public string[] GetFileNames() => Execute<string[]>(new Func<string[]>(() => getFileNames())); // (260322Ch) 旧実装
        public string[] GetFileNames() => Execute<string[]>(new Func<string[]>(() =>
        {
            var dlg = new OpenFileDialog() { Multiselect = true };
            return dlg.ShowDialog() == DialogResult.OK ? dlg.FileNames : [];
        })); // (260322Ch) 複数選択の file dialog 取得処理をその場でインライン化

        [Help("Loads a crystal list file in XML format. If 'filename' is omitted, opens a file selection dialog.", "string filename")]
        public void ReadCrystalList(string filename = "") => Execute(() => main.ReadCrystalList(filename, false, false));

        [Help("Loads a crystal file in CIF or AMC format. If 'filename' is omitted, opens a file selection dialog.", "string filename")]
        public void ReadCrystal(string filename = "") => Execute(() => main.ReadCrystal(filename));

        [Help("Exports the selected crystal in CIF format. If 'filename' is omitted, opens a save dialog.", "string filename")]
        public void ExportAsCIF(string filename = "") => Execute(() => main.ExportCIF(filename));

        //260805Cl 追加: SaveText の対。IronPython 埋め込み環境は Python 標準ライブラリの encodings を持たず
        //open(path, encoding=...) が使えない (LookupError) ため、テキスト読みはこの API が唯一の経路。
        [Help("Reads a text file as UTF-8 and returns its content as a string. If 'filename' is omitted, opens a file selection dialog. Pair it with Crystal.LoadCifText() or SaveText().", "string filename")]
        public string ReadText(string filename = "")
        {
            if (filename == "")
            {
                filename = GetFileName();
                if (filename == "")
                    return "";
            }
            return Execute(() => System.IO.File.ReadAllText(filename, Encoding.UTF8));
        }

        [Help("Saves text in UTF-8. If 'filename' is omitted, opens a save dialog.", "string textData, string filename")]
        public void SaveText(string textData, string filename = "")
        {
            if(filename == "")
            {
                var dlg = new SaveFileDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                    filename = dlg.FileName;
                else
                    return;
            }
            Execute(() => System.IO.File.WriteAllText(filename, textData,Encoding.UTF8));
        }
    }
    #endregion

    #region Crystal クラス
    public class CrystalClass(Macro _p) : MacroSub(_p.main)
    {
        private FormMain main => _p.main;

        // (260414Ch) Help text revised for clarity and to match the underlying properties.
        // 260805Cl: [Help] と宣言の間の空行を除去 (整備方針 §3-3)
        [Help("Gets the name of the selected crystal.")]
        public string Name { get => main.Crystal.Name; }

        [Help("Gets the chemical formula of the selected crystal.")]
        public string ChemicalFormula { get => main.Crystal.ChemicalFormulaSum; }

        [Help("Gets the density of the selected crystal in g/cm^3.")]
        public double Density { get => main.Crystal.Density; }

        //260805Cl 追加 (マクロ整備方針 P1-3。設計正本: .project-guidance/ReciPro/ReciPro_マクロCrystal編集API設計.md):
        //結晶の生成・編集 API。draft オブジェクト返却は補完が効かないため「Crystal 名前空間内の暗黙 pending 状態」方式 (codex 相談で確定)。
        //単位は交換レイヤ (CIF/Crystal2) と同じ Å・度・B(Å²) で受け、Commit 境界で内部 nm/rad/nm² へ換算する。
        //Commit は原子的: 全検証 → Crystal 構築 → crystalControl.Crystal 代入 (CIF 読込と同一経路) を一度だけ。失敗時は現結晶を変えず pending を保持する。

        [Help("Gets the cell constants of the current crystal as a six-element array [a, b, c, alpha, beta, gamma] in angstroms and degrees.")]
        public double[] GetCellInAng()
        {
            var c = main.Crystal;
            return [c.A * 10, c.B * 10, c.C * 10, c.Alpha / Math.PI * 180, c.Beta / Math.PI * 180, c.Gamma / Math.PI * 180];
        }

        [Help("Gets the Hermann-Mauguin space-group symbol of the current crystal, with the setting suffix (e.g. ':2', ':H') where the group has multiple settings.")]
        public string SpaceGroupName
        {
            get
            {
                var s = main.Crystal.Symmetry;
                return s.SpaceGroupHMStr + (s.SpaceGroupHMsubStr.Length > 0 ? ":" + s.SpaceGroupHMsubStr : "");
            }
        }

        [Help("Gets the International Tables space-group number (1-230) of the current crystal.")]
        public int SpaceGroupNumber => main.Crystal.Symmetry.SpaceGroupNumber;

        [Help("Gets whether a pending crystal draft (started by BeginCreate(), BeginEdit(), or LoadCifText()) is open.")]
        public bool HasPending => pending != null;

        [Help("Starts a pending draft for a new crystal with the given name (identity orientation, random color, no symmetry until SetSpaceGroup). Throws if another draft is already pending -- Commit() or Cancel() it first.", "string name")]
        public void BeginCreate(string name)
        {
            RequireNoPending("BeginCreate");
            pending = new PendingCrystal { Name = name ?? "" };
        }

        [Help("Starts a pending draft from the currently selected crystal (cell, space group, atoms, orientation, color and bonds are carried over). Note that after a successful Commit(), the next BeginEdit() starts from the UPDATED crystal, so changes accumulate; for absolute scans, read the base values before the loop and set them explicitly each iteration.")]
        public void BeginEdit()
        {
            RequireNoPending("BeginEdit");
            pending = SnapshotOf(main.Crystal ?? throw new InvalidOperationException("BeginEdit: no crystal is selected."));
        }

        [Help("Starts a pending draft from the given CIF text (the content of a .cif file, not a file path). The draft can then be adjusted with the setter functions before Commit(). Throws if the text cannot be interpreted or another draft is already pending.", "string cifText")]
        public void LoadCifText(string cifText)
        {
            RequireNoPending("LoadCifText");
            var c = ConvertCrystalData.ConvertToCrystalFromCifText(cifText) ?? throw new ArgumentException("LoadCifText: the text could not be interpreted as a CIF.");
            pending = SnapshotOf(c);
        }

        [Help("Discards the pending draft (no effect on the current crystal).")]
        public void Cancel() => pending = null;

        [Help("Sets the name of the pending crystal.", "string name")]
        public void SetName(string name) => RequirePending("SetName").Name = name ?? "";

        [Help("Sets the pending cell constants in angstroms and degrees. Each call replaces the whole cell: omitted arguments are derived from the space-group constraints at Commit() -- e.g. SetCellInAng(4.05) is enough for a cubic crystal, and explicit values that contradict the constraints raise an error.", "double a, double b, double c, double alpha, double beta, double gamma")]
        public void SetCellInAng(double a, double b = double.NaN, double c = double.NaN, double alpha = double.NaN, double beta = double.NaN, double gamma = double.NaN)
        {
            var p = RequirePending("SetCellInAng");
            double[] v = [a, b, c, alpha, beta, gamma];
            for (int i = 0; i < 6; i++)
            {
                p.Cell[i] = v[i];
                p.CellSet[i] = !double.IsNaN(v[i]);
            }
        }

        [Help("Sets the pending space group by symbol: Hermann-Mauguin short or full notation, Hall symbol, or an IT number as text. Spaces and '_' are ignored ('F m -3 m', 'P4_2/mnm'). If the group has multiple settings, append one (':1', ':2', ':H', ':R', ':b1', ...) -- an ambiguous symbol raises an error listing the candidates.", "string symbol")]
        public void SetSpaceGroup(string symbol)
        {
            var p = RequirePending("SetSpaceGroup");
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException("SetSpaceGroup: symbol is empty.");
            var text = symbol.Trim();

            //":setting" suffix を切り出す ("Fd-3m:2" / "R-3c:H" / "P21/c:b2" など)
            string setting = "";
            var colon = text.LastIndexOf(':');
            if (colon > 0)
            {
                setting = text[(colon + 1)..].Trim();
                text = text[..colon].Trim();
            }

            if (int.TryParse(text, out var it))
            {
                p.Series = ResolveSeriesByNumber(it, setting);
                return;
            }

            var key = CanonSG(text);
            var matches = new List<int>();
            for (int i = 1; i < SymmetryStatic.TotalSpaceGroupNumber; i++)//0 = Unknown はスキップ
                if (SymbolVariants(SymmetryStatic.Symmetries[i]).Any(n => CanonSG(n) == key))
                    matches.Add(i);
            if (setting.Length > 0)
                matches = [.. matches.Where(i => SymmetryStatic.Symmetries[i].SpaceGroupHMsubStr.Equals(setting, StringComparison.OrdinalIgnoreCase))];

            if (matches.Count == 0)
                throw new ArgumentException($"SetSpaceGroup: unknown space-group symbol '{symbol}'.");
            if (matches.Count > 1)
                throw new ArgumentException($"SetSpaceGroup: '{symbol}' matches {matches.Count} settings: {CandidateText(matches)} -- append a setting like ':{SymmetryStatic.Symmetries[matches[0]].SpaceGroupHMsubStr}'.");
            p.Series = matches[0];
        }

        [Help("Sets the pending space group by International Tables number (1-230). If the group has multiple settings (origin choices, unique axes, hexagonal/rhombohedral), pass one via 'setting' ('1', '2', 'H', 'R', 'b1', ...); with the default empty setting, an ambiguous number raises an error listing the candidates.", "int itNumber, string setting")]
        public void SetSpaceGroupByNumber(int itNumber, string setting = "")
        {
            var p = RequirePending("SetSpaceGroupByNumber");
            p.Series = ResolveSeriesByNumber(itNumber, setting ?? "");
        }

        [Help("Adds an atom to the pending draft: 'label' is a free label, 'element' is the element symbol (e.g. 'Mg'), x/y/z are fractional coordinates, 'occ' is the occupancy (0 < occ <= 1), and 'bIso' is the isotropic displacement parameter B in A^2. Equivalent positions, Wyckoff letters and multiplicities are derived automatically at Commit().", "string label, string element, double x, double y, double z, double occ, double bIso")]
        public void AddAtom(string label, string element, double x, double y, double z, double occ = 1.0, double bIso = 0.0)
            => RequirePending("AddAtom").AtomList.Add(new NewAtom(label ?? "", element ?? "", x, y, z, occ, bIso));

        [Help("Removes all atoms from the pending draft (e.g. to replace them after BeginEdit()). Bonds are regenerated from the new atoms at Commit().")]
        public void ClearAtoms()
        {
            var p = RequirePending("ClearAtoms");
            p.AtomList.Clear();
            p.AtomsReplaced = true;
        }

        [Help("Validates the pending draft, builds the crystal, and applies it as the current crystal in one step (the GUI and all open simulators update, as when a CIF file is loaded). All validation errors are reported together; on failure the current crystal is unchanged and the draft is kept, so it can be fixed and committed again.")]
        public void Commit()
        {
            var p = RequirePending("Commit");
            var errors = new List<string>();
            var sym = SymmetryStatic.Symmetries[p.Series];

            var cell = ResolveCell(p, sym, errors);

            //原子の検証 (エラーは全件集めてからまとめて投げる)
            var resolvedZ = new Dictionary<NewAtom, int>();
            foreach (var na in p.AtomList.OfType<NewAtom>())
            {
                var z = AtomStatic.AtomicNumber(na.Element, caseSensitive: false);
                if (z <= 0)
                    errors.Add($"AddAtom '{na.Label}': unknown element '{na.Element}'.");
                else
                    resolvedZ[na] = z;
                if (!(na.Occ > 0 && na.Occ <= 1))
                    errors.Add($"AddAtom '{na.Label}': occupancy must be 0 < occ <= 1 (got {na.Occ}).");
                if (na.BisoAng2 < 0)
                    errors.Add($"AddAtom '{na.Label}': bIso must be >= 0 A^2 (got {na.BisoAng2}).");
            }
            if (errors.Count > 0)
                throw new ArgumentException("Commit failed:\n" + string.Join("\n", errors));

            //構築 (Å/度 → 内部 nm/rad、B: Å² → nm²)
            var cellNmRad = (cell[0] / 10, cell[1] / 10, cell[2] / 10,
                             cell[3] / 180 * Math.PI, cell[4] / 180 * Math.PI, cell[5] / 180 * Math.PI);
            var atoms = new List<Atoms>();
            foreach (var o in p.AtomList)
            {
                if (o is Atoms a)
                    atoms.Add(a);//BeginEdit/LoadCifText 由来はそのまま渡す (異方性因子等を保持。Crystal ctor が ResetSymmetry を呼ぶ)
                else if (o is NewAtom na)
                {
                    var at = new Atoms(na.Label, resolvedZ[na], 0, 0, null, p.Series, new Vector3DBase(na.X, na.Y, na.Zf), na.Occ,
                        new DiffuseScatteringFactor(DiffuseScatteringFactor.Type.B, true, na.BisoAng2 / 100.0, 0, new double[6], new double[6], cellNmRad));
                    at.ResetVesta();//CIF 読込と同じ VESTA 既定色・半径
                    atoms.Add(at);
                }
            }

            //bonds: BeginEdit の引き継ぎを尊重。原子を入れ替えた/新規のときは VESTA 規則で自動生成 (CIF 読込と同じ)
            var bonds = (!p.AtomsReplaced && p.BondList is { Length: > 0 }) ? p.BondList
                      : Bonds.GetVestaBonds(atoms.Select(a => a.AtomicNumber));

            var built = new Crystal(cellNmRad, null, p.Series, p.Name, p.Col, p.Rot, [.. atoms], p.Reference, bonds);

            main.crystalControl.Crystal = built;//CIF 読込と同一の適用経路 (SetToInterface + CrystalChanged → 全シミュレータ更新)
            Application.DoEvents();
            pending = null;//成功時のみ破棄
        }

        #region pending の内部実装 (260805Cl)

        private PendingCrystal pending;
        private static readonly Random rndColor = new();

        private sealed class PendingCrystal
        {
            public string Name = "";
            public readonly double[] Cell = new double[6];//Å, Å, Å, 度, 度, 度
            public readonly bool[] CellSet = new bool[6];//明示指定フラグ (SG からの導出値と区別する)
            public int Series = 0;//SymmetryStatic.Symmetries の系列番号。0 = Unknown (対称性なし。CIF に空間群が無いときと同じ既定)
            public readonly List<object> AtomList = [];//Atoms (スナップショット由来) または NewAtom (AddAtom 由来)
            public bool AtomsReplaced = false;
            public Matrix3D Rot = new();
            public Color Col = Color.FromArgb(rndColor.Next(255), rndColor.Next(255), rndColor.Next(255));//CIF 読込と同じランダム色
            public (string Note, string Authors, string Journal, string Title) Reference = ("", "", "", "");
            public Bonds[] BondList = [];
        }

        private sealed record NewAtom(string Label, string Element, double X, double Y, double Zf, double Occ, double BisoAng2);

        private PendingCrystal RequirePending(string caller)
            => pending ?? throw new InvalidOperationException($"{caller}: no pending draft. Call BeginCreate(), BeginEdit(), or LoadCifText() first.");

        private void RequireNoPending(string caller)
        {
            if (pending != null)
                throw new InvalidOperationException($"{caller}: another draft is already pending. Call Commit() or Cancel() first.");
        }

        /// <summary>既存 Crystal を pending へ分解する (BeginEdit / LoadCifText 共用)</summary>
        private static PendingCrystal SnapshotOf(Crystal c)
        {
            var p = new PendingCrystal
            {
                Name = c.Name,
                Series = c.SymmetrySeriesNumber,
                Rot = new Matrix3D(c.RotationMatrix),
                Col = Color.FromArgb(c.Argb),
                Reference = (c.Note, c.PublAuthorName, c.Journal, c.PublSectionTitle),
                BondList = c.Bonds ?? [],
            };
            double[] cell = [c.A * 10, c.B * 10, c.C * 10, c.Alpha / Math.PI * 180, c.Beta / Math.PI * 180, c.Gamma / Math.PI * 180];
            for (int i = 0; i < 6; i++)
            {
                p.Cell[i] = cell[i];
                p.CellSet[i] = true;
            }
            foreach (var a in c.Atoms)
                p.AtomList.Add(a);
            return p;
        }

        /// <summary>空間群記号の照合用正規化 (空白・"_" を除去して小文字化)</summary>
        private static string CanonSG(string s) => s.Replace(" ", "").Replace("_", "").ToLowerInvariant();

        /// <summary>1 系列が名乗る記号のバリエーション (HM 短縮の "=" 別名・"(1)" 原点表示・"Hex"/"Rho" 軸表示・HM full・Hall)</summary>
        private static IEnumerable<string> SymbolVariants(Symmetry s)
        {
            foreach (var raw in s.SpaceGroupHMStr.Split('='))
            {
                var t = raw.Replace("sub", "");//DB は下付き添字を "sub" で表す ("P4sub2/mnm")
                yield return t;
                if (t.Length > 3 && t[^1] == ')' && char.IsDigit(t[^2]) && t[^3] == '(')
                    yield return t[..^3];//"Fd-3m(1)" → "Fd-3m"
                if (t.EndsWith("Hex", StringComparison.Ordinal) || t.EndsWith("Rho", StringComparison.Ordinal))
                    yield return t[..^3];//"R-3cHex" → "R-3c"
            }
            yield return s.SpaceGroupHMfullStr.Replace("sub", "");
            yield return s.SpaceGroupHallStr;
        }

        private static string CandidateText(List<int> series)
            => string.Join(", ", series.Select(i =>
            {
                var s = SymmetryStatic.Symmetries[i];
                return $"'{s.SpaceGroupHMStr}'" + (s.SpaceGroupHMsubStr.Length > 0 ? $" (setting '{s.SpaceGroupHMsubStr}')" : "");
            }));

        private static int ResolveSeriesByNumber(int itNumber, string setting)
        {
            var matches = new List<int>();
            for (int i = 1; i < SymmetryStatic.TotalSpaceGroupNumber; i++)
                if (SymmetryStatic.Symmetries[i].SpaceGroupNumber == itNumber)
                    matches.Add(i);
            if (matches.Count == 0)
                throw new ArgumentException($"SetSpaceGroupByNumber: no space group has IT number {itNumber}.");
            if (setting.Length > 0)
            {
                var filtered = matches.Where(i => SymmetryStatic.Symmetries[i].SpaceGroupHMsubStr.Equals(setting, StringComparison.OrdinalIgnoreCase)).ToList();
                if (filtered.Count == 0)
                    throw new ArgumentException($"SetSpaceGroupByNumber: IT {itNumber} has no setting '{setting}'. Available: {CandidateText(matches)}.");
                matches = filtered;
            }
            if (matches.Count > 1)
                throw new ArgumentException($"SetSpaceGroupByNumber: IT {itNumber} has {matches.Count} settings: {CandidateText(matches)} -- pass the 'setting' argument.");
            return matches[0];
        }

        /// <summary>セル 6 成分を SG 制約と明示/導出フラグから解決する (Å/度)。エラーは errors へ蓄積。</summary>
        private static double[] ResolveCell(PendingCrystal p, Symmetry sym, List<string> errors)
        {
            string[] names = ["a", "b", "c", "alpha", "beta", "gamma"];
            //eq[i] = 成分 i が等値に従う先頭成分 (-1 = 独立)。fix[i] = 固定角 (度, NaN = 固定なし)
            int[] eq = [-1, -1, -1, -1, -1, -1];
            double[] fix = [double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN];
            var system = sym.CrystalSystemStr;
            switch (system)
            {
                case "cubic": eq[1] = 0; eq[2] = 0; fix[3] = fix[4] = fix[5] = 90; break;
                case "tetragonal": eq[1] = 0; fix[3] = fix[4] = fix[5] = 90; break;
                case "orthorhombic": fix[3] = fix[4] = fix[5] = 90; break;
                case "hexagonal": eq[1] = 0; fix[3] = fix[4] = 90; fix[5] = 120; break;
                case "trigonal":
                    if (sym.SpaceGroupHMsubStr.Equals("R", StringComparison.OrdinalIgnoreCase))
                    { eq[1] = 0; eq[2] = 0; eq[4] = 3; eq[5] = 3; }//菱面体設定: b=c=a、β=γ=α (a と α が独立)
                    else
                    { eq[1] = 0; fix[3] = fix[4] = 90; fix[5] = 120; }//六方設定
                    break;
                case "monoclinic":
                    //系列の主軸設定 (unique axis) に従い、自由な角以外を 90° に固定する
                    var axis = sym.MainAxis.Length > 0 ? sym.MainAxis[0] : 'b';
                    fix[3] = fix[4] = fix[5] = 90;
                    fix[axis switch { 'a' => 3, 'c' => 5, _ => 4 }] = double.NaN;
                    break;
                default: break;//triclinic / Unknown: 全成分独立
            }

            var v = new double[6];
            const double tol = 1e-4;
            //第 1 パス: 固定値と独立成分
            for (int i = 0; i < 6; i++)
            {
                if (!double.IsNaN(fix[i]))
                {
                    if (p.CellSet[i] && Math.Abs(p.Cell[i] - fix[i]) > tol)
                        errors.Add($"{names[i]} must be {fix[i]}° for {system} '{sym.SpaceGroupHMStr}' (got {p.Cell[i]}°).");
                    v[i] = fix[i];
                }
                else if (eq[i] < 0)
                {
                    if (!p.CellSet[i])
                        errors.Add($"{names[i]} is required for {system} '{sym.SpaceGroupHMStr}'.");
                    else
                        v[i] = p.Cell[i];
                }
            }
            //第 2 パス: 等値成分 (参照先は必ず独立成分)
            for (int i = 0; i < 6; i++)
            {
                if (double.IsNaN(fix[i]) && eq[i] >= 0)
                {
                    var j = eq[i];
                    if (p.CellSet[i] && p.CellSet[j] && Math.Abs(p.Cell[i] - p.Cell[j]) > tol * Math.Max(1, Math.Abs(p.Cell[j])))
                        errors.Add($"{names[i]} must equal {names[j]} for {system} '{sym.SpaceGroupHMStr}' ({names[j]} = {p.Cell[j]}, {names[i]} = {p.Cell[i]}).");
                    v[i] = v[j];
                }
            }
            //数値検証
            for (int i = 0; i < 3; i++)
                if (p.CellSet[i] && v[i] <= 0)
                    errors.Add($"{names[i]} must be > 0 Å (got {v[i]}).");
            for (int i = 3; i < 6; i++)
                if (p.CellSet[i] && (v[i] <= 0 || v[i] >= 180))
                    errors.Add($"{names[i]} must be within (0°, 180°) (got {v[i]}).");
            if (errors.Count == 0)
            {
                double ca = Math.Cos(v[3] / 180 * Math.PI), cb = Math.Cos(v[4] / 180 * Math.PI), cg = Math.Cos(v[5] / 180 * Math.PI);
                if (1 - ca * ca - cb * cb - cg * cg + 2 * ca * cb * cg <= 0)
                    errors.Add($"the cell angles (alpha = {v[3]}°, beta = {v[4]}°, gamma = {v[5]}°) are geometrically impossible.");
            }
            return v;
        }

        #endregion pending の内部実装
    }
    #endregion

    #region CrystalList クラス
    public class CrystalListClass(Macro _p) : MacroSub(_p.main)
    {
        private FormMain main => _p.main;

        // (260414Ch) Help text revised to use consistent property/method wording.
        [Help("Gets or sets the index of the selected crystal in the crystal list.")]
        public int SelectedIndex { get => main.SelectedCrystalIndex; set => main.SelectedCrystalIndex = value; }

        // 260415Cl 追加 結晶リスト全件数 (マクロサンプル 05 等で利用)
        // 注: main.Crystals は選択中の結晶のみを返すので使用不可。listBox.Items.Count を返す main.CrystalCount を使う。
        [Help("Gets the number of crystals currently in the crystal list.")]
        public int Count { get => main.CrystalCount; }

        [Help("Adds the crystal currently shown in 'Crystal Information' to the end of the list.")]
        public void Add() => Execute(() => main.AddCrystal());

        [Help("Replaces the selected item in the list with the crystal currently shown in 'Crystal Information'.")]
        public void Replace() => Execute(() => main.ReplaceCrystal());

        [Help("Deletes the selected crystal from the list.")]
        public void Delete() => Execute(() => main.DeleteCrystal());

        [Help("Deletes all crystals from the list.")]
        public void ClearAll() => Execute(() => main.CrystalListClear());

        [Help("Moves the selected crystal up in the list.")]
        public void MoveUp() => Execute(() => main.MoveUp());

        [Help("Moves the selected crystal down in the list.")]
        public void MoveDown() => Execute(() => main.MoveDown());
    }
    #endregion

    #region Dir (Direction 方位)クラス
    public class DirectionClass(Macro _p) : MacroSub(_p.main)
    {
        private FormMain main => _p.main;

        // (260414Ch) Help text revised and corrected where axis/plane meanings were swapped.
        [Help("Sets the current crystal orientation from Euler angles in radians.", "double phi, double theta, double psi")]
        public void Euler(double phi, double theta, double psi)
        {
            main.SetRotation(phi, theta, psi);
            Application.DoEvents();
        }

        [Help("Sets the current crystal orientation from Euler angles in degrees.", "double phi, double theta, double psi")]
        public void EulerInDegree(double phi, double theta, double psi)
        {
            main.SetRotation(phi / 180.0 * Math.PI, theta / 180.0 * Math.PI, psi / 180.0 * Math.PI);
            Application.DoEvents();
        }

        [Help("Sets the current crystal orientation from Euler angles in degrees.", "double phi, double theta, double psi")]
        public void EulerInDeg(double phi, double theta, double psi)
        {
            main.SetRotation(phi / 180.0 * Math.PI, theta / 180.0 * Math.PI, psi / 180.0 * Math.PI);
            Application.DoEvents();
        }

        [Help("Rotates the current crystal around the specified axis vector (vX, vY, vZ) by the specified angle in radians.", 
            "double vX, double vY, double vZ, double angle")]
        public void Rotate(double vX, double vY, double vZ, double angle) => main.Rotate((vX, vY, vZ), angle);

        [Help("Rotates the current crystal around the specified axis vector (vX, vY, vZ) by the specified angle in degrees."
            , "double vX, double vY, double vZ, double angle")]
        public void RotateInDeg(double vX, double vY, double vZ, double angle) => Rotate(vX, vY, vZ, angle*Math.PI/180.0);

        [Help("Rotates the current crystal around the crystallographic direction [uvw] by the specified angle in radians.", 
            "int u, int v, int w, double angle")]
        public void RotateAroundAxis(int u, int v, int w, double angle)
        {
            Vector3DBase a = main.Crystal.A_Axis, b = main.Crystal.B_Axis, c = main.Crystal.C_Axis;
            var axis = main.Crystal.RotationMatrix * (u * a + v * b + w * c);
            main.Rotate(axis, angle);
        }
        [Help("Rotates the current crystal around the crystallographic direction [uvw] by the specified angle in degrees."
            , "int u, int v, int w, double angle")]
        public void RotateAroundAxisInDeg(int u, int v, int w, double angle)=> RotateAroundAxis(u, v, w, angle * Math.PI / 180.0);

        [Help("Rotates the current crystal around the normal of the crystallographic plane (hkl) by the specified angle in radians.", "int h, int k, int l, double angle")]
        
        public void RotateAroundPlane(int h, int k, int l, double angle)
        {
            var rot = main.Crystal.MatrixInverse;
            var axis = main.Crystal.RotationMatrix * (h * rot.Row1 + k * rot.Row2 + l * rot.Row3);
            main.Rotate(axis, angle);
        }

        [Help("Rotates the current crystal around the normal of the crystallographic plane (hkl) by the specified angle in degrees.", "int h, int k, int l, double angle")]
        public void RotateAroundPlaneInDeg(int h, int k, int l, double angle)=>RotateAroundPlane(h, k, l, angle * Math.PI / 180.0);

        [Help("Rotates the current crystal so that the specified plane (hkl) becomes parallel to the screen.", "int h, int k, int l")]
        public void ProjectAlongPlane(int h, int k, int l)
        {
            main.SetPlane(h, k, l);
            main.ProjectAlongPlane();
            Application.DoEvents();
        }

        [Help("Rotates the current crystal so that the specified direction [uvw] is normal to the screen.", "int u, int v, int w")]
        public void ProjectAlongAxis(int u, int v, int w)
        {
            main.SetAxis(u, v, w);
            main.ProjectAlongAxis();
            Application.DoEvents();
        }

        //260805Cl 追加 (マクロ整備方針 P1-2): 現在方位の取得系。正本は回転行列 — Euler は gimbal 位置 (θ=0, π) で
        //非一意のため、Euler→GetEuler の往復は「同じ姿勢」のみ保証し、同じ数値列は保証しない。
        //行列の規約は SpotID.CandidateList() の R11–R33 と同一 (結晶座標系→実験室座標系、列ベクトルに作用、行優先で列挙)。
        [Help("Gets the current crystal orientation as Z-X-Z Euler angles in radians, as a three-element array [phi, theta, psi]. Euler angles are not unique at gimbal positions (theta = 0 or pi), so setting them back reproduces the same attitude, not necessarily the same numbers; use GetRotationMatrix() for exact save/restore.")]
        public double[] GetEuler()
        {
            var (phi, theta, psi) = Crystallography.Euler.FromMatrix(main.Crystal.RotationMatrix);
            return [phi, theta, psi];
        }

        [Help("Gets the current crystal orientation as Z-X-Z Euler angles in degrees, as a three-element array [phi, theta, psi]. See GetEuler() for the uniqueness caveat.")]
        public double[] GetEulerInDeg()
        {
            var (phi, theta, psi) = Crystallography.Euler.FromMatrix(main.Crystal.RotationMatrix);
            return [phi / Math.PI * 180, theta / Math.PI * 180, psi / Math.PI * 180];
        }

        [Help("Gets the current crystal rotation matrix as a nine-element array [R11, R12, R13, R21, R22, R23, R31, R32, R33] (crystal frame to laboratory frame, applied to column vectors) -- the same convention as SpotID.CandidateList(). Pair it with SetRotationMatrix() to save and restore the orientation exactly.")]
        public double[] GetRotationMatrix()
        {
            var m = main.Crystal.RotationMatrix;
            return [m.E11, m.E12, m.E13, m.E21, m.E22, m.E23, m.E31, m.E32, m.E33];
        }

        [Help("Sets the crystal orientation from nine rotation-matrix elements, in the same convention and order as GetRotationMatrix(). The elements must form a proper rotation (orthonormal, determinant +1) within a tolerance of 0.01, and are re-orthonormalized before being applied (so slightly rounded values, e.g. copied from a CSV, are accepted).", "double r11, double r12, double r13, double r21, double r22, double r23, double r31, double r32, double r33")]
        public void SetRotationMatrix(double r11, double r12, double r13, double r21, double r22, double r23, double r31, double r32, double r33)
        {
            //検証: 各行の長さ・行間の直交性・右手系 (det=+1) を許容誤差 0.01 で確認 (CSV からの丸め値も通る緩さ。鏡映 det=-1 は拒否)
            double n1 = Math.Sqrt(r11 * r11 + r12 * r12 + r13 * r13);
            double n2 = Math.Sqrt(r21 * r21 + r22 * r22 + r23 * r23);
            double n3 = Math.Sqrt(r31 * r31 + r32 * r32 + r33 * r33);
            double d12 = r11 * r21 + r12 * r22 + r13 * r23;
            double d23 = r21 * r31 + r22 * r32 + r23 * r33;
            double d31 = r31 * r11 + r32 * r12 + r33 * r13;
            double det = r11 * (r22 * r33 - r23 * r32) - r12 * (r21 * r33 - r23 * r31) + r13 * (r21 * r32 - r22 * r31);
            const double tol = 0.01;
            if (Math.Abs(n1 - 1) > tol || Math.Abs(n2 - 1) > tol || Math.Abs(n3 - 1) > tol ||
                Math.Abs(d12) > tol || Math.Abs(d23) > tol || Math.Abs(d31) > tol || Math.Abs(det - 1) > tol)
                throw new ArgumentException("SetRotationMatrix: the nine elements do not form a proper rotation matrix (orthonormal, determinant +1).");

            //Gram-Schmidt で再直交化してから適用する (入力の丸め誤差を回転状態に持ち込まない。第 3 行は外積で右手系を保証)
            r11 /= n1; r12 /= n1; r13 /= n1;
            var p = r21 * r11 + r22 * r12 + r23 * r13;
            r21 -= p * r11; r22 -= p * r12; r23 -= p * r13;
            var n2b = Math.Sqrt(r21 * r21 + r22 * r22 + r23 * r23);
            r21 /= n2b; r22 /= n2b; r23 /= n2b;
            r31 = r12 * r23 - r13 * r22; r32 = r13 * r21 - r11 * r23; r33 = r11 * r22 - r12 * r21;

            //⚠ Matrix3D の 9 引数コンストラクタは列優先 (e11, e21, e31, …) なので、行の取り違えを避けるためフィールド初期化子で構築する
            main.SetRotation(new Matrix3D { E11 = r11, E12 = r12, E13 = r13, E21 = r21, E22 = r22, E23 = r23, E31 = r31, E32 = r32, E33 = r33 });
            Application.DoEvents();
        }
    }
    #endregion

    #region DiffractionSimulatorクラス
    public class DifSimClass(Macro _p) : MacroSub(_p.main)
    {
        private FormDiffractionSimulator difSim => _p.main.FormDiffractionSimulator;
        private Crystal c => _p.main.Crystal;

        // (260414Ch) Help text revised for natural English and corrected units/semantics where needed.
        [Help("Opens the Diffraction Simulator window.")]
        public void Open() => Execute(new Action(() => difSim.Visible = true));
        
        [Help("Closes the Diffraction Simulator window.")] 
        public void Close() => Execute(new Action(() => difSim.Visible = false));


        [Help("Sets the incident wave source to X-ray.")]
        public void Source_Xray() { difSim.Source = WaveSource.Xray; }
        [Help("Sets the incident wave source to electrons.")]
        public void Source_Electron() { difSim.Source = WaveSource.Electron; }
        
        [Help("Sets the incident wave source to neutrons.")]
        public void Source_Neutron() { difSim.Source = WaveSource.Neutron; }

        [Help("Gets or sets the incident beam energy. Units: keV for X-rays and electrons, meV for neutrons.")]
        public double Energy { get => difSim.Energy; set => difSim.Energy = value; }

        [Help("Gets or sets the incident wavelength in nm.")] 
        public double Wavelength { get => difSim.WaveLength; set => difSim.WaveLength = value; }

        [Help("Gets or sets the specimen thickness in nm.")]
        public double Thickness { get => difSim.Thickness; set => difSim.Thickness = value; }

        [Help("Gets or sets the number of diffracted waves used for dynamical calculations.")]
        public int NumberOfDiffractedWaves { get => difSim.NumberOfDiffractedWaves; set => difSim.NumberOfDiffractedWaves = value; }

        [Help("Sets the beam mode to a parallel beam.")]
        public void Beam_Parallel() => difSim.BeamMode = FormDiffractionSimulator.BeamModes.Parallel;

        [Help("Sets the beam mode to X-ray precession.")]
        public void Beam_PrecessionXray() => difSim.BeamMode = FormDiffractionSimulator.BeamModes.PrecessionXray;

        [Help("Sets the beam mode to electron precession.")]
        public void Beam_PrecessionElectron() => difSim.BeamMode = FormDiffractionSimulator.BeamModes.PrecessionElectron;

        [Help("Sets the beam mode to a convergent electron beam.")]
        public void Beam_Convergence() => difSim.BeamMode = FormDiffractionSimulator.BeamModes.Convergence;

        [Help("Uses excitation error only for intensity calculations.")]
        public void Calc_Excitation() => difSim.CalcMode = FormDiffractionSimulator.CalcModes.Excitation;

        [Help("Uses excitation error and structure factors for intensity calculations.")]
        public void Calc_Kinematical() => difSim.CalcMode = FormDiffractionSimulator.CalcModes.Kinematical;

        [Help("Uses dynamical theory for intensity calculations.")]
        public void Calc_Dynamical() => difSim.CalcMode = FormDiffractionSimulator.CalcModes.Dynamical;

        [Help("Gets or sets the image resolution in mm/pixel.")]
        public double ImageResolutionInMM
        {
            get 
            {
                difSim.ResolutionUnit = LengthUnitEnum.MilliMeter;
                return difSim.Resolution; 
            }
            set
            {
                difSim.ResolutionUnit = LengthUnitEnum.MilliMeter;
                difSim.Resolution = value;
            }
        }

        [Help("Gets or sets the image resolution in nm^-1/pixel.")]
        public double ImageResolutionInNMinv
        {
            get
            {
                difSim.ResolutionUnit = LengthUnitEnum.NanoMeterInverse; 
                return difSim.ResolutionInNMinv;
            }
            set
            {
                difSim.ResolutionUnit = LengthUnitEnum.NanoMeterInverse;
                difSim.ResolutionInNMinv = value;
            }
        }
 
        [Help("Gets or sets the image width in pixels.")]
        public int ImageWidth { get => difSim.ClientWidth; set => difSim.ClientWidth = value; }

        [Help("Gets or sets the image height in pixels.")] 
        public int ImageHeight { get => difSim.ClientHeight; set => difSim.ClientHeight = value; }

        [Help("Sets the image size in pixels.", "int width, int height")] 
        public void ImageSize(int width, int height) { ImageWidth = width; ImageHeight = height; }

        [Help("Gets or sets CameraLength2, the sample-to-detector distance, in mm.")]
        public double CameraLength2 { get => difSim.CameraLength2; set => difSim.CameraLength2 = value; }

        [Help("Gets or sets the detector tilt angle Tau in radians.")]
        public double Tau { get => difSim.Tau; set => difSim.Tau = value; }

        [Help("Gets or sets the detector tilt angle Tau in degrees.")]
        public double TauInDeg { get => difSim.Tau/Math.PI*180; set => difSim.Tau = value*Math.PI / 180; }

        [Help("Gets or sets the detector tilt angle Phi in radians.")]
        public double Phi { get => difSim.Phi ; set => difSim.Phi = value; }

        [Help("Gets or sets the detector tilt angle Phi in degrees.")]
        public double PhiInDeg { get => difSim.Phi / Math.PI * 180; set => difSim.Phi = value * Math.PI / 180; }

        [Help("Sets the detector-coordinate position of the image center in mm. (0, 0) corresponds to the foot of the perpendicular from the sample to the detector.", "double x, double y")]
        public void Foot(double x, double y) { difSim.Foot = new PointD(x, y); }

        [Help("Gets or sets whether the final screen rendering step is skipped. Spot positions are still calculated.")]
        public bool SkipRendering { get => difSim.SkipRendering; set => difSim.SkipRendering = value; }

        [Help("Saves the current simulation image as a PNG file. If 'filename' is omitted, opens a save dialog.", "string filename")]
        public void SaveAsPng(string filename = "") => difSim.SaveOrCopy(true, true, true, filename);




        // 260723Cl Help文言更新 (旧: "Returns spot information for the current dynamical calculation as CSV text.")
        //   Kinematical/Excitation モード対応と検出器座標列 (detX, detY) の追加に伴い記述を拡張。
        [Help("Returns spot information for the current calculation as CSV text. Units: d in nm, g and Sg in nm^-1, detX/detY in mm (detector coordinates; origin = foot of the perpendicular from the sample). In dynamical mode, dynamical quantities (Ug/Vg, Sg, φ) are included; in kinematical/excitation mode, |F|^2 and relative intensity are included for reflections whose excitation error is within the spot radius.")]
        public string SpotInfo() => (Execute(() => spotInfo()));
        private string spotInfo()
        {
            // 260723Cl 追加: Back Laue は ConvertReciprocalToDetector が常に (0,0) を返し無効データになるため明示的に未対応とする (codex 指摘)
            if (difSim.IsBackLaue)
                return "Error: SpotInfo() does not support the Back Laue mode.";

            // 260723Cl 数値は全て InvariantCulture (小数点=ピリオド) で出力する (codex 指摘: 小数点=カンマのロケールで CSV が壊れる)
            var gamma = 1 + UniversalConstants.e0 * Energy * 1000 / UniversalConstants.m0 / UniversalConstants.c2;
            double coeff;
            var sb = new StringBuilder();
            if (difSim.CalcMode == FormDiffractionSimulator.CalcModes.Dynamical)
            {
                if (difSim.FormDiffractionBeamTable.UnitOfPotential == FormDiffractionSpotInfo.UnitOfPotentialEnum.Vg)
                {
                    //sb.Append("No., R, H, K, L, d, gX, gY, gZ,|g|=1/d, Vg_re, Vg_im, V'g_re, V'g_im, Sg, Pg, Qg, φ_re, φ_im, |φ|^2\n"); // 260723Cl 旧: 検出器座標なし・区切り後空白あり
                    sb.Append("No.,R,H,K,L,d(nm),gX(1/nm),gY(1/nm),gZ(1/nm),|g|=1/d(1/nm),Vg_re,Vg_im,V'g_re,V'g_im,Sg(1/nm),Pg,Qg,φ_re,φ_im,|φ|^2,detX(mm),detY(mm)\n");
                    coeff = 1 / gamma * 6.62606896 * 6.62606896 / 2 / 9.1093897 / 1.60217733;
                }
                else
                {
                    //sb.Append("No., R, H, K, L, d, gX, gY, gZ,|g|=1/d, Ug_re, Ug_im, U'g_re, U'g_im, Sg, Pg, Qg, φ_re, φ_im, |φ|^2\n"); // 260723Cl 旧: 検出器座標なし・区切り後空白あり
                    sb.Append("No.,R,H,K,L,d(nm),gX(1/nm),gY(1/nm),gZ(1/nm),|g|=1/d(1/nm),Ug_re,Ug_im,U'g_re,U'g_im,Sg(1/nm),Pg,Qg,φ_re,φ_im,|φ|^2,detX(mm),detY(mm)\n");
                    coeff = 1 / gamma;
                }

                int n = 0;
                foreach (var b in c.Bethe.Beams)
                {
                    var g = b.Vec.Length;
                    sb.Append(Invariant($"{n++},{b.Rating},{b.H},{b.K},{b.L},{1 / g},"));
                    sb.Append(Invariant($"{b.Vec.X},{b.Vec.Y},{b.Vec.Z},{g},"));
                    sb.Append(Invariant($"{b.Ureal.Real * coeff},{b.Ureal.Imaginary * coeff},{b.Uimag.Real * coeff},{b.Uimag.Imaginary * coeff},"));
                    sb.Append(Invariant($"{b.S},{b.P},{b.Q},"));
                    sb.Append(Invariant($"{b.Psi.Real},{b.Psi.Imaginary},{b.Psi.MagnitudeSquared()}"));
                    var pt = difSim.ConvertReciprocalToDetector(b.Vec); // 260723Cl 追加: 検出器座標 (mm)
                    sb.Append(Invariant($",{pt.X},{pt.Y}"));
                    sb.Append('\n');
                }
                return sb.ToString();
            }
            else // 260723Cl 追加: Kinematical / Excitation モード (旧: 未実装で空文字を返していた)
            {
                //描画ループ (FormDiffractionSimulator.Draw) と同じ規約で、結晶回転後の逆格子ベクトルから励起誤差 Sg を求め、
                //|Sg| が Spot radius (ExcitationError) 以内の反射のみ出力する (描画される集合とは点広がり表示等で厳密には一致しない)。
                sb.Append("No.,H,K,L,d(nm),gX(1/nm),gY(1/nm),gZ(1/nm),|g|=1/d(1/nm),Sg(1/nm),|F|^2,RelativeIntensity,detX(mm),detY(mm)\n");
                int n = 0;
                double ewald = difSim.EwaldRadius;
                var precessionX = difSim.BeamMode == FormDiffractionSimulator.BeamModes.PrecessionXray;
                foreach (var g in c.VectorOfG.Where(g => g.Flag1))
                {
                    var vec = c.RotationMatrix * g;
                    var dev = precessionX ? -vec.Z : ewald - Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y + (-vec.Z + ewald) * (-vec.Z + ewald));
                    if (Math.Abs(dev) < difSim.ExcitationError)
                    {
                        var pt = difSim.ConvertReciprocalToDetector(vec);
                        sb.Append(Invariant($"{n++},{g.Index.h},{g.Index.k},{g.Index.l},{g.d},"));
                        sb.Append(Invariant($"{vec.X},{vec.Y},{vec.Z},{1 / g.d},{dev},"));
                        sb.Append(Invariant($"{g.RawIntensity},{g.RelativeIntensity},{pt.X},{pt.Y}\n"));
                    }
                }
                return sb.ToString();
            }
            //return ""; // 260723Cl 旧: else 分岐が未実装だったときの到達コード
        }

    }



    #endregion

    #region SpotIDクラス
    // 260801Cl 追加: Spot ID (v2) をマクロ/コマンドラインから駆動するためのクラス。
    // 画像またはスポット一覧を読み込み → スポット検出 → 方位同定 → 候補リストの取得、までを無人で実行できる。
    // FindSpots()/Identify() は完了を待って戻る (フォーム側で同期実行するように実装してある)。
    public class SpotIDClass(Macro _p) : MacroSub(_p.main)
    {
        private FormSpotIDV2 spotID => _p.main.FormSpotIDv2;

        [Help("Opens the Spot ID window.")]
        public void Open() => Execute(new Action(() => { spotID.Visible = true; _p.main.toolStripButtonSpotIDv2.Checked = true; }));

        [Help("Closes the Spot ID window.")]
        public void Close() => Execute(new Action(() => { spotID.Visible = false; _p.main.toolStripButtonSpotIDv2.Checked = false; }));

        [Help("Loads a file into the Spot ID window, as File > Load does: a '.csv' file is read as a spot list (an image must have been loaded first), and any other extension is read as a diffraction pattern image (dm3, dm4, mrc, ipa, tif, and other supported formats). If 'filename' is omitted, opens a file selection dialog.", "string filename")]
        public void LoadFile(string filename = "") => Execute(() => spotID.LoadFile(filename));

        [Help("Detects diffraction spots in the loaded image and fits them, as the 'Find spots' button does. Returns after the detection has finished.")]
        public void FindSpots() => Execute(() => spotID.FindSpots());

        [Help("Searches for orientations that explain the detected spots, as the 'Identify spots' button does, and returns the number of candidates found. The crystals to be tested are those selected in the crystal list of the main window (see CrystalList.SelectedIndex). Returns after the search has finished.")]
        public int Identify() => Execute(() => spotID.Identify());

        [Help("Returns the candidate orientation list as CSV text: crystal name, the Z-X-Z Euler angles (deg), the nine rotation-matrix elements R11-R33 (crystal frame to laboratory frame, applied to column vectors), the mean-squared residual (nm^-2), and the assignment of observed spots to hkl indices. The candidates are ordered by the total number of assigned spots (descending), then by the mean-squared residual (ascending). Numbers are written in invariant culture (decimal point = period).")]
        public string CandidateList() => Execute(() => spotID.GetCandidateListText(','));

        [Help("Returns the list of observed spots as CSV text, with the same columns as File > Save in the Spot ID window. Combine it with File.SaveText() to write the list to a file; the saved file can be read back with LoadFile().")]
        public string SpotList() => Execute(() => spotID.GetSpotListText(','));

        [Help("Gets the number of detected spots.")]
        public int NumberOfDetectedSpots => spotID.NumberOfDetectedSpots;

        [Help("Gets the number of candidates found by the last Identify() call.")]
        public int NumberOfCandidates => spotID.NumberOfCandidates;

        [Help("Sets the incident wave source to X-ray.")]
        public void Source_Xray() => spotID.Source = WaveSource.Xray;

        [Help("Sets the incident wave source to electrons.")]
        public void Source_Electron() => spotID.Source = WaveSource.Electron;

        [Help("Sets the incident wave source to neutrons.")]
        public void Source_Neutron() => spotID.Source = WaveSource.Neutron;

        [Help("Gets or sets the incident beam energy. Units: keV for X-rays and electrons, meV for neutrons.")]
        public double Energy { get => spotID.Energy; set => spotID.Energy = value; }

        [Help("Gets or sets the camera length in mm.")]
        public double CameraLength { get => spotID.CameraLength; set => spotID.CameraLength = value; }

        [Help("Gets or sets the pixel size of the image in mm. Reading or writing it also switches the pixel-size unit to mm.")]
        public double PixelSizeInMM { get => spotID.PixelSizeInMM; set => spotID.PixelSizeInMM = value; }

        [Help("Gets or sets the pixel size of the image in nm^-1. Reading or writing it also switches the pixel-size unit to nm^-1.")]
        public double PixelSizeInNMinv { get => spotID.PixelSizeInNMinv; set => spotID.PixelSizeInNMinv = value; }

        [Help("Gets or sets the maximum number of spots to be detected by FindSpots().")]
        public int MaxNumberOfSpots { get => spotID.MaxNumberOfSpots; set => spotID.MaxNumberOfSpots = value; }

        [Help("Gets or sets the minimum separation, in pixels, allowed between detected spots.")]
        public int NearestNeighbor { get => spotID.NearestNeighbor; set => spotID.NearestNeighbor = value; }

        [Help("Gets or sets the radius, in pixels, of the circular region around each spot used for peak fitting.")]
        public double FittingRange { get => spotID.FittingRange; set => spotID.FittingRange = value; }

        // 260801Cl: フォーム側の既存プロパティ ToleranceLength (比) をそのまま使い、ここで GUI 表示と同じ % に換算する
        // (同じコントロールを指す重複プロパティをフォーム側に増やさないため)。
        [Help("Gets or sets the tolerance, in %, of the relative d-spacing difference allowed when matching observed spots to candidate reflections.")]
        public double AcceptableError { get => spotID.ToleranceLength * 100; set => spotID.ToleranceLength = value / 100; }

        [Help("Gets or sets whether kinematically forbidden reflections, which can appear via multiple diffraction, are ignored.")]
        public bool IgnoreProhibitedReflections { get => spotID.IgnoreProhibitedReflections; set => spotID.IgnoreProhibitedReflections = value; }

        [Help("Gets or sets whether multiple grains are searched for. False means a single grain.")]
        public bool MultiGrain { get => spotID.MultiGrain; set => spotID.MultiGrain = value; }

        [Help("Gets or sets the maximum number of grain orientations searched for when MultiGrain is true.")]
        public int MaxNumberOfGrains { get => spotID.MaxNumberOfGrains; set => spotID.MaxNumberOfGrains = value; }
    }
    #endregion

    #region StructureViewer クラス
    //260805Cl 追加 (マクロ整備方針 P1-4): 結晶構造ビューア (FormStructureViewer) をマクロから駆動する。
    //SaveImage / Export3DModel は GL とモデルが表示時に初期化されるため、未表示なら先に Open() 相当を行う。
    public class StructureViewerClass(Macro _p) : MacroSub(_p.main)
    {
        private FormStructureViewer viewer => _p.main.FormStructureViewer;

        [Help("Opens the Structure Viewer window.")]
        public void Open() => Execute(new Action(() => { viewer.Visible = true; _p.main.toolStripButtonStructureViewer.Checked = true; Application.DoEvents(); }));

        [Help("Closes the Structure Viewer window.")]
        public void Close() => Execute(new Action(() => { viewer.Visible = false; _p.main.toolStripButtonStructureViewer.Checked = false; }));

        [Help("Saves the rendered main view as a PNG file, at the pixel size given by the Size (W x H) box of the window. The window is opened first when necessary. If 'filename' is omitted, opens a save dialog.", "string filename")]
        public void SaveImage(string filename = "")
        {
            Open();
            Execute(() => { viewer.SaveMainImage(filename ?? ""); return true; });
        }

        [Help("Exports the displayed structure as a 3D-print model, like File > Export 3D Model (3MF/STL). The extension picks the format: '.stl' (single color) or '.3mf' (parts colored by element). The model is scaled so that its largest dimension becomes maxSizeInMM, or by the fixed scale fixedScaleInMMperNm when that is > 0. The include switches act only on element kinds actually displayed; polyhedraAsEdges outputs coordination polyhedra as edge frames of diameter polyEdgeDiaInMM; includeCellEdges turns the unit-cell frame into cylinders of diameter cellEdgeDiaInMM; bonds that would print thinner than thickenBondsToMM are thickened to that diameter (0 disables). The defaults equal the dialog defaults. Returns an information string with the triangle count and the printed size.", "string filename, double maxSizeInMM, double fixedScaleInMMperNm, bool includeAtoms, bool includeBonds, bool includePolyhedra, bool polyhedraAsEdges, double polyEdgeDiaInMM, bool includeCellEdges, double cellEdgeDiaInMM, double thickenBondsToMM")]
        public string Export3DModel(string filename, double maxSizeInMM = 80, double fixedScaleInMMperNm = 0,
            bool includeAtoms = true, bool includeBonds = true, bool includePolyhedra = true, bool polyhedraAsEdges = false,
            double polyEdgeDiaInMM = 2.0, bool includeCellEdges = true, double cellEdgeDiaInMM = 2.4, double thickenBondsToMM = 1.2)
        {
            Open();
            return Execute(() => export3DModel(filename, maxSizeInMM, fixedScaleInMMperNm, includeAtoms, includeBonds,
                includePolyhedra, polyhedraAsEdges, polyEdgeDiaInMM, includeCellEdges, cellEdgeDiaInMM, thickenBondsToMM));
        }

        private string export3DModel(string filename, double maxSizeInMM, double fixedScaleInMMperNm,
            bool includeAtoms, bool includeBonds, bool includePolyhedra, bool polyhedraAsEdges,
            double polyEdgeDiaInMM, bool includeCellEdges, double cellEdgeDiaInMM, double thickenBondsToMM)
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentException("Export3DModel: filename is required.");
            bool use3mf;
            if (filename.EndsWith(".3mf", StringComparison.OrdinalIgnoreCase)) use3mf = true;
            else if (filename.EndsWith(".stl", StringComparison.OrdinalIgnoreCase)) use3mf = false;
            else throw new ArgumentException("Export3DModel: the filename must end with '.stl' or '.3mf'.");

            GLObject[] objs;
            lock (viewer.lockObj1)
                objs = [.. viewer.GLObjects];
            var snaps = ModelExporter.Collect(objs);
            var lineSnaps = ModelExporter.CollectLines(objs);

            //表示中の種類だけを対象にする (ダイアログの has* と同じ)
            static bool isAtom(MeshSnapshot s) => s.Kind is SnapshotKind.Sphere or SnapshotKind.Ellipsoid;
            static bool isPoly(MeshSnapshot s) => s.Kind == SnapshotKind.Polyhedron;
            includeAtoms &= snaps.Any(isAtom);
            includePolyhedra &= snaps.Any(isPoly);
            includeBonds &= snaps.Any(s => !isAtom(s) && !isPoly(s));
            includeCellEdges &= lineSnaps.Count > 0;
            if (!(includeAtoms || includeBonds || includePolyhedra || includeCellEdges))
                throw new InvalidOperationException("Export3DModel: nothing to export (no displayed atoms, bonds, polyhedra, or unit-cell edges match the switches).");

            //含める要素の合成バウンディングからスケールを決める (ダイアログの SizeNm と同じ規約)
            var v3max = new OpenTK.Mathematics.Vector3d(double.MaxValue);
            OpenTK.Mathematics.Vector3d min = v3max, max = -v3max;
            void merge((OpenTK.Mathematics.Vector3d Min, OpenTK.Mathematics.Vector3d Max) b)
            { min = OpenTK.Mathematics.Vector3d.ComponentMin(min, b.Min); max = OpenTK.Mathematics.Vector3d.ComponentMax(max, b.Max); }
            if (includeAtoms) merge(ModelExporter.GetBounds([.. snaps.Where(isAtom)]));
            if (includeBonds) merge(ModelExporter.GetBounds([.. snaps.Where(s => !isAtom(s) && !isPoly(s))]));
            if (includePolyhedra) merge(ModelExporter.GetBounds([.. snaps.Where(isPoly)]));
            if (includeCellEdges)
                foreach (var (s, t) in lineSnaps.SelectMany(l => l.Segments))
                { min = OpenTK.Mathematics.Vector3d.ComponentMin(OpenTK.Mathematics.Vector3d.ComponentMin(min, s), t); max = OpenTK.Mathematics.Vector3d.ComponentMax(OpenTK.Mathematics.Vector3d.ComponentMax(max, s), t); }
            var sizeNm = max - min;
            var maxExtent = Math.Max(sizeNm.X, Math.Max(sizeNm.Y, sizeNm.Z));
            var scale = fixedScaleInMMperNm > 0 ? fixedScaleInMMperNm
                      : maxExtent > 0 ? maxSizeInMM / maxExtent
                      : throw new InvalidOperationException("Export3DModel: the model extent is zero; pass fixedScaleInMMperNm.");

            return viewer.ExportModel(filename, use3mf, scale,
                includeAtoms, includeBonds, includePolyhedra, polyhedraAsEdges, polyEdgeDiaInMM / 2 / scale,
                includeCellEdges, cellEdgeDiaInMM / 2 / scale, thickenBondsToMM > 0, thickenBondsToMM / 2 / scale, snaps, lineSnaps);
        }
    }
    #endregion

    #region ImageSimulatorクラス (HRTEMandSTEMとPotentialの親クラス)

    public abstract class ImageSimulationClass(Macro _p, FormImageSimulator.ImageModes mode) : MacroSub(_p.main)
    {
        //internal readonly Macro p = _p;
        internal FormImageSimulator sim => _p.main.FormImageSimulator;

        internal FormImageSimulator.ImageModes Mode = mode;

        // (260414Ch) Help text revised and corrected for actual exposed units.
        [Help("Gets or sets the electron accelerating voltage in kV.")]
        public double AccVol { get => sim.AccVol; set => sim.AccVol = value; }

        [Help("Gets or sets the maximum number of diffracted waves (Bloch waves) used in dynamical scattering calculations.")]
        public int NumberOfDiffractedWaves { get => sim.BlochNum; set => sim.BlochNum = value; }

        [Help("Gets or sets the simulated image width in pixels.")]
        public int ImageWidth { get => sim.ImageSize.Width; set => sim.ImageSize = new Size(value, sim.ImageSize.Height); }

        [Help("Gets or sets the simulated image height in pixels.")]
        public int ImageHeight { get => sim.ImageSize.Height; set => sim.ImageSize = new Size(sim.ImageSize.Width, value); }

        [Help("Sets the simulated image size in pixels.", "int width, int height")]
        public void ImageSize(int width, int height) => sim.ImageSize = new Size(width, height);

        [Help("Gets or sets the simulated image resolution in nm/pixel.")]
        public double ImageResolution { get => sim.ImageResolution; set => sim.ImageResolution = value; }

        [Help("Gets or sets whether the unit cell is displayed.")]
        public bool UnitCellVisible { get => sim.UnitCellVisible; set => sim.UnitCellVisible = value; }

        [Help($"Gets or sets whether the image label is displayed.")]
        public bool LabelVisible { get => sim.LabelVisible; set => sim.LabelVisible = value; }

        [Help($"Gets or sets the label font size.")]
        public int LabelSize { get => sim.LabelSize; set => sim.LabelSize = value; }

        [Help($"Gets or sets whether the scale bar is displayed.")]
        public bool ScaleBarVisible { get => sim.ScaleBarVisible; set => sim.ScaleBarVisible = value; }

        [Help($"Gets or sets the scale bar length in nm.")]
        public double ScaleBarLength { get => sim.ScaleBarLength; set => sim.ScaleBarLength = value; }

        [Help($"Gets or sets whether Gaussian blur is applied.")]
        public bool GaussianBlurEnabled { get => sim.GaussianBlurEnabled; set => sim.GaussianBlurEnabled = value; }

        [Help($"Gets or sets the Gaussian blur FWHM in pm.")]
        public double GaussianBlurFWHM { get => sim.GaussianBlurFWHM; set => sim.GaussianBlurFWHM = value; }

        [Help("Opens the ### simulator window.")]
        public void Open() { sim.Visible = true; sim.ImageMode = Mode; }

        [Help("Closes the ### simulator window.")]
        public void Close() => sim.Visible = false;

        [Help("Runs the ### simulation with the current settings.")]
        public void Simulate() { Open(); sim.Simulate(true); }

        [Help("Gets or sets whether the unit cell, labels, and scale bar are overprinted on saved images.")]
        public bool OverprintSymbols { get => sim.OverprintSymbols; set => sim.OverprintSymbols = value; }

        [Help("Gets or sets whether each image is saved separately in serial-image mode.")]
        public bool SaveIndividually { get => sim.SaveIndividually; set => sim.SaveIndividually = value; }

        [Help("Saves the simulated image as a PNG file. If 'filename' is omitted, opens a save dialog.", "string filename")]//260805Cl: Argument 追加 (引数を取るのにヘルプが SaveImageAsPng() と表示していた)
        public void SaveImageAsPng(string filename = null) => sim.Save(FormImageSimulator.FormatEnum.PNG, FormImageSimulator.ActionEnum.Save, filename);
        
        [Help("Saves the simulated image as a TIFF file. If 'filename' is omitted, opens a save dialog.", "string filename")]//260805Cl: Argument 追加 
        public void SaveImageAsTif(string filename = null) => sim.Save(FormImageSimulator.FormatEnum.TIFF, FormImageSimulator.ActionEnum.Save, filename);

        [Help("Saves the simulated image as an EMF file. If 'filename' is omitted, opens a save dialog.", "string filename")]//260805Cl: Argument 追加
        public void SaveImageAsEmf(string filename = null) => sim.Save(FormImageSimulator.FormatEnum.Meta, FormImageSimulator.ActionEnum.Save, filename);

    }

    #endregion

    #region STEMとHRTEMの親クラスであり、ImageSimulatorの子クラス。
    /// <summary>STEMとHRTEMの親クラスであり、ImageSimulatorの子クラス。</summary>
    /// <param name="_p"></param>
    /// <param name="mode"></param>
    public class STEMandHRTEM_class(Macro _p, FormImageSimulator.ImageModes mode) : ImageSimulationClass(_p, mode)
    {
        // (260414Ch) Help text revised and corrected where the public API exposed different units than the old text claimed.
        [Help("Gets or sets the specimen thickness in nm.")]
        public double Thickness { get => sim.Thickness; set => sim.Thickness = value; }

        [Help("Gets or sets the defocus in nm.")]
        public double Defocus { get => sim.Defocus; set => sim.Defocus = value; }

        [Help("Gets or sets the spherical aberration coefficient Cs in mm.")]
        public double Cs { get => sim.Cs * 1E-6; set => sim.Cs = value * 1E6; }

        [Help("Gets or sets the chromatic aberration coefficient Cc in mm.")]
        public double Cc { get => sim.Cc * 1E-6; set => sim.Cc = value * 1E6; }

        [Help("Gets or sets the energy spread ΔV as FWHM in eV.")]
        public double DeltaV { get => sim.DeltaVolFWHM * 1000.0; set => sim.DeltaVolFWHM = value / 1000.0; }

        [Help("Gets the Scherzer defocus in nm.")]
        public double Scherzer => sim.Scherzer;

        [Help("Switches to single-image mode.")]
        public void SingleImageMode() => sim.SingleImageMode = true;

        [Help("Switches to serial-image mode and chooses whether thickness and defocus are varied.", "bool withThickness, bool withDefocus")]
        public void SerialImageMode(bool withThickness, bool withDefocus)
        {
            sim.SerialImageMode = true;
            sim.SerialImageWithThickness = withThickness;
            sim.SerialImageWithDefocus = withDefocus;
        }

        [Help("Gets or sets the starting thickness in serial-image mode, in nm.")]
        public double SerialImageThicknessStart { get => sim.SerialImageThicknessStart; set => sim.SerialImageThicknessStart = value; }

        [Help("Gets or sets the thickness step in serial-image mode, in nm.")]
        public double SerialImageThicknessStep { get => sim.SerialImageThicknessStep; set => sim.SerialImageThicknessStep = value; }

        [Help("Gets or sets the number of thickness values in serial-image mode.")]
        public int SerialImageThicknessNum { get => sim.SerialImageThicknessNum; set => sim.SerialImageThicknessNum = value; }

        [Help("Gets or sets the starting defocus in serial-image mode, in nm.")]
        public double SerialImageDefocusStart { get => sim.SerialImageDefocusStart; set => sim.SerialImageDefocusStart = value; }

        [Help("Gets or sets the defocus step in serial-image mode, in nm.")]
        public double SerialImageDefocusStep { get => sim.SerialImageDefocusStep; set => sim.SerialImageDefocusStep = value; }

        // public int SerialImageDefocusNum { get => sim.SerialImageDefocusNum; set => sim.SerialImageDefocusStep = value; } // (260414Ch) 旧実装: setter が個数ではなく step を書き換えていた
        [Help("Gets or sets the number of defocus values in serial-image mode.")]
        public int SerialImageDefocusNum { get => sim.SerialImageDefocusNum; set => sim.SerialImageDefocusNum = value; }
    }
    #endregion

    #region STEMクラス
    public class STEMClass(Macro _p) : STEMandHRTEM_class(_p, FormImageSimulator.ImageModes.STEM)
    {
        // (260414Ch) Help text revised for terminology and unit consistency.
        [Help("Gets or sets the convergence semi-angle in mrad.")]
        public double ConvergenceAngle { get => sim.STEM_ConvergenceAngle * 1000; set => sim.STEM_ConvergenceAngle = value / 1000; }

        [Help("Gets or sets the inner semi-angle of the annular detector in mrad.")]
        public double DetectorInnerAngle { get => sim.STEM_DetectorInnerAngle * 1000; set => sim.STEM_DetectorInnerAngle = value / 1000; }
        
        [Help("Gets or sets the outer semi-angle of the annular detector in mrad.")]
        public double DetectorOuterAngle { get => sim.STEM_DetectorOuterAngle * 1000; set => sim.STEM_DetectorOuterAngle = value / 1000; }

        [Help("Gets or sets the effective source size as FWHM in pm.")]
        public double EffectiveSourceSize { get => sim.STEM_SourceSizeFWHM * 1000; set => sim.STEM_SourceSizeFWHM = value / 1000; }
        
        [Help("Gets or sets the angular resolution of the convergent beam in mrad.")]
        public double AngularResolution { get => sim.STEM_AngularResolution * 1000; set => sim.STEM_AngularResolution = value / 1000; }

        [Help("Gets or sets the slice thickness for TDS calculations in nm.")] 
        public double SliceThickness { get => sim.STEM_SliceThickness; set => sim.STEM_SliceThickness = value; }

        [Help("Displays both the elastic and TDS (inelastic) components.")]
        public void DisplayBoth() => sim.STEM_Mode = FormImageSimulator.STEM_ModeEnum.Both;
        
        [Help("Displays only the elastic component.")]
        public void DisplayElastic() => sim.STEM_Mode = FormImageSimulator.STEM_ModeEnum.Elastic;
        
        [Help("Displays only the TDS (inelastic) component.")] 
        public void DisplayTDS() => sim.STEM_Mode = FormImageSimulator.STEM_ModeEnum.TDS;
    }
    #endregion

    #region HRTEMクラス
    public class HRTEMClass(Macro _p) : STEMandHRTEM_class(_p, FormImageSimulator.ImageModes.HRTEM)
    {
        // (260414Ch) Help text corrected to the actual public units (radians, not mrad).
        [Help("Gets or sets the illumination semi-angle beta in radians.")]
        public double Beta { get => sim.HRTEM_Beta; set => sim.HRTEM_Beta = value; }
   
        [Help("Gets or sets the objective-aperture semi-angle in radians.")]
        public double ApertureSemiangle { get => sim.HRTEM_ObjAperRadius; set => sim.HRTEM_ObjAperRadius = value; }
        
        [Help("Gets or sets the x shift of the objective aperture in radians.")]
        public double ApertureShiftX { get => sim.HRTEM_ObjAperX; set => sim.HRTEM_ObjAperX = value; }

        [Help("Gets or sets the y shift of the objective aperture in radians.")]
        public double ApertureShiftY { get => sim.HRTEM_ObjAperY; set => sim.HRTEM_ObjAperY = value; }

        [Help("Gets or sets whether the objective aperture is open.")]
        public bool OpenAperture { get => sim.HRTEM_OpenObjAper; set => sim.HRTEM_OpenObjAper = value; }

        [Help("Uses the linear image model for partial-coherency calculations.")]
        public void Mode_LinearImage() => sim.HRTEM_Mode = FormImageSimulator.HRTEM_Modes.Quasi;

        [Help("Uses the TCC (transmission cross coefficient) model for partial-coherency calculations.")]
        public void Mode_TCC() => sim.HRTEM_Mode = FormImageSimulator.HRTEM_Modes.TCC;
    }
    #endregion

    #region Potentialクラス
    public class PotentialClass : ImageSimulationClass
    {
        public PotentialClass(Macro _p) : base(_p, FormImageSimulator.ImageModes.POTENTIAL)
        {
        }



    }

    #endregion

}
