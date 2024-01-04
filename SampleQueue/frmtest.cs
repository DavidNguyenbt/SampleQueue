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
    public partial class frmtest : Form
    {
        public frmtest()
        {
            InitializeComponent();
        }
        public delegate void SEND(string s);
        public SEND goi;
        private void frmtest_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            goi(DateTime.Now.ToString("HH-mm-ss"));
        }
    }
}
