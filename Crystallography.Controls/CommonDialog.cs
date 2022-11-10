using System;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class CommonDialog : Form
    {
        #region プロパティ,フィールド

        public (string Text, double Ratio) Progress
        {
            set
            {
                Text = value.Text;
                var progress = (int)(progressBar.Maximum * value.Ratio);
                progressBar.Value = progress < progressBar.Maximum ? progress : progressBar.Maximum ;
                Application.DoEvents();
            }
        }

        public enum DialogModeEnum { Initialize, History, License, Hint }

        public DialogModeEnum DialogMode
        {
            set
            {
                buttonNext.Visible = false;
                flowLayoutPanelSoftwareInformation.Visible = true;
                if (value == DialogModeEnum.Initialize)
                {
                    progressBar.Visible = true;
                    checkBoxCloseWindow.Visible = true;

                    textBox.Visible = false;
                    
                    labelSoftwareAndVersion.Visible = true;

                    ClientSize = new Size(420, progressBar.Height + flowLayoutPanelSoftwareInformation.Height + panelOK.Height);
                }
                else
                {
                    progressBar.Visible = false;
                    checkBoxCloseWindow.Visible = false;
                    textBox.Visible = true;
                    if (value == DialogModeEnum.License)
                    {
                        labelSoftwareAndVersion.Visible = false;
                        Text = Software + " License (MIT)";
                        textBox.Text = License;
                    }
                    else
                    {
                        flowLayoutPanelSoftwareInformation.Visible = false;
                        if (value == DialogModeEnum.History)
                        {
                            Text = "Version history";
                            textBox.Text = History;
                        }

                        else if (value == DialogModeEnum.Hint)
                        {
                            buttonNext.Visible = true;
                            Text = "Hint";
                            setToolTips();
                        }
                    }
                    Size = new Size(400, 200);
                }
            }
        }

        private string software="";// e.g., "ReciPro"　
        public string Software
        {
            get => software;
            set
            {
                software = value;
                labelSoftwareAndVersion.Text = software + "  " + versionAndDate;
            }
        }

        private string versionAndDate="";// e.g., "ver3.456(2020/12/31)"　
        public string VersionAndDate
        {
            get => versionAndDate;
            set
            {
                versionAndDate = value;
                labelSoftwareAndVersion.Text = software + "  " + versionAndDate;

                var year =versionAndDate.Split(new[] { '/', '(' }, StringSplitOptions.RemoveEmptyEntries)[1];
                labelCopyRight.Text = "Copyright(C) 2005-" + year + "   Yusuke Seto";
            }
        }

        public string History { get; set; } = "";

        public string[] Hint { set { hint = value; setToolTips(); } get => hint; }
        private string[] hint;

        public bool AutomaticallyClose { set => checkBoxCloseWindow.Checked = value; get => checkBoxCloseWindow.Checked; }

        private int currentHintIndex = 0;

        /// <summary>
        /// License
        /// </summary>
        static public string License =
            "Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation" +
            " files(the \"Software\"), to deal in the Software without restriction, including without limitation the rights to use, copy," +
            " modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software" +
            " is furnished to do so, subject to the following conditions:\r\n\r\n" +
            "The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.\r\n\r\n" +
            "THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES" +
            " OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE " +
            "LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN " +
            "CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.";


        #endregion

        #region コンストラクタ、ロード、クローズ

        public CommonDialog()
        {
            InitializeComponent();
        }
        private void CommonDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }

        #endregion

        #region ボタンイベント

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
                textBox.Text = hint[currentHintIndex];
            }
        }
        #endregion

        private void setToolTips() => textBox.Text = hint.Length > 0 ? hint[new Random().Next(hint.Length)] : "";

        private void checkBoxCloseWindow_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCloseWindow.Checked)
                Visible = false;
        }
    }
}