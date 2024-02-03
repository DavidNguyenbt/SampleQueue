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
        List<string> color = new List<string>();
        List<string> size = new List<string>();
        public frmfindstyle(frmitem f)
        {
            InitializeComponent();

            kn = new Connect(Temp.erp);

            frm = f;
        }
        private void LoadData()
        {
            try
            {
                if (txtstyle.Text != "")
                {
                    DataTable dt = kn.Doc("SELECT * FROM [AXDB].[dbo].[AllFGItem] WHERE style LIKE '%" + txtstyle.Text.ToUpper() + "%'").Tables[0];

                    dtgdata.DataSource = dt;

                    color = dt.Select().Select(x => x["color"].ToString()).ToList();
                    size = dt.Select().Select(x => x["sizx"].ToString()).ToList();
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

                frm.color = color;
                frm.size = size;

                DataTable dt = new DataTable();
                dt.Columns.Add("smptype", typeof(string));
                dt.Columns.Add("smptype", typeof(string));
                dt.Columns.Add("smptype", typeof(string));
                dt.Columns.Add("smptype", typeof(string));
                dt.Columns.Add("smptype", typeof(string));
                dt.Columns.Add("smptype", typeof(string));
            }
            catch { }
        }
        private void frmfindstyle_Load(object sender, EventArgs e)
        {

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

        }

    }
}
