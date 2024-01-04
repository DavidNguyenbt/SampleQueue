namespace SampleQueue
{
    partial class frmsqid
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbrows = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbselect = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbprint = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbaddprinter = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unselectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.setStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setLocationIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.removeRFIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbrows,
            this.lbselect,
            this.lbprint,
            this.lbaddprinter,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 638);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1246, 24);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbrows
            // 
            this.lbrows.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbrows.Name = "lbrows";
            this.lbrows.Size = new System.Drawing.Size(44, 19);
            this.lbrows.Text = "Rows : ";
            // 
            // lbselect
            // 
            this.lbselect.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lbselect.ForeColor = System.Drawing.Color.Yellow;
            this.lbselect.Name = "lbselect";
            this.lbselect.Size = new System.Drawing.Size(60, 19);
            this.lbselect.Text = "Selected : ";
            this.lbselect.Visible = false;
            // 
            // lbprint
            // 
            this.lbprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lbprint.Font = new System.Drawing.Font("Segoe UI", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lbprint.ForeColor = System.Drawing.Color.Blue;
            this.lbprint.Name = "lbprint";
            this.lbprint.Size = new System.Drawing.Size(149, 19);
            this.lbprint.Text = "PRINT THE BARCODE";
            this.lbprint.Visible = false;
            this.lbprint.Click += new System.EventHandler(this.lbprint_Click);
            // 
            // lbaddprinter
            // 
            this.lbaddprinter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lbaddprinter.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lbaddprinter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lbaddprinter.Name = "lbaddprinter";
            this.lbaddprinter.Size = new System.Drawing.Size(117, 19);
            this.lbaddprinter.Text = "ADD INTO PRINTER";
            this.lbaddprinter.Visible = false;
            this.lbaddprinter.Click += new System.EventHandler(this.lbaddprinter_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Green;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(74, 19);
            this.toolStripStatusLabel1.Text = "RESET DATA";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.menu;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 5;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1246, 638);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectToolStripMenuItem,
            this.unselectToolStripMenuItem,
            this.toolStripMenuItem1,
            this.setStatusToolStripMenuItem,
            this.setLocationToolStripMenuItem,
            this.setLocationIDToolStripMenuItem,
            this.toolStripMenuItem2,
            this.removeRFIDToolStripMenuItem});
            this.menu.Name = "contextMenuStrip1";
            this.menu.Size = new System.Drawing.Size(160, 148);
            this.menu.Opening += new System.ComponentModel.CancelEventHandler(this.menu_Opening);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.selectToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.selectToolStripMenuItem.Text = "Select ";
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // unselectToolStripMenuItem
            // 
            this.unselectToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.unselectToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.unselectToolStripMenuItem.Name = "unselectToolStripMenuItem";
            this.unselectToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.unselectToolStripMenuItem.Text = "Unselect";
            this.unselectToolStripMenuItem.Click += new System.EventHandler(this.unselectToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(156, 6);
            // 
            // setStatusToolStripMenuItem
            // 
            this.setStatusToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.setStatusToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.setStatusToolStripMenuItem.Name = "setStatusToolStripMenuItem";
            this.setStatusToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.setStatusToolStripMenuItem.Text = "Go Shipping";
            this.setStatusToolStripMenuItem.Click += new System.EventHandler(this.setStatusToolStripMenuItem_Click);
            // 
            // setLocationToolStripMenuItem
            // 
            this.setLocationToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.setLocationToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.setLocationToolStripMenuItem.Name = "setLocationToolStripMenuItem";
            this.setLocationToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.setLocationToolStripMenuItem.Text = "Set Location";
            this.setLocationToolStripMenuItem.Click += new System.EventHandler(this.setLocationToolStripMenuItem_Click);
            // 
            // setLocationIDToolStripMenuItem
            // 
            this.setLocationIDToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.setLocationIDToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.setLocationIDToolStripMenuItem.Name = "setLocationIDToolStripMenuItem";
            this.setLocationIDToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.setLocationIDToolStripMenuItem.Text = "Set Location ID";
            this.setLocationIDToolStripMenuItem.Click += new System.EventHandler(this.setLocationIDToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(156, 6);
            // 
            // removeRFIDToolStripMenuItem
            // 
            this.removeRFIDToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.removeRFIDToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.removeRFIDToolStripMenuItem.Name = "removeRFIDToolStripMenuItem";
            this.removeRFIDToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.removeRFIDToolStripMenuItem.Text = "Remove RFID";
            this.removeRFIDToolStripMenuItem.Click += new System.EventHandler(this.removeRFIDToolStripMenuItem_Click);
            // 
            // frmsqid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 662);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmsqid";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SAMPLE ID";
            this.Load += new System.EventHandler(this.frmsqid_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbrows;
        private System.Windows.Forms.ToolStripStatusLabel lbselect;
        private System.Windows.Forms.ToolStripStatusLabel lbprint;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unselectToolStripMenuItem;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem setStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lbaddprinter;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem removeRFIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setLocationIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}