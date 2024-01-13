using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Crystallography
{
    public enum Languages { English, Japanese };

    public static class Language
    {
        public static void Change(object obj, ComponentResourceManager resources = null)
        {
            if (obj is Form form)
            {
                var form_resources = new ComponentResourceManager(form.GetType());
                form_resources.ApplyResources(form, "$this", Thread.CurrentThread.CurrentUICulture);
                foreach (var fm in form.OwnedForms) { Change(fm, form_resources); }
                foreach (var fm in form.MdiChildren) { Change(fm, form_resources); }
                foreach (var c in form.Controls) { Change(c, form_resources); }
            }
            else if (obj is ToolStripItem item)
            {
                resources.ApplyResources(item, item.Name, Thread.CurrentThread.CurrentUICulture);
                if (item is ToolStripDropDownItem dditem)
                    foreach (ToolStripItem tsi in dditem.DropDownItems) { Change(tsi, resources); }
            }
            else if (obj is Control ctrl)
            {
                resources.ApplyResources(ctrl, ctrl.Name, Thread.CurrentThread.CurrentUICulture);
                foreach (Control c in ctrl.Controls) { Change(c, resources); }
                if (ctrl is ToolStrip ts)
                    foreach (ToolStripItem tsi in ts.Items) { Change(tsi, resources); }
            }
        }
    }
}