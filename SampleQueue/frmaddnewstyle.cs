using CSDL;
using ExcelDataReader;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleQueue
{
    public partial class frmaddnewstyle : Form
    {
        Connect kn = new Connect(Temp.ch2);
        public frmaddnewstyle()
        {
            InitializeComponent();
        }

        private void frmaddnewstyle_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = kn.Doc("USE [DtradeProduction] exec SampleQueueLoading 2, 'ADS', '', ''");

                cmbsmtype.Items.AddRange(ds.Tables[1].Select().Select(r => r[0].ToString() + " - " + r[1].ToString()).ToArray()); cmbsmtype.SelectedIndex = 0;

                cmbunit.Items.AddRange(ds.Tables[2].Select().Select(r => r[0].ToString() + " - " + r[1].ToString()).ToArray()); cmbunit.SelectedIndex = 0;

                cmbcustomer.Items.AddRange(ds.Tables[4].Select().Select(r => r[0].ToString() + " - " + r[1].ToString()).ToArray()); cmbcustomer.SelectedIndex = 0;

                cmbprogramcode.Items.AddRange(ds.Tables[5].Select().Select(r => r[0].ToString() + " - " + r[1].ToString()).ToArray()); cmbprogramcode.SelectedIndex = 0;

                cmbgmttype.Items.AddRange(ds.Tables[6].Select().Select(r => r[0].ToString() + " - " + r[1].ToString()).ToArray()); cmbgmttype.SelectedIndex = 0;
            }
            catch { }
        }

        private void btnew_Click(object sender, EventArgs e)
        {
            string smorderno = SMOrder();

            txtsmorderno1.Text = smorderno;
            txtsmorderno2.Text = smorderno;
            txtsmorderno3.Text = smorderno;
        }
        private string SMOrder()
        {
            string smorderno = "SM" + DateTime.Now.ToString("yyMM") + "/";
            try
            {
                DataTable dt = new DataTable();

                dt = kn.Doc("select top 100 * from smomstr where smorderno like 'SM" + DateTime.Now.ToString("yyMM") + "%' order by smorderno desc").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string sm = dt.Rows[0][0].ToString();

                    smorderno = GetSMOrder(sm);
                }
                else smorderno += "0001";
            }
            catch { }

            return smorderno;
        }
        private string GetSMOrder(string sm)
        {
            int stt = int.Parse(sm.Split('/')[1]);
            stt++;

            string smorderno = sm.Split('/')[0];

            smorderno += "/" + stt.ToString("0000");

            return smorderno;
        }
        private void btexcel_Click(object sender, EventArgs e)
        {
            try
            {
                string smorderno = SMOrder();
                OpenFileDialog dlg = new OpenFileDialog();

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var stream = File.Open(dlg.FileName, FileMode.Open, FileAccess.Read))
                    {
                        IExcelDataReader reader;

                        reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream);

                        var conf = new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true
                            }
                        };

                        var dataSet = reader.AsDataSet(conf);

                        DataTable dt = dataSet.Tables["Sheet1"];

                        string[] style = dt.Select().Select(s => s["style"].ToString() + "|" + s["season"].ToString() + "|" + s["smptype"].ToString()).Distinct().ToArray();

                        foreach (string styleName in style)
                        {
                            DataRow[] rows = dt.Select("style = '" + styleName.Split('|')[0] + "' and season = '" + styleName.Split('|')[1] + "' and smptype = '" + styleName.Split('|')[2] + "'");

                            DataRow row = rows[0];

                            dtgsmorder.Rows.Add(smorderno, row["season"].ToString(), row["customer"].ToString(), row["mftr"].ToString(), row["style"].ToString(), row["custstyle"].ToString(), row["smptype"].ToString(),
                                                row["programcode"].ToString(), row["unit"].ToString(), row["description"].ToString(), row["gmttype"].ToString());

                            List<string> color = new List<string>(); int seqcolor = 1;
                            List<string> size = new List<string>(); int seqsize = 1;
                            foreach (DataRow r in rows)
                            {
                                if (!color.Contains(r["color"].ToString()))
                                {
                                    color.Add(r["color"].ToString());

                                    dtgcolor.Rows.Add(smorderno, seqcolor.ToString(), (seqcolor * 10).ToString(), r["color"].ToString(), r["color"].ToString());

                                    seqcolor++;
                                }

                                if (!color.Contains(r["sizx"].ToString()))
                                {
                                    color.Add(r["sizx"].ToString());

                                    dtgsize.Rows.Add(smorderno, seqsize.ToString(), (seqsize * 10).ToString(), r["sizx"].ToString());

                                    seqsize++;
                                }
                            }

                            smorderno = GetSMOrder(smorderno);
                        }
                    }
                }
            }
            catch { }
        }

        private void btadd1_Click(object sender, EventArgs e)
        {
            if (txtsmorderno1.Text != "") dtgsmorder.Rows.Add(txtsmorderno1.Text, txtseason.Text, cmbcustomer.Text.Split('-')[0].Trim(), txtmftr.Text, txtstyle.Text, txtcuststyle.Text, cmbsmtype.Text.Split('-')[0].Trim(),
                                                cmbprogramcode.Text.Split('-')[0].Trim(), cmbunit.Text.Split('-')[0].Trim(), txtdescription.Text, cmbgmttype.Text.Split('-')[0].Trim());
        }

        private void btadd2_Click(object sender, EventArgs e)
        {
            if (txtsmorderno2.Text != "") dtgcolor.Rows.Add(txtsmorderno2.Text, txtcolorid.Text, txtseqno1.Text, txtcolor.Text, txtcolor2.Text);
        }

        private void btadd3_Click(object sender, EventArgs e)
        {
            if (txtsmorderno3.Text != "") dtgsize.Rows.Add(txtsmorderno3.Text, txtsizeid.Text, txtseqno2.Text, txtsize.Text);
        }

        private void txtseason_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            txt.Text = txt.Text.ToUpper();
            txt.SelectionStart = txt.TextLength;

            if (txt.Name == "txtcolorid")
            {
                int i = int.Parse(txt.Text == "" ? "0" : txt.Text);
                txtseqno1.Text = (i * 10).ToString();
            }

            if (txt.Name == "txtsizeid")
            {
                int i = int.Parse(txt.Text == "" ? "0" : txt.Text);
                txtseqno2.Text = (i * 10).ToString();
            }
        }

        private void txtcolorid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btupload_Click(object sender, EventArgs e)
        {
            if (dtgsmorder.RowCount == 0) MessageBox.Show("No have data to upload !!!");
            else if (dtgcolor.RowCount == 0) MessageBox.Show("No have data of Color to upload !!!");
            else if (dtgsize.RowCount == 0) MessageBox.Show("No have data of Size to upload !!!");
            else
            {
                string insert = "";
                foreach (DataGridViewRow r in dtgsmorder.Rows)
                {
                    insert += "insert into smomstr (smorderno,orderdate,season,customer,mftr,style,custstyle,smptype,programcode,unit,description,gmttype,sysowner,syscreatedate,syslmuser,syslmdate,versioncount) " +
                        "values ('" + r.Cells[0].Value.ToString() + "', DATEADD(DAY, -3, GETDATE()), '" + r.Cells[1].Value.ToString() + "', '" + r.Cells[2].Value.ToString() + "', '" + r.Cells[3].Value.ToString() + "', " +
                        "'" + r.Cells[4].Value.ToString() + "', '" + r.Cells[5].Value.ToString() + "', '" + r.Cells[6].Value.ToString() + "', '" + r.Cells[7].Value.ToString() + "', '" + r.Cells[8].Value.ToString() + "'," +
                        " '" + r.Cells[9].Value.ToString() + "', '" + r.Cells[10].Value.ToString() + "', 'ADMIN', DATEADD(DAY, -3, GETDATE()),'ADMIN', DATEADD(DAY, -3, GETDATE()),1) \n";
                }

                foreach (DataGridViewRow r in dtgcolor.Rows)
                {
                    insert += "insert into smycolor values ('" + r.Cells[0].Value.ToString() + "'," + r.Cells[1].Value.ToString() + "," + r.Cells[2].Value.ToString() + ",'" + r.Cells[3].Value.ToString() + "','') \n";
                }

                foreach (DataGridViewRow r in dtgsize.Rows)
                {
                    insert += "insert into smysize values ('" + r.Cells[0].Value.ToString() + "'," + r.Cells[1].Value.ToString() + "," + r.Cells[2].Value.ToString() + ",'" + r.Cells[3].Value.ToString() + "') \n";
                }

                if (insert.Length > 0)
                {
                    kn.Ghi(insert);

                    if (kn.ErrorMessage == "")
                    {
                        MessageBox.Show("Done");
                        Close();
                    }
                    else MessageBox.Show(kn.ErrorMessage);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string smorderno = SMOrder();
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Multiselect = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    List<string> styles = new List<string>();
                    foreach (string file in dlg.FileNames)
                    {
                        using (var stream = File.Open(file, FileMode.Open, FileAccess.Read))
                        {
                            IExcelDataReader reader;

                            reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream);

                            var conf = new ExcelDataSetConfiguration
                            {
                                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                                {
                                    UseHeaderRow = true
                                }
                            };

                            var dataSet = reader.AsDataSet(conf);

                            DataTable dt = dataSet.Tables[0];

                            if (dt.Rows.Count > 0)
                            {
                                string[] style = dt.Select().Select(s => s["style"].ToString() + "|" + s["season"].ToString()).Distinct().ToArray();

                                foreach (string styleName in style)
                                {
                                    if(!styles.Contains(styleName))
                                    {
                                        DataRow[] rows = dt.Select("style = '" + styleName.Split('|')[0] + "' and season = '" + styleName.Split('|')[1] + "'");

                                        DataRow row = rows[0];

                                        dtgsmorder.Rows.Add(smorderno, row["season"].ToString(), row["customer"].ToString(), row["mftr"].ToString(), row["style"].ToString(), "", "01",
                                                            "", row["unit"].ToString(), row["description"].ToString(), row["gmttype"].ToString());

                                        List<string> color = new List<string>(); int seqcolor = 1;
                                        List<string> size = new List<string>(); int seqsize = 1;
                                        foreach (DataRow r in rows)
                                        {
                                            if (!color.Contains(r["color"].ToString()))
                                            {
                                                color.Add(r["color"].ToString());

                                                dtgcolor.Rows.Add(smorderno, seqcolor.ToString(), (seqcolor * 10).ToString(), r["color"].ToString(), r["color"].ToString());

                                                seqcolor++;
                                            }

                                            if (!color.Contains(r["sizx"].ToString()))
                                            {
                                                color.Add(r["sizx"].ToString());

                                                dtgsize.Rows.Add(smorderno, seqsize.ToString(), (seqsize * 10).ToString(), r["sizx"].ToString());

                                                seqsize++;
                                            }
                                        }

                                        styles.Add(styleName);

                                        smorderno = GetSMOrder(smorderno);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }
}
