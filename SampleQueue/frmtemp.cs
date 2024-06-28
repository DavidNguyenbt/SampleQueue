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
using System.Windows.Media;

namespace SampleQueue
{
    public partial class frmtemp : Form
    {
        Connect kn = new Connect(Temp.ch);
        List<SizeQty> ls = new List<SizeQty>();
        string message = "";
        public frmtemp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains('\n'))
            {
                string[] vl = textBox1.Text.Split('\n');

                foreach (string v in vl)
                {
                    LoadColorSize(v.Trim());
                }

                textBox2.Text = message;
            }
        }

        private void LoadColorSize(string docno)
        {
            DataTable sizeqty = new DataTable();

            if (docno != "")
            {
                sizeqty = kn.Doc("select * from sroasm WITH(NOLOCK) where DocNo = '" + docno + "'").Tables[0];

                foreach (DataRow r in sizeqty.Rows)
                {
                    SizeQty it = new SizeQty()
                    {
                        ColorID = r["ColorID"].ToString(),
                        SizeID = r["SizeID"].ToString(),
                        Color = r["Color"].ToString(),
                        Size = r["Sizx"].ToString(),
                        Qty = r["Qty"].ToString()
                    };
                    ls.Add(it);
                }

                AddSampleID(docno);
            }
        }

        private void AddSampleID(string docno)
        {
            DataTable dt = kn.Doc("select * from sromstrsampleid where DocNo = '" + docno + "'").Tables[0];
            if (dt.Rows.Count > 0) message += docno + " is existing \n";
            else
            {
                int order = 1; string query = "";//"delete from sromstrsampleid where DocNo = '" + txtdocno.Text + "' \n";
                foreach (SizeQty sx in ls)
                {
                    int q = int.Parse(sx.Qty);
                    for (int i = 0; i < q; i++)
                    {
                        query += "insert into sromstrsampleid values ('" + docno + "'," + order + ",'" + docno + "-" + order + "','" + sx.Color + "','" + sx.Size + "'," +
                                         "0,NULL,NULL,NULL,NULL,NULL,getdate()) \n";
                        order++;
                    }
                }

                query += " EXEC [dbs].[usp_sromstrsampleid_UpdateData]";

                if (query != "") kn.Ghi(query);

                if (kn.ErrorMessage == "") message += docno + " add success !!! \n";
                else message += docno + " add failed !!! \n";
            }
        }
    }
}
