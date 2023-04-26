using System;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class HorizontalAxisUserControl : UserControl
    {
        public delegate void MyEventHandler();

        public event MyEventHandler AxisPropertyChanged;

        public bool SkipAxisPropertyChangedEvent = false;
        #region プロパティ

        public HorizontalAxisProperty HorizontalAxisProperty
        {
            get => new(AxisMode, WaveSource, WaveColor, WaveLength, XrayNumber, XrayLine, ElectronAccVol, TakeoffAngle, TofAngle, TofLength,
                TwoThetaUnit, DspacingUnit, WaveNumberUnit, EnergyUnit, TofTimeUnit);

            set
            {
                bool tmp = SkipAxisPropertyChangedEvent;
                SkipAxisPropertyChangedEvent = true;
                WaveSource = value.WaveSource;
                WaveColor = value.WaveColor;

                AxisMode = value.AxisMode;

                XrayNumber = value.XrayElementNumber;
                XrayLine = value.XrayLine;
                ElectronAccVol = value.ElectronAccVolatage;
                TakeoffAngle = value.EnergyTakeoffAngle;
                TofAngle = value.TofAngle;
                TofLength = value.TofLength;
                TwoThetaUnit = value.TwoThetaUnit;
                DspacingUnit = value.DspacingUnit;
                WaveNumberUnit = value.WaveNumberUnit;
                EnergyUnit = value.EnergyUnit;
                TofTimeUnit = value.TofTimeUnit;

                if (WaveColor == WaveColor.Monochrome)
                {
                    if (WaveSource == WaveSource.Electron || WaveSource == WaveSource.Neutron)
                        WaveLength = value.WaveLength;
                    else if (XrayNumber == 0)
                        WaveLength = value.WaveLength;
                }
                SkipAxisPropertyChangedEvent = tmp;
                if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
            }
        }

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

                if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
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
                    return HorizontalAxis.None;
            }
        }

        /// <summary>
        /// 波長をÅ単位のテキストで取得/設定
        /// </summary>
        public string WaveLengthText
        {
            set
            {
                waveLengthControl.WaveLengthText = value;
                if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
            }
            get => waveLengthControl.WaveLengthText;
        }

        /// <summary>
        /// nm単位の実数で取得/設定
        /// </summary>
        public double WaveLength
        {
            set
            {
                waveLengthControl.WaveLength = value;
                if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
            }
            get => waveLengthControl.WaveLength;
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
                        if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
                    }
                }
                catch { }
            }
            get => numericBoxTwoTheta.Value.ToString();
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
                    AxisPropertyChanged?.Invoke();
                }
            }
            get => numericBoxTwoTheta.Value / 180.0 * Math.PI;
        }

        /// <summary>
        /// エネルギーの単位
        /// </summary>
        public EnergyUnitEnum EnergyUnit
        {
            set
            {
                if (value == EnergyUnit) return;
                if (value != EnergyUnitEnum.eV && value != EnergyUnitEnum.KeV && value != EnergyUnitEnum.MeV)
                    return;
                radioButtonEnergyUnitEv.Checked = (value == EnergyUnitEnum.eV);
                radioButtonEnergyUnitKev.Checked = (value == EnergyUnitEnum.KeV);
                radioButtonEnergyUnitMev.Checked = (value == EnergyUnitEnum.MeV);
                if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
            }
            get
            {
                if (radioButtonEnergyUnitEv.Checked)
                    return EnergyUnitEnum.eV;
                else if (radioButtonEnergyUnitKev.Checked)
                    return EnergyUnitEnum.KeV;
                else
                    return EnergyUnitEnum.MeV;
            }
        }

        /// <summary>
        /// d値の単位
        /// </summary>
        public LengthUnitEnum DspacingUnit
        {
            set
            {
                if (value == DspacingUnit) return;
                if (value != LengthUnitEnum.Angstrom && value != LengthUnitEnum.NanoMeter)
                    return;
                radioButtonDspacingUnitAng.Checked = (value == LengthUnitEnum.Angstrom);
                radioButtonDspacingUnitNm.Checked = (value == LengthUnitEnum.NanoMeter);
                if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
            }
            get
            {
                if (radioButtonDspacingUnitAng.Checked)
                    return LengthUnitEnum.Angstrom;
                else
                    return LengthUnitEnum.NanoMeter;
            }
        }

        /// <summary>
        /// 波数の単位 (1/nmか1/Aかのどちらか)
        /// </summary>
        public LengthUnitEnum WaveNumberUnit
        {
            set
            {
                if (value == WaveNumberUnit) return;
                if (value != LengthUnitEnum.NanoMeterInverse && value != LengthUnitEnum.AngstromInverse)
                    return;
                radioButtonWavenumberUnitNmInv.Checked = (value == LengthUnitEnum.NanoMeterInverse);
                radioButtonWavenumberAngInv.Checked = (value == LengthUnitEnum.AngstromInverse);
                if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
            }
            get
            {
                if (radioButtonWavenumberUnitNmInv.Checked)
                    return LengthUnitEnum.NanoMeterInverse;
                else
                    return LengthUnitEnum.AngstromInverse;
            }
        }


        public AngleUnitEnum TwoThetaUnit
        {
            set
            {
                if (value == TwoThetaUnit) return;
                if (value != AngleUnitEnum.Degree && value != AngleUnitEnum.Radian) return;

                radioButtonAngleUnitRadian.Checked = (value == AngleUnitEnum.Radian);
                radioButtonAngleUnitDegree.Checked = (value == AngleUnitEnum.Degree);
                if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
            }
            get
            {
                if (radioButtonAngleUnitRadian.Checked)
                    return AngleUnitEnum.Radian;
                else
                    return AngleUnitEnum.Degree;
            }
        }
        /// <summary>
        /// TOF時間の単位
        /// </summary>
        public TimeUnitEnum TofTimeUnit
        {
            set
            {
                if (value == TofTimeUnit)
                    return;

                radioButtonTofUnitNanoSec.Checked = value == TimeUnitEnum.NanoSecond;
                radioButtonTofUnitMicroSec.Checked = value == TimeUnitEnum.MicroSecond;
                if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged();
            }
            get
            {
                if (radioButtonTofUnitNanoSec.Checked) return TimeUnitEnum.NanoSecond;
                else return TimeUnitEnum.MicroSecond;
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
                    if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
                }
                catch { }
            }
            get => numericBoxTofTakeOffAngle.Value.ToString();
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
                    if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
                }
            }
            get => numericBoxTofTakeOffAngle.Value / 180.0 * Math.PI;
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
                        if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
                    }
                }
                catch { }
            }
            get => numericBoxTofDistance.Value;
        }

        /// <summary>
        /// 線源を取得/設定
        /// </summary>
        public WaveSource WaveSource
        {
            set
            {
                if (value == WaveSource)
                    return;

                if (value == WaveSource.Xray)
                    radioButtonXray.Checked = true;
                else if (value == WaveSource.Electron)
                    radioButtonElectron.Checked = true;
                else if (value == WaveSource.Neutron)
                    radioButtonNeutron.Checked = true;
                if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
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
                if (WaveColor == value)
                    return;

                if (value == WaveColor.Monochrome)
                    radioButtonMonochro.Checked = true;
                else if (value == WaveColor.FlatWhite)
                    radioButtonFlatWhite.Checked = true;
                else if (value == WaveColor.CustomWhite)
                    radioButtonCustomWhite.Checked = true;
                if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
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
        public int XrayNumber
        {
            set
            {
                if (waveLengthControl.XrayWaveSourceElementNumber == value)
                    return;
                waveLengthControl.XrayWaveSourceElementNumber = value;
                if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
            }
            get => waveLengthControl.XrayWaveSourceElementNumber;
        }

        /// <summary>
        /// X線の線源を取得/設定
        /// </summary>
        public XrayLine XrayLine
        {
            set
            {
                if (waveLengthControl.XrayWaveSourceLine == value)
                    return;
                waveLengthControl.XrayWaveSourceLine = value;
                if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
            }
            get => waveLengthControl.XrayWaveSourceLine;
        }

        /// <summary>
        /// 電子線加速電圧(kV)を取得/設定
        /// </summary>
        public double ElectronAccVol
        {
            set
            {
                if (waveLengthControl.Energy == value)
                    return;
                waveLengthControl.Energy = value;
                if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
            }
            get => waveLengthControl.Energy;
        }

        /// <summary>
        /// 電子線加速電圧(kV)を取得/設定
        /// </summary>
        public string ElectronAccVoltageText
        {
            set
            {
                if (waveLengthControl.EnergyText == value)
                    return;
                waveLengthControl.EnergyText = value;
                if (!SkipAxisPropertyChangedEvent) AxisPropertyChanged?.Invoke();
            }
            get => waveLengthControl.EnergyText;
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

        private void numericBoxWaveLength_ValueChanged(object sender, EventArgs e) => AxisPropertyChanged?.Invoke();

        private void numericBoxTwoTheta_ValueChanged(object sender, EventArgs e) => AxisPropertyChanged?.Invoke();

        private void radioButtonDegree_CheckedChanged(object sender, EventArgs e) => AxisPropertyChanged?.Invoke();

        private void radioButtonEv_CheckedChanged(object sender, EventArgs e) => AxisPropertyChanged?.Invoke();

        private void radioButtonAngstrom_CheckedChanged(object sender, EventArgs e) => AxisPropertyChanged?.Invoke();

        private void waveLengthControl_WavelengthChanged(object sender, EventArgs e) => AxisPropertyChanged?.Invoke();

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