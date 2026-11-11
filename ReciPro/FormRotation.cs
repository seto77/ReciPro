using Crystallography.OpenGL;
using OpenTK;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using C4 = OpenTK.Mathematics.Color4;
using V3 = OpenTK.Mathematics.Vector3d;
using OpenTK.Mathematics;

namespace ReciPro;

public partial class FormRotationMatrix : FormBase
{

    #region プロパティ
    /// <summary>R_base  =R_ex ^-1 * R_reci の関係がある.</summary>
    public Matrix3D RotBase { get; set; } = new Matrix3D();

    public Matrix3D RotReciPro => Euler.ToMatrix(FormMain.Phi, FormMain.Theta, FormMain.Psi);

    public Matrix3D RotExp
    {
        get
        {
            var dir = getExpDirections();

            var rot = Matrix3D.Rot(dir[0], numericBoxExp1.RadianValue);
            if (dir.Length > 1)
                rot *= Matrix3D.Rot(dir[1], numericBoxExp2.RadianValue);
            if (dir.Length > 2)
                rot *= Matrix3D.Rot(dir[2], numericBoxExp3.RadianValue);

            return rot;
        }
    }

    public FormRotationMatrix() { InitializeComponent(); HelpPage = "4-rotation-geometry"; } //260529Cl HelpPage 追加

    public bool Linked => Visible && checkBoxLink.Checked;

    #endregion

    #region フィールド
    private bool skip = false;
    public FormMain FormMain;

    private GLControlAlpha glControlReciProObjects;
    private GLControlAlpha glControlReciProAxes;
    private GLControlAlpha glControlExpObjects;
    private GLControlAlpha glControlExpAxes;
    private GLControlAlpha glControlReciProGonio;
    private GLControlAlpha glControlExpGonio;
    private static readonly string[] separator = new[] { "\r\n" };
    #endregion

    #region コンストラクタ、ロード、クローズ、Visible

    /// <summary>起動時</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FormRotationMatrix_Load(object sender, EventArgs e)
    {
        #region glControlの追加 (デザイナが壊れるため)

        // 260331Cl 追加: Load時に追加するコントロールはPerformAutoScale対象外のため手動スケーリング
        var s = DeviceDpi / 96f;
        int S(int v) => (int)(v * s);

        //
        // glControlReciProObjects
        //
        this.glControlReciProObjects = new GLControlAlpha
        {
            AllowMouseScaling = false,
            AllowMouseTranslating = false,
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            BorderStyle = BorderStyle.Fixed3D,
            // 260726Cl: 一旦 S(262) へ 11px 左寄せしたが、同じ groupBox 内の glControl*Gonio (Location 5, Size 264
            //   = 右端 269) と 7px 重なることが判明したため差し戻した。Gonio は Add 順で最背面なので、
            //   重ねると Gonio の右端 7px×130px が常時覆われる。273 は 273+130=403 で groupBox クライアント幅 404 に
            //   ちょうど収まる設計値。左に余地を作るなら Gonio 側の Size を縮めること。
            Location = new Point(S(273), S(248)),
            Margin = new Padding(0),
            Name = "glControlReciProObjects",
            NodeCoefficient = 1,
            ProjWidth = 1D,
            Size = new Size(S(130), S(130)),
        };
        this.glControlReciProObjects.WorldMatrixChanged += new System.EventHandler(this.GlControlReciProAxes_WorldMatrixChanged);

        //
        // glControlReciProAxes
        //
        this.glControlReciProAxes = new GLControlAlpha
        {
            AllowMouseScaling = false,
            AllowMouseTranslating = false,
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            BorderStyle = BorderStyle.Fixed3D,
            // 260726Cl: S(262) への 11px 左寄せは glControl*Gonio (右端 269) と 7px 重なるため差し戻し (上記参照)。
            Location = new Point(S(273), S(114)),
            Margin = new Padding(0),
            Name = "glControlReciProAxes",
            NodeCoefficient = 1,
            ProjWidth = 2.8D,
            Size = new Size(S(130), S(130)),
        };
        glControlReciProAxes.WorldMatrixChanged += new EventHandler(GlControlReciProAxes_WorldMatrixChanged);

        //
        // glControlReciProGonio
        //
        this.glControlReciProGonio = new GLControlAlpha
        {
            AllowMouseTranslating = false,
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            BorderStyle = BorderStyle.Fixed3D,
            Location = new Point(S(5), S(114)),
            Margin = new Padding(0),
            Name = "glControlReciProGonio",
            ProjWidth = 5D,
            Size = new Size(S(264), S(264)),
        };
        glControlReciProGonio.WorldMatrixChanged += new EventHandler(this.GlControlReciProAxes_WorldMatrixChanged);

        //
        // glControlExpObjects
        //
        this.glControlExpObjects = new GLControlAlpha
        {
            AllowMouseScaling = false,
            AllowMouseTranslating = false,
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            BorderStyle = BorderStyle.Fixed3D,
            // 260726Cl: 一旦 S(262) へ 11px 左寄せしたが、同じ groupBox 内の glControl*Gonio (Location 5, Size 264
            //   = 右端 269) と 7px 重なることが判明したため差し戻した。Gonio は Add 順で最背面なので、
            //   重ねると Gonio の右端 7px×130px が常時覆われる。273 は 273+130=403 で groupBox クライアント幅 404 に
            //   ちょうど収まる設計値。左に余地を作るなら Gonio 側の Size を縮めること。
            Location = new Point(S(273), S(248)),
            Margin = new Padding(0),
            Name = "glControlExpObjects",
            ProjWidth = 1D,
            Size = new Size(S(130), S(130)),
        };
        this.glControlExpObjects.WorldMatrixChanged += new EventHandler(GlControlReciProAxes_WorldMatrixChanged);
        //
        // glControlExpAxes
        //
        this.glControlExpAxes = new GLControlAlpha
        {
            AllowMouseScaling = false,
            AllowMouseTranslating = false,
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            BorderStyle = BorderStyle.Fixed3D,
            // 260726Cl: S(262) への 11px 左寄せは glControl*Gonio (右端 269) と 7px 重なるため差し戻し (上記参照)。
            Location = new Point(S(273), S(114)),
            Margin = new System.Windows.Forms.Padding(0),
            Name = "glControlExpAxes",
            ProjWidth = 2.8D,
            Size = new Size(S(130), S(130)),
        };
        this.glControlExpAxes.WorldMatrixChanged += new EventHandler(GlControlReciProAxes_WorldMatrixChanged);
        //
        // glControlExpGonio
        //
        this.glControlExpGonio = new GLControlAlpha
        {
            AllowMouseTranslating = false,
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            BorderStyle = BorderStyle.Fixed3D,
            Location = new Point(S(5), S(114)),
            Margin = new Padding(0),
            Name = "glControlExpGonio",
            ProjWidth = 5D,
            Size = new Size(S(264), S(264)),
        };
        this.glControlExpGonio.WorldMatrixChanged += new System.EventHandler(this.GlControlReciProAxes_WorldMatrixChanged);


        groupBoxExperimentalCoordinate.Controls.Add(glControlExpObjects);
        groupBoxExperimentalCoordinate.Controls.Add(glControlExpAxes);
        groupBoxExperimentalCoordinate.Controls.Add(glControlExpGonio);

        groupBoxReciProCoordinate.Controls.Add(glControlReciProObjects);
        groupBoxReciProCoordinate.Controls.Add(glControlReciProAxes);
        groupBoxReciProCoordinate.Controls.Add(glControlReciProGonio);

        #endregion
        glControlReciProGonio.WorldMatrix = Matrix4d.CreateRotationZ(-Math.PI / 4) * Matrix4d.CreateRotationX(-0.4 * Math.PI);
        // 261111Cl 追加: XYZ 軸は方位に依らない静的なオブジェクト (視点は WorldMatrix の連動で回る) なので、ここで 1 回だけ作る。
        //  SetRotation で回転のたびに作り直していた (TextObject 3 個を含む)。GL の Program はコンストラクタで作られ、軸の窓のオブジェクトを消すのは setAxes だけ。
        //  TextObject の大きさは DPI に依らないので、DPI の変更で作り直す必要もない
        setAxes(glControlReciProAxes);
        setAxes(glControlExpAxes);
        SetRotation();

        numericBoxPhi.ValueBackColor = numericBoxExp1.ValueBackColor = Color.FromArgb(255, 180, 180, 0);
        numericBoxTheta.ValueBackColor = numericBoxExp2.ValueBackColor = Color.FromArgb(255, 0, 180, 180);
        numericBoxPsi.ValueBackColor = numericBoxExp3.ValueBackColor = Color.FromArgb(255, 180, 0, 180);

        numericBoxPhi.ValueForeColor = numericBoxExp1.ValueForeColor =
        numericBoxTheta.ValueForeColor = numericBoxExp2.ValueForeColor =
        numericBoxPsi.ValueForeColor = numericBoxExp3.ValueForeColor = Color.FromArgb(255, 255, 255, 255);
    }

    private void FormRotationMatrix_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        FormMain.toolStripButtonRotation.Checked = false;
        this.Visible = false;
    }

    private void FormRotationMatrix_VisibleChanged(object sender, EventArgs e)
    {
        //FormMain.toolStripButtonRotation.Checked = true; // 261105Cl 変更前: × で閉じたとき (FormClosing → Visible=false) にも点灯させていた
        FormMain.toolStripButtonRotation.Checked = Visible; // 261105Cl 変更 (監査 G-21)

        // 261105Cl 追加 (監査 G-10): 閉じている間は主窓から更新されないので、開いたときに今の方位で描き直す。
        //  Link は閉じている間は止まっている (Linked = Visible && Link) ので、ゴニオの角度はそのままに、今の方位を基準に結び直す
        //  (Link を入れ直したときと同じ。ゴニオの制約・結晶の方位は変えない)
        if (Visible && glControlReciProGonio != null && FormMain.Crystal != null)
        {
            if (checkBoxLink.Checked)
                RotBase = RotExp.Inverse() * RotReciPro;
            SetRotation(false);
        }
    }

    /// <summary>
    /// 260524Cl 追加: --capture 用。Show しただけでは OpenGL のトーラス/軸/球が描かれない (VisibleChanged は描画しない) ため、
    /// SetRotation() を呼んで現在の Euler 角に対応する GL オブジェクトを生成・描画する。
    /// 通常操作には影響させず、呼び出し元は GuiCapture に限定する。
    /// </summary>
    internal void PrepareCaptureForGuiAudit()
    {
        if (FormMain == null)
            return;
        SetRotation();
    }

    #endregion

    #region Excelのコピーペースト
    private void buttonCopy_Click(object sender, EventArgs e)
    {
        //var str =
        //    $"{numericBox11.Value}\t{numericBox12.Value}\t{numericBox13.Value}\n" +
        //    $"{numericBox21.Value}\t{numericBox22.Value}\t{numericBox23.Value}\n" +
        //    $"{numericBox31.Value}\t{numericBox32.Value}\t{numericBox33.Value}"; // 261105Cl 変更前: 行の区切りが \n だけで、下の Paste (\r\n で分割) が読めなかった
        // 261105Cl 変更 (監査 G-14): Excel と同じ \r\n で行を区切る
        var str =
            $"{numericBox11.Value}\t{numericBox12.Value}\t{numericBox13.Value}\r\n" +
            $"{numericBox21.Value}\t{numericBox22.Value}\t{numericBox23.Value}\r\n" +
            $"{numericBox31.Value}\t{numericBox32.Value}\t{numericBox33.Value}";
        Clipboard.SetDataObject(str);
    }

    private void buttonPaste_Click(object sender, EventArgs e)
    {
        if (Clipboard.GetDataObject().GetDataPresent(typeof(string)))
        {
            var data = Clipboard.GetDataObject();
            var str = (string)data.GetData(typeof(string).ToString(), true);

            //var str1 = str.Split(separator, StringSplitOptions.None); // 261105Cl 変更前: \r\n でしか分割せず、Copy (\n 区切り) の内容を Paste できなかった
            var str1 = str.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries); // 261105Cl 変更 (監査 G-14): \r\n・\n のどちらの改行でも読む
            if (str1.Length < 3) return;

            var row1 = str1[0].Split(new[] { '\t' }, StringSplitOptions.None);
            var row2 = str1[1].Split(new[] { '\t' }, StringSplitOptions.None);
            var row3 = str1[2].Split(new[] { '\t' }, StringSplitOptions.None);

            if (row1.Length != 3 || row2.Length != 3 || row3.Length != 3)
                return;

            if (!double.TryParse(row1[0], out var e11)) return;
            if (!double.TryParse(row1[1], out var e12)) return;
            if (!double.TryParse(row1[2], out var e13)) return;
            if (!double.TryParse(row2[0], out var e21)) return;
            if (!double.TryParse(row2[1], out var e22)) return;
            if (!double.TryParse(row2[2], out var e23)) return;
            if (!double.TryParse(row3[0], out var e31)) return;
            if (!double.TryParse(row3[1], out var e32)) return;
            if (!double.TryParse(row3[2], out var e33)) return;

            // 261105Cl 追加 (監査 G-14): 回転行列 (直交・行列式 +1) でなければ方位に入れない (旧: 検査なしで結晶の方位になっていた)。
            //  表計算の丸め程度のずれ (RᵀR − I が 1e-3 以下) は、最も近い回転行列 (SVD の U·Vᵀ) に直して使う
            var m = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.DenseOfArray(new[,] { { e11, e12, e13 }, { e21, e22, e23 }, { e31, e32, e33 } });
            var deviation = (m.Transpose() * m - MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.DenseIdentity(3)).Enumerate().Max(v => Math.Abs(v));
            if (!(deviation <= 1e-3) || !(m.Determinant() > 0))
            {
                MessageBox.Show(this,
                    Localization.Loc(
                        en: "The pasted values are not a rotation matrix\n(rows and columns must be orthonormal and the determinant +1).",
                        ja: "貼り付けた値は回転行列ではありません\n(行と列が正規直交で、行列式が +1 である必要があります)。",
                        de: "Die eingefügten Werte bilden keine Rotationsmatrix\n(Zeilen und Spalten müssen orthonormal sein, Determinante +1).",
                        fr: "Les valeurs collées ne forment pas une matrice de rotation\n(lignes et colonnes orthonormées, déterminant +1).",
                        es: "Los valores pegados no forman una matriz de rotación\n(filas y columnas ortonormales, determinante +1).",
                        pt: "Os valores colados não formam uma matriz de rotação\n(linhas e colunas ortonormais, determinante +1).",
                        it: "I valori incollati non formano una matrice di rotazione\n(righe e colonne ortonormali, determinante +1).",
                        ru: "Вставленные значения не образуют матрицу поворота\n(строки и столбцы должны быть ортонормированы, определитель +1).",
                        zhHans: "粘贴的值不是旋转矩阵\n（行与列须正交归一，行列式为 +1）。",
                        zhHant: "貼上的值不是旋轉矩陣\n（各列與各行須為正規正交，行列式為 +1）。",
                        ko: "붙여 넣은 값은 회전 행렬이 아닙니다\n(행과 열이 정규 직교이고 행렬식이 +1이어야 합니다)."),
                    Localization.Loc(en: "Rotation matrix", ja: "回転行列", de: "Rotationsmatrix", fr: "Matrice de rotation", es: "Matriz de rotación",
                        pt: "Matriz de rotação", it: "Matrice di rotazione", ru: "Матрица поворота", zhHans: "旋转矩阵", zhHant: "旋轉矩陣", ko: "회전 행렬"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var svd = m.Svd();
            var nearest = svd.U * svd.VT;
            (e11, e12, e13) = (nearest[0, 0], nearest[0, 1], nearest[0, 2]);
            (e21, e22, e23) = (nearest[1, 0], nearest[1, 1], nearest[1, 2]);
            (e31, e32, e33) = (nearest[2, 0], nearest[2, 1], nearest[2, 2]);

            skip = true;
            numericBox11.Value = e11;
            numericBox12.Value = e12;
            numericBox13.Value = e13;
            numericBox21.Value = e21;
            numericBox22.Value = e22;
            numericBox23.Value = e23;
            numericBox31.Value = e31;
            numericBox32.Value = e32;
            numericBox33.Value = e33;
            skip = false;

            numericBox_ValueChanged(sender, e);

            //260801Cl 追加: 貼り付けた行列で実際に結晶を回転させる。
            //従来は 9 個のボックス (いずれも ReadOnly の表示専用) と Phi/Theta/Psi の表示を書き換えるだけで
            //FormMain.SetRotation を呼んでおらず、貼り付けても結晶は回らないうえ、他の窓で回転した瞬間に
            //SetRotation() がボックスを RotReciPro で上書きして貼り付け値が消えていた。
            //列優先の並び (E11,E21,E31, E12,E22,E32, E13,E23,E33) は numericBox_ValueChanged と同じ。
            FormMain.SetRotation(new Matrix3D(e11, e21, e31, e12, e22, e32, e13, e23, e33));
        }
    }

    #endregion

    private void numericBox_ValueChanged(object sender, EventArgs e)
    {
        if (skip) return;
        skip = true;

        var rotMatrix = new Matrix3D(numericBox11.Value, numericBox21.Value, numericBox31.Value, numericBox12.Value, numericBox22.Value, numericBox32.Value, numericBox13.Value, numericBox23.Value, numericBox33.Value);
        var (Phi, Theta, Psi) = Euler.FromMatrix(rotMatrix);
        numericBoxPhi.RadianValue = Phi;
        numericBoxTheta.RadianValue = Theta;
        numericBoxPsi.RadianValue = Psi;

        skip = false;
    }

    /// <summary>
    /// Link状態の時、FormMainから呼ばれる。rotにもっとも近い回転行列をExperimetal coordinatesの
    /// オイラー角で表現する。その後、この回転行列で他のウィンドウに回転命令を出す。
    /// </summary>
    /// <param name="rot"></param>
    public void SetRotation(Matrix3D rot)
    {
        var settings = new List<(V3 Vec, double Angle, bool Variable)>();
        var dir = getExpDirections();
        settings.Add((dir[0], numericBoxExp1.RadianValue, !checkBoxFix1st.Checked));
        if (checkBoxEnable2nd.Checked)
            settings.Add((dir[1], numericBoxExp2.RadianValue, !checkBoxFix2nd.Checked));
        if (checkBoxEnable3rd.Checked)
            settings.Add((dir[2], numericBoxExp3.RadianValue, !checkBoxFix3rd.Checked));

        var angles = Euler.DecomposeMatrix2(rot * RotBase.Inverse(), [.. settings]);
        skip = true;
        numericBoxExp1.RadianValue = angles[0];
        if (checkBoxEnable2nd.Checked)
            numericBoxExp2.RadianValue = angles[1];
        if (checkBoxEnable3rd.Checked)
            numericBoxExp3.RadianValue = angles[2];
        skip = false;
        NumericBoxExp_ValueChanged(this, EventArgs.Empty);
    }

    /// <summary>角度をセット.</summary>
    /// <param name="fromExp">trueの時は、Experimental coordinatesの制限を解除して、オイラー角を更新する。</param>
    public void SetRotation(bool renewExpEuler = true)
    {
        if (skip) return;

        numericBoxPhi.RadianValue = FormMain.Phi;
        numericBoxTheta.RadianValue = FormMain.Theta;
        numericBoxPsi.RadianValue = FormMain.Psi;

        //var rotMatrix = Euler.SetEulerAngle(numericBoxPhi.RadianValue, numericBoxTheta.RadianValue, numericBoxPsi.RadianValue);
        skip = true;
        numericBox11.Value = RotReciPro.E11;
        numericBox12.Value = RotReciPro.E12;
        numericBox13.Value = RotReciPro.E13;
        numericBox21.Value = RotReciPro.E21;
        numericBox22.Value = RotReciPro.E22;
        numericBox23.Value = RotReciPro.E23;
        numericBox31.Value = RotReciPro.E31;
        numericBox32.Value = RotReciPro.E32;
        numericBox33.Value = RotReciPro.E33;
        skip = false;

        //if (renewExpEuler && Linked)
        //{
        //    skip = true;
        //    checkBoxEnable2nd.Checked = checkBoxEnable3rd.Checked = true;
        //    checkBoxFix1st.Checked = checkBoxFix2nd.Checked = checkBoxFix3rd.Checked = false;
        //    var settings = new List<(V3 Vec, double Angle, bool Variable)>();
        //    var dir = getExpDirections();
        //    settings.Add((dir[0], numericBoxExp1.RadianValue, !checkBoxFix1st.Checked));
        //    settings.Add((dir[1], numericBoxExp2.RadianValue, !checkBoxFix2nd.Checked));
        //    settings.Add((dir[2], numericBoxExp3.RadianValue, !checkBoxFix3rd.Checked));
        //
        //    var angles = Euler.DecomposeMatrix2(RotReciPro * RotBase.Inverse(), [.. settings]);
        //    numericBoxExp1.RadianValue = angles[0];
        //    numericBoxExp2.RadianValue = angles[1];
        //    numericBoxExp3.RadianValue = angles[2];
        //    skip = false;
        //} // 261105Cl 変更前: Link 中に方位を外から決められると、ゴニオの制約 (Fix・第 2/第 3 軸の有効) を黙って全部外して 3 軸に分解し直していた

        // 261105Cl 変更 (作者の決定 G-13): Link 中に方位を外から絶対値で決められたとき (主窓の Euler 角の入力・Reset・View along、この窓の Paste、
        //  結晶の切り替え、他の窓やマクロからの方位の設定) は、Link を外して主窓のステータスバーで知らせる。
        //  方位は入力どおり正確に入り、ゴニオの角度と制約の設定はそのまま残る (Link を入れ直せば、その時点の方位で結び直す)。
        //  矢印ボタン・ドラッグなどの相対回転は FormMain.Rotate → SetRotation(Matrix3D) で制約を保ったまま回すので、ここには来ない。
        //  方位が Link の関係 (R = R_exp R_base) から変わっていないとき (結晶の編集など) は Link を保つ
        if (renewExpEuler && Linked && (RotExp * RotBase - RotReciPro).ToArrayRight().Max(v => Math.Abs(v)) > 1e-6)
        {
            skip = true; // CheckBoxLink_CheckedChanged の描き直しは飛ばし、下でまとめて描く
            checkBoxLink.Checked = false;
            skip = false;
            FormMain.toolStripStatusLabel.Text = Localization.Loc(
                en: "Rotation Geometry: Link was turned off to set the orientation exactly (goniometer settings are kept).",
                ja: "回転ジオメトリ: 方位を入力どおりに設定するため、リンクを外しました (ゴニオメーターの設定は保たれます)。",
                de: "Rotationsgeometrie: „Verknüpfen“ wurde ausgeschaltet, um die Orientierung genau zu setzen (Goniometer-Einstellungen bleiben erhalten).",
                fr: "Géométrie de rotation : « Lier » a été désactivé pour appliquer l'orientation exacte (réglages du goniomètre conservés).",
                es: "Geometría de rotación: se desactivó «Enlazar» para fijar la orientación exacta (se conservan los ajustes del goniómetro).",
                pt: "Geometria de rotação: \"Vincular\" foi desativado para definir a orientação exata (as configurações do goniômetro são mantidas).",
                it: "Geometria di rotazione: «Collega» è stato disattivato per impostare l'orientazione esatta (le impostazioni del goniometro restano).",
                ru: "Геометрия вращения: «Связать» отключено, чтобы задать ориентацию точно (настройки гониометра сохранены).",
                zhHans: "旋转几何：为精确设置取向，已取消“联动”（测角仪设置保持不变）。",
                zhHant: "旋轉幾何：為精確設定方位，已取消「連結」（測角儀設定保持不變）。",
                ko: "회전 기하학: 방위를 정확히 설정하기 위해 '연동'을 해제했습니다 (고니오미터 설정은 유지됩니다)."); // 261105Cl
        }
        //ReciPro coordinatesの描画
        var dirReciPro = new[] { new V3(0, 0, 1), new V3(1, 0, 0), new V3(0, 0, 1) };
        var angleReciPro = new[] { FormMain.Phi, FormMain.Theta, FormMain.Psi };
        setGonio(glControlReciProGonio, dirReciPro, angleReciPro);
        // setObject(glControlReciProObjects, dirReciPro, angleReciPro); // (260322Ch) 旧実装: 2 回しか使わない短い helper を経由していた
        glControlReciProObjects.DeleteAllObjects();
        glControlReciProObjects.AddObjects(createObject(glControlReciProObjects, dirReciPro, angleReciPro));
        glControlReciProObjects.Refresh(); // (260322Ch) 短い helper は介さずその場で処理する
        //setAxes(glControlReciProAxes); // 261111Cl 削除: Load で 1 回だけ作る

        //Experimetal coordinatesの描画
        var dirExp = getExpDirections();
        var angleExp = new[] { numericBoxExp1.RadianValue, numericBoxExp2.RadianValue, numericBoxExp3.RadianValue };
        setGonio(glControlExpGonio, dirExp, angleExp);
        // setObject(glControlExpObjects, dirExp, angleExp); // (260322Ch) 旧実装
        glControlExpObjects.DeleteAllObjects();
        glControlExpObjects.AddObjects(createObject(glControlExpObjects, dirExp, angleExp));
        glControlExpObjects.Refresh(); // (260322Ch)
        //setAxes(glControlExpAxes); // 261111Cl 削除: Load で 1 回だけ作る
    }

    #region OpenGL

    /// <summary>軸オブジェクトを生成</summary>
    /// <param name="gl"></param>
    private static void setAxes(GLControlAlpha gl)
    {
        gl.DeleteAllObjects();
        var obj = new List<GLObject>();
        var r = 0.065;
        //X軸
        var c = new C4(0.7f, 0.5f, 0.5f, 1f);
        obj.Add(new Cylinder(new V3(-1, 0, 0), new V3(2, 0, 0), r, new Material(c), DrawingMode.Surfaces));//軸
        obj.Add(new Cone(new V3(1.1, 0, 0), new V3(-0.2, 0, 0), r * 2, new Material(c), DrawingMode.Surfaces));//矢
        obj.Add(new TextObject("X", 11, new V3(1.25, 0, 0), 0, true, new Material(c), gl));

        //Y軸
        c = new C4(0.5f, 0.7f, 0.5f, 1f);
        obj.Add(new Cylinder(new V3(0, -1, 0), new V3(0, 2, 0), r, new Material(c), DrawingMode.Surfaces));//軸
        obj.Add(new Cone(new V3(0, 1.1, 0), new V3(0, -0.2, 0), r * 2, new Material(c), DrawingMode.Surfaces));//矢
        obj.Add(new TextObject("Y", 11, new V3(0, 1.25, 0), 0, true, new Material(c), gl));

        //Z軸
        c = new C4(0.5f, 0.5f, 0.7f, 1f);
        obj.Add(new Cylinder(new V3(0, 0, -1), new V3(0, 0, 2), r, new Material(c), DrawingMode.Surfaces));//軸
        obj.Add(new Cone(new V3(0, 0, 1.1), new V3(0, 0, -0.2), r * 2, new Material(c), DrawingMode.Surfaces));//矢
        obj.Add(new TextObject("Z", 11, new V3(0, 0, 1.25), 0, true, new Material(c), gl));

        //中央の球
        obj.Add(new Sphere(new V3(0, 0, 0), r * 2, new Material(C4.Gray), DrawingMode.Surfaces));

        gl.AddObjects(obj);
        gl.Refresh();

    }

    /// <summary>ゴニオオブジェクトを生成</summary>
    /// <param name="gl"></param>
    /// <param name="dir"></param>
    /// <param name="angle"></param>
    private void setGonio(GLControlAlpha gl, V3[] dir, double[] angle)
    {
        gl.DeleteAllObjects();

        var r = 0.05;
        var obj = new List<GLObject>();

        var rot = dir.Select((d, i) => Matrix3D.Rot(d, angle[i])).ToArray();
        var isReciPro = gl.Name.Contains("ReciPro");//260718Cl 追加: 同一判定が 3 箇所で反復されていたため一次受け

        //1st
        var mat = new Material(new C4(0.8f, 0.8f, 0f, 1f));
        obj.Add(new Cone(dir[0] * 2.1, dir[0] * -0.2, r * 2, mat, DrawingMode.Surfaces));//矢
        obj.Add(new TextObject(isReciPro ? "Φ" : "1st", 12, dir[0] * 2.1 + dir[0].Normalized() * 0.01, 0.05, true, mat, gl));

        if (!checkBoxEnable2nd.Checked)//2ndが存在しない時
        {
            obj.Add(new Cylinder(dir[0] * -1.9, dir[0] * 3.8, r, mat, DrawingMode.Surfaces));//軸
            obj.Add(new Torus(new V3(0, 0, 0), rot[0] * V3.Cross(dir[0], new V3(dir[0].Z, dir[0].X, dir[0].Y)), 1.6, r * 1.5, mat, DrawingMode.Surfaces));//トーラス
        }
        else //2ndが存在する時
        {
            obj.Add(new Cylinder(dir[0] * -1.9, dir[0] * 0.3, r, mat, DrawingMode.Surfaces));//軸
            obj.Add(new Cylinder(dir[0] * 1.6, dir[0] * 0.3, r, mat, DrawingMode.Surfaces));//軸
                                                                                            //1stトーラスの法線は、1st軸と2nd軸が直交する方向
            obj.Add(new Torus(new V3(0, 0, 0), rot[0] * V3.Cross(dir[0], dir[1]), 1.6, r * 1.5, mat, DrawingMode.Surfaces));//トーラス

            //以下2nd
            var rot01 = rot[0] * rot[1];
            mat = new Material(new C4(0f, 0.8f, 0.8f, 1f));
            var n = rot[0] * dir[1];
            obj.Add(new Cone(n * 2.0, -n * 0.2, r * 2, mat, DrawingMode.Surfaces));//矢
            obj.Add(new TextObject(isReciPro ? "Θ" : "2nd", 12, n * 2.0 + n.Normalized() * 0.01, 0.05, true, mat, gl));

            if (!checkBoxEnable3rd.Checked)
            {
                obj.Add(new Cylinder(n * 1.9, -n * 3.8, r, mat, DrawingMode.Surfaces));//軸
                obj.Add(new Torus(new V3(0, 0, 0), rot01 * V3.Cross(dir[1], new V3(dir[1].Z, dir[1].X, dir[1].Y)), 1.1, r * 1.5, mat, DrawingMode.Surfaces));//トーラス
            }
            else
            {
                obj.Add(new Cylinder(n * 1.9, -n * 0.8, r, mat, DrawingMode.Surfaces));//軸
                obj.Add(new Cylinder(-n * 1.9, n * 0.8, r, mat, DrawingMode.Surfaces));//軸
                                                                                       //2ndトーラスの法線は、2nd軸と3rd軸が直交する方向
                obj.Add(new Torus(new V3(0, 0, 0), rot01 * V3.Cross(dir[1], dir[2]), 1.1, r * 1.5, mat, DrawingMode.Surfaces));//トーラス

                //以下、3rd
                mat = new Material(new C4(0.8f, 0f, 0.8f, 1f));
                var rot012 = rot[0] * rot[1] * rot[2];
                n = rot[0] * rot[1] * dir[2];
                obj.Add(new Cylinder(n * 1.3, -n * 2.6, r, mat, DrawingMode.Surfaces));//軸
                obj.Add(new Cone(n * 1.45, -n * 0.2, r * 2, mat, DrawingMode.Surfaces));//矢
                obj.Add(new TextObject(isReciPro ? "Ψ" : "3rd", 12, n * 1.45 + n.Normalized() * 0.01, 0.05, true, mat, gl));

                if (dir[2].Z == 0)
                    obj.Add(new Torus(new V3(0, 0, 0), rot012 * new V3(0, 0, 1), 0.6, r * 1.5, mat, DrawingMode.Surfaces));//トーラス
                else
                    obj.Add(new Torus(new V3(0, 0, 0), rot012 * new V3(0, 1, 0), 0.6, r * 1.5, mat, DrawingMode.Surfaces));//トーラス
            }
        }


        //中央の球
        obj.AddRange(createObject(gl, dir, angle));
        gl.AddObjects(obj);
        gl.Refresh();

    }
    // private void setObject(GLControlAlpha gl, V3[] dir, double[] angle)
    // {
    //     gl.DeleteAllObjects();
    //     gl.AddObjects(createObject(gl, dir, angle));
    //     gl.Refresh();
    // } // (260322Ch) 旧実装: 2 回しか使わない短い helper

    //球体オブジェクトを生成
    private List<GLObject> createObject(GLControlAlpha gl, V3[] dir, double[] angle)
    {
        var (a, b, c) = (FormMain.Crystal.A_Axis.ToOpenTK().Normalized(), FormMain.Crystal.B_Axis.ToOpenTK().Normalized(), FormMain.Crystal.C_Axis.ToOpenTK().Normalized());

        var r = 0.05;
        var obj = new List<GLObject>();

        var rot = Matrix3D.Rot(dir[0], angle[0]);
        if (dir.Length > 1)
            rot *= Matrix3D.Rot(dir[1], angle[1]);
        if (dir.Length > 2)
            rot *= Matrix3D.Rot(dir[2], angle[2]);

        if (checkBoxLink.Checked && gl.Name.Contains("Ex"))
            rot = RotReciPro;

        obj.Add(new Sphere(new V3(0, 0, 0), r * 6, new Material(C4.Gray), DrawingMode.Surfaces));
        var nX = rot * a * 6 * r;
        var nY = rot * b * 6 * r;
        var nZ = rot * c * 6 * r;

        obj.Add(new Sphere(nX, r * 2, new Material(C4.Red), DrawingMode.Surfaces));
        obj.Add(new TextObject("+a", 11, nX + nX.Normalized() * r * 2, r * 2 + 0.01, true, new Material(C4.Red), gl));
        obj.Add(new Sphere(-nX, r * 1.5, new Material(C4.Red), DrawingMode.Surfaces));
        obj.Add(new TextObject("-a", 11, -nX - nX.Normalized() * r * 1.5, r * 1.5 + 0.01, true, new Material(C4.Red), gl));

        obj.Add(new Sphere(nY, r * 2, new Material(C4.Green), DrawingMode.Surfaces));
        obj.Add(new TextObject("+b", 11, nY + nY.Normalized() * r * 2, r * 2 + 0.01, true, new Material(C4.Green), gl));
        obj.Add(new Sphere(-nY, r * 1.5, new Material(C4.Green), DrawingMode.Surfaces));
        obj.Add(new TextObject("-b", 11, -nY - nY.Normalized() * r * 1.5, r * 1.5 + 0.01, true, new Material(C4.Green), gl));

        obj.Add(new Sphere(nZ, r * 2, new Material(C4.Blue), DrawingMode.Surfaces));
        obj.Add(new TextObject("+c", 11, nZ + nZ.Normalized() * r * 1.5, r * 2 + 0.01, true, new Material(C4.Blue), gl));
        obj.Add(new Sphere(-nZ, r * 1.5, new Material(C4.Blue), DrawingMode.Surfaces));
        obj.Add(new TextObject("-c", 11, -nZ - nZ.Normalized() * r * 1.5, r * 1.5 + 0.01, true, new Material(C4.Blue), gl));
        return obj;
    }
    #endregion



    private void ButtonViewIsometric_Click(object sender, EventArgs e)
        => glControlReciProGonio.WorldMatrix = Matrix4d.CreateRotationZ(-Math.PI / 4) * Matrix4d.CreateRotationX(-0.4 * Math.PI);

    private void ButtonViewAlongBeam_Click(object sender, EventArgs e)
        => glControlReciProGonio.WorldMatrix = Matrix4d.Identity;


    /// <summary>ラジオボタンのチェック状態</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RadioButton_CheckedChanged(object sender, EventArgs e)
    {

        if (!(sender as RadioButton).Checked)
            return;
        var name = (sender as RadioButton).Name;

        //260718Cl 統合: 選んだ軸 (X/Y/Z) と同名の下位軸を無効化し、それがチェック中なら次の軸 (X→Y→Z→X) へチェックを移す
        //処理が 1st→2nd と 2nd→3rd で完全に同一だったため共通化。
        static void updateDependentAxis(char axis, RadioButton x, RadioButton y, RadioButton z)
        {
            x.Enabled = axis != 'X';
            y.Enabled = axis != 'Y';
            z.Enabled = axis != 'Z';
            if (axis == 'X' && x.Checked) y.Checked = true;
            else if (axis == 'Y' && y.Checked) z.Checked = true;
            else if (axis == 'Z' && z.Checked) x.Checked = true;
        }

        var axis = name.Contains('X') ? 'X' : name.Contains('Y') ? 'Y' : 'Z';
        if (name.Contains("1st"))
            updateDependentAxis(axis, radioButton2ndX, radioButton2ndY, radioButton2ndZ);
        else if (name.Contains("2nd"))
            updateDependentAxis(axis, radioButton3rdX, radioButton3rdY, radioButton3rdZ);
        SetRotation(false);
    }

    private V3[] getExpDirections()
    {
        //260718Cl 統合: 軸選択 (X 既定 / Y / Z) を X/Y/Z の 3 軸で共通化 (旧: 同じ if/else 連鎖を 3 度展開)
        static V3 axisOf(RadioButton y, RadioButton z) => y.Checked ? new V3(0, 1, 0) : z.Checked ? new V3(0, 0, 1) : new V3(1, 0, 0);
        return
        [
            axisOf(radioButton1stY, radioButton1stZ),
            axisOf(radioButton2ndY, radioButton2ndZ),
            axisOf(radioButton3rdY, radioButton3rdZ),
        ];
    }

    private void GlControlReciProAxes_WorldMatrixChanged(object sender, EventArgs e)
    {
        var matrix = (sender as GLControlAlpha).WorldMatrix;
        var name = (sender as GLControlAlpha).Name;
        var glControls = new[] { glControlReciProGonio, glControlReciProAxes, glControlReciProObjects, glControlExpGonio, glControlExpAxes, glControlExpObjects };

        foreach (var glControl in glControls)
            if (glControl.Name != name)
            {
                glControl.WorldMatrixChanged -= GlControlReciProAxes_WorldMatrixChanged;
                glControl.WorldMatrix = matrix;
                glControl.WorldMatrixChanged += GlControlReciProAxes_WorldMatrixChanged;
            }
    }

    private void NumericBoxExp_ValueChanged(object sender, EventArgs e)
    {
        if (sender is NumericBox box)
        {
            if (box.Value > 180)
            {
                box.Value -= 360;
                return;
            }
            else if (box.Value < -180)
            {
                box.Value += 360;
                return;
            }
        }

        if (skip)
            return;

        if (checkBoxLink.Checked)
        {
            skip = true;
            var (Phi, Theta, Psi) = Euler.FromMatrix(RotExp * RotBase);
            FormMain.SkipEulerChange = true;
            FormMain.Phi = Phi;
            FormMain.Theta = Theta;
            FormMain.Psi = Psi;
            FormMain.SkipEulerChange = false;
            FormMain.SetRotation(RotReciPro);
            skip = false;
        }

        SetRotation(false);
    }

    private void CheckBoxLink_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxLink.Checked)
            RotBase = RotExp.Inverse() * RotReciPro;

        SetRotation(false);
    }

    private void checkBox1st_CheckedChanged(object sender, EventArgs e)
    {
        var check = (CheckBox)sender;
        if (check.Name.Contains("2nd"))
        {
            checkBoxFix2nd.Enabled = numericBoxExp2.Enabled = checkBoxEnable3rd.Enabled = flowLayoutPanelSecondAxis.Enabled = check.Checked;
            if (!check.Checked)
            {
                checkBoxFix2nd.Checked = checkBoxEnable3rd.Checked = checkBoxFix3rd.Checked = false;
                numericBoxExp2.Value = 0;
            }
        }
        else if (check.Name.Contains("3rd"))
        {
            checkBoxFix3rd.Enabled = numericBoxExp3.Enabled = flowLayoutPanelThirdAxis.Enabled = check.Checked;
            if (!check.Checked)
            {
                checkBoxFix3rd.Checked = false;
                numericBoxExp3.Value = 0;
            }
        }
        SetRotation(false);
    }

    private void buttonResetExpEuler_Click(object sender, EventArgs e)
    {
        skip = true;
        numericBoxExp1.Value = numericBoxExp2.Value = numericBoxExp3.Value = 0;
        skip = false;
        NumericBoxExp_ValueChanged(sender, e);
    }


}

