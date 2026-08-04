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
public partial class FormExport3DModel : FormBase
{
    private readonly bool hasAtoms, hasBonds, hasPoly, hasLines;
    private readonly (V3 Min, V3 Max) boundsAtoms, boundsBonds, boundsPoly, boundsLines;
    private readonly double minBondRadiusAngSrc;//表示中の最小の結合 (円柱) 半径 (Å)
    private bool updating = false;

    /// <summary>選択されたスケール (mm/Å)</summary>
    public double MmPerAngstrom { get; private set; } = 1;

    public bool IncludeAtoms => checkBoxAtoms.Checked && hasAtoms;
    public bool IncludeBonds => checkBoxBonds.Checked && hasBonds;
    public bool IncludePolyhedra => checkBoxPolyhedra.Checked && hasPoly;

    /// <summary>多面体を面ではなく稜線枠 (円柱+頂点球) で出力するか</summary>
    public bool PolyhedraAsEdges => radioButtonPolyEdges.Checked;

    /// <summary>多面体稜線の円柱半径 (Å)</summary>
    public double PolyEdgeRadiusAng => (double)numericUpDownPolyEdgeDia.Value / 2 / MmPerAngstrom;

    /// <summary>単位胞枠 (線オブジェクト) を円柱化して含めるか</summary>
    public bool IncludeCellEdges => checkBoxCellEdges.Checked && hasLines;

    /// <summary>円柱化する枠の半径 (Å)</summary>
    public double EdgeRadiusAng => (double)numericUpDownEdgeDia.Value / 2 / MmPerAngstrom;

    /// <summary>細い結合を最小径まで増径するか</summary>
    public bool ThickenBonds => checkBoxThicken.Checked && hasBonds;

    /// <summary>増径後の最小結合半径 (Å)</summary>
    public double MinBondRadiusAng => (double)numericUpDownMinBond.Value / 2 / MmPerAngstrom;

    /// <summary>3MF (色分け) で出力するか。false なら STL (単色)</summary>
    public bool Use3mf => radioButton3mf.Checked;

    /// <summary>ダイアログ表示用に収集済みスナップショットを受け取る</summary>
    /// <param name="solids">ModelExporter.Collect() の結果 (閉じた立体)</param>
    /// <param name="lines">ModelExporter.CollectLines() の結果 (単位胞枠などの線)</param>
    public FormExport3DModel(List<MeshSnapshot> solids, List<MeshSnapshot> lines)
    {
        InitializeComponent();

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
        minBondRadiusAngSrc = bondRadii.Any() ? bondRadii.Min() : 0;

        labelInfo.Text = $"Objects: {solids.Count:n0},  Triangles: {solids.Sum(s => s.Triangles.Length / 3):n0}";
        updating = true;
        checkBoxAtoms.Enabled = checkBoxAtoms.Checked = hasAtoms;
        checkBoxBonds.Enabled = checkBoxBonds.Checked = hasBonds;
        checkBoxPolyhedra.Enabled = checkBoxPolyhedra.Checked = hasPoly;
        checkBoxCellEdges.Enabled = checkBoxCellEdges.Checked = hasLines;
        checkBoxThicken.Enabled = hasBonds;
        updating = false;
        update(null, null);
    }

    /// <summary>チェック中の要素を合わせたバウンディングボックス寸法 (Å)</summary>
    private V3 SizeAng
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

        var size = SizeAng;
        var maxExtent = Math.Max(size.X, Math.Max(size.Y, size.Z));
        MmPerAngstrom = radioButtonFit.Checked
            ? (maxExtent > 0 ? (double)numericUpDownMaxSize.Value / maxExtent : 1)
            : (double)numericUpDownScale.Value;
        //固定スケールに切り替えた瞬間は直前の実効スケールを初期値にする (寸法指定からの連続性)
        if (sender == radioButtonScale && radioButtonScale.Checked)
            numericUpDownScale.Value = Math.Clamp((decimal)Math.Round(MmPerAngstrom, 3), numericUpDownScale.Minimum, numericUpDownScale.Maximum);

        numericUpDownMaxSize.Enabled = radioButtonFit.Checked;
        numericUpDownScale.Enabled = radioButtonScale.Checked;
        radioButtonPolySolid.Enabled = radioButtonPolyEdges.Enabled = IncludePolyhedra;
        numericUpDownPolyEdgeDia.Enabled = IncludePolyhedra && radioButtonPolyEdges.Checked;
        numericUpDownEdgeDia.Enabled = IncludeCellEdges;
        numericUpDownMinBond.Enabled = ThickenBonds;

        labelSizeAng.Text = $"Model size: {size.X:f2} × {size.Y:f2} × {size.Z:f2} Å";
        labelResult.Text = $"Scale: {MmPerAngstrom:f3} mm/Å,   Output size: " +
            $"{size.X * MmPerAngstrom:f1} × {size.Y * MmPerAngstrom:f1} × {size.Z * MmPerAngstrom:f1} mm";

        //印刷適性チェック簡易版: 細すぎる結合の警告 (増径オプションが ON なら解消されるので出さない)
        var minDia = 2 * minBondRadiusAngSrc * MmPerAngstrom;
        if (IncludeBonds && !ThickenBonds && minBondRadiusAngSrc > 0 && minDia < (double)numericUpDownMinBond.Value)
            labelWarning.Text = $"⚠ Thinnest bond ≈ {minDia:f2} mm: may break easily. " +
                "Enable thickening, increase the size, or increase the bond radius.";
        else
            labelWarning.Text = "";

        buttonOK.Enabled = IncludeAtoms || IncludeBonds || IncludePolyhedra || IncludeCellEdges;
        updating = false;
    }
}
