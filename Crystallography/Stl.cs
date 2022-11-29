using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Crystallography
{
    /// <summary>
    /// STLファイル
    /// </summary>
    public class StlFile
    {
        #region Constructions
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StlFile()
        {
        }
        #endregion Constructions

        #region Constants
        /// <summary>
        /// バイナリ形式I/O時の有効最大ヘッダバイト数
        /// </summary>
        private const int HeaderLength = 80;
        #endregion Constants

        #region Properties
        /// <summary>
        /// ヘッダーテキスト
        /// </summary>
        /// <remarks>
        /// 初期値は null なので必要に応じて領域確保してください
        /// バイナリ形式のI/Oでは最大80バイトまで
        /// </remarks>
        public byte[] Header { get; set; }

        /// <summary>
        /// フッターテキスト
        /// </summary>
        /// <remarks>
        /// 初期値は null なので必要に応じて領域確保してください
        /// ASCII形式のI/Oのみ利用
        /// </remarks>
        public byte[] Footer { get; set; }

        /// <summary>
        /// ファセット(三角形)配列
        /// </summary>
        /// <remarks>
        /// 初期値は null なので必要に応じて領域確保してください
        /// </remarks>
        public StlFacet[] Facets { get; set; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// バイナリ形式でのSTLファイル書き込み
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>正常終了した場合は true、その他の場合は false</returns>
        /// <remarks>前提としてリトルエンディアンとする</remarks>
        public bool WriteBinary(string filePath)
        {
            // filePath が入っていない場合はエラーとする
            if (filePath == null)
                return false;

            // ファセットデータが無い場合はエラー
            if (Facets == null || Facets.Length == 0)
                return false;

            try
            {
                // バイナリファイルの書き込み
                using var writer = new BinaryWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write));
                // ヘッダ書き込み用配列
                byte[] header = new byte[HeaderLength];

                // プロパティのヘッダに中身が有る場合
                if (Header != null && 1 <= Header.Length)
                {
                    // プロパティのヘッダを、最大80バイトまで、ヘッダ書き込み用配列にコピーする
                    int length = HeaderLength < Header.Length ? HeaderLength : Header.Length;
                    Array.Copy(Header, 0, header, 0, length);
                }

                // ヘッダ書き込み
                writer.Write(header, 0, HeaderLength);

                // ファセットの枚数書き込み
                uint size = (uint)Facets.Length;
                writer.Write(size);

                // 全ファセット書き込み
                ushort buff = 0;
                foreach (var facet in Facets)
                {
                    writer.Write(facet.Normal.X);
                    writer.Write(facet.Normal.Y);
                    writer.Write(facet.Normal.Z);
                    writer.Write(facet.Vertex1.X);
                    writer.Write(facet.Vertex1.Y);
                    writer.Write(facet.Vertex1.Z);
                    writer.Write(facet.Vertex2.X);
                    writer.Write(facet.Vertex2.Y);
                    writer.Write(facet.Vertex2.Z);
                    writer.Write(facet.Vertex3.X);
                    writer.Write(facet.Vertex3.Y);
                    writer.Write(facet.Vertex3.Z);
                    writer.Write(buff);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// バイナリ形式でのSTLファイル読み込み
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>正常終了した場合は true、その他の場合は false</returns>
        /// <remarks>前提としてリトルエンディアンとする</remarks>
        public bool ReadBinary(string filePath)
        {
            // filePath が null か、ファイルが存在しない場合はエラーとする
            if (filePath == null || File.Exists(filePath) == false)
                return false;

            try
            {
                // バイナリファイルの読み込み
                using var reader = new BinaryReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));
                // ヘッダ読み込み
                Header = reader.ReadBytes(HeaderLength);

                // ファセットの枚数読み込み
                uint size = reader.ReadUInt32();

                // ファイルの残りのバイト数
                long rest = reader.BaseStream.Length - reader.BaseStream.Position;

                // ファセット1枚分のバイト数
                const int FacetLength = 50;

                // ファイルの残りのバイト数が、求められるファセットの枚数分のバイト数より少なければエラー
                if (rest < FacetLength * size)
                    return false;

                // 全ファセット読み込み
                Facets = new StlFacet[size];
                for (int i = 0; i < size; ++i)
                {
                    // ファセット1個分のバイト配列読み込み
                    byte[] bytes = reader.ReadBytes(FacetLength);

                    // ファセットデータ生成と配列への格納
                    int index = 0;
                    const int offset = sizeof(float);
                    Facets[i] = new StlFacet(
                        new StlVertex(
                            BitConverter.ToSingle(bytes, index),
                            BitConverter.ToSingle(bytes, index += offset),
                            BitConverter.ToSingle(bytes, index += offset)),
                        new StlVertex(
                            BitConverter.ToSingle(bytes, index += offset),
                            BitConverter.ToSingle(bytes, index += offset),
                            BitConverter.ToSingle(bytes, index += offset)),
                        new StlVertex(
                            BitConverter.ToSingle(bytes, index += offset),
                            BitConverter.ToSingle(bytes, index += offset),
                            BitConverter.ToSingle(bytes, index += offset)),
                        new StlVertex(
                            BitConverter.ToSingle(bytes, index += offset),
                            BitConverter.ToSingle(bytes, index += offset),
                            BitConverter.ToSingle(bytes, index += offset))
                    );
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// テキスト(ASCII)形式でのSTLファイル書き込み
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>正常終了した場合は true、その他の場合は false</returns>
        public bool WriteAscii(string filePath)
        {
            // filePath が入っていない場合はエラーとする
            if (filePath == null)
                return false;

            // ファセットデータが無い場合はエラー
            if (Facets == null || Facets.Length == 0)
                return false;

            try
            {
                // 上書きの書き込みモードでファイルを開く
                using var writer = new StreamWriter(filePath, false, Encoding.ASCII);
                // ヘッダ書き込み
                string header = Header == null ? null : Encoding.ASCII.GetString(Header);
                writer.WriteLine("solid " + header);

                // 全ファセットデータ書き込み
                foreach (var facet in Facets)
                {
                    writer.WriteLine("  facet normal " + ToText(facet.Normal));
                    writer.WriteLine("    outer loop");
                    writer.WriteLine("      vertex " + ToText(facet.Vertex1));
                    writer.WriteLine("      vertex " + ToText(facet.Vertex2));
                    writer.WriteLine("      vertex " + ToText(facet.Vertex3));
                    writer.WriteLine("    endloop");
                    writer.WriteLine("  endfacet");
                }

                // フッタ書き込み
                string footer = Footer == null ? null : Encoding.ASCII.GetString(Footer);
                writer.WriteLine("endsolid " + footer);

                // 頂点データをテキストに変換
                static string ToText(in StlVertex vec) => $"{vec.X:e} {vec.Y:e} {vec.Z:e}";
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// テキスト(ASCII)形式でのSTLファイル読み込み
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>正常終了した場合は true、その他の場合は false</returns>
        public bool ReadAscii(string filePath)
        {
            // filePath が null か、ファイルが存在しない場合はエラーとする
            if (filePath == null || File.Exists(filePath) == false)
                return false;

            try
            {
                // テキストファイルの読み込み
                using StreamReader reader = new StreamReader(filePath, Encoding.ASCII);
                // ファセットデータの一時格納用リスト
                var facets = new List<StlFacet>();

                // ファセット1個分のデータ生成
                var facet = new StlFacet();

                // 頂点読み込み時、何個目の頂点か
                int vertexNumber = 0;

                // ファイル末尾まで繰り返す
                while (!reader.EndOfStream)
                {
                    // ファイルの一行を読み込む
                    string line = reader.ReadLine();

                    // 一行が空ならスルー
                    if (line == null || line.Length <= 0)
                        continue;

                    // 一行の先頭の空白文字を削除
                    line = line.TrimStart();

                    if (line.StartsWith("vertex "))
                    {
                        var text = line.Remove(0, 7);
                        switch (vertexNumber)
                        {
                            case 0: TextToVertex(text, ref facet.Vertex1); break;
                            case 1: TextToVertex(text, ref facet.Vertex2); break;
                            case 2: TextToVertex(text, ref facet.Vertex3); break;
                        }
                        vertexNumber++;
                    }
                    else if (line.StartsWith("facet normal "))
                    {
                        var text = line.Remove(0, 13);
                        TextToVertex(text, ref facet.Normal);
                    }
                    else if (line.StartsWith("endfacet"))
                    {
                        facets.Add(facet);
                        facet = new StlFacet();
                        vertexNumber = 0;
                    }
                    else if (line.StartsWith("solid "))
                    {
                        var header = line.Remove(0, 6);
                        Header = Encoding.ASCII.GetBytes(header);
                    }
                    else if (line.StartsWith("endsolid "))
                    {
                        var footer = line.Remove(0, 9);
                        Footer = Encoding.ASCII.GetBytes(footer);
                    }

                    // テキストを頂点データに変換する
                    static void TextToVertex(string text, ref StlVertex vec)
                    {
                        var values = text.Split(' ');
                        vec.X = float.Parse(values[0], NumberStyles.Float);
                        vec.Y = float.Parse(values[1], NumberStyles.Float);
                        vec.Z = float.Parse(values[2], NumberStyles.Float);
                    }
                }

                // ファセットデータの一時格納用リストを配列化してメンバーに設定
                Facets = facets.ToArray();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion Methods
    }

    /// <summary>
    /// 頂点
    /// </summary>
    public struct StlVertex
    {
        #region Constructions
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StlVertex(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        #endregion Constructions

        #region Properties
        /// <summary>
        /// X成分
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Y成分
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Z成分
        /// </summary>
        public float Z { get; set; }
        #endregion Properties
    }

    /// <summary>
    /// ファセット（3頂点と1法線で表現する三角形）
    /// </summary>
    public class StlFacet
    {
        #region Constructions
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StlFacet()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StlFacet(in StlVertex normal, in StlVertex vertex1, in StlVertex vertex2, in StlVertex vertex3)
        {
            Normal = normal;
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Vertex3 = vertex3;
        }
        #endregion Constructions

        #region Properties
        /// <summary>
        /// 法線
        /// </summary>
        public StlVertex Normal;

        /// <summary>
        /// 1点目頂点
        /// </summary>
        public StlVertex Vertex1;

        /// <summary>
        /// 2点目頂点
        /// </summary>
        public StlVertex Vertex2;

        /// <summary>
        /// 3点目頂点
        /// </summary>
        public StlVertex Vertex3;
        #endregion Properties
    }
}