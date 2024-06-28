using CSDL;
using ExcelDataReader;
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
    public partial class frmupload : Form
    {
        string path = "";
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        string process = "", id1 = "", id2 = "", ship = "", sewingstart = "", sewingfinish = "", sewer = "", cuttingstart = "", cuttingfinish = "", remark = "", dept;
        Connect kn = new Connect(Temp.ch);
        int oper = 0;
        bool smv = false;
        frmassumesmv frm;
        public frmupload(string p, bool smv = false, string dept = "ADS", frmassumesmv frm = null)
        {
            InitializeComponent();
            path = p;
            this.smv = smv;
            this.dept = dept;
            this.frm = frm;
        }

        private void frmupload_Load(object sender, EventArgs e)
        {
            LoadFile();

            lbprocess2.Visible = false;
            processbar2.Visible = false;
            lbprocess1.Visible = false;
            processbar1.Visible = false;
            lbprocess3.Visible = false;
            processbar3.Visible = false;
            timer1.Stop();
        }

        private void LoadFile()
        {
            try
            {
                if (path != "")
                {
                    using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true
                                }
                            });

                            if (smv)
                            {
                                //SMV
                                dt3 = result.Tables[0];
                                dtgsmv.DataSource = dt3;
                                lbrow3.Text = "Rows : " + dt3.Rows.Count;

                                string[] cl3 = dt3.ColumnNameToArray();

                                cmbstyle.Items.AddRange(cl3);
                                cmbstyle.SelectedIndex = 0;

                                cmbsmv.Items.AddRange(cl3);
                                cmbsmv.SelectedIndex = 1;

                                tabControl1.SelectedIndex = tabControl1.TabCount - 1;
                            }
                            else
                            {
                                //sewing
                                dt1 = result.Tables[0];
                                dtgsewing.DataSource = dt1;
                                lbrow1.Text = "Rows : " + dt1.Rows.Count;

                                string[] cl = dt1.ColumnNameToArray();

                                cmbid.Items.AddRange(cl);
                                cmbid.SelectedIndex = 0;

                                cmbstart.Items.AddRange(cl);
                                cmbstart.SelectedIndex = 1;

                                cmbfinish.Items.AddRange(cl);
                                cmbfinish.SelectedIndex = 2;

                                cmbshiptomer.Items.AddRange(cl);
                                cmbshiptomer.SelectedIndex = 3;

                                cmbsewer.Items.AddRange(cl);
                                cmbsewer.SelectedIndex = 4;

                                //cutting
                                dt2 = result.Tables[1];
                                dtgcutting.DataSource = dt2;
                                lbrow2.Text = "Rows : " + dt2.Rows.Count;

                                string[] cl2 = dt2.ColumnNameToArray();

                                cmbid2.Items.AddRange(cl2);
                                cmbid2.SelectedIndex = 0;

                                cmbcuttingstart.Items.AddRange(cl2);
                                cmbcuttingstart.SelectedIndex = 1;

                                cmbcuttingfinish.Items.AddRange(cl2);
                                cmbcuttingfinish.SelectedIndex = 2;

                                cmbcuttingremark.Items.AddRange(cl2);
                                cmbcuttingremark.SelectedIndex = 3;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void btupload3_Click(object sender, EventArgs e)
        {
            if (dt3.Rows.Count > 0)
            {
                string qry = "";
                foreach (DataRow dr in dt3.Rows) qry += "exec SampleQueueLoading 44, '" + dept + "', '" + dr[cmbstyle.Text] + "', '" + dr[cmbsmv.Text].ToString().Replace(',', '.') + "' \n";

                if (qry != "")
                {
                    kn.Ghi(qry);

                    if (kn.ErrorMessage == "")
                    {
                        MessageBox.Show("Done !!!");
                        frm?.LoadData();
                    }
                    else MessageBox.Show("Failed !!! " + kn.ErrorMessage);
                }
            }
            else MessageBox.Show("No have data !!!");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (oper == 0) lbprocess1.Text = process;
            else lbprocess2.Text = process;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (oper == 0) Sewing();
            else Cutting();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lbprocess1.Visible = false;
            processbar1.Visible = false;
            lbprocess2.Visible = false;
            processbar2.Visible = false;
            timer1.Stop();
        }

        private void btupload_Click(object sender, EventArgs e)
        {
            id1 = cmbid.Text;
            sewingstart = cmbstart.Text;
            sewingfinish = cmbfinish.Text;
            sewer = cmbsewer.Text;
            ship = cmbshiptomer.Text;
            //oper = 0;
            //lbprocess1.Visible = true;
            //processbar1.Visible = true;
            //timer1.Start();
            //worker.RunWorkerAsync();
            Sewing();
        }

        private void Sewing()
        {
            try
            {
                if (dt1.Rows.Count > 0)
                {

                    string update = "";
                    foreach (DataRow r in dt1.Rows)
                    {
                        update += "update sromstr set TechpackPlan = " + (string.IsNullOrEmpty(r[ship].ToString()) ? "NULL," : "'" + DateTime.Parse(r[ship].ToString()).ToString("yyyyMMdd") + "',") +
                            " SewingActualStart = " + (string.IsNullOrEmpty(r[sewingstart].ToString()) ? "NULL," : "'" + DateTime.Parse(r[sewingstart].ToString()).ToString("yyyyMMdd") + "',") +
                            " SewingActualFinish = " + (string.IsNullOrEmpty(r[sewingfinish].ToString()) ? "NULL," : "'" + DateTime.Parse(r[sewingfinish].ToString()).ToString("yyyyMMdd") + "',") +
                            " SewersName = N'" + r[cmbsewer.Text].ToString() + "' where DocNo = '" + r[id1].ToString() + "' \n" +
                            "   update sromstr set Status = (case when TechpackPlan is null then 'P' else (case when RSD < TechpackPlan then 'F2' else 'F1' end) end),SysLMUser = '" + Temp.User + "',SysLMDate = getdate() " +
                            "   where DocNo = '" + r[id1].ToString() + "' \n";

                        process = "Collecting for Sample ID : " + r[id1].ToString();
                    }
                    if (update != "")
                    {
                        process = "Uploaded for all !!!";

                        kn.Ghi(update);

                        if (kn.ErrorMessage == "") MessageBox.Show("Done !!!");
                        else MessageBox.Show(update + " \n " + kn.ErrorMessage);
                    }
                }
            }
            catch { }
        }
        private void Cutting()
        {
            try
            {
                if (dt2.Rows.Count > 0)
                {

                    string update = "";
                    foreach (DataRow r in dt2.Rows)
                    {
                        update += "update sromstr set " +
                            " CuttingStart = " + (string.IsNullOrEmpty(r[cuttingstart].ToString()) ? "NULL," : "'" + DateTime.Parse(r[cuttingstart].ToString()).ToString("yyyyMMdd") + "',") +
                            " CuttingFinish = " + (string.IsNullOrEmpty(r[cuttingfinish].ToString()) ? "NULL," : "'" + DateTime.Parse(r[cuttingfinish].ToString()).ToString("yyyyMMdd") + "',") +
                            " Remark = Remark + N', " + r[remark].ToString() + "' where DocNo = '" + r[id2].ToString() + "' \n" +
                            "   update sromstr set Status = 'P',SysLMUser = '" + Temp.User + "',SysLMDate = getdate() " +
                            "   where DocNo = '" + r[id2].ToString() + "' and Status in ('I','N') \n";

                        process = "Collecting for Sample ID : " + r[id2].ToString();
                    }
                    if (update != "")
                    {
                        process = "Uploaded for all !!!";

                        kn.Ghi(update);

                        if (kn.ErrorMessage == "") MessageBox.Show("Done !!!");
                        else MessageBox.Show(update + " \n " + kn.ErrorMessage);
                    }
                }
            }
            catch { }
        }

        private void btupload2_Click(object sender, EventArgs e)
        {
            cuttingstart = cmbcuttingstart.Text;
            cuttingfinish = cmbcuttingfinish.Text;
            id2 = cmbid2.Text;
            remark = cmbcuttingremark.Text;
            //oper = 1;
            //lbprocess2.Visible = true;
            //processbar2.Visible = true;
            //timer1.Start();
            //worker.RunWorkerAsync();
            Cutting();
        }

        private void frmupload_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void frmupload_DragDrop(object sender, DragEventArgs e)
        {
            path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            LoadFile();
        }
    }
}
