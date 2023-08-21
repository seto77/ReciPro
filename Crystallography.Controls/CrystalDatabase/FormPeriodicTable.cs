using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class FormPeriodicTable : Form
    {
        public byte[] Includes => button.Where(b => b.BackColor == IncludeColor).Select(b => (byte)((int)b.Tag)).ToArray();
        public string[] IncludesStr => Includes.Select(i => i.ToString("000")).ToArray();

        public byte[] Excludes => button.Where(b => b.BackColor == ExcludeColor).Select(b => (byte)((int)b.Tag)).ToArray();
        public string[] ExcludesStr => Excludes.Select(i => i.ToString("000")).ToArray();


        private readonly List<Button> button = new();

        private readonly Color ExcludeColor = Color.LightCoral;
        private readonly Color IncludeColor = Color.LightBlue;
        private readonly Color NeutralColor = Color.LightYellow;

        public FormPeriodicTable()
        {
            InitializeComponent();

            for (int i = 0; i < 112; i++)
            {
                button.Add(new Button());
                button[i].Size = new Size(0, 0);
                button[i].Tag = i;
            }

            #region テキストの設定

            button[1].Text = "H";
            button[2].Text = "He";
            button[3].Text = "Li";
            button[4].Text = "Be";
            button[5].Text = "B";
            button[6].Text = "C";
            button[7].Text = "N";
            button[8].Text = "O";
            button[9].Text = "F";
            button[10].Text = "Ne";
            button[11].Text = "Na";
            button[12].Text = "Mg";
            button[13].Text = "Al";
            button[14].Text = "Si";
            button[15].Text = "P";
            button[16].Text = "S";
            button[17].Text = "Cl";
            button[18].Text = "Ar";
            button[19].Text = "K";
            button[20].Text = "Ca";
            button[21].Text = "Sc";
            button[22].Text = "Ti";
            button[23].Text = "V";
            button[24].Text = "Cr";
            button[25].Text = "Mn";
            button[26].Text = "Fe";
            button[27].Text = "Co";
            button[28].Text = "Ni";
            button[29].Text = "Cu";
            button[30].Text = "Zn";
            button[31].Text = "Ga";
            button[32].Text = "Ge";
            button[33].Text = "As";
            button[34].Text = "Se";
            button[35].Text = "Br";
            button[36].Text = "Kr";
            button[37].Text = "Rb";
            button[38].Text = "Sr";
            button[39].Text = "Y";
            button[40].Text = "Zr";
            button[41].Text = "Nb";
            button[42].Text = "Mo";
            button[43].Text = "Tc";
            button[44].Text = "Ru";
            button[45].Text = "Rh";
            button[46].Text = "Pd";
            button[47].Text = "Ag";
            button[48].Text = "Cd";
            button[49].Text = "In";
            button[50].Text = "Sn";
            button[51].Text = "Sb";
            button[52].Text = "Te";
            button[53].Text = "I";
            button[54].Text = "Xe";
            button[55].Text = "Cs";
            button[56].Text = "Ba";
            button[57].Text = "La";
            button[58].Text = "Ce";
            button[59].Text = "Pr";
            button[60].Text = "Nd";
            button[61].Text = "Pm";
            button[62].Text = "Sm";
            button[63].Text = "Eu";
            button[64].Text = "Gd";
            button[65].Text = "Tb";
            button[66].Text = "Dy";
            button[67].Text = "Ho";
            button[68].Text = "Er";
            button[69].Text = "Tm";
            button[70].Text = "Yb";
            button[71].Text = "Lu";
            button[72].Text = "Hf";
            button[73].Text = "Ta";
            button[74].Text = "W";
            button[75].Text = "Re";
            button[76].Text = "Os";
            button[77].Text = "Ir";
            button[78].Text = "Pt";
            button[79].Text = "Au";
            button[80].Text = "Hg";
            button[81].Text = "Tl";
            button[82].Text = "Pb";
            button[83].Text = "Bi";
            button[84].Text = "Po";
            button[85].Text = "At";
            button[86].Text = "Rn";
            button[87].Text = "Fr";
            button[88].Text = "Ra";
            button[89].Text = "Ac";
            button[90].Text = "Th";
            button[91].Text = "Pa";
            button[92].Text = "U";
            button[93].Text = "Np";
            button[94].Text = "Pu";
            button[95].Text = "Am";
            button[96].Text = "Cm";
            button[97].Text = "Bk";
            button[98].Text = "Cf";
            button[99].Text = "Es";
            button[100].Text = "Fm";
            button[101].Text = "Md";
            button[102].Text = "No";
            button[103].Text = "Lr";
            button[104].Text = "Rf";
            button[105].Text = "Db";
            button[106].Text = "Sg";
            button[107].Text = "Bh";
            button[108].Text = "Hs";
            button[109].Text = "Mt";
            button[110].Text = "Ds";
            button[111].Text = "Rg";

            #endregion テキストの設定

            for (int i = 1; i < 112; i++)
            {
                button[i].AutoSize = true;
                button[i].AutoSizeMode = AutoSizeMode.GrowAndShrink;
                button[i].Font = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
                if (i != 0)
                {
                    Controls.Add(button[i]);
                    button[i].Click += new EventHandler(button_Click);
                    button[i].BackColor = Color.LightYellow;
                }
            }

            int width = button.Max(e => e.Width);
            int height = button.Max(e => e.Height);
            for (int i = 1; i < 112; i++)
            {
                button[i].AutoSize = false;
                button[i].Size = new Size(width, height);
            }
            buttonLa.Size = buttonAc.Size = new Size(width, height);

            int column = 0;
            button[1].Location = new Point(width * 0, height * column);//H
            button[2].Location = new Point(width * 17, height * column);//He
            for (int i = 3; i <= 4; i++) button[i].Location = new Point((i - 3) * width, height * 1);//Li~Be
            for (int i = 5; i <= 10; i++) button[i].Location = new Point((i - 5 + 12) * width, height * 1);//B~Ne
            for (int i = 11; i <= 12; i++) button[i].Location = new Point((i - 11) * width, height * 2);//Na~Mg
            for (int i = 13; i <= 18; i++) button[i].Location = new Point((i - 13 + 12) * width, height * 2);//Al~Ar
            for (int i = 19; i <= 36; i++) button[i].Location = new Point((i - 19) * width, height * 3);//K~Kr
            for (int i = 37; i <= 54; i++) button[i].Location = new Point((i - 37) * width, height * 4);//Rb~Xe
            for (int i = 55; i <= 56; i++) button[i].Location = new Point((i - 55) * width, height * 5);//Cs~Ba
            buttonLa.Location = new Point(width * 2, height * 5);

            for (int i = 72; i <= 86; i++) button[i].Location = new Point((i - 72 + 3) * width, height * 5);//Hf~Rn

            for (int i = 87; i <= 88; i++) button[i].Location = new Point((i - 87) * width, height * 6);//Rr~Ra
            buttonAc.Location = new Point(width * 2, height * 6);

            for (int i = 104; i <= 111; i++) button[i].Location = new Point((i - 104 + 3) * width, height * 6);//Rf~Rg

            labelLa.Location = new Point(14, height * 7 + 4);
            for (int i = 57; i <= 71; i++) button[i].Location = new Point((i - 57 + 3) * width, height * 7);//La~Lu

            labelAc.Location = new Point(14, height * 8 + 4);
            for (int i = 89; i <= 103; i++) button[i].Location = new Point((i - 89 + 3) * width, height * 8);//Ac~Lr

            this.ClientSize = new Size(18 * width, height * 9 + buttonOK.Height + 5);

            // buttonOK.Location = new Point(button[103].Location.X + width - buttonOK.Width,
            //      button[103].Location.Y + height + 4);

            //Size = new Size(buttonOK.Location.X + buttonOK.Width + 4, buttonOK.Location.Y + buttonOK.Height + 4);


            ;
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor == NeutralColor)
                ((Button)sender).BackColor = IncludeColor;
            else if (((Button)sender).BackColor == IncludeColor)
                ((Button)sender).BackColor = ExcludeColor;
            else
                ((Button)sender).BackColor = NeutralColor;
        }

        private void FormPeriodicTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void buttonMayInclude_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 111; i++)
                button[i].BackColor = NeutralColor;
        }

        private void buttonMustInclude_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 111; i++)
            {
                button[i].BackColor = IncludeColor;
            }
        }

        private void buttonMustExclude_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 111; i++)
                button[i].BackColor = ExcludeColor;
        }

        private void FormPeriodicTable_Load(object sender, EventArgs e)
        {
            //  buttonOK.Location = new Point(button[103].Location.X + button[103].Width - buttonOK.Width,
            //        button[103].Location.Y + button[103].Height + 4);

            // Size = new Size(buttonOK.Location.X + buttonOK.Width + 4, buttonOK.Location.Y + buttonOK.Height + 4);
            //  buttonOK.BringToFront();
        }
    }
}