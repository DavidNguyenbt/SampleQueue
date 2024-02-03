using CSDL;
using QRCoder;
using SampleQueue.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SampleQueue
{
    public partial class frmsqid : Form
    {
        DataTable dt = new DataTable();
        string style = "", ss = "", qty = "", smt = "", color = "", size = "", status = "", printitem = "";
        int col = 0, row = 0;
        List<int> list = new List<int>();
        List<string> recno = new List<string>();
        List<IdItem> id = new List<IdItem>();
        Connect kn;
        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> indexes = new List<int>();
            foreach (DataGridViewRow r1 in dataGridView1.SelectedRows)
            {
                //MessageBox.Show(r1.Cells["PrintBarcode"].Value.ToString());
                if (r1.Cells["PrintBarcode"].Value.ToString() == "False") indexes.Add(r1.Index);
                else if (r1.Cells["LastUpdated"].Value.ToString() == "") indexes.Add(r1.Index);
            }
            
            foreach (int index in indexes)
            {
                if (index >= 0)
                {
                    if (!list.Contains(index))
                    {
                        list.Add(index);

                        DataRow r2 = dt.Rows[index];

                        r2["PrintBarcode"] = 1;
                        dt.AcceptChanges();

                        if (!recno.Contains(r2[0].ToString())) recno.Add(r2[0].ToString());

                        if (!id.Exists(s => s.id.Equals(r2["SampleID"].ToString())))
                            id.Add(new IdItem
                            {
                                id = r2["SampleID"].ToString(),
                                color = r2["Color"].ToString(),
                                size = r2["Size"].ToString()
                            });

                        Counting();
                    }
                }
            }
        }

        private void unselectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> indexes = new List<int>();
            foreach (DataGridViewRow r1 in dataGridView1.SelectedRows)
            {
                if (r1.Cells["PrintBarcode"].Value.ToString() == "True") indexes.Add(r1.Index);
                else if (r1.Cells["LastUpdated"].Value.ToString() == "") indexes.Add(r1.Index);
            }

            foreach (int index in indexes)
            {
                if (index >= 0)
                {
                    if (list.Contains(index))
                    {
                        list.Remove(index);

                        DataRow r = dt.Rows[index];

                        r["PrintBarcode"] = 0;
                        dt.AcceptChanges();

                        recno.Remove(r[0].ToString());

                        id.Remove(id.Where(s => s.id.Equals(r["SampleID"].ToString())).FirstOrDefault());

                        Counting();
                    }
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["PrintBarcode"].Value.ToString() == "False")
                {
                    DataRow r = dt.Rows[row];
                    if (!list.Contains(row))
                    {
                        list.Add(row);

                        r["PrintBarcode"] = 1;
                        dt.AcceptChanges();

                        if (!recno.Contains(r[0].ToString())) recno.Add(r[0].ToString());

                        if (!id.Exists(s => s.id.Equals(r["SampleID"].ToString())))
                            id.Add(new IdItem
                            {
                                id = r["SampleID"].ToString(),
                                color = r["Color"].ToString(),
                                size = r["Size"].ToString()
                            });
                    }
                    else
                    {
                        list.Remove(row);

                        r["PrintBarcode"] = 0;
                        dt.AcceptChanges();

                        recno.Remove(r[0].ToString());

                        id.Remove(id.Where(s => s.id.Equals(r["SampleID"].ToString())).FirstOrDefault());
                    }

                    Counting();
                }
                else if (dataGridView1.Rows[e.RowIndex].Cells["LastUpdated"].Value.ToString() == "")
                {
                    DataRow r = dt.Rows[row];
                    if (!list.Contains(row))
                    {
                        list.Add(row);

                        r["PrintBarcode"] = 1;
                        dt.AcceptChanges();

                        if (!recno.Contains(r[0].ToString())) recno.Add(r[0].ToString());

                        if (!id.Exists(s => s.id.Equals(r["SampleID"].ToString())))
                            id.Add(new IdItem
                            {
                                id = r["SampleID"].ToString(),
                                color = r["Color"].ToString(),
                                size = r["Size"].ToString()
                            });
                    }
                    else
                    {
                        list.Remove(row);

                        r["PrintBarcode"] = 0;
                        dt.AcceptChanges();

                        recno.Remove(r[0].ToString());

                        id.Remove(id.Where(s => s.id.Equals(r["SampleID"].ToString())).FirstOrDefault());
                    }

                    Counting();
                }
            }
        }
        //int d = 0, page = 0;
        private void lbprint_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.Landscape = false;
            printDocument1.PrintController = new StandardPrintController();
            printDocument1.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("4cm x 6cm", 600, 400);

            PrintDialog printdlg = new PrintDialog();

            printdlg.Document = printDocument1;

            if (printdlg.ShowDialog() == DialogResult.OK)
            {
                foreach (IdItem str in id)
                {
                    printitem = str.id;
                    color = str.color;
                    size = str.size;
                    printDocument1.Print();
                }

                SaveStatus();
            }

            //d = 1; page = 0;
        }
        private void SaveStatus()
        {
            foreach (IdItem str in id) kn.Ghi("update sromstrsampleid set PrintBarcode = 1 where SampleID = '" + str.id + "'");

            dt.Rows.Clear();

            dt = kn.Doc("select * from sromstrsampleid where DocNo = '" + id[0].id.Split('-')[0] + "' order by RecNo").Tables[0];

            dataGridView1.DataSource = dt; lbrows.Text = "Rows : " + dataGridView1.RowCount;

            recno.Clear();
            id.Clear();
            list.Clear();
        }
        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (printitem != "")
            {
                Image img = QRCode(printitem);

                int x = AppConfig.X, y = AppConfig.Y;

                //e.Graphics.DrawImage(img, 25, x, 100, 100);

                //e.Graphics.DrawString(printitem, new Font("Arial", 9.0f, FontStyle.Bold), Brushes.Black, new Point(10, x + 110));//, new StringFormat() { Alignment = StringAlignment.Center });

                //e.Graphics.DrawString("Style : " + style, new Font("Arial", 6.0f, FontStyle.Bold), Brushes.Black, new Point(10, x + 130));
                //e.Graphics.DrawString("Season : " + ss, new Font("Arial", 6.0f, FontStyle.Bold), Brushes.Black, new Point(10, x + 140));
                //e.Graphics.DrawString("NO. : " + printitem.Split('-')[1] + "/" + qty, new Font("Arial", 6.0f, FontStyle.Bold), Brushes.Black, new Point(10, x + 150));
                //e.Graphics.DrawString("Sample Type : " + smt, new Font("Arial", 6.0f, FontStyle.Bold), Brushes.Black, new Point(10, x + 160));

                e.Graphics.DrawImage(img, x + 5, y, 100, 100);

                e.Graphics.DrawString(printitem, new Font("Arial", 8.0f, FontStyle.Bold), Brushes.Black, new Point(x, y + 110));//, new StringFormat() { Alignment = StringAlignment.Center });

                e.Graphics.DrawString("Style : " + style, new Font("Arial", 7.0f), Brushes.Black, new Point(x, y + 130));
                e.Graphics.DrawString("Season : " + ss + " Size : " + size, new Font("Arial", 7.0f), Brushes.Black, new Point(x, y + 140));
                e.Graphics.DrawString("NO. : " + printitem.Split('-')[1] + "/" + qty + "  Color : " + color, new Font("Arial", 7.0f), Brushes.Black, new Point(x, y + 150));
                e.Graphics.DrawString("Sample Type : " + smt, new Font("Arial", 7.0f), Brushes.Black, new Point(x, y + 160));
                e.Graphics.DrawLine(new Pen(Brushes.Black), x, y + 180, x + 150, y + 180);
            }
        }

        private void setStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string qry = "", spid = "";
                foreach (DataGridViewRow r1 in dataGridView1.SelectedRows)
                {
                    if (r1.Cells["Location"].Value.ToString() != "") MessageBox.Show("Already have the location !!!");
                    else if (r1.Cells["Status"].Value.ToString() == "Shipped") MessageBox.Show("Already sent to ship !!!");
                    else if (r1.Cells["Status"].Value.ToString() != "") MessageBox.Show("Already set the status !!!");
                    else
                    {
                        spid = r1.Cells["SampleID"].Value.ToString();
                        qry += "update sromstrsampleid set Status = 'Shipped',LastUpdated = getdate() where SampleID = '" + spid + "' \n";
                    }
                }


                kn.Ghi(qry);

                Reload(spid);
            }
        }

        private void setLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow r1 = dataGridView1.SelectedRows[0];
                if (r1.Cells["RFID"].Value.ToString() != "") MessageBox.Show("Already have the location !!!");
                //else if (r1.Cells["Status"].Value.ToString() == "Shipped") MessageBox.Show("Already sent to ship !!!");
                //else if (r1.Cells["Status"].Value.ToString() != "") MessageBox.Show("Already set the status !!!");
                else
                {
                    string spid = r1.Cells["SampleID"].Value.ToString();

                    frmsetlocation frm = new frmsetlocation(spid, this, kn);
                    frm.ShowDialog();
                }
            }
        }
        public void Reload(string spid)
        {
            dt.Rows.Clear();

            dt = kn.Doc("select * from sromstrsampleid where DocNo = '" + spid.Split('-')[0] + "' order by RecNo").Tables[0];

            dataGridView1.DataSource = dt; lbrows.Text = "Rows : " + dataGridView1.RowCount;
        }

        private void lbaddprinter_Click(object sender, EventArgs e)
        {
            foreach (IdItem str in id)
            {
                Temp.Printers.Add(new PrintingItem
                {
                    SampleID = str.id,
                    Style = style,
                    Season = ss,
                    Smptype = smt,
                    Qty = qty,
                    Color = str.color,
                    Size = str.size,
                });
            }

            Close();
        }

        private void menu_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                if (menu.Items.Count > 8) menu.Items.RemoveAt(menu.Items.Count - 1);

                ToolStripMenuItem it = new ToolStripMenuItem();
                it.Text = "Copy " + dataGridView1.Columns[col].HeaderText;
                it.ForeColor = Color.Blue;
                it.Font = new Font("Arial", 9, FontStyle.Bold);
                it.Click += delegate
                {
                    Clipboard.SetText(dataGridView1.Rows[row].Cells[col].Value.ToString());
                };

                menu.Items.Add(it);

                if (Temp.Dept != "MER")
                {
                    removeRFIDToolStripMenuItem.Enabled = false;
                    setLocationToolStripMenuItem.Enabled = false;
                    setLocationIDToolStripMenuItem.Enabled = false;
                    setStatusToolStripMenuItem.Enabled = false;
                }
                else
                {
                    if (dataGridView1.Rows[row].Cells["RFID"].Value.ToString() == "")
                    {
                        removeRFIDToolStripMenuItem.Enabled = false;
                        setLocationIDToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        removeRFIDToolStripMenuItem.Enabled = true;
                        setLocationIDToolStripMenuItem.Enabled = true;
                    }
                }
            }
            catch { }
        }

        private void removeRFIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string spid = dataGridView1.Rows[row].Cells["SampleID"].Value.ToString();

            if (MessageBox.Show("Are you sure to remove the RFID of the Sample ID : " + spid + " ?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string qry = "insert into sromstrsampleremoverfid select *,GETDATE() from sromstrsampleid where SampleID = '" + spid + "' \n" +
                    "update sromstrsampleid set Status = NULL,LastUpdated = NULL,Location = 'Remove RFID on " + DateTime.Now.ToString("yyMMdd HH:mm:ss") + "',RFID = NULL,LocationID = NULL where SampleID = '" + spid + "'";
                kn.Ghi(qry);

                if (kn.ErrorMessage == "") Reload(spid);
                else MessageBox.Show(kn.ErrorMessage);
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            bool r = true;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Status"].ToString() != "")
                {
                    r = false;
                    break;
                }
            }

            if (r)
            {
                kn.Ghi("delete from sromstrsampleid where DocNo = '" + dt.Rows[0]["DocNo"] + "'");

                if (kn.ErrorMessage == "")
                {
                    MessageBox.Show("Reset data succeeded !!!");
                    Close();
                }
                else MessageBox.Show("Reset data failed !!! " + kn.ErrorMessage);
            }
            else MessageBox.Show("You cannot reset data !!!");
        }

        private void setLocationIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow r1 = dataGridView1.SelectedRows[0];
                if (r1.Cells["LocationID"].Value.ToString() != "") MessageBox.Show("Already have the location ID !!!");
                //else if (r1.Cells["Status"].Value.ToString() == "Shipped") MessageBox.Show("Already sent to ship !!!");
                //else if (r1.Cells["Status"].Value.ToString() != "") MessageBox.Show("Already set the status !!!");
                else
                {
                    string spid = r1.Cells["SampleID"].Value.ToString();

                    frmlocation frm = new frmlocation(2, spid, kn, null, this);
                    frm.ShowDialog();
                }
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
        private void Counting()
        {
            if (list.Count > 0)
            {
                lbprint.Visible = true;
                lbselect.Visible = true;
                lbaddprinter.Visible = true;
                lbselect.Text = "Selected : " + list.Count;

                foreach (int i in list) dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
            }
            else
            {
                lbprint.Visible = false;
                lbselect.Visible = false;
                lbaddprinter.Visible = false;
            }
        }
        public frmsqid(DataTable _dt, string _s, string _ss, string _q, string _smt, string _st)
        {
            InitializeComponent();
            this.dt = _dt;
            style = _s; ss = _ss; qty = _q; smt = _smt; status = _st;
        }

        private void frmsqid_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt;

            kn = new Connect(Temp.ch);

            lbrows.Text = "Rows : " + dataGridView1.RowCount;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                col = e.ColumnIndex; row = e.RowIndex;
            }
        }
    }
    class IdItem
    {
        public string id { get; set; }
        public string color { get; set; }
        public string size { get; set; }
    }
}
