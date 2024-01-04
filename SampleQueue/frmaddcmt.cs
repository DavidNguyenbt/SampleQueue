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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleQueue
{
    public partial class frmaddcmt : Form
    {
        List<string> bp = new List<string>();
        List<string> file = new List<string>();
        WebClient wb = new WebClient();
        string docno = "", style = "", ss = "", smt = "", ads = "", url = "http://192.168.1.100:8083/", parentid = "";
        Connect kn = new Connect(Temp.ch);
        frmitem frm;
        public frmaddcmt(string dn, frmitem f, string _ss, string _style, string _smt, string _ads, string _parentid = "")
        {
            InitializeComponent();

            docno = dn; frm = f; style = _style; ss = _ss; smt = _smt; ads = _ads; parentid = _parentid;
        }

        private void frmaddcmt_Load(object sender, EventArgs e)
        {
            cmbdept.Items.AddRange(Temp.BoPhan.ToArray());
            //dtgcmtcontent.Hide(); 
            chkgrid.Checked = true; //btload.Hide();

            lbinfor.Text = style + "|" + ss + "|" + smt + "|" + ads;

            try
            {
                url = kn.Doc("select IPSERVER from InlineQCSystem where STT = 62").Tables[0].Rows[0][0].ToString();
            }
            catch { }

            if (Temp.Profile.Contains("A1A")) cmbid.Text = Temp.Profile;
        }

        private void cmbdept_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbdept.Text != "")
                {
                    if (cmbdept.Text == "All")
                    {
                        bp.Clear(); bp.Add("All");
                    }
                    else
                    {
                        if (!bp.Contains(cmbdept.Text) && !bp.Contains("All")) bp.Add(cmbdept.Text);
                    }

                    AddBoPhan();
                }
            }
            catch { }
        }
        private void AddBoPhan()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("BoPhan", typeof(string));
                if (bp.Count > 0)
                {
                    foreach (string str in bp)
                    {
                        if (str == "All")
                            dt.Rows.Add("All");
                        else dt.Rows.Add(str.Split('-')[1].Trim());
                    }
                }

                dtgbophan.DataSource = dt;
            }
            catch { }
        }
        private void AddFile()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("FilePath", typeof(string));
                if (file.Count > 0)
                {
                    foreach (string str in file)
                    {
                        dt.Rows.Add(str);
                    }
                }

                dtgfile.DataSource = dt;

                if (dt.Rows.Count == 0) picture.Image = null;
            }
            catch { }
        }

        private void cmbid_TextChanged(object sender, EventArgs e)
        {
            cmbid.Text = cmbid.Text.ToUpper();
            cmbid.SelectionStart = cmbid.Text.Length;
        }

        private void cmbid_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dtgfile_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string str = dtgfile.Rows[e.RowIndex].Cells[0].Value.ToString();

                file.Remove(str);

                AddFile();
            }
            catch { }
        }

        private void pasteImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Image clipboardImage = Clipboard.GetImage();

                if (clipboardImage != null)
                {
                    string imagePath = Path.GetTempFileName();
                    //Path.ChangeExtension(imagePath, ".jpg");
                    imagePath = imagePath.Replace(".tmp", ".jpg");
                    clipboardImage.Save(imagePath);

                    picture.Image = clipboardImage;

                    file.Add(imagePath);
                    AddFile();

                    picture.Click += delegate
                    {
                        Process.Start(imagePath);
                    };
                }
                else
                {
                    if (Clipboard.ContainsFileDropList())
                    {
                        string path = Clipboard.GetFileDropList()[0];

                        picture.Image = new Bitmap(path);

                        file.Add(path);
                        AddFile();

                        picture.Click += delegate
                        {
                            Process.Start(path);
                        };
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void chkgrid_CheckedChanged(object sender, EventArgs e)
        {
            if (chkgrid.Checked)
            {
                dtgcmtcontent.Show();
                btload.Show();
            }
            else
            {
                dtgcmtcontent.Hide();
                btload.Hide();
            }
        }

        private void btload_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Excel File (*.xlsx)|*.xlsx|Excel File (*.xls)|*.xls";
            op.Title = "Select your file";


            if (op.ShowDialog() == DialogResult.OK)
            {
                using (var stream = File.Open(op.FileName, FileMode.Open, FileAccess.Read))
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

                        dtgcmtcontent.DataSource = result.Tables[0];
                    }
                }
            }
        }

        private void cmbid_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmbid.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbid.Items.Clear();

                    DataTable dt = kn.Doc("exec GetLoadData 30,'" + cmbid.Text + "',''").Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow r = dt.Rows[0];

                        cmbid.Items.AddRange(dt.Select().Select(s => s[0].ToString()).ToArray());

                        cmbid.SelectedIndex = 0;
                    }
                    else cmbid.Text = "";
                }
            }
        }

        private void dtgbophan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string str = bp.Find(d => d.Contains(dtgbophan.Rows[e.RowIndex].Cells[0].Value.ToString()));

                bp.Remove(str);

                AddBoPhan();
            }
            catch { }
        }

        private void btaddfile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();

                op.Multiselect = true;

                if (op.ShowDialog() == DialogResult.OK)
                {
                    foreach (string str in op.FileNames)
                    {
                        if (!file.Contains(str)) file.Add(str);
                    }

                    AddFile();
                }
            }
            catch { }
        }
        private bool CheckData()
        {
            bool t = false;

            if (dtgcmtcontent.Rows[0].Cells[0].Value is null || dtgcmtcontent.Rows[0].Cells[1].Value is null || dtgcmtcontent.Rows[0].Cells[2].Value is null)
                t = true;
            else if (dtgcmtcontent.Rows[0].Cells[0].Value.ToString() == "" && dtgcmtcontent.Rows[0].Cells[1].Value.ToString() == "" && dtgcmtcontent.Rows[0].Cells[2].Value.ToString() == "")
                t = true;

            return t;
        }
        private void btaddcomment_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show(dtgcmtcontent.RowCount.ToString());
                string cmtid = DateTime.Now.ToString("yyMMddHHmmss");
                if (txtcmtcontent.Text == "" && CheckData())
                {
                    MessageBox.Show("You need to add some comments first !!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcmtcontent.Focus();
                }
                else if (bp.Count == 0)
                {
                    MessageBox.Show("You need to select the department !!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbdept.Focus();
                }
                else if (file.Count > 0 && cmbid.Text.Length < 10)
                {
                    MessageBox.Show("You need to select your employee ID !!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbid.Focus();
                }
                else
                {
                    string ch = "", ads = "", dept = ";";

                    foreach (DataGridViewRow r in dtgbophan.Rows) dept += r.Cells[0].Value.ToString();

                    if (Temp.DeptDesc.ToUpper().Contains("ADIDAS") || Temp.DeptDesc.ToUpper().Contains("MERA")) ads = "ADS"; else ads = "PUMA";

                    int i = 0;
                    foreach (string str in file)
                    {
                        wb.Headers.Add("Content-Type", "binary/octet-stream");
                        byte[] result = wb.UploadFile(string.Format(url + "upfile.php?s={0}&st={1}&t={2}&u={3}&c={4}&cs={5}&i={6}",
                                                                ss, style, smt, cmbid.Text, txtcmtcontent.Text, ads, i.ToString()), "POST", str);//wb.UploadFile(Temp.url + "filemanager.php", "POST", str);
                        string Result_msg = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);

                        //MessageBox.Show(Result_msg);

                        ch += "insert into SampleCommentFileAttachment values ('" + cmtid + "','" + Result_msg + "',getdate()) \n";

                        i++;
                    }

                    if (i > 0) ch += "update nguser set Profile = '" + cmbid.Text + "' where Username = '" + Temp.User + "' \n";

                    ch += "insert into SampleCommentManager values ('" + DateTime.Now.ToString("yyyyMMdd") + "','" + cmtid + "','" + docno + "','" + Temp.User + "','" + ads + "','" +
                                (chkcustomer.Checked ? "Customer" : "Internal") + "','" + Temp.Dept + "','" + dept.Substring(1) + "','" + (chk.Checked ? "Not Resolved" : "Resolved") +
                                "',N'" + txtcmtcontent.Text + "',getdate()) \n";

                    if (chkgrid.Checked)
                    {
                        foreach (DataGridViewRow r in dtgcmtcontent.Rows)
                        {
                            if (r.Cells[0].Value != null && r.Cells[1].Value != null && r.Cells[2].Value != null)
                                ch += "insert into SampleCommentContentDetail values ('" + cmtid + "',N'" + r.Cells[0].Value.ToString() + "',N'" + r.Cells[1].Value.ToString() + "',N'" + r.Cells[2].Value.ToString() + "',getdate()) \n";
                        }
                    }

                    //MessageBox.Show(ch);
                    kn.Ghi(ch);

                    if (kn.ErrorMessage == "")
                    {
                        MessageBox.Show("Add new comment successfully !!!", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frm.LoadComments(docno);
                        Close();
                    }
                    else MessageBox.Show(kn.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dtgfile_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbdept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
