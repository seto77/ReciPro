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

    public FormMovieSetting()
    {
        InitializeComponent();
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
                b.ForeColor = (sender as Button).Name == b.Name ? Color.Black : Color.Gray;
    }

}
