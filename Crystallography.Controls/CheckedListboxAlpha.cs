using System.Windows.Forms;

namespace Crystallography.Controls
{
    [System.ComponentModel.ToolboxItem(true)] // 260605Cl 追加: 基底 UserControlBase の [ToolboxItem(false)] 継承を打ち消しデザイナのツールボックスに表示
    public partial class CheckedListboxAlpha : UserControlBase
    {
        public CheckedListboxAlpha()
        {
            InitializeComponent();
        }
    }
}
