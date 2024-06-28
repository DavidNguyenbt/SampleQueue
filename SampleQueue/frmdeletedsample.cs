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
    public partial class frmdeletedsample : Form
    {
        Connect kn = new Connect(Temp.ch);
        public frmdeletedsample()
        {
            InitializeComponent();
        }

        private void frmdeletedsample_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = kn.Doc("exec SampleQueueLoading 46, '" + Temp.User + "', '', ''").Tables[0];

            dtgdata.DataSource = dt;
        }

        private void frmdeletedsample_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbrow.Text = "Rows : " + dtgdata.RowCount;
        }

        private void restoreSampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dtgdata.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dtgdata.SelectedRows[0];

                string docno = row.Cells["DocNo"].Value.ToString();

                //MessageBox.Show(docno);
                Clipboard.SetText(docno);

                string qy = "exec SampleQueueLoading 45, '" + docno + "', '', ''";

                DataTable d = kn.Doc(qy).Tables[0];

                if (d.Rows.Count > 0)
                {
                    frmitem frm = new frmitem(true, false, new List<int>(), null);
                    frm.DataRow = d.Rows[0];

                    frm.ShowDialog();
                }
            }
        }
    }
}
