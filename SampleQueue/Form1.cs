using CSDL;
using ExcelDataReader;
using Microsoft.Win32;
using SampleQueue.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Reflection;
using DevExpress.XtraEditors.Filtering.Templates;
using System.Net.Http;
using System.Net;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace SampleQueue
{
    public partial class Form1 : Form
    {
        Connect kn;
        public BackgroundWorker worker;
        List<int> cmt = new List<int>();
        DataSet ds = new DataSet();
        int row = 0, col = 0, repaint = 0, cfttrend = 1, cfttrend2 = 1;//1 daily, 2 monthly, 3 season, 4 style
        string DocNo = "", ads = "";
        bool run = false, date = false;
        DataSet dfds = new DataSet();
        frmprinter prt;
        DataSet dshchart = new DataSet();

        [DllImport("urlmon.dll", CharSet = CharSet.Ansi)]
        private static extern int UrlMkSetSessionOption(int dwOption, string pBuffer, int dwBufferLength, int dwReserved);


        //private ChromiumWebBrowser browser;
        private void UpdateUserAgent()
        {
            // Set the User-Agent of the WebBrowser control to a modern browser's User-Agent string
            const int URLMON_OPTION_USERAGENT = 0x10000001;
            string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36 Edg/90.0.818.66";
            UrlMkSetSessionOption(URLMON_OPTION_USERAGENT, userAgent, userAgent.Length, 0);
        }
        public Form1()
        {
            //SetBrowserFeatureControl();

            InitializeComponent();

            //UpdateUserAgent();

            kn = new Connect(Temp.ch);

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.DoWork += Worker_DoWork;

            //AppConfig.CommentTip = true;
            //AppConfig.Save();
            prt = new frmprinter();

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadData();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            processbar.Visible = false;
            txtloading.Visible = false;
            run = false;

            try
            {
                DocNo = dataGridView1.Rows[0].Cells[0].Value.ToString();
            }
            catch { }

            //if (AppConfig.CapacityTip)
            //{
            //    frmhelp frm = new frmhelp(1);
            //    frm.ShowDialog();
            //}
            //if (AppConfig.UrgentTip)
            //{
            //    frmhelp frm = new frmhelp(2);
            //    frm.ShowDialog();
            //}
            if (AppConfig.CommentTip)
            {
                frmhelp frm = new frmhelp(3);
                frm.ShowDialog();
            }
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text += " - " + Temp.version;

            notifyIcon1.BalloonTipTitle = "Welcome back ! " + Temp.User;
            notifyIcon1.ShowBalloonTip(1000);
            txtname.Text = "Name : " + Temp.User;
            txtdept.Text = "Department : " + Temp.DeptDesc;
            worker.RunWorkerAsync(); run = true;

            datefrom.Hide(); dateto.Hide();

            if (AppConfig.Notification) notification.Text = "Alert Notification : On";
            else notification.Text = "Alert Notification : Off";

            if (!Temp.DeptDesc.Contains("SAMPLE ROOM") && !Temp.DeptDesc.Contains("ADMIN")) sMPCapacityToolStripMenuItem.Enabled = false;

            cmbcft1.SelectedIndex = 0;
            cmbcft2.SelectedIndex = 0;
        }

        private void LoadData()
        {
            try
            {
                if (ds.Tables.Count > 0) ds.Clear();

                if (Temp.DeptDesc.ToUpper().Contains("ADIDAS") || Temp.DeptDesc.ToUpper().Contains("MERA")) ads = "ADS"; else ads = "PUMA";

                ds = kn.Doc("exec SampleQueueLoading 3,'" + Temp.User + "','" + ads + "','' \n" +
                            "exec SampleQueueLoading 20, '" + ads + "', '', ''");

                Invoke((Action)(() =>
                {
                    DataTable dt = kn.Doc("exec SampleQueueLoading 31, '', '', ''").Tables[0];
                    if (dt.Rows.Count > 4)
                    {
                        cmbss1.Items.AddRange(dt.Select().Select(s => s[0].ToString()).ToArray()); cmbss1.SelectedIndex = 3;
                        cmbss2.Items.AddRange(dt.Select().Select(s => s[0].ToString()).ToArray()); cmbss2.SelectedIndex = 2;
                        cmbss3.Items.AddRange(dt.Select().Select(s => s[0].ToString()).ToArray()); cmbss3.SelectedIndex = 1;
                        cmbss4.Items.AddRange(dt.Select().Select(s => s[0].ToString()).ToArray()); cmbss4.SelectedIndex = 0;
                    }

                    ReLoad();

                    cmbcolumn.Items.AddRange(ds.Tables[5].Columns.OfType<DataColumn>().Select(c => c.ColumnName).ToArray());

                    if (cmbcolumn.Items.Contains(AppConfig.FilterColumn1)) cmbcolumn.Text = AppConfig.FilterColumn1;
                }));
            }
            catch { }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowRowsCount();

            ChangeFilterValue();

            repaint = 0;
        }
        private void ShowRowsCount()
        {
            try
            {
                foreach (ToolStripItem it in contextMenuStrip1.Items) it.Enabled = false;

                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        txtrows.Text = "Rows : " + dataGridView1.RowCount;

                        btnew.Enabled = true; btdelete.Enabled = true; btcopytonew.Enabled = true; changeowner.Enabled = true;
                        foreach (ToolStripItem it in contextMenuStrip1.Items) it.Enabled = true; showDefectImageToolStripMenuItem.Enabled = false;
                        break;
                    case 1:
                        txtrows.Text = "Rows : " + dataGridView2.RowCount;

                        btnew.Enabled = false; btdelete.Enabled = false; btcopytonew.Enabled = false; changeowner.Enabled = false;
                        exportToExcelToolStripMenuItem.Enabled = true; openToolStripMenuItem.Enabled = true; copyDocNoSampleIDToolStripMenuItem.Enabled = true;
                        break;
                    case 2:
                        txtrows.Text = "Rows : " + dataGridView3.RowCount;

                        btnew.Enabled = false; btdelete.Enabled = false; btcopytonew.Enabled = false; changeowner.Enabled = false;
                        exportToExcelToolStripMenuItem.Enabled = true; openToolStripMenuItem.Enabled = true; copyDocNoSampleIDToolStripMenuItem.Enabled = true;
                        break;
                    case 3:
                        txtrows.Text = "Rows : " + dataGridView4.RowCount;

                        btnew.Enabled = false; btdelete.Enabled = false; btcopytonew.Enabled = false; changeowner.Enabled = false;
                        exportToExcelToolStripMenuItem.Enabled = true; openToolStripMenuItem.Enabled = true; copyDocNoSampleIDToolStripMenuItem.Enabled = true;
                        break;
                    case 4:
                        txtrows.Text = "Rows : " + dataGridView5.RowCount;

                        btnew.Enabled = false; btdelete.Enabled = false; btcopytonew.Enabled = false; changeowner.Enabled = false;
                        exportToExcelToolStripMenuItem.Enabled = true; openToolStripMenuItem.Enabled = true; copyDocNoSampleIDToolStripMenuItem.Enabled = true;
                        break;
                    case 5:
                        txtrows.Text = "Rows : " + dataGridView6.RowCount;

                        btnew.Enabled = false; btdelete.Enabled = false; btcopytonew.Enabled = false; changeowner.Enabled = false;
                        exportToExcelToolStripMenuItem.Enabled = true; openToolStripMenuItem.Enabled = true; copyDocNoSampleIDToolStripMenuItem.Enabled = true;
                        break;
                    case 7:
                        txtrows.Text = "Rows : " + dataGridView7.RowCount;

                        btnew.Enabled = false; btdelete.Enabled = false; btcopytonew.Enabled = false; changeowner.Enabled = false;
                        exportToExcelToolStripMenuItem.Enabled = true; openToolStripMenuItem.Enabled = true; copyDocNoSampleIDToolStripMenuItem.Enabled = true;
                        break;
                    case 6:
                        txtrows.Text = "Rows : " + dtgdetail.RowCount;

                        btnew.Enabled = false; btdelete.Enabled = false; btcopytonew.Enabled = false; changeowner.Enabled = false;
                        exportToExcelToolStripMenuItem.Enabled = true; openToolStripMenuItem.Enabled = false; copyDocNoSampleIDToolStripMenuItem.Enabled = true; showDefectImageToolStripMenuItem.Enabled = true;
                        break;
                    case 8:
                        txtrows.Text = "Rows : " + dtgjson.RowCount;

                        btnew.Enabled = false; btdelete.Enabled = false; btcopytonew.Enabled = false; changeowner.Enabled = false; btopen.Enabled = false;
                        exportToExcelToolStripMenuItem.Enabled = true; showDefectImageToolStripMenuItem.Enabled = true; copyDocNoSampleIDToolStripMenuItem.Enabled = true;
                        break;
                    case 9:
                        txtrows.Text = "Rows : " + dtgurgent.RowCount;

                        btnew.Enabled = false; btdelete.Enabled = false; btcopytonew.Enabled = false; changeowner.Enabled = false; btopen.Enabled = true;
                        exportToExcelToolStripMenuItem.Enabled = true; showDefectImageToolStripMenuItem.Enabled = false; copyDocNoSampleIDToolStripMenuItem.Enabled = true;
                        break;
                    case 11:
                        txtrows.Text = "Rows : " + dtgsampleidmaster.RowCount;

                        btnew.Enabled = false; btdelete.Enabled = false; btcopytonew.Enabled = false; changeowner.Enabled = false; btopen.Enabled = true;
                        exportToExcelToolStripMenuItem.Enabled = true; showDefectImageToolStripMenuItem.Enabled = false; copyDocNoSampleIDToolStripMenuItem.Enabled = true;
                        break;
                    case 12:
                        txtrows.Text = "Rows : 0";

                        btnew.Enabled = false; btdelete.Enabled = false; btcopytonew.Enabled = false; changeowner.Enabled = false; btopen.Enabled = true;
                        exportToExcelToolStripMenuItem.Enabled = true; showDefectImageToolStripMenuItem.Enabled = false; copyDocNoSampleIDToolStripMenuItem.Enabled = false;
                        break;
                    case 13:
                        txtrows.Text = "Rows : 0";

                        btnew.Enabled = false; btdelete.Enabled = false; btcopytonew.Enabled = false; changeowner.Enabled = false; btopen.Enabled = true;
                        exportToExcelToolStripMenuItem.Enabled = false; showDefectImageToolStripMenuItem.Enabled = false; copyDocNoSampleIDToolStripMenuItem.Enabled = false;
                        break;
                }

                if (tabControl1.SelectedIndex == 6)
                {
                    cmbcolumn.Enabled = false;
                    cmbvalue.Enabled = false;
                }
                else
                {
                    cmbcolumn.Enabled = true;
                    cmbvalue.Enabled = true;
                }
            }
            catch { }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btnew_Click(object sender, EventArgs e)
        {
            cmt.Clear();
            frmitem frm = new frmitem(false, false, cmt, this);
            frm.DataRow = ds.Tables[6].Rows[0];

            frm.ShowDialog();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == tabControl1.SelectedIndex)
            {
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                    new Font(tabControl1.Font, FontStyle.Bold),
                    Brushes.Blue,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
            else
            {
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                    tabControl1.Font,
                    Brushes.Black,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
        }

        private void btopen_Click(object sender, EventArgs e)
        {
            OpenRow(DocNo);
        }
        public void OpenRow(string docno)
        {
            try
            {
                DataRow[] r = ds.Tables[6].Select("DocNo = '" + docno + "'");

                if (r.Length > 0)
                {
                    frmitem frm = new frmitem(true, false, cmt, this);
                    frm.DataRow = r[0];

                    frm.ShowDialog();
                }
                else
                {
                    string qy = "exec SampleQueueLoading 19, '" + ads + "', 'a.DocNo', '" + docno + "'";

                    DataTable d = kn.Doc(qy).Tables[0];

                    if (d.Rows.Count > 0)
                    {
                        frmitem frm = new frmitem(true, false, cmt, this);
                        frm.DataRow = d.Rows[0];

                        frm.ShowDialog();
                    }
                }
            }
            catch { }
        }
        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView dtg = new DataGridView();
                DataTable dt = new DataTable();
                ExportToExcel.ToExcel excel = new ExportToExcel.ToExcel();
                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        dataGridView1.Columns.Remove(dataGridView1.Columns["Remark"]);
                        excel.ExportToExcel(dataGridView1);
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = ds.Tables[0];
                        break;
                    case 1:
                        dataGridView2.Columns.Remove(dataGridView2.Columns["Remark"]);
                        excel.ExportToExcel(dataGridView2);
                        dataGridView2.DataSource = null;
                        dataGridView2.DataSource = ds.Tables[1];
                        break;
                    case 2:
                        //dataGridView3.Columns.Remove(dataGridView3.Columns["Remark"]);
                        excel.ExportToExcel(dataGridView3);
                        //dataGridView3.DataSource = null;
                        //dataGridView3.DataSource = ds.Tables[0];
                        break;
                    case 3:
                        dataGridView4.Columns.Remove(dataGridView4.Columns["Remark"]);
                        excel.ExportToExcel(dataGridView4);
                        dataGridView4.DataSource = null;
                        dataGridView4.DataSource = ds.Tables[2];
                        break;
                    case 4:
                        dataGridView5.Columns.Remove(dataGridView5.Columns["Remark"]);
                        excel.ExportToExcel(dataGridView5);
                        dataGridView5.DataSource = null;
                        dataGridView5.DataSource = ds.Tables[3];
                        break;
                    case 5:
                        //dataGridView6.Columns.Remove(dataGridView6.Columns["Remark"]);
                        excel.ExportToExcel(dataGridView6);
                        //dataGridView6.DataSource = null;
                        //dataGridView6.DataSource = ds.Tables[4];
                        break;
                    case 6:
                        //dataGridView6.Columns.Remove(dataGridView6.Columns["Remark"]);
                        if (dtgdetail.Rows.Count > 0)
                        {
                            int d = dtgdetail.Rows.Count;
                            string path = "";
                            FolderBrowserDialog dir = new FolderBrowserDialog();

                            if (dir.ShowDialog() == DialogResult.OK) path = dir.SelectedPath + @"\" + cmbcft1.Text + " RFT Report.xlsx";
                            else path = @"D:\" + cmbcft1.Text + " RFT Report.xlsx";

                            if (File.Exists(path)) File.Delete(path);

                            ExportToExcel.ToExcel toExcel = new ExportToExcel.ToExcel();
                            toExcel.ExportToExcel(dtgdetail, true, path);


                            ExcelPackage package1 = new ExcelPackage(path);

                            ExcelWorksheet ws = package1.Workbook.Worksheets["Sheet1"];
                            ws.Name = "RFT REPORT";

                            ExcelRange range1 = ws.Cells["A1:I1"];
                            range1.Style.Font.Bold = true;
                            range1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            range1.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range1.Style.Fill.BackgroundColor.SetColor(Color.Bisque);

                            ExcelRange range2 = ws.Cells["A1:I" + (dtgdetail.RowCount + 1)];
                            range2.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            range2.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            range2.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            range2.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                            ExcelRange range3 = ws.Cells["A" + (d + 1) + ":I" + (d + 1)];
                            range3.Style.Font.Bold = true;
                            range3.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            range3.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range3.Style.Fill.BackgroundColor.SetColor(Color.Bisque);

                            ExcelRange range4 = ws.Cells["A" + (d + 1) + ":E" + (d + 1)];
                            range4.Merge = true;
                            range4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            range4.Value = "Total : ";

                            ExcelRange range5 = ws.Cells["L1:M6"];
                            range5.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            range5.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            range5.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            range5.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                            ExcelRange range6 = ws.Cells["L1:M1"];
                            range6.Style.Font.Bold = true;
                            range6.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            range6.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range6.Style.Fill.BackgroundColor.SetColor(Color.Bisque);

                            ws.Cells["L1"].Value = "Defect Name";
                            ws.Cells["M1"].Value = "Defect Qty";

                            int dd = 2;
                            foreach (DataRow r in dfds.Tables[1].Rows)
                            {
                                ws.Cells["L" + dd].Value = r[0].ToString();
                                ws.Cells["M" + dd].Value = r[1].ToString();

                                dd++;
                            }

                            ws.Columns.AutoFit();

                            package1.Save();

                            Process.Start(path);
                        }

                        if (dtginfor.RowCount > 0) excel.ExportToExcel(dtginfor);
                        //dataGridView6.DataSource = null;
                        //dataGridView6.DataSource = ds.Tables[4];
                        break;
                    case 8:
                        //dataGridView6.Columns.Remove(dataGridView6.Columns["Remark"]);
                        excel.ExportToExcel(dtgjson);
                        //dataGridView6.DataSource = null;
                        //dataGridView6.DataSource = ds.Tables[4];
                        break;
                    case 9:
                        //dataGridView6.Columns.Remove(dataGridView6.Columns["Remark"]);
                        excel.ExportToExcel(dtgurgent);
                        //dataGridView6.DataSource = null;
                        //dataGridView6.DataSource = ds.Tables[4];
                        break;
                    case 11:
                        //dataGridView6.Columns.Remove(dataGridView6.Columns["Remark"]);
                        excel.ExportToExcel(dtgsampleidmaster);
                        //dataGridView6.DataSource = null;
                        //dataGridView6.DataSource = ds.Tables[4];
                        break;
                }
            }
            catch { }
        }

        private void cmbcolumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFilterValue();

            AppConfig.FilterColumn1 = cmbcolumn.Text;
            Temp.SaveConfig();
        }

        int len = 0;
        private void Filter(bool ori = false)
        {
            bool f = true;

            if (cmbcolumn.Text == "") f = false;
            else
            {
                if (!date)
                {
                    if (cmbvalue.Text == "") f = false;
                }
            }

            if (f)
            {
                try
                {
                    DataColumn cl = ds.Tables[6].Columns[cmbcolumn.Text];
                    //MessageBox.Show(cl.DataType.ToString());
                    switch (tabControl1.SelectedIndex)
                    {
                        case 0:
                            DataTable dt1 = ori ? ds.Tables[0] : (dataGridView1.DataSource as DataTable);

                            if (!date) dataGridView1.DataSource = dt1.Select(cmbcolumn.Text + " like '%" + cmbvalue.Text + "%'").CopyToDataTable();
                            else dataGridView1.DataSource = dt1.Select(cmbcolumn.Text + " >= '" + datefrom.Value.ToString("dd-MM-yyyy") + "' AND " + cmbcolumn.Text + " <= '" + dateto.Value.ToString("dd-MM-yyyy") + "'").CopyToDataTable();

                            dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[cmbcolumn.Text];
                            txtrows.Text = "Rows : " + dataGridView1.RowCount;
                            break;
                        case 1:
                            DataTable dt2 = ori ? ds.Tables[1] : (dataGridView2.DataSource as DataTable);

                            if (!date) dataGridView2.DataSource = dt2.Select(cmbcolumn.Text + " like '%" + cmbvalue.Text + "%'").CopyToDataTable();
                            else dataGridView2.DataSource = dt2.Select(cmbcolumn.Text + " >= '" + datefrom.Value.ToString("dd-MM-yyyy") + "' AND " + cmbcolumn.Text + " <= '" + dateto.Value.ToString("dd-MM-yyyy") + "'").CopyToDataTable();

                            dataGridView2.CurrentCell = dataGridView2.Rows[0].Cells[cmbcolumn.Text];
                            txtrows.Text = "Rows : " + dataGridView2.RowCount;
                            break;
                        case 2:
                            DataTable dt3 = ori ? ds.Tables[2] : (dataGridView3.DataSource as DataTable);

                            if (!date) dataGridView3.DataSource = dt3.Select(cmbcolumn.Text + " like '%" + cmbvalue.Text + "%'").CopyToDataTable();
                            else dataGridView3.DataSource = dt3.Select(cmbcolumn.Text + " >= '" + datefrom.Value.ToString("dd-MM-yyyy") + "' AND " + cmbcolumn.Text + " <= '" + dateto.Value.ToString("dd-MM-yyyy") + "'").CopyToDataTable();

                            dataGridView3.CurrentCell = dataGridView3.Rows[0].Cells[cmbcolumn.Text];
                            txtrows.Text = "Rows : " + dataGridView3.RowCount;
                            break;
                        case 3:
                            DataTable dt4 = ori ? ds.Tables[3] : (dataGridView4.DataSource as DataTable);

                            if (!date) dataGridView4.DataSource = dt4.Select(cmbcolumn.Text + " like '%" + cmbvalue.Text + "%'").CopyToDataTable();
                            else dataGridView4.DataSource = dt4.Select(cmbcolumn.Text + " >= '" + datefrom.Value.ToString("dd-MM-yyyy") + "' AND " + cmbcolumn.Text + " <= '" + dateto.Value.ToString("dd-MM-yyyy") + "'").CopyToDataTable();

                            dataGridView4.CurrentCell = dataGridView4.Rows[0].Cells[cmbcolumn.Text];
                            txtrows.Text = "Rows : " + dataGridView4.RowCount;
                            break;
                        case 4:
                            DataTable dt5 = ori ? ds.Tables[4] : (dataGridView5.DataSource as DataTable);

                            if (!date) dataGridView5.DataSource = dt5.Select(cmbcolumn.Text + " like '%" + cmbvalue.Text + "%'").CopyToDataTable();
                            else dataGridView5.DataSource = dt5.Select(cmbcolumn.Text + " >= '" + datefrom.Value.ToString("dd-MM-yyyy") + "' AND " + cmbcolumn.Text + " <= '" + dateto.Value.ToString("dd-MM-yyyy") + "'").CopyToDataTable();

                            dataGridView5.CurrentCell = dataGridView5.Rows[0].Cells[cmbcolumn.Text];
                            txtrows.Text = "Rows : " + dataGridView5.RowCount;
                            break;
                        case 5:
                            DataTable dt6 = ori ? ds.Tables[5] : (dataGridView6.DataSource as DataTable);

                            if (!date) dataGridView6.DataSource = dt6.Select(cmbcolumn.Text + " like '%" + cmbvalue.Text + "%'").CopyToDataTable();
                            else dataGridView6.DataSource = dt6.Select(cmbcolumn.Text + " >= '" + datefrom.Value.ToString("dd-MM-yyyy") + "' AND " + cmbcolumn.Text + " <= '" + dateto.Value.ToString("dd-MM-yyyy") + "'").CopyToDataTable();

                            dataGridView6.CurrentCell = dataGridView6.Rows[0].Cells[cmbcolumn.Text];
                            txtrows.Text = "Rows : " + dataGridView6.RowCount;
                            break;
                        case 7:
                            DataTable dt7 = ori ? ds.Tables[7] : (dataGridView7.DataSource as DataTable);

                            if (!date) dataGridView7.DataSource = dt7.Select(cmbcolumn.Text + " like '%" + cmbvalue.Text + "%'").CopyToDataTable();
                            else dataGridView7.DataSource = dt7.Select(cmbcolumn.Text + " >= '" + datefrom.Value.ToString("dd-MM-yyyy") + "' AND " + cmbcolumn.Text + " <= '" + dateto.Value.ToString("dd-MM-yyyy") + "'").CopyToDataTable();

                            dataGridView7.CurrentCell = dataGridView7.Rows[0].Cells[cmbcolumn.Text];
                            txtrows.Text = "Rows : " + dataGridView7.RowCount;
                            break;
                        case 8:

                            txtrows.Text = "Rows : " + dtgjson.RowCount;
                            break;
                        case 9:
                            DataTable dt9 = ori ? ds.Tables[9] : (dtgurgent.DataSource as DataTable);

                            if (!date) dtgurgent.DataSource = dt9.Select(cmbcolumn.Text + " like '%" + cmbvalue.Text + "%'").CopyToDataTable();
                            else dtgurgent.DataSource = dt9.Select(cmbcolumn.Text + " >= '" + datefrom.Value.ToString("dd-MM-yyyy") + "' AND " + cmbcolumn.Text + " <= '" + dateto.Value.ToString("dd-MM-yyyy") + "'").CopyToDataTable();

                            dtgurgent.CurrentCell = dtgurgent.Rows[0].Cells[cmbcolumn.Text];
                            txtrows.Text = "Rows : " + dtgurgent.RowCount;
                            break;
                        case 11:
                            DataTable dt11 = ori ? ds.Tables[8] : (dtgsampleidmaster.DataSource as DataTable);

                            if (!date) dtgsampleidmaster.DataSource = dt11.Select(cmbcolumn.Text + " like '%" + cmbvalue.Text + "%'").CopyToDataTable();
                            else dtgsampleidmaster.DataSource = dt11.Select(cmbcolumn.Text + " >= '" + datefrom.Value.ToString("dd-MM-yyyy") + "' AND " + cmbcolumn.Text + " <= '" + dateto.Value.ToString("dd-MM-yyyy") + "'").CopyToDataTable();

                            dtgsampleidmaster.CurrentCell = dtgsampleidmaster.Rows[0].Cells[cmbcolumn.Text];
                            txtrows.Text = "Rows : " + dtgsampleidmaster.RowCount;
                            break;
                    }

                    repaint = 0;
                }
                catch
                {
                    MessageBox.Show("Your filter was not found, please double check again !!!");
                    ReLoad(false);
                }
            }
        }
        private void ChangeFilterValue()
        {
            try
            {
                DataGridView dtg = new DataGridView();
                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        dtg = dataGridView1;
                        break;
                    case 1:
                        dtg = dataGridView2;
                        break;
                    case 2:
                        dtg = dataGridView3;
                        break;
                    case 3:
                        dtg = dataGridView4;
                        break;
                    case 4:
                        dtg = dataGridView5;
                        break;
                    case 5:
                        dtg = dataGridView6;
                        break;
                    case 7:
                        dtg = dataGridView7;
                        break;
                    case 8:
                        //dtg = dtgrftreport;
                        break;
                    case 9:
                        dtg = dtgurgent;
                        break;
                    case 11:
                        dtg = dtgsampleidmaster;
                        break;
                }
                if (dtg.RowCount > 0)
                {
                    DocNo = dtg.Rows[0].Cells[0].Value.ToString();

                    DataTable d = dtg.DataSource as DataTable;


                    //cmbcolumn.Items.Clear();
                    //cmbcolumn.Items.AddRange(d.Columns.OfType<DataColumn>().Select(c => c.ColumnName).ToArray());
                    //if (cmbcolumn.Items.Contains(AppConfig.FilterColumn1)) cmbcolumn.Text = AppConfig.FilterColumn1;

                    //MessageBox.Show(d.Columns[cmbcolumn.Text].DataType.ToString());

                    if (d.Columns.Contains(cmbcolumn.Text))
                    {
                        if (d.Columns[cmbcolumn.Text].DataType == typeof(DateTime))
                        {
                            date = true;

                            cmbvalue.Hide();

                            datefrom.Show(); dateto.Show();
                        }
                        else
                        {
                            date = false;

                            cmbvalue.Show();

                            datefrom.Hide(); dateto.Hide();

                            cmbvalue.Items.Clear();
                            cmbvalue.Text = "";
                            DataView dv = d.DefaultView;
                            dv.Sort = cmbcolumn.Text;
                            cmbvalue.Items.AddRange(dv.ToTable().Select().Select(r => r[cmbcolumn.Text].ToString()).Distinct().ToArray());
                        }
                    }
                }
            }
            catch { }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReLoad(false);
        }
        public void ReLoad(bool filter = true)
        {
            //MessageBox.Show(filter.ToString());
            //foreach (DataTable dt in ds.Tables)
            //{
            //    try
            //    {
            //        dt.Columns["TechpackPlan"].ColumnName = "ShipToMer";
            //    }
            //    catch { continue; }
            //}

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ds.Tables[0]; dataGridView1.Columns["Style"].Frozen = true;
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = ds.Tables[1]; dataGridView2.Columns["Style"].Frozen = true;
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = ds.Tables[2]; dataGridView3.Columns["Style"].Frozen = true;
            dataGridView4.DataSource = null;
            dataGridView4.DataSource = ds.Tables[3]; dataGridView4.Columns["Style"].Frozen = true;
            dataGridView5.DataSource = null;
            dataGridView5.DataSource = ds.Tables[4]; dataGridView5.Columns["Style"].Frozen = true;
            dataGridView6.DataSource = null;
            dataGridView6.DataSource = ds.Tables[5]; dataGridView6.Columns["Style"].Frozen = true;
            dtgurgent.DataSource = null;
            dtgurgent.DataSource = ds.Tables[7]; dtgurgent.Columns["Style"].Frozen = true;
            dataGridView7.DataSource = null;
            dataGridView7.DataSource = ds.Tables[9];
            dtgsampleidmaster.DataSource = null;
            dtgsampleidmaster.DataSource = ds.Tables[8];

            if (filter) Filter();

            LoadCFTTrend(DateTime.Now, DateTime.Now);

            //if (cmbss1.Text != "" && cmbss2.Text != "" && cmbss3.Text != "" && cmbss4.Text != "") LoadPredictive();

            cmbstyle.Items.AddRange(ds.Tables[6].Select().Select(r => r["Style"].ToString()).Distinct().ToArray());
            cmbseason.Items.AddRange(ds.Tables[6].Select().Select(r => r["Season"].ToString()).Distinct().ToArray());
            cmbstage.Items.AddRange(ds.Tables[6].Select().Select(r => r["SmpType"].ToString()).Distinct().ToArray());

            cmbstyle2.Items.AddRange(ds.Tables[6].Select().Select(r => r["Style"].ToString()).Distinct().ToArray());
            cmbseason2.Items.AddRange(ds.Tables[6].Select().Select(r => r["Season"].ToString()).Distinct().ToArray());
            cmbstage2.Items.AddRange(ds.Tables[6].Select().Select(r => r["SmpType"].ToString()).Distinct().ToArray());

            ShowRowsCount();

            repaint = 0;

            if (dataGridView1.RowCount > 0) ShowDelay();

            DataTable bu = kn.Doc("select distinct ProgramCode,ProgramName from saprgrm where ExtTerm1 = '" + ads + "' order by ProgramCode").Tables[0];
            string[] it = bu.Select().Select(s => s[0] + "-" + s[1]).ToArray();
            cmbbu.Items.AddRange(it);
            cmbbu2.Items.AddRange(it);
        }

        private void ShowDelay()
        {
            try
            {
                int delaytime = int.Parse(kn.Doc("select * from [00SampleType] where SampleType = 'DelayTime'").Tables[0].Rows[0][1].ToString());
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DateTime trim = DateTime.Parse(r["TrimcardPlan"].ToString());
                    DateTime fab = DateTime.Parse(r["FabricPlan"].ToString());
                    DateTime acc = DateTime.Parse(r["DecorationPlan"].ToString());

                    if (!r["Status"].ToString().Contains("Finish"))
                    {
                        if (r["TrimcardActual"].ToString() == "")
                        {
                            TimeSpan tp = trim - DateTime.Now;

                            if (tp.TotalDays < delaytime)
                                CallShowDelay(r["DocNo"].ToString(), "Trimcard near or late with the planning date !!!", "Trimcard Delay");
                        }

                        if (r["FabricActual"].ToString() == "")
                        {
                            TimeSpan tp = fab - DateTime.Now;

                            if (tp.TotalDays < delaytime)
                                CallShowDelay(r["DocNo"].ToString(), "Fabric near or late with the planning date !!!", "Fabric Delay");
                        }

                        if (r["DecorationActual"].ToString() == "")
                        {
                            TimeSpan tp = acc - DateTime.Now;

                            if (tp.TotalDays < delaytime)
                                CallShowDelay(r["DocNo"].ToString(), "Accessory near or late with the planning date !!!", "Accessory Delay");
                        }
                    }

                    //MessageBox.Show("a " + r["DecorationPlan"].ToString());
                }
            }
            catch { }
        }
        private void CallShowDelay(string docno, string ms, string content)
        {
            frmalert frm = new frmalert(this, docno);
            frm.ShowAlert(ms, content);
        }
        /// <summary>
        /// Daily report
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        private void LoadCFTTrend(DateTime dt1, DateTime dt2)
        {
            dfds.Tables.Clear();
            charttop5.Series["Series1"].Points.Clear();
            dfds = kn.Doc("exec SampleQueueLoading 11, '" + ads + "-" + cmbcft1.Text + "', '" + dt1.ToString("yyyyMMdd") + "', '" + dt2.ToString("yyyyMMdd") + "'");

            foreach (DataRow r in dfds.Tables[1].Rows) charttop5.Series["Series1"].Points.AddXY(r[0].ToString(), float.Parse(r[1].ToString()));

            dtgtopdefect.DataSource = dfds.Tables[2];

            dtgdetail.DataSource = dfds.Tables[0];
            txtrows.Text = "Rows : " + dtgdetail.RowCount;
            //detail.Hide();
        }
        /// <summary>
        /// Monthly Report
        /// </summary>
        /// <param name="mon"></param>
        private void LoadCFTTrend(int mon)
        {
            dfds.Tables.Clear();
            charttop5.Series["Series1"].Points.Clear();
            dfds = kn.Doc("exec SampleQueueLoading 12, '" + ads + "-" + cmbcft1.Text + "', " + mon + ", ''");

            foreach (DataRow r in dfds.Tables[1].Rows) charttop5.Series["Series1"].Points.AddXY(r[0].ToString(), float.Parse(r[1].ToString()));

            dtgtopdefect.DataSource = dfds.Tables[2];

            dtgdetail.DataSource = dfds.Tables[0];
            txtrows.Text = "Rows : " + dtgdetail.RowCount;
            //detail.Show();
        }
        /// <summary>
        /// Season summary
        /// </summary>
        /// <param name="season"></param>
        private void LoadCFTTrend(string season = "", string stage = "")
        {
            dfds.Tables.Clear();
            charttop5.Series["Series1"].Points.Clear();
            string query = "exec SampleQueueLoading 13, '" + ads + "-" + cmbcft1.Text + "', '" + season + "', '" + stage + "'";
            dfds = kn.Doc(query);


            foreach (DataRow r in dfds.Tables[1].Rows) charttop5.Series["Series1"].Points.AddXY(r[0].ToString(), float.Parse(r[1].ToString()));

            dtgtopdefect.DataSource = dfds.Tables[2];

            dtgdetail.DataSource = dfds.Tables[0];
            txtrows.Text = "Rows : " + dtgdetail.RowCount;
            //detail.Show();
        }
        /// <summary>
        /// Style summary
        /// </summary>
        private void LoadCFTTrend()
        {
            dfds.Tables.Clear();
            charttop5.Series["Series1"].Points.Clear();
            dfds = kn.Doc("exec SampleQueueLoading 14, '" + ads + "-" + cmbcft1.Text + "', '" + cmbstyle.Text + "', ''");


            foreach (DataRow r in dfds.Tables[1].Rows) charttop5.Series["Series1"].Points.AddXY(r[0].ToString(), float.Parse(r[1].ToString()));

            dtgtopdefect.DataSource = dfds.Tables[2];

            dtgdetail.DataSource = dfds.Tables[0];
            txtrows.Text = "Rows : " + dtgdetail.RowCount;
            ///detail.Show();
        }
        private void LoadPredictive()
        {
            DataSet predictive = kn.Doc("exec SampleQueueLoading 32, '" + cmbss1.Text + "', '" + ads + "', ''\n " +
                                        "exec SampleQueueLoading 32, '" + cmbss2.Text + "', '" + ads + "', ''\n " +
                                        "exec SampleQueueLoading 32, '" + cmbss3.Text + "', '" + ads + "', ''\n " +
                                        "exec SampleQueueLoading 32, '" + cmbss4.Text + "', '" + ads + "', ''");

            chrtseason1.DataSource = predictive.Tables[0];
            chrtseason1.Series[0].XValueMember = "Defect";
            chrtseason1.Series[0].YValueMembers = "Per";
            chrtseason1.DataBind();

            chrtseason2.DataSource = predictive.Tables[1];
            chrtseason2.Series[0].XValueMember = "Defect";
            chrtseason2.Series[0].YValueMembers = "Per";
            chrtseason2.DataBind();

            chrtseason3.DataSource = predictive.Tables[2];
            chrtseason3.Series[0].XValueMember = "Defect";
            chrtseason3.Series[0].YValueMembers = "Per";
            chrtseason3.DataBind();

            chrtseason4.DataSource = predictive.Tables[3];
            chrtseason4.Series[0].XValueMember = "Defect";
            chrtseason4.Series[0].YValueMembers = "Per";
            chrtseason4.DataBind();

            DataTable dt1 = predictive.Tables[0];
            dt1.Merge(predictive.Tables[1]);
            dt1.Merge(predictive.Tables[2]);
            dt1.Merge(predictive.Tables[3]);

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Defect", typeof(string));
            dt2.Columns.Add("Per", typeof(float));

            List<string> ls = new List<string>();
            ls = dt1.Select().Select(s => s[0].ToString()).Distinct().ToList();

            foreach (string s in ls)
            {
                float p1 = 0, p2 = 0, p3 = 0, p4 = 0;

                if (predictive.Tables[0].Select("Defect = '" + s + "'").Count() > 0) p1 = float.Parse(predictive.Tables[0].Select("Defect = '" + s + "'")[0][2].ToString());
                if (predictive.Tables[1].Select("Defect = '" + s + "'").Count() > 0) p2 = float.Parse(predictive.Tables[1].Select("Defect = '" + s + "'")[0][2].ToString());
                if (predictive.Tables[2].Select("Defect = '" + s + "'").Count() > 0) p3 = float.Parse(predictive.Tables[2].Select("Defect = '" + s + "'")[0][2].ToString());
                if (predictive.Tables[3].Select("Defect = '" + s + "'").Count() > 0) p4 = float.Parse(predictive.Tables[3].Select("Defect = '" + s + "'")[0][2].ToString());

                DataRow r = dt2.NewRow();
                r[0] = s;
                r[1] = GetP(p1, p2, p3, p4);
                dt2.Rows.Add(r);
                dt2.AcceptChanges();
            }

            DataView dv = dt2.DefaultView;
            dv.Sort = "Per";

            chrtseason.DataSource = dv.ToTable();
            chrtseason.Series[0].XValueMember = "Defect";
            chrtseason.Series[0].YValueMembers = "Per";
            chrtseason.DataBind();
        }
        private float GetP(float p1, float p2, float p3, float p4)
        {
            float pp1 = 0, pp2 = 0, pp3 = 0;

            pp1 = p2 == 0 ? p1 : (p2 - p1) < 0 ? p2 : (p2 + (p2 - p1) + (p2 - p1) * p1);
            pp2 = p4 == 0 ? p3 : (p4 - p3) < 0 ? p4 : (p4 + (p4 - p3) + (p4 - p3) * p3);
            pp3 = pp2 == 0 ? pp1 : (pp2 - pp1) < 0 ? pp2 : (pp2 + (pp2 - pp1) + (pp2 - pp1) * pp1);

            return pp3;
        }
        private void btcopytonew_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] r = ds.Tables[6].Select("DocNo = '" + DocNo + "'");

                if (r.Length > 0)
                {
                    frmitem frm = new frmitem(true, true, cmt, this);
                    frm.DataRow = r[0];

                    frm.ShowDialog();
                }
            }
            catch { }
        }

        private void btdelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] r = ds.Tables[6].Select("DocNo = '" + DocNo + "'");

                if (MessageBox.Show("Are you sure to delete this record <DocNo : " + DocNo + ", Style : " + r[0]["Style"] + "> ?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    kn.Ghi("delete from sromstrdel where DocNo = '" + DocNo + "'" +
                            "insert into sromstrdel select * from sromstr where DocNo = '" + DocNo + "' \n" +
                            "delete from sromstr where DocNo = '" + DocNo + "' \n" +
                            "delete from sroasm where DocNo = '" + DocNo + "'");

                    if (kn.ErrorMessage == "") worker.RunWorkerAsync();
                    else MessageBox.Show(kn.ErrorMessage);
                }
            }
            catch { }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!run)
            {
                processbar.Visible = true;
                txtloading.Visible = true;
                worker.RunWorkerAsync();
                run = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //notifyIcon1.BalloonTipTitle = "Mer Tracing";
            //notifyIcon1.BalloonTipText = "Sample Information " + DateTime.Now.ToString("ss");
            //notifyIcon1.ShowBalloonTip(1000);

            if (Temp.Printers.Count > 0)
            {
                //prt.WindowState = FormWindowState.Minimized;
                if (prt.IsDisposed) prt = new frmprinter();

                prt.Show();
            }
        }

        private void openAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = Temp.culture;
            Application.ExitThread();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; Hide();
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            MessageBox.Show("You are in the NotifyIcon.BalloonTipClicked event.");
            //Show();
            //OpenRow(notifyIcon1.BalloonTipText);
        }

        private void cmbvalue_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView dtg = sender as DataGridView;
                string docno = dtg.Rows[e.RowIndex].Cells[0].Value.ToString();
                OpenRow(docno);
            }
        }

        private void btsearch_Click(object sender, EventArgs e)
        {
            if (txtdocno.Text != "") OpenRow(txtdocno.Text);

            txtdocno.Text = "";

            //frmalert frm = new frmalert(this, ds.Tables[6].Rows[0]["DocNo"].ToString());
            //frm.ShowAlert("Notification", "Sample welcome !!!");
        }

        private void dataGridView6_Paint(object sender, PaintEventArgs e)
        {
            DataGridView dtg = sender as DataGridView;

            if (dtg.Rows.Count > 0)
            {
                try
                {
                    if (repaint == 0)
                    {
                        foreach (DataGridViewRow rw in dtg.Rows)
                        {
                            if (dtg.Name == "dataGridView6")
                            {
                                DataGridViewCheckBoxCell cell1 = new DataGridViewCheckBoxCell();
                                cell1.ValueType = typeof(bool);
                                cell1.Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };

                                if (rw.Cells["LaserCut"].Value.ToString() == "0") cell1.Value = false;
                                else cell1.Value = true;
                                rw.Cells["LaserCut"] = cell1;

                                DataGridViewCheckBoxCell cell2 = new DataGridViewCheckBoxCell();
                                cell2.ValueType = typeof(bool);
                                cell2.Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };

                                if (rw.Cells["Bonding"].Value.ToString() == "0") cell2.Value = false;
                                else cell2.Value = true;
                                rw.Cells["Bonding"] = cell2;

                                DataGridViewCheckBoxCell cell3 = new DataGridViewCheckBoxCell();
                                cell3.ValueType = typeof(bool);
                                cell3.Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };

                                if (rw.Cells["Embroidery"].Value.ToString() == "0") cell3.Value = false;
                                else cell3.Value = true;
                                rw.Cells["Embroidery"] = cell3;

                                DataGridViewCheckBoxCell cell4 = new DataGridViewCheckBoxCell();
                                cell4.ValueType = typeof(bool);
                                cell4.Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };

                                if (rw.Cells["Embossing"].Value.ToString() == "0") cell4.Value = false;
                                else cell4.Value = true;
                                rw.Cells["Embossing"] = cell4;

                                DataGridViewCheckBoxCell cell5 = new DataGridViewCheckBoxCell();
                                cell5.ValueType = typeof(bool);
                                cell5.Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };

                                if (rw.Cells["Debossing"].Value.ToString() == "0") cell5.Value = false;
                                else cell5.Value = true;
                                rw.Cells["Debossing"] = cell5;

                                DataGridViewCheckBoxCell cell6 = new DataGridViewCheckBoxCell();
                                cell6.ValueType = typeof(bool);
                                cell6.Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };

                                if (rw.Cells["HeatTransfer"].Value.ToString() == "0") cell6.Value = false;
                                else cell6.Value = true;
                                rw.Cells["HeatTransfer"] = cell6;

                                DataGridViewCheckBoxCell cell7 = new DataGridViewCheckBoxCell();
                                cell7.ValueType = typeof(bool);
                                cell7.Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };

                                if (rw.Cells["Printing"].Value.ToString() == "0") cell7.Value = false;
                                else cell7.Value = true;
                                rw.Cells["Printing"] = cell7;

                                DataGridViewCheckBoxCell cell8 = new DataGridViewCheckBoxCell();
                                cell8.ValueType = typeof(bool);
                                cell8.Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };

                                if (rw.Cells["PadPrint"].Value.ToString() == "0") cell8.Value = false;
                                else cell8.Value = true;
                                rw.Cells["PadPrint"] = cell8;

                                DataGridViewCheckBoxCell cell9 = new DataGridViewCheckBoxCell();
                                cell9.ValueType = typeof(bool);
                                cell9.Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };

                                if (rw.Cells["SubPrint"].Value.ToString() == "0") cell9.Value = false;
                                else cell9.Value = true;
                                rw.Cells["SubPrint"] = cell9;
                            }

                            if (rw.Cells["Status"].Value != null)
                            {
                                switch (rw.Cells["Status"].Value.ToString())
                                {
                                    case "New":
                                        rw.DefaultCellStyle.BackColor = AppConfig.New;
                                        break;
                                    case "Incomplete":
                                        rw.DefaultCellStyle.BackColor = AppConfig.Incomplete;
                                        break;
                                    case "In Queue":
                                        rw.DefaultCellStyle.BackColor = AppConfig.InQueue;
                                        break;
                                    case "In Production":
                                        rw.DefaultCellStyle.BackColor = AppConfig.InDecoration;
                                        break;
                                    case "In Decoration":
                                        rw.DefaultCellStyle.BackColor = AppConfig.InDecoration;
                                        break;
                                    case "In Sewing":
                                        rw.DefaultCellStyle.BackColor = AppConfig.InSewing;
                                        break;
                                    case "CFT Passed":
                                        rw.DefaultCellStyle.BackColor = AppConfig.CFTPassed;
                                        break;
                                    case "QC Passed":
                                        rw.DefaultCellStyle.BackColor = AppConfig.CFTPassed;
                                        break;
                                    case "Finish":
                                        rw.DefaultCellStyle.BackColor = AppConfig.FinishOnTime;
                                        break;
                                    case "Finish On Time":
                                        rw.DefaultCellStyle.BackColor = AppConfig.FinishOnTime;
                                        break;
                                    case "Finish in Delay":
                                        rw.DefaultCellStyle.BackColor = AppConfig.FinishDelay;
                                        break;
                                }
                            }

                        }
                        //dataGridView6.Columns["Status"].Frozen = true;

                        repaint = 1;
                    }
                }
                catch { }
            }
        }
        private void dataGridView6_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView6_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmlogin frm = new frmlogin();
            frm.ShowDialog();

            Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmSetColor frm = new frmSetColor(this);
            frm.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmchangeuserpass frm = new frmchangeuserpass();
            frm.ShowDialog();
        }

        private void changeowner_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] r = ds.Tables[6].Select("DocNo = '" + DocNo + "'");

                if (r.Length > 0)
                {
                    frmchangeowner frm = new frmchangeowner(r.CopyToDataTable(), this);
                    frm.ShowDialog();
                }
                else
                    MessageBox.Show("No row selected !!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { }
        }

        private void datefrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TimeSpan tsp = dateto.Value - datefrom.Value;

                if (tsp.TotalDays >= 0) Filter();
            }
            catch { }
        }

        private void notification_Click(object sender, EventArgs e)
        {
            bool s = true;

            if (AppConfig.Notification)
            {
                string input = "";
                ShowInputDialog(ref input);

                if (input == "?" + DateTime.Now.Day)
                {
                    notification.Text = "Alert Notification : Off"; s = false;
                }
                else MessageBox.Show("Authentication failed !!!");
            }
            else { notification.Text = "Alert Notification : On"; s = true; }

            AppConfig.Notification = s;
            Temp.SaveConfig();
        }
        private static DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(300, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            inputBox.ClientSize = size;
            inputBox.Text = "Authentication";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.PasswordChar = '*';
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }
        private void btrftdownload_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.1.248:8083/run2.php?s=1", json = ""; // Replace with your JSON API URL

            using (WebClient client = new WebClient())
            {
                try
                {
                    json = client.DownloadString(url);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("HTTP request error: " + ex.Message);
                }

                client.DownloadStringCompleted += delegate
                {
                    MessageBox.Show(json);
                };
            }
            //List<ClassJson> people = JsonConvert.DeserializeObject<List<ClassJson>>(json);
            DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);

                // Get all the properties of the type
                PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in props)
                {
                    // Create a new column for each property
                    dataTable.Columns.Add(prop.Name, prop.PropertyType);
                }

                foreach (T item in items)
                {
                    var values = props.Select(prop => prop.GetValue(item)).ToArray();
                    dataTable.Rows.Add(values);
                }

                return dataTable;
            }
        }
        //private void LoadRFTReport1()
        //{
        //    try
        //    {
        //        DataSet ds = kn.Doc("exec SampleQueueLoading 8, '" + rftdate1.Value.ToString("yyyyMMdd") + "', '" + rftdate2.Value.ToString("yyyyMMdd") + "', '" + ads + "-" + cmbcft2.Text + "'");

        //        dtgrftreport.DataSource = ds.Tables[0];

        //        txtrows.Text = "Rows : " + dtgrftreport.RowCount;

        //        charttop5.Series["Series1"].Points.Clear();

        //        foreach (DataRow r in ds.Tables[1].Rows) charttop5.Series["Series1"].Points.AddXY(r[0].ToString(), float.Parse(r[1].ToString()));

        //        dtgtopdefect.DataSource = ds.Tables[2];

        //        lbstyle.Text = ds.Tables[3].Rows[0][0].ToString();
        //        lbinsqty.Text = ds.Tables[3].Rows[0][1].ToString();
        //        lbpassqty.Text = ds.Tables[3].Rows[0][2].ToString();
        //        lbfailqty.Text = ds.Tables[3].Rows[0][3].ToString();
        //        lbavgrft.Text = ds.Tables[3].Rows[0][4].ToString() + "%";
        //    }
        //    catch { }
        //}
        //private void LoadRFTReport2()
        //{
        //    try
        //    {
        //        DataSet ds = kn.Doc("exec SampleQueueLoading 27, '" + cmbmonth2.Text.Split(' ')[0] + "', '', '" + ads + "-" + cmbcft2.Text + "'");

        //        dtgrftreport.DataSource = ds.Tables[0];

        //        txtrows.Text = "Rows : " + dtgrftreport.RowCount;

        //        charttop5.Series["Series1"].Points.Clear();

        //        foreach (DataRow r in ds.Tables[1].Rows) charttop5.Series["Series1"].Points.AddXY(r[0].ToString(), float.Parse(r[1].ToString()));

        //        dtgtopdefect.DataSource = ds.Tables[2];

        //        lbstyle.Text = ds.Tables[3].Rows[0][0].ToString();
        //        lbinsqty.Text = ds.Tables[3].Rows[0][1].ToString();
        //        lbpassqty.Text = ds.Tables[3].Rows[0][2].ToString();
        //        lbfailqty.Text = ds.Tables[3].Rows[0][3].ToString();
        //        lbavgrft.Text = ds.Tables[3].Rows[0][4].ToString() + "%";
        //    }
        //    catch { }
        //}
        //private void LoadRFTReport3()
        //{
        //    try
        //    {
        //        DataSet ds = kn.Doc("exec SampleQueueLoading 28, '" + cmbseason2.Text + "', '', '" + ads + "-" + cmbcft2.Text + "'");

        //        dtgrftreport.DataSource = ds.Tables[0];

        //        txtrows.Text = "Rows : " + dtgrftreport.RowCount;

        //        charttop5.Series["Series1"].Points.Clear();

        //        foreach (DataRow r in ds.Tables[1].Rows) charttop5.Series["Series1"].Points.AddXY(r[0].ToString(), float.Parse(r[1].ToString()));

        //        dtgtopdefect.DataSource = ds.Tables[2];

        //        lbstyle.Text = ds.Tables[3].Rows[0][0].ToString();
        //        lbinsqty.Text = ds.Tables[3].Rows[0][1].ToString();
        //        lbpassqty.Text = ds.Tables[3].Rows[0][2].ToString();
        //        lbfailqty.Text = ds.Tables[3].Rows[0][3].ToString();
        //        lbavgrft.Text = ds.Tables[3].Rows[0][4].ToString() + "%";
        //    }
        //    catch { }
        //}
        //private void LoadRFTReport4()
        //{
        //    try
        //    {
        //        DataSet ds = kn.Doc("exec SampleQueueLoading 29, '" + cmbstyle2.Text + "', '', '" + ads + "-" + cmbcft2.Text + "'");

        //        dtgrftreport.DataSource = ds.Tables[0];

        //        txtrows.Text = "Rows : " + dtgrftreport.RowCount;

        //        charttop5.Series["Series1"].Points.Clear();

        //        foreach (DataRow r in ds.Tables[1].Rows) charttop5.Series["Series1"].Points.AddXY(r[0].ToString(), float.Parse(r[1].ToString()));

        //        dtgtopdefect.DataSource = ds.Tables[2];

        //        lbstyle.Text = ds.Tables[3].Rows[0][0].ToString();
        //        lbinsqty.Text = ds.Tables[3].Rows[0][1].ToString();
        //        lbpassqty.Text = ds.Tables[3].Rows[0][2].ToString();
        //        lbfailqty.Text = ds.Tables[3].Rows[0][3].ToString();
        //        lbavgrft.Text = ds.Tables[3].Rows[0][4].ToString() + "%";
        //    }
        //    catch { }
        //}
        //private void LoadRFTReport5()
        //{
        //    try
        //    {
        //        DataSet ds = kn.Doc("exec SampleQueueLoading 30, '" + cmbstage2.Text.Substring(0, 2) + "', '', '" + ads + "-" + cmbcft2.Text + "'");

        //        dtgrftreport.DataSource = ds.Tables[0];

        //        txtrows.Text = "Rows : " + dtgrftreport.RowCount;

        //        charttop5.Series["Series1"].Points.Clear();

        //        foreach (DataRow r in ds.Tables[1].Rows) charttop5.Series["Series1"].Points.AddXY(r[0].ToString(), float.Parse(r[1].ToString()));

        //        dtgtopdefect.DataSource = ds.Tables[2];

        //        lbstyle.Text = ds.Tables[3].Rows[0][0].ToString();
        //        lbinsqty.Text = ds.Tables[3].Rows[0][1].ToString();
        //        lbpassqty.Text = ds.Tables[3].Rows[0][2].ToString();
        //        lbfailqty.Text = ds.Tables[3].Rows[0][3].ToString();
        //        lbavgrft.Text = ds.Tables[3].Rows[0][4].ToString() + "%";
        //    }
        //    catch { }
        //}
        private void limitQtyByDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmlimit frm = new frmlimit();
            frm.ShowDialog();
        }

        private void showDefectImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string img = "";

                if (tabControl1.SelectedIndex == 6) img = dtginfor.Rows[row].Cells["Picture"].Value.ToString();

                string path = kn.Doc("select * from InlineQCSystem where STT = 63").Tables[0].Rows[0][0].ToString();

                MessageBox.Show(Path.Combine(path, img));
                Process.Start(Path.Combine(path, img));
            }
            catch { }
        }

        private void copyDocNoSampleIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(DocNo);
        }

        private void txtdocno_Click(object sender, EventArgs e)
        {
            //txtdocno
        }

        private void logCheckingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmchecklog frm = new frmchecklog();
            frm.ShowDialog();
        }

        private void btdownload_Click(object sender, EventArgs e)
        {
            dtgdetail.DataSource = null;
            dtginfor.DataSource = null;

            switch (cfttrend)
            {
                case 1:
                    LoadCFTTrend(datetime1.Value, datetime2.Value);
                    break;
                case 2:
                    int mon = 3;
                    if (cmbmonthly.Text == "") cmbmonthly.SelectedIndex = 0;
                    else mon = int.Parse(cmbmonthly.Text.Split(' ')[0]);

                    LoadCFTTrend(mon);
                    break;
                case 3:
                    if (cmbseason.Text == "") MessageBox.Show("Please select the Season !!!");
                    else LoadCFTTrend(cmbseason.Text, "");
                    break;
                case 4:
                    if (cmbstyle.Text == "") MessageBox.Show("Please select the Style !!!");
                    else LoadCFTTrend();
                    break;
                case 5:
                    if (cmbstage.Text == "") MessageBox.Show("Please select the Stage !!!");
                    else LoadCFTTrend("", cmbstage.Text);
                    break;
                case 6:
                    if (cmbbu.Text == "") MessageBox.Show("Please select the BU !!!");
                    else LoadCFTTrend("", cmbbu.Text);
                    break;
            }
        }

        private void sMPCapacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmcapacity frm = new frmcapacity();
            frm.ShowDialog();
        }

        private void capacityTipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                int index = (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);

                frmhelp frm = new frmhelp(3, false);//index + 1
                frm.Show();
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            //foreach (string file in files) MessageBox.Show(file);

            //using (var stream = File.Open(files[0], FileMode.Open, FileAccess.Read))
            //{
            //    using (var reader = ExcelReaderFactory.CreateReader(stream))
            //    {
            //        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
            //        {
            //            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
            //            {
            //                UseHeaderRow = true
            //            }
            //        });

            //        dataGridView7.DataSource = result.Tables[0];
            //    }
            //}

            frmsmpupload frm = new frmsmpupload(files[0]);
            frm.ShowDialog();
        }

        private void sMPUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Excel File (*.xlsx)|*.xlsx|Excel File (*.xls)|*.xls";
            op.Title = "Select your file";


            if (op.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(op.FileName);
                frmsmpupload frm = new frmsmpupload(op.FileName);
                frm.ShowDialog();
            }
        }

        private void addNewStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmaddnewstyle frm = new frmaddnewstyle();
            frm.ShowDialog();
        }

        private void cmbvalue_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmbvalue_KeyUp(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            if (e.KeyCode == Keys.Enter)
            {
                //MessageBox.Show("a");
                bool ori = cmbvalue.Text.Length < len; len = cmbvalue.Text.Length;
                Filter(ori);
            }
        }

        private void getDataFromERPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmgetdatafromERP frm = new frmgetdatafromERP();
            frm.ShowDialog();
        }

        private void standardSMVByGmtTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmsmv frm = new frmsmv(kn);
            frm.ShowDialog();
        }

        private void dataGridView7_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void accumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmassumesmv frm = new frmassumesmv(kn, ads);
            frm.ShowDialog();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string tt = "";
            if (cmbcolumn.Text != "" && cmbcolumn.Text != "DocNo")
            {
                if (datefrom.Visible)
                {
                    string qy = "exec SampleQueueLoading 19, '" + ads + "', '" + cmbcolumn.Text + "', '" + datefrom.Value.ToString("yyyyMMdd") + ":" + dateto.Value.ToString("yyyyMMdd") + "'";

                    dt = kn.Doc(qy).Tables[0];

                    tt = cmbcolumn.Text.ToUpper() + " : " + datefrom.Value.ToString("yyyyMMdd") + " - " + dateto.Value.ToString("yyyyMMdd");
                }
                else
                {
                    string qy = "exec SampleQueueLoading 19, '" + ads + "', '" + cmbcolumn.Text + "', '" + cmbvalue.Text.ToUpper() + "'";

                    dt = kn.Doc(qy).Tables[0];

                    tt = cmbcolumn.Text.ToUpper() + " : " + cmbvalue.Text;
                }
            }

            if (dt.Rows.Count > 0)
            {
                frmmoredata frm = new frmmoredata(dt, tt);
                frm.ShowDialog();
            }
        }

        private void scanSampleIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmscan frm = new frmscan(this);
            frm.ShowDialog();
        }

        private void transferSampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmitem frm = new frmitem(false, false, new List<int>(), this, true);
            frm.DataRow = ds.Tables[6].Rows[0];

            frm.ShowDialog();
        }

        private void userManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = @"http://192.168.1.248/AppGuiding/SAMLEQUEUE.pptx";

            Process.Start(url);
        }

        private void dtgsampleidmaster_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string docno = dtgsampleidmaster.Rows[e.RowIndex].Cells[0].Value.ToString();

                DataRow r = ds.Tables[6].Select("DocNo = '" + docno + "'")[0];

                DataTable dt = kn.Doc("select * from sromstrsampleid where DocNo = '" + docno + "' order by RecNo").Tables[0];

                frmsqid frm = new frmsqid(dt, r["Style"].ToString(), r["Season"].ToString(), r["Qty"].ToString(), r["SmpType"].ToString(), r["Status"].ToString());
                frm.ShowDialog();
            }
        }

        private void cmbseason_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btpredictive_Click(object sender, EventArgs e)
        {
            if (cmbss1.Text != "" && cmbss2.Text != "" && cmbss3.Text != "" && cmbss4.Text != "") LoadPredictive();
        }

        private void cmbss4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            switch (cmb.Name)
            {
                case "cmbss4":
                    if (cmb.SelectedIndex + 1 < cmb.Items.Count) cmbss3.SelectedIndex = cmb.SelectedIndex + 1;
                    if (cmb.SelectedIndex + 2 < cmb.Items.Count) cmbss2.SelectedIndex = cmb.SelectedIndex + 2;
                    if (cmb.SelectedIndex + 3 < cmb.Items.Count) cmbss1.SelectedIndex = cmb.SelectedIndex + 3;
                    break;
                case "cmbss3":
                    if (cmb.SelectedIndex + 1 < cmb.Items.Count) cmbss2.SelectedIndex = cmb.SelectedIndex + 1;
                    if (cmb.SelectedIndex + 2 < cmb.Items.Count) cmbss1.SelectedIndex = cmb.SelectedIndex + 2;
                    break;
                case "cmbss2":
                    if (cmb.SelectedIndex + 1 < cmb.Items.Count) cmbss1.SelectedIndex = cmb.SelectedIndex + 1;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();

            if (path.ShowDialog() == DialogResult.OK)
            {
                string file = Path.Combine(path.SelectedPath, DateTime.Now.ToString("yyMMddHHmmss") + ".jpg");

                Bitmap bm = new Bitmap(panel11.Width, panel11.Height);
                panel11.DrawToBitmap(bm, new Rectangle(0, 0, bm.Width, bm.Height));

                bm.Save(file, ImageFormat.Jpeg);

                Process.Start(file);
            }
        }

        private void dtgrftreport_DataSourceChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Changed");
        }

        private void dtgrftreport_Paint(object sender, PaintEventArgs e)
        {
            DataGridView dtg = sender as DataGridView;
            if (dtg.RowCount > 0)
            {
                foreach (DataGridViewRow r in dtg.Rows)
                {
                    if (r.Cells["CD"].Value != null && r.Cells["CD"].Value.ToString() == "*")
                        r.Cells["CD"].Style.BackColor = Color.LightPink;
                }
            }
        }

        private void dtgdetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dtginfor.DataSource = null;

                DataGridViewRow r = dtgdetail.SelectedRows[0];

                DataTable dt = kn.Doc("exec SampleQueueLoading 34, '" + r.Cells["Style"].Value.ToString() + "', '" + r.Cells["Stage"].Value.ToString() + "', '" + ads + "-" + cmbcft1.Text + "'").Tables[0];

                dtginfor.DataSource = dt;
            }
        }

        private void cmbseason2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbbu2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rddaily2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = sender as RadioButton;

            switch (rd.Name)
            {
                case "rddaily2":
                    if (rd.Checked) cfttrend2 = 1;
                    break;
                case "rdmonthly2":
                    if (rd.Checked) cfttrend2 = 2;
                    break;
                case "rdseason2":
                    if (rd.Checked) cfttrend2 = 3;
                    break;
                case "rdstyle2":
                    if (rd.Checked) cfttrend2 = 4;
                    break;
                case "rdstage2":
                    if (rd.Checked) cfttrend2 = 5;
                    break;
                case "rdbu2":
                    if (rd.Checked) cfttrend2 = 6;
                    break;
            }
        }

        private void rddaily_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = sender as RadioButton;

            switch (rd.Name)
            {
                case "rddaily":
                    if (rd.Checked) cfttrend = 1;
                    break;
                case "rdmonthly":
                    if (rd.Checked) cfttrend = 2;
                    break;
                case "rdseason":
                    if (rd.Checked) cfttrend = 3;
                    break;
                case "rdstyle":
                    if (rd.Checked) cfttrend = 4;
                    break;
                case "rdstage":
                    if (rd.Checked) cfttrend = 5;
                    break;
                case "rdbu":
                    if (rd.Checked) cfttrend = 6;
                    break;
            }

            //MessageBox.Show(cfttrend.ToString());
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (AppConfig.Notification) LoadComment();
        }

        private void btchart_Click(object sender, EventArgs e)
        {
            Process.Start("http://192.168.1.248:8083/cftreport.php?d=" + ads);
        }

        private void adjustPrinterSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmadjustprinter frm = new frmadjustprinter();
            frm.ShowDialog();
        }

        private void LoadComment()
        {
            try
            {
                DataTable comm = kn.Doc("exec SmartLineLoadData 4,'" + ads + "','" + Temp.User + "'", 60).Tables[0];

                foreach (DataRow r in comm.Rows)
                {
                    if (r["Readed"].ToString() != "Yes")
                    {
                        if (r["Partner"].ToString() == "All" || r["Partner"].ToString().Contains(Temp.Dept))
                        {
                            if (Exit(r["DocNo"].ToString()))
                            {
                                frmalert frm = new frmalert(this, r["DocNo"].ToString());
                                frm.ShowAlert(r["CmtOwner"].ToString() + "/" + r["DeptCode"].ToString() + " - " + r["DocNo"].ToString(), r["CmtContent"].ToString());
                            }
                        }
                    }
                }

                bool Exit(string dn)
                {
                    return ds.Tables[6].Select("DocNo = '" + dn + "'").Length == 0 ? false : true;
                }
            }
            catch { }
        }
        private void dataGridView6_Sorted(object sender, EventArgs e)
        {
            repaint = 0;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex; col = e.ColumnIndex;

            DataGridView dtg = sender as DataGridView;
            if (e.RowIndex >= 0) DocNo = dtg.Rows[e.RowIndex].Cells["DocNo"].Value.ToString();

            //MessageBox.Show(dtg.Columns[e.ColumnIndex].ValueType.ToString());
        }
        private void SetBrowserFeatureControl()
        {
            // http://msdn.microsoft.com/en-us/library/ee330720(v=vs.85).aspx

            // FeatureControl settings are per-process
            var fileName = System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

            // make the control is not running inside Visual Studio Designer
            if (String.Compare(fileName, "devenv.exe", true) == 0 || String.Compare(fileName, "XDesProc.exe", true) == 0)
                return;

            SetBrowserFeatureControlKey("FEATURE_BROWSER_EMULATION", fileName, GetBrowserEmulationMode()); // Webpages containing standards-based !DOCTYPE directives are displayed in IE10 Standards mode.
            SetBrowserFeatureControlKey("FEATURE_AJAX_CONNECTIONEVENTS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_MANAGE_SCRIPT_CIRCULAR_REFS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_DOMSTORAGE ", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_GPU_RENDERING ", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_IVIEWOBJECTDRAW_DMLT9_WITH_GDI  ", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_DISABLE_LEGACY_COMPRESSION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_LOCALMACHINE_LOCKDOWN", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_OBJECT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_SCRIPT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_DISABLE_NAVIGATION_SOUNDS", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_SCRIPTURL_MITIGATION", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_SPELLCHECKING", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_STATUS_BAR_THROTTLING", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_TABBED_BROWSING", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_VALIDATE_NAVIGATE_URL", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_DOCUMENT_ZOOM", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_POPUPMANAGEMENT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_WEBOC_MOVESIZECHILD", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_ADDON_MANAGEMENT", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_WEBSOCKET", fileName, 1);
            SetBrowserFeatureControlKey("FEATURE_WINDOW_RESTRICTIONS ", fileName, 0);
            SetBrowserFeatureControlKey("FEATURE_XMLHTTP", fileName, 1);
        }
        private void SetBrowserFeatureControlKey(string feature, string appName, uint value)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(
                String.Concat(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\", feature),
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(appName, (UInt32)value, RegistryValueKind.DWord);
            }
        }
        private UInt32 GetBrowserEmulationMode()
        {
            int browserVersion = 7;
            using (var ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                var version = ieKey.GetValue("svcVersion");
                if (null == version)
                {
                    version = ieKey.GetValue("Version");
                    if (null == version)
                        throw new ApplicationException("Microsoft Internet Explorer is required!");
                }
                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }

            UInt32 mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode. Default value for Internet Explorer 11.
            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. Default value for applications hosting the WebBrowser Control.
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. Default value for Internet Explorer 8
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode. Default value for Internet Explorer 9.
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE10 mode. Default value for Internet Explorer 10.
                    break;
                default:
                    // use IE11 mode by default
                    break;
            }

            return mode;
        }
    }
}
