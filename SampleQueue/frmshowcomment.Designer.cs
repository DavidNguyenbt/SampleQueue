
namespace SampleQueue
{
    partial class frmshowcomment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.history = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.replyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collaspeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstdocument = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lstread = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cl3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtreplycontent = new System.Windows.Forms.TextBox();
            this.btsubmit = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtowner = new System.Windows.Forms.TextBox();
            this.txtdept = new System.Windows.Forms.TextBox();
            this.txtcommenttype = new System.Windows.Forms.TextBox();
            this.txtstatus = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkresolve = new System.Windows.Forms.CheckBox();
            this.txtreply = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btdeletecmt = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtgcomment = new System.Windows.Forms.DataGridView();
            this.txtcomment = new System.Windows.Forms.TextBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgcomment)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Comments : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(14, 282);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Owner : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(14, 303);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Department : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(14, 327);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Comment Type : ";
            // 
            // history
            // 
            this.history.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.history.ContextMenuStrip = this.contextMenuStrip1;
            this.history.LineColor = System.Drawing.Color.Blue;
            this.history.Location = new System.Drawing.Point(389, 303);
            this.history.Margin = new System.Windows.Forms.Padding(2);
            this.history.Name = "history";
            this.history.Size = new System.Drawing.Size(724, 243);
            this.history.TabIndex = 2;
            this.history.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.history_AfterSelect);
            this.history.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.history_NodeMouseClick);
            this.history.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.history_NodeMouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.replyToolStripMenuItem,
            this.collaspeAllToolStripMenuItem,
            this.expandAllToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 70);
            // 
            // replyToolStripMenuItem
            // 
            this.replyToolStripMenuItem.Name = "replyToolStripMenuItem";
            this.replyToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.replyToolStripMenuItem.Text = "Reply";
            this.replyToolStripMenuItem.Click += new System.EventHandler(this.replyToolStripMenuItem_Click);
            // 
            // collaspeAllToolStripMenuItem
            // 
            this.collaspeAllToolStripMenuItem.Name = "collaspeAllToolStripMenuItem";
            this.collaspeAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.collaspeAllToolStripMenuItem.Text = "Collapse All";
            this.collaspeAllToolStripMenuItem.Click += new System.EventHandler(this.collaspeAllToolStripMenuItem_Click);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.expandAllToolStripMenuItem.Text = "Expand All";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // lstdocument
            // 
            this.lstdocument.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstdocument.HideSelection = false;
            this.lstdocument.Location = new System.Drawing.Point(18, 687);
            this.lstdocument.Margin = new System.Windows.Forms.Padding(2);
            this.lstdocument.Name = "lstdocument";
            this.lstdocument.Size = new System.Drawing.Size(1094, 110);
            this.lstdocument.TabIndex = 3;
            this.lstdocument.UseCompatibleStateImageBehavior = false;
            this.lstdocument.View = System.Windows.Forms.View.Details;
            this.lstdocument.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstdocument_ItemSelectionChanged);
            this.lstdocument.SelectedIndexChanged += new System.EventHandler(this.lstdocument_SelectedIndexChanged);
            this.lstdocument.Click += new System.EventHandler(this.lstdocument_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Path";
            this.columnHeader2.Width = 981;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(14, 350);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Status : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(17, 672);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Document : ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(15, 424);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Who Read : ";
            // 
            // lstread
            // 
            this.lstread.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.cl3});
            this.lstread.HideSelection = false;
            this.lstread.Location = new System.Drawing.Point(17, 439);
            this.lstread.Margin = new System.Windows.Forms.Padding(2);
            this.lstread.Name = "lstread";
            this.lstread.Size = new System.Drawing.Size(350, 228);
            this.lstread.TabIndex = 3;
            this.lstread.UseCompatibleStateImageBehavior = false;
            this.lstread.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 250;
            // 
            // cl3
            // 
            this.cl3.Text = "Dept";
            this.cl3.Width = 100;
            // 
            // txtreplycontent
            // 
            this.txtreplycontent.Location = new System.Drawing.Point(388, 629);
            this.txtreplycontent.Margin = new System.Windows.Forms.Padding(2);
            this.txtreplycontent.Multiline = true;
            this.txtreplycontent.Name = "txtreplycontent";
            this.txtreplycontent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtreplycontent.Size = new System.Drawing.Size(626, 38);
            this.txtreplycontent.TabIndex = 1;
            // 
            // btsubmit
            // 
            this.btsubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btsubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btsubmit.ForeColor = System.Drawing.Color.Blue;
            this.btsubmit.Location = new System.Drawing.Point(1018, 629);
            this.btsubmit.Margin = new System.Windows.Forms.Padding(2);
            this.btsubmit.Name = "btsubmit";
            this.btsubmit.Size = new System.Drawing.Size(94, 37);
            this.btsubmit.TabIndex = 5;
            this.btsubmit.Text = "SUBMIT";
            this.btsubmit.UseVisualStyleBackColor = false;
            this.btsubmit.Click += new System.EventHandler(this.btsubmit_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(385, 284);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Reply History : ";
            // 
            // txtowner
            // 
            this.txtowner.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtowner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtowner.Location = new System.Drawing.Point(116, 278);
            this.txtowner.Margin = new System.Windows.Forms.Padding(2);
            this.txtowner.Name = "txtowner";
            this.txtowner.Size = new System.Drawing.Size(152, 19);
            this.txtowner.TabIndex = 8;
            this.txtowner.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtdept
            // 
            this.txtdept.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtdept.Location = new System.Drawing.Point(116, 299);
            this.txtdept.Margin = new System.Windows.Forms.Padding(2);
            this.txtdept.Name = "txtdept";
            this.txtdept.Size = new System.Drawing.Size(152, 19);
            this.txtdept.TabIndex = 8;
            this.txtdept.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtcommenttype
            // 
            this.txtcommenttype.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcommenttype.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtcommenttype.Location = new System.Drawing.Point(116, 322);
            this.txtcommenttype.Margin = new System.Windows.Forms.Padding(2);
            this.txtcommenttype.Name = "txtcommenttype";
            this.txtcommenttype.Size = new System.Drawing.Size(152, 19);
            this.txtcommenttype.TabIndex = 8;
            this.txtcommenttype.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtstatus
            // 
            this.txtstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtstatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtstatus.Location = new System.Drawing.Point(116, 344);
            this.txtstatus.Margin = new System.Windows.Forms.Padding(2);
            this.txtstatus.Name = "txtstatus";
            this.txtstatus.Size = new System.Drawing.Size(152, 19);
            this.txtstatus.TabIndex = 8;
            this.txtstatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(15, 372);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Resolved : ";
            // 
            // chkresolve
            // 
            this.chkresolve.AutoSize = true;
            this.chkresolve.Enabled = false;
            this.chkresolve.Location = new System.Drawing.Point(116, 372);
            this.chkresolve.Margin = new System.Windows.Forms.Padding(2);
            this.chkresolve.Name = "chkresolve";
            this.chkresolve.Size = new System.Drawing.Size(15, 14);
            this.chkresolve.TabIndex = 9;
            this.chkresolve.UseVisualStyleBackColor = true;
            this.chkresolve.CheckedChanged += new System.EventHandler(this.chkresolve_CheckedChanged);
            // 
            // txtreply
            // 
            this.txtreply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtreply.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtreply.Location = new System.Drawing.Point(388, 573);
            this.txtreply.Margin = new System.Windows.Forms.Padding(2);
            this.txtreply.Multiline = true;
            this.txtreply.Name = "txtreply";
            this.txtreply.ReadOnly = true;
            this.txtreply.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtreply.Size = new System.Drawing.Size(724, 28);
            this.txtreply.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(386, 557);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Content : ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(386, 612);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Reply Content : ";
            // 
            // btdeletecmt
            // 
            this.btdeletecmt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btdeletecmt.Enabled = false;
            this.btdeletecmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btdeletecmt.ForeColor = System.Drawing.Color.Red;
            this.btdeletecmt.Location = new System.Drawing.Point(156, 369);
            this.btdeletecmt.Margin = new System.Windows.Forms.Padding(2);
            this.btdeletecmt.Name = "btdeletecmt";
            this.btdeletecmt.Size = new System.Drawing.Size(110, 19);
            this.btdeletecmt.TabIndex = 10;
            this.btdeletecmt.Text = "DELETE";
            this.btdeletecmt.UseVisualStyleBackColor = false;
            this.btdeletecmt.Click += new System.EventHandler(this.btdeletecmt_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtgcomment);
            this.panel1.Controls.Add(this.txtcomment);
            this.panel1.Location = new System.Drawing.Point(13, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1098, 249);
            this.panel1.TabIndex = 11;
            // 
            // dtgcomment
            // 
            this.dtgcomment.AllowUserToAddRows = false;
            this.dtgcomment.AllowUserToDeleteRows = false;
            this.dtgcomment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgcomment.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgcomment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgcomment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgcomment.DefaultCellStyle = dataGridViewCellStyle1;
            this.dtgcomment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgcomment.Location = new System.Drawing.Point(0, 0);
            this.dtgcomment.Name = "dtgcomment";
            this.dtgcomment.ReadOnly = true;
            this.dtgcomment.RowHeadersWidth = 5;
            this.dtgcomment.Size = new System.Drawing.Size(1098, 249);
            this.dtgcomment.TabIndex = 1;
            // 
            // txtcomment
            // 
            this.txtcomment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtcomment.Location = new System.Drawing.Point(0, 0);
            this.txtcomment.Multiline = true;
            this.txtcomment.Name = "txtcomment";
            this.txtcomment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtcomment.Size = new System.Drawing.Size(1098, 249);
            this.txtcomment.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Stage";
            this.Column1.FillWeight = 22.84264F;
            this.Column1.HeaderText = "Stage";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "English";
            this.Column2.FillWeight = 138.5787F;
            this.Column2.HeaderText = "English";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Vietnamese";
            this.Column3.FillWeight = 138.5787F;
            this.Column3.HeaderText = "Vietnamese";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // frmshowcomment
            // 
            this.AcceptButton = this.btsubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1122, 804);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btdeletecmt);
            this.Controls.Add(this.chkresolve);
            this.Controls.Add(this.txtstatus);
            this.Controls.Add(this.txtcommenttype);
            this.Controls.Add(this.txtdept);
            this.Controls.Add(this.txtowner);
            this.Controls.Add(this.btsubmit);
            this.Controls.Add(this.lstread);
            this.Controls.Add(this.lstdocument);
            this.Controls.Add(this.history);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtreplycontent);
            this.Controls.Add(this.txtreply);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmshowcomment";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SHOW COMMENT";
            this.Load += new System.EventHandler(this.frmshowcomment_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmshowcomment_KeyUp);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgcomment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TreeView history;
        private System.Windows.Forms.ListView lstdocument;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem replyToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView lstread;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TextBox txtreplycontent;
        private System.Windows.Forms.Button btsubmit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtowner;
        private System.Windows.Forms.TextBox txtdept;
        private System.Windows.Forms.TextBox txtcommenttype;
        private System.Windows.Forms.TextBox txtstatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkresolve;
        private System.Windows.Forms.ColumnHeader cl3;
        private System.Windows.Forms.TextBox txtreply;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btdeletecmt;
        private System.Windows.Forms.ToolStripMenuItem collaspeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtcomment;
        private System.Windows.Forms.DataGridView dtgcomment;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}