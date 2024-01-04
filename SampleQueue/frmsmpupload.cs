using ExcelDataReader;
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
using CSDL;
using System.Threading;

namespace SampleQueue
{
    public partial class frmsmpupload : Form
    {
        string path = "";
        DataTable dt = new DataTable();
        string process = "";
        Connect kn = new Connect(Temp.ch);
        public frmsmpupload(string p)
        {
            InitializeComponent();
            path = p;
        }

        private void frmsmpupload_Load(object sender, EventArgs e)
        {
            LoadFile();

            lbprocess.Visible = false;
            processbar.Visible = false;
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

                            dt = result.Tables[0];
                            dataGridView1.DataSource = dt;
                            lbrow.Text = "Rows : " + dt.Rows.Count;

                            string[] cl = dt.ColumnNameToArray();

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
                        }
                    }
                }
            }
            catch { }
        }
        private void btupload_Click(object sender, EventArgs e)
        {
            lbprocess.Visible = true;
            processbar.Visible = true;
            timer1.Start();
            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    string update = "";
                    foreach (DataRow r in dt.Rows)
                    {
                        update += "update sromstr set TechpackPlan = " + (string.IsNullOrEmpty(r[cmbshiptomer.Text].ToString()) ? "NULL," : "'" + DateTime.Parse(r[cmbshiptomer.Text].ToString()).ToString("yyyyMMdd") + "',") +
                            "SewingActualStart = " + (string.IsNullOrEmpty(r[cmbstart.Text].ToString()) ? "NULL," : "'" + DateTime.Parse(r[cmbstart.Text].ToString()).ToString("yyyyMMdd") + "',") +
                            "SewingActualFinish = " + (string.IsNullOrEmpty(r[cmbfinish.Text].ToString()) ? "NULL," : "'" + DateTime.Parse(r[cmbfinish.Text].ToString()).ToString("yyyyMMdd") + "',") +
                            "SewersName = N'" + r[cmbsewer.Text].ToString() + "' where DocNo = '" + r[cmbid.Text].ToString() + "' \n" +
                            "   update sromstr set Status = (case when TechpackPlan is null then 'P' else (case when RSD < TechpackPlan then 'F2' else 'F1' end) end),SysLMUser = '" + Temp.User + "',SysLMDate = getdate() " +
                            "   where DocNo = '" + r[cmbid.Text].ToString() + "' \n";

                        process = "Collecting for Sample ID : " + r[cmbid.Text].ToString();
                    }

                    if (update != "")
                    {
                        process = "Uploading for all !!!";

                        kn.Ghi(update);

                        if (kn.ErrorMessage == "") MessageBox.Show("Done !!!");
                        else MessageBox.Show(update + " \n " + kn.ErrorMessage);
                    }
                }
            }
            catch { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbprocess.Text = process;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lbprocess.Visible = false;
            processbar.Visible = false;
            timer1.Stop();
        }

        private void frmsmpupload_DragDrop(object sender, DragEventArgs e)
        {
            path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            LoadFile();
        }

        private void frmsmpupload_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
    }
}
