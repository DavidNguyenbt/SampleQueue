using CSDL;
using SampleQueue.Properties;
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
    public partial class frmfindstyle : Form
    {
        Connect kn;
        BackgroundWorker worker;
        DataTable dt = new DataTable();
        frmitem frm;
        int rowindex = 0, run = 0;
        public frmfindstyle(frmitem f)
        {
            InitializeComponent();

            kn = new Connect(Temp.ch);

            frm = f;

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.DoWork += Worker_DoWork;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (run == 0) LoadData();
            else Run();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            processbar.Visible = false;

            if (run != 0) Close();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void LoadData()
        {
            try
            {
                string ads = "";
                if (Temp.DeptDesc.ToUpper().Contains("ADIDAS") || Temp.DeptDesc.ToUpper().Contains("MERA")) ads = "ADS";

                dt = kn.Doc("exec SampleQueueLoading 1,'" + ads + "','',''").Tables[0];

                Invoke((Action)(() =>
                {
                    dataGridView1.DataSource = dt; txtrows.Text = "Rows : " + dataGridView1.RowCount;

                    cmbcolumn.Items.AddRange(dt.Columns.OfType<DataColumn>().Select(c => c.ColumnName).ToArray());

                    cmbcolumn.Text = AppConfig.FilterColumn2;
                }));
            }
            catch { }
        }
        private void frmfindstyle_Load(object sender, EventArgs e)
        {
            worker.RunWorkerAsync();
        }

        private void cmbcolumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable d = dataGridView1.DataSource as DataTable;
                cmbvalue.Items.Clear();
                cmbvalue.Text = "";
                cmbvalue.Items.AddRange(d.Select().Select(r => r[cmbcolumn.Text].ToString()).Distinct().ToArray());

                AppConfig.FilterColumn2 = cmbcolumn.Text;
                Temp.SaveConfig();
            }
            catch { }
        }
        int len = 0;
        private void cmbvalue_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void refeshDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt; txtrows.Text = "Rows : " + dataGridView1.RowCount;cmbvalue.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    dataGridView1.Rows[e.RowIndex].Selected = true; rowindex = e.RowIndex;
            //}
            if (e.RowIndex > 0) rowindex = e.RowIndex;
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void oKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            run = 1;
            processbar.Visible = true;

            worker.RunWorkerAsync();
        }

        private void cmbcolumn_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                rowindex = e.RowIndex;
                run = 1;
                processbar.Visible = true;

                worker.RunWorkerAsync();
            }
        }

        private void cmbvalue_Click(object sender, EventArgs e)
        {

        }

        private void cmbvalue_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                try
                {
                    DataTable d = cmbvalue.Text.Length > len ? (dataGridView1.DataSource as DataTable) : dt;

                    dataGridView1.DataSource = d.Select(cmbcolumn.Text + " like '%" + cmbvalue.Text + "%'").CopyToDataTable();
                    txtrows.Text = "Rows : " + dataGridView1.RowCount;

                    len = cmbvalue.Text.Length;
                }
                catch { }
            }
        }

        private void Run()
        {
            try
            {
                DataTable d = dataGridView1.DataSource as DataTable;

                Invoke((Action)(() =>
                {
                    frm.FillData(d.Rows[rowindex]);
                }));
            }
            catch { }
        }
    }
}
