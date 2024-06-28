using CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
            if (Temp.User.Contains("CHO")) groupBox2.Enabled = true;
        }
        public void LoadData()
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
            if (cmbstyleadjustsmv.Text != "" && txtsmv.Text != "")
            {
                DataTable dt = kn.Doc("select * from sromstrassumesmv where Dept = '" + dept + "' and StyleNo = '" + cmbstyleadjustsmv.Text + "'").Tables[0];

                if (dt.Rows.Count > 0) kn.Ghi("update sromstrassumesmv set AssumeSMV = " + txtsmv.Text.Replace(",", ".") + " where Dept = '" + dept + "' and StyleNo = '" + cmbstyleadjustsmv.Text + "'");
                else
                    kn.Ghi("insert into sromstrassumesmv values ('" + dept + "','" + cmbstyleadjustsmv.Text + "'," + txtsmv.Text.Replace(",", ".") + ",getdate())");

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

        private void cmbstyleadjustsmv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtsmv_KeyPress(object sender, KeyPressEventArgs e)
        {
            char separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != separator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == separator) && ((sender as TextBox).Text.IndexOf(separator) > -1))
            {
                e.Handled = true;
            }
        }

        private void btimport_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Excel File (*.xlsx)|*.xlsx|Excel File (*.xls)|*.xls";
            op.Title = "Select your file";


            if (op.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(op.FileName);
                frmupload frm = new frmupload(op.FileName, true, dept, this);
                frm.ShowDialog();
            }
        }
    }
}
