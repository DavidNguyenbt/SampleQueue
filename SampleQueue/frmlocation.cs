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
    public partial class frmlocation : Form
    {
        Connect kn;
        string spid;
        frmsetlocation frm;
        frmsqid frmsqid;
        DataTable dt;
        int i;
        public frmlocation(int i, string spid, Connect kn, frmsetlocation frm, frmsqid frmsqid)
        {
            InitializeComponent();
            this.spid = spid;
            this.kn = kn;
            this.frm = frm;
            this.frmsqid = frmsqid;
            this.i = i;
        }

        private void btexit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmlocation_Load(object sender, EventArgs e)
        {
            lbspid.Text += spid;

            dt = kn.Doc("exec SampleQueueLoading 25, '', '', ''").Tables[0];

            if (dt.Rows.Count > 0)
            {
                cmbstore.Items.AddRange(dt.Select().Select(s => s[1].ToString()).Distinct().ToArray());
            }
        }

        private void btapply_Click(object sender, EventArgs e)
        {
            if (cmblocation.Text != "")
            {
                if (i == 1) frm.LoadLocation(cmblocation.Text);//cmbstore.Text + "/" + 
                else
                {
                    kn.Ghi("exec SampleQueueLoading 26, '" + spid + "', '" + cmblocation.Text + "', ''");
                    frmsqid.Reload(spid);
                }

                Close();
            }
        }

        private void cmbstore_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                cmblocation.Items.AddRange(dt.Select("Stored = '" + cmbstore.Text + "'").Select(s => s[2].ToString()).Distinct().ToArray());
            }
        }
    }
}
