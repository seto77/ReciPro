using System;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class HorizontalAxisUserControl : UserControl
    {
        public delegate void MyEventHandler();

        public event MyEventHandler AxisPropertyChanged;

        #region プロパティ

        //現在の軸の情報を返すプロパティ
        public HorizontalAxis AxisMode
        {
            set
            {
                if (value == HorizontalAxis.Angle)
                    radioButtonTwoTheta.Checked = true;
                else if (value == HorizontalAxis.d)
                    radioButtonDspacing.Checked = true;
                else if (value == HorizontalAxis.EnergyXray)
                {
                    radioButtonXray.Checked = true;
                    radioButtonEnergy.Checked = true;
                }
                else if (value == HorizontalAxis.EnergyElectron)
                {
                    radioButtonElectron.Checked = true;
                    radioButtonEnergy.Checked = true;
                }
                else if (value == HorizontalAxis.EnergyNeutron)
                {
                    radioButtonNeutron.Checked = true;
                    radioButtonEnergy.Checked = true;
                }
                else if (value == HorizontalAxis.NeutronTOF)
                    radioButtonTOF.Checked = true;
                else if (value == HorizontalAxis.WaveNumber)
                    radioButtonWavenumber.Checked = true;
            }
            get
            {
                if (radioButtonTwoTheta.Checked)
                    return HorizontalAxis.Angle;
                else if (radioButtonDspacing.Checked)
                    return HorizontalAxis.d;
                else if (radioButtonEnergy.Checked)
                {
                    if (radioButtonXray.Checked)
                        return HorizontalAxis.EnergyXray;
                    else
                        return HorizontalAxis.EnergyElectron;
                }
                else if (radioButtonTOF.Checked)
                    return HorizontalAxis.NeutronTOF;
                else if (radioButtonWavenumber.Checked)
                    return HorizontalAxis.WaveNumber;
                else
                    return HorizontalAxis.none;
            }
        }

        /// <summary>
        /// 波長をÅ単位のテキストで取得/設定
        /// </summary>
        public string WaveLengthText
        {
            set { waveLengthControl.WaveLengthText = value; }
            get { return waveLengthControl.WaveLengthText; }
        }

        /// <summary>
        /// nm単位の実数で取得/設定
        /// </summary>
        public double WaveLength
        {
            set { waveLengthControl.WaveLength = value; }
            get { return waveLengthControl.WaveLength; }
        }

        /// <summary>
        /// EDXの取り出し角 度単位で取得/設定
        /// </summary>
        public string TakeoffAngleText
        {
            set
            {
                try
                {
                    if (numericBoxTwoTheta.Value != Convert.ToDouble(value))
                    {
                        numericBoxTwoTheta.Value = Convert.ToDouble(value);

                        if (AxisPropertyChanged != null)
                            AxisPropertyChanged();
                    }
                }
                catch { }
            }
            get
            {
                return numericBoxTwoTheta.Value.ToString();
            }
        }

        /// <summary>
        /// EDXの取り出し角 ラジアン単位で取得/設定
        /// </summary>
        public double TakeoffAngle
        {
            set
            {
                if (value > 0 && numericBoxTwoTheta.Value != value / Math.PI * 180.0)
                {
                    numericBoxTwoTheta.Value = value / Math.PI * 180.0;
                    if (AxisPropertyChanged != null)
                        AxisPropertyChanged();
                }
            }
            get { return numericBoxTwoTheta.Value / 180.0 * Math.PI; }
        }

        public EnergyUnitEnum EnergyUnit
        {
            set
            {
                radioButtonEnergyUnitEv.Checked = (value == EnergyUnitEnum.eV);
                radioButtonEnergyUnitKev.Checked = !(value == EnergyUnitEnum.eV);
            }
            get
            {
                return radioButtonEnergyUnitEv.Checked ? EnergyUnitEnum.eV : EnergyUnitEnum.KeV;
            }
        }

        /// <summary>
        /// TOFの取り出し角 度単位で取得/設定
        /// </summary>
        public string TofAngleText
        {
            set
            {
                try
                {
                    numericBoxTofTakeOffAngle.Value = Convert.ToDouble(value);

                    if (AxisPropertyChanged != null)
                        AxisPropertyChanged();
                }
                catch { }
            }
            get
            {
                return numericBoxTofTakeOffAngle.Value.ToString();
            }
        }

        /// <summary>
        /// TOFの取り出し角 ラジアン単位で取得/設定
        /// </summary>
        public double TofAngle
        {
            set
            {
                if (value > 0 && numericBoxTofTakeOffAngle.Value != value / Math.PI * 180.0)
                {
                    numericBoxTofTakeOffAngle.Value = value / Math.PI * 180.0;
                    if (AxisPropertyChanged != null)
                        AxisPropertyChanged();
                }
            }
            get { return numericBoxTofTakeOffAngle.Value / 180.0 * Math.PI; }
        }

        /// <summary>
        /// TOFの検出距離 mm単位で取得/設定
        /// </summary>
        public double TofLength
        {
            set
            {
                try
                {
                    if (numericBoxTofDistance.Value != value)
                    {
                        numericBoxTofDistance.Value = value;

                        if (AxisPropertyChanged != null)
                            AxisPropertyChanged();
                    }
                }
                catch { }
            }
            get
            {
                return numericBoxTofDistance.Value;
            }
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
                else if (radioButtonNeutron.Checked)
                    return WaveSource.Neutron;
                else
                    return WaveSource.None;
            }
        }

        public WaveColor WaveColor
        {
            set
            {
                if (value == WaveColor.Monochrome)
                    radioButtonMonochro.Checked = true;
                else if (value == WaveColor.FlatWhite)
                    radioButtonFlatWhite.Checked = true;
                else if (value == WaveColor.CustomWhite)
                    radioButtonCustomWhite.Checked = true;
            }
            get
            {
                if (radioButtonMonochro.Checked)
                    return WaveColor.Monochrome;
                else if (radioButtonFlatWhite.Checked)
                    return WaveColor.FlatWhite;
                else if (radioButtonCustomWhite.Checked)
                    return WaveColor.CustomWhite;
                else
                    return WaveColor.None;
            }
        }

        /// <summary>
        /// X線の線源を取得/設定
        /// </summary>
        public int XrayWaveSourceElementNumber
        {
            set { waveLengthControl.XrayWaveSourceElementNumber = value; }
            get { return waveLengthControl.XrayWaveSourceElementNumber; }
        }

        /// <summary>
        /// X線の線源を取得/設定
        /// </summary>
        public XrayLine XrayWaveSourceLine
        {
            set { waveLengthControl.XrayWaveSourceLine = value; }
            get { return waveLengthControl.XrayWaveSourceLine; }
        }

        /// <summary>
        /// 電子線加速電圧(kV)を取得/設定
        /// </summary>
        public double ElectronAccVoltage
        {
            set { waveLengthControl.Energy = value; }
            get { return waveLengthControl.Energy; }
        }

        /// <summary>
        /// 電子線加速電圧(kV)を取得/設定
        /// </summary>
        public string ElectronAccVoltageText
        {
            set { waveLengthControl.EnergyText = value; }
            get { return waveLengthControl.EnergyText; }
        }

        #endregion プロパティ

        public HorizontalAxisUserControl()
        {
            InitializeComponent();
        }

        private void radioButtonTwoTheta_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                flowLayoutPanelTwoTheta.Visible = radioButtonTwoTheta.Checked;
                flowLayoutPanelDspacing.Visible = radioButtonDspacing.Checked;
                flowLayoutPanelEnergy.Visible = radioButtonEnergy.Checked;
                flowLayoutPanelNeutronTOF.Visible = radioButtonTOF.Checked;
                flowLayoutPanelWavenumber.Visible = radioButtonWavenumber.Checked;

                AxisPropertyChanged?.Invoke();
            }
        }

        private void numericBoxWaveLength_ValueChanged(object sender, EventArgs e)
        {
            AxisPropertyChanged?.Invoke();
        }

        private void numericBoxTwoTheta_ValueChanged(object sender, EventArgs e)
        {
            AxisPropertyChanged?.Invoke();
        }

        private void radioButtonDegree_CheckedChanged(object sender, EventArgs e)
        {
            AxisPropertyChanged?.Invoke();
        }

        private void radioButtonEv_CheckedChanged(object sender, EventArgs e)
        {
            AxisPropertyChanged?.Invoke();
        }

        private void radioButtonAngstrom_CheckedChanged(object sender, EventArgs e)
        {
            AxisPropertyChanged?.Invoke();
        }

        private void waveLengthControl_WavelengthChanged(object sender, EventArgs e)
        {
            AxisPropertyChanged?.Invoke();
        }

        private void radioButtonWaveSource_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonXray.Checked)
            {
                waveLengthControl.WaveSource = WaveSource.Xray;
                radioButtonFlatWhite.Enabled = true;
            }
            else if (radioButtonElectron.Checked)
            {
                waveLengthControl.WaveSource = WaveSource.Electron;
                radioButtonFlatWhite.Enabled = true;
            }
            else
            {
                waveLengthControl.WaveSource = WaveSource.Neutron;
                radioButtonFlatWhite.Enabled = true;
            }
            radioButtonMonochro_CheckedChanged(sender, e);
        }

        private void radioButtonMonochro_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMonochro.Checked)
            {
                if (radioButtonEnergy.Checked)
                    radioButtonTwoTheta.Checked = true;
                radioButtonEnergy.Enabled = false;

                if (radioButtonTOF.Checked)
                    radioButtonTwoTheta.Checked = true;
                radioButtonTOF.Enabled = false;

                radioButtonTwoTheta.Enabled = true;

                groupBoxTwoTheta.Enabled = true;
            }
            else if (radioButtonFlatWhite.Checked)
            {
                if (radioButtonTwoTheta.Checked)
                    radioButtonEnergy.Checked = true;
                radioButtonTwoTheta.Enabled = false;

                radioButtonEnergy.Enabled = true;
                radioButtonTOF.Enabled = radioButtonNeutron.Checked;
                if (radioButtonTOF.Checked && radioButtonTOF.Enabled == false)
                    radioButtonEnergy.Checked = true;

                groupBoxTwoTheta.Enabled = false;
            }
        }
    }
}