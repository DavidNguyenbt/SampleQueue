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
    public partial class frmchangeuserpass : Form
    {
        Connect kn = new Connect(Temp.ch);
        string mk = "";
        public frmchangeuserpass(string mk = "")
        {
            InitializeComponent();
            this.mk = mk;
        }

        private void frmchangeuserpass_Load(object sender, EventArgs e)
        {
            lbname.Text = "User : " + Temp.User;
            if (mk != "") txtoldpass.Text = mk;
        }

        private void btexit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btchange_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = kn.Doc("select * from nguser where Username = '" + Temp.User + "' and Passwd = '" + txtoldpass.Text + "'").Tables[0];

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("The old password is incorrect !!!!");
                    txtoldpass.Focus();
                }
                else
                {
                    if (txtnewpass.Text != txtconfirm.Text)
                    {
                        MessageBox.Show("The confirmation password is not the same !!!!");
                        txtconfirm.Focus();
                    }
                    else if (!Temp.IsValidPassword(txtconfirm.Text))
                    {

                        MessageBox.Show("Your new password must comply with IMS policy: Minimum 12 characters, At least one uppercase letter, At least one lowercase letter, At least one number, At least one special character");
                        txtnewpass.Focus();
                    }
                    else
                    {
                        kn.Ghi("update nguser set Passwd = '" + txtnewpass.Text + "' where Username = '" + Temp.User + "'");

                        if (kn.ErrorMessage == "")
                        {
                            MessageBox.Show("Change the password is done !!!!");

                            Close();
                        }
                        else MessageBox.Show("Error  !!! " + kn.ErrorMessage);
                    }
                }
            }
            catch { }
        }
    }
}
