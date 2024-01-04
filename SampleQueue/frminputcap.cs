using CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleQueue
{
    public partial class frminputcap : Form
    {
        frmcapacity frm;
        string ads = "";
        Connect kn = new Connect(Temp.ch);
        public frminputcap(frmcapacity f, string s)
        {
            InitializeComponent();

            frm = f;
            ads = s;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtcapacity.Text != "")
            {
                string date = "NULL";
                if (checkBox1.Checked) date = "'" + dateTimePicker1.Value.ToString("yyyyMMdd") + "'";

                string ch = "insert into sromstrcapacity values ('','" + ads + "'," + txtmanpower.Text + "," + txtworkinghour.Text.Replace(",", ".") +
                            ",'" + txtabsence.Text + "%','" + txteff.Text + "%'," + txtcapacity.Text + "," + date + ",getdate())";

                kn.Ghi(ch);

                if (kn.ErrorMessage == "")
                {
                    frm.LoadData();
                    Close();
                }
                else MessageBox.Show(kn.ErrorMessage);
            }
            else MessageBox.Show("The information is not enough !!!");
        }

        private void txtmanpower_TextChanged(object sender, EventArgs e)
        {
            if (txtmanpower.Text != "" && txtworkinghour.Text != "" && txtabsence.Text != "" && txteff.Text != "")
            {
                double man = double.Parse(txtmanpower.Text);
                double wk = double.Parse(txtworkinghour.Text);
                double ab = double.Parse(txtabsence.Text);
                double eff = double.Parse(txteff.Text);

                int cap = (int)(man * ((100 - ab) / 100) * wk * 60 * (eff / 100));

                txtcapacity.Text = cap.ToString();
            }
        }

        private void txtmanpower_KeyPress(object sender, KeyPressEventArgs e)
        {
            char dc = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != dc))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == dc) && ((sender as TextBox).Text.IndexOf(dc) > -1))
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = checkBox1.Checked;
        }
    }
}
