using Crystallography;
using Crystallography.Controls;
using Crystallography.OpenGL;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using C4 = OpenTK.Graphics.Color4;
using V3 = OpenTK.Vector3d;
//using System.Windows.Media.Media3D;

namespace ReciPro
{
    public partial class FormRotationMatrix : Form
    {
        public FormMain FormMain;

        /// <summary>
        ///  R_base  =R_ex ^-1 * R_reci の関係がある.
        /// </summary>
        public Matrix3D RotBase { get; set; } = new Matrix3D();

        public Matrix3D RotReciPro => Euler.SetEulerAngle(FormMain.Phi, FormMain.Theta, FormMain.Psi);

        public Matrix3D RotExp
        {
            get
            {
                var dir = getExpDirections();
                return Matrix3D.Rot(dir[0], numericBoxExp1.RadianValue)
                    * Matrix3D.Rot(dir[1], numericBoxExp2.RadianValue)
                    * Matrix3D.Rot(dir[2], numericBoxExp3.RadianValue);
            }
        }

        public FormRotationMatrix() => InitializeComponent();

        private bool skip = false;

        private void numericBox6_ValueChanged(object sender, EventArgs e)
        {
            if (skip) return;
            skip = true;
            var rotMatrix = new Matrix3D(numericBox11.Value, numericBox21.Value, numericBox31.Value, numericBox12.Value, numericBox22.Value, numericBox32.Value, numericBox13.Value, numericBox23.Value, numericBox33.Value);
            var euler = Euler.GetEulerAngle(rotMatrix);
            numericBoxPhi.Value = euler.Phi / Math.PI * 180;
            numericBoxTheta.Value = euler.Theta / Math.PI * 180;
            numericBoxPsi.Value = euler.Psi / Math.PI * 180;

            skip = false;
        }

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

                double e11, e12, e13, e21, e22, e23, e31, e32, e33;
                if (!double.TryParse(row1[0], out e11)) return;
                if (!double.TryParse(row1[1], out e12)) return;
                if (!double.TryParse(row1[2], out e13)) return;
                if (!double.TryParse(row2[0], out e21)) return;
                if (!double.TryParse(row2[1], out e22)) return;
                if (!double.TryParse(row2[2], out e23)) return;
                if (!double.TryParse(row3[0], out e31)) return;
                if (!double.TryParse(row3[1], out e32)) return;
                if (!double.TryParse(row3[2], out e33)) return;

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

                numericBox6_ValueChanged(sender, e);
            }
        }


        private void FormRotationMatrix_Load(object sender, EventArgs e)
        {
            glControlReciProGonio.WorldMatrix = Matrix4d.CreateRotationZ(-Math.PI / 4) * Matrix4d.CreateRotationX(-0.4 * Math.PI);
            SetRotation();

            numericBoxPhi.TextBoxBackColor = numericBoxExp1.TextBoxBackColor = Color.FromArgb(255, 180, 180, 0);
            numericBoxTheta.TextBoxBackColor = numericBoxExp2.TextBoxBackColor = Color.FromArgb(255, 0, 180, 180);
            numericBoxPsi.TextBoxBackColor = numericBoxExp3.TextBoxBackColor = Color.FromArgb(255, 180, 0, 180);

            numericBoxPhi.TextBoxForeColor = numericBoxExp1.TextBoxForeColor =
            numericBoxTheta.TextBoxForeColor = numericBoxExp2.TextBoxForeColor =
            numericBoxPsi.TextBoxForeColor = numericBoxExp3.TextBoxForeColor = Color.FromArgb(255, 255, 255, 255);
        }

        /// <summary>
        /// 角度をセット. exp=trueの時は、expのオイラー角が入力されたとき
        /// </summary>
        /// <param name="fromExp"></param>
        public void SetRotation(bool fromExp = false)
        {
            if (skip)
                return;
            setRotationMatrix(fromExp);

            var dirReciPro = new[] { new V3(0, 0, 1), new V3(1, 0, 0), new V3(0, 0, 1) };
            var angleReciPro = new[] { FormMain.Phi, FormMain.Theta, FormMain.Psi };
            setGonio(glControlReciProGonio, dirReciPro, angleReciPro);
            setObject(glControlReciProObjects, dirReciPro, angleReciPro);
            setAxes(glControlReciProAxes);

            var dirExp = getExpDirections();
            var angleExp = new[] { numericBoxExp1.RadianValue, numericBoxExp2.RadianValue, numericBoxExp3.RadianValue };
            setGonio(glControlExpGonio, dirExp, angleExp);
            setObject(glControlExpObjects, dirExp, angleExp);
            setAxes(glControlExpAxes);
        }

        private void setRotationMatrix(bool fromExp = false)
        {
            numericBoxPhi.RadianValue = FormMain.Phi;
            numericBoxTheta.RadianValue = FormMain.Theta;
            numericBoxPsi.RadianValue = FormMain.Psi;

            if (skip) return;
            var rotMatrix = Euler.SetEulerAngle(numericBoxPhi.RadianValue, numericBoxTheta.RadianValue, numericBoxPsi.RadianValue);
            skip = true;
            numericBox11.Value = rotMatrix.E11;
            numericBox12.Value = rotMatrix.E12;
            numericBox13.Value = rotMatrix.E13;
            numericBox21.Value = rotMatrix.E21;
            numericBox22.Value = rotMatrix.E22;
            numericBox23.Value = rotMatrix.E23;
            numericBox31.Value = rotMatrix.E31;
            numericBox32.Value = rotMatrix.E32;
            numericBox33.Value = rotMatrix.E33;


            if (checkBoxLink.Checked && fromExp == false)
            {
                var dir = getExpDirections();
                var result = Euler.DecomposeMatrix(RotReciPro * RotBase.Inverse(), dir[0], dir[1], dir[2]);
                numericBoxExp1.RadianValue = result[0];
                numericBoxExp2.RadianValue = result[1];
                numericBoxExp3.RadianValue = result[2];
            }


            skip = false;
        }

        // enum dir { PlusX, MinusX, PlusY, MinusY, PlusZ, MinusZ }

        private void setAxes(GLControlAlpha gl)
        {
            gl.DeleteAllObjects();
            var mat = new Material(C4.White, 0.2, 0.7, 0.8, 50, 0.2);
            var obj = new List<GLObject>();
            var r = 0.065;
            //X軸
            mat.Color = new C4(0.8f, 0f, 0f, 1f);
            obj.Add(new Cylinder(new V3(-1, 0, 0), new V3(2, 0, 0), r, mat, DrawingMode.Surfaces));//軸
            obj.Add(new Cone(new V3(1.1, 0, 0), new V3(-0.2, 0, 0), r * 2, mat, DrawingMode.Surfaces));//矢
            //Y軸
            mat.Color = new C4(0f, 0.6f, 0f, 1f);
            obj.Add(new Cylinder(new V3(0, -1, 0), new V3(0, 2, 0), r, mat, DrawingMode.Surfaces));//軸
            obj.Add(new Cone(new V3(0, 1.1, 0), new V3(0, -0.2, 0), r * 2, mat, DrawingMode.Surfaces));//矢
            //Z軸
            mat.Color = new C4(0f, 0f, 0.8f, 1f);
            obj.Add(new Cylinder(new V3(0, 0, -1), new V3(0, 0, 2), r, mat, DrawingMode.Surfaces));//軸
            obj.Add(new Cone(new V3(0, 0, 1.1), new V3(0, 0, -0.2), r * 2, mat, DrawingMode.Surfaces));//矢

            //中央の球
            mat.Color = C4.Gray;
            obj.Add(new Sphere(new V3(0, 0, 0), r * 2, mat, DrawingMode.Surfaces));

            gl.AddObjects(obj);
            gl.Refresh();

        }
        private void setGonio(GLControlAlpha gl, V3[] dir, double[] angle)
        {
            gl.DeleteAllObjects();

            var r = 0.05;
            var obj = new List<GLObject>();
            var mat = new Material(C4.White, 0.2, 0.7, 0.8, 50, 0.2);

            var rot = new Matrix3D[] { Matrix3D.Rot(dir[0], angle[0]), Matrix3D.Rot(dir[1], angle[1]), Matrix3D.Rot(dir[2], angle[2]) };
            Matrix3D rot01 = rot[0] * rot[1], rot012 = rot01 * rot[2];


            //1st
            mat.Color = new C4(0.8f, 0.8f, 0f, 1f);

            obj.Add(new Cylinder(dir[0] * -1.9, dir[0] * 0.3, r, mat, DrawingMode.Surfaces));//軸
            obj.Add(new Cylinder(dir[0] * 1.6, dir[0] * 0.3, r, mat, DrawingMode.Surfaces));//軸
            obj.Add(new Cone(dir[0] * 2.1, dir[0] * -0.2, r * 2, mat, DrawingMode.Surfaces));//矢
            //1stトーラスの法線は、1st軸と2nd軸が直交する方向
            obj.Add(new Torus(new V3(0, 0, 0), rot[0] * V3.Cross(dir[0], dir[1]), 1.6, r * 1.5, mat, DrawingMode.Surfaces));//トーラス

            //2nd
            mat.Color = new C4(0f, 0.8f, 0.8f, 1f);
            var n = rot[0] * dir[1];

            obj.Add(new Cylinder(n * 1.9, -n * 0.8, r, mat, DrawingMode.Surfaces));//軸
            obj.Add(new Cylinder(-n * 1.9, n * 0.8, r, mat, DrawingMode.Surfaces));//軸
            obj.Add(new Cone(n * 2.0, -n * 0.2, r * 2, mat, DrawingMode.Surfaces));//矢
            //2ndトーラスの法線は、1st軸と2nd軸が直交する方向
            obj.Add(new Torus(new V3(0, 0, 0), rot01 * V3.Cross(dir[1], dir[2]), 1.1, r * 1.5, mat, DrawingMode.Surfaces));//トーラス

            //3rd
            mat.Color = new C4(0.8f, 0f, 0.8f, 1f);
            n = rot01 * dir[2];
            obj.Add(new Cylinder(n * 1.3, -n * 2.6, r, mat, DrawingMode.Surfaces));//軸
            obj.Add(new Cone(n * 1.45, -n * 0.2, r * 2, mat, DrawingMode.Surfaces));//矢
            if (dir[2].Z == 0)
                obj.Add(new Torus(new V3(0, 0, 0), rot012 * new V3(0, 0, 1), 0.6, r * 1.5, mat, DrawingMode.Surfaces));//トーラス
            else
                obj.Add(new Torus(new V3(0, 0, 0), rot012 * new V3(0, 1, 0), 0.6, r * 1.5, mat, DrawingMode.Surfaces));//トーラス

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
        private List<GLObject> createObject(GLControlAlpha gl, V3[] dir, double[] angle)
        {

            var r = 0.05;
            var obj = new List<GLObject>();
            var mat = new Material(C4.Gray, 0.2, 0.7, 0.8, 50, 0.2);

            var rot = Matrix3D.Rot(dir[0], angle[0]) * Matrix3D.Rot(dir[1], angle[1]) * Matrix3D.Rot(dir[2], angle[2]);

            if (checkBoxLink.Checked && gl.Name.Contains("Ex"))
                rot = RotReciPro;

            obj.Add(new Sphere(new V3(0, 0, 0), r * 6, mat, DrawingMode.Surfaces));
            var nX = rot * new V3(r * 6, 0, 0);
            var nY = rot * new V3(0, r * 6, 0);
            var nZ = rot * new V3(0, 0, r * 6);
            mat.Color = C4.Red;
            obj.Add(new Sphere(nX, r * 2, mat, DrawingMode.Surfaces));
            obj.Add(new Sphere(-nX, r * 1.5, mat, DrawingMode.Surfaces));
            mat.Color = C4.Green;
            obj.Add(new Sphere(nY, r * 2, mat, DrawingMode.Surfaces));
            obj.Add(new Sphere(-nY, r * 1.5, mat, DrawingMode.Surfaces));
            mat.Color = C4.Blue;
            obj.Add(new Sphere(nZ, r * 2, mat, DrawingMode.Surfaces));
            obj.Add(new Sphere(-nZ, r * 1.5, mat, DrawingMode.Surfaces));
            return obj;
        }

        private void FormRotationMatrix_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            FormMain.toolStripButtonRotation.Checked = false;
        }

        private void ButtonViewIsometric_Click(object sender, EventArgs e) => glControlReciProGonio.WorldMatrix = Matrix4d.CreateRotationZ(-Math.PI / 4) * Matrix4d.CreateRotationX(-0.4 * Math.PI);

        private void ButtonViewAlongBeam_Click(object sender, EventArgs e) => glControlReciProGonio.WorldMatrix = Matrix4d.Identity;


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
                if (name.Contains("X"))
                {
                    radioButton2ndXminus.Enabled = radioButton2ndXplus.Enabled = false;
                    radioButton2ndYminus.Enabled = radioButton2ndYplus.Enabled = true;
                    radioButton2ndZminus.Enabled = radioButton2ndZplus.Enabled = true;
                    if (radioButton2ndXminus.Checked || radioButton2ndXplus.Checked)
                        radioButton2ndYplus.Checked = true;
                }
                else if (name.Contains("Y"))
                {
                    radioButton2ndXminus.Enabled = radioButton2ndXplus.Enabled = true;
                    radioButton2ndYminus.Enabled = radioButton2ndYplus.Enabled = false;
                    radioButton2ndZminus.Enabled = radioButton2ndZplus.Enabled = true;
                    if (radioButton2ndYminus.Checked || radioButton2ndYplus.Checked)
                        radioButton2ndZplus.Checked = true;
                }
                else
                {
                    radioButton2ndXminus.Enabled = radioButton2ndXplus.Enabled = true;
                    radioButton2ndYminus.Enabled = radioButton2ndYplus.Enabled = true;
                    radioButton2ndZminus.Enabled = radioButton2ndZplus.Enabled = false;
                    if (radioButton2ndZminus.Checked || radioButton2ndZplus.Checked)
                        radioButton2ndXplus.Checked = true;
                }
            }
            else if (name.Contains("2nd"))
            {
                if (name.Contains("X"))
                {
                    radioButton3rdXminus.Enabled = radioButton3rdXplus.Enabled = false;
                    radioButton3rdYminus.Enabled = radioButton3rdYplus.Enabled = true;
                    radioButton3rdZminus.Enabled = radioButton3rdZplus.Enabled = true;
                    if (radioButton3rdXminus.Checked || radioButton3rdXplus.Checked)
                        radioButton3rdYplus.Checked = true;
                }
                else if (name.Contains("Y"))
                {
                    radioButton3rdXminus.Enabled = radioButton3rdXplus.Enabled = true;
                    radioButton3rdYminus.Enabled = radioButton3rdYplus.Enabled = false;
                    radioButton3rdZminus.Enabled = radioButton3rdZplus.Enabled = true;
                    if (radioButton3rdYminus.Checked || radioButton3rdYplus.Checked)
                        radioButton3rdZplus.Checked = true;
                }
                else
                {
                    radioButton3rdXminus.Enabled = radioButton3rdXplus.Enabled = true;
                    radioButton3rdYminus.Enabled = radioButton3rdYplus.Enabled = true;
                    radioButton3rdZminus.Enabled = radioButton3rdZplus.Enabled = false;
                    if (radioButton3rdZminus.Checked || radioButton3rdZplus.Checked)
                        radioButton3rdXplus.Checked = true;
                }
            }
            SetRotation();
        }

        private V3[] getExpDirections()
        {
            var v1 = new V3(1, 0, 0);
            if (radioButton1stXplus.Checked)
                v1 = new V3(1, 0, 0);
            else if (radioButton1stXminus.Checked)
                v1 = new V3(-1, 0, 0);
            else if (radioButton1stYplus.Checked)
                v1 = new V3(0, 1, 0);
            else if (radioButton1stYminus.Checked)
                v1 = new V3(0, -1, 0);
            else if (radioButton1stZplus.Checked)
                v1 = new V3(0, 0, 1);
            else if (radioButton1stZminus.Checked)
                v1 = new V3(0, 0, -1);

            var v2 = new V3(1, 0, 0);
            if (radioButton2ndXplus.Checked)
                v2 = new V3(1, 0, 0);
            else if (radioButton2ndXminus.Checked)
                v2 = new V3(-1, 0, 0);
            else if (radioButton2ndYplus.Checked)
                v2 = new V3(0, 1, 0);
            else if (radioButton2ndYminus.Checked)
                v2 = new V3(0, -1, 0);
            else if (radioButton2ndZplus.Checked)
                v2 = new V3(0, 0, 1);
            else if (radioButton2ndZminus.Checked)
                v2 = new V3(0, 0, -1);

            var v3 = new V3(1, 0, 0);
            if (radioButton3rdXplus.Checked)
                v3 = new V3(1, 0, 0);
            else if (radioButton3rdXminus.Checked)
                v3 = new V3(-1, 0, 0);
            else if (radioButton3rdYplus.Checked)
                v3 = new V3(0, 1, 0);
            else if (radioButton3rdYminus.Checked)
                v3 = new V3(0, -1, 0);
            else if (radioButton3rdZplus.Checked)
                v3 = new V3(0, 0, 1);
            else if (radioButton3rdZminus.Checked)
                v3 = new V3(0, 0, -1);

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
            if (skip)
                return;


            var box = sender as NumericBox;
            if (box.Value > 360)
            {
                box.Value -= 360;
                return;
            }
            else if (box.Value < -360)
            {
                box.Value += 360;
                return;
            }

            if (checkBoxLink.Checked)
            {
                skip = true;
                var euler = Euler.GetEulerAngle(RotExp * RotBase);
                FormMain.SkipEulerChange = true;
                FormMain.Phi = euler.Phi;
                FormMain.Theta = euler.Theta;
                FormMain.SkipEulerChange = false;
                if (FormMain.Psi == euler.Psi)
                    FormMain.SetRotation(RotReciPro);
                else
                    FormMain.Psi = euler.Psi;
                skip = false;
            }

            SetRotation(true);
        }

        private void CheckBoxLink_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBoxLink.Checked)
                RotBase = RotExp.Inverse() * RotReciPro;

            SetRotation(true);


        }
    }
}