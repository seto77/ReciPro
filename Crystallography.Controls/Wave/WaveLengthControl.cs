using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls;

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

    public FlowDirection Direction
    {
        set
        {
            direction = value;
            if (direction == FlowDirection.LeftToRight)
            {
                flowLayoutPanelWaveSource.FlowDirection = FlowDirection.TopDown;
                flowLayoutPanelWaveSource.Dock = DockStyle.Left;
            }
            else
            {
                flowLayoutPanelWaveSource.FlowDirection = FlowDirection.LeftToRight;
                flowLayoutPanelWaveSource.Dock = DockStyle.Top;
            }
        }
        get
        {
            if (direction == FlowDirection.LeftToRight)
                return FlowDirection.LeftToRight;
            else
                return FlowDirection.TopDown;
        }
    }
    public FlowDirection direction = FlowDirection.TopDown;

    public Font TextFont
    {
        set
        {
            numericBoxEnergy.TextFont = value;
            numericBoxWaveLength.TextFont = value;
        }
        get => numericBoxWaveLength.TextFont;
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
                numericBoxWaveLength.Value = Convert.ToDouble(value);
                comboBoxXRayElement.SelectedIndex = 0;
            }
            catch
            {
            }
        }
        get => numericBoxWaveLength.Text;
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

                numericBoxWaveLength.Value = value * 10.0;
            }
        }
        get => numericBoxWaveLength.Value / 10.0;
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
                numericBoxEnergy.Value = value;
        }
        get => numericBoxEnergy.Value;
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
                numericBoxEnergy.Value = Convert.ToDouble(value);
            }
            catch { }
        }
        get
        {
            return numericBoxEnergy.Value.ToString();
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
        if (DesignMode)
            return;
        InitializeComponent();


        comboBoxXRayElement.SelectedIndex = 0;
    }

    

    /// <summary>
    /// X線のElementが変更されたとき
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void comboBoxXRayElement_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!radioButtonXray.Checked) return;

        if (comboBoxXRayElement.SelectedIndex == 0)//Customが選択されたとき
        {
            comboBoxXrayLine.Visible = false;
            comboBoxXRayElement.Width = 100;
            numericBoxEnergy.Enabled = true;
            numericBoxWaveLength.Enabled = true;
        }
        else
        {
            comboBoxXRayElement.Width = 70;
            comboBoxXrayLine.Visible = true;
            numericBoxEnergy.Enabled = false;

            numericBoxWaveLength.Enabled = false;

            comboBoxXrayLine.Items.Clear();
            XrayLine[] temp = (XrayLine[])Enum.GetValues(typeof(XrayLine));
            for (int i = 0; i < temp.Length; i++)
            {
                if (!double.IsNaN(AtomStatic.CharacteristicXrayWavelength(comboBoxXRayElement.SelectedIndex, temp[i])))
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
            var d = AtomStatic.CharacteristicXrayWavelength(comboBoxXRayElement.SelectedIndex, (XrayLine)comboBoxXrayLine.SelectedItem);
            if (!double.IsNaN(d))
            {
                skipEvent = true;
                numericBoxWaveLength.Value = d;
                numericBoxEnergy.Value = UniversalConstants.Convert.WavelengthToXrayEnergy(numericBoxWaveLength.Value / 10) / 1000;
                skipEvent = false;
                WavelengthChanged?.Invoke(this, new EventArgs());
            }
        }
    }

    /// <summary>
    /// 現状の原子番号、線種で、特性X線の波長とエネルギーをリセット
    /// </summary>
    public void SetCharacteristicXray() => comboBoxXrayLine_SelectedIndexChanged(new object(), new EventArgs());


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
            numericBoxEnergy.FooterText = "keV";
            comboBoxXRayElement_SelectedIndexChanged(sender, e);
        }
        else
        {
            flowLayoutPanelElement.Visible = false;
            numericBoxEnergy.Visible = true;
            numericBoxEnergy.Enabled = true;
            numericBoxWaveLength.Enabled = true;

            if (radioButtonElectron.Checked)
                numericBoxEnergy.FooterText = "kV";
            else if (radioButtonNeutron.Checked)
                numericBoxEnergy.FooterText = "meV";
        }
        numericBoxWaveLength_ValueChanged(sender, e);
        WaveSourceChanged?.Invoke(sender, e);

    }

    private bool skipEvent = false;

    /// <summary>
    /// 波長が直接変更されたとき
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void numericBoxWaveLength_ValueChanged(object sender, EventArgs e)
    {

        if (skipEvent) return;

        skipEvent = true;
        if (radioButtonXray.Checked)
            numericBoxEnergy.Value = UniversalConstants.Convert.WavelengthToXrayEnergy(numericBoxWaveLength.Value / 10) / 1000;
        else if (radioButtonElectron.Checked)
            numericBoxEnergy.Value = UniversalConstants.Convert.WaveLengthToElectronEnergy(numericBoxWaveLength.Value / 10);
        else
            numericBoxEnergy.Value = UniversalConstants.Convert.WaveLengthToNeutronEnergy(numericBoxWaveLength.Value / 10) / 1.0E6;
        skipEvent = false;
        WavelengthChanged?.Invoke(this, new EventArgs());
    }

    /// <summary>
    /// エネルギーが直接変更されたとき
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void numericBoxEnergy_ValueChanged(object sender, EventArgs e)
    {
        if (skipEvent) return;
        skipEvent = true;
        if (radioButtonXray.Checked) //X線の時
            numericBoxWaveLength.Value = UniversalConstants.Convert.EnergyToXrayWaveLength(numericBoxEnergy.Value * 1000) * 10;
        else if (radioButtonElectron.Checked)//電子線の時
            numericBoxWaveLength.Value = UniversalConstants.Convert.EnergyToElectronWaveLength(numericBoxEnergy.Value) * 10;
        else//中性子
            numericBoxWaveLength.Value = UniversalConstants.Convert.EnergyToNeutronWaveLength(numericBoxEnergy.Value * 1.0E6) * 10;
        skipEvent = false;
        WavelengthChanged?.Invoke(this, new EventArgs());
    }
}