using CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleQueue
{
    public partial class frmlimit : Form
    {
        Connect kn = new Connect(Temp.ch);
        string ads = "PUMA";
        System.Globalization.CultureInfo culture;
        public frmlimit()
        {
            InitializeComponent();

            culture = System.Threading.Thread.CurrentThread.CurrentCulture;
        }

        private void frmlimit_Load(object sender, EventArgs e)
        {
            dateTimePicker2.Value = DateTime.Now.AddMonths(1);

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (Temp.DeptDesc.ToUpper().Contains("ADIDAS") || Temp.DeptDesc.ToUpper().Contains("MERA")) ads = "ADS"; else ads = "PUMA";

                DataTable dt = kn.Doc("exec SampleQueueLoading 9,'" + ads + "','" + dateTimePicker1.Value.ToString("yyyyMMdd") + "','" + dateTimePicker2.Value.ToString("yyyyMMdd") + "'").Tables[0];

                dataGridView1.DataSource = dt;
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExportToExcel.ToExcel excel = new ExportToExcel.ToExcel();
            excel.ExportToExcel(dataGridView1);
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi-VN");
            try
            {
                if (dataGridView1.RowCount > 0)
                {
                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        //DataGridViewCell cl = r.Cells[1];

                        //int qty = string.IsNullOrEmpty(cl.Value.ToString()) ? 0 : int.Parse(cl.Value.ToString());

                        //if (qty > 120) cl.Style.BackColor = Color.Red;
                        //else if (qty > 100) cl.Style.BackColor = Color.Pink;
                        //else cl.Style.BackColor = Color.LightGreen;

                        float cap = string.IsNullOrEmpty(r.Cells[2].Value.ToString()) ? 0 : float.Parse(r.Cells[2].Value.ToString().Replace(".", ","));
                        float cur = string.IsNullOrEmpty(r.Cells[3].Value.ToString()) ? 0 : float.Parse(r.Cells[3].Value.ToString().Replace(".", ","));
                        float limit = cur - (cur * 5 / 100);

                        if (cap >= cur) r.DefaultCellStyle.BackColor = Color.Red;
                        else if (cap >= limit) r.DefaultCellStyle.BackColor = Color.Pink;
                        //else r.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                }
            }
            catch { }
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
        }
        int row = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) row = e.RowIndex;
        }

        private void detailsByDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string ch = dataGridView1.Rows[row].Cells[0].Value.ToString();
                DateTime date = DateTime.Parse(ch);

                frmdetailsbyday frm = new frmdetailsbyday(date, kn, ads);
                frm.ShowDialog();
            }
            catch { }
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Right click to see more details !!!";
            }
        }
    }
}
