using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class FormPDF : Form
    {
        public FormPDF()
        {
            InitializeComponent();
        }

        public FormPDF(string filename, string title="")
        {
            InitializeComponent();
            // webBrowser.Navigate(filename + "#toolbar=0&navpanes=0");
             webBrowser.Navigate(filename);
            if (title != "")
                this.Text = title;
        }

    }
}
