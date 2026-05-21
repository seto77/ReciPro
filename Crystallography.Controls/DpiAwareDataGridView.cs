using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Crystallography.Controls;

/// <summary>
/// DataGridView variant that keeps designer column widths proportional to the current DPI.
/// </summary>
/// <remarks>
/// 260518Cl 追加: DataGridViewColumn は Control ではないため WinForms の自動 DPI スケーリングが
/// Width / MinimumWidth に効かない。本クラスはデザイナで指定された値を 96dpi 論理値として保持し、
/// 実行時 DPI に応じて物理ピクセル幅をスケールする。
/// </remarks>
[ToolboxItem(true)]
public class DpiAwareDataGridView : DataGridView
{
    private readonly Dictionary<DataGridViewColumn, ColumnMetrics> columnMetrics = new();
    private int? logicalRowHeadersWidth;
    // 260518Cl: 自動スケーリング (本クラスの ApplyDpiScaling / フレームワークの ScaleControl) 中は
    //           OnColumnWidthChanged / OnRowHeadersWidthChanged による論理値の再ベースライン化を抑止する。
    //           旧 applyingDpi + frameworkScaling の 2 フラグを統合 (どちらも同義のため)。
    private bool suppressMetricsUpdate;
    private bool applyScheduled;
    private bool initialScalingDone; // 260518Cl 追加: 初回 ApplyDpiScaling 完了フラグ (デザイナ値の論理値解釈に使用)
    private Form dpiSourceForm;

    /// <summary>列幅 (Width / MinimumWidth) を DPI に応じてスケーリングするか。260518Cl 追加。</summary>
    [DefaultValue(true)]
    [Category("Layout")]
    public bool ScaleColumnWidthsForDpi { get; set; } = true;

    /// <summary>行ヘッダ幅 (RowHeadersWidth) を DPI に応じてスケーリングするか。260518Cl 追加。</summary>
    [DefaultValue(true)]
    [Category("Layout")]
    public bool ScaleRowHeadersWidthForDpi { get; set; } = true;

    /// <summary>
    /// 全 DpiAwareDataGridView で列ヘッダの配置 (中央寄せ) を統一するか。
    /// 260521Cl 追加 (GUI統一 §3.9): フォームごとにヘッダの Alignment がバラバラ (中央寄せ/既定左) だった不統一を解消する。
    /// フォント/配色はテーマ任せ (EnableHeadersVisualStyles=true、DPI 対応) で既に一貫しているため触らない。
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    public bool UnifyHeaderStyle { get; set; } = true;

    // 260518Cl: 初回 DPI スケーリングはフレームワーク (PerformAutoScale) が列幅まで面倒見てくれないため
    //           本クラスで OnHandleCreated 後に BeginInvoke 経由で実施する。
    //           ただし OnDataBindingComplete / OnFontChanged など実行時にのみ繰り返し発火するイベントは
    //           ScheduleDpiScaling を呼ばない (Designer と実行時で列幅が一致しなくなる不具合の原因)。
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        UpdateDpiSourceForm();
        ScheduleDpiScaling();
        ApplyUnifiedHeaderStyle(); // 260521Cl (§3.9)
    }

    protected override void OnParentChanged(EventArgs e)
    {
        base.OnParentChanged(e);
        UpdateDpiSourceForm();
    }

    protected override void OnDpiChangedAfterParent(EventArgs e)
    {
        base.OnDpiChangedAfterParent(e);
        ScheduleDpiScaling();
    }

    /// <summary>
    /// 260521Cl 追加 (GUI統一 §3.9): 列ヘッダの配置を全グリッドで中央寄せに統一する (Designer 設定の有無を問わず実行時に適用)。
    /// 当初は Bold + EnableHeadersVisualStyles=false + SystemColors 配色も設定していたが、(1) ユーザ要望で Bold 廃止、
    /// (2) EnableHeadersVisualStyles=false + 実行時生成フォントだと高 DPI でヘッダ文字がセルより小さく見える問題が出たため、
    /// テーマ任せ (DPI 対応) のまま「配置の統一」のみに簡素化した。フォント/配色には触れない。
    /// </summary>
    private void ApplyUnifiedHeaderStyle()
    {
        if (!UnifyHeaderStyle)
            return;

        ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
    }

    protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
    {
        base.OnColumnAdded(e);
        StoreLogicalColumnMetrics(e.Column);
    }

    protected override void OnColumnRemoved(DataGridViewColumnEventArgs e)
    {
        columnMetrics.Remove(e.Column);
        base.OnColumnRemoved(e);
    }

    protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
    {
        base.OnColumnWidthChanged(e);
        // (260518Cl) 旧条件 `(!IsHandleCreated || !ContainsKey)` だと、DataSource 設定等で
        // IsHandleCreated が早期に true になった場合に ApplyResources による Width 変更を取り逃がし、
        // 初期デフォルト幅 (100) が論理値として残ってしまう不具合があった。
        // suppressMetricsUpdate 中の自動スケーリング以外は最新値を論理値として保存する
        // (ユーザの手動リサイズも論理値ベースラインとして取り込まれる)。
        if (!suppressMetricsUpdate)
            StoreLogicalColumnMetrics(e.Column);
    }

    // 260518Cl 追加: .NET 6+ の DataGridView は ScaleControl 内で列幅も自動スケールする。
    // これを検知して OnColumnWidthChanged / OnRowHeadersWidthChanged で論理値の再保存を抑止する。
    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
        var prev = suppressMetricsUpdate;
        suppressMetricsUpdate = true;
        try
        {
            base.ScaleControl(factor, specified);
        }
        finally
        {
            suppressMetricsUpdate = prev;
        }
    }

    protected override void OnRowHeadersWidthChanged(EventArgs e)
    {
        base.OnRowHeadersWidthChanged(e);
        if (!suppressMetricsUpdate)
            logicalRowHeadersWidth = ToLogicalPixels(RowHeadersWidth);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && dpiSourceForm != null)
        {
            dpiSourceForm.DpiChanged -= DpiSourceForm_DpiChanged;
            dpiSourceForm = null;
        }
        base.Dispose(disposing);
    }

    /// <summary>
    /// デザイナで保持した論理値を現在の DPI に基づいて列幅・行ヘッダ幅へ反映する。
    /// 260518Cl 追加。
    /// </summary>
    public void ApplyDpiScaling()
    {
        // 260518Cl: 初回 ApplyDpiScaling 完了 = 構築フェーズ終了 として initialScalingDone を立てる。
        // 以降の OnColumnWidthChanged は現在 DPI 基準で論理値化される (旧実装は早期 return / finally の
        // 両方で代入していたが、ガードを try 内に移動して 1 箇所に集約)。
        var prev = suppressMetricsUpdate;
        suppressMetricsUpdate = true;
        try
        {
            if (!ScaleColumnWidthsForDpi && !ScaleRowHeadersWidthForDpi)
                return;

            // 260518Cl: dpi はループ前後で 1 回だけ取得し、StoreLogicalColumnMetrics にも引き渡す。
            //           旧実装は ToLogicalPixels 経由で列数分 GetDpiForWindow を呼んでいた。
            var dpi = CurrentDpi;
            var metricsDpi = MetricsDpi;
            if (ScaleColumnWidthsForDpi)
            {
                foreach (DataGridViewColumn column in Columns)
                {
                    if (!ShouldScale(column))
                        continue;

                    if (!columnMetrics.TryGetValue(column, out var metrics))
                    {
                        StoreLogicalColumnMetrics(column, metricsDpi);
                        metrics = columnMetrics[column];
                    }

                    var minimumWidth = FromLogicalPixels(metrics.MinimumWidth, dpi);
                    var width = Math.Max(FromLogicalPixels(metrics.Width, dpi), minimumWidth);
                    if (column.MinimumWidth != minimumWidth)
                        column.MinimumWidth = minimumWidth;
                    if (column.Width != width)
                        column.Width = width;
                }
            }

            if (ScaleRowHeadersWidthForDpi)
            {
                logicalRowHeadersWidth ??= ToLogicalPixels(RowHeadersWidth, metricsDpi);
                var rowHeadersWidth = FromLogicalPixels(logicalRowHeadersWidth.Value, dpi);
                if (RowHeadersWidth != rowHeadersWidth)
                    RowHeadersWidth = rowHeadersWidth;
            }
        }
        finally
        {
            suppressMetricsUpdate = prev;
            initialScalingDone = true;
        }
    }

    private void StoreLogicalColumnMetrics(DataGridViewColumn column)
        => StoreLogicalColumnMetrics(column, MetricsDpi); // 260518Cl 追加: dpi キャッシュなし版 (イベント経路用)

    private void StoreLogicalColumnMetrics(DataGridViewColumn column, int metricsDpi)
    {
        if (column == null)
            return;

        columnMetrics[column] = new ColumnMetrics(
            ToLogicalPixels(column.Width, metricsDpi),
            ToLogicalPixels(column.MinimumWidth, metricsDpi));
    }

    private void ScheduleDpiScaling()
    {
        if (!IsHandleCreated || IsDisposed || applyScheduled)
            return;

        applyScheduled = true;
        BeginInvoke((MethodInvoker)(() =>
        {
            applyScheduled = false;
            if (!IsDisposed)
                ApplyDpiScaling();
        }));
    }

    private void UpdateDpiSourceForm()
    {
        var form = FindForm();
        if (ReferenceEquals(form, dpiSourceForm))
            return;

        if (dpiSourceForm != null)
            dpiSourceForm.DpiChanged -= DpiSourceForm_DpiChanged;

        dpiSourceForm = form;
        if (dpiSourceForm != null)
            dpiSourceForm.DpiChanged += DpiSourceForm_DpiChanged;
    }

    private void DpiSourceForm_DpiChanged(object sender, DpiChangedEventArgs e)
        => ScheduleDpiScaling();

    private bool ShouldScale(DataGridViewColumn column)
    {
        if (column.AutoSizeMode != DataGridViewAutoSizeColumnMode.NotSet)
            return column.AutoSizeMode == DataGridViewAutoSizeColumnMode.None;

        return AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.None;
    }

    private int ToLogicalPixels(int pixels)
        => ToLogicalPixels(pixels, MetricsDpi);

    private static int ToLogicalPixels(int pixels, int metricsDpi)
        => Math.Max(1, (int)Math.Round(pixels * 96.0 / metricsDpi));

    private static int FromLogicalPixels(int logicalPixels, int dpi)
        => Math.Max(1, (int)Math.Round(logicalPixels * dpi / 96.0));

    private int CurrentDpi
    {
        get
        {
            // (260518Cl) Control.DeviceDpi can still be 96 while the window is actually on a scaled monitor.
            var dpi = IsHandleCreated ? GetDpiForWindow(Handle) : 0;
            if (dpi > 0)
                return dpi;

            dpi = dpiSourceForm?.IsHandleCreated == true ? GetDpiForWindow(dpiSourceForm.Handle) : 0;
            if (dpi > 0)
                return dpi;

            return DeviceDpi > 0 ? DeviceDpi : 96;
        }
    }

    // (260518Cl) デザイナ初期化中の Width 値は常に 96dpi 論理値として扱う。
    // 旧実装は IsHandleCreated で判定していたが、DataSource 設定等で
    // ハンドルが早期生成されるグリッドでデザイナ値が誤って物理ピクセル扱いされる不具合があった。
    // 初回 ApplyDpiScaling 完了 = 構築フェーズ終了 とみなし、それ以降は現在 DPI で論理値化する
    // (実行時のユーザリサイズも論理値ベースラインとして取り込まれる)。
    // private int MetricsDpi => IsHandleCreated ? CurrentDpi : 96; // 260518Cl 旧実装
    private int MetricsDpi => initialScalingDone ? CurrentDpi : 96;

    private readonly record struct ColumnMetrics(int Width, int MinimumWidth);

    [DllImport("user32.dll")]
    private static extern int GetDpiForWindow(IntPtr hwnd);
}
