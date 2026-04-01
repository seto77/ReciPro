using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace Crystallography.Controls;

/// <summary>(260323Ch) Form / UserControl / Control / ToolStripItem に設計時の Capture フラグを付与する extender component。</summary>
[ProvideProperty("Capture", typeof(Component))]
[DesignerCategory("Component")]
[ToolboxItem(true)]
public sealed class CaptureExtender : Component, IExtenderProvider
{
    private readonly Dictionary<Component, bool> captureTargets = new();

    public CaptureExtender()
    {
    }

    public CaptureExtender(IContainer container)
        : this()
    {
        container?.Add(this); // (260323Ch) designer から components コンテナ付きで生成できるようにする
    }

    public bool CanExtend(object extendee)
    {
        return extendee is Control or ToolStripItem && !ReferenceEquals(extendee, this);
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("FormCaptureGUI で個別キャプチャ対象に含めるかどうかを指定します。")]
    public bool GetCapture(Component extendee)
    {
        return extendee != null
            && captureTargets.TryGetValue(extendee, out var capture)
            && capture;
    }

    public void SetCapture(Component extendee, bool value)
    {
        if (extendee == null || !CanExtend(extendee))
            return;

        // captureTargets[extendee] = value; // 旧実装案: false も辞書に保持する
        if (value)
        {
            captureTargets[extendee] = true; // (260323Ch) デフォルト false なので true のみ保持して designer serialize を簡潔にする
            extendee.Disposed -= Extendee_Disposed;
            extendee.Disposed += Extendee_Disposed;
        }
        else if (captureTargets.Remove(extendee))
        {
            extendee.Disposed -= Extendee_Disposed;
        }
    }

    public bool ShouldSerializeCapture(Component extendee) => GetCapture(extendee);

    public void ResetCapture(Component extendee) => SetCapture(extendee, false);

    private void Extendee_Disposed(object sender, EventArgs e)
    {
        if (sender is Component extendee)
        {
            extendee.Disposed -= Extendee_Disposed;
            captureTargets.Remove(extendee);
        }
    }

    internal bool HasCaptureTargets() => captureTargets.Count > 0;

    internal static bool IsCaptureEnabled(Component extendee)
    {
        foreach (var extender in EnumerateCandidateExtenders(extendee))
        {
            if (extender.GetCapture(extendee))
                return true;
        }
        return false;
    }

    internal static bool HasAnyCaptureTargets(Control root)
    {
        if (root == null) return false;

        var visitedContainers = new HashSet<object>();
        foreach (var container in EnumerateVisualContainers(root))
        {
            if (!visitedContainers.Add(container)) continue;

            foreach (var extender in GetExtenders(container))
            {
                if (extender.HasCaptureTargets())
                    return true;
            }
        }
        return false;
    }

    private static IEnumerable<object> EnumerateVisualContainers(Control root)
    {
        yield return root;

        foreach (Control child in root.Controls)
        {
            foreach (var descendant in EnumerateVisualContainers(child))
                yield return descendant;
        }

        if (root is ToolStripContainer toolStripContainer)
        {
            foreach (var panel in new Control[]
                     {
                         toolStripContainer.TopToolStripPanel,
                         toolStripContainer.BottomToolStripPanel,
                         toolStripContainer.LeftToolStripPanel,
                         toolStripContainer.RightToolStripPanel,
                         toolStripContainer.ContentPanel
                     })
            {
                foreach (var descendant in EnumerateVisualContainers(panel))
                    yield return descendant;
            }
        }

        if (root is SplitContainer splitContainer)
        {
            foreach (var panel in new Control[] { splitContainer.Panel1, splitContainer.Panel2 })
            {
                foreach (var descendant in EnumerateVisualContainers(panel))
                    yield return descendant;
            }
        }
    }

    private static IEnumerable<CaptureExtender> EnumerateCandidateExtenders(Component extendee)
    {
        var visitedContainers = new HashSet<object>();

        foreach (var container in EnumerateOwningContainers(extendee))
        {
            if (!visitedContainers.Add(container)) continue;

            foreach (var extender in GetExtenders(container))
                yield return extender;
        }
    }

    private static IEnumerable<object> EnumerateOwningContainers(Component extendee)
    {
        var stack = new Stack<object>();
        var visited = new HashSet<object>();
        stack.Push(extendee);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (!visited.Add(current)) continue;

            yield return current;

            switch (current)
            {
                case ToolStripItem item:
                    if (item.OwnerItem != null) stack.Push(item.OwnerItem);
                    if (item.Owner != null) stack.Push(item.Owner);
                    break;

                case Control control when control.Parent != null:
                    stack.Push(control.Parent);
                    break;
            }
        }
    }

    private static IEnumerable<CaptureExtender> GetExtenders(object container)
    {
        var visitedExtenders = new HashSet<CaptureExtender>();
        for (var type = container.GetType(); type != null; type = type.BaseType)
        {
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
            {
                if (field.FieldType == typeof(CaptureExtender) && field.GetValue(container) is CaptureExtender extender && visitedExtenders.Add(extender))
                    yield return extender; // (260323Ch) base Form / base UserControl に置いた CaptureExtender も拾えるようにする
            }
        }
    }
}
