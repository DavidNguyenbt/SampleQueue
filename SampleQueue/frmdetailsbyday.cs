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
    public partial class frmdetailsbyday : Form
    {
        string ads = "ADS";
        Connect kn;
        DateTime date = DateTime.Now;
        public frmdetailsbyday(DateTime d, Connect _kn, string _ads)
        {
            InitializeComponent();
            date = d; kn = _kn; ads = _ads;
        }

        private void frmdetailsbyday_Load(object sender, EventArgs e)
        {
            Text = "Details by Day : " + date.ToString("dd-MM-yyyy");
            dataGridView1.DataSource = kn.Doc("exec SampleQueueLoading 17, '" + ads + "', '" + date.ToString("yyyyMMdd") + "', ''").Tables[0];

            //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
        }
    }
}
