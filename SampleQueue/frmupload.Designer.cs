namespace SampleQueue
{
    partial class frmupload
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dtgsewing = new System.Windows.Forms.DataGridView();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.lbrow1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.processbar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.lbprocess1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btupload = new System.Windows.Forms.Button();
            this.cmbsewer = new System.Windows.Forms.ComboBox();
            this.cmbshiptomer = new System.Windows.Forms.ComboBox();
            this.cmbfinish = new System.Windows.Forms.ComboBox();
            this.cmbstart = new System.Windows.Forms.ComboBox();
            this.cmbid = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtgcutting = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbrow2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.processbar2 = new System.Windows.Forms.ToolStripProgressBar();
            this.lbprocess2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btupload2 = new System.Windows.Forms.Button();
            this.cmbcuttingfinish = new System.Windows.Forms.ComboBox();
            this.cmbcuttingstart = new System.Windows.Forms.ComboBox();
            this.cmbid2 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbcuttingremark = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgsewing)).BeginInit();
            this.statusStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgcutting)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1257, 619);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1249, 593);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SEWING AND SHIP TO MER DATE";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dtgsewing);
            this.panel4.Controls.Add(this.statusStrip2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(188, 3);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1058, 587);
            this.panel4.TabIndex = 102;
            // 
            // dtgsewing
            // 
            this.dtgsewing.AllowUserToAddRows = false;
            this.dtgsewing.AllowUserToDeleteRows = false;
            this.dtgsewing.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgsewing.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgsewing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgsewing.DefaultCellStyle = dataGridViewCellStyle6;
            this.dtgsewing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgsewing.Location = new System.Drawing.Point(0, 0);
            this.dtgsewing.Margin = new System.Windows.Forms.Padding(2);
            this.dtgsewing.Name = "dtgsewing";
            this.dtgsewing.ReadOnly = true;
            this.dtgsewing.RowHeadersWidth = 5;
            this.dtgsewing.RowTemplate.Height = 24;
            this.dtgsewing.Size = new System.Drawing.Size(1058, 565);
            this.dtgsewing.TabIndex = 2;
            // 
            // statusStrip2
            // 
            this.statusStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbrow1,
            this.processbar1,
            this.lbprocess1});
            this.statusStrip2.Location = new System.Drawing.Point(0, 565);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip2.Size = new System.Drawing.Size(1058, 22);
            this.statusStrip2.TabIndex = 1;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // lbrow1
            // 
            this.lbrow1.Name = "lbrow1";
            this.lbrow1.Size = new System.Drawing.Size(41, 17);
            this.lbrow1.Text = "Rows :";
            // 
            // processbar1
            // 
            this.processbar1.MarqueeAnimationSpeed = 10;
            this.processbar1.Name = "processbar1";
            this.processbar1.Size = new System.Drawing.Size(75, 16);
            this.processbar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // lbprocess1
            // 
            this.lbprocess1.Name = "lbprocess1";
            this.lbprocess1.Size = new System.Drawing.Size(13, 17);
            this.lbprocess1.Text = "  ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.btupload);
            this.panel1.Controls.Add(this.cmbsewer);
            this.panel1.Controls.Add(this.cmbshiptomer);
            this.panel1.Controls.Add(this.cmbfinish);
            this.panel1.Controls.Add(this.cmbstart);
            this.panel1.Controls.Add(this.cmbid);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 587);
            this.panel1.TabIndex = 101;
            // 
            // btupload
            // 
            this.btupload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btupload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btupload.ForeColor = System.Drawing.Color.Blue;
            this.btupload.Location = new System.Drawing.Point(12, 310);
            this.btupload.Margin = new System.Windows.Forms.Padding(2);
            this.btupload.Name = "btupload";
            this.btupload.Size = new System.Drawing.Size(154, 33);
            this.btupload.TabIndex = 2;
            this.btupload.Text = "UPLOAD";
            this.btupload.UseVisualStyleBackColor = false;
            this.btupload.Click += new System.EventHandler(this.btupload_Click);
            // 
            // cmbsewer
            // 
            this.cmbsewer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbsewer.FormattingEnabled = true;
            this.cmbsewer.Location = new System.Drawing.Point(12, 251);
            this.cmbsewer.Margin = new System.Windows.Forms.Padding(2);
            this.cmbsewer.Name = "cmbsewer";
            this.cmbsewer.Size = new System.Drawing.Size(156, 21);
            this.cmbsewer.TabIndex = 1;
            // 
            // cmbshiptomer
            // 
            this.cmbshiptomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbshiptomer.FormattingEnabled = true;
            this.cmbshiptomer.Location = new System.Drawing.Point(12, 191);
            this.cmbshiptomer.Margin = new System.Windows.Forms.Padding(2);
            this.cmbshiptomer.Name = "cmbshiptomer";
            this.cmbshiptomer.Size = new System.Drawing.Size(156, 21);
            this.cmbshiptomer.TabIndex = 1;
            // 
            // cmbfinish
            // 
            this.cmbfinish.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbfinish.FormattingEnabled = true;
            this.cmbfinish.Location = new System.Drawing.Point(11, 136);
            this.cmbfinish.Margin = new System.Windows.Forms.Padding(2);
            this.cmbfinish.Name = "cmbfinish";
            this.cmbfinish.Size = new System.Drawing.Size(156, 21);
            this.cmbfinish.TabIndex = 1;
            // 
            // cmbstart
            // 
            this.cmbstart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbstart.FormattingEnabled = true;
            this.cmbstart.Location = new System.Drawing.Point(12, 80);
            this.cmbstart.Margin = new System.Windows.Forms.Padding(2);
            this.cmbstart.Name = "cmbstart";
            this.cmbstart.Size = new System.Drawing.Size(156, 21);
            this.cmbstart.TabIndex = 1;
            // 
            // cmbid
            // 
            this.cmbid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbid.FormattingEnabled = true;
            this.cmbid.Location = new System.Drawing.Point(12, 28);
            this.cmbid.Margin = new System.Windows.Forms.Padding(2);
            this.cmbid.Name = "cmbid";
            this.cmbid.Size = new System.Drawing.Size(156, 21);
            this.cmbid.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(10, 234);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Sewer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(10, 174);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Ship to Mer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(9, 119);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Finish Sewing";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(10, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Start Sewing";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sample ID";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1249, 593);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CUTTING DATE";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dtgcutting);
            this.panel3.Controls.Add(this.statusStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(188, 3);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1058, 587);
            this.panel3.TabIndex = 102;
            // 
            // dtgcutting
            // 
            this.dtgcutting.AllowUserToAddRows = false;
            this.dtgcutting.AllowUserToDeleteRows = false;
            this.dtgcutting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgcutting.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dtgcutting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgcutting.DefaultCellStyle = dataGridViewCellStyle8;
            this.dtgcutting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgcutting.Location = new System.Drawing.Point(0, 0);
            this.dtgcutting.Margin = new System.Windows.Forms.Padding(2);
            this.dtgcutting.Name = "dtgcutting";
            this.dtgcutting.ReadOnly = true;
            this.dtgcutting.RowHeadersWidth = 5;
            this.dtgcutting.RowTemplate.Height = 24;
            this.dtgcutting.Size = new System.Drawing.Size(1058, 565);
            this.dtgcutting.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbrow2,
            this.processbar2,
            this.lbprocess2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 565);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1058, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbrow2
            // 
            this.lbrow2.Name = "lbrow2";
            this.lbrow2.Size = new System.Drawing.Size(41, 17);
            this.lbrow2.Text = "Rows :";
            // 
            // processbar2
            // 
            this.processbar2.MarqueeAnimationSpeed = 10;
            this.processbar2.Name = "processbar2";
            this.processbar2.Size = new System.Drawing.Size(75, 16);
            this.processbar2.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // lbprocess2
            // 
            this.lbprocess2.Name = "lbprocess2";
            this.lbprocess2.Size = new System.Drawing.Size(13, 17);
            this.lbprocess2.Text = "  ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Info;
            this.panel2.Controls.Add(this.btupload2);
            this.panel2.Controls.Add(this.cmbcuttingremark);
            this.panel2.Controls.Add(this.cmbcuttingfinish);
            this.panel2.Controls.Add(this.cmbcuttingstart);
            this.panel2.Controls.Add(this.cmbid2);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(185, 587);
            this.panel2.TabIndex = 101;
            // 
            // btupload2
            // 
            this.btupload2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btupload2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btupload2.ForeColor = System.Drawing.Color.Blue;
            this.btupload2.Location = new System.Drawing.Point(14, 248);
            this.btupload2.Margin = new System.Windows.Forms.Padding(2);
            this.btupload2.Name = "btupload2";
            this.btupload2.Size = new System.Drawing.Size(154, 33);
            this.btupload2.TabIndex = 2;
            this.btupload2.Text = "UPLOAD";
            this.btupload2.UseVisualStyleBackColor = false;
            this.btupload2.Click += new System.EventHandler(this.btupload2_Click);
            // 
            // cmbcuttingfinish
            // 
            this.cmbcuttingfinish.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcuttingfinish.FormattingEnabled = true;
            this.cmbcuttingfinish.Location = new System.Drawing.Point(11, 136);
            this.cmbcuttingfinish.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcuttingfinish.Name = "cmbcuttingfinish";
            this.cmbcuttingfinish.Size = new System.Drawing.Size(156, 21);
            this.cmbcuttingfinish.TabIndex = 1;
            // 
            // cmbcuttingstart
            // 
            this.cmbcuttingstart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcuttingstart.FormattingEnabled = true;
            this.cmbcuttingstart.Location = new System.Drawing.Point(12, 80);
            this.cmbcuttingstart.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcuttingstart.Name = "cmbcuttingstart";
            this.cmbcuttingstart.Size = new System.Drawing.Size(156, 21);
            this.cmbcuttingstart.TabIndex = 1;
            // 
            // cmbid2
            // 
            this.cmbid2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbid2.FormattingEnabled = true;
            this.cmbid2.Location = new System.Drawing.Point(12, 28);
            this.cmbid2.Margin = new System.Windows.Forms.Padding(2);
            this.cmbid2.Name = "cmbid2";
            this.cmbid2.Size = new System.Drawing.Size(156, 21);
            this.cmbid2.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(9, 119);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Finish Cutting";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(10, 63);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Start Cutting";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(10, 11);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Sample ID";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // worker
            // 
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_DoWork);
            this.worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(10, 178);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Remark";
            // 
            // cmbcuttingremark
            // 
            this.cmbcuttingremark.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcuttingremark.FormattingEnabled = true;
            this.cmbcuttingremark.Location = new System.Drawing.Point(12, 195);
            this.cmbcuttingremark.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcuttingremark.Name = "cmbcuttingremark";
            this.cmbcuttingremark.Size = new System.Drawing.Size(156, 21);
            this.cmbcuttingremark.TabIndex = 1;
            // 
            // frmupload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 619);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmupload";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SAMPLE ROOM UPLOAD DATA";
            this.Load += new System.EventHandler(this.frmupload_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmupload_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmupload_DragEnter);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgsewing)).EndInit();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgcutting)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btupload;
        private System.Windows.Forms.ComboBox cmbsewer;
        private System.Windows.Forms.ComboBox cmbshiptomer;
        private System.Windows.Forms.ComboBox cmbfinish;
        private System.Windows.Forms.ComboBox cmbstart;
        private System.Windows.Forms.ComboBox cmbid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btupload2;
        private System.Windows.Forms.ComboBox cmbcuttingfinish;
        private System.Windows.Forms.ComboBox cmbcuttingstart;
        private System.Windows.Forms.ComboBox cmbid2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dtgsewing;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel lbrow1;
        private System.Windows.Forms.ToolStripProgressBar processbar1;
        private System.Windows.Forms.ToolStripStatusLabel lbprocess1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dtgcutting;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbrow2;
        private System.Windows.Forms.ToolStripProgressBar processbar2;
        private System.Windows.Forms.ToolStripStatusLabel lbprocess2;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker worker;
        private System.Windows.Forms.ComboBox cmbcuttingremark;
        private System.Windows.Forms.Label label6;
    }
}