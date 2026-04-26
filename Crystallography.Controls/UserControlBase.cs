using System.ComponentModel;
using System.Windows.Forms;

namespace Crystallography.Controls;

[ToolboxItem(false)]
public partial class UserControlBase : UserControl
{
    protected UserControlBase() => InitializeComponent();

    /// <summary>
    /// 260426Cl 追加: 親方向に走査して Designer の編集中かを判定する。
    /// UserControl 既定の DesignMode は自身の Site しか見ないため、子コントロールでは false になる問題を回避する。
    /// </summary>
    public new bool DesignMode
    {
        get
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return true;
            for (var ctrl = (Control)this; ctrl != null; ctrl = ctrl.Parent)
                if (ctrl.Site != null && ctrl.Site.DesignMode) return true;
            return false;
        }
    }
}
