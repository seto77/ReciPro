using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class WaveLengthControl : UserControl
    {

        public event EventHandler WavelengthChanged;
        public event EventHandler WaveSourceChanged;

        #region プロパティ

        /// <summary>
        /// VisualStudioデザイナーの編集の時はTrue
        /// </summary>
        public new bool DesignMode
        {
            get
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                    return true;
                Control ctrl = this;
                while (ctrl != null)
                {
                    if (ctrl.Site != null && ctrl.Site.DesignMode)
                        return true;
                    ctrl = ctrl.Parent;
                }
                return false;
            }
        }

        public Font TextFont
        {
            set
            {
                numericalTextBoxEnergy.TextFont = value;
                numericalTextBoxWaveLength.TextFont = value;
            }
            get { return numericalTextBoxWaveLength.TextFont; }
        }

        public bool showWaveSource = true;

        /// <summary>
        /// WaveSourceを表示するかどうか
        /// </summary>
        public bool ShowWaveSource
        {
            set { showWaveSource = flowLayoutPanelWaveSource.Visible = value; }
            get { return showWaveSource; }
        }

        public string waveLengthText = "0.4";

        /// <summary>
        /// 波長をÅ単位のテキスト形式で取得/設定
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string WaveLengthText
        {
            set
            {
                try
                {
                    numericalTextBoxWaveLength.Value = Convert.ToDouble(value);
                    comboBoxXRayElement.SelectedIndex = 0;
                }
                catch
                {
                }
            }
            get
            {
                return numericalTextBoxWaveLength.Text;
            }
        }

        /// <summary>
        /// 波長をnm単位のdoubleで取得/設定
        /// </summary>
        public double WaveLength
        {
            set
            {
                if (value > 0)
                {
                    skipEvent = true;
                    if (radioButtonXray.Checked)
                        comboBoxXRayElement.SelectedIndex = 0;
                    skipEvent = false;

                    numericalTextBoxWaveLength.Value = value * 10.0;
                }
            }
            get=>numericalTextBoxWaveLength.Value / 10.0;
        }

        /// <summary>
        /// 線源を取得/設定
        /// </summary>
        public WaveSource WaveSource
        {
            set
            {
                if (value == WaveSource.Xray)
                    radioButtonXray.Checked = true;
                else if (value == WaveSource.Electron)
                    radioButtonElectron.Checked = true;
                else if (value == WaveSource.Neutron)
                    radioButtonNeutron.Checked = true;
            }
            get
            {
                if (radioButtonXray.Checked)
                    return WaveSource.Xray;
                else if (radioButtonElectron.Checked)
                    return WaveSource.Electron;
                else // (radioButtonNeutron.Checked)
                    return WaveSource.Neutron;
            }
        }

        private int _XrayWaveSourceElementNumber = 0;

        /// <summary>
        /// X線の線源の元素を取得/設定
        /// </summary>
        public int XrayWaveSourceElementNumber
        {
            set
            {
                if (value < comboBoxXRayElement.Items.Count && value >= 0)
                {
                    comboBoxXRayElement.SelectedIndex = value;
                    _XrayWaveSourceElementNumber = value;
                }
            }
            get
            {

                if (InvokeRequired)
                    return (int)Invoke(new Func<int>(() => XrayWaveSourceElementNumber), null);
                else
                    return comboBoxXRayElement.SelectedIndex;
            }
        }

        /// <summary>
        /// X線の線源のLineを取得/設定
        /// </summary>
        public XrayLine XrayWaveSourceLine
        {
            set => comboBoxXrayLine.SelectedItem = value; 
            get
            {
                if (comboBoxXrayLine.SelectedItem == null)
                    return XrayLine.Ka1;
                else
                    return (XrayLine)comboBoxXrayLine.SelectedItem;
            }
        }

        /// <summary>
        /// 線源のエネルギー (kV)を取得/設定
        /// X線と電子は単位はkev,中性子はmev
        /// </summary>
        public double Energy
        {
            set
            {
                if (value > 0)
                    numericalTextBoxEnergy.Value = value;
            }
            get
            {
                return numericalTextBoxEnergy.Value;
            }
        }


      
        /// <summary>
        /// 電子線加速電圧(kV)をテキスト形式で取得/設定
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EnergyText
        {
            set
            {
                try
                {
                    numericalTextBoxEnergy.Value = Convert.ToDouble(value);
                }
                catch { }
            }
            get
            {
                return numericalTextBoxEnergy.Value.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public WaveProperty Property
        {
            set
            {
                if (value == null)
                    return;

                WaveSource = value.Source;
                Energy = value.Energy;
                XrayWaveSourceLine = value.XrayLine;
                XrayWaveSourceElementNumber = value.XrayElementNumber;
                if (WaveSource == Crystallography.WaveSource.Xray && XrayWaveSourceElementNumber == 0)
                    WaveLength = value.WaveLength;
            }
            get
            {
                return new WaveProperty(WaveSource, WaveLength, XrayWaveSourceElementNumber, XrayWaveSourceLine, Energy);
            }
        }

        #endregion プロパティ

        public WaveLengthControl()

        {
            InitializeComponent();
            if (DesignMode)
                return;

            comboBoxXRayElement.SelectedIndex = 0;
        }

        /// <summary>
        /// X線のElementが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxXRayElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxXRayElement.SelectedIndex == 0)//Customが選択されたとき
            {
                comboBoxXrayLine.Visible = false;
                comboBoxXRayElement.Width = 100;
                flowLayoutPanelEnergy.Enabled = true;
                numericalTextBoxWaveLength.Enabled = true;
            }
            else
            {
                comboBoxXRayElement.Width = 70;
                comboBoxXrayLine.Visible = true;
                flowLayoutPanelEnergy.Enabled = false;

                numericalTextBoxWaveLength.Enabled = false;

                comboBoxXrayLine.Items.Clear();
                XrayLine[] temp = (XrayLine[])Enum.GetValues(typeof(XrayLine));
                for (int i = 0; i < temp.Length; i++)
                {
                    if (!double.IsNaN(AtomConstants.CharacteristicXrayWavelength(comboBoxXRayElement.SelectedIndex, temp[i])))
                        comboBoxXrayLine.Items.Add(temp[i]);
                }
                if (comboBoxXrayLine.Items.Count == 0)
                    comboBoxXrayLine.Enabled = false;
                else
                {
                    comboBoxXrayLine.Enabled = true;
                    comboBoxXrayLine.SelectedIndex = 0;
                    WavelengthChanged?.Invoke(this, new EventArgs());
                }
            }
            
        }

        /// <summary>
        /// X線のラインが変更したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxXrayLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Enabled && comboBoxXrayLine.SelectedItem != null)
            {
                var d = AtomConstants.CharacteristicXrayWavelength(comboBoxXRayElement.SelectedIndex, (XrayLine)comboBoxXrayLine.SelectedItem);
                if (!double.IsNaN(d))
                {
                    skipEvent = true;
                    numericalTextBoxWaveLength.Value = d;
                    numericalTextBoxEnergy.Value = UniversalConstants.Convert.WavelengthToXrayEnergy(numericalTextBoxWaveLength.Value / 10) / 1000;
                    skipEvent = false;
                    WavelengthChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// 線源が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonWaveSource_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonXray.Checked)
            {
                flowLayoutPanelElement.Visible = true;
                numericalTextBoxEnergy.FooterText = "keV";
            }
            else
            {
                flowLayoutPanelElement.Visible = false;
                flowLayoutPanelEnergy.Visible = true;
                flowLayoutPanelEnergy.Enabled = true;
                numericalTextBoxWaveLength.Enabled = true;

                if (radioButtonElectron.Checked)
                    numericalTextBoxEnergy.FooterText = "kV";
                else if (radioButtonNeutron.Checked)
                    numericalTextBoxEnergy.FooterText = "meV";
            }
            numericalTextBoxWaveLength_ValueChanged(sender, e);
            WaveSourceChanged?.Invoke(sender, e);

        }

        private bool skipEvent = false;

        /// <summary>
        /// 波長が直接変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericalTextBoxWaveLength_ValueChanged(object sender, EventArgs e)
        {
            
            if (skipEvent) return;

            skipEvent = true;
            if (radioButtonXray.Checked)
                numericalTextBoxEnergy.Value = UniversalConstants.Convert.WavelengthToXrayEnergy(numericalTextBoxWaveLength.Value / 10) / 1000;
            else if (radioButtonElectron.Checked)
                numericalTextBoxEnergy.Value = UniversalConstants.Convert.WaveLengthToElectronEnergy(numericalTextBoxWaveLength.Value / 10);
            else
                numericalTextBoxEnergy.Value = UniversalConstants.Convert.WaveLengthToNeutronEnergy(numericalTextBoxWaveLength.Value / 10) / 1.0E6;
            skipEvent = false;
            WavelengthChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// エネルギーが直接変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericalTextBoxEnergy_ValueChanged(object sender, EventArgs e)
        {
            if (skipEvent) return;
            skipEvent = true;
            if (radioButtonXray.Checked) //X線の時
                numericalTextBoxWaveLength.Value = UniversalConstants.Convert.EnergyToXrayWaveLength(numericalTextBoxEnergy.Value * 1000) * 10;
            else if (radioButtonElectron.Checked)//電子線の時
                numericalTextBoxWaveLength.Value = UniversalConstants.Convert.EnergyToElectronWaveLength(numericalTextBoxEnergy.Value) * 10;
            else//中性子
                numericalTextBoxWaveLength.Value = UniversalConstants.Convert.EnergyToNeutronWaveLength(numericalTextBoxEnergy.Value * 1.0E6) * 10;
            skipEvent = false;
            WavelengthChanged?.Invoke(this, new EventArgs());
        }
    }
}