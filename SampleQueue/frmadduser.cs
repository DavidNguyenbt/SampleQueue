using CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleQueue
{
    public partial class frmadduser : Form
    {
        List<string> users = new List<string>();
        Connect kn = new Connect(Temp.ch);
        public frmadduser()
        {
            InitializeComponent();
        }

        private void frmadduser_Load(object sender, EventArgs e)
        {
            cmbdept.Items.AddRange(Temp.BoPhan.ToArray());
            cmbdept.Items.Remove("All");

            DataTable dt = kn.Doc("select * from nguser").Tables[0];
            users = dt.Select().Select(s => s[0].ToString()).ToList();

            string ads = "ADM - ADMIN";
            if (Temp.DeptDesc.ToUpper().Contains("ADIDAS") || Temp.DeptDesc.ToUpper().Contains("MERA")) ads = "ADIDAS - ADIDAS";
            else if (Temp.DeptDesc.ToUpper().Contains("PUMA")) ads = "PUMA - PUMA";
            else if (Temp.DeptDesc.ToUpper().Contains("NB")) ads = "NB - NEW BALANCE";
            else if (Temp.DeptDesc.ToUpper().Contains("PW")) ads = "PW - PROWER";

            cmbbrand.SelectedItem = ads;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btadd_Click(object sender, EventArgs e)
        {
            if (cmbbrand.Text == "")
            {
                MessageBox.Show("Please select the brand !!!");
                cmbbrand.Focus();
            }
            else if (cmbdept.Text == "")
            {
                MessageBox.Show("Please select the department !!!");
                cmbdept.Focus();
            }
            else if (txtid.Text == "")
            {
                MessageBox.Show("Please enter the Employee ID !!!");
                txtid.Focus();
            }
            else if (txtusername.Text == "")
            {
                MessageBox.Show("Please enter the Username !!!");
                txtusername.Focus();
            }
            else if (txtpass.Text == "")
            {
                MessageBox.Show("Please enter the Password !!!");
                txtusername.Focus();
            }
            else if (txtpass.Text.Length <12)
            {
                MessageBox.Show("Password length must be greater than 12 digits !!!");
                txtusername.Focus();
            }
            else if (users.Contains(txtusername.Text.ToUpper()))
            {
                MessageBox.Show("This Username is existed, please select another Username !!!");
                txtid.Focus();
            }
            else
            {
                string ads = cmbbrand.Text.Split('-')[0].Trim();
                //if (Temp.DeptDesc.ToUpper().Contains("ADIDAS") || Temp.DeptDesc.ToUpper().Contains("MERA")) ads = "ADIDAS";
                //else if (Temp.DeptDesc.ToUpper().Contains("PUMA")) ads = "PUMA";
                //else if (Temp.DeptDesc.ToUpper().Contains("NB")) ads = "NB";
                //else if (Temp.DeptDesc.ToUpper().Contains("PW")) ads = "PW";

                string[] dept = cmbdept.Text.Split('-');

                string d1 = dept[0].ToUpper().Trim(), d2 = dept[1].ToUpper().Trim() + " " + ads, user = txtusername.Text.ToUpper(), pass = txtpass.Text, id = txtid.Text.ToUpper();

                //MessageBox.Show(d1 + "," + d2 + "," + user + "," + pass + "," + id);

                kn.Ghi("exec GetDataFromQuery 61,'" + user + "','" + d2 + "','" + pass + "','" + d1 + "','" + id + "'");

                if (kn.ErrorMessage == "")
                {
                    MessageBox.Show("Add new user success !!!");
                    users.Add(user);
                }
                else MessageBox.Show(kn.ErrorMessage);
            }
        }

        private void txtid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = new DataTable();
                dt = kn.Doc("select * from Hr.dbo.staff where code = " + txtid.Text).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    txtid.Text = dt.Rows[0][0].ToString();
                }
            }
        }
    }
}
