using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class FormCrystalSelection : Form
    {
        public FormCrystalSelection()
        {
            InitializeComponent();
            ShowCrystalInformation = false;
        }

        private bool saveMode = true;

        public bool SaveMode
        {
            set
            {
                saveMode = value; loadMode = !value;
                buttonLoadOrSave.Text = saveMode ? "Save" : "Load";
            }
            get { return saveMode; }
        }

        private bool loadMode = false;

        public bool LoadMode
        {
            set
            {
                loadMode = value; saveMode = !value;
                buttonLoadOrSave.Text = saveMode ? "Save" : "Load";
            }
            get { return loadMode; }
        }

        private bool showCrystalInformation = false;

        public bool ShowCrystalInformation
        {
            set
            {
                showCrystalInformation = value;
                if (showCrystalInformation)
                {
                    buttonExpand.Text = "<<<<<<<<<<";
                    panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left;
                    crystalControl1.Anchor = AnchorStyles.None;

                    crystalControl1.Size = new Size(crystalControl1.Width, this.ClientSize.Height);

                    this.ClientSize = new Size(panel1.Width + 4 + crystalControl1.Width, this.ClientSize.Height);
                    crystalControl1.Location = new Point(panel1.Width + 2, 0);
                    crystalControl1.Visible = true;

                    panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    crystalControl1.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right;
                }
                else
                {
                    buttonExpand.Text = ">>>>>>>>>>";
                    panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left;
                    this.ClientSize = new Size(panel1.Width, this.ClientSize.Height);
                    crystalControl1.Visible = false;
                    panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                }
            }
            get { return showCrystalInformation; }
        }

        public Crystal[] CheckedCrystalList
        {
            get
            {
                List<Crystal> crystalList = [];
                for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                    crystalList.Add((Crystal)checkedListBox1.CheckedItems[i]);
                return [.. crystalList];
            }
        }

        public void SetCrystalList(List<Crystal> crystals)
        {
            foreach (Crystal c in crystals)
                checkedListBox1.Items.Add(c, true);
        }

        private void buttonCheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, true);
        }

        private void buttonUnchekAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, false);
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex >= 0)
                crystalControl1.Crystal = (Crystal)checkedListBox1.SelectedItem;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (CheckedCrystalList.Length > e.Index && CheckedCrystalList[e.Index].Reserved)
                e.NewValue = CheckState.Checked;
        }

        private void buttonExpand_Click(object sender, EventArgs e)
        {
            ShowCrystalInformation = !showCrystalInformation;
        }
    }
}