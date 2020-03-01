using System;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class WaitDlg : Form
    {
        public WaitDlg()
        {
            InitializeComponent();
        }

        public string Version
        {
            set => labelVersion.Text = value;
            get => labelVersion.Text;
        }

        private string[] hint;

        public string[] Hint
        {
            set
            {
                hint = value;
                setToolTips();
            }
            get => hint;
        }

        public bool ShowProgressBar
        {
            set => panelProgresspar.Visible = value;
            get => panelProgresspar.Visible;
        }

        public bool ShowVersion
        {
            set => flowLayoutPanel1.Visible = value;
            get => flowLayoutPanel1.Visible;
        }
        public bool ShowHints
        {
            set => panel1.Visible = value;
            get => panel1.Visible;
        }


        private void setToolTips()
        {
            Random r = new Random();
            if (hint.Length > 0)
                textBox1.Text = hint[r.Next(hint.Length)];
            else
                textBox1.Text = "";
        }

        public bool AutomaricallyClose
        {
            set { checkBoxCloseWindow.Checked = value; }
            get { return checkBoxCloseWindow.Checked; }
        }

        private int currentHintIndex = 0;

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (hint.Length > 0)
            {
                currentHintIndex += 1;
                if (currentHintIndex >= hint.Length)
                    currentHintIndex = 0;
                textBox1.Text = hint[currentHintIndex];
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (hint.Length > 0)
            {
                currentHintIndex -= 1;
                if (currentHintIndex < 0)
                    currentHintIndex = hint.Length - 1;
                textBox1.Text = hint[currentHintIndex];
            }
        }

    }
}