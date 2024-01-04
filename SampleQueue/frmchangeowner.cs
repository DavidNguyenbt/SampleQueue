using CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleQueue
{
    public partial class frmchangeowner : Form
    {
        DataTable row;
        Connect kn = new Connect(Temp.ch);
        Form1 frm;
        public frmchangeowner(DataTable r, Form1 f)
        {
            InitializeComponent();

            row = r; frm = f;
        }

        private void btexit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmchangeowner_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = row;

                lbname.Text = "Name : " + Temp.User;

                string dept = "";
                if (Temp.DeptDesc.ToUpper().Contains("ADIDAS") || Temp.DeptDesc.ToUpper().Contains("MERA")) dept = "ADIDAS"; else dept = "PUMA";

                DataTable user = kn.Doc("select Username from nguser where Department = 'MER' and Description like '%" + dept + "%' order by Username").Tables[0];

                cmbuser.Items.AddRange(user.Select().Select(d => d[0].ToString()).ToArray());
            }
            catch { }
        }

        private void btchange_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount > 0 && cmbuser.Text != "")
                {
                    DataTable user = kn.Doc("select * from nguser where Username = '" + Temp.User + "' and Passwd = '" + txtpass.Text.Replace("--", "") + "'").Tables[0];

                    if (user.Rows.Count > 0)
                    {
                        kn.Ghi("update sromstr set SysOwner = '" + cmbuser.Text + "' where DocNo = '" + dataGridView1.Rows[0].Cells["DocNo"].Value.ToString() + "'");

                        if (kn.ErrorMessage == "")
                        {
                            MessageBox.Show("Change owner successfully !!!");
                            frm.worker.RunWorkerAsync();
                            Close();
                        }
                        else MessageBox.Show(kn.ErrorMessage);
                    }
                    else
                    {
                        MessageBox.Show("Your password is incorrect !!!");
                        txtpass.Focus();
                    }
                }
            }
            catch { }
        }
    }
}
