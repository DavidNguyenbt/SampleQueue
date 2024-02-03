using CSDL;
using OfficeOpenXml.Drawing.Slicer.Style;
using SampleQueue.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleQueue
{
    public partial class frmitem : Form
    {
        Connect kn;
        Connect erp;
        BackgroundWorker worker;
        DataSet ds = new DataSet();
        List<int> tb = new List<int>();
        bool show = false, copy = false, isopen = true, tf = false;
        Form1 fr1;
        string ads = "", versioncount = "", dept = "", bookedqty = "0", email = "", updateurgent = "";
        public DataRow DataRow { get; set; }
        int ss = 0, capacity = 0, qty = 0, capacityid = 0, urgent = 0;//urgent 0: normal, 1: not approved, 2: approved
        decimal smv = 0;
        Graphics grp;
        DateTimePicker dtk = new DateTimePicker();
        bool unlock = false;
        int dem = 0, row = 0;
        List<SizeQty> ls = new List<SizeQty>();
        public List<string> color = new List<string>();
        public List<string> size = new List<string>();
        //System.Globalization.CultureInfo culture;
        public frmitem(bool s, bool cp, List<int> cm, Form1 f, bool _tf = false)
        {
            InitializeComponent();

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.DoWork += Worker_DoWork;

            show = s; tb = cm; fr1 = f; copy = cp; tf = _tf;

            if (show) ss = 0; else ss = 1;

            grp = this.CreateGraphics();

            //culture = Thread.CurrentThread.CurrentCulture;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadItem();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            //fr1.StopWorking();
            progressBar1.Visible = false;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void orderdate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTimePicker date = sender as DateTimePicker;

                DataTable check = kn.Doc("SELECT * FROM [DtradeProduction].[dbo].[00SampleType] where SampleType = 'Holiday' and Description = '" + date.Value.ToString("yyyyMMdd") + "'").Tables[0];
                if (check.Rows.Count > 0) date.Format = DateTimePickerFormat.Custom;
                else
                {
                    dtk = date;
                    date.Format = DateTimePickerFormat.Short;

                    //TimeSpan tm = DateTime.Now - date.Value; //TimeSpan sp = rsddate.Value - shiptomerdate.Value; MessageBox.Show(sp.TotalDays.ToString());

                    //if (tm.TotalDays > 90) date.Format = DateTimePickerFormat.Custom;
                    //else date.Format = DateTimePickerFormat.Short;

                    if (date.Value.DayOfWeek.ToString() == "Sunday") date.Value = date.Value.AddDays(1);

                    if (date.Name == "rsddate") Run();

                    if (date.Name == "csddate")
                    {
                        if (rsddate.Format == DateTimePickerFormat.Custom)
                        {
                            date.Format = DateTimePickerFormat.Custom;
                        }
                        else Run();
                    }
                }

                void Run()
                {
                    DateTime rsd = DateTime.Now.Hour >= 13 ? date.Value.AddDays(1) : date.Value;
                    //rsddate.Value = rsd;

                    int qty = 0;
                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        for (int j = 1; j < dataGridView2.ColumnCount; j++)
                        {
                            string vl = dataGridView2.Rows[i].Cells[j].Value.ToString();

                            try
                            {
                                if (vl != "") qty += int.Parse(vl);
                            }
                            catch { }
                        }
                    }

                    if (CheckLimit(date) && ss > 0 && urgent == 0 && dept == "PUMA")
                    {
                        MessageBox.Show("Limit SMV <RSD/CSD : " + date.Value.ToString("dd/MM/yyyy") + " - Booked SMV/Total SMV : " + bookedqty + "/" + capacity + "> : " + (qty * smv) + ", you have to change the RSD to another date !!!", "LIMIT SMV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        date.Format = DateTimePickerFormat.Custom;
                    }
                    else
                    {
                        manualplan.Format = DateTimePickerFormat.Short; manualplan.Value = rsd.AddDays(-11);
                        fabricplan.Format = DateTimePickerFormat.Short; fabricplan.Value = rsd.AddDays(-12);
                        patternplanplan.Format = DateTimePickerFormat.Short; patternplanplan.Value = rsd.AddDays(-17);
                        finishplan.Format = DateTimePickerFormat.Short; finishplan.Value = rsd.AddDays(-15);
                        markerplan.Format = DateTimePickerFormat.Short; markerplan.Value = rsd.AddDays(-15);
                        markerreleaseplan.Format = DateTimePickerFormat.Short; markerreleaseplan.Value = rsd.AddDays(-14);
                        cuttingplan.Format = DateTimePickerFormat.Short; cuttingplan.Value = rsd.AddDays(-14);
                        decorationtrimsplan.Format = DateTimePickerFormat.Short; decorationtrimsplan.Value = rsd.AddDays(-11);

                        SetPlanDecoration(rsd, date);
                    }

                    ss++;
                }
            }
            catch { }
        }
        private bool CheckLimit(DateTimePicker d)
        {
            bool rs = false;
            bookedqty = "0";
            try
            {
                if (d.Format != DateTimePickerFormat.Custom)
                {
                    string qr = "exec SampleQueueLoading 4, '" + d.Value.ToString("yyyyMMdd") + "', '" + dept + "','" + txtdocno.Text + "'";
                    //MessageBox.Show(qr);
                    DataTable check = kn.Doc(qr).Tables[0];
                    //MessageBox.Show(check.Rows.Count.ToString());

                    if (check.Rows.Count > 0) bookedqty = check.Rows[0][2].ToString();

                    decimal bl = bookedqty == "" ? 0 : decimal.Parse(bookedqty);

                    decimal bk = qty * smv;

                    if (bk > capacity || (bk + bl) > capacity) rs = true;
                }
            }
            catch { }

            return rs;
        }
        private void SetPlanDecoration(DateTime rsd, DateTimePicker dk)
        {
            try
            {
                int i = -13;
                if (rsddate.Format == DateTimePickerFormat.Short)
                {
                    if (printing.Checked) { printingreleaseplan.Format = DateTimePickerFormat.Short; printingreleaseplan.Value = rsd.AddDays(i + 3); printingplan.Format = DateTimePickerFormat.Short; printingplan.Value = rsd.AddDays(i); i += 3; }
                    if (embroidery.Checked) { embroideryreleaseplan.Format = DateTimePickerFormat.Short; embroideryreleaseplan.Value = rsd.AddDays(i + 1); embroideryplan.Format = DateTimePickerFormat.Short; embroideryplan.Value = rsd.AddDays(i); i += 1; }
                    if (heat.Checked) { heatreleaseplan.Format = DateTimePickerFormat.Short; heatreleaseplan.Value = rsd.AddDays(i); heatplan.Format = DateTimePickerFormat.Short; heatplan.Value = rsd.AddDays(i); i += 1; }
                    if (debossing.Checked) { debossingreleaseplan.Format = DateTimePickerFormat.Short; debossingreleaseplan.Value = rsd.AddDays(i + 1); debossingplan.Format = DateTimePickerFormat.Short; debossingplan.Value = rsd.AddDays(i); i += 1; }
                    if (embossing.Checked) { embossingreleaseplan.Format = DateTimePickerFormat.Short; embossingreleaseplan.Value = rsd.AddDays(i + 1); embossingplan.Format = DateTimePickerFormat.Short; embossingplan.Value = rsd.AddDays(i); i += 1; }
                    if (bonding.Checked) { bondingreleaseplan.Format = DateTimePickerFormat.Short; bondingreleaseplan.Value = rsd.AddDays(i + 1); bondingplan.Format = DateTimePickerFormat.Short; bondingplan.Value = rsd.AddDays(i); i += 1; }
                    if (lazer.Checked) { lazerreleaseplan.Format = DateTimePickerFormat.Short; lazerreleaseplan.Value = rsd.AddDays(i + 1); lazerplan.Format = DateTimePickerFormat.Short; lazerplan.Value = rsd.AddDays(i); i += 1; }
                    if (padprint.Checked) { padprintreleaseplan.Format = DateTimePickerFormat.Short; padprintreleaseplan.Value = rsd.AddDays(i); padprintplan.Format = DateTimePickerFormat.Short; padprintplan.Value = rsd.AddDays(i); i += 1; }
                    if (subprint.Checked) { subprintreleaseplan.Format = DateTimePickerFormat.Short; subprintreleaseplan.Value = rsd.AddDays(i + 2); subprintplan.Format = DateTimePickerFormat.Short; subprintplan.Value = rsd.AddDays(i); i += 2; }

                    sewingplanstart.Format = DateTimePickerFormat.Short; sewingplanstart.Value = rsd.AddDays(i);
                    sewingplanfinish.Format = DateTimePickerFormat.Short; sewingplanfinish.Value = rsd.AddDays(-1);
                }

                dtk = dk;
            }
            catch { }
        }
        private void resetDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dtk != null) dtk.Format = DateTimePickerFormat.Custom;
            //foreach (Control ctr in panel1.Controls)
            //{
            //    if (ctr is DateTimePicker)
            //    {
            //        DateTimePicker date = ctr as DateTimePicker;
            //        if (date.TabIndex != 1) date.Format = DateTimePickerFormat.Custom;
            //    }
            //}

            //foreach (TabPage tp in tabControl1.TabPages)
            //{
            //    foreach (Control ctl1 in tp.Controls)
            //    {
            //        if (ctl1.Controls.Count > 0)
            //        {
            //            foreach (Control ctl2 in ctl1.Controls) ReSetControl(ctl2);
            //        }
            //        else ReSetControl(ctl1);
            //    }
            //}

            //void ReSetControl(Control ctl)
            //{
            //    foreach (Control ctr in ctl.Controls)
            //    {
            //        if (ctr is DateTimePicker)
            //        {
            //            DateTimePicker date = ctr as DateTimePicker;
            //            if (date.TabIndex != 1) date.Format = DateTimePickerFormat.Custom;
            //        }
            //    }
            //}
        }

        private void frmitem_Load(object sender, EventArgs e)
        {
            kn = new Connect(Temp.ch);
            erp = new Connect(Temp.erp);

            timer1.Stop();

            string sm = tf ? "TF" : "SM";

            if (Temp.DeptDesc.ToUpper().Contains("ADIDAS") || Temp.DeptDesc.ToUpper().Contains("MERA")) { ads = sm + "AD"; dept = "ADS"; }
            else { ads = sm + "AA"; dept = "PUMA"; }


            txtdocno.Text = ads + DateTime.Now.ToString("yyMMddHHmmss");

            txtpass.Hide();
            btapply.Hide();

            DataTable open = kn.Doc("select * from sromstr where DocNo = '" + DataRow[0].ToString() + "'").Tables[0];

            if (open.Rows.Count > 0)
            {
                DateTime date1 = DateTime.Parse(open.Rows[0]["SysLMDate"].ToString());
                DateTime date2 = DateTime.Parse(DataRow["SysLMDate"].ToString());

                if (date1 == date2)
                {
                    kn.Ghi("update sromstr set IsOpen = '" + Temp.User + " " + DateTime.Now.ToString("dd/MM/yy HH:mm:ss") + "' where DocNo = '" + DataRow[0].ToString() + "'");
                    isopen = true;
                    timer1.Start();

                    worker.RunWorkerAsync();
                }
                else
                {
                    if (MessageBox.Show("This sample is opening in another place !!! [" + open.Rows[0]["IsOpen"].ToString() + "], do you wanna continue with READ ONLY mode ?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        isopen = false;
                        btsave.Enabled = false;
                        worker.RunWorkerAsync();
                        lbstatus.Text = "READ ONLY MODE";
                    }
                    else
                    {
                        MessageBox.Show("Your data is obsolete, it needs to refesh the data first !!!");
                        isopen = false;
                        Close();

                        fr1.worker.RunWorkerAsync();
                    }
                }
            }

            //if (tf) shiptomerdate.Value = DateTime.Now;
        }
        private void SetDate(string data, DateTimePicker day)
        {
            try
            {
                if (data != "")
                {
                    day.Value = DateTime.Parse(data);
                    day.Format = DateTimePickerFormat.Short;
                }
            }
            catch { }
        }
        private void SetTime(string data, DateTimePicker day)
        {
            try
            {
                if (data != "")
                {
                    day.Value = DateTime.Parse(data);
                    day.Format = DateTimePickerFormat.Time;
                }
            }
            catch { }
        }
        private void LoadItem()
        {
            try
            {
                ds = kn.Doc("exec SampleQueueLoading 2, '" + dept + "', '', ''");

                Invoke((Action)(() =>
                {
                    cmbrule.Items.AddRange(ds.Tables[0].Select().Select(r => r[0].ToString()).ToArray()); cmbrule.SelectedIndex = 0;

                    smptype.Items.AddRange(ds.Tables[1].Select().Select(r => r[0].ToString() + " - " + r[1].ToString()).ToArray());

                    unit.Items.AddRange(ds.Tables[2].Select().Select(r => r[0].ToString() + " - " + r[1].ToString()).ToArray());

                    division.Items.AddRange(ds.Tables[3].Select().Select(r => r[0].ToString() + " - " + r[1].ToString()).ToArray());

                    customer.Items.AddRange(ds.Tables[4].Select().Select(r => r[0].ToString() + " - " + r[1].ToString()).ToArray());

                    programcode.Items.AddRange(ds.Tables[5].Select().Select(r => r[0].ToString() + " - " + r[1].ToString()).ToArray());

                    gmttype.Items.AddRange(ds.Tables[6].Select().Select(r => r[0].ToString() + " - " + r[1].ToString()).ToArray());

                    if (ds.Tables[7].Rows.Count > 0)
                    {
                        capacityid = int.Parse(ds.Tables[7].Rows[0][0].ToString());
                        capacity = int.Parse(ds.Tables[7].Rows[0]["Capacity"].ToString());
                    }

                    //show = false;
                    if (show)
                    {
                        bturgent.Text = "Approve Urgent";

                        dataGridView2.ReadOnly = true;
                        foreach (Control ctr in panel1.Controls) { if (!(ctr is Label)) ctr.Enabled = false; }

                        #region Fill Data
                        string docno = "";
                        if (copy) docno = ads + DateTime.Now.ToString("yyMMddHHmmss"); else { docno = DataRow["DocNo"].ToString(); SetDate(DataRow["TechpackPlan"].ToString(), shiptomerdate); }
                        txtdocno.Text = docno;
                        SetDate(DataRow["IssDate"].ToString(), orderdate);
                        SetDate(DataRow["ShipDate"].ToString(), firstbulkdate);
                        smorderno.Text = DataRow["SmOrderNo"].ToString();
                        style.Text = DataRow["Style"].ToString();
                        custstyle.Text = DataRow["CustStyle"].ToString();
                        fulldesc.Text = DataRow["Description"].ToString();
                        try
                        { smptype.SelectedIndex = ds.Tables[1].Rows.IndexOf(ds.Tables[1].Select("Code = '" + DataRow["SmpType"].ToString().Split('-')[0] + "'")[0]); }
                        catch { smptype.SelectedIndex = -1; }
                        try
                        { unit.SelectedIndex = ds.Tables[2].Rows.IndexOf(ds.Tables[2].Select("Code = '" + DataRow["Unit"].ToString() + "'")[0]); }
                        catch { unit.SelectedIndex = -1; }
                        try
                        { division.SelectedIndex = ds.Tables[3].Rows.IndexOf(ds.Tables[3].Select("Code = '" + DataRow["Division"].ToString().Split('-')[0] + "'")[0]); }
                        catch { division.SelectedIndex = -1; }
                        try
                        { customer.SelectedIndex = ds.Tables[4].Rows.IndexOf(ds.Tables[4].Select("Code = '" + DataRow["Customer"].ToString() + "'")[0]); }
                        catch { customer.SelectedIndex = -1; }
                        try
                        { programcode.SelectedIndex = ds.Tables[5].Rows.IndexOf(ds.Tables[5].Select("ProgramCode = '" + DataRow["ProgramCode"].ToString().Split('-')[0] + "'")[0]); }
                        catch { programcode.SelectedIndex = -1; }
                        try
                        { gmttype.SelectedIndex = ds.Tables[6].Rows.IndexOf(ds.Tables[6].Select("Code = '" + DataRow["GmtType"].ToString() + "'")[0]); }
                        catch { gmttype.SelectedIndex = -1; }

                        txtcftsampleqty.Text = DataRow["Qty"].ToString(); txtqcsampleqty.Text = DataRow["Qty"].ToString();

                        SetDate(DataRow["BuyReady"].ToString(), buyreadydate);
                        SetDate(DataRow["RSD"].ToString(), rsddate);
                        SetDate(DataRow["CSD"].ToString(), csddate);
                        season.Text = DataRow["Season"].ToString();
                        txtsewer.Text = DataRow["OfSewers"].ToString();
                        txtsewername.Text = DataRow["SewersName"].ToString();
                        txtqc.Text = DataRow["QCInspection"].ToString();
                        //txtleadtime.Text = DataRow["Description"].ToString();
                        //txtavailable.Text = DataRow["Description"].ToString();
                        SetDate(DataRow["ActShipDate"].ToString(), shipmentdate);

                        if (DataRow["SMV"].ToString() != "")
                        {
                            if (dataGridView1.RowCount > 0) dataGridView1.Rows.RemoveAt(0);

                            smv = decimal.Parse(DataRow["SMV"].ToString());
                            dataGridView1.Rows.Add(style.Text, smv);
                        }

                        if (smv == 0) LoadSMVData();

                        bool ur = true;

                        if (!copy)
                        {
                            //SetDate(DataRow["TechpackPlan"].ToString(), shiptomerdate);
                            //SetDate(DataRow["TechpackActual"].ToString(), techpackactual);
                            SetDate(DataRow["TrimcardPlan"].ToString(), manualplan);
                            SetDate(DataRow["TrimcardActual"].ToString(), manualactual);
                            SetDate(DataRow["FabricPlan"].ToString(), fabricplan);
                            SetDate(DataRow["FabricActual"].ToString(), fabricactual);
                            SetDate(DataRow["DecorationPlan"].ToString(), decorationtrimsplan);
                            SetDate(DataRow["DecorationActual"].ToString(), decorationtrimsactual);
                            SetDate(DataRow["SewingPlan"].ToString(), sewingplanstart);
                            SetDate(DataRow["SewingActual"].ToString(), sewingplanfinish);
                            SetDate(DataRow["PlanPattern"].ToString(), patternplanplan);
                            SetDate(DataRow["PlanActual"].ToString(), patternplanactual);
                            SetDate(DataRow["BasicPattern"].ToString(), basicpatternplan);
                            SetDate(DataRow["CopyPattern"].ToString(), copypatternplan);
                            SetDate(DataRow["FinalCheck"].ToString(), finalcheckplan);
                            SetDate(DataRow["Finish"].ToString(), finishplan);
                            SetDate(DataRow["PatternReleaseActual"].ToString(), finishactual);
                            SetDate(DataRow["CuttingStart"].ToString(), cuttingstart);
                            SetDate(DataRow["CuttingFinish"].ToString(), cuttingfinish);

                            if (DataRow["LaserCut"].ToString() == "1") lazer.Checked = true;
                            SetDate(DataRow["LaserCutStart"].ToString(), lazeractual);
                            SetDate(DataRow["LaserCutFinish"].ToString(), lazerreleaseactual);
                            SetDate(DataRow["LaserReceivePlan"].ToString(), lazerplan);
                            SetDate(DataRow["LaserReleasePlan"].ToString(), lazerreleaseplan);
                            if (DataRow["Bonding"].ToString() == "1") bonding.Checked = true;
                            SetDate(DataRow["BondingStart"].ToString(), bondingactual);
                            SetDate(DataRow["BondingFinish"].ToString(), bondingreleaseactual);
                            SetDate(DataRow["BondingReceivePlan"].ToString(), bondingplan);
                            SetDate(DataRow["BondingReleasePlan"].ToString(), bondingreleaseplan);
                            if (DataRow["Embroidery"].ToString() == "1") embroidery.Checked = true;
                            SetDate(DataRow["EmbroideryStart"].ToString(), embroideryactual);
                            SetDate(DataRow["EmbroideryFinish"].ToString(), embroideryreleaseactual);
                            SetDate(DataRow["EMBReceivePlan"].ToString(), embroideryplan);
                            SetDate(DataRow["EMBReleasePlan"].ToString(), embroideryreleaseplan);
                            if (DataRow["Embossing"].ToString() == "1") embossing.Checked = true;
                            SetDate(DataRow["EmbossingStart"].ToString(), embossingactual);
                            SetDate(DataRow["EmbossingFinish"].ToString(), embossingreleaseactual);
                            SetDate(DataRow["EmbossingReceivePlan"].ToString(), embossingplan);
                            SetDate(DataRow["EmbossingReleasePlan"].ToString(), embossingreleaseplan);
                            if (DataRow["Debossing"].ToString() == "1") debossing.Checked = true;
                            SetDate(DataRow["DebossingStart"].ToString(), debossingactual);
                            SetDate(DataRow["DebossingFinish"].ToString(), debossingreleaseactual);
                            SetDate(DataRow["DebossingReceivePlan"].ToString(), debossingplan);
                            SetDate(DataRow["DebossingReleasePlan"].ToString(), debossingreleaseplan);
                            if (DataRow["HeatTransfer"].ToString() == "1") heat.Checked = true;
                            SetDate(DataRow["HeatTransferStart"].ToString(), heatactual);
                            SetDate(DataRow["HeatTransferFinish"].ToString(), heatreleaseactual);
                            SetDate(DataRow["HeatReceivePlan"].ToString(), heatplan);
                            SetDate(DataRow["HeatReleasePlan"].ToString(), heatreleaseplan);
                            if (DataRow["Printing"].ToString() == "1") printing.Checked = true;
                            SetDate(DataRow["PrintingStart"].ToString(), printingactual);
                            SetDate(DataRow["PrintingFinish"].ToString(), printingreleaseactual);
                            SetDate(DataRow["PrintingReceivePlan"].ToString(), printingplan);
                            SetDate(DataRow["PrintingReleasePlan"].ToString(), printingreleaseplan);
                            if (DataRow["PadPrint"].ToString() == "1") padprint.Checked = true;
                            SetDate(DataRow["PadPrintStart"].ToString(), padprintactual);
                            SetDate(DataRow["PadPrintFinish"].ToString(), padprintreleaseactual);
                            SetDate(DataRow["PadprintReceivePlan"].ToString(), padprintplan);
                            SetDate(DataRow["PadprintReleasePlan"].ToString(), padprintreleaseplan);
                            if (DataRow["SubPrint"].ToString() == "1") subprint.Checked = true;
                            SetDate(DataRow["SubPrintStart"].ToString(), subprintactual);
                            SetDate(DataRow["SubPrintFinish"].ToString(), subprintreleaseactual);
                            SetDate(DataRow["SubprintReceivePlan"].ToString(), subprintplan);
                            SetDate(DataRow["SubprintReleasePlan"].ToString(), subprintreleaseplan);

                            //SetDate(DataRow["SewingPlan"].ToString(), sewingplanstart);
                            //SetDate(DataRow["SewingActual"].ToString(), sewingplanfinish);
                            SetDate(DataRow["SewingActualStart"].ToString(), sewingactualstart);
                            SetDate(DataRow["SewingActualFinish"].ToString(), sewingactualfinish);
                            txtremark.Text = DataRow["Remark"].ToString();

                            try
                            {
                                SetDate(DataRow["MarkerPlan"].ToString(), markerplan);
                                SetDate(DataRow["MarkerActual"].ToString(), markeractual);
                                SetDate(DataRow["MarkerReleasePlan"].ToString(), markerreleaseplan);
                                SetDate(DataRow["MarkerReleaseActual"].ToString(), markerreleaseactual);
                                SetDate(DataRow["QCPlan"].ToString(), qcplan);
                                SetDate(DataRow["QCActual"].ToString(), qcactual);
                                SetDate(DataRow["CFTPlan"].ToString(), cftplan);
                                SetDate(DataRow["CFTActual"].ToString(), cftactual);
                                SetTime(DataRow["QCPlan"].ToString(), qcreceivetime);
                                SetTime(DataRow["QCActual"].ToString(), qcreleasetime);
                                SetTime(DataRow["CFTPlan"].ToString(), cftreceivetime);
                                SetTime(DataRow["CFTActual"].ToString(), cftreleasetime);

                                printingcount.Value = decimal.Parse(DataRow["PrintingCount"].ToString());
                                embroiderycount.Value = decimal.Parse(DataRow["EMBCount"].ToString());
                                heatcount.Value = decimal.Parse(DataRow["HeatCount"].ToString());
                                debossingcount.Value = decimal.Parse(DataRow["DebossingCount"].ToString());
                                embossingcount.Value = decimal.Parse(DataRow["EmbossingCount"].ToString());
                                bondingcount.Value = decimal.Parse(DataRow["BondingCount"].ToString());
                                lazercount.Value = decimal.Parse(DataRow["LaserCount"].ToString());
                                padprintcount.Value = decimal.Parse(DataRow["PadprintCount"].ToString());
                                subprintcount.Value = decimal.Parse(DataRow["SubprintCount"].ToString());
                            }
                            catch { }

                            LoadInspection();

                            urgent = string.IsNullOrEmpty(DataRow["Urgent"].ToString()) ? 0 : int.Parse(DataRow["Urgent"].ToString());
                            if (urgent == 1)
                            {
                                if (Temp.Profile == "MANAGER") ur = true;
                                else ur = false;
                            }
                            else ur = false;
                        }

                        LoadSize(DataRow["SmOrderNo"].ToString(), docno);

                        #endregion

                        if (Temp.Dept.Contains("MER"))
                        {
                            //foreach (Control ctr in panel1.Controls) { if (!(ctr is Label)) ctr.Enabled = true; }
                            //Lock(false);

                            if (DataRow["Status"].ToString() == "New" || DataRow["Status"].ToString() == "Incomplete")
                            {
                                dataGridView2.ReadOnly = false;

                                foreach (Control ctr in panel1.Controls) { if (!(ctr is Label)) ctr.Enabled = true; }

                                Lock(false);
                            }
                            else
                            {
                                string ch = "select * from sromstrunlock where DocNo = '" + txtdocno.Text + "' and UnlockStatus = 0";
                                DataTable dt = kn.Doc(ch).Tables[0];

                                if (dt.Rows.Count > 0)
                                {
                                    unlock = true;

                                    dataGridView2.ReadOnly = false;

                                    foreach (Control ctr in panel1.Controls) { if (!(ctr is Label)) ctr.Enabled = true; }
                                }

                                Lock(true);
                            }

                            csddate.Enabled = true;
                            bturgent.Enabled = true;
                        }

                        if (Temp.Dept.Contains("SMP"))
                        {
                            txtsewer.Enabled = true; txtsewername.Enabled = true; txtqc.Enabled = true; csddate.Enabled = true;

                            qcplan.Enabled = false;
                            qcactual.Enabled = false;
                            cftplan.Enabled = false;
                            cftactual.Enabled = false;
                        }

                        if (Temp.Dept.Contains("CFT"))
                        {
                            Lock(true);

                            qcplan.Enabled = true;
                            qcactual.Enabled = true;
                            cftplan.Enabled = true;
                            cftactual.Enabled = true;
                            qcreceivetime.Enabled = true;
                            qcreleasetime.Enabled = true;
                            cftreceivetime.Enabled = true;
                            cftreleasetime.Enabled = true;
                        }

                        if (Temp.Dept.Contains("QC"))
                        {
                            Lock(true);

                            qcplan.Enabled = true;
                            qcactual.Enabled = true;
                            cftplan.Enabled = true;
                            cftactual.Enabled = true;
                        }

                        if (Temp.Dept.Contains("EMB"))
                        {
                            Lock(false);
                        }

                        btfindsampleno.Enabled = false;

                        btmeraddcomment.Enabled = true; shiptomerdate.Enabled = true;

                        bturgent.Enabled = ur;

                        LoadComments(txtdocno.Text);

                        lbstatus.Text = DataRow["Status"].ToString();

                        switch (lbstatus.Text)
                        {
                            case "New":
                                lbstatus.BackColor = AppConfig.New;
                                break;
                            case "Incomplete":
                                lbstatus.BackColor = AppConfig.Incomplete;
                                break;
                            case "In Queue":
                                lbstatus.BackColor = AppConfig.InQueue;
                                break;
                            case "In Production":
                                lbstatus.BackColor = AppConfig.InDecoration;
                                break;
                            case "In Decoration":
                                lbstatus.BackColor = AppConfig.InDecoration;
                                break;
                            case "In Sewing":
                                lbstatus.BackColor = AppConfig.InSewing;
                                break;
                            case "CFT Passed":
                                lbstatus.BackColor = AppConfig.CFTPassed;
                                break;
                            case "QC Passed":
                                lbstatus.BackColor = AppConfig.CFTPassed;
                                break;
                            case "Finish":
                                lbstatus.BackColor = AppConfig.FinishOnTime;
                                break;
                            case "Finish On Time":
                                lbstatus.BackColor = AppConfig.FinishOnTime;
                                break;
                            case "Finish in Delay":
                                lbstatus.BackColor = AppConfig.FinishDelay;
                                break;
                        }
                    }
                    else
                    {
                        Lock(true);
                        printing.Enabled = true; embossing.Enabled = true; embroidery.Enabled = true; bonding.Enabled = true; heat.Enabled = true;
                        subprint.Enabled = true; padprint.Enabled = true; lazer.Enabled = true; debossing.Enabled = true; bturgent.Enabled = true;

                        lbstatus.BackColor = AppConfig.New;
                    }

                    if (Temp.Dept.Contains("MER"))
                    {
                        manualplan.Enabled = true;
                        fabricplan.Enabled = true;
                        decorationtrimsplan.Enabled = true;
                    }

                    btcheck.Enabled = true;
                    btsampleidlist.Enabled = true;

                    if (tf) shiptomerdate.Value = DateTime.Now;

                    void Lock(bool deco)
                    {
                        manualactual.Enabled = false;
                        decorationtrimsactual.Enabled = false;
                        fabricactual.Enabled = false;
                        //patternplanactual.Enabled = false;
                        //finishactual.Enabled = false;
                        markeractual.Enabled = false;
                        markerreleaseactual.Enabled = false;
                        cuttingstart.Enabled = false;
                        cuttingfinish.Enabled = false;
                        sewingactualstart.Enabled = false;
                        sewingactualfinish.Enabled = false;
                        sewingplanstart.Enabled = false;
                        sewingplanfinish.Enabled = false;
                        qcplan.Enabled = false;
                        qcactual.Enabled = false;
                        cftplan.Enabled = false;
                        cftactual.Enabled = false;
                        qcreceivetime.Enabled = false;
                        qcreleasetime.Enabled = false;
                        cftreceivetime.Enabled = false;
                        cftreleasetime.Enabled = false;
                        btunlock.Enabled = false;

                        if (deco) foreach (Control ctr in decorationtab.Controls) { if (!(ctr is GroupBox)) ctr.Enabled = false; }
                    }
                }));
            }
            catch { }
        }

        private void LoadInspection()
        {
            try
            {
                DataSet critical = kn.Doc("exec SampleQueueLoading 10,'" + txtdocno.Text + "','" + dept + "-CFT','" + dept + "-ENDLINE'");

                if (critical.Tables[0].Rows.Count > 0)
                {
                    DataRow r = critical.Tables[0].Rows[0];

                    txtcftinsqty.Text = r["InsQty"].ToString();
                    txtcftpass.Text = r["PassQty"].ToString();
                    txtcftfail.Text = r["FailQty"].ToString();
                    txtcftrft.Text = r["RFT"].ToString();
                }

                if (critical.Tables[1].Rows.Count > 0) dtgcftstatus.DataSource = critical.Tables[1];

                if (critical.Tables[2].Rows.Count > 0)
                {
                    DataRow r = critical.Tables[2].Rows[0];

                    txtqcsampleqty.Text = r["InsQty"].ToString();
                    txtqcpass.Text = r["PassQty"].ToString();
                    txtqcfail.Text = r["FailQty"].ToString();
                    txtrft.Text = r["RFT"].ToString();
                    txtqcsewer.Text = kn.Doc("select SewersName from sromstr where DocNo = '" + txtdocno.Text + "'").Tables[0].Rows[0][0].ToString();
                }

                if (critical.Tables[3].Rows.Count > 0) dtgqcdefectlist.DataSource = critical.Tables[3];
            }
            catch { }
        }
        private void LoadReason()
        {
            DataSet d = kn.Doc("exec SampleQueueLoading 42, '" + dept + "', '" + txtdocno.Text + "', ''");

            if (d != null)
            {
                cmbreason.Items.Clear();
                cmbreason.Items.AddRange(d.Tables[0].Select().Select(s => s[0].ToString()).ToArray());
                cmbreason.Items.Add("Other");

                dtgreason.DataSource = d.Tables[1];
            }
        }
        private void btfindsampleno_Click(object sender, EventArgs e)
        {
            frmfindstyle frm = new frmfindstyle(this);
            frm.ShowDialog();
        }
        public void LoadComments(string docno)
        {
            try
            {
                tb.Clear();
                DataSet ds = kn.Doc("exec GetLoadData 60,'" + docno + "','" + dept + "'");
                DataTable comm = ds.Tables[0];

                if (dtgmercomment.Rows.Count > 0) for (int i = dtgmercomment.Rows.Count - 1; i >= 0; i--) dtgmercomment.Rows.RemoveAt(i);
                if (dtgpatterncomment.Rows.Count > 0) for (int i = dtgpatterncomment.Rows.Count - 1; i >= 0; i--) dtgpatterncomment.Rows.RemoveAt(i);
                if (dtgdecorationcomment.Rows.Count > 0) for (int i = dtgdecorationcomment.Rows.Count - 1; i >= 0; i--) dtgdecorationcomment.Rows.RemoveAt(i);
                if (dtgmarkercomment.Rows.Count > 0) for (int i = dtgmarkercomment.Rows.Count - 1; i >= 0; i--) dtgmarkercomment.Rows.RemoveAt(i);
                if (dtgcuttingcomment.Rows.Count > 0) for (int i = dtgcuttingcomment.Rows.Count - 1; i >= 0; i--) dtgcuttingcomment.Rows.RemoveAt(i);
                if (dtgsewingcomment.Rows.Count > 0) for (int i = dtgsewingcomment.Rows.Count - 1; i >= 0; i--) dtgsewingcomment.Rows.RemoveAt(i);
                if (dtgqccomment.Rows.Count > 0) for (int i = dtgqccomment.Rows.Count - 1; i >= 0; i--) dtgqccomment.Rows.RemoveAt(i);
                if (dtgcftcomment.Rows.Count > 0) for (int i = dtgcftcomment.Rows.Count - 1; i >= 0; i--) dtgcftcomment.Rows.RemoveAt(i);

                foreach (DataRow r in comm.Rows)
                {
                    switch (r["DeptCode"].ToString())
                    {
                        case "MER":
                            LoadDtg(r, dtgmercomment, 0);
                            break;
                        case "SMP":
                            LoadDtg(r, dtgpatterncomment, 1);
                            break;
                        case "PTT":
                            LoadDtg(r, dtgpatterncomment, 1);
                            break;
                        case "MK":
                            LoadDtg(r, dtgmarkercomment, 2);
                            break;
                        case "CUT":
                            LoadDtg(r, dtgcuttingcomment, 3);
                            break;
                        case "EMB":
                            LoadDtg(r, dtgdecorationcomment, 4);
                            break;
                        case "SEW":
                            LoadDtg(r, dtgsewingcomment, 5);
                            break;
                        case "ENDLINE":
                            LoadDtg(r, dtgqccomment, 6);
                            break;
                        case "CFT":
                            LoadDtg(r, dtgcftcomment, 7);
                            break;
                    }
                }

                void LoadDtg(DataRow r, DataGridView dtg, int tab)
                {
                    dtg.Rows.Add("Read", r["CmtContent"].ToString(), r["CmtID"].ToString());

                    //List<string> userread = new List<string>();

                    //foreach (DataRow r2 in ds.Tables[1].Rows)
                    //{
                    //    if (r2["CmtID"].ToString() == r["CmtID"].ToString())
                    //        userread.Add(r2[2].ToString());
                    //}

                    if (r["Readed"].ToString() == "Yes")//if (userread.Contains(Temp.User))
                    {
                        DataGridViewLinkCell cl = dtg.Rows[dtg.Rows.Count - 1].Cells[0] as DataGridViewLinkCell;
                        cl.LinkColor = Color.Black;
                    }
                    else
                    {
                        //grp.DrawString(tab.Text.Trim() + "(1)",
                        //    new Font(tabControl1.Font, FontStyle.Bold),
                        //    Brushes.Blue,
                        //    new PointF(tab.Bounds.X + 3, tab.Bounds.Y + 3));

                        tb.Add(tab);

                        tabControl1.SelectedIndex = tab;

                        dtg.Rows[dtg.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Blue;
                    }
                }
            }
            catch { }
        }
        public void FillData(DataRow data)
        {
            try
            {
                List<string> ls = data.Table.Columns.OfType<DataColumn>().Select(c => c.ColumnName).ToList();
                foreach (Control ctr in panel1.Controls)
                {
                    if (ls.Contains(ctr.Name) && !(ctr is ComboBox)) ctr.Text = data[ctr.Name].ToString();
                }

                try { smptype.SelectedIndex = ds.Tables[1].Rows.IndexOf(ds.Tables[1].Select("Code = '" + data["smptype"].ToString() + "'")[0]); } catch { smptype.SelectedIndex = -1; }
                try { unit.SelectedIndex = ds.Tables[2].Rows.IndexOf(ds.Tables[2].Select("Code = '" + data["unit"].ToString() + "'")[0]); } catch { unit.SelectedIndex = -1; }
                try { division.SelectedIndex = ds.Tables[3].Rows.IndexOf(ds.Tables[3].Select("Code = '" + data["division"].ToString() + "'")[0]); } catch { division.SelectedIndex = -1; }
                try { customer.SelectedIndex = ds.Tables[4].Rows.IndexOf(ds.Tables[4].Select("Code = '" + data["customer"].ToString() + "'")[0]); } catch { customer.SelectedIndex = -1; }
                try { programcode.SelectedIndex = ds.Tables[5].Rows.IndexOf(ds.Tables[5].Select("ProgramCode = '" + data["programcode"].ToString() + "'")[0]); } catch { programcode.SelectedIndex = -1; }
                try { gmttype.SelectedIndex = ds.Tables[6].Rows.IndexOf(ds.Tables[6].Select("Code = '" + data["gmttype"].ToString() + "'")[0]); } catch { gmttype.SelectedIndex = -1; }

                //LoadSMVData();

                versioncount = data["versioncount"].ToString();

                LoadSize(data["smorderno"].ToString(), "");
            }
            catch { }
        }
        private void LoadSMVData()
        {
            if (dataGridView1.RowCount > 0) dataGridView1.Rows.RemoveAt(0);

            if (smptype.Text.Contains("Sealing") || smptype.Text.Contains("Size Set"))
            {
                DataTable dtsmv = kn.Doc("select top 1  StyleNo,cast(DefSAM as float) DefSAM from sygseason where StyleNo = '" + style.Text + "' and DefSAM is not null order by cast(DefSAM as float) desc").Tables[0];

                if (dtsmv.Rows.Count > 0)
                {
                    dataGridView1.Rows.Add(style.Text, dtsmv.Rows[0][1].ToString());

                    smv = decimal.Parse(dtsmv.Rows[0][1].ToString());
                }
                else
                {
                    DataTable dtsmv2 = kn.Doc("select * from sromstrassumesmv where Dept = '" + dept + "' and StyleNo = '" + style.Text + "'").Tables[0];

                    if (dtsmv2.Rows.Count > 0)
                    {
                        dataGridView1.Rows.Add(style.Text, dtsmv2.Rows[0][3].ToString());

                        smv = decimal.Parse(dtsmv2.Rows[0][3].ToString());
                    }
                    //else Run();
                }
            }
            else Run();

            DataTable exception = kn.Doc("exec SampleQueueLoading 22, '" + style.Text + "', '', ''").Tables[0];
            dtgexception.DataSource = exception;

            void Run()
            {
                if (gmttype.Text != "")
                {
                    string gmt = gmttype.Text.Split('-')[0].Trim().ToUpper();

                    DataTable dtgmt = kn.Doc("select SMV from sromstrsmv where Code = '" + gmt + "'").Tables[0];

                    if (dtgmt.Rows.Count > 0)
                    {
                        string v = dtgmt.Rows[0][0].ToString();

                        smv = decimal.Parse(v == "" ? "0" : v);

                        dataGridView1.Rows.Add(style.Text, v);
                    }
                }
            }
        }
        public void LoadSize(string orderno, string docno)
        {
            try
            {
                tb.Clear();
                if (orderno != "")
                {
                    string ch = "exec SampleQueueLoading 24, '" + orderno + "', '" + docno + "', ''";

                    DataSet ds1 = kn.Doc(ch);
                    DataTable size = ds1.Tables[0];
                    DataTable color = ds1.Tables[1];

                    stage.Clear();
                    foreach (DataRow r in ds1.Tables[2].Rows)
                    {
                        //dtgcftstatus.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString());
                        stage.Add(r[0].ToString());
                    }

                    DataTable dt = new DataTable();
                    DataTable sizeqty = new DataTable();

                    if (docno != "") sizeqty = kn.Doc("select * from sroasm where DocNo = '" + docno + "'").Tables[0];

                    dt.Columns.Add("Color/Size", typeof(string));

                    foreach (DataRow r in size.Rows) dt.Columns.Add(r["sizx"].ToString(), typeof(string));

                    foreach (DataRow r in color.Rows)
                    {
                        DataRow rr = dt.NewRow();
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (i == 0) rr[0] = r["color"].ToString();
                            else
                            {
                                if (sizeqty.Rows.Count > 0)
                                {
                                    try
                                    {
                                        rr[i] = sizeqty.Select("Color = '" + r["color"].ToString() + "' and Sizx = '" + dt.Columns[i].ColumnName + "'").Select(d => d["Qty"].ToString()).ToArray()[0];
                                    }
                                    catch { rr[i] = ""; }
                                }
                                else rr[i] = "";
                            }
                        }

                        dt.Rows.Add(rr); //MessageBox.Show(dt.Rows.Count.ToString());
                    }

                    dataGridView2.Invoke((Action)(() =>
                    {
                        dataGridView2.DataSource = null;
                        dataGridView2.DataSource = dt;

                        CaculateSMV();
                    }));
                }
            }
            catch { }
        }
        private void CaculateSMV()
        {
            ls.Clear();
            qty = 0;
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int j = 1; j < dataGridView2.ColumnCount; j++)
                {
                    string vl = dataGridView2.Rows[i].Cells[j].Value.ToString();
                    //MessageBox.Show(dataGridView2.Columns[j].HeaderText + ":" + vl);
                    try
                    {
                        if (vl != "")
                        {
                            qty += int.Parse(vl);

                            ls.Add(new SizeQty
                            {
                                ColorID = (i + 1).ToString(),
                                SizeID = j.ToString(),
                                Color = dataGridView2.Rows[i].Cells[0].Value.ToString(),
                                Size = dataGridView2.Columns[j].HeaderText,
                                Qty = vl
                            });
                        }
                    }
                    catch { }
                }
            }

            lbsmv.Text = "Capacity : " + (smv * qty);
        }
        private void dataGridView2_Paint(object sender, PaintEventArgs e)
        {
            if (dataGridView2.ColumnCount > 0)
            {
                dataGridView2.Columns[0].ReadOnly = true;
                dataGridView2.Columns[0].DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            }
        }

        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)) { }
            else
            {
                e.Handled = true;
            }
        }

        private void btcancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //e.Graphics.Clear(TransparencyKey);
            if (e.Index == tabControl1.SelectedIndex)
            {
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                    new Font(tabControl1.Font, FontStyle.Bold),
                    Brushes.Black,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
            else if (tb.Contains(e.Index))
            {
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text.Trim() + "(*)",
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

        private void dtgmercomment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dtg = sender as DataGridView;

                if (e.ColumnIndex == 0)
                {
                    dtg.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;

                    bool end = true;
                    foreach (DataGridViewRow r in dtg.Rows)
                    {
                        if (r.DefaultCellStyle.ForeColor == Color.Blue)
                        {
                            end = false; break;
                        }
                    }
                    if (end) tb.Remove(tabControl1.SelectedIndex);

                    string ct = dtg.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string id = dtg.Rows[e.RowIndex].Cells[2].Value.ToString();

                    string read = Temp.User + "/" + Temp.Dept;
                    kn.Ghi("exec GetLoadData 61,'" + id + "','" + read + "'");

                    frmshowcomment frm = new frmshowcomment(id, dept, txtdocno.Text, this);

                    frm.ShowDialog();
                }
            }
            catch { }
        }

        private void chcklazercut_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DateTimePicker d = new DateTimePicker();

                if (csddate.Format == DateTimePickerFormat.Custom) d = rsddate;
                else d = csddate;

                DateTime rsd = DateTime.Now.Hour >= 14 ? d.Value.AddDays(1) : d.Value;
                CheckBox chck = sender as CheckBox;

                foreach (Control ctr in decorationtab.Controls)
                {
                    if (ctr is DateTimePicker)
                    {
                        DateTimePicker date = ctr as DateTimePicker;

                        if (chck.Checked)
                        {
                            if (date.Name.Contains(chck.Name) && date.Name.Contains("actual") && !Temp.Dept.Contains("MER")) date.Enabled = true;
                        }
                        else
                        {
                            if (date.Name.Contains(chck.Name))
                            {
                                date.Enabled = false;
                                date.Format = DateTimePickerFormat.Custom;
                            }
                        }
                    }

                    if (ctr is NumericUpDown)
                    {
                        NumericUpDown num = ctr as NumericUpDown;

                        if (num.Name.Contains(chck.Name))
                        {
                            if (chck.Checked) num.Enabled = true;
                            else
                            {
                                num.Enabled = false;
                                num.Value = 0;
                            }
                        }
                    }
                }

                SetPlanDecoration(rsd, d);
            }
            catch { }
        }

        private void style_TextChanged(object sender, EventArgs e)
        {

        }

        private void btsave_Click(object sender, EventArgs e)
        {
            if (smorderno.Text != "")
            {
                if (tf) SaveCmd();
                else
                {
                    if (smv == 0)
                    {
                        MessageBox.Show("You are booking without the SMV, Please double check it again or contact with GSD department !!!");
                        dataGridView1.Focus();
                    }
                    else SaveCmd();
                }
            }
        }
        private void SaveCmd()
        {
            if (tf) Run();
            else
            {
                DataTable open = kn.Doc("select * from sromstr where DocNo = '" + DataRow[0].ToString() + "'").Tables[0];

                if (open.Rows.Count > 0)
                {
                    DateTime date1 = DateTime.Parse(open.Rows[0]["SysLMDate"].ToString());
                    DateTime date2 = DateTime.Parse(DataRow["SysLMDate"].ToString());

                    if (date1 == date2)
                    {
                        StartRun();
                    }
                    else
                    {
                        MessageBox.Show("Your data is obsolete, it needs to refesh the data first and try it again !!!");
                        isopen = false;
                        Close();

                        fr1.worker.RunWorkerAsync();
                    }
                }
                else StartRun();
            }

            void StartRun()
            {
                try
                {
                    DateTimePicker dk = new DateTimePicker();
                    if (csddate.Format == DateTimePickerFormat.Custom) dk = rsddate;
                    else dk = csddate;

                    bool rs = CheckLimit(dk);

                    if (rsddate.Format == DateTimePickerFormat.Custom && qty != 0)
                    {
                        MessageBox.Show("You have to input the RSD or the RSD is not loaded, pls check it again before you save the status !!!");
                        rsddate.Focus();
                    }
                    else if (unit.Text == "")// && Temp.Dept == "MER")
                    {
                        MessageBox.Show("You have to input the Unit !!!");
                        unit.Focus();
                    }
                    else if (smptype.Text == "")// && Temp.Dept == "MER")
                    {
                        MessageBox.Show("You have to input the Sample Type !!!");
                        smptype.Focus();
                    }
                    else if (gmttype.Text == "")// && Temp.Dept == "MER")
                    {
                        MessageBox.Show("You have to input the Garment Type !!!");
                        gmttype.Focus();
                    }
                    else if (qty == 0 && rsddate.Format != DateTimePickerFormat.Custom)// && Temp.Dept == "MER")
                    {
                        MessageBox.Show("You have to key any qty !!!");
                        dataGridView2.Focus();
                    }
                    else if (rs && Temp.Dept == "MER" && urgent == 0 && dept == "PUMA")
                    {
                        MessageBox.Show("Limit SMV <RSD/CSD : " + dk.Value.ToString("dd/MM/yyyy") + " - Booked SMV/Total SMV : " + bookedqty + "/" + capacity + "> : " + (qty * smv) + ", you have to change the RSD to another date !!!", "OVER CAPACITY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        rsddate.Format = DateTimePickerFormat.Custom;
                    }
                    else
                    {
                        Run();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            }
            void Run()
            {
                bool doi = false;
                string chen = "insert into sromstr values (";

                if (!show)
                {
                    DataRow["VersionCount"] = versioncount;
                    DataRow["IssDate"] = DateTime.Now.ToShortDateString();
                    DataRow["ShipDate"] = DBNull.Value;
                    DataRow["Status"] = "N";
                    DataRow["Locked"] = 0;
                    DataRow["SysOwner"] = Temp.User;
                    DataRow["SysCreateDate"] = DateTime.Now.ToString();
                    DataRow["LaserCutMost"] = 0;
                    DataRow["BondingMost"] = 0;
                    DataRow["EmbroideryMost"] = 0;
                    DataRow["EmbossingMost"] = 0;
                    DataRow["DebossingMost"] = 0;
                    DataRow["HeatTransferMost"] = 0;
                    DataRow["PrintingMost"] = 0;
                    DataRow["PadPrintMost"] = 0;
                    DataRow["SubPrintMost"] = 0;
                }

                if (shipmentdate.Format != DateTimePickerFormat.Custom) DataRow["Status"] = "S";
                else if (shiptomerdate.Format != DateTimePickerFormat.Custom)
                {
                    if (tf) DataRow["Status"] = "T";
                    else
                    {
                        DateTime rsd = csddate.Format == DateTimePickerFormat.Custom ? rsddate.Value : csddate.Value;
                        TimeSpan sp = rsd - shiptomerdate.Value;
                        if (sp.TotalDays >= 0) DataRow["Status"] = "F1";
                        else DataRow["Status"] = "F2";
                    }

                    //MessageBox.Show(sp.TotalDays + "  " + DataRow["Status"]);
                }
                else if (cftactual.Format != DateTimePickerFormat.Custom) DataRow["Status"] = "C";
                else if (qcactual.Format != DateTimePickerFormat.Custom) DataRow["Status"] = "E";
                else if (sewingactualstart.Format != DateTimePickerFormat.Custom) DataRow["Status"] = "P";
                else
                {
                    bool deco = false;
                    foreach (Control ctr in decorationtab.Controls)
                    {
                        if (ctr is DateTimePicker)
                        {
                            if (ctr.Name.Contains("actual") && ((DateTimePicker)ctr).Format != DateTimePickerFormat.Custom) deco = true;
                        }
                    }

                    if (deco) DataRow["Status"] = "D";
                    else if (manualactual.Format != DateTimePickerFormat.Custom && fabricactual.Format != DateTimePickerFormat.Custom && decorationtrimsactual.Format != DateTimePickerFormat.Custom) DataRow["Status"] = "Q";
                    else if (manualactual.Format == DateTimePickerFormat.Custom || fabricactual.Format == DateTimePickerFormat.Custom || decorationtrimsactual.Format == DateTimePickerFormat.Custom) DataRow["Status"] = "I";
                    else DataRow["Status"] = "N";
                }

                if (!string.IsNullOrEmpty(DataRow["RSD"].ToString()) && unlock)
                {
                    try
                    {
                        DateTime d = DateTime.Parse(DataRow["RSD"].ToString());
                        TimeSpan sp = rsddate.Value - d;

                        if (sp.TotalDays != 0) doi = true;
                    }
                    catch { }
                }

                DataRow["DocNo"] = txtdocno.Text; chen += "'" + txtdocno.Text + "',"; chen += "" + DataRow["VersionCount"].ToString().Replace(',', '.') + ","; chen += "" + GetDate1(orderdate) + ",";
                DataRow["RuleCode"] = cmbrule.Text; chen += "'" + cmbrule.Text + "',";
                DataRow["SmOrderNo"] = smorderno.Text; chen += "'" + smorderno.Text + "',";
                DataRow["Division"] = GetData(division.Text); chen += "'" + GetData(division.Text) + "',";
                DataRow["Season"] = season.Text; chen += "'" + season.Text + "',";
                DataRow["Customer"] = GetData(customer.Text); chen += "'" + GetData(customer.Text) + "',";
                DataRow["Style"] = style.Text; chen += "'" + style.Text + "',";
                DataRow["CustStyle"] = custstyle.Text; chen += "'" + custstyle.Text + "',";
                DataRow["Description"] = fulldesc.Text; chen += "'" + fulldesc.Text + "',";
                DataRow["ProgramCode"] = GetData(programcode.Text); chen += "'" + GetData(programcode.Text) + "',";
                DataRow["Unit"] = GetData(unit.Text); chen += "'" + GetData(unit.Text) + "',";
                DataRow["SmpType"] = GetData(smptype.Text); chen += "'" + GetData(smptype.Text) + "',";
                DataRow["GmtType"] = GetData(gmttype.Text); chen += "'" + GetData(gmttype.Text) + "',";
                DataRow["Qty"] = qty; chen += "" + qty + ",";
                DataRow["BuyReady"] = GetDate(buyreadydate); chen += "" + GetDate1(buyreadydate) + ",NULL,";
                DataRow["ActShipDate"] = GetDate(shipmentdate); chen += "" + GetDate1(shipmentdate) + ",";
                DataRow["RSD"] = GetDate(rsddate); chen += "" + GetDate1(rsddate) + ",";
                DataRow["CSD"] = GetDate(csddate); chen += "" + GetDate1(csddate) + ",";
                DataRow["TechpackPlan"] = GetDate(shiptomerdate); chen += "" + GetDate1(shiptomerdate) + ",";
                /*DataRow["TechpackActual"] = GetDate(techpackactual);*/
                chen += "" + GetDate1(techpackactual) + ",";
                DataRow["TrimcardPlan"] = GetDate(manualplan); chen += "" + GetDate1(manualplan) + ",";
                DataRow["TrimcardActual"] = GetDate(manualactual); chen += "" + GetDate1(manualactual) + ",";
                DataRow["FabricPlan"] = GetDate(fabricplan); chen += "" + GetDate1(fabricplan) + ",";
                DataRow["FabricActual"] = GetDate(fabricactual); chen += "" + GetDate1(fabricactual) + ",";
                DataRow["DecorationPlan"] = GetDate(decorationtrimsplan); chen += "" + GetDate1(decorationtrimsplan) + ",";
                DataRow["DecorationActual"] = GetDate(decorationtrimsactual); chen += "" + GetDate1(decorationtrimsactual) + ",";
                DataRow["SewingPlan"] = GetDate(sewingtrimsplan); chen += "" + GetDate1(sewingtrimsplan) + ",";
                DataRow["SewingActual"] = GetDate(sewingtrimsactual); chen += "" + GetDate1(sewingtrimsactual) + ",";
                DataRow["PlanPattern"] = GetDate(patternplanplan); chen += "" + GetDate1(patternplanplan) + ",";
                DataRow["BasicPattern"] = GetDate(basicpatternplan); chen += "" + GetDate1(basicpatternplan) + ",";
                DataRow["CopyPattern"] = GetDate(copypatternplan); chen += "" + GetDate1(copypatternplan) + ",";
                DataRow["FinalCheck"] = GetDate(finalcheckplan); chen += "" + GetDate1(finalcheckplan) + ",";
                DataRow["Finish"] = GetDate(finishplan); chen += "" + GetDate1(finishplan) + ",";
                DataRow["PlanActual"] = GetDate(patternplanactual); chen += "" + GetDate1(patternplanactual) + ",";
                DataRow["OfSewers"] = txtsewer.Text == "" ? 0 : decimal.Parse(txtsewer.Text); chen += "" + (txtsewer.Text == "" ? 0 : decimal.Parse(txtsewer.Text)) + ",";
                DataRow["SewersName"] = txtsewername.Text; chen += "N'" + txtsewername.Text + "',";
                DataRow["QCInspection"] = txtqc.Text == "" ? 0 : decimal.Parse(txtqc.Text); chen += "" + (txtqc.Text == "" ? 0 : decimal.Parse(txtqc.Text)) + ",";
                DataRow["CuttingStart"] = GetDate(cuttingstart); chen += "" + GetDate1(cuttingstart) + ",";
                DataRow["CuttingFinish"] = GetDate(cuttingfinish); chen += "" + GetDate1(cuttingfinish) + ",";
                DataRow["SewingActualStart"] = GetDate(sewingactualstart);
                DataRow["SewingActualFinish"] = GetDate(sewingactualfinish);
                DataRow["Remark"] = txtremark.Text;
                DataRow["SysLMUser"] = Temp.User;
                DataRow["SysLMDate"] = DateTime.Now.ToString();

                if (lazer.Checked)
                {
                    DataRow["LaserCut"] = 1; chen += "1,";
                    DataRow["LaserCutStart"] = GetDate(lazeractual); chen += "" + GetDate1(lazeractual) + ",";
                    DataRow["LaserCutFinish"] = GetDate(lazerreleaseactual); chen += "" + GetDate1(lazerreleaseactual) + ",";
                }
                else
                {
                    DataRow["LaserCut"] = 0; chen += "0,";
                    DataRow["LaserCutStart"] = DBNull.Value; chen += "NULL,";
                    DataRow["LaserCutFinish"] = DBNull.Value; chen += "NULL,";
                }
                if (bonding.Checked)
                {
                    DataRow["Bonding"] = 1; chen += "1,";
                    DataRow["BondingStart"] = GetDate(bondingactual); chen += "" + GetDate1(bondingactual) + ",";
                    DataRow["BondingFinish"] = GetDate(bondingreleaseactual); chen += "" + GetDate1(bondingreleaseactual) + ",";
                }
                else
                {
                    DataRow["Bonding"] = 0; chen += "0,";
                    DataRow["BondingStart"] = DBNull.Value; chen += "NULL,";
                    DataRow["BondingFinish"] = DBNull.Value; chen += "NULL,";
                }
                if (embroidery.Checked)
                {
                    DataRow["Embroidery"] = 1; chen += "1,";
                    DataRow["EmbroideryStart"] = GetDate(embroideryactual); chen += "" + GetDate1(embroideryactual) + ",";
                    DataRow["EmbroideryFinish"] = GetDate(embroideryreleaseactual); chen += "" + GetDate1(embroideryreleaseactual) + ",";
                }
                else
                {
                    DataRow["Embroidery"] = 0; chen += "0,";
                    DataRow["EmbroideryStart"] = DBNull.Value; chen += "NULL,";
                    DataRow["EmbroideryFinish"] = DBNull.Value; chen += "NULL,";
                }
                if (embossing.Checked)
                {
                    DataRow["Embossing"] = 1; chen += "1,";
                    DataRow["EmbossingStart"] = GetDate(embossingactual); chen += "" + GetDate1(embossingactual) + ",";
                    DataRow["EmbossingFinish"] = GetDate(embossingreleaseactual); chen += "" + GetDate1(embossingreleaseactual) + ",";
                }
                else
                {
                    DataRow["Embossing"] = 0; chen += "0,";
                    DataRow["EmbossingStart"] = DBNull.Value; chen += "NULL,";
                    DataRow["EmbossingFinish"] = DBNull.Value; chen += "NULL,";
                }
                if (debossing.Checked)
                {
                    DataRow["Debossing"] = 1; chen += "1,";
                    DataRow["DebossingStart"] = GetDate(debossingactual); chen += "" + GetDate1(debossingactual) + ",";
                    DataRow["DebossingFinish"] = GetDate(debossingreleaseactual); chen += "" + GetDate1(debossingreleaseactual) + ",";
                }
                else
                {
                    DataRow["Debossing"] = 0; chen += "0,";
                    DataRow["DebossingStart"] = DBNull.Value; chen += "NULL,";
                    DataRow["DebossingFinish"] = DBNull.Value; chen += "NULL,";
                }
                if (heat.Checked)
                {
                    DataRow["HeatTransfer"] = 1; chen += "1,";
                    DataRow["HeatTransferStart"] = GetDate(heatactual); chen += "" + GetDate1(heatactual) + ",";
                    DataRow["HeatTransferFinish"] = GetDate(heatreleaseactual); chen += "" + GetDate1(heatreleaseactual) + ",";
                }
                else
                {
                    DataRow["HeatTransfer"] = 0; chen += "0,";
                    DataRow["HeatTransferStart"] = DBNull.Value; chen += "NULL,";
                    DataRow["HeatTransferFinish"] = DBNull.Value; chen += "NULL,";
                }
                if (printing.Checked)
                {
                    DataRow["Printing"] = 1; chen += "1,";
                    DataRow["PrintingStart"] = GetDate(printingactual); chen += "" + GetDate1(printingactual) + ",";
                    DataRow["PrintingFinish"] = GetDate(printingreleaseactual); chen += "" + GetDate1(printingreleaseactual) + ",";
                }
                else
                {
                    DataRow["Printing"] = 0; chen += "0,";
                    DataRow["PrintingStart"] = DBNull.Value; chen += "NULL,";
                    DataRow["PrintingFinish"] = DBNull.Value; chen += "NULL,";
                }

                chen += "" + GetDate1(sewingactualstart) + ",";
                chen += "" + GetDate1(sewingactualfinish) + ",";
                chen += "'" + txtremark.Text + "',";
                chen += "0,";
                chen += "'" + DataRow["Status"].ToString() + "',";
                chen += "'" + DataRow["SysOwner"].ToString() + "',";
                chen += "'" + DateTime.Parse(DataRow["SysCreateDate"].ToString()).ToString("yyyyMMdd HH:mm:ss") + "',";
                chen += "'" + DataRow["SysLMUser"].ToString() + "',getdate(),";
                chen += "" + DataRow["LaserCutMost"].ToString() + "," + DataRow["BondingMost"].ToString() + "," + DataRow["EmbroideryMost"].ToString() + "," + DataRow["EmbossingMost"].ToString() + ",";
                chen += "" + DataRow["DebossingMost"].ToString() + "," + DataRow["HeatTransferMost"].ToString() + "," + DataRow["PrintingMost"].ToString() + ",";

                if (padprint.Checked)
                {
                    DataRow["PadPrint"] = 1; chen += "1,";
                    DataRow["PadPrintStart"] = GetDate(padprintactual); chen += "" + GetDate1(padprintactual) + ",";
                    DataRow["PadPrintFinish"] = GetDate(padprintreleaseactual); chen += "" + GetDate1(padprintreleaseactual) + ",";
                }
                else
                {
                    DataRow["PadPrint"] = 0; chen += "0,";
                    DataRow["PadPrintStart"] = DBNull.Value; chen += "NULL,";
                    DataRow["PadPrintFinish"] = DBNull.Value; chen += "NULL,";
                }
                chen += "" + DataRow["PadPrintMost"].ToString() + ",";
                if (subprint.Checked)
                {
                    DataRow["SubPrint"] = 1; chen += "1,";
                    DataRow["SubPrintStart"] = GetDate(subprintactual); chen += "" + GetDate1(subprintactual) + ",";
                    DataRow["SubPrintFinish"] = GetDate(subprintreleaseactual); chen += "" + GetDate1(subprintreleaseactual) + ",";
                }
                else
                {
                    DataRow["SubPrint"] = 0; chen += "0,";
                    DataRow["SubPrintStart"] = DBNull.Value; chen += "NULL,";
                    DataRow["SubPrintFinish"] = DBNull.Value; chen += "NULL,";
                }
                chen += "" + DataRow["SubPrintMost"].ToString() + ",'" + DataRow["Barcode"].ToString() + "','" + urgent + "','')";

                //switch (DataRow["Status"].ToString())
                //{
                //    case "New": DataRow["Status"] = "N"; break;
                //    case "Incomplete": DataRow["Status"] = "I"; break;
                //    case "In Queue": DataRow["Status"] = "Q"; break;
                //    case "In Production": DataRow["Status"] = "P"; break;
                //    case "Shipped": DataRow["Status"] = "S"; break;
                //    case "Finish": DataRow["Status"] = "F"; break;
                //}

                bool exit = true;
                DataTable dt = DataRow.Table.Clone(); dt.Rows.Add(DataRow.ItemArray);

                dt.Columns.Remove(dt.Columns["MarkerPlan"]);
                dt.Columns.Remove(dt.Columns["MarkerActual"]);
                dt.Columns.Remove(dt.Columns["QCPlan"]);
                dt.Columns.Remove(dt.Columns["QCActual"]);
                dt.Columns.Remove(dt.Columns["CFTPlan"]);
                dt.Columns.Remove(dt.Columns["CFTActual"]);
                dt.Columns.Remove(dt.Columns["MarkerReleasePlan"]);
                dt.Columns.Remove(dt.Columns["MarkerReleaseActual"]);
                dt.Columns.Remove(dt.Columns["PatternReleaseActual"]);
                dt.Columns.Remove(dt.Columns["PrintingReleasePlan"]);
                dt.Columns.Remove(dt.Columns["PrintingReceivePlan"]);
                dt.Columns.Remove(dt.Columns["EMBReleasePlan"]);
                dt.Columns.Remove(dt.Columns["EMBReceivePlan"]);
                dt.Columns.Remove(dt.Columns["HeatReleasePlan"]);
                dt.Columns.Remove(dt.Columns["HeatReceivePlan"]);
                dt.Columns.Remove(dt.Columns["DebossingReleasePlan"]);
                dt.Columns.Remove(dt.Columns["DebossingReceivePlan"]);
                dt.Columns.Remove(dt.Columns["EmbossingReleasePlan"]);
                dt.Columns.Remove(dt.Columns["EmbossingReceivePlan"]);
                dt.Columns.Remove(dt.Columns["BondingReleasePlan"]);
                dt.Columns.Remove(dt.Columns["BondingReceivePlan"]);
                dt.Columns.Remove(dt.Columns["LaserReleasePlan"]);
                dt.Columns.Remove(dt.Columns["LaserReceivePlan"]);
                dt.Columns.Remove(dt.Columns["PadprintReleasePlan"]);
                dt.Columns.Remove(dt.Columns["PadprintReceivePlan"]);
                dt.Columns.Remove(dt.Columns["SubprintReleasePlan"]);
                dt.Columns.Remove(dt.Columns["SubprintReceivePlan"]);
                dt.Columns.Remove(dt.Columns["PrintingCount"]);
                dt.Columns.Remove(dt.Columns["EMBCount"]);
                dt.Columns.Remove(dt.Columns["HeatCount"]);
                dt.Columns.Remove(dt.Columns["DebossingCount"]);
                dt.Columns.Remove(dt.Columns["EmbossingCount"]);
                dt.Columns.Remove(dt.Columns["BondingCount"]);
                dt.Columns.Remove(dt.Columns["LaserCount"]);
                dt.Columns.Remove(dt.Columns["PadprintCount"]);
                dt.Columns.Remove(dt.Columns["SubprintCount"]);

                RunCmd();

                string GetData(string str)
                {
                    string rt = "";
                    try
                    {
                        rt = str.Split('-')[0];
                    }
                    catch { }
                    return rt;
                }
                object GetDate(DateTimePicker dtp)
                {
                    if (dtp.Format == DateTimePickerFormat.Custom) return DBNull.Value;
                    else return dtp.Value.ToLongDateString();
                }
                string GetDate1(DateTimePicker dtp)
                {
                    if (dtp.Format == DateTimePickerFormat.Custom) return "NULL";
                    else return "'" + dtp.Value.ToString("yyyyMMdd") + "'";
                }
                bool Existed()
                {
                    bool t = false;

                    DataTable kt = kn.Doc("select * from sromstr where DocNo = '" + txtdocno.Text + "'").Tables[0];

                    if (kt.Rows.Count > 0) t = true;

                    return t;
                }

                void RunCmd()
                {
                    //frmshowcomment frm = new frmshowcomment(chen);frm.ShowDialog();
                    if (show) kn.Ghi("delete from sromstrdel where DocNo = '" + txtdocno.Text + "' \n" +
                                    "insert into sromstrdel select * from sromstr where DocNo = '" + txtdocno.Text + "' \n" +
                                    "delete from sromstr where DocNo = '" + txtdocno.Text + "' \n" +
                                    "delete from sroasm where DocNo = '" + txtdocno.Text + "' \n" +
                                    "delete from sromstrsampleid where DocNo = '" + txtdocno.Text + "'");
                    if (Existed())
                    {
                        txtdocno.Text = ads + DateTime.Now.ToString("yyMMddHHmmss");

                        SaveCmd();
                    }
                    else
                    {
                        kn.Ghi(chen);

                        if (kn.ErrorMessage != "")
                        {
                            exit = false;
                            MessageBox.Show("sromstr  " + chen + kn.ErrorMessage);
                        }
                        else
                        {
                            kn.Ghi("insert into sromstrlog values ('" + txtdocno.Text + "',0,'" + DateTime.Now.ToString("yyyyMMdd") + "','" + chen.Replace("'", "") + "',100,'" + Temp.User + "',getdate())");

                            if (doi) kn.Ghi("update sromstrunlock set UnlockStatus = 1,SysLMBy = '" + Temp.User + "',SysLMDate = getdate() where DocNo = '" + txtdocno.Text + "' and UnlockStatus = 0");

                            //if (dtgreason.RowCount > 0)
                            {
                                string rs = "delete from sromstrreason where DocNo = '" + txtdocno.Text + "' \n";
                                foreach (DataGridViewRow dr in dtgreason.Rows)
                                {
                                    rs += "insert into sromstrreason values ('" + dept + "','" + txtdocno.Text + "','" + dr.Cells["Type"].Value + "',N'" + dr.Cells["Reason"].Value + "','" + DateTime.Parse(dr.Cells["SysCreatedDate"].Value.ToString()).ToString("yyyyMMdd HH:mm:ss") + "') \n";
                                }
                                //MessageBox.Show(rs);
                                //Clipboard.SetText(rs);
                                kn.Ghi(rs);
                            }
                        }
                        //if (kn.ErrorMessage != "") MessageBox.Show(chen + kn.ErrorMessage);
                        kn.Ghi("delete from smomstr2 where DocNo = '" + txtdocno.Text + "'");
                        string ch = "insert into smomstr2 values ('" + txtdocno.Text + "'," +
                                    (markerplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + markerplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (markeractual.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + markeractual.Value.ToString("yyyyMMdd") + "'") + "," +
                                   (qcplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + qcplan.Value.ToString("yyyyMMdd") +
                                   (qcreceivetime.Format == DateTimePickerFormat.Custom ? "'" : " " + qcreceivetime.Value.ToString("HH:mm:ss") + "'")) + "," +
                                    (qcactual.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + qcactual.Value.ToString("yyyyMMdd") +
                                    (qcreleasetime.Format == DateTimePickerFormat.Custom ? "'" : " " + qcreleasetime.Value.ToString("HH:mm:ss") + "'")) + "," +
                                    (cftplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + cftplan.Value.ToString("yyyyMMdd") +
                                    (cftreceivetime.Format == DateTimePickerFormat.Custom ? "'" : " " + cftreceivetime.Value.ToString("HH:mm:ss") + "'")) + "," +
                                   (cftactual.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + cftactual.Value.ToString("yyyyMMdd") +
                                   (cftreleasetime.Format == DateTimePickerFormat.Custom ? "'" : " " + cftreleasetime.Value.ToString("HH:mm:ss") + "'")) + "," +
                                   (finishactual.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + finishactual.Value.ToString("yyyyMMdd") + "'") + "," +
                                   (markerreleaseplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + markerreleaseplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (markerreleaseactual.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + markerreleaseactual.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (printingplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + printingplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (printingreleaseplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + printingreleaseplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (embroideryplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + embroideryplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (embroideryreleaseplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + embroideryreleaseplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (heatplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + heatplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (heatreleaseplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + heatreleaseplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (debossingplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + debossingplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (debossingreleaseplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + debossingreleaseplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (embossingplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + embossingplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (embossingreleaseplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + embossingreleaseplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (bondingplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + bondingplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (bondingreleaseplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + bondingreleaseplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (lazerplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + lazerplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (lazerreleaseplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + lazerreleaseplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (padprintplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + padprintplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (padprintreleaseplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + padprintreleaseplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (subprintplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + subprintplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    (subprintreleaseplan.Format == DateTimePickerFormat.Custom ? "NULL" : "'" + subprintreleaseplan.Value.ToString("yyyyMMdd") + "'") + "," +
                                    printingcount.Value + "," + embroiderycount.Value + "," + heatcount.Value + "," + debossingcount.Value + "," + embossingcount.Value + "," +
                                    bondingcount.Value + "," + lazercount.Value + "," + padprintcount.Value + "," + subprintcount.Value + "," + capacityid + "," + smv.ToString().Replace(",", ".") + ") ";
                        kn.Ghi(ch);
                        if (kn.ErrorMessage != "") MessageBox.Show(ch + kn.ErrorMessage);
                        else kn.Ghi("insert into sromstrlog values ('" + txtdocno.Text + "',1,'" + DateTime.Now.ToString("yyyyMMdd") + "','" + ch.Replace("'", "") + "',100,'" + Temp.User + "',getdate())");

                        if (urgent == 1)
                        {
                            if (updateurgent != "")
                            {
                                kn.Ghi(updateurgent);
                                updateurgent = "";
                            }
                        }

                        if (ls.Count > 0)
                        {
                            string insert = "";
                            foreach (SizeQty sq in ls)
                            {
                                insert += "insert into sroasm values ('" + txtdocno.Text + "'," + sq.ColorID + "," + sq.SizeID + ",'" + sq.Color + "','" + sq.Size + "'," + sq.Qty + ") \n";
                            }

                            if (insert != "")
                            {
                                kn.Ghi(insert);
                                if (kn.ErrorMessage != "")
                                {
                                    exit = false;
                                    MessageBox.Show("Insert Size Qty !!! " + kn.ErrorMessage);
                                }
                            }

                            kn.Ghi("delete from sromstrsampleid where DocNo = '" + txtdocno.Text + "' and Status is null");
                        }

                        if (exit) { Close(); fr1.worker.RunWorkerAsync(); }
                    }
                }
            }
        }

        private void AddSampleID(int it = 1)
        {
            int order = 1; string query = "";//"delete from sromstrsampleid where DocNo = '" + txtdocno.Text + "' \n";
            foreach (SizeQty sx in ls)
            {
                int q = int.Parse(sx.Qty);
                for (int i = 0; i < q; i++)
                {
                    if (order <= qty)
                    {
                        if (it > 1)
                        {
                            for (int j = 1; j <= it; j++)
                                query += "insert into sromstrsampleid values ('" + txtdocno.Text + "'," + order + ",'" + txtdocno.Text + "-" + order + "-" + j + "','" + sx.Color + "','" + sx.Size + "'," +
                                     "0,NULL,NULL,NULL,NULL,NULL,getdate()) \n";
                        }
                        else query += "insert into sromstrsampleid values ('" + txtdocno.Text + "'," + order + ",'" + txtdocno.Text + "-" + order + "','" + sx.Color + "','" + sx.Size + "'," +
                                     "0,NULL,NULL,NULL,NULL,NULL,getdate()) \n";

                        order++;
                    }
                    else break;
                }
            }

            query += " EXEC [dbs].[usp_sromstrsampleid_UpdateData]";

            if (query != "") kn.Ghi(query);

            if (kn.ErrorMessage != "")
            {
                MessageBox.Show(kn.ErrorMessage);
                txtremark.Text = query;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!lbstatus.Text.Contains("NEW"))
            {
                lbtimeleft.Visible = true;
                dem++;

                if (dem > 120) Close();
                else
                {
                    if (Form.ActiveForm == this) dem = 0;
                }

                lbtimeleft.Text = "Auto close after 120 seconds : " + dem;

                if (dem > 0)
                {
                    Random rd = new Random();

                    lbtimeleft.ForeColor = Color.FromArgb(rd.Next(1, 255), rd.Next(1, 255), rd.Next(1, 255));
                }
                else lbtimeleft.ForeColor = Color.Black;
            }
            else lbtimeleft.Visible = false;
        }

        private void btmeraddcomment_Click(object sender, EventArgs e)
        {
            if (txtdocno.Text != "")
            {
                try
                {
                    string smt = "";
                    smt = ds.Tables[1].Select("Code = '" + smptype.Text.ToString().Split('-')[0].Trim() + "'")[0][2].ToString();

                    frmaddcmt frm = new frmaddcmt(txtdocno.Text, this, season.Text, style.Text, smt, dept);
                    frm.ShowDialog();
                }
                catch { }
            }
        }

        private void txtcftqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                    (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtcftqty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int q1 = string.IsNullOrEmpty(txtcftsampleqty.Text) ? 0 : int.Parse(txtcftsampleqty.Text);
                int q2 = string.IsNullOrEmpty(txtcftpass.Text) ? 0 : int.Parse(txtcftpass.Text);
                int q3 = 0;

                foreach (DataGridViewRow r in dtgcftstatus.Rows) q3 += string.IsNullOrEmpty(r.Cells[1].Value.ToString()) ? 0 : int.Parse(r.Cells[1].Value.ToString());
                q2 += q3;

                if (q1 < q2) txtcftpass.Text = (q1 - q3).ToString();
                else if (q1 == q3 || q2 == 0) txtcftpass.Text = "";
            }
            catch { }
        }
        List<string> stage = new List<string>();
        private void btcftadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgcftstatus.RowCount > 0)
                {
                    string url = kn.Doc("select * from InlineQCSystem where STT = 63").Tables[0].Rows[0][0].ToString();

                    string path = System.IO.Path.Combine(url, dtgcftstatus.Rows[dtgcftstatusrow].Cells["Picture"].Value.ToString());

                    Process.Start(path);
                }
            }
            catch { }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == tabControl1.TabPages.Count - 1 && cmbreason.Items.Count == 0) LoadReason();
        }

        private void btunlock_Click(object sender, EventArgs e)
        {
            try
            {
                string ch = "delete from sromstrunlock where DocNo = '" + txtdocno.Text + "' and UnlockStatus = 0 \n" +
                            " insert into sromstrunlock values ('" + dept + "','" + txtdocno.Text + "','" + Temp.User + "',getdate(),0,NULL,NULL)";

                kn.Ghi(ch);

                if (kn.ErrorMessage == "") MessageBox.Show("Unlock done");
                else MessageBox.Show(kn.ErrorMessage);
            }
            catch { }
        }

        private void resetDateToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {

        }

        private void frmitem_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmitem_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void frmitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.U && e.Modifiers == Keys.Control)
            {
                MessageBox.Show("Modification Mode !!!");
                txtpass.Show(); txtpass.Enabled = true;
                btapply.Show(); btapply.Enabled = true;

                txtpass.Focus();
            }
        }

        private void btapply_Click(object sender, EventArgs e)
        {
            if (txtpass.Text == "pro123")
            {
                MessageBox.Show("Modified mode is opened");

                dataGridView2.ReadOnly = false;
                foreach (Control ctr in panel1.Controls) { if (!(ctr is Label)) ctr.Enabled = true; }
            }

            txtpass.Hide();
            btapply.Hide();
        }

        private void bturgent_Click(object sender, EventArgs e)
        {
            string str = "";
            if (bturgent.Text == "Urgent")
            {
                urgent = 1;
                bturgent.Text = "Abort Urgent";
                email = kn.Doc("select * from [DtradeProduction].[dbo].[00SampleType] where SampleType = '" + dept + "'").Tables[0].Rows[0][1].ToString();

                str = "Your sample order will be placed in urgent mode !!!";

                frmthongbao fmr = new frmthongbao(str);
                fmr.Show();

                updateurgent = "insert into sromstrurgent values ('" + DateTime.Now.ToString("yyyyMMdd") + "','" + txtdocno.Text + "','Not Approved',NULL,NULL,getdate(),'" + email + "',NULL)";
            }
            else if (bturgent.Text == "Abort Urgent")
            {
                urgent = 0;
                bturgent.Text = "Urgent";
                str = "You have already aborted the urgent mode !!!";

                frmthongbao fmr = new frmthongbao(str);
                fmr.Show();

                updateurgent = "delete sromstrurgent where DocNo = '" + txtdocno.Text + "')";
            }
            else
            {
                urgent = 2;
                string update = "update sromstrurgent set Status = 'Approved',ApprovedBy = '" + Temp.User + "',ApprovedDate = getdate() where DocNo = '" + txtdocno.Text + "' \n" +
                                "update sromstr set Urgent = '2' where DocNo = '" + txtdocno.Text + "'";

                kn.Ghi(update);

                if (kn.ErrorMessage == "")
                {
                    str = "Thanks for your approval !!!";

                    frmthongbao fmr = new frmthongbao(str);
                    fmr.Show();

                    Close();
                    fr1.worker.RunWorkerAsync();
                }
            }
        }
        int dtgcftstatusrow = 0;

        private void frmitem_Activated(object sender, EventArgs e)
        {
            dem = 0;
        }

        private void addColorSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (smorderno.Text != "" && txtdocno.Text != "")
            {
                frmaddcolorsize frm = new frmaddcolorsize(this, smorderno.Text, txtdocno.Text);
                frm.ShowDialog();
            }
        }

        private void smptype_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSMVData();

            lbsmv.Text = "Capacity : " + (smv * qty);
        }

        private void gmttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSMVData();

            lbsmv.Text = "Capacity : " + (smv * qty);
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CaculateSMV();
        }

        private void btsampleidlist_Click(object sender, EventArgs e)
        {
            DataTable ex = kn.Doc("select * from sromstrsampleid where DocNo = '" + txtdocno.Text + "' ").Tables[0];

            if (ex.Rows.Count == 0)
            {
                if (unit.Text.Contains("SET"))
                {
                    frmaddidqty frm = new frmaddidqty();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        int i = frm.qty; //MessageBox.Show("KQ : " + frm.qty);
                        AddSampleID(i);
                    }
                }
                else AddSampleID();
            }

            ShowSQID(txtdocno.Text);
        }

        private void programcode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dtgreason.RowCount > row)
            {
                dtgreason.Rows.RemoveAt(row);
            }
        }

        private void btaddreason_Click(object sender, EventArgs e)
        {
            try
            {
                string type = rdcustomer.Checked ? "Customer" : "Internal";
                string reason = cmbreason.Text == "Other" ? txtother.Text : cmbreason.Text;

                if (reason == "") MessageBox.Show("Please input the reason !!!");
                else
                {
                    DataTable d = dtgreason.DataSource as DataTable;

                    DataRow r = d.NewRow();

                    r[0] = type;
                    r[1] = reason;
                    r[2] = DateTime.Now;

                    d.Rows.Add(r);
                    d.AcceptChanges();

                    dtgreason.DataSource = d;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void btcmtinddt_Click(object sender, EventArgs e)
        {
            if (style.Text != "")
            {
                DataTable dt = kn.Doc("exec SampleQueueLoading 43, '" + style.Text + "', '', ''").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    string url = "http://192.168.1.100:8083/sharefile/preview2_pdf.php?dl=" + dt.Rows[0][0] + "&ss=" + dt.Rows[0][1] + "&ct=AD";
                    Process.Start(url);
                }
                else MessageBox.Show("No found the file in DDT !!!");
            }
        }

        private void dtgreason_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) row = e.RowIndex;
        }

        public void ShowSQID(string dn)
        {
            DataTable dt = kn.Doc("select * from sromstrsampleid where DocNo = '" + dn + "' order by RecNo").Tables[0];

            frmsqid frm = new frmsqid(dt, style.Text, season.Text, qty.ToString(), smptype.Text, lbstatus.Text);
            frm.ShowDialog();

        }
        private void frmitem_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isopen) kn.Ghi("update sromstr set IsOpen = '' where DocNo = '" + DataRow[0].ToString() + "'");
        }

        private void dtgcftstatus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) dtgcftstatusrow = e.RowIndex;
        }

        private void btcheck_Click(object sender, EventArgs e)
        {
            frmlimit frm = new frmlimit();
            frm.ShowDialog();
        }

        int dtgqcdefectlistrow = 0;
        private void dtgqcdefectlist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) dtgqcdefectlistrow = e.RowIndex;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ID = " + capacityid + " Capacity = " + capacity);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgqcdefectlist.RowCount > 0)
                {
                    string url = kn.Doc("select * from InlineQCSystem where STT = 63").Tables[0].Rows[0][0].ToString();

                    string path = System.IO.Path.Combine(url, dtgqcdefectlist.Rows[dtgqcdefectlistrow].Cells["Picture"].Value.ToString());
                    MessageBox.Show(path);
                    Process.Start(path);
                }
            }
            catch { }
        }

        private void dtgcftstatus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you wanna delete this record ?", "DELETE RECORD", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dtgcftstatus.Rows.RemoveAt(e.RowIndex);
                }
            }
            catch { }
        }

        private void smorderno_TextChanged(object sender, EventArgs e)
        {

        }

        private void btclose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save this action ?", "SAVE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                SaveCmd();
            else Close();
        }

        private void lazerplan_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTimePicker date = sender as DateTimePicker;

                TimeSpan tm = DateTime.Now - date.Value;

                if (tm.TotalDays > 120) date.Format = DateTimePickerFormat.Custom;
                else date.Format = DateTimePickerFormat.Short;
            }
            catch { }
        }
    }
    class SizeQty
    {
        public string ColorID { get; set; }
        public string SizeID { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Qty { get; set; }
    }
}
