using Crystallography.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using V3 = OpenTK.Mathematics.Vector3d;

namespace ReciPro;

//260803Cl 追加: 3Dプリント用モデルエクスポートの設定ダイアログ。
//260804Cl 全面改稿: オプション選択フォーム化 (旧版 = 最大寸法+枠のみ、commit 9f5c3510 参照)。
// - スケール: 最大寸法指定 ⇔ 固定スケール (mm/Å) 指定 (複数模型を同縮尺で作る用途)
// - 含める要素: 原子 / 結合 / 配位多面体 (表示中でも印刷から除外できる)
// - 多面体スタイル: 面 (ソリッド) / 稜線のみ (円柱+頂点球。中の原子が見える)
// - 単位胞の辺の円柱化 + 径
// - 印刷適性: 細すぎる結合を指定径まで自動増径 (元プリミティブから再生成)
// - 出力形式: STL (単色) / 3MF (元素ごと色分け)
//残る拡張予定: プリンタプロファイル・浮遊部品検出・銘板 (.project-guidance/ReciPro/ReciPro_3Dプリント出力設計.md 参照)。
//260805Cl 修正: 長さの単位表記を Å → nm に訂正 (GLObject の座標は ReciPro 内部標準の nm。
//  spinel 1 単位胞が "1.09 Å" と表示されていた)。出力ジオメトリの寸法は元から正しいので変えていない。
public partial class FormExport3DModel : FormBase
{
    private readonly bool hasAtoms, hasBonds, hasPoly, hasLines;
    private readonly (V3 Min, V3 Max) boundsAtoms, boundsBonds, boundsPoly, boundsLines;
    //private readonly double minBondRadiusAngSrc;//260805Cl 旧 (単位は Å ではなく nm)
    private readonly double minBondRadiusNmSrc;//表示中の最小の結合 (円柱) 半径 (nm)
    private bool updating = false;

    //public double MmPerAngstrom { get; private set; } = 1;//260805Cl 旧
    /// <summary>選択されたスケール (mm/nm)</summary>
    public double MmPerNm { get; private set; } = 1;

    public bool IncludeAtoms => checkBoxAtoms.Checked && hasAtoms;
    public bool IncludeBonds => checkBoxBonds.Checked && hasBonds;
    public bool IncludePolyhedra => checkBoxPolyhedra.Checked && hasPoly;

    /// <summary>多面体を面ではなく稜線枠 (円柱+頂点球) で出力するか</summary>
    public bool PolyhedraAsEdges => radioButtonPolyEdges.Checked;

    /// <summary>多面体を透かし格子 (稜線枠+面内メッシュバー) で出力するか (260805Cl 追加)</summary>
    public bool PolyhedraAsMesh => radioButtonPolyMesh.Checked;

    /// <summary>多面体稜線・メッシュバーの円柱半径 (nm)</summary>
    public double PolyEdgeRadiusNm => (double)numericUpDownPolyEdgeDia.Value / 2 / MmPerNm;

    /// <summary>透かし格子のピッチ (nm) (260805Cl 追加)</summary>
    public double PolyPitchNm => (double)numericUpDownPolyPitch.Value / MmPerNm;

    /// <summary>単位胞枠 (線オブジェクト) を円柱化して含めるか</summary>
    public bool IncludeCellEdges => checkBoxCellEdges.Checked && hasLines;

    /// <summary>円柱化する枠の半径 (nm)</summary>
    public double EdgeRadiusNm => (double)numericUpDownEdgeDia.Value / 2 / MmPerNm;

    /// <summary>細い結合を最小径まで増径するか</summary>
    public bool ThickenBonds => checkBoxThicken.Checked && hasBonds;

    /// <summary>増径後の最小結合半径 (nm)</summary>
    public double MinBondRadiusNm => (double)numericUpDownMinBond.Value / 2 / MmPerNm;

    /// <summary>3MF (色分け) で出力するか。false なら STL (単色)</summary>
    public bool Use3mf => radioButton3mf.Checked;

    /// <summary>ダイアログ表示用に収集済みスナップショットを受け取る</summary>
    /// <param name="solids">ModelExporter.Collect() の結果 (閉じた立体)</param>
    /// <param name="lines">ModelExporter.CollectLines() の結果 (単位胞枠などの線)</param>
    public FormExport3DModel(List<MeshSnapshot> solids, List<MeshSnapshot> lines)
    {
        InitializeComponent();
        HelpPage = "5-structure-viewer"; //260805Cl 追加: 未設定だと F1 がマニュアルのトップに飛ぶ (タイトルには "(F1: Help)" が出ている)

        static bool isAtom(MeshSnapshot s) => s.Kind is SnapshotKind.Sphere or SnapshotKind.Ellipsoid;
        static bool isPoly(MeshSnapshot s) => s.Kind == SnapshotKind.Polyhedron;
        var atoms = solids.Where(isAtom).ToList();
        var poly = solids.Where(isPoly).ToList();
        var bonds = solids.Where(s => !isAtom(s) && !isPoly(s)).ToList();//円柱・円錐・トーラスなど棒状のもの
        hasAtoms = atoms.Count > 0;
        hasBonds = bonds.Count > 0;
        hasPoly = poly.Count > 0;
        hasLines = lines.Count > 0;
        boundsAtoms = ModelExporter.GetBounds(atoms);
        boundsBonds = ModelExporter.GetBounds(bonds);
        boundsPoly = ModelExporter.GetBounds(poly);
        V3 min = new(double.MaxValue), max = new(double.MinValue);
        foreach (var (s, t) in lines.SelectMany(l => l.Segments))
        {
            min = V3.ComponentMin(V3.ComponentMin(min, s), t);
            max = V3.ComponentMax(V3.ComponentMax(max, s), t);
        }
        boundsLines = (min, max);
        var bondRadii = bonds.Where(s => s.Kind == SnapshotKind.Cylinder && s.PipeRadius1 > 0).Select(s => s.PipeRadius1);
        minBondRadiusNmSrc = bondRadii.Any() ? bondRadii.Min() : 0;

        //260805Cl 変更: 実行時に組み立てるラベルは Localization.Loc で多言語化する (Designer 直書きの静的ラベルは
        //LocalizationData の reg["FormExport3DModel"] 側)。旧: $"Objects: {..},  Triangles: {..}"
        labelInfo.Text =
            $"{Localization.Loc(en: "Objects", ja: "オブジェクト", de: "Objekte", fr: "Objets", es: "Objetos", pt: "Objetos", it: "Oggetti", ru: "Объекты", zhHans: "对象", zhHant: "物件", ko: "개체")}: {solids.Count:n0},  " +
            $"{Localization.Loc(en: "Triangles", ja: "三角形", de: "Dreiecke", fr: "Triangles", es: "Triángulos", pt: "Triângulos", it: "Triangoli", ru: "Треугольники", zhHans: "三角形", zhHant: "三角形", ko: "삼각형")}: {solids.Sum(s => s.Triangles.Length / 3):n0}";
        updating = true;
        checkBoxAtoms.Enabled = checkBoxAtoms.Checked = hasAtoms;
        checkBoxBonds.Enabled = checkBoxBonds.Checked = hasBonds;
        checkBoxPolyhedra.Enabled = checkBoxPolyhedra.Checked = hasPoly;
        checkBoxCellEdges.Enabled = checkBoxCellEdges.Checked = hasLines;
        checkBoxThicken.Enabled = hasBonds;
        updating = false;
        update(null, null);
    }

    /// <summary>チェック中の要素を合わせたバウンディングボックス寸法 (nm)</summary>
    private V3 SizeNm
    {
        get
        {
            V3 min = new(double.MaxValue), max = new(double.MinValue);
            void merge((V3 Min, V3 Max) b) { min = V3.ComponentMin(min, b.Min); max = V3.ComponentMax(max, b.Max); }
            if (IncludeAtoms) merge(boundsAtoms);
            if (IncludeBonds) merge(boundsBonds);
            if (IncludePolyhedra) merge(boundsPoly);
            if (IncludeCellEdges) merge(boundsLines);
            return min.X > max.X ? new V3(0) : max - min;
        }
    }

    private void update(object sender, EventArgs e)
    {
        if (updating) return;
        updating = true;

        var size = SizeNm;
        var maxExtent = Math.Max(size.X, Math.Max(size.Y, size.Z));
        MmPerNm = radioButtonFit.Checked
            ? (maxExtent > 0 ? (double)numericUpDownMaxSize.Value / maxExtent : 1)
            : (double)numericUpDownScale.Value;
        //固定スケールに切り替えた瞬間は直前の実効スケールを初期値にする (寸法指定からの連続性)
        if (sender == radioButtonScale && radioButtonScale.Checked)
            numericUpDownScale.Value = Math.Clamp((decimal)Math.Round(MmPerNm, 3), numericUpDownScale.Minimum, numericUpDownScale.Maximum);

        numericUpDownMaxSize.Enabled = radioButtonFit.Checked;
        numericUpDownScale.Enabled = radioButtonScale.Checked;
        radioButtonPolySolid.Enabled = radioButtonPolyEdges.Enabled = radioButtonPolyMesh.Enabled = IncludePolyhedra; //260805Cl 変更: mesh 追加
        numericUpDownPolyEdgeDia.Enabled = IncludePolyhedra && (radioButtonPolyEdges.Checked || radioButtonPolyMesh.Checked);
        numericUpDownPolyPitch.Enabled = IncludePolyhedra && radioButtonPolyMesh.Checked; //260805Cl 追加
        numericUpDownEdgeDia.Enabled = IncludeCellEdges;
        numericUpDownMinBond.Enabled = ThickenBonds;

        //260805Cl 旧 (単位表記が Å だが実体は nm):
        //labelSizeAng.Text = $"Model size: {size.X:f2} × {size.Y:f2} × {size.Z:f2} Å";
        //labelResult.Text = $"Scale: {MmPerAngstrom:f3} mm/Å,   Output size: " + ...
        //260805Cl 変更: 実行時ラベルを Localization.Loc で多言語化 (旧は英語直書き)
        labelSizeAng.Text =
            $"{Localization.Loc(en: "Model size", ja: "モデル寸法", de: "Modellgröße", fr: "Taille du modèle", es: "Tamaño del modelo", pt: "Tamanho do modelo", it: "Dimensioni modello", ru: "Размер модели", zhHans: "模型尺寸", zhHant: "模型尺寸", ko: "모델 크기")}: " +
            $"{size.X:f3} × {size.Y:f3} × {size.Z:f3} nm";
        labelResult.Text =
            $"{Localization.Loc(en: "Scale", ja: "スケール", de: "Maßstab", fr: "Échelle", es: "Escala", pt: "Escala", it: "Scala", ru: "Масштаб", zhHans: "比例", zhHant: "比例尺", ko: "배율")}: {MmPerNm:f3} mm/nm,   " +
            //260805Cl: es/pt/it/ru はレイアウト実測 (ハーネス) で groupBox 幅を超えたため短縮形にする
            $"{Localization.Loc(en: "Output size", ja: "出力寸法", de: "Ausgabegröße", fr: "Taille de sortie", es: "Tam. salida", pt: "Tam. saída", it: "Dim. output", ru: "Вывод", zhHans: "输出尺寸", zhHant: "輸出尺寸", ko: "출력 크기")}: " +
            $"{size.X * MmPerNm:f1} × {size.Y * MmPerNm:f1} × {size.Z * MmPerNm:f1} mm";

        //印刷適性チェック簡易版: 細すぎる結合の警告 (増径オプションが ON なら解消されるので出さない)
        var minDia = 2 * minBondRadiusNmSrc * MmPerNm;
        if (IncludeBonds && !ThickenBonds && minBondRadiusNmSrc > 0 && minDia < (double)numericUpDownMinBond.Value)
            labelWarning.Text = string.Format(Localization.Loc(
                en: "⚠ Thinnest bond ≈ {0} mm: may break easily. Enable thickening, increase the size, or increase the bond radius.",
                ja: "⚠ 最も細い結合が約 {0} mm: 折れやすくなります。太らせるか、寸法か結合半径を大きくしてください。",
                de: "⚠ Dünnste Bindung ≈ {0} mm: bricht leicht. Verdicken aktivieren oder Größe bzw. Bindungsradius erhöhen.",
                fr: "⚠ Liaison la plus fine ≈ {0} mm : risque de casse. Activez l'épaississement ou augmentez la taille ou le rayon.",
                es: "⚠ Enlace más fino ≈ {0} mm: puede romperse. Active el engrosado o aumente el tamaño o el radio del enlace.",
                pt: "⚠ Ligação mais fina ≈ {0} mm: pode quebrar. Ative o engrossamento ou aumente o tamanho ou o raio.",
                it: "⚠ Legame più sottile ≈ {0} mm: può rompersi. Attiva l'ispessimento o aumenta dimensione o raggio.",
                ru: "⚠ Самая тонкая связь ≈ {0} mm: легко ломается. Включите утолщение или увеличьте размер либо радиус.",
                zhHans: "⚠ 最细的键约 {0} mm: 易折断。请启用加粗，或增大尺寸或键半径。",
                zhHant: "⚠ 最細的鍵約 {0} mm：易折斷。請啟用加粗，或增大尺寸或鍵半徑。",
                ko: "⚠ 가장 가는 결합이 약 {0} mm: 부러지기 쉽습니다. 굵게 하기를 켜거나 크기 또는 결합 반지름을 늘리세요."),
                minDia.ToString("f2"));
        else
            labelWarning.Text = "";

        buttonOK.Enabled = IncludeAtoms || IncludeBonds || IncludePolyhedra || IncludeCellEdges;
        updating = false;
    }
}
