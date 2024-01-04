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
    public partial class frmmoredata : Form
    {
        DataTable dt = new DataTable();
        string tt = "";
        public frmmoredata(DataTable dt, string t)
        {
            InitializeComponent();
            this.dt = dt; tt = t;
        }

        private void frmmoredata_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;

            lbrow.Text = "Rows : " + dataGridView1.RowCount;

            Text = tt;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                ExportToExcel.ToExcel excel = new ExportToExcel.ToExcel();
                excel.ExportToExcel(dataGridView1);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow r = dataGridView1.Rows[e.RowIndex];
                frmitem frm = new frmitem(true, false, new List<int>(), null);
                frm.DataRow = dt.Select("DocNo = '" + r.Cells[0].Value.ToString() + "'").FirstOrDefault();
                frm.ShowDialog();
            }
        }
    }
}
