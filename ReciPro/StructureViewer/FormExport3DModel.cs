using Crystallography.Controls;
using System;
using V3 = OpenTK.Mathematics.Vector3d;

namespace ReciPro;

//260803Cl 追加: 3Dプリント用モデルエクスポートの設定ダイアログ (Phase 0: 最大寸法によるスケール指定)。
//260803Cl 変更 (Phase 1): 単位胞枠の円柱化オプションと細すぎる結合の警告表示を追加。
//残る拡張予定: プリンタプロファイル・浮遊部品検出・銘板 (.project-guidance/ReciPro/ReciPro_3Dプリント出力設計.md 参照)。
public partial class FormExport3DModel : FormBase
{
    private readonly V3 sizeSolids, sizeWithLines;
    private readonly double minBondRadiusAng;

    /// <summary>3×ノズル径 (0.4mm) = FDM で折れやすくなる結合径の下限 (mm)。プリンタプロファイル導入までの固定値</summary>
    private const double MinBondDiameterMm = 1.2;

    /// <summary>選択されたスケール (mm/Å)</summary>
    public double MmPerAngstrom { get; private set; } = 1;

    /// <summary>単位胞枠 (線オブジェクト) を円柱化して含めるか</summary>
    public bool IncludeCellEdges => checkBoxCellEdges.Checked;

    /// <summary>円柱化する枠の半径 (Å)</summary>
    public double EdgeRadiusAng => (double)numericUpDownEdgeDia.Value / 2 / MmPerAngstrom;

    //260803Cl 旧シグネチャ: public FormExport3DModel(int objectCount, int triangleCount, V3 sizeAng)
    /// <param name="objectCount">対象ソリッド数</param>
    /// <param name="triangleCount">対象三角形数</param>
    /// <param name="sizeSolids">ソリッドのみのバウンディングボックス寸法 (Å)</param>
    /// <param name="sizeWithLines">線オブジェクトの端点も含めた寸法 (Å)</param>
    /// <param name="hasLines">表示中の線オブジェクト (単位胞枠など) が存在するか</param>
    /// <param name="minBondRadiusAng">最小の結合円柱半径 (Å)。0 以下なら警告判定をしない</param>
    public FormExport3DModel(int objectCount, int triangleCount, V3 sizeSolids, V3 sizeWithLines, bool hasLines, double minBondRadiusAng)
    {
        InitializeComponent();
        this.sizeSolids = sizeSolids;
        this.sizeWithLines = sizeWithLines;
        this.minBondRadiusAng = minBondRadiusAng;
        labelInfo.Text = $"Objects: {objectCount:n0},  Triangles: {triangleCount:n0}";
        checkBoxCellEdges.Enabled = hasLines;
        checkBoxCellEdges.Checked = hasLines;
        update(null, null);
    }

    private V3 SizeAng => IncludeCellEdges ? sizeWithLines : sizeSolids;

    private void update(object sender, EventArgs e)
    {
        var size = SizeAng;
        var maxExtent = Math.Max(size.X, Math.Max(size.Y, size.Z));
        MmPerAngstrom = maxExtent > 0 ? (double)numericUpDownMaxSize.Value / maxExtent : 1;
        numericUpDownEdgeDia.Enabled = checkBoxCellEdges.Checked;
        labelSizeAng.Text = $"Model size: {size.X:f2} × {size.Y:f2} × {size.Z:f2} Å";
        labelResult.Text = $"Scale: {MmPerAngstrom:f3} mm/Å,   Output size: " +
            $"{size.X * MmPerAngstrom:f1} × {size.Y * MmPerAngstrom:f1} × {size.Z * MmPerAngstrom:f1} mm";

        //印刷適性チェック簡易版: 細すぎる結合の警告 (3×ノズル径未満は折れやすい)
        if (minBondRadiusAng > 0 && 2 * minBondRadiusAng * MmPerAngstrom < MinBondDiameterMm)
            labelWarning.Text = $"⚠ Thinnest bond ≈ {2 * minBondRadiusAng * MmPerAngstrom:f2} mm (< {MinBondDiameterMm} mm): " +
                "may break easily. Increase the model size or the bond radius.";
        else
            labelWarning.Text = "";
    }
}
