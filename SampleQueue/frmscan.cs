using CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SampleQueue
{
    public partial class frmscan : Form
    {
        Form1 frm;
        Connect kn;
        public frmscan(Form1 frm)
        {
            InitializeComponent();
            this.frm = frm;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 16) frm.OpenRow(textBox1.Text.Substring(0, 16));
        }

        private void frmscan_Load(object sender, EventArgs e)
        {
            LoadUSB();
        }
        private void LoadUSB()
        {
            try
            {
                int port = CFHidApi.CFHid_GetUsbCount();
                byte[] arrBuffer = new byte[512];
                CFHidApi.CFHid_GetUsbInfo(0, arrBuffer);
                string usb = System.Text.Encoding.Default.GetString(arrBuffer);

                if (CFHidApi.CFHid_OpenDevice((UInt16)0)) MessageBox.Show("Connected");
                else MessageBox.Show("Connect failed");
            }
            catch
            {
                MessageBox.Show("Cannot get the USB device !!!");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DataTable check = kn.Doc("exec SampleQueueLoading 23, '" + txtrfid.Text + "', '', ''").Tables[0];

            if (check.Rows.Count == 0)
            {
                MessageBox.Show("This RFID is not existing, please check again !!!");
                txtrfid.Text = "";
            }
            else frm.OpenRow(check.Rows[0]["DocNo"].ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            byte[] Data = new byte[35];
            byte[] Password = new byte[4];
            Password[0] = 0;
            Password[1] = 0;
            Password[2] = 0;
            Password[3] = 0;
            if (CFHidApi.CFHid_ReadCardG2(0xFF, Password, 0x01, 0x02, 0x06, Data) == true)
            {
                string rs = "";
                for (int i = 0; i < 12; i++)
                {
                    rs += Data[i].ToString("X2") + "-";
                }

                txtrfid.Text = rs.Substring(0, rs.Length - 1);
            }
            else
            {
                MessageBox.Show("Read Failed");
                txtrfid.Text = "";
            }
        }
    }
}
