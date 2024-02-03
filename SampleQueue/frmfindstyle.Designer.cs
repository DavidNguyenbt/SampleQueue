
namespace SampleQueue
{
    partial class frmfindstyle
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.oKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtstyle = new System.Windows.Forms.ToolStripTextBox();
            this.btfind = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtrows = new System.Windows.Forms.ToolStripStatusLabel();
            this.dtgdata = new Zuby.ADGV.AdvancedDataGridView();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgdata)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oKToolStripMenuItem,
            this.cancelToolStripMenuItem,
            this.filterColumnToolStripMenuItem,
            this.txtstyle,
            this.btfind});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(928, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // oKToolStripMenuItem
            // 
            this.oKToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.oKToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.oKToolStripMenuItem.Image = global::SampleQueue.Properties.Resources.Play;
            this.oKToolStripMenuItem.Name = "oKToolStripMenuItem";
            this.oKToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.oKToolStripMenuItem.Text = "OK";
            this.oKToolStripMenuItem.Click += new System.EventHandler(this.oKToolStripMenuItem_Click);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cancelToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.cancelToolStripMenuItem.Image = global::SampleQueue.Properties.Resources.Cancel2;
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.cancelToolStripMenuItem.Text = "Cancel";
            this.cancelToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
            // 
            // filterColumnToolStripMenuItem
            // 
            this.filterColumnToolStripMenuItem.Name = "filterColumnToolStripMenuItem";
            this.filterColumnToolStripMenuItem.Size = new System.Drawing.Size(50, 24);
            this.filterColumnToolStripMenuItem.Text = "Style :";
            // 
            // txtstyle
            // 
            this.txtstyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtstyle.Name = "txtstyle";
            this.txtstyle.Size = new System.Drawing.Size(300, 24);
            // 
            // btfind
            // 
            this.btfind.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btfind.ForeColor = System.Drawing.Color.Blue;
            this.btfind.Image = global::SampleQueue.Properties.Resources.icons8_link;
            this.btfind.Name = "btfind";
            this.btfind.Size = new System.Drawing.Size(93, 24);
            this.btfind.Text = "Find Style";
            this.btfind.Click += new System.EventHandler(this.btfind_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtrows});
            this.statusStrip1.Location = new System.Drawing.Point(0, 518);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(928, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtrows
            // 
            this.txtrows.Name = "txtrows";
            this.txtrows.Size = new System.Drawing.Size(44, 17);
            this.txtrows.Text = "Rows : ";
            // 
            // dtgdata
            // 
            this.dtgdata.AllowUserToAddRows = false;
            this.dtgdata.AllowUserToDeleteRows = false;
            this.dtgdata.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtgdata.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgdata.FilterAndSortEnabled = true;
            this.dtgdata.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dtgdata.Location = new System.Drawing.Point(0, 28);
            this.dtgdata.Name = "dtgdata";
            this.dtgdata.ReadOnly = true;
            this.dtgdata.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtgdata.RowHeadersWidth = 10;
            this.dtgdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgdata.Size = new System.Drawing.Size(928, 490);
            this.dtgdata.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.dtgdata.TabIndex = 2;
            // 
            // frmfindstyle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 540);
            this.Controls.Add(this.dtgdata);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmfindstyle";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FIND ORDER STYLE";
            this.Load += new System.EventHandler(this.frmfindstyle_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgdata)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem oKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterColumnToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtrows;
        private System.Windows.Forms.ToolStripTextBox txtstyle;
        private System.Windows.Forms.ToolStripMenuItem btfind;
        private Zuby.ADGV.AdvancedDataGridView dtgdata;
    }
}