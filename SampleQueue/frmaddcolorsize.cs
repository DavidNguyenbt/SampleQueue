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
    public partial class frmaddcolorsize : Form
    {
        frmitem frm;
        string smorder, docno;
        Connect kn = new Connect(Temp.ch2);
        List<string> color = new List<string>();
        List<string> size = new List<string>();
        public frmaddcolorsize(frmitem f, string sm, string d)
        {
            InitializeComponent();

            frm = f;
            smorder = sm;
            docno = d;
        }

        private void frmaddcolorsize_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            color.Clear(); size.Clear();
            DataSet ds = kn.Doc("select * from smycolor where smorderno = '" + smorder + "' order by seqno \n " +
                                "select * from smysize where smorderno = '" + smorder + "' order by seqno");

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView2.DataSource = ds.Tables[1];

            color.AddRange(ds.Tables[0].Select().Select(s => s[3].ToString()).ToArray());
            size.AddRange(ds.Tables[1].Select().Select(s => s[3].ToString()).ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtcolor.Text != "" && !color.Contains(txtcolor.Text))
            {
                int i = dataGridView1.RowCount + 1;
                kn.Ghi("insert into smycolor values ('" + smorder + "'," + i + "," + (i * 10) + ",'" + txtcolor.Text.ToUpper() + "','')");
                LoadData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtsize.Text != "" && !size.Contains(txtsize.Text))
            {
                int i = dataGridView2.RowCount + 1;
                kn.Ghi("insert into smysize values ('" + smorder + "'," + i + "," + (i * 10) + ",'" + txtsize.Text.ToUpper() + "')");
                LoadData();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[dataGridView1.Rows.GetLastRow(DataGridViewElementStates.Visible)];
                kn.Ghi("delete from smycolor where smorderno = '" + smorder + "' and colorid = " + row.Cells[1].Value.ToString());
                LoadData();
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[dataGridView2.Rows.GetLastRow(DataGridViewElementStates.Visible)];
                kn.Ghi("delete from smysize where smorderno = '" + smorder + "' and sizeid = " + row.Cells[1].Value.ToString());
                LoadData();
            }
        }

        private void frmaddcolorsize_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm.LoadSize(smorder, docno);
        }
    }
}
