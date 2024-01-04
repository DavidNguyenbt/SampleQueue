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
    public partial class frmaddidqty : Form
    {
        public int qty = 1;
        public frmaddidqty()
        {
            InitializeComponent();
        }

        private void btclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btapply_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            qty = int.Parse(nmqty.Value.ToString());
            Close();
        }
    }
}
