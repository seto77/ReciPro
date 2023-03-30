using MathNet.Numerics.Integration;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Crystallography.AtomStatic;
using static IronPython.Modules._ast;
using static OpenTK.Graphics.OpenGL.GL;
using static System.Math;

namespace ReciPro;

public partial class FormCTF : Form
{
    public FormImageSimulator FormImageSimulator;

    public double Cc => FormImageSimulator.Cc;
    public double Cs => FormImageSimulator.Cs;
    public double Δf => FormImageSimulator.Defocus;
    public double DeltaV => FormImageSimulator.DeltaVol;
    public double V => FormImageSimulator.AccVol;
    public double Beta => FormImageSimulator.Beta;
    public double Lambda => FormImageSimulator.Lambda;
    public double SigmaS => FormImageSimulator.SourceSizeSigma;

    public double Convergence => FormImageSimulator.ConvergenceAngle;

    public double ObjAperRadius => FormImageSimulator.ObjAperRadius;

    public FormImageSimulator.ImageModes ImageMode => FormImageSimulator.ImageMode;

    private const double Pi2 = PI * PI;

    public FormCTF()
    {
        InitializeComponent();
    }

    #region コントラスト伝達描画関連
    private readonly Color colorSinW = Color.FromArgb(0, 0, 255), colorCosW = Color.FromArgb(128, 0, 255),
        colorEs = Color.Green, colorEc = Color.Red, colorPCTF = Color.FromArgb(0, 64, 128), colorACTF = Color.FromArgb(64, 0, 64);
    /// <summary>
    /// レンズの各種関数のグラフを描画
    /// </summary>
    public void Renew()
    {
        if (Visible == false)
            return;

        SetControl();

        double lambda2 = Lambda * Lambda;

        var delta = Cc * DeltaV / V;
        var πλδ2 = Pi2 * lambda2 * delta * delta;

        var profiles = new List<Profile>();

        if (ImageMode == FormImageSimulator.ImageModes.HRTEM || (ImageMode == FormImageSimulator.ImageModes.STEM && radioButtonCTF_coherent.Checked))
        {
            List<PointD> sinW = new(), cosW = new(), es = new(), ec = new(), pctf = new(), actf = new();

            var limit = ImageMode == FormImageSimulator.ImageModes.HRTEM ?
                2 * Sin(ObjAperRadius / 2) / Lambda :
                2 * Sin(Convergence / 2) / Lambda;
            if (double.IsNaN(limit))
                limit = numericBoxMaxU1.Value;

            var uStep = 0.002;
            for (double u = 0; u < numericBoxMaxU1.Value; u += uStep)
            {
                var u2 = u * u;
                sinW.Add(new PointD(u, Sin(PI * Lambda * u2 * (Cs * lambda2 * u2 / 2.0 + Δf))));//球面収差
                cosW.Add(new PointD(u, Cos(PI * Lambda * u2 * (Cs * lambda2 * u2 / 2.0 + Δf))));//球面収差

                if (ImageMode == FormImageSimulator.ImageModes.HRTEM)
                    es.Add(new PointD(u, Exp(-Pi2 * Beta * Beta * u2 * (Δf + lambda2 * Cs * u2) * (Δf + lambda2 * Cs * u2))));//空間的インコヒーレンス(HRTEM)
                else
                    es.Add(new PointD(u, Exp(-2 * Pi2 * SigmaS * SigmaS * u2)));//空間的インコヒーレンス(STEM)

                ec.Add(new PointD(u, Exp(-πλδ2 * u2 * u2 / 2)));//時間的インコヒーレンス
                if (u < limit)
                {
                    pctf.Add(new PointD(u, sinW[^1].Y * es[^1].Y * ec[^1].Y));
                    actf.Add(new PointD(u, cosW[^1].Y * es[^1].Y * ec[^1].Y));
                }
                else
                {
                    pctf.Add(new PointD(u, 0));
                    actf.Add(new PointD(u, 0));
                }

            }
            if (checkBoxSinW.Checked) profiles.Add(new Profile(sinW, colorSinW) { LineWidth = 2 });
            if (checkBoxCosW.Checked) profiles.Add(new Profile(cosW, colorCosW) { LineWidth = 2 });
            if (ImageMode == FormImageSimulator.ImageModes.HRTEM && checkBoxEs_HRTEM.Checked) profiles.Add(new Profile(es, colorEs) { LineWidth = 2 });
            if (ImageMode == FormImageSimulator.ImageModes.STEM && checkBoxEs_STEM.Checked) profiles.Add(new Profile(es, colorEs) { LineWidth = 2 });
            if (checkBoxEc.Checked) profiles.Add(new Profile(ec, colorEc) { LineWidth = 2 });
            if (checkBoxPCTF.Checked) profiles.Add(new Profile(pctf, colorPCTF) { LineWidth = 3 });
            if (checkBoxACTF.Checked) profiles.Add(new Profile(actf, colorACTF) { LineWidth = 3 });
        }
        else if (ImageMode == FormImageSimulator.ImageModes.STEM && radioButtonCTF_Incoherent.Checked)
        {
            double r = Sin(Convergence) / Lambda, r2 = r * r;

            //レンズ関数
            var coeff1 = -PI * Complex.ImaginaryOne * Lambda;
            var coeff2 = Cs * lambda2 / 2;
            var coeff3 = PI * Lambda * delta;
            Complex Lenz(double k2, double kq2)
            {
                //大塚さんの資料「瀬戸先生・質問3への回答(瀬戸追記).docx」を参照
                //球面収差の部分
                //var tmp1 = -PI * Complex.ImaginaryOne * Lambda * ((k2 - kq2) * Δf + Cs * lambda2 * (k2 * k2 - kq2 * kq2) / 2);
                var tmp1 = coeff1 * (k2 - kq2) * (Δf + coeff2 * (k2 + kq2));
                //色収差の部分
                //var tmp2 = Math.PI * Lambda * delta * (kq2 - k2);
                var tmp2 = coeff3 * (kq2 - k2);
                return Complex.Exp(tmp1 - tmp2 * tmp2);
            }

            //絞り内の空間を192x192に分割
            var div = 192.0;
            var k = new List<(double x, double y2, double k2)>((int)(div * div));
            for (int _x = 0; _x < div; _x++)
                for (int _y = 0; _y < div; _y++)
                {
                    var (x, y) = ((_x - div / 2.0 + 0.5) / (div / 2.0 - 0.5) * r, (_y - div / 2.0 + 0.5) / (div / 2.0 - 0.5) * r);
                    if (x * x + y * y < r2)
                        k.Add((x, y * y, x * x + y * y));
                }

            var uStep = 0.01;
            var pts = new PointD[(int)(Min(numericBoxMaxU1.Value, r * 2.4) / uStep)];
            Parallel.For(0, pts.Length, i =>
            {
                var u = i * uStep;
                Complex result = 0;
                foreach (var (x, y2, k2) in CollectionsMarshal.AsSpan(k))
                {
                    var ku2 = (x - u) * (x - u) + y2;
                    if (ku2 < r2)
                        result += Lenz(k2, ku2);
                }
                result *= Exp(-2 * Pi2 * SigmaS * SigmaS * u * u) * (r2 / div / div);
                pts[i] = new PointD(u, result.Magnitude);
            });

            profiles.Add(new Profile(pts, colorSinW) { LineWidth = 3 });
        }

        graphControl.ClearProfile();
        graphControl.UseLineWidth = false;
        graphControl.AddProfiles(profiles.ToArray());
        graphControl.MinimalX = graphControl.LowerX = 0;
        graphControl.Draw();
    }

    /// <summary>
    /// グラフのコピーボタン。エクセルに張り付けられるように
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonCopyGraph_Click(object sender, EventArgs e)
    {
        var p = graphControl.ProfileList;
        if (p.Length > 0)
        {
            var sb = new StringBuilder();
            if (ImageMode == FormImageSimulator.ImageModes.STEM && radioButtonCTF_Incoherent.Checked)
            {
                sb.Append("|u|\tCTFI\r\n");
            }
            else
            {
                sb.Append("|u|");
                if (checkBoxSinW.Checked) sb.Append("\tSin[W(u)]");
                if (checkBoxCosW.Checked) sb.Append("\tCos[W(u)]");
                if (ImageMode == FormImageSimulator.ImageModes.HRTEM && checkBoxEs_HRTEM.Checked) sb.Append("\tEs(u)");
                if (ImageMode == FormImageSimulator.ImageModes.STEM && checkBoxEs_STEM.Checked) sb.Append("\tEs(u)");
                if (checkBoxEc.Checked) sb.Append("\tEc(u)");
                if (checkBoxPCTF.Checked) sb.Append("\tPCTF");
                if (checkBoxACTF.Checked) sb.Append("\tACTF");
                sb.Append("\r\n");
            }

            for (int i = 0; i < p[0].Pt.Count; i++)
            {
                sb.Append(p[0].Pt[i].X);
                for (int j = 0; j < p.Length; j++)
                    sb.Append($"\t{p[j].Pt[i].Y.ToString()}");
                sb.Append("\r\n");
            }
            Clipboard.SetDataObject(sb.ToString());
        }
    }
    #endregion

    private void FormCTF_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        FormImageSimulator.CTFVisible = false;
    }

    private void FormCTF_VisibleChanged(object sender, EventArgs e)
    {
        if (Visible)
            Renew();
    }

    private void numericBoxMaxU1_ValueChanged(object sender, EventArgs e)
    {
        Renew();
    }

    private void radioButtonCTF_coherent_CheckedChanged(object sender, EventArgs e)
    {
        Renew();
    }

    public void SetControl()
    {
        SuspendLayout();
        if (ImageMode == FormImageSimulator.ImageModes.HRTEM)
        {
            flowLayoutPanelSTEM.Visible = false;

            pictureBoxSTEM_CTFI.Visible = false;

            checkBoxEc.Visible = checkBoxSinW.Visible = checkBoxCosW.Visible = checkBoxPCTF.Visible = checkBoxACTF.Visible = true;

            checkBoxEs_STEM.Visible = false;
            checkBoxEs_HRTEM.Visible = true;

            pictureBoxA_STEM.Visible = false;
            pictureBoxA_HRTEM.Visible = true;
        }
        else if (ImageMode == FormImageSimulator.ImageModes.STEM)
        {
            flowLayoutPanelSTEM.Visible = true;

            if (radioButtonCTF_coherent.Checked)
            {

                pictureBoxSTEM_CTFI.Visible = false;

                checkBoxEc.Visible = checkBoxSinW.Visible = checkBoxCosW.Visible = checkBoxPCTF.Visible = checkBoxACTF.Visible = true;

                checkBoxEs_STEM.Visible = true;
                checkBoxEs_HRTEM.Visible = false;

                pictureBoxA_STEM.Visible = true;
                pictureBoxA_HRTEM.Visible = false;

            }
            else
            {
                pictureBoxSTEM_CTFI.Visible = true;

                checkBoxEc.Visible = checkBoxSinW.Visible = checkBoxCosW.Visible = checkBoxPCTF.Visible = checkBoxACTF.Visible = false;

                checkBoxEs_STEM.Visible = false;
                checkBoxEs_HRTEM.Visible = false;

                pictureBoxA_STEM.Visible = false;
                pictureBoxA_HRTEM.Visible = false;
            }
        }
        ResumeLayout(true);
    }

    private void checkBoxSinW_CheckedChanged(object sender, EventArgs e)
    {
        Renew();
    }
}
