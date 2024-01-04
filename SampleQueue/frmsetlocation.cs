using CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleQueue
{
    public partial class frmsetlocation : Form
    {
        string spid = "", usb = "";
        frmsqid frm;
        Connect kn;
        public frmsetlocation(string spid, frmsqid frm, Connect kn)
        {
            InitializeComponent();
            this.spid = spid;
            this.frm = frm;
            this.kn = kn;
        }

        private void btexit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btapply_Click(object sender, EventArgs e)
        {
            if (cmbstorage.Text == "")
            {
                MessageBox.Show("No have the storage name !!!");
                cmbstorage.Focus();
            }
            else if (txtrfid.Text == "")
            {
                MessageBox.Show("No have the RFID !!!");
                txtrfid.Focus();
            }
            else
            {
                DataTable check = kn.Doc("exec SampleQueueLoading 23, '" + txtrfid.Text + "', '', ''").Tables[0];

                if (check.Rows.Count > 0)
                {
                    MessageBox.Show("This RFID is existing, please check again !!!");
                    txtrfid.Text = "";
                }
                else
                {
                    string qry = "exec SampleQueueLoading 21, '" + spid + "', '" + cmbstorage.Text + "', '" + txtrfid.Text + "'";

                    if (txtlocation.Text != "") qry += "\n exec SampleQueueLoading 26, '" + spid + "', '" + txtlocation.Text + "', ''";

                    kn.Ghi(qry);
                    frm.Reload(spid);

                    Close();
                }
            }
        }

        private void frmsetlocation_Load(object sender, EventArgs e)
        {
            lbspid.Text += spid;

            LoadUSB();

            DataTable dt = kn.Doc("select * from [DtradeProduction].[dbo].[00SampleType] where SampleType = 'Storage'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                cmbstorage.Items.AddRange(dt.Select().Select(s => s[1].ToString()).ToArray());
                cmbstorage.SelectedIndex = 0;
            }
        }
        private void LoadUSB()
        {
            try
            {
                int port = CFHidApi.CFHid_GetUsbCount();
                byte[] arrBuffer = new byte[512];
                CFHidApi.CFHid_GetUsbInfo(0, arrBuffer);
                usb = System.Text.Encoding.Default.GetString(arrBuffer);

                if (CFHidApi.CFHid_OpenDevice((UInt16)0)) MessageBox.Show("Connected");
                else MessageBox.Show("Connect failed");
            }
            catch
            {
                MessageBox.Show("Cannot get the USB device !!!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmlocation frm = new frmlocation(1, spid, kn, this, this.frm);
            frm.ShowDialog();
        }
        public void LoadLocation(string location)
        {
            txtlocation.Text = location;
        }
        private void button1_Click(object sender, EventArgs e)
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
