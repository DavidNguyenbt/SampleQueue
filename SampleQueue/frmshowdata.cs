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
    public partial class frmshowdata : Form
    {
        DataTable dt = new DataTable();
        public frmshowdata(DataTable d)
        {
            InitializeComponent();

            dt = d;
        }

        private void frmshowdata_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;
        }
    }
}
