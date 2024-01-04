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
    public partial class frmchecklog : Form
    {
        Connect kn = new Connect(Temp.ch);
        DataTable dt = new DataTable();
        public frmchecklog()
        {
            InitializeComponent();
        }

        private void frmchecklog_Load(object sender, EventArgs e)
        {
            dt = kn.Doc("select * from sromstr where DocNo = '0'").Tables[0];

            foreach (DataColumn cl in dt.Columns) cl.DataType = typeof(string);

            dt.Columns.Add("LMModifyDate", typeof(string));
            dt.Columns.Add("LMModifyBy", typeof(string));

            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable d = kn.Doc("select * from sromstrlog where DocNo = '" + textBox1.Text + "' and No = 0 order by SysLMDate desc").Tables[0];

                dt.Rows.Clear();

                foreach (DataRow r in d.Rows)
                {
                    DataRow rr = dt.NewRow();

                    string c = r["Memo"].ToString();
                    string ch = c.Replace("insert into sromstr values (", "").Replace(")", "");

                    int i = 0;
                    foreach(string str in ch.Split(','))
                    {
                        rr[i] = str;
                        i++;
                    }
                    rr["LMModifyDate"] = r["SysLMDate"];
                    rr["LMModifyBy"] = r["SysLMUser"];

                    dt.Rows.Add(rr);
                }

                dataGridView1.DataSource = dt;
            }
            catch { }
        }
    }
}
