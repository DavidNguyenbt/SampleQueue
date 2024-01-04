using CSDL;
using SampleQueue.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleQueue
{
    public partial class frmhelp : Form
    {
        bool show = true;
        int tip = 1;
        public frmhelp(int t = 1, bool _s = true)
        {
            InitializeComponent();

            show = _s; tip = t;
        }

        private void frmhelp_Load(object sender, EventArgs e)
        {
            switch (tip)
            {
                case 1:
                    txttip.Text = Temp.CapacityTip;
                    grtip.Text = "Sample Room Capacity Tip : ";
                    lbwelcome.Text += "Sample Room Capacity";
                    break;
                case 2:
                    txttip.Text = Temp.UrgentTip;
                    grtip.Text = "Sample Urgent Tip : ";
                    lbwelcome.Text += "Sample Urgent";
                    break;
                case 3:
                    txttip.Text = Temp.CommentTip;
                    grtip.Text = "Comment Management System (CMS) Tip : ";
                    lbwelcome.Text += "Comment Management System (CMS)";
                    break;
            }

            if (!show)
            {
                chkshow.Hide();
                lbwelcome.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (chkshow.Checked)
            {
                switch (tip)
                {
                    case 1:
                        AppConfig.CapacityTip = false;
                        break;
                    case 2:
                        AppConfig.UrgentTip = false;
                        break;
                    case 3:
                        AppConfig.CommentTip = false;
                        break;
                }
                Temp.SaveConfig();
            }
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            string path = @"\\192.168.1.245\Software\File";//AppDomain.CurrentDomain.BaseDirectory;//
            switch (tip)
            {
                case 1:
                    Process.Start(Path.Combine(path, "Comment Management System Guiding.pdf"));
                    break;
                case 2:
                    Process.Start(Path.Combine(path, "Comment Management System Guiding.pdf"));
                    break;
                case 3:
                    Process.Start(Path.Combine(path, "Comment Management System Guiding.pdf"));
                    break;
            }
        }
    }
}
