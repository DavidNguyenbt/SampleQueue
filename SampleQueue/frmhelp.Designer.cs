
namespace SampleQueue
{
    partial class frmhelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmhelp));
            this.grtip = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.chkshow = new System.Windows.Forms.CheckBox();
            this.txttip = new System.Windows.Forms.TextBox();
            this.lbwelcome = new System.Windows.Forms.Label();
            this.grtip.SuspendLayout();
            this.SuspendLayout();
            // 
            // grtip
            // 
            this.grtip.Controls.Add(this.lbwelcome);
            this.grtip.Controls.Add(this.label1);
            this.grtip.Controls.Add(this.button1);
            this.grtip.Controls.Add(this.chkshow);
            this.grtip.Controls.Add(this.txttip);
            this.grtip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grtip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grtip.ForeColor = System.Drawing.Color.Blue;
            this.grtip.Location = new System.Drawing.Point(0, 0);
            this.grtip.Name = "grtip";
            this.grtip.Size = new System.Drawing.Size(1056, 578);
            this.grtip.TabIndex = 0;
            this.grtip.TabStop = false;
            this.grtip.Text = "Sample Urgent Tip : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(843, 495);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "See more ....";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(894, 522);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 42);
            this.button1.TabIndex = 2;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkshow
            // 
            this.chkshow.AutoSize = true;
            this.chkshow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkshow.Location = new System.Drawing.Point(44, 533);
            this.chkshow.Name = "chkshow";
            this.chkshow.Size = new System.Drawing.Size(155, 21);
            this.chkshow.TabIndex = 1;
            this.chkshow.Text = "Do not show again .";
            this.chkshow.UseVisualStyleBackColor = true;
            // 
            // txttip
            // 
            this.txttip.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txttip.Enabled = false;
            this.txttip.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttip.ForeColor = System.Drawing.Color.Blue;
            this.txttip.Location = new System.Drawing.Point(44, 84);
            this.txttip.Multiline = true;
            this.txttip.Name = "txttip";
            this.txttip.ReadOnly = true;
            this.txttip.Size = new System.Drawing.Size(942, 408);
            this.txttip.TabIndex = 0;
            // 
            // lbwelcome
            // 
            this.lbwelcome.AutoSize = true;
            this.lbwelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbwelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lbwelcome.Location = new System.Drawing.Point(189, 43);
            this.lbwelcome.Name = "lbwelcome";
            this.lbwelcome.Size = new System.Drawing.Size(272, 25);
            this.lbwelcome.TabIndex = 4;
            this.lbwelcome.Text = "Welcome to new function : ";
            // 
            // frmhelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 578);
            this.Controls.Add(this.grtip);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmhelp";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TOOLTIP";
            this.Load += new System.EventHandler(this.frmhelp_Load);
            this.grtip.ResumeLayout(false);
            this.grtip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grtip;
        private System.Windows.Forms.TextBox txttip;
        private System.Windows.Forms.CheckBox chkshow;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbwelcome;
    }
}