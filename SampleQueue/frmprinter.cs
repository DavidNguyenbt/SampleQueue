using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Forms;
using static QRCoder.PayloadGenerator;
using QRCoder;
using Point = System.Drawing.Point;
using CSDL;
using FontStyle = System.Drawing.FontStyle;
using DevExpress.Printing;
using DevExpress.Utils.ScrollAnnotations;

namespace SampleQueue
{
    public partial class frmprinter : Form
    {
        Connect kn;
        PrintingItem str;
        public frmprinter()
        {
            InitializeComponent();

            MaximumSize = new System.Drawing.Size(500, 400);
            MinimumSize = new System.Drawing.Size(150, 150);

            kn = new Connect(Temp.ch);
        }

        private void frmprinter_MouseClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void frmprinter_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void frmprinter_MaximumSizeChanged(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmprinter_MinimumSizeChanged(object sender, EventArgs e)
        {
            Rectangle workingArea = Screen.PrimaryScreen.Bounds;
            Location = new Point(workingArea.Right - Size.Width,
                                      workingArea.Bottom - Size.Height);
        }

        private void frmprinter_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Temp.Printers.Count > 0)
            {
                dataGridView1.Rows.Clear();

                foreach (PrintingItem pr in Temp.Printers) dataGridView1.Rows.Add(pr.SampleID, pr.Style, pr.Season, pr.Smptype, pr.Color, pr.Size);

                lbrow.Text = "Rows : " + Temp.Printers.Count;

                Text = "PRINTER " + Temp.Printers.Count;
            }
            else Hide();
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            Temp.Printers.Clear();
            Close();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.Landscape = false;
            printDocument1.PrintController = new StandardPrintController();
            printDocument1.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("4cm x 6cm", 600, 400);

            PrintDialog printdlg = new PrintDialog();

            printdlg.Document = printDocument1;

            if (printdlg.ShowDialog() == DialogResult.OK)
            {
                foreach (var it in Temp.Printers)
                {
                    str = it;
                    printDocument1.Print();
                }

                SaveStatus();
            }
        }
        private void SaveStatus()
        {
            foreach (var str in Temp.Printers) kn.Ghi("update sromstrsampleid set PrintBarcode = 1 where SampleID = '" + str.SampleID + "'");

            Temp.Printers.Clear();
        }
        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (str != null)
            {
                Image img = QRCode(str.SampleID);

                //e.Graphics.DrawImage(img, 50, 10, 100, 100);

                //e.Graphics.DrawString(str.SampleID.Split('-')[1], new Font("Arial", 20.0f, FontStyle.Bold), Brushes.Black, new Point(85, 100));//, new StringFormat() { Alignment = StringAlignment.Center });

                //e.Graphics.DrawString("Style : " + str.Style, new Font("Arial", 8.0f), Brushes.Black, new Point(150, 20));
                //e.Graphics.DrawString("Season : " + str.Season, new Font("Arial", 8.0f), Brushes.Black, new Point(150, 40));
                //e.Graphics.DrawString("NO. : " + str.SampleID.Split('-')[1] + "/" + str.Qty, new Font("Arial", 10.0f), Brushes.Black, new Point(150, 60));
                //e.Graphics.DrawString("Sample Type : " + str.Smptype, new Font("Arial", 8.0f), Brushes.Black, new Point(150, 80));
                //e.Graphics.DrawString("Sample ID : " + str.SampleID, new Font("Arial", 8.0f), Brushes.Black, new Point(60, 130));

                int x = AppConfig.X, y = AppConfig.Y;

                e.Graphics.DrawImage(img, x + 5, y, 100, 100);

                e.Graphics.DrawString(str.SampleID, new Font("Arial", 8.0f, FontStyle.Bold), Brushes.Black, new Point(x, y + 110));//, new StringFormat() { Alignment = StringAlignment.Center });

                e.Graphics.DrawString("Style : " + str.Style, new Font("Arial", 7.0f), Brushes.Black, new Point(x, y + 130));
                e.Graphics.DrawString("Season : " + str.Season + " Size : " + str.Size, new Font("Arial", 7.0f), Brushes.Black, new Point(x, y + 140));
                e.Graphics.DrawString("NO. : " + str.SampleID.Split('-')[1] + "/" + str.Qty + "  Color : " + str.Color, new Font("Arial", 7.0f), Brushes.Black, new Point(x, y + 150));
                e.Graphics.DrawString("Sample Type : " + str.Smptype, new Font("Arial", 7.0f), Brushes.Black, new Point(x, y + 160));
                e.Graphics.DrawLine(new Pen(Brushes.Black), x, y + 180, x + 150, y + 180);
            }
        }
        private Image QRCode(string txt)
        {
            if (txt != "")
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCode = qrGenerator.CreateQrCode(txt, QRCodeGenerator.ECCLevel.Q);
                QRCode code = new QRCode(qrCode);

                var im = code.GetGraphic(5);

                return im;
            }
            else return null;
        }
    }
}
