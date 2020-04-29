using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class BoundControl : UserControl
    {

        public Crystal Crystal { get; set; } = null;
        public BoundControl()
        {
            InitializeComponent();
        }


        public Bound GetFromInterface()
        {
            return new Bound(Crystal, numericBoxH.ValueInteger, numericBoxK.ValueInteger, numericBoxL.ValueInteger, 
                checkBoxEquivalency.Checked, numericBoxDistance.Value, colorControl.Argb); ;
        }
    }
}
