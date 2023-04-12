using Crystallography.OpenGL;
using OpenTK;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using C4 = OpenTK.Graphics.Color4;
using V3 = OpenTK.Vector3d;

namespace ReciPro;

public partial class FormRotationMatrix : Form
{

    #region プロパティ
    /// <summary>
    ///  R_base  =R_ex ^-1 * R_reci の関係がある.
    /// </summary>
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

    public FormRotationMatrix() => InitializeComponent();

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
    #endregion

    #region コンストラクタ、ロード、クローズ、Visible

    /// <summary>
    /// 起動時
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FormRotationMatrix_Load(object sender, EventArgs e)
    {
        #region glControlの追加 (デザイナが壊れるため)

        // 
        // glControlReciProObjects
        // 
        this.glControlReciProObjects = new GLControlAlpha
        {
            AllowMouseScaling = false,
            AllowMouseTranslating = false,
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            BorderStyle = BorderStyle.Fixed3D,
            Location = new Point(273, 248),
            Margin = new Padding(0),
            Name = "glControlReciProObjects",
            NodeCoefficient = 1,
            ProjWidth = 1D,
            Size = new Size(130, 130),
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
            Location = new Point(273, 114),
            Margin = new Padding(0),
            Name = "glControlReciProAxes",
            NodeCoefficient = 1,
            ProjWidth = 2.8D,
            Size = new Size(130, 130),
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
            Location = new Point(5, 114),
            Margin = new Padding(0),
            Name = "glControlReciProGonio",
            ProjWidth = 5D,
            Size = new Size(264, 264),
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
            Location = new Point(273, 248),
            Margin = new Padding(0),
            Name = "glControlExpObjects",
            ProjWidth = 1D,
            Size = new Size(130, 130),
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
            Location = new Point(273, 114),
            Margin = new System.Windows.Forms.Padding(0),
            Name = "glControlExpAxes",
            ProjWidth = 2.8D,
            Size = new Size(130, 130),
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
            Location = new Point(5, 114),
            Margin = new Padding(0),
            Name = "glControlExpGonio",
            ProjWidth = 5D,
            Size = new Size(264, 264),
        };
        this.glControlExpGonio.WorldMatrixChanged += new System.EventHandler(this.GlControlReciProAxes_WorldMatrixChanged);


        groupBox2.Controls.Add(glControlExpObjects);
        groupBox2.Controls.Add(glControlExpAxes);
        groupBox2.Controls.Add(glControlExpGonio);

        groupBox1.Controls.Add(glControlReciProObjects);
        groupBox1.Controls.Add(glControlReciProAxes);
        groupBox1.Controls.Add(glControlReciProGonio);

        #endregion
        glControlReciProGonio.WorldMatrix = Matrix4d.CreateRotationZ(-Math.PI / 4) * Matrix4d.CreateRotationX(-0.4 * Math.PI);
        SetRotation();

        numericBoxPhi.TextBoxBackColor = numericBoxExp1.TextBoxBackColor = Color.FromArgb(255, 180, 180, 0);
        numericBoxTheta.TextBoxBackColor = numericBoxExp2.TextBoxBackColor = Color.FromArgb(255, 0, 180, 180);
        numericBoxPsi.TextBoxBackColor = numericBoxExp3.TextBoxBackColor = Color.FromArgb(255, 180, 0, 180);

        numericBoxPhi.TextBoxForeColor = numericBoxExp1.TextBoxForeColor =
        numericBoxTheta.TextBoxForeColor = numericBoxExp2.TextBoxForeColor =
        numericBoxPsi.TextBoxForeColor = numericBoxExp3.TextBoxForeColor = Color.FromArgb(255, 255, 255, 255);
    }

    private void FormRotationMatrix_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        FormMain.toolStripButtonRotation.Checked = false;
        this.Visible = false;
    }

    private void FormRotationMatrix_VisibleChanged(object sender, EventArgs e)
    {
        FormMain.toolStripButtonRotation.Checked = true;
    }

    #endregion

    #region Excelのコピーペースト
    private void buttonCopy_Click(object sender, EventArgs e)
    {
        var str =
            numericBox11.Value.ToString() + "\t" + numericBox12.Value.ToString() + "\t" + numericBox13.Value.ToString() + "\n" +
            numericBox21.Value.ToString() + "\t" + numericBox22.Value.ToString() + "\t" + numericBox23.Value.ToString() + "\n" +
            numericBox31.Value.ToString() + "\t" + numericBox32.Value.ToString() + "\t" + numericBox33.Value.ToString();
        Clipboard.SetDataObject(str);
    }

    private void buttonPaste_Click(object sender, EventArgs e)
    {
        if (Clipboard.GetDataObject().GetDataPresent(typeof(string)))
        {
            var data = Clipboard.GetDataObject();
            var str = (string)data.GetData(typeof(string).ToString(), true);

            var str1 = str.Split(new[] { "\r\n" }, StringSplitOptions.None);
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

        var angles = Euler.DecomposeMatrix2(rot * RotBase.Inverse(), settings.ToArray());
        skip = true;
        numericBoxExp1.RadianValue = angles[0];
        if (checkBoxEnable2nd.Checked)
            numericBoxExp2.RadianValue = angles[1];
        if (checkBoxEnable3rd.Checked)
            numericBoxExp3.RadianValue = angles[2];
        skip = false;
        NumericBoxExp_ValueChanged(new object(), new EventArgs());
    }

    /// <summary>
    /// 角度をセット. 
    /// </summary>
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

        if (renewExpEuler && Linked)
        {
            skip = true;
            checkBoxEnable2nd.Checked = checkBoxEnable3rd.Checked = true;
            checkBoxFix1st.Checked = checkBoxFix2nd.Checked = checkBoxFix3rd.Checked = false;
            var settings = new List<(V3 Vec, double Angle, bool Variable)>();
            var dir = getExpDirections();
            settings.Add((dir[0], numericBoxExp1.RadianValue, !checkBoxFix1st.Checked));
            settings.Add((dir[1], numericBoxExp2.RadianValue, !checkBoxFix2nd.Checked));
            settings.Add((dir[2], numericBoxExp3.RadianValue, !checkBoxFix3rd.Checked));

            var angles = Euler.DecomposeMatrix2(RotReciPro * RotBase.Inverse(), settings.ToArray());
            numericBoxExp1.RadianValue = angles[0];
            numericBoxExp2.RadianValue = angles[1];
            numericBoxExp3.RadianValue = angles[2];
            skip = false;



        }
        //ReciPro coordinatesの描画
        var dirReciPro = new[] { new V3(0, 0, 1), new V3(1, 0, 0), new V3(0, 0, 1) };
        var angleReciPro = new[] { FormMain.Phi, FormMain.Theta, FormMain.Psi };
        setGonio(glControlReciProGonio, dirReciPro, angleReciPro);
        setObject(glControlReciProObjects, dirReciPro, angleReciPro);
        setAxes(glControlReciProAxes);

        //Experimetal coordinatesの描画
        var dirExp = getExpDirections();
        var angleExp = new[] { numericBoxExp1.RadianValue, numericBoxExp2.RadianValue, numericBoxExp3.RadianValue };
        setGonio(glControlExpGonio, dirExp, angleExp);
        setObject(glControlExpObjects, dirExp, angleExp);
        setAxes(glControlExpAxes);
    }

    #region OpenGL

    /// <summary>
    /// 軸オブジェクトを生成
    /// </summary>
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
        obj.Add(new TextObject("X", 11, new V3(1.25, 0, 0), 0, true, new Material(c)));

        //Y軸
        c = new C4(0.5f, 0.7f, 0.5f, 1f);
        obj.Add(new Cylinder(new V3(0, -1, 0), new V3(0, 2, 0), r, new Material(c), DrawingMode.Surfaces));//軸
        obj.Add(new Cone(new V3(0, 1.1, 0), new V3(0, -0.2, 0), r * 2, new Material(c), DrawingMode.Surfaces));//矢
        obj.Add(new TextObject("Y", 11, new V3(0, 1.25, 0), 0, true, new Material(c)));

        //Z軸
        c = new C4(0.5f, 0.5f, 0.7f, 1f);
        obj.Add(new Cylinder(new V3(0, 0, -1), new V3(0, 0, 2), r, new Material(c), DrawingMode.Surfaces));//軸
        obj.Add(new Cone(new V3(0, 0, 1.1), new V3(0, 0, -0.2), r * 2, new Material(c), DrawingMode.Surfaces));//矢
        obj.Add(new TextObject("Z", 11, new V3(0, 0, 1.25), 0, true, new Material(c)));

        //中央の球
        obj.Add(new Sphere(new V3(0, 0, 0), r * 2, new Material(C4.Gray), DrawingMode.Surfaces));

        gl.AddObjects(obj);
        gl.Refresh();

    }

    /// <summary>
    /// ゴニオオブジェクトを生成
    /// </summary>
    /// <param name="gl"></param>
    /// <param name="dir"></param>
    /// <param name="angle"></param>
    private void setGonio(GLControlAlpha gl, V3[] dir, double[] angle)
    {
        gl.DeleteAllObjects();

        var r = 0.05;
        var obj = new List<GLObject>();

        var rot = dir.Select((d, i) => Matrix3D.Rot(d, angle[i])).ToArray();

        //1st
        var mat = new Material(new C4(0.8f, 0.8f, 0f, 1f));
        obj.Add(new Cone(dir[0] * 2.1, dir[0] * -0.2, r * 2, mat, DrawingMode.Surfaces));//矢
        obj.Add(new TextObject(gl.Name.Contains("ReciPro") ? "Φ" : "1st", 12, dir[0] * 2.1 + dir[0].Normalized() * 0.01, 0.05, true, mat));

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
            obj.Add(new TextObject(gl.Name.Contains("ReciPro") ? "Θ" : "2nd", 12, n * 2.0 + n.Normalized() * 0.01, 0.05, true, mat));

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
                obj.Add(new TextObject(gl.Name.Contains("ReciPro") ? "Ψ" : "3rd", 12, n * 1.45 + n.Normalized() * 0.01, 0.05, true, mat));

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


    private void setObject(GLControlAlpha gl, V3[] dir, double[] angle)
    {
        gl.DeleteAllObjects();
        gl.AddObjects(createObject(gl, dir, angle));
        gl.Refresh();
    }

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
        obj.Add(new TextObject("+a", 11, nX + nX.Normalized() * r * 2, r * 2 + 0.01, true, new Material(C4.Red)));
        obj.Add(new Sphere(-nX, r * 1.5, new Material(C4.Red), DrawingMode.Surfaces));
        obj.Add(new TextObject("-a", 11, -nX - nX.Normalized() * r * 1.5, r * 1.5 + 0.01, true, new Material(C4.Red)));

        obj.Add(new Sphere(nY, r * 2, new Material(C4.Green), DrawingMode.Surfaces));
        obj.Add(new TextObject("+b", 11, nY + nY.Normalized() * r * 2, r * 2 + 0.01, true, new Material(C4.Green)));
        obj.Add(new Sphere(-nY, r * 1.5, new Material(C4.Green), DrawingMode.Surfaces));
        obj.Add(new TextObject("-b", 11, -nY - nY.Normalized() * r * 1.5, r * 1.5 + 0.01, true, new Material(C4.Green)));

        obj.Add(new Sphere(nZ, r * 2, new Material(C4.Blue), DrawingMode.Surfaces));
        obj.Add(new TextObject("+c", 11, nZ + nZ.Normalized() * r * 1.5, r * 2 + 0.01, true, new Material(C4.Blue)));
        obj.Add(new Sphere(-nZ, r * 1.5, new Material(C4.Blue), DrawingMode.Surfaces));
        obj.Add(new TextObject("-c", 11, -nZ - nZ.Normalized() * r * 1.5, r * 1.5 + 0.01, true, new Material(C4.Blue)));
        return obj;
    }
    #endregion

  

    private void ButtonViewIsometric_Click(object sender, EventArgs e)
        => glControlReciProGonio.WorldMatrix = Matrix4d.CreateRotationZ(-Math.PI / 4) * Matrix4d.CreateRotationX(-0.4 * Math.PI);

    private void ButtonViewAlongBeam_Click(object sender, EventArgs e)
        => glControlReciProGonio.WorldMatrix = Matrix4d.Identity;


    /// <summary>
    /// ラジオボタンのチェック状態
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RadioButton_CheckedChanged(object sender, EventArgs e)
    {

        if (!(sender as RadioButton).Checked)
            return;
        var name = (sender as RadioButton).Name;
        if (name.Contains("1st"))
        {
            if (name.Contains('X'))
            {
                radioButton2ndX.Enabled = false;
                radioButton2ndY.Enabled = true;
                radioButton2ndZ.Enabled = true;
                if (radioButton2ndX.Checked)
                    radioButton2ndY.Checked = true;
            }
            else if (name.Contains('Y'))
            {
                radioButton2ndX.Enabled = true;
                radioButton2ndY.Enabled = false;
                radioButton2ndZ.Enabled = true;
                if (radioButton2ndY.Checked)
                    radioButton2ndZ.Checked = true;
            }
            else
            {
                radioButton2ndX.Enabled = true;
                radioButton2ndY.Enabled = true;
                radioButton2ndZ.Enabled = false;
                if (radioButton2ndZ.Checked)
                    radioButton2ndX.Checked = true;
            }
        }
        else if (name.Contains("2nd"))
        {
            if (name.Contains('X'))
            {
                radioButton3rdX.Enabled = false;
                radioButton3rdY.Enabled = true;
                radioButton3rdZ.Enabled = true;
                if (radioButton3rdX.Checked)
                    radioButton3rdY.Checked = true;
            }
            else if (name.Contains('Y'))
            {
                radioButton3rdX.Enabled = true;
                radioButton3rdY.Enabled = false;
                radioButton3rdZ.Enabled = true;
                if (radioButton3rdY.Checked)
                    radioButton3rdZ.Checked = true;
            }
            else
            {
                radioButton3rdX.Enabled = true;
                radioButton3rdY.Enabled = true;
                radioButton3rdZ.Enabled = false;
                if (radioButton3rdZ.Checked)
                    radioButton3rdX.Checked = true;
            }
        }
        SetRotation(false);
    }

    private V3[] getExpDirections()
    {
        var v1 = new V3(1, 0, 0);
        if (radioButton1stY.Checked)
            v1 = new V3(0, 1, 0);
        else if (radioButton1stZ.Checked)
            v1 = new V3(0, 0, 1);

        var v2 = new V3(1, 0, 0);
        if (radioButton2ndY.Checked)
            v2 = new V3(0, 1, 0);
        else if (radioButton2ndZ.Checked)
            v2 = new V3(0, 0, 1);

        var v3 = new V3(1, 0, 0);
        if (radioButton3rdY.Checked)
            v3 = new V3(0, 1, 0);
        else if (radioButton3rdZ.Checked)
            v3 = new V3(0, 0, 1);

        return new[] { v1, v2, v3 };
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
            checkBoxFix2nd.Enabled = numericBoxExp2.Enabled = checkBoxEnable3rd.Enabled = flowLayoutPanel2.Enabled = check.Checked;
            if (!check.Checked)
            {
                checkBoxFix2nd.Checked = checkBoxEnable3rd.Checked = checkBoxFix3rd.Checked = false;
                numericBoxExp2.Value = 0;
            }
        }
        else if (check.Name.Contains("3rd"))
        {
            checkBoxFix3rd.Enabled = numericBoxExp3.Enabled = flowLayoutPanel3.Enabled = check.Checked;
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
