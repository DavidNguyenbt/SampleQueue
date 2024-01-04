using CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleQueue
{
    public partial class frmshowcomment : Form
    {
        string id = "", parentid = "", ads = "", docno = "";
        Connect kn = new Connect(Temp.ch);
        TreeNode node = null;
        DataTable Data = new DataTable();
        frmitem frm;
        public frmshowcomment(string i, string d, string dn, frmitem f)
        {
            InitializeComponent();

            id = i; ads = d; docno = dn; frm = f;
        }

        private void frmshowcomment_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = kn.Doc("exec GetLoadData 62,'" + id + "',''");
                DataTable dt = ds.Tables[0];

                DataRow r = dt.Rows[0];

                txtcomment.Text = r["CmtContent"].ToString();

                if (ds.Tables[3].Rows.Count > 0) dtgcomment.DataSource = ds.Tables[3].DefaultView.ToTable(false, "Stage", "English", "Vietnamese");
                else dtgcomment.Hide();

                txtowner.Text = r["CmtOwner"].ToString();
                txtdept.Text = r["DeptCode"].ToString();
                txtcommenttype.Text = r["CommentsType"].ToString();
                txtstatus.Text = r["CmtStatus"].ToString();

                if (r["CmtStatus"].ToString() == "Resolved")
                {
                    chkresolve.Checked = true;
                    chkresolve.Enabled = false;
                }

                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    ListViewItem it = new ListViewItem();
                    it.Text = row[2].ToString().Split('/')[0];
                    it.SubItems.Add(row[2].ToString().Split('/')[1]);

                    lstread.Items.Add(it);
                }

                foreach (DataRow row in ds.Tables[2].Rows)
                {
                    ListViewItem it = new ListViewItem();
                    it.Text = "Open";
                    it.Font = new Font("Arial", 10, FontStyle.Bold | FontStyle.Underline);
                    it.ForeColor = Color.DarkBlue;

                    it.SubItems.Add(row[2].ToString());

                    lstdocument.Items.Add(it);
                }

                if (Temp.User == txtowner.Text)
                {
                    //chkresolve.Enabled = true;
                    btdeletecmt.Enabled = true;
                }
                else
                {
                    chkresolve.Enabled = false;
                    btdeletecmt.Enabled = false;
                }

                LoadTreeNode();
            }
            catch { }
        }
        private void LoadTreeNode()
        {
            try
            {
                history.Nodes.Clear();
                if (Data.Rows.Count > 0) Data.Rows.Clear();

                Data = kn.Doc("exec SmartLineLoadData 3,'" + id + "',''").Tables[0];

                List<Item> ls = new List<Item>();
                foreach (DataRow dr in Data.Rows)
                {
                    string pr = dr["ParentID"].ToString();
                    string cmt = dr["CmtID"].ToString();
                    bool max = dr["MaxCount"].ToString() == "Max" ? true : false;
                    if (pr == id)
                    {
                        TreeNode tn = TN(dr);

                        history.Nodes.Add(tn);

                        ls.Add(new Item
                        {
                            ID = cmt,
                            TN = tn
                        });

                        if (max)
                        {
                            tn.ForeColor = Color.SeaGreen;
                            tn.ExpandAll();
                        }
                    }
                    else
                    {
                        if (ls.Exists(s => s.ID == pr))
                        {
                            TreeNode tn1 = ls.Find(s => s.ID == pr).TN;
                            TreeNode tn2 = TN(dr);

                            tn1.Nodes.Add(tn2);

                            ls.Add(new Item
                            {
                                ID = cmt,
                                TN = tn2
                            });

                            if (max)
                            {
                                tn2.ForeColor = Color.SeaGreen;
                                //tn1.ExpandAll();
                                tn2.EnsureVisible();
                            }
                        }
                        else
                        {
                            TreeNode tn = TN(dr);

                            history.Nodes.Add(tn);

                            ls.Add(new Item
                            {
                                ID = cmt,
                                TN = tn
                            });

                            if (max)
                            {
                                tn.ForeColor = Color.SeaGreen;
                                tn.ExpandAll();
                            }
                        }
                    }
                }

                TreeNode TN(DataRow dr)
                {
                    TreeNode tn = new TreeNode();

                    string ng = dr["CmtID"].ToString().Substring(0, 6); ng = ng.Insert(2, "-"); ng = ng.Insert(5, "-");
                    string gi = dr["CmtID"].ToString().Substring(6, 6); gi = gi.Insert(2, ":"); gi = gi.Insert(5, ":");

                    tn.Text = ng + " " + gi + "  [" + dr["DeptCode"] + "]" + dr["ReplyOwner"] + " : " + dr["ReplyContent"];

                    return tn;
                }
            }
            catch { }
        }
        private void btsubmit_Click(object sender, EventArgs e)
        {
            if (txtreplycontent.Text != "")
            {
                if (parentid == "") parentid = id;
                string cmtid = DateTime.Now.ToString("yyMMddHHmmss");
                string ch = "insert into SampleCommentReplyManager values ('" + id + "','" + parentid + "','" + cmtid + "',N'" + txtreplycontent.Text + "','" + Temp.User + "'," +
                                "'" + ads + "','" + Temp.Dept + "',getdate()) \n delete from SampleCommentRead where CmtID = '" + id + "'";

                kn.Ghi(ch);
                if (kn.ErrorMessage == "")
                {
                    parentid = "";
                    txtreply.Text = "";
                    txtreplycontent.Text = "";

                    node = null;

                    LoadTreeNode();
                }
                else MessageBox.Show(kn.ErrorMessage);
            }
        }

        private void history_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                node = e.Node;

                string[] vl = node.Text.Split('[');

                parentid = vl[0].Replace("-", "").Replace(":", "").Replace(" ", "");
            }
            catch { }
        }

        private void replyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (node != null) txtreply.Text = node.Text;
        }

        private void history_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //e.Node.p
        }

        private void lstdocument_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

        }

        private void lstdocument_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void collaspeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            history.CollapseAll();
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            history.ExpandAll();
        }

        private void frmshowcomment_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

        private void chkresolve_CheckedChanged(object sender, EventArgs e)
        {
            kn.Ghi("update SampleCommentManager set CmtStatus = 'Resolved' where CmtID = '" + id + "'");
            chkresolve.Checked = true;
            chkresolve.Enabled = false;
        }

        private void btdeletecmt_Click(object sender, EventArgs e)
        {
            kn.Ghi("delete from SampleCommentManager where CmtID = '" + id + "'");

            if (kn.ErrorMessage == "")
            {
                frm.LoadComments(docno);

                Close();
            }
        }

        private void lstdocument_Click(object sender, EventArgs e)
        {
            Process.Start(lstdocument.FocusedItem.SubItems[1].Text);
        }

        private void history_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                node = e.Node;
                txtreply.Text = node.Text;

                string[] vl = node.Text.Split('[');

                parentid = vl[0].Replace("-", "").Replace(":", "").Replace(" ", "");
            }
            catch { }
        }
    }
    class Item
    {
        public string ID { get; set; }
        public TreeNode TN { get; set; }
    }
}
