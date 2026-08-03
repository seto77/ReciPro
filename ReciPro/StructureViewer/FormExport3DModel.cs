using Crystallography.Controls;
using System;
using V3 = OpenTK.Mathematics.Vector3d;

namespace ReciPro;

//260803Cl 追加: 3Dプリント用モデルエクスポートの設定ダイアログ (Phase 0: 最大寸法によるスケール指定のみ)。
//Phase 1 でプリンタプロファイル・出力要素選択・印刷適性チェック表示に拡張する予定
//(.project-guidance/ReciPro/ReciPro_3Dプリント出力設計.md 参照)。
public partial class FormExport3DModel : FormBase
{
    private readonly double maxExtentAng;
    private readonly V3 sizeAng;

    /// <summary>選択されたスケール (mm/Å)</summary>
    public double MmPerAngstrom { get; private set; } = 1;

    public FormExport3DModel(int objectCount, int triangleCount, V3 sizeAng)
    {
        InitializeComponent();
        this.sizeAng = sizeAng;
        maxExtentAng = Math.Max(sizeAng.X, Math.Max(sizeAng.Y, sizeAng.Z));
        labelInfo.Text = $"Objects: {objectCount:n0},  Triangles: {triangleCount:n0}";
        labelSizeAng.Text = $"Model size: {sizeAng.X:f2} × {sizeAng.Y:f2} × {sizeAng.Z:f2} Å";
        numericUpDownMaxSize_ValueChanged(null, null);
    }

    private void numericUpDownMaxSize_ValueChanged(object sender, EventArgs e)
    {
        MmPerAngstrom = maxExtentAng > 0 ? (double)numericUpDownMaxSize.Value / maxExtentAng : 1;
        labelResult.Text = $"Scale: {MmPerAngstrom:f3} mm/Å,   Output size: " +
            $"{sizeAng.X * MmPerAngstrom:f1} × {sizeAng.Y * MmPerAngstrom:f1} × {sizeAng.Z * MmPerAngstrom:f1} mm";
    }
}
