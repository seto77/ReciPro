using MemoryPack;
using System.Buffers;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static ReciPro.FormImageSimulator;

namespace ReciPro;

public partial class FormPresets : FormBase
{
    #region フィールド、プロパティ
    public FormImageSimulator FormImageSimulator;

    public ImageSimulatorSetting CurrentSetting;

    public ListBox ListBox => this.listBox;

    public string PresetName { get => textBoxPresetName.Text; set => textBoxPresetName.Text = value; }

    public ImageSimulatorSetting[] Settings
    {
        get => listBox.Items.Cast<ImageSimulatorSetting>().ToArray();
        set
        {
            listBox.Items.Clear();
            foreach (var setting in value)
                listBox.Items.Add(setting);
        }
    }



    #endregion

    #region コンストラクタ、クローズ
    public FormPresets()
    {
        InitializeComponent();
        HelpPage = "9-hrtem-stem-simulator"; //260801Cl 追加: 未設定だと F1 がマニュアルのトップページを開いてしまう
    }

    private void FormPresets_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        FormImageSimulator.PresetVisible = false;
    }
    #endregion

    #region Add, Replace, Rename, Deleteボタン
    public void buttonAdd_Click(object sender, EventArgs e)
    {
        listBox.Items.Add(new ImageSimulatorSetting(PresetName, FormImageSimulator));
        listBox.SelectedIndex = listBox.Items.Count - 1;
    }

    public void buttonReplace_Click(object sender, EventArgs e)
    {
        if (listBox.SelectedItem != null)
        {
            var index = listBox.SelectedIndex;
            listBox.Items.Insert(index, new ImageSimulatorSetting(PresetName, FormImageSimulator));
            listBox.Items.RemoveAt(index + 1);
            listBox.SelectedIndex = index;
        }
    }

    private void buttonRename_Click(object sender, EventArgs e)
    {
        if (listBox.SelectedItem != null)
        {
            var setting = (ImageSimulatorSetting)listBox.SelectedItem;
            setting.Name = PresetName;
            var index = listBox.SelectedIndex;
            listBox.Items.Insert(index, setting);
            listBox.Items.RemoveAt(index + 1);
            listBox.SelectedIndex = index;
        }
    }

    public void buttonDelete_Click(object sender, EventArgs e)
    {
        if (listBox.SelectedItem != null)
        {
            var index = listBox.SelectedIndex;
            listBox.Items.RemoveAt(index);
            if (listBox.Items.Count != 0)
            {
                if (index == 0)
                    listBox.SelectedIndex = 0;
                else
                    listBox.SelectedIndex = index - 1;
            }
        }
    }
    #endregion


    private void FormPresets_VisibleChanged(object sender, EventArgs e)
    {
        if (Visible)
        {
            CurrentSetting = new ImageSimulatorSetting("", FormImageSimulator);
            listBox.SelectedIndex = -1;
        }
    }

    #region OK, Cancel ボタン
    //260801Cl 修正: 直接 Visible = false にすると FormImageSimulator の checkBoxPreset がチェックされたまま残り、
    //次にプリセットを開くのにクリックが 2 回必要になっていた (checkBoxPreset → FormPresets.Visible の一方向同期のため)。
    //×ボタン経路 (FormPresets_FormClosing) と同じく PresetVisible 経由で閉じて、状態を 1 系統に統一する。
    //旧: private void buttonOK_Click(object sender, EventArgs e) { Visible = false; }
    private void buttonOK_Click(object sender, EventArgs e) => FormImageSimulator.PresetVisible = false;

    //旧: private void buttonCancel_Click(object sender, EventArgs e) { Visible = false; CurrentSetting.Apply(FormImageSimulator); }
    private void buttonCancel_Click(object sender, EventArgs e)
    {
        FormImageSimulator.PresetVisible = false;
        CurrentSetting.Apply(FormImageSimulator);
    }

    //260801Cl 追加: "Manage list" にチェックを入れると flowLayoutPanelOkCancel ごと OK/Cancel が非表示になり、
    //非表示ボタンは PerformClick しても CanSelect を満たさず何も起きないため、そのモードでは Esc が完全に死んでいた。
    //ProcessCmdKey は CancelButton を処理する ProcessDialogKey より前に呼ばれるので、両モードで同じ経路に揃う
    //(通常モードで CancelButton と二重に走ることもない)。
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Escape)
        {
            buttonCancel_Click(this, EventArgs.Empty);
            return true;
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }
    #endregion

    private void checkBoxManageList_CheckedChanged(object sender, EventArgs e)
    {
        panelManageList.Visible = checkBoxManageList.Checked;
        flowLayoutPanelOkCancel.Visible = !checkBoxManageList.Checked;
    }

    private void listBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listBox.SelectedItem != null)
        {
            PresetName = ((ImageSimulatorSetting)listBox.SelectedItem).Name;

            if (!checkBoxManageList.Checked)
                ((ImageSimulatorSetting)listBox.SelectedItem).Apply(FormImageSimulator);
        }
    }
}

#region Setting クラス
[MemoryPackable]
public partial struct ImageSimulatorSetting
{
    #region フィールド

    //共通
    public string Name;
    public bool Native;

    public HRTEM_Modes HRTEM_Mode;
    public ImageModes ImageMode;
    public double AccVol;
    public int BlochNum;
    public double Cs;
    public double Cc;
    public double DeltaVol;
    public double Defocus;
    public double Thickness;

    public double ImageResolution;
    public Size ImageSize;

    //HRTEM固有
    public double ObjAperRadius;
    public double ObjAperX;
    public double ObjAperY;
    public double Beta;

    //シリアルイメージモード
    public double[] ThicknessArray;
    public double[] DefocusArray;

    //STEM固有
    public double DetectorInnerAngle;
    public double DetectorOuterAngle;
    public double ConvergenceAngle;
    public double SourceSize;
    public double SliceThicknessForInelastic;

    //260802Cl 追加: STEM probe sampling と STEM-EDX の要求 (設計書 §5.9.1-6)。
    //**必ず末尾に足すこと・既存フィールドの並べ替え禁止**: MemoryPack は非 unmanaged な型を
    //「メンバ数 1 byte + 各メンバ」で書くため、旧 blob (メンバ数 24) を新型 (27) で読むと
    //足りない分が既定値になる = 上位互換。順序を変えると型が食い違って壊れる (実測確認済み)。
    //なお新 blob を旧版で読むと MemoryPackSerializationException になるが、Reg.RW が catch して
    //既定値のままにするので、ダウングレード時はプリセットが復元されないだけで害はない。
    /// <summary>収束ビームの角度分解能 (rad)。EDX 固有ではなく STEM 共通の probe sampling 値として保存する</summary>
    public double AngularResolution;
    /// <summary>STEM-EDX マップを計算するか</summary>
    public bool EdxEnabled;
    /// <summary>260802Cl 変更 (作者指示): 元素×殻の選択 UI を廃し、EDX が ON なら利用可能な特性 X 線を常に全部
    /// 計算するようにしたため、この項目は**未使用**。ただし**削除しない**: 削るとメンバ数が 27→26 になり、
    /// 27 で書かれた既存 blob が MemoryPackSerializationException になる (Reg.RW が握り潰すのでプリセットが
    /// 黙って消える)。将来また選択 UI を入れるときの枠として残す。</summary>
    public (int Z, IonizationShell Shell)[] EdxChannels;
    #endregion

    public override readonly string ToString() => Name;

    [MemoryPackConstructor]
    public ImageSimulatorSetting() { }

    //コンストラクタ
    public ImageSimulatorSetting(string name, FormImageSimulator f)
    {
        Name = name;
        //共通

        HRTEM_Mode = f.HRTEM_Mode;
        ImageMode = f.ImageMode;
        AccVol = f.AccVol;
        BlochNum = f.BlochNum;
        Cs = f.Cs;
        Cc = f.Cc;
        DeltaVol = f.DeltaVol;
        Defocus = f.Defocus;
        Thickness = f.Thickness;

        ImageResolution = f.ImageResolution;
        ImageSize = f.ImageSize;

        //HRTEM固有
        ObjAperRadius = f.HRTEM_ObjAperRadius;
        ObjAperX = f.HRTEM_ObjAperX;
        ObjAperY = f.HRTEM_ObjAperY;
        Beta = f.HRTEM_Beta;

        //シリアルイメージモード
        ThicknessArray = f.ThicknessArray;
        DefocusArray = f.DefocusArray;

        //STEM固有
        DetectorInnerAngle = f.STEM_DetectorInnerAngle;
        DetectorOuterAngle = f.STEM_DetectorOuterAngle;
        ConvergenceAngle = f.STEM_ConvergenceAngle;
        SourceSize = f.STEM_SourceSizeFWHM;
        SliceThicknessForInelastic = f.STEM_SliceThickness;

        //STEM probe sampling と STEM-EDX (260802Cl 追加)
        AngularResolution = f.STEM_AngularResolution;
        EdxEnabled = f.EdxEnabled;
        EdxChannels = null;//260802Cl: 未使用 (EDX が ON なら利用可能な特性 X 線を常に全部計算する)。枠だけ残す
    }

    public readonly void Apply(FormImageSimulator f)
    {
        //共通

        f.HRTEM_Mode = HRTEM_Mode;
        f.ImageMode = ImageMode;
        f.AccVol = AccVol;
        f.BlochNum = BlochNum;
        f.Cs = Cs;
        f.Cc = Cc;
        f.DeltaVol = DeltaVol;
        f.Defocus = Defocus;
        f.Thickness = Thickness;

        f.ImageResolution = ImageResolution;
        f.ImageSize = ImageSize;

        //HRTEM固有
        f.HRTEM_ObjAperRadius = ObjAperRadius;
        f.HRTEM_ObjAperX = ObjAperX;
        f.HRTEM_ObjAperY = ObjAperY;
        f.HRTEM_Beta = Beta;

        //シリアルイメージモード
        f.ThicknessArray = ThicknessArray;
        f.DefocusArray = DefocusArray;

        //STEM固有
        f.STEM_DetectorInnerAngle = DetectorInnerAngle;
        f.STEM_DetectorOuterAngle = DetectorOuterAngle;
        f.STEM_ConvergenceAngle = ConvergenceAngle;
        f.STEM_SourceSizeFWHM = SourceSize;
        f.STEM_SliceThickness = SliceThicknessForInelastic;

        //STEM probe sampling と STEM-EDX (260802Cl 追加)。順序に意味がある:
        //角度分解能 → EdxEnabled (候補一覧を作る) → チャネル復元 (積集合のみ)。
        //旧 blob では AngularResolution が 0 になるので、その場合だけ現在値を保つ (0 は probe 分割数が 0 除算になる無効値)
        if (AngularResolution > 0)
            f.STEM_AngularResolution = AngularResolution;
        f.EdxEnabled = EdxEnabled;
        //260802Cl: EdxChannels は未使用 (常に全チャネル計算) なので適用しない
    }
}
#endregion

