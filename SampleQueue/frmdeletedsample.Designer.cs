namespace SampleQueue
{
    partial class frmdeletedsample
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
            this.dtgdata = new Zuby.ADGV.AdvancedDataGridView();
            this.lbrow = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreSampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgdata)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbrow});
            this.statusStrip1.Location = new System.Drawing.Point(0, 612);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1102, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dtgdata
            // 
            this.dtgdata.AllowUserToAddRows = false;
            this.dtgdata.AllowUserToDeleteRows = false;
            this.dtgdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgdata.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgdata.FilterAndSortEnabled = true;
            this.dtgdata.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dtgdata.Location = new System.Drawing.Point(0, 0);
            this.dtgdata.Name = "dtgdata";
            this.dtgdata.ReadOnly = true;
            this.dtgdata.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtgdata.RowHeadersWidth = 10;
            this.dtgdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgdata.Size = new System.Drawing.Size(1102, 612);
            this.dtgdata.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dtgdata.TabIndex = 1;
            // 
            // lbrow
            // 
            this.lbrow.Name = "lbrow";
            this.lbrow.Size = new System.Drawing.Size(44, 17);
            this.lbrow.Text = "Rows : ";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreSampleToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 26);
            // 
            // restoreSampleToolStripMenuItem
            // 
            this.restoreSampleToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.restoreSampleToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.restoreSampleToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.restoreSampleToolStripMenuItem.Image = global::SampleQueue.Properties.Resources.icons8_change;
            this.restoreSampleToolStripMenuItem.Name = "restoreSampleToolStripMenuItem";
            this.restoreSampleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.restoreSampleToolStripMenuItem.Text = "Restore Sample";
            this.restoreSampleToolStripMenuItem.Click += new System.EventHandler(this.restoreSampleToolStripMenuItem_Click);
            // 
            // frmdeletedsample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 634);
            this.Controls.Add(this.dtgdata);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmdeletedsample";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DELETED SAMPLE";
            this.Load += new System.EventHandler(this.frmdeletedsample_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmdeletedsample_Paint);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgdata)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private Zuby.ADGV.AdvancedDataGridView dtgdata;
        private System.Windows.Forms.ToolStripStatusLabel lbrow;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem restoreSampleToolStripMenuItem;
    }
}