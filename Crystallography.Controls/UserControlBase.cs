using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
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

    #region 260531Cl 追加: 配置先 Form/親で設定された標準 ToolTip を内部子コントロールへ反映する仕組み

    // 独自 ToolTip プロパティ([Browsable(false)] でデザイナのプロパティグリッドから隠した) を将来的に廃止するための代替経路。
    // NumericBox / ColorControl のような複合コントロールは内部の子 (textBox / pictureBox / ラベル等) が表面を覆うため、
    // 配置先 Form が host.SetToolTip(thisControl, …) しただけでは、子の上に hover してもチップが出ない
    // (標準 ToolTip は登録したコントロールのウィンドウ上でのみ反応し、子は別ウィンドウなので素通りする)。
    // そこでハンドル生成後に、親 (配置先 Form / 親 UserControl) の標準 ToolTip に this 用として設定された文字列を読み取り、
    // 同じ ToolTip で内部子へも SetToolTip して、子の上でもチップが表示されるようにする。
    // 既存の resx (独自 ToolTip プロパティ経由) で設定済みのチップはこれまで通り独自プロパティが子へ配るので、本機構は
    // 「配置先 Form の標準 ToolTip 拡張子でチップを設定した」場合のみ働く (両者は競合しない)。

    /// <summary>チップ配布先の内部子コントロール。複合コントロールの派生クラスが内部子を返す。既定 null = 配布なし。260531Cl 追加</summary>
    protected virtual Control[] GetToolTipTargets() => null;

    /// <summary>複合コントロール自身が子へチップを配るために持つ内部 ToolTip。派生クラスが返す。既定 null。260531Cl 追加</summary>
    protected internal virtual ToolTip InternalToolTip => null;

    private bool _toolTipRelayed; // 260531Cl 追加: ハンドル再生成で複数回呼ばれても配布は一度だけにするガード

    /// <summary>260531Cl 追加: ハンドル生成後、親の標準 ToolTip からこのコントロール宛のチップを読み取り内部子へ反映する。</summary>
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        if (_toolTipRelayed) return;
        _toolTipRelayed = true;
        if (InternalToolTip != null) InternalToolTip.IsBalloon = true; // 内部 ToolTip もバルーン化(親がチップ未設定＝独自プロパティ単独で表示する場面用)
        // フォーム構築 (InitializeComponent 内の SetToolTip) 完了後に走らせるため BeginInvoke で遅延実行する。
        try { BeginInvoke((Action)(() => RelayHostToolTip(this, GetToolTipTargets(), InternalToolTip))); } catch { }
    }

    /// <summary>
    /// 260531Cl 追加: self に対し親 (配置先 Form / 親 UserControl) の標準 ToolTip で設定されたチップ文字列を探し、
    /// 見つかれば「内部 ToolTip(独自プロパティ由来の矩形チップ) を抑止し、親の ToolTip(バルーン) へ一本化」する。
    /// これにより複合コントロール上で上下2つ(バルーン＋矩形)出ていたのを単一バルーンに統一する。
    /// 親がチップ未設定なら何もしない(内部 ToolTip の従来動作のまま＝独自プロパティだけで動く他リポでも影響なし)。
    /// 引数のみに依存する純粋手続きにつき static(OnHandleCreated から呼ぶ。inner は self 自身の内部 ToolTip)。
    /// </summary>
    private static void RelayHostToolTip(Control self, Control[] targets, ToolTip inner)
    {
        if (self == null || targets == null || targets.Length == 0) return;
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        for (var c = self; c != null; c = c.Parent)
            if (c.Site != null && c.Site.DesignMode) return; // デザイナ編集中は配布しない
        try
        {
            ToolTip hostTT = null; string text = null;
            for (var host = self.Parent; host != null && text == null; host = host.Parent)
                foreach (var tt in enumContainerToolTips(host))
                {
                    var s = tt.GetToolTip(self);
                    if (!string.IsNullOrEmpty(s)) { hostTT = tt; text = s; break; }
                }
            if (text == null) return;     // 親がチップ未設定 → 内部 ToolTip に委ねる(従来動作・他リポ無影響)
            hostTT.IsBalloon = true;      // バルーン表示に統一(ユーザー要望 260531Cl)。同一フォーム内の全チップに効く
            relayInto(hostTT, text, self, targets, inner);
        }
        catch { /* チップ配布の失敗で UI を落とさない */ }
    }

    private static void relayInto(ToolTip hostTT, string text, Control composite, Control[] targets, ToolTip inner)
    {
        inner?.SetToolTip(composite, ""); // 複合コントロール本体に対する内部 ToolTip(矩形) を抑止
        foreach (var t in targets)
        {
            if (t == null) continue;
            inner?.SetToolTip(t, "");      // 子に対する内部 ToolTip(矩形) を抑止
            hostTT.SetToolTip(t, text);    // 親の ToolTip(バルーン) へ一本化(子の上でも表示)
            if (t is UserControlBase ucb)  // 子も複合(例: SizeControl 内の NumericBox)なら再帰
                relayInto(hostTT, text, ucb, ucb.GetToolTipTargets() ?? Array.Empty<Control>(), ucb.InternalToolTip);
        }
    }

    /// <summary>host の型から designer 生成の components (IContainer) を取り出し、登録された ToolTip を列挙する。260531Cl 追加</summary>
    private static IEnumerable<ToolTip> enumContainerToolTips(Control host)
    {
        FieldInfo fi = null;
        for (var t = host.GetType(); fi == null && t != null && t != typeof(object); t = t.BaseType)
            fi = t.GetField("components", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (fi != null && fi.GetValue(host) is IContainer cont)
            foreach (Component comp in cont.Components)
                if (comp is ToolTip tt) yield return tt;
    }

    #endregion
}
