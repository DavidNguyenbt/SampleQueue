using SampleQueue.Properties;
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
    public partial class frmSetColor : Form
    {
        Form1 frm;
        public frmSetColor(Form1 f)
        {
            InitializeComponent();

            frm = f;
        }

        private void frmSetColor_Load(object sender, EventArgs e)
        {
            btnew.BackColor = AppConfig.New;
            btincomplete.BackColor = AppConfig.Incomplete;
            btindecoration.BackColor = AppConfig.InDecoration;
            btinsewing.BackColor = AppConfig.InSewing;
            btinqueue.BackColor = AppConfig.InQueue;
            btfinishontime.BackColor = AppConfig.FinishOnTime;
            btfinishdelay.BackColor = AppConfig.FinishDelay;
            btcftpassed.BackColor = AppConfig.CFTPassed;
        }

        private void btnew_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                bt.BackColor = colorDialog1.Color;
            }
        }

        private void btexit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btdefault_Click(object sender, EventArgs e)
        {
            btnew.BackColor = Temp.New;
            btincomplete.BackColor = Temp.Incomplete;
            btindecoration.BackColor = Temp.InDecoration;
            btinsewing.BackColor = Temp.InSewing;
            btinqueue.BackColor = Temp.InQueue;
            btfinishontime.BackColor = Temp.FinishOnTime;
            btfinishdelay.BackColor = Temp.FinishDelay;
            btcftpassed.BackColor = Temp.CFTPassed;
        }

        private void btsave_Click(object sender, EventArgs e)
        {
            AppConfig.New = btnew.BackColor;
            AppConfig.Incomplete = btincomplete.BackColor;
            AppConfig.InDecoration = btindecoration.BackColor;
            AppConfig.InSewing = btinsewing.BackColor;
            AppConfig.InQueue = btinqueue.BackColor;
            AppConfig.FinishOnTime = btfinishontime.BackColor;
            AppConfig.FinishDelay = btfinishdelay.BackColor;
            AppConfig.CFTPassed = btcftpassed.BackColor;
            Temp.SaveConfig();

            frm.ReLoad();

            Close();
        }
    }
}
