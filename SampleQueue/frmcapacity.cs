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
    public partial class frmcapacity : Form
    {
        Connect kn = new Connect(Temp.ch);
        DataTable dt = new DataTable();
        string ads = "";
        int currow = 0;
        bool repaint = true;
        public frmcapacity()
        {
            InitializeComponent();
        }

        private void frmcapacity_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btadd, "Add new value");
            toolTip1.SetToolTip(btactive, "Active");
            toolTip1.SetToolTip(btinactive, "Inactive");
            toolTip1.SetToolTip(btexport, "Export");
            toolTip1.SetToolTip(btclose, "Close");

            LoadData();

            //webBrowser1.Url = new Uri("http://171.10.0.122/5/on");
            webBrowser1.Hide();

        }

        public void LoadData()
        {
            if (Temp.DeptDesc.ToUpper().Contains("ADIDAS") || Temp.DeptDesc.ToUpper().Contains("MERA")) ads = "ADS"; else ads = "PUMA";

            if (dt.Rows.Count > 0) dt.Rows.Clear();
            dt = kn.Doc("exec GetLoadData 34,'" + ads + "',''").Tables[0];

            dataGridView1.DataSource = dt.DefaultView.ToTable(false, "Inactive", "ID", "ManPower", "WorkingHour", "Absence", "EFF", "Capacity", "DesignatedDate", "SysCreateDate");

            txtrow.Text = "Rows : " + dataGridView1.RowCount;

            repaint = true;
        }

        private void btadd_Click(object sender, EventArgs e)
        {
            frminputcap frm = new frminputcap(this, ads);
            frm.ShowDialog();
        }

        private void btclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btexport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                ExportToExcel.ToExcel excel = new ExportToExcel.ToExcel();
                excel.ExportToExcel(dataGridView1);
            }
        }

        private void btinactive_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.Rows[currow].Cells[1].Value.ToString();

            kn.Ghi("update sromstrcapacity set Inactive = '0' where ID = " + id);

            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) currow = e.RowIndex;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            if (repaint)
            {
                foreach (DataGridViewRow rw in dataGridView1.Rows)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)rw.Cells[0];
                    cell.ValueType = typeof(bool);
                    cell.Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };

                    if (rw.Cells[0].Value.ToString() == "0") cell.Value = false;
                    else cell.Value = true;
                }

                repaint = false;
            }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            repaint = true;
        }

        private void btactive_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.Rows[currow].Cells[1].Value.ToString();

            kn.Ghi("update sromstrcapacity set Inactive = '' where ID = " + id);

            LoadData();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (Temp.Dept != "SMP" && Temp.User != "CHO")
            {
                btactive.Enabled = false;
                btadd.Enabled = false;
                btinactive.Enabled = false;
            }
        }
    }
}
