#region using
using Crystallography.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using V3 = OpenTK.Mathematics.Vector3d;
#endregion

namespace ReciPro;

//260803Cl 追加: 3Dプリント用モデルエクスポート (Phase 0: バイナリSTL単色出力)。
//設計の全体像は .project-guidance/ReciPro/ReciPro_3Dプリント出力設計.md を参照。
//Phase 1 で 3MF (元素別カラー) 出力・印刷適性チェックをここに追加する予定。

public static class ModelExporter
{
    /// <summary>閉じた立体としてエクスポート対象にする種別 (線・文字・開いた面は 3D プリントできないため除外)</summary>
    public static bool IsSolid(MeshSnapshot s)
        => s.Kind is SnapshotKind.Sphere or SnapshotKind.Ellipsoid or SnapshotKind.Cylinder
                  or SnapshotKind.Cone or SnapshotKind.Pipe or SnapshotKind.Polyhedron or SnapshotKind.Torus;

    /// <summary>表示中 (Rendered) の閉じた立体のスナップショットを収集する</summary>
    public static List<MeshSnapshot> Collect(IEnumerable<GLObject> objects)
        => objects.Where(o => o.Rendered).Select(MeshSnapshot.From)
                  .Where(s => IsSolid(s) && s.Triangles.Length > 0).ToList();

    /// <summary>全三角形のバウンディングボックス (ワールド座標 = Å)</summary>
    public static (V3 Min, V3 Max) GetBounds(IEnumerable<MeshSnapshot> snaps)
    {
        V3 min = new(double.MaxValue), max = new(double.MinValue);
        foreach (var s in snaps)
            foreach (var p in s.Triangles)
            {
                min = V3.ComponentMin(min, p);
                max = V3.ComponentMax(max, p);
            }
        return (min, max);
    }

    /// <summary>
    /// バイナリ STL を書き出す。座標は Å × scale で mm に変換し、XY 中心が原点、Z 最小値が 0 (ビルドプレート面) に来るよう平行移動する。
    /// 面の向きは、凸形状 (球・楕円球・円柱・円錐・多面体) では「物体中心から外向き」に揃える (凸体では厳密に正しい判定)。
    /// 法線は頂点の巻き順から再計算し、縮退三角形 (面積ゼロ) は出力しない。
    /// </summary>
    /// <param name="path">出力ファイルパス</param>
    /// <param name="snaps">Collect() で収集したスナップショット</param>
    /// <param name="scale">スケール (mm/Å)</param>
    /// <returns>書き出した三角形数</returns>
    public static int ExportStl(string path, List<MeshSnapshot> snaps, double scale)
    {
        var (min, max) = GetBounds(snaps);
        var shift = new V3((min.X + max.X) / 2, (min.Y + max.Y) / 2, min.Z);

        var tris = new List<(V3 A, V3 B, V3 C, V3 N)>(snaps.Sum(s => s.Triangles.Length / 3));
        foreach (var s in snaps)
        {
            //Torus は非凸なので中心基準の向き判定が使えない (生成時の巻き順を信頼する)
            var convex = s.Kind != SnapshotKind.Torus;
            var center = (s.Center - shift) * scale;
            for (int i = 0; i < s.Triangles.Length; i += 3)
            {
                V3 a = (s.Triangles[i] - shift) * scale,
                   b = (s.Triangles[i + 1] - shift) * scale,
                   c = (s.Triangles[i + 2] - shift) * scale;
                var n = V3.Cross(b - a, c - a);
                var len = n.Length;
                if (len < 1E-12)
                    continue;//縮退三角形は出力しない
                n /= len;
                if (convex && V3.Dot(n, (a + b + c) / 3 - center) < 0)
                {
                    (b, c) = (c, b);
                    n = -n;
                }
                tris.Add((a, b, c, n));
            }
        }

        using var bw = new BinaryWriter(File.Create(path));
        var header = new byte[80];
        Encoding.ASCII.GetBytes("ReciPro binary STL (unit: mm)").CopyTo(header, 0);
        bw.Write(header);
        bw.Write((uint)tris.Count);
        foreach (var (a, b, c, n) in tris)
        {
            write(n); write(a); write(b); write(c);
            bw.Write((ushort)0);//attribute byte count
        }
        return tris.Count;

        void write(in V3 v) { bw.Write((float)v.X); bw.Write((float)v.Y); bw.Write((float)v.Z); }
    }
}
