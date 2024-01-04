using CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleQueue
{
    public partial class frmsmv : Form
    {
        string dept = "ADS";
        Connect kn;
        DataSet ds = new DataSet();
        public frmsmv(Connect _kn)
        {
            InitializeComponent();

            kn = _kn;
        }

        private void frmsmv_Load(object sender, EventArgs e)
        {
            if (Temp.DeptDesc.ToUpper().Contains("ADIDAS") || Temp.DeptDesc.ToUpper().Contains("MERA")) dept = "ADS";
            else dept = "PUMA";

            if (Temp.Dept == "MER") panel1.Enabled = false;
            if (Temp.User == "CHO") panel1.Enabled = true;

            LoadData();
        }
        private void LoadData()
        {
            try
            {
                ds.Clear();
                ds = kn.Doc("select Code,Description,SMV from sromstrsmv where Dept = '" + dept + "' order by Code \n select distinct Code,Description from [SecurityReport].[dbo].[sygtype] order by Code");

                dataGridView1.DataSource = ds.Tables[0];

                cmbgmttype.Items.Clear();
                cmbgmttype.Items.AddRange(ds.Tables[1].Select().Select(s => s[0] + " - " + s[1]).ToArray());
            }
            catch { }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataRow[] row = ds.Tables[1].Select("Code = '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'");

                if (row.Length > 0)
                {
                    cmbgmttype.SelectedItem = row[0][0] + " - " + row[0][1];

                    string vl = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

                    numericUpDown1.Value = decimal.Parse(vl == "" ? "0" : vl);
                    //MessageBox.Show(numericUpDown1.Value.ToString());
                }
            }
        }

        private void btedit_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbgmttype.Text != "")
                {
                    string code = cmbgmttype.Text.Split('-')[0].Trim().ToUpper();

                    kn.Ghi("update sromstrsmv set SMV = " + numericUpDown1.Value.ToString().Replace(",", ".") + " where Code = '" + code + "'");

                    if (kn.ErrorMessage == "") LoadData();
                    else MessageBox.Show(kn.ErrorMessage);
                }
            }
            catch { }
        }

        private void btadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbgmttype.Text != "")
                {
                    string code = cmbgmttype.Text.Split('-')[0].Trim().ToUpper();
                    string desc = cmbgmttype.Text.Split('-')[1].Trim().ToUpper();

                    DataTable dt = kn.Doc("select * from sromstrsmv where Code = '" + code + "'").Tables[0];

                    if (dt.Rows.Count > 0) MessageBox.Show("Can't add new because it is existing !!!");
                    else
                    {
                        kn.Ghi("insert into sromstrsmv values ('" + dept + "','" + code + "','" + desc + "'," + numericUpDown1.Value.ToString().Replace(",", ".") + ",getdate())");

                        if (kn.ErrorMessage == "") LoadData();
                        else MessageBox.Show(kn.ErrorMessage);
                    }
                }
            }
            catch { }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
