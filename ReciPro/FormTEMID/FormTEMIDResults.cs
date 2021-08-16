using Crystallography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ReciPro
{
    public partial class FormTEMIDResults : Form
    {
        public FormTEMID formTEMID = new FormTEMID();
        public List<ZoneAxes> zoneAxes;
        public List<ZoneAxis> zoneAxis;

        public FormTEMIDResults() => InitializeComponent();

        public FormTEMIDResults(FormTEMID formTEMID)
        {
            InitializeComponent();
            this.formTEMID = formTEMID;
        }

        public void SetDataSet(PhotoInformation photo, List<ZoneAxis> zoneaxis)
        {
            dataGridView2.Visible = false;
            label1.Text = zoneaxis.Count.ToString() + " candidates are found.";
            this.zoneAxis = zoneaxis;

            for (int i = 0; i < zoneaxis.Count; i++)
                if (zoneaxis[i].plane1.IsRootIndex)
                    dataSet.Tables[0].Rows.Add(GetTabelRows(i, photo, zoneaxis[i]));
            if (photo.IsTriangleMode)
            {
                dataGridView1.Columns[5].Visible = true;
                dataGridView1.Columns[6].Visible = true;
                dataGridView1.Columns[7].Visible = false;
            }
            else
            {
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = true;
            }
        }

        public void SetDataSet(double[] obsAngle, List<ZoneAxes> zoneaxes)
        {
            dataGridView1.Visible = false;
            label1.Text = zoneaxes.Count.ToString() + " candidates are found.";
            this.zoneAxes = zoneaxes;
            for (int i = 0; i < zoneaxes.Count; i++)
                dataSet.Tables[1].Rows.Add(GetTabelRows(i, obsAngle, zoneaxes[i]));

            if (zoneaxes[0].IsTwoPhoho)
            {
                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Columns[5].Visible = false;
                dataGridView2.Columns[6].Visible = false;
            }
        }

        public static object[] GetTabelRows(int i, PhotoInformation photo, ZoneAxis zoneaxis) => new object[] {
                i,
                $"[{zoneaxis.u} {zoneaxis.v} {zoneaxis.w}]",

                $"{zoneaxis.plane1.h} {zoneaxis.plane1.k} {zoneaxis.plane1.l}",
                $"{zoneaxis.plane1.d:f3}nm ({(zoneaxis.plane1.d-photo.d1)/photo.d1 * 100:f3}%)",

                $"{zoneaxis.plane2.h} {zoneaxis.plane2.k} {zoneaxis.plane2.l}",
                $"{zoneaxis.plane2.d:f3}nm ({(zoneaxis.plane2.d-photo.d2)/photo.d2 * 100:f3}%)",

                $"{zoneaxis.plane3.h} {zoneaxis.plane3.k} {zoneaxis.plane3.l}",
                $"{zoneaxis.plane3.d:f3}nm ({(zoneaxis.plane3.d-photo.d3)/photo.d3 * 100:f3}%)",

                $"{zoneaxis.Theta/Math.PI*180:f3}‹ ({(zoneaxis.Theta-photo.Theta)/Math.PI*180:f3}‹)",

                zoneaxis.Phase
            };

        public static object[] GetTabelRows(int i, double[] obsAngle, ZoneAxes zoneaxes) => new object[] {
                i,
                $"[{zoneaxes.Za1.u} {zoneaxes.Za1.v} {zoneaxes.Za1.w}]",
                $"[{zoneaxes.Za2.u} {zoneaxes.Za2.v} {zoneaxes.Za2.w}]",
                $"[{zoneaxes.Za3.u} {zoneaxes.Za3.v} {zoneaxes.Za3.w}]",
                $"{zoneaxes.AngleBet12/Math.PI*180:f3}‹ ({(zoneaxes.AngleBet12-obsAngle[0])/Math.PI*180:f3}‹)",
                $"{zoneaxes.AngleBet23/Math.PI*180:f3}‹ ({(zoneaxes.AngleBet23-obsAngle[1])/Math.PI*180:f3}‹)",
                $"{zoneaxes.AngleBet31/Math.PI*180:f3}‹ ({(zoneaxes.AngleBet31-obsAngle[2])/Math.PI*180:f3}‹)",

                zoneaxes.Za1.Phase
            };

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int i = (int)((DataRowView)bindingSource1.Current).Row[0];
                for (int j = 0; j < formTEMID.formMain.listBox.Items.Count; j++)
                {
                    if ((Crystal)formTEMID.formMain.listBox.Items[j] == zoneAxis[i].Phase)
                    {
                        formTEMID.formMain.listBox.SelectionMode = SelectionMode.One;
                        formTEMID.formMain.listBox.SelectedIndex = j;
                        formTEMID.formMain.listBox.SelectionMode = SelectionMode.MultiExtended;
                        break;
                    }
                }
                formTEMID.formMain.SetRotation(Euler.SerchEulerAngleFromZoneAxes(zoneAxis[i], formTEMID.formMain.Crystal));
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = (int)((DataRowView)bindingSource2.Current).Row[0];
            if (zoneAxes[i].IsTwoPhoho)
            {
                for (int j = 0; j < formTEMID.formMain.listBox.Items.Count; j++)
                {
                    if ((Crystal)formTEMID.formMain.listBox.Items[j] == zoneAxis[i].Phase)
                    {
                        formTEMID.formMain.listBox.SelectionMode = SelectionMode.One;
                        formTEMID.formMain.listBox.SelectedIndex = j;
                        formTEMID.formMain.listBox.SelectionMode = SelectionMode.MultiExtended;
                        break;
                    }
                }
                formTEMID.formMain.SetRotation(Euler.SerchEulerAngleFromZoneAxes(zoneAxes[i].Za1, zoneAxes[i].Za2, formTEMID.formMain.Crystal));
            }
            else
            {
                for (int j = 0; j < formTEMID.formMain.listBox.Items.Count; j++)
                {
                    if ((Crystal)formTEMID.formMain.listBox.Items[j] == zoneAxis[i].Phase)
                    {
                        formTEMID.formMain.listBox.SelectionMode = SelectionMode.One;
                        formTEMID.formMain.listBox.SelectedIndex = j;
                        formTEMID.formMain.listBox.SelectionMode = SelectionMode.MultiExtended;
                        break;
                    }
                }
                formTEMID.formMain.SetRotation(Euler.SerchEulerAngleFromZoneAxes(zoneAxes[i].Za1, zoneAxes[i].Za2, zoneAxes[i].Za3, formTEMID.formMain.Crystal));
            }
        }
    }
}