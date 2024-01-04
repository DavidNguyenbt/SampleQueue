using CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleQueue
{
    public partial class frmassumesmv : Form
    {
        DataTable Data = new DataTable();
        Connect kn;
        string dept = "";
        public frmassumesmv(Connect _kn, string _dept)
        {
            InitializeComponent();
            kn = _kn; dept = _dept;
        }

        private void frmassumesmv_Load(object sender, EventArgs e)
        {
            LoadData();

            if (Temp.Dept == "MER") groupBox2.Enabled = false;
            if (Temp.User == "CHO") groupBox2.Enabled = false;
        }
        private void LoadData()
        {
            try
            {
                if (Data.Rows.Count > 0) Data.Rows.Clear();

                Data = kn.Doc("exec SampleQueueLoading 18, '" + dept + "', '', ''").Tables[0];

                dataGridView1.DataSource = Data;
                lbrow.Text = "Rows : " + dataGridView1.Rows.Count;

                cmbstyleadjustsmv.Items.Clear();
                cmbsearchstyle.Items.Clear();

                cmbsearchstyle.Items.AddRange(Data.Select().Select(s => s[0].ToString()).ToArray());
                cmbstyleadjustsmv.Items.AddRange(Data.Select().Select(s => s[0].ToString()).ToArray());
            }
            catch { }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) cmbstyleadjustsmv.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Data.Rows.Count > 0 && cmbsearchstyle.Text != "") dataGridView1.DataSource = Data.Select("style like '" + cmbsearchstyle.Text + "%'").CopyToDataTable();
            }
            catch
            {
                MessageBox.Show("Not Found !!!");
                dataGridView1.DataSource = Data;
            }

            lbrow.Text = "Rows : " + dataGridView1.Rows.Count;
        }

        private void cmbsearchstyle_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && Data.Rows.Count > 0) dataGridView1.DataSource = Data.Select("style like '" + cmbsearchstyle.Text + "%'").CopyToDataTable();
            }
            catch
            {
                MessageBox.Show("Not Found !!!");
                dataGridView1.DataSource = Data;
            }

            lbrow.Text = "Rows : " + dataGridView1.Rows.Count;
        }

        private void cmbsearchstyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Data.Rows.Count > 0 && cmbsearchstyle.Text != "") dataGridView1.DataSource = Data.Select("style = '" + cmbsearchstyle.Text + "'").CopyToDataTable();

            lbrow.Text = "Rows : " + dataGridView1.Rows.Count;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Data;
            lbrow.Text = "Rows : " + dataGridView1.Rows.Count;
        }

        private void btadjust_Click(object sender, EventArgs e)
        {
            if (cmbstyleadjustsmv.Text != "" && nmsmv.Value != 0)
            {
                DataTable dt = kn.Doc("select * from sromstrassumesmv where Dept = '" + dept + "' and StyleNo = '" + cmbstyleadjustsmv.Text + "'").Tables[0];

                if (dt.Rows.Count > 0) kn.Ghi("update sromstrassumesmv set AssumeSMV = " + nmsmv.Value.ToString().Replace(",", ".") + " where Dept = '" + dept + "' and StyleNo = '" + cmbstyleadjustsmv.Text + "'");
                else
                    kn.Ghi("insert into sromstrassumesmv values ('" + dept + "','" + cmbstyleadjustsmv.Text + "'," + nmsmv.Value.ToString().Replace(",", ".") + ",getdate())");

                if (kn.ErrorMessage == "")
                {
                    MessageBox.Show("Successfully !!!");
                    LoadData();
                }
                else MessageBox.Show(kn.ErrorMessage);
            }
        }

        private void nmsmv_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
