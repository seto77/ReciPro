#region using
using Crystallography.OpenGL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml;
using V3 = OpenTK.Mathematics.Vector3d;
#endregion

namespace ReciPro;

//260803Cl 追加: 3Dプリント用モデルエクスポート (Phase 0: バイナリSTL単色出力 / Phase 1: 3MF色分け出力)。
//設計の全体像は .project-guidance/ReciPro/ReciPro_3Dプリント出力設計.md を参照。

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

    /// <summary>表示中 (Rendered) の線オブジェクト (単位胞枠など) のスナップショットを収集する (260803Cl 追加)</summary>
    public static List<MeshSnapshot> CollectLines(IEnumerable<GLObject> objects)
        => objects.Where(o => o.Rendered).Select(MeshSnapshot.From)
                  .Where(s => s.Kind == SnapshotKind.Lines && s.Segments.Length > 0).ToList();

    /// <summary>
    /// 線分スナップショット (単位胞枠など) を円柱ソリッドへ変換する (260803Cl 追加, Phase 1)。
    /// 各線分を半径 radiusAng (Å) の円柱にし、端点 (重複除去済) には同半径の球を置いて角の継ぎ目を丸く埋める。
    /// </summary>
    public static List<MeshSnapshot> CylinderizeLines(IEnumerable<MeshSnapshot> lines, double radiusAng)
    {
        var result = new List<MeshSnapshot>();
        var cornerKeys = new HashSet<(long X, long Y, long Z)>();
        var corners = new List<(V3 Pos, int Argb)>();
        foreach (var s in lines)
        {
            var mat = new Material(s.Argb);
            foreach (var (start, end) in s.Segments)
            {
                result.Add(MeshSnapshot.From(new Cylinder(start, end - start, radiusAng, mat, DrawingMode.Surfaces)));
                foreach (var p in (V3[])[start, end])
                    if (cornerKeys.Add(((long)Math.Round(p.X * 1E4), (long)Math.Round(p.Y * 1E4), (long)Math.Round(p.Z * 1E4))))
                        corners.Add((p, s.Argb));
            }
        }
        foreach (var (pos, argb) in corners)
            result.Add(MeshSnapshot.From(new Sphere(pos, radiusAng, new Material(argb), DrawingMode.Surfaces)));
        return result;
    }

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

    /// <summary>XY 中心が原点、Z 最小値が 0 (ビルドプレート面) に来る平行移動量</summary>
    private static V3 GetShift(List<MeshSnapshot> snaps)
    {
        var (min, max) = GetBounds(snaps);
        return new V3((min.X + max.X) / 2, (min.Y + max.Y) / 2, min.Z);
    }

    /// <summary>
    /// 1 スナップショット分の三角形を mm 座標に変換し、外向きに揃えて返す。
    /// 面の向きは、凸形状 (球・楕円球・円柱・円錐・多面体) では「物体中心から外向き」に揃える (凸体では厳密に正しい判定)。
    /// Torus は非凸なので生成時の巻き順を信頼する。法線は巻き順から再計算し、縮退三角形 (面積ゼロ) は除く。
    /// </summary>
    private static IEnumerable<(V3 A, V3 B, V3 C, V3 N)> OrientedTriangles(MeshSnapshot s, V3 shift, double scale)
    {
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
            yield return (a, b, c, n);
        }
    }

    /// <summary>
    /// バイナリ STL (単色) を書き出す。座標は Å × scale で mm に変換し、XY 中心が原点、Z 最小値が 0 に来るよう平行移動する。
    /// </summary>
    /// <param name="path">出力ファイルパス</param>
    /// <param name="snaps">Collect() で収集したスナップショット</param>
    /// <param name="scale">スケール (mm/Å)</param>
    /// <returns>書き出した三角形数</returns>
    public static int ExportStl(string path, List<MeshSnapshot> snaps, double scale)
    {
        var shift = GetShift(snaps);
        var tris = new List<(V3 A, V3 B, V3 C, V3 N)>(snaps.Sum(s => s.Triangles.Length / 3));
        foreach (var s in snaps)
            tris.AddRange(OrientedTriangles(s, shift, scale));

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

    //260803Cl 追加 (Phase 1): 3MF 色分け出力。
    //構成は Codex 相談 (設計書 §5) の結論に従う:
    // - 最小構成 = [Content_Types].xml + _rels/.rels + 3D/3dmodel.model, unit="millimeter"
    // - 色は basematerials + object の pid/pindex (m:colorgroup 拡張は不要)
    // - 色 (≒元素) ごとに 1 メッシュパーツにまとめ、親 components オブジェクト配下に置く
    //   (別々の build item にするとスライサで別部品としてバラけるため)
    // - パーツ内は頂点を共有 (weld)。異パーツ間は跨がない。数値は InvariantCulture、色は不透明 #RRGGBB

    /// <summary>
    /// 3MF (色分け) を書き出す。スナップショットを RGB (アルファ無視: 原子と半透明ボンドを同じ材料に載せるため) ごとに
    /// 1 メッシュパーツへまとめ、単一の components 親オブジェクトとして build に載せる。座標系は ExportStl と同じ。
    /// </summary>
    /// <param name="path">出力ファイルパス</param>
    /// <param name="snaps">Collect() で収集したスナップショット</param>
    /// <param name="scale">スケール (mm/Å)</param>
    /// <param name="title">モデル名 (親オブジェクト名とメタデータに使用)</param>
    /// <param name="colorNames">RGB (0xRRGGBB) → 材料名 (元素名など)。無指定またはヒットしない色は #RRGGBB 表記</param>
    /// <returns>書き出した三角形数</returns>
    public static int Export3mf(string path, List<MeshSnapshot> snaps, double scale, string title, Dictionary<int, string> colorNames = null)
    {
        var shift = GetShift(snaps);
        var ci = CultureInfo.InvariantCulture;

        //RGB ごとにグループ化し、パーツ内で頂点を weld したインデックスドメッシュを作る
        var parts = new List<(int Rgb, List<V3> Vertices, List<(int V1, int V2, int V3)> Triangles)>();
        foreach (var group in snaps.GroupBy(s => s.Argb & 0xFFFFFF))
        {
            var vertices = new List<V3>();
            var indexOf = new Dictionary<(long X, long Y, long Z), int>();
            var triangles = new List<(int, int, int)>();
            int weld(in V3 v)
            {
                var key = ((long)Math.Round(v.X * 1E4), (long)Math.Round(v.Y * 1E4), (long)Math.Round(v.Z * 1E4));//0.1µm で量子化
                if (!indexOf.TryGetValue(key, out var i))
                {
                    i = vertices.Count;
                    vertices.Add(v);
                    indexOf.Add(key, i);
                }
                return i;
            }
            foreach (var s in group)
                foreach (var (a, b, c, _) in OrientedTriangles(s, shift, scale))
                {
                    int i1 = weld(a), i2 = weld(b), i3 = weld(c);
                    if (i1 != i2 && i2 != i3 && i3 != i1)//weld で潰れた縮退三角形は捨てる
                        triangles.Add((i1, i2, i3));
                }
            if (triangles.Count > 0)
                parts.Add((group.Key, vertices, triangles));
        }

        using var zip = new ZipArchive(File.Create(path), ZipArchiveMode.Create);
        var settings = new XmlWriterSettings { Encoding = new UTF8Encoding(false), Indent = false, CloseOutput = true };

        using (var xw = XmlWriter.Create(zip.CreateEntry("[Content_Types].xml", CompressionLevel.Optimal).Open(), settings))
        {
            xw.WriteStartElement("Types", "http://schemas.openxmlformats.org/package/2006/content-types");
            xw.WriteStartElement("Default"); xw.WriteAttributeString("Extension", "rels"); xw.WriteAttributeString("ContentType", "application/vnd.openxmlformats-package.relationships+xml"); xw.WriteEndElement();
            xw.WriteStartElement("Default"); xw.WriteAttributeString("Extension", "model"); xw.WriteAttributeString("ContentType", "application/vnd.ms-package.3dmanufacturing-3dmodel+xml"); xw.WriteEndElement();
            xw.WriteEndElement();
        }
        using (var xw = XmlWriter.Create(zip.CreateEntry("_rels/.rels", CompressionLevel.Optimal).Open(), settings))
        {
            xw.WriteStartElement("Relationships", "http://schemas.openxmlformats.org/package/2006/relationships");
            xw.WriteStartElement("Relationship");
            xw.WriteAttributeString("Target", "/3D/3dmodel.model");
            xw.WriteAttributeString("Id", "rel-1");
            xw.WriteAttributeString("Type", "http://schemas.microsoft.com/3dmanufacturing/2013/01/3dmodel");
            xw.WriteEndElement();
            xw.WriteEndElement();
        }
        using (var xw = XmlWriter.Create(zip.CreateEntry("3D/3dmodel.model", CompressionLevel.Optimal).Open(), settings))
        {
            const string ns = "http://schemas.microsoft.com/3dmanufacturing/core/2015/02";
            xw.WriteStartElement("model", ns);
            xw.WriteAttributeString("unit", "millimeter");
            xw.WriteAttributeString("xml", "lang", null, "und");
            xw.WriteStartElement("metadata", ns); xw.WriteAttributeString("name", "Title"); xw.WriteString(title); xw.WriteEndElement();
            xw.WriteStartElement("metadata", ns); xw.WriteAttributeString("name", "Application"); xw.WriteString("ReciPro"); xw.WriteEndElement();

            xw.WriteStartElement("resources", ns);

            //basematerials (id=1): パーツと同順
            xw.WriteStartElement("basematerials", ns);
            xw.WriteAttributeString("id", "1");
            foreach (var (rgb, _, _) in parts)
            {
                var hex = $"#{rgb:X6}";
                xw.WriteStartElement("base", ns);
                xw.WriteAttributeString("name", colorNames != null && colorNames.TryGetValue(rgb, out var nm) ? nm : hex);
                xw.WriteAttributeString("displaycolor", hex);
                xw.WriteEndElement();
            }
            xw.WriteEndElement();

            //各色パーツ (id = 2..) と親 components オブジェクト (id = parts.Count + 2)
            for (int p = 0; p < parts.Count; p++)
            {
                var (rgb, vertices, triangles) = parts[p];
                xw.WriteStartElement("object", ns);
                xw.WriteAttributeString("id", (p + 2).ToString(ci));
                xw.WriteAttributeString("type", "model");
                xw.WriteAttributeString("pid", "1");
                xw.WriteAttributeString("pindex", p.ToString(ci));
                xw.WriteAttributeString("name", colorNames != null && colorNames.TryGetValue(rgb, out var nm) ? nm : $"#{rgb:X6}");
                xw.WriteStartElement("mesh", ns);
                xw.WriteStartElement("vertices", ns);
                foreach (var v in vertices)
                {
                    xw.WriteStartElement("vertex", ns);
                    xw.WriteAttributeString("x", v.X.ToString("0.####", ci));
                    xw.WriteAttributeString("y", v.Y.ToString("0.####", ci));
                    xw.WriteAttributeString("z", v.Z.ToString("0.####", ci));
                    xw.WriteEndElement();
                }
                xw.WriteEndElement();
                xw.WriteStartElement("triangles", ns);
                foreach (var (v1, v2, v3) in triangles)
                {
                    xw.WriteStartElement("triangle", ns);
                    xw.WriteAttributeString("v1", v1.ToString(ci));
                    xw.WriteAttributeString("v2", v2.ToString(ci));
                    xw.WriteAttributeString("v3", v3.ToString(ci));
                    xw.WriteEndElement();
                }
                xw.WriteEndElement();
                xw.WriteEndElement();
                xw.WriteEndElement();
            }

            xw.WriteStartElement("object", ns);
            xw.WriteAttributeString("id", (parts.Count + 2).ToString(ci));
            xw.WriteAttributeString("type", "model");
            xw.WriteAttributeString("name", title);
            xw.WriteStartElement("components", ns);
            for (int p = 0; p < parts.Count; p++)
            {
                xw.WriteStartElement("component", ns);
                xw.WriteAttributeString("objectid", (p + 2).ToString(ci));
                xw.WriteEndElement();
            }
            xw.WriteEndElement();
            xw.WriteEndElement();

            xw.WriteEndElement();//resources

            xw.WriteStartElement("build", ns);
            xw.WriteStartElement("item", ns);
            xw.WriteAttributeString("objectid", (parts.Count + 2).ToString(ci));
            xw.WriteEndElement();
            xw.WriteEndElement();

            xw.WriteEndElement();//model
        }
        return parts.Sum(p => p.Triangles.Count);
    }
}
