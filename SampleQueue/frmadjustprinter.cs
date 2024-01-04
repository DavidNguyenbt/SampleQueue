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
    public partial class frmadjustprinter : Form
    {
        public frmadjustprinter()
        {
            InitializeComponent();
        }

        private void frmadjustprinter_Load(object sender, EventArgs e)
        {
            nbx.Value = AppConfig.X;
            nby.Value = AppConfig.Y;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AppConfig.X = int.Parse(nbx.Value.ToString());
            AppConfig.Y = int.Parse(nby.Value.ToString());

            Temp.SaveConfig();
        }
    }
}
