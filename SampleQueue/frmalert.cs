using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleQueue
{
    public partial class frmalert : Form
    {
        Form1 frm;
        string docno = "";

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        public frmalert(Form1 f,string d)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));

            frm = f;docno = d;
        }

        public enum enmAction
        {
            wait,
            start,
            close
        }

        private enmAction Action;
        private int x, y;

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1;
            Action = enmAction.close;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (Action)
            {
                case enmAction.wait:
                    timer1.Interval = 5000;
                    Action = enmAction.close;
                    break;
                case enmAction.start:
                    timer1.Interval = 1;
                    Opacity += 0.1;
                    //if (x < Location.X) Left--;
                    //else
                    {
                        if (Opacity == 1.0) Action = enmAction.wait;
                    }
                    break;
                case enmAction.close:
                    timer1.Interval = 1;
                    Opacity -= 0.1;
                    Left -= 3;

                    if (base.Opacity == 0.0) base.Close();
                    break;
                default:
                    break;
            }
        }

        private void frmalert_MouseHover(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void frmalert_MouseLeave(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void frmalert_MouseEnter(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        public void ShowAlert(string message,string content)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.Stream = Properties.Resources.beep;
            player.Play();

            Opacity = 0.0;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            string frmname;

            for (int i = 1; i < 20; i++)
            {
                frmname = "alert " + i;

                frmalert frm = (frmalert)Application.OpenForms[frmname];

                if (frm == null)
                {
                    Name = frmname;

                    x = Screen.PrimaryScreen.WorkingArea.Width - Width + 15;
                    y = Screen.PrimaryScreen.WorkingArea.Height - Height * i;

                    Location = new Point(x, y);

                    break;
                }
            }

            x = Screen.PrimaryScreen.WorkingArea.X - base.Width - 5;

            lbmsg.Text = message;
            txtcontent.Text = content;

            Show();
            Action = enmAction.start;

            timer1.Interval = 1;
            timer1.Start();
        }

        private void lbmsg_Click(object sender, EventArgs e)
        {
            ShowData();
        }

        private void frmalert_Load(object sender, EventArgs e)
        {

        }

        private void ShowData()
        {
            for (int i = 1; i < 20; i++)
            {
                string frmname = "alert " + i;

                frmalert frm = (frmalert)Application.OpenForms[frmname];

                if (frm != null) frm.Close();
            }

            frm.Show();
            frm.OpenRow(docno);
        }
    }
}
