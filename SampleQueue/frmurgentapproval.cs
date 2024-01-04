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
    public partial class frmurgentapproval : Form
    {
        Connect kn;
        string docno = "";
        public frmurgentapproval(Connect _kn, string _docno)
        {
            InitializeComponent();

            kn = _kn; docno = _docno;
        }

        private void frmurgentapproval_Load(object sender, EventArgs e)
        {

        }
    }
}
