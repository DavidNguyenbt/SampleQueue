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
        frmitem frm;
        BackgroundWorker bgWorker;
        string style = "";
        DataTable Data = new DataTable();
        public frmfindstyle(frmitem f)
        {
            InitializeComponent();

            kn = new Connect(Temp.erp);

            frm = f;

            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += delegate
            {
                Data = kn.Doc("SELECT * FROM [AXDB].[dbo].[AllFGItem] WITH(NOLOCK) WHERE style = '" + style + "'", 3000).Tables[0];
            };
            bgWorker.RunWorkerCompleted += delegate
            {
                dtgdata.DataSource = Data;

                process.Visible = false;

                txtrows.Text = "Rows : " + Data.Rows.Count;
            };
        }
        private void LoadData()
        {
            try
            {
                if (txtstyle.Text != "")
                {
                    style = txtstyle.Text.ToUpper();

                    process.Visible = true;

                    bgWorker.RunWorkerAsync();
                }
            }
            catch { }
        }
        private void RunData()
        {
            try
            {
                DataGridViewRow row = new DataGridViewRow();

                if (dtgdata.SelectedRows.Count > 0) row = dtgdata.SelectedRows[0];
                else row = dtgdata.Rows[0];

                DataTable dt = new DataTable();
                dt.Columns.Add("smptype", typeof(string));
                dt.Columns.Add("unit", typeof(string));
                dt.Columns.Add("division", typeof(string));
                dt.Columns.Add("customer", typeof(string));
                dt.Columns.Add("programcode", typeof(string));
                dt.Columns.Add("gmttype", typeof(string));
                dt.Columns.Add("versioncount", typeof(decimal));
                dt.Columns.Add("smorderno", typeof(string));
                dt.Columns.Add("style", typeof(string));
                dt.Columns.Add("custstyle", typeof(string));
                dt.Columns.Add("fulldesc", typeof(string));
                dt.Columns.Add("season", typeof(string));

                DataRow dr = dt.NewRow();
                dr[0] = row.Cells[5].Value.ToString();
                dr[1] = row.Cells[6].Value.ToString();
                dr[2] = "";
                dr[3] = row.Cells[3].Value.ToString();
                dr[4] = "";
                dr[5] = row.Cells[8].Value.ToString();
                dr[6] = 1;
                dr[7] = row.Cells[0].Value.ToString();
                dr[8] = row.Cells[1].Value.ToString();
                dr[9] = row.Cells[1].Value.ToString();
                dr[10] = row.Cells[7].Value.ToString();
                dr[11] = row.Cells[2].Value.ToString();

                dt.Rows.Add(dr);
                dt.AcceptChanges();

                frm.ColorSize = Data;
                frm.FillData(dr);

                Hide();
            }
            catch { }
        }
        private void frmfindstyle_Load(object sender, EventArgs e)
        {
            txtstyle.Focus();
            process.Visible = false;
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btfind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void oKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunData();
        }

        private void dtgdata_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RunData();
        }

        private void txtstyle_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadData();
        }
    }
}
