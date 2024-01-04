using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleQueue
{
    public partial class frmgetdatafromERP : Form
    {
        public frmgetdatafromERP()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                string filename = textBox1.Text.ToUpper() + ".txt";

                if (!File.Exists(Path.Combine(Temp.path, filename))) File.Create(Path.Combine(Temp.path, filename)).Close();

                MessageBox.Show("Submitted Successfully !!!");
            }
        }
    }
}
