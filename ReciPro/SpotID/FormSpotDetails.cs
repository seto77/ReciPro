using Crystallography;
using Crystallography.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ReciPro
{
    public partial class FormSpotDetails : Form
    {
        public FormSpotID FormSpotID;
        public int BoxWide => scalablePictureBoxAdvanced.PictureSize.Width;
        public int BoxHeight => scalablePictureBoxAdvanced.PictureSize.Height;

        public FormSpotDetails() => InitializeComponent();

        public void SetData(bool renewImage = true)
        {
            if (renewImage)
                scalablePictureBoxAdvanced.PseudoBitmap = FormSpotID.scalablePictureBoxAdvanced.PseudoBitmap;

            if (FormSpotID.bindingSourceObsSpots.Current == null)
                return;

            var srcValues = scalablePictureBoxAdvanced.PseudoBitmap.SrcValuesGray;

            if (srcValues == null)
                return;

            var pixelWidth = scalablePictureBoxAdvanced.PseudoBitmap.Width;
            int selectedIndex;
            try
            {
                selectedIndex = (int)((DataRowView)FormSpotID.bindingSourceObsSpots.Current).Row["No"];
            }
            catch
            {
                return;
            }

            var (_, _, Range, X0, Y0, H1, H2, Theta, Eta, A, B0, Bx, By, _) = FormSpotID.dataSet.DataTableSpot.GetPrms(selectedIndex);
            var funcs = new List<Marquardt.Function>
            {
                new Marquardt.Function(Marquardt.FuncType.PV2E, X0, Y0, H1, H2, Theta, Eta, A),
                new Marquardt.Function(Marquardt.FuncType.Plane, B0, Bx, By)
            };

            var srcList = new List<(double x, double y, double z)>();
            var calcList = new List<(double x, double y, double z)>();
            var bgList = new List<(double x, double y, double z)>();
            for (int h = 0; h < srcValues.Length / pixelWidth; h++)
                for (int w = 0; w < pixelWidth; w++)
                    if ((w - X0) * (w - X0) + (h - Y0) * (h - Y0) < (Range + 2) * (Range + 2))
                    {
                        srcList.Add((w, h, srcValues[h * pixelWidth + w]));
                        bgList.Add((w, h, funcs[1].GetValue(w, h)));
                        calcList.Add((w, h, funcs[0].GetValue(w, h) + funcs[1].GetValue(w, h)));
                    }

            var src = srcList.AsParallel();
            var calc = calcList.AsParallel();
            var bg = bgList.AsParallel();

            //scalablePictureBoxAdvancedのシンボルとしてN-S方向, W-E方向、などの線をセット
            var sqrt2 = Math.Sqrt(2);
            scalablePictureBoxAdvanced.Symbols = new List<ScalablePictureBox.Symbol>(FormSpotID.scalablePictureBoxAdvanced.Symbols);

            PointD pt1, pt2;
            Profile obsProfile, calcProfile, bgProfile;
            //SW - NE
            pt1 = new PointD(X0 - Range / sqrt2, Y0 + Range / sqrt2);
            pt2 = new PointD(X0 + Range / sqrt2, Y0 - Range / sqrt2);
            scalablePictureBoxAdvanced.Symbols.Add(new ScalablePictureBox.Symbol("", pt1, pt2, Color.OrangeRed));
            obsProfile = new Profile(ImageProcess.GetLineProfile(src, pt1, pt2, 1, 1), Color.Blue);
            calcProfile = new Profile(ImageProcess.GetLineProfile(calc, pt1, pt2, 1, 1), Color.Red);
            bgProfile = new Profile(ImageProcess.GetLineProfile(bg, pt1, pt2, 1, 1), Color.Pink);
            graphControlSWtoNE.ClearProfile();
            graphControlSWtoNE.AddProfiles(new[] { obsProfile, calcProfile, bgProfile });

            //NW-SE
            pt1 = new PointD(X0 - Range / sqrt2, Y0 - Range / sqrt2);
            pt2 = new PointD(X0 + Range / sqrt2, Y0 + Range / sqrt2);
            scalablePictureBoxAdvanced.Symbols.Add(new ScalablePictureBox.Symbol("", pt1, pt2, Color.Purple));
            obsProfile = new Profile(ImageProcess.GetLineProfile(src, pt1, pt2, 1, 1), Color.Blue);
            calcProfile = new Profile(ImageProcess.GetLineProfile(calc, pt1, pt2, 1, 1), Color.Red);
            bgProfile = new Profile(ImageProcess.GetLineProfile(bg, pt1, pt2, 1, 1), Color.Pink);
            graphControlNWtoSE.ClearProfile();
            graphControlNWtoSE.AddProfiles(new[] { obsProfile, calcProfile, bgProfile });

            //N-S
            pt1 = new PointD(X0, Y0 - Range);
            pt2 = new PointD(X0, Y0 + Range);
            scalablePictureBoxAdvanced.Symbols.Add(new ScalablePictureBox.Symbol("", pt1, pt2, Color.Red));
            obsProfile = new Profile(ImageProcess.GetLineProfile(src, pt1, pt2, 1, 1), Color.Blue);
            calcProfile = new Profile(ImageProcess.GetLineProfile(calc, pt1, pt2, 1, 1), Color.Red);
            bgProfile = new Profile(ImageProcess.GetLineProfile(bg, pt1, pt2, 1, 1), Color.Pink);
            graphControlNtoS.ClearProfile();
            graphControlNtoS.AddProfiles(new[] { obsProfile, calcProfile, bgProfile });

            //W-E
            pt1 = new PointD(X0 - Range, Y0);
            pt2 = new PointD(X0 + Range, Y0);
            scalablePictureBoxAdvanced.Symbols.Add(new ScalablePictureBox.Symbol("", pt1, pt2, Color.Orange));
            obsProfile = new Profile(ImageProcess.GetLineProfile(src, pt1, pt2, 1, 1), Color.Blue);
            calcProfile = new Profile(ImageProcess.GetLineProfile(calc, pt1, pt2, 1, 1), Color.Red);
            bgProfile = new Profile(ImageProcess.GetLineProfile(bg, pt1, pt2, 1, 1), Color.Pink);
            graphControlWtoE.ClearProfile();
            graphControlWtoE.AddProfiles(new[] { obsProfile, calcProfile, bgProfile });

            //中心位置や倍率を設定
            scalablePictureBoxAdvanced.ZoomAndCenter = (BoxWide / Range / 4, new PointD(X0, Y0));
        }

        private void FormSpotDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            FormSpotID.checkBoxDetailsSpot.Checked = false;
        }

        private void FormSpotDetails_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
                SetData();
        }
    }
}