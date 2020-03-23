using System;
using System.Windows.Forms;

namespace ReciPro
{
    public partial class FormAboutMe : Form
    {
        public FormAboutMe() => InitializeComponent();

        private void linkLabelHomePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(linkLabelHomePage.Text);
            }
            catch { }
        }

        private void linkLabelMail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("mailto:" + linkLabelMail.Text);
            }
            catch { }
        }

        private void FormAboutMe_Load(object sender, EventArgs e)
        {
            labelVersion.Text = "ReciPro   " + Version.VersionAndDate;

            string str = "";

            str += Version.Introduction + "\r\n\r\n";//ÇÕÇ∂ÇﬂÇ…
            str += Version.CopyRight + "\r\n\r\n";//íòçÏå†
            str += Version.Condition + "\r\n\r\n";//é¿çsèåè
            str += Version.Exemption + "\r\n\r\n";//ñ∆ê”
            str += Version.Adress + "\r\n\r\n";//òAóçêÊ
            str += Version.Acknowledge + "\r\n\r\n";//é”é´
            str += Version.History;//óöó

            textBoxReadMe.Text += str;
        }
    }
}