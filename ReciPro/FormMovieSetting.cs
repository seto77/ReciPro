using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReciPro;
public partial class FormMovieSetting : Form
{
    public Vector3DBase Direction { get; set; } = new Vector3DBase(0, 1, 0);

    public double Speed => numericBoxSpeed.Value;
    public double Duration => numericBoxDuration.Value;

    private Vector3DBase A = null;
    private Vector3DBase B = null;
    private Vector3DBase C = null;

    private Matrix3D Rot = null;

    public FormMovieSetting()
    {
        InitializeComponent();
        radioButtonAxis.Enabled = false;
        radioButtonPlane.Enabled = false;
    }

    public FormMovieSetting(Vector3DBase a, Vector3DBase b, Vector3DBase c, Matrix3D rot)
    {
        InitializeComponent();
        A = a;
        B = b;
        C = c;
        Rot = rot;
    }

    private void buttonDirection_Click(object sender, EventArgs e)
    {
        Direction = (sender as Button).Name switch
        {
            "buttonTopRight" => new Vector3DBase(-1, 1, 0),
            "buttonRight" => new Vector3DBase(0, 1, 0),
            "buttonBottomRight" => new Vector3DBase(1, 1, 0),
            "buttonBottom" => new Vector3DBase(1, 0, 0),
            "buttonBottomLeft" => new Vector3DBase(1, -1, 0),
            "buttonLeft" => new Vector3DBase(0, -1, 0),
            "buttonTopLeft" => new Vector3DBase(-1, -1, 0),
            "buttonTop" => new Vector3DBase(-1, 0, 0),
            "buttonClock" => new Vector3DBase(0, 0, -1),
            "buttonAntiClock" => new Vector3DBase(0, 0, 1),
            _ => new Vector3DBase(0, 0, 1)
        };
        var buttons = new[] { buttonTopRight, buttonRight, buttonBottomRight, buttonBottom, buttonBottomLeft, buttonLeft, buttonTopLeft, buttonTop, buttonClock, buttonAntiClock };

        foreach (var b in buttons)
                b.ForeColor = (sender as Button).Name == b.Name ? Color.Blue : Color.Gray;
    }

    private void radioButtonCurrent_CheckedChanged(object sender, EventArgs e)
    {
        tableLayoutPanelAxis.Enabled = radioButtonAxis.Checked;
        tableLayoutPanelPlane.Enabled = radioButtonPlane.Checked;
        tableLayoutPanelCurrent.Enabled = radioButtonCurrent.Checked;
    }

    private void numericBoxAxisU_ValueChanged(object sender, EventArgs e)
    {
        Direction = Rot * (numericBoxAxisU.Value * A + numericBoxAxisV.Value * B + numericBoxAxisW.Value * C);
    }

    private void numericBoxPlaneH_ValueChanged(object sender, EventArgs e)
    {
        var rot = new Matrix3D(A, B, C).Inverse();
        Direction = Rot * (numericBoxPlaneH.Value * rot.Row1+ numericBoxPlaneK.Value * rot.Row2 + numericBoxPlaneL.Value * rot.Row3);
    }
}
