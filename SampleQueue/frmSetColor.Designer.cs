
namespace SampleQueue
{
    partial class frmSetColor
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
            this.btnew = new System.Windows.Forms.Button();
            this.btincomplete = new System.Windows.Forms.Button();
            this.btindecoration = new System.Windows.Forms.Button();
            this.btinsewing = new System.Windows.Forms.Button();
            this.btinqueue = new System.Windows.Forms.Button();
            this.btfinishontime = new System.Windows.Forms.Button();
            this.btfinishdelay = new System.Windows.Forms.Button();
            this.btdefault = new System.Windows.Forms.Button();
            this.btsave = new System.Windows.Forms.Button();
            this.btexit = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btcftpassed = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnew
            // 
            this.btnew.Location = new System.Drawing.Point(17, 17);
            this.btnew.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnew.Name = "btnew";
            this.btnew.Size = new System.Drawing.Size(227, 34);
            this.btnew.TabIndex = 0;
            this.btnew.Text = "New";
            this.btnew.UseVisualStyleBackColor = true;
            this.btnew.Click += new System.EventHandler(this.btnew_Click);
            // 
            // btincomplete
            // 
            this.btincomplete.Location = new System.Drawing.Point(17, 76);
            this.btincomplete.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btincomplete.Name = "btincomplete";
            this.btincomplete.Size = new System.Drawing.Size(227, 34);
            this.btincomplete.TabIndex = 0;
            this.btincomplete.Text = "Incomplete";
            this.btincomplete.UseVisualStyleBackColor = true;
            this.btincomplete.Click += new System.EventHandler(this.btnew_Click);
            // 
            // btindecoration
            // 
            this.btindecoration.Location = new System.Drawing.Point(17, 136);
            this.btindecoration.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btindecoration.Name = "btindecoration";
            this.btindecoration.Size = new System.Drawing.Size(227, 34);
            this.btindecoration.TabIndex = 0;
            this.btindecoration.Text = "In Decoration";
            this.btindecoration.UseVisualStyleBackColor = true;
            this.btindecoration.Click += new System.EventHandler(this.btnew_Click);
            // 
            // btinsewing
            // 
            this.btinsewing.Location = new System.Drawing.Point(17, 195);
            this.btinsewing.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btinsewing.Name = "btinsewing";
            this.btinsewing.Size = new System.Drawing.Size(227, 34);
            this.btinsewing.TabIndex = 0;
            this.btinsewing.Text = "In Sewing";
            this.btinsewing.UseVisualStyleBackColor = true;
            this.btinsewing.Click += new System.EventHandler(this.btnew_Click);
            // 
            // btinqueue
            // 
            this.btinqueue.Location = new System.Drawing.Point(17, 314);
            this.btinqueue.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btinqueue.Name = "btinqueue";
            this.btinqueue.Size = new System.Drawing.Size(227, 34);
            this.btinqueue.TabIndex = 0;
            this.btinqueue.Text = "In Queue";
            this.btinqueue.UseVisualStyleBackColor = true;
            this.btinqueue.Click += new System.EventHandler(this.btnew_Click);
            // 
            // btfinishontime
            // 
            this.btfinishontime.Location = new System.Drawing.Point(17, 373);
            this.btfinishontime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btfinishontime.Name = "btfinishontime";
            this.btfinishontime.Size = new System.Drawing.Size(227, 34);
            this.btfinishontime.TabIndex = 0;
            this.btfinishontime.Text = "Finish On Time";
            this.btfinishontime.UseVisualStyleBackColor = true;
            this.btfinishontime.Click += new System.EventHandler(this.btnew_Click);
            // 
            // btfinishdelay
            // 
            this.btfinishdelay.Location = new System.Drawing.Point(17, 432);
            this.btfinishdelay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btfinishdelay.Name = "btfinishdelay";
            this.btfinishdelay.Size = new System.Drawing.Size(227, 34);
            this.btfinishdelay.TabIndex = 0;
            this.btfinishdelay.Text = "Finish in Delay";
            this.btfinishdelay.UseVisualStyleBackColor = true;
            this.btfinishdelay.Click += new System.EventHandler(this.btnew_Click);
            // 
            // btdefault
            // 
            this.btdefault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btdefault.Location = new System.Drawing.Point(17, 491);
            this.btdefault.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btdefault.Name = "btdefault";
            this.btdefault.Size = new System.Drawing.Size(73, 24);
            this.btdefault.TabIndex = 0;
            this.btdefault.Text = "Default";
            this.btdefault.UseVisualStyleBackColor = false;
            this.btdefault.Click += new System.EventHandler(this.btdefault_Click);
            // 
            // btsave
            // 
            this.btsave.BackColor = System.Drawing.Color.Lime;
            this.btsave.Location = new System.Drawing.Point(94, 491);
            this.btsave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btsave.Name = "btsave";
            this.btsave.Size = new System.Drawing.Size(73, 24);
            this.btsave.TabIndex = 0;
            this.btsave.Text = "Save";
            this.btsave.UseVisualStyleBackColor = false;
            this.btsave.Click += new System.EventHandler(this.btsave_Click);
            // 
            // btexit
            // 
            this.btexit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btexit.Location = new System.Drawing.Point(172, 491);
            this.btexit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btexit.Name = "btexit";
            this.btexit.Size = new System.Drawing.Size(73, 24);
            this.btexit.TabIndex = 0;
            this.btexit.Text = "Exit";
            this.btexit.UseVisualStyleBackColor = false;
            this.btexit.Click += new System.EventHandler(this.btexit_Click);
            // 
            // btcftpassed
            // 
            this.btcftpassed.Location = new System.Drawing.Point(17, 254);
            this.btcftpassed.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btcftpassed.Name = "btcftpassed";
            this.btcftpassed.Size = new System.Drawing.Size(227, 34);
            this.btcftpassed.TabIndex = 0;
            this.btcftpassed.Text = "QC/CFT Passed";
            this.btcftpassed.UseVisualStyleBackColor = true;
            this.btcftpassed.Click += new System.EventHandler(this.btnew_Click);
            // 
            // frmSetColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(272, 527);
            this.Controls.Add(this.btexit);
            this.Controls.Add(this.btsave);
            this.Controls.Add(this.btdefault);
            this.Controls.Add(this.btfinishdelay);
            this.Controls.Add(this.btfinishontime);
            this.Controls.Add(this.btcftpassed);
            this.Controls.Add(this.btinqueue);
            this.Controls.Add(this.btinsewing);
            this.Controls.Add(this.btindecoration);
            this.Controls.Add(this.btincomplete);
            this.Controls.Add(this.btnew);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetColor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSetColor";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmSetColor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnew;
        private System.Windows.Forms.Button btincomplete;
        private System.Windows.Forms.Button btindecoration;
        private System.Windows.Forms.Button btinsewing;
        private System.Windows.Forms.Button btinqueue;
        private System.Windows.Forms.Button btfinishontime;
        private System.Windows.Forms.Button btfinishdelay;
        private System.Windows.Forms.Button btdefault;
        private System.Windows.Forms.Button btsave;
        private System.Windows.Forms.Button btexit;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btcftpassed;
    }
}