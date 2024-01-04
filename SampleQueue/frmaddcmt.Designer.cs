
namespace SampleQueue
{
    partial class frmaddcmt
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtgcmtcontent = new System.Windows.Forms.DataGridView();
            this.txtcmtcontent = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btload = new System.Windows.Forms.Button();
            this.chkgrid = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.picture = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkcustomer = new System.Windows.Forms.CheckBox();
            this.chk = new System.Windows.Forms.CheckBox();
            this.btaddfile = new System.Windows.Forms.Button();
            this.dtgfile = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btaddcomment = new System.Windows.Forms.Button();
            this.lbinfor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgbophan = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbid = new System.Windows.Forms.ComboBox();
            this.cmbdept = new System.Windows.Forms.ComboBox();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgcmtcontent)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgfile)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgbophan)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtgcmtcontent);
            this.groupBox1.Controls.Add(this.txtcmtcontent);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(1130, 262);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Comment Content : ";
            // 
            // dtgcmtcontent
            // 
            this.dtgcmtcontent.AllowUserToDeleteRows = false;
            this.dtgcmtcontent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgcmtcontent.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgcmtcontent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgcmtcontent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column3,
            this.Column4});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgcmtcontent.DefaultCellStyle = dataGridViewCellStyle1;
            this.dtgcmtcontent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgcmtcontent.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgcmtcontent.Location = new System.Drawing.Point(92, 15);
            this.dtgcmtcontent.Name = "dtgcmtcontent";
            this.dtgcmtcontent.RowHeadersWidth = 5;
            this.dtgcmtcontent.Size = new System.Drawing.Size(1036, 245);
            this.dtgcmtcontent.TabIndex = 3;
            // 
            // txtcmtcontent
            // 
            this.txtcmtcontent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtcmtcontent.Location = new System.Drawing.Point(92, 15);
            this.txtcmtcontent.Multiline = true;
            this.txtcmtcontent.Name = "txtcmtcontent";
            this.txtcmtcontent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtcmtcontent.Size = new System.Drawing.Size(1036, 245);
            this.txtcmtcontent.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btload);
            this.panel2.Controls.Add(this.chkgrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(2, 15);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(90, 245);
            this.panel2.TabIndex = 1;
            // 
            // btload
            // 
            this.btload.Location = new System.Drawing.Point(10, 136);
            this.btload.Name = "btload";
            this.btload.Size = new System.Drawing.Size(75, 23);
            this.btload.TabIndex = 1;
            this.btload.Text = "Load";
            this.btload.UseVisualStyleBackColor = true;
            this.btload.Click += new System.EventHandler(this.btload_Click);
            // 
            // chkgrid
            // 
            this.chkgrid.AutoSize = true;
            this.chkgrid.Location = new System.Drawing.Point(10, 112);
            this.chkgrid.Name = "chkgrid";
            this.chkgrid.Size = new System.Drawing.Size(68, 17);
            this.chkgrid.TabIndex = 0;
            this.chkgrid.Text = "GridView";
            this.chkgrid.UseVisualStyleBackColor = true;
            this.chkgrid.CheckedChanged += new System.EventHandler(this.chkgrid_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 262);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1130, 295);
            this.panel1.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.chkcustomer);
            this.groupBox3.Controls.Add(this.chk);
            this.groupBox3.Controls.Add(this.btaddfile);
            this.groupBox3.Controls.Add(this.dtgfile);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(200, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(930, 295);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Attachments : ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.picture);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox4.Location = new System.Drawing.Point(425, 15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(503, 278);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Picture : ";
            // 
            // picture
            // 
            this.picture.ContextMenuStrip = this.contextMenuStrip1;
            this.picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picture.Location = new System.Drawing.Point(3, 16);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(497, 259);
            this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture.TabIndex = 4;
            this.picture.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteImageToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 26);
            // 
            // pasteImageToolStripMenuItem
            // 
            this.pasteImageToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.pasteImageToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.pasteImageToolStripMenuItem.Image = global::SampleQueue.Properties.Resources.icons8_paste;
            this.pasteImageToolStripMenuItem.Name = "pasteImageToolStripMenuItem";
            this.pasteImageToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.pasteImageToolStripMenuItem.Text = "Paste Image";
            this.pasteImageToolStripMenuItem.Click += new System.EventHandler(this.pasteImageToolStripMenuItem_Click);
            // 
            // chkcustomer
            // 
            this.chkcustomer.AutoSize = true;
            this.chkcustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcustomer.ForeColor = System.Drawing.Color.Red;
            this.chkcustomer.Location = new System.Drawing.Point(112, 20);
            this.chkcustomer.Margin = new System.Windows.Forms.Padding(2);
            this.chkcustomer.Name = "chkcustomer";
            this.chkcustomer.Size = new System.Drawing.Size(167, 17);
            this.chkcustomer.TabIndex = 3;
            this.chkcustomer.Text = "Comments from Customer";
            this.chkcustomer.UseVisualStyleBackColor = true;
            // 
            // chk
            // 
            this.chk.AutoSize = true;
            this.chk.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk.ForeColor = System.Drawing.Color.Blue;
            this.chk.Location = new System.Drawing.Point(289, 20);
            this.chk.Margin = new System.Windows.Forms.Padding(2);
            this.chk.Name = "chk";
            this.chk.Size = new System.Drawing.Size(105, 17);
            this.chk.TabIndex = 3;
            this.chk.Text = "Wait for solve";
            this.chk.UseVisualStyleBackColor = true;
            // 
            // btaddfile
            // 
            this.btaddfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btaddfile.ForeColor = System.Drawing.Color.Blue;
            this.btaddfile.Location = new System.Drawing.Point(5, 18);
            this.btaddfile.Margin = new System.Windows.Forms.Padding(2);
            this.btaddfile.Name = "btaddfile";
            this.btaddfile.Size = new System.Drawing.Size(93, 19);
            this.btaddfile.TabIndex = 2;
            this.btaddfile.Text = "ADD FILE";
            this.btaddfile.UseVisualStyleBackColor = true;
            this.btaddfile.Click += new System.EventHandler(this.btaddfile_Click);
            // 
            // dtgfile
            // 
            this.dtgfile.AllowUserToAddRows = false;
            this.dtgfile.AllowUserToDeleteRows = false;
            this.dtgfile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgfile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2});
            this.dtgfile.Location = new System.Drawing.Point(4, 41);
            this.dtgfile.Margin = new System.Windows.Forms.Padding(2);
            this.dtgfile.Name = "dtgfile";
            this.dtgfile.ReadOnly = true;
            this.dtgfile.RowHeadersWidth = 10;
            this.dtgfile.RowTemplate.Height = 24;
            this.dtgfile.Size = new System.Drawing.Size(416, 244);
            this.dtgfile.TabIndex = 1;
            this.dtgfile.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgfile_CellContentClick);
            this.dtgfile.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgfile_CellDoubleClick);
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "FilePath";
            this.Column2.HeaderText = "File Path";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 400;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btaddcomment);
            this.groupBox2.Controls.Add(this.lbinfor);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtgbophan);
            this.groupBox2.Controls.Add(this.cmbid);
            this.groupBox2.Controls.Add(this.cmbdept);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(200, 295);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Partner : ";
            // 
            // btaddcomment
            // 
            this.btaddcomment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btaddcomment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btaddcomment.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btaddcomment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btaddcomment.Location = new System.Drawing.Point(2, 261);
            this.btaddcomment.Margin = new System.Windows.Forms.Padding(2);
            this.btaddcomment.Name = "btaddcomment";
            this.btaddcomment.Size = new System.Drawing.Size(196, 32);
            this.btaddcomment.TabIndex = 2;
            this.btaddcomment.Text = "ADD COMMENT";
            this.btaddcomment.UseVisualStyleBackColor = false;
            this.btaddcomment.Click += new System.EventHandler(this.btaddcomment_Click);
            // 
            // lbinfor
            // 
            this.lbinfor.AutoSize = true;
            this.lbinfor.Location = new System.Drawing.Point(10, 237);
            this.lbinfor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbinfor.Name = "lbinfor";
            this.lbinfor.Size = new System.Drawing.Size(0, 13);
            this.lbinfor.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 207);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID : ";
            // 
            // dtgbophan
            // 
            this.dtgbophan.AllowUserToAddRows = false;
            this.dtgbophan.AllowUserToDeleteRows = false;
            this.dtgbophan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgbophan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dtgbophan.Location = new System.Drawing.Point(10, 41);
            this.dtgbophan.Margin = new System.Windows.Forms.Padding(2);
            this.dtgbophan.Name = "dtgbophan";
            this.dtgbophan.ReadOnly = true;
            this.dtgbophan.RowHeadersWidth = 10;
            this.dtgbophan.RowTemplate.Height = 24;
            this.dtgbophan.Size = new System.Drawing.Size(177, 155);
            this.dtgbophan.TabIndex = 1;
            this.dtgbophan.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgbophan_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "BoPhan";
            this.Column1.HeaderText = "Department";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // cmbid
            // 
            this.cmbid.FormattingEnabled = true;
            this.cmbid.Location = new System.Drawing.Point(39, 202);
            this.cmbid.Margin = new System.Windows.Forms.Padding(2);
            this.cmbid.Name = "cmbid";
            this.cmbid.Size = new System.Drawing.Size(149, 21);
            this.cmbid.TabIndex = 0;
            this.cmbid.TextChanged += new System.EventHandler(this.cmbid_TextChanged);
            this.cmbid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbid_KeyPress);
            this.cmbid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbid_KeyUp);
            // 
            // cmbdept
            // 
            this.cmbdept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbdept.FormattingEnabled = true;
            this.cmbdept.Location = new System.Drawing.Point(9, 17);
            this.cmbdept.Margin = new System.Windows.Forms.Padding(2);
            this.cmbdept.Name = "cmbdept";
            this.cmbdept.Size = new System.Drawing.Size(179, 21);
            this.cmbdept.TabIndex = 0;
            this.cmbdept.SelectedIndexChanged += new System.EventHandler(this.cmbdept_SelectedIndexChanged);
            this.cmbdept.TextChanged += new System.EventHandler(this.cmbdept_TextChanged);
            // 
            // Column5
            // 
            this.Column5.FillWeight = 22.84264F;
            this.Column5.HeaderText = "Stage";
            this.Column5.Name = "Column5";
            // 
            // Column3
            // 
            this.Column3.FillWeight = 138.5787F;
            this.Column3.HeaderText = "English";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.FillWeight = 138.5787F;
            this.Column4.HeaderText = "Vietnamese";
            this.Column4.Name = "Column4";
            // 
            // frmaddcmt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 557);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmaddcmt";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ADD COMMENT";
            this.Load += new System.EventHandler(this.frmaddcmt_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgcmtcontent)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgfile)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgbophan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbdept;
        private System.Windows.Forms.DataGridView dtgbophan;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dtgfile;
        private System.Windows.Forms.Button btaddfile;
        private System.Windows.Forms.Button btaddcomment;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.CheckBox chk;
        private System.Windows.Forms.CheckBox chkcustomer;
        private System.Windows.Forms.ComboBox cmbid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbinfor;
        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pasteImageToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtcmtcontent;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btload;
        private System.Windows.Forms.CheckBox chkgrid;
        private System.Windows.Forms.DataGridView dtgcmtcontent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}