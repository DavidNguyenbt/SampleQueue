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
    public partial class frmthongbao : Form
    {
        int dem = 0;
        string str = "";
        public frmthongbao(string s)
        {
            InitializeComponent();

            str = s;
        }

        private void frmthongbao_Load(object sender, EventArgs e)
        {
            txtthongbao.Text = str;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dem > 5) Close();

            dem++;
        }
    }
}
