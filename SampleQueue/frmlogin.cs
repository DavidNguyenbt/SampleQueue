using CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using vCardLib.Models;
using vCardLib.Serializers;

namespace SampleQueue
{
    public partial class frmlogin : Form
    {
        Connect kn;

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
        public frmlogin()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));

            kn = new Connect(Temp.ch);

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi-VN");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = kn.Doc("select * from nguser where Username = '" + txtuser.Text.Replace("--", "") + "' and Passwd = '" + txtpass.Text + "'").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];

                    Temp.User = r[0].ToString();
                    Temp.Dept = r[4].ToString();
                    Temp.DeptDesc = r[1].ToString();
                    Temp.Profile = r[7].ToString();

                    AppConfig.User = Temp.User; Temp.SaveConfig();

                    if (Temp.IsValidPassword(txtpass.Text))
                    {
                        Form1 frm = new Form1();
                        frm.Show();

                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("You must change your password according to IMS policy !!!");
                        frmchangeuserpass frm = new frmchangeuserpass(txtpass.Text);
                        frm.ShowDialog();

                        txtpass.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Log in failed !!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtpass.Focus();
                }

                Temp.url = kn.Doc("select * from InlineQCSystem where STT = 8").Tables[0].Rows[0][0].ToString();
            }
            catch { }
        }

        private void frmlogin_Load(object sender, EventArgs e)
        {
            try
            {
                string mydocu = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Sample Queue");
                if (!Directory.Exists(mydocu)) Directory.CreateDirectory(mydocu);

                Temp.myconfig = Path.Combine(mydocu, "config.txt");
                string myconfig = Temp.myconfig;

                if (!File.Exists(myconfig))
                {
                    File.Create(myconfig).Close();

                    Temp.SaveConfig();
                }
                else
                {
                    StreamReader rd = new StreamReader(myconfig);

                    string ch = rd.ReadToEnd();

                    if (ch != "")
                    {
                        List<string> config = ch.Split('\n').ToList();

                        AppConfig.New = Color.FromName(config.Find(s => s.Contains("New")).Split(':')[1]);
                        AppConfig.Incomplete = Color.FromName(config.Find(s => s.Contains("Incomplete")).Split(':')[1]);
                        AppConfig.InDecoration = Color.FromName(config.Find(s => s.Contains("InDecoration")).Split(':')[1]);
                        AppConfig.InSewing = Color.FromName(config.Find(s => s.Contains("InSewing")).Split(':')[1]);
                        AppConfig.FinishOnTime = Color.FromName(config.Find(s => s.Contains("FinishOnTime")).Split(':')[1]);
                        AppConfig.FinishDelay = Color.FromName(config.Find(s => s.Contains("FinishDelay")).Split(':')[1]);
                        AppConfig.InQueue = Color.FromName(config.Find(s => s.Contains("InQueue")).Split(':')[1]);
                        AppConfig.User = config.Find(s => s.Contains("User")).Split(':')[1];
                        AppConfig.CFTPassed = Color.FromName(config.Find(s => s.Contains("CFTPassed")).Split(':')[1]);
                        AppConfig.FilterColumn1 = config.Find(s => s.Contains("FilterColumn1")).Split(':')[1];
                        AppConfig.FilterColumn2 = config.Find(s => s.Contains("FilterColumn2")).Split(':')[1];
                        AppConfig.Notification = bool.Parse(config.Find(s => s.Contains("Notification")).Split(':')[1]);
                        AppConfig.ShowNews = bool.Parse(config.Find(s => s.Contains("ShowNews")).Split(':')[1]);
                        AppConfig.CapacityTip = bool.Parse(config.Find(s => s.Contains("CapacityTip")).Split(':')[1]);
                        AppConfig.UrgentTip = bool.Parse(config.Find(s => s.Contains("UrgentTip")).Split(':')[1]);
                        AppConfig.CommentTip = bool.Parse(config.Find(s => s.Contains("CommentTip")).Split(':')[1]);
                        if (config.Exists(s => s.Contains("PrinterX"))) AppConfig.X = int.Parse(config.Find(s => s.Contains("PrinterX")).Split(':')[1]);
                        if (config.Exists(s => s.Contains("PrinterY"))) AppConfig.Y = int.Parse(config.Find(s => s.Contains("PrinterY")).Split(':')[1]);
                    }

                    rd.Close();
                }
            }
            catch { MessageBox.Show("Cannot load config file !!!"); }

            txtuser.Text = AppConfig.User;

        }

        private void label1_Click(object sender, EventArgs e)
        {
            vCard card = new vCard();

            card.GivenName = "Nguyen Van Cho";
            card.NickName = "David";
            card.Organization = "A1A Company";
            card.CustomFields.Add(new KeyValuePair<string, string>("X-ZALO", "0399911775"));

            string filePath = "contact.vcf";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                string vc = v3Serializer.Serialize(card);
                writer.Write(vc);
                writer.Close();
            }
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
