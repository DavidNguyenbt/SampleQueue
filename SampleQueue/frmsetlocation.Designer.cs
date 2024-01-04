namespace SampleQueue
{
    partial class frmsetlocation
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbstorage = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtrfid = new System.Windows.Forms.TextBox();
            this.btapply = new System.Windows.Forms.Button();
            this.btexit = new System.Windows.Forms.Button();
            this.lbspid = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtlocation = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Storage Name :";
            // 
            // cmbstorage
            // 
            this.cmbstorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbstorage.FormattingEnabled = true;
            this.cmbstorage.Location = new System.Drawing.Point(16, 67);
            this.cmbstorage.Name = "cmbstorage";
            this.cmbstorage.Size = new System.Drawing.Size(78, 21);
            this.cmbstorage.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "RFID  :";
            // 
            // txtrfid
            // 
            this.txtrfid.Location = new System.Drawing.Point(116, 67);
            this.txtrfid.Name = "txtrfid";
            this.txtrfid.Size = new System.Drawing.Size(262, 20);
            this.txtrfid.TabIndex = 2;
            // 
            // btapply
            // 
            this.btapply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btapply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btapply.ForeColor = System.Drawing.Color.Blue;
            this.btapply.Location = new System.Drawing.Point(143, 154);
            this.btapply.Name = "btapply";
            this.btapply.Size = new System.Drawing.Size(75, 23);
            this.btapply.TabIndex = 3;
            this.btapply.Text = "APPLY";
            this.btapply.UseVisualStyleBackColor = false;
            this.btapply.Click += new System.EventHandler(this.btapply_Click);
            // 
            // btexit
            // 
            this.btexit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btexit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btexit.ForeColor = System.Drawing.Color.Teal;
            this.btexit.Location = new System.Drawing.Point(224, 154);
            this.btexit.Name = "btexit";
            this.btexit.Size = new System.Drawing.Size(75, 23);
            this.btexit.TabIndex = 3;
            this.btexit.Text = "EXIT";
            this.btexit.UseVisualStyleBackColor = false;
            this.btexit.Click += new System.EventHandler(this.btexit_Click);
            // 
            // lbspid
            // 
            this.lbspid.AutoSize = true;
            this.lbspid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbspid.ForeColor = System.Drawing.Color.Blue;
            this.lbspid.Location = new System.Drawing.Point(12, 9);
            this.lbspid.Name = "lbspid";
            this.lbspid.Size = new System.Drawing.Size(73, 13);
            this.lbspid.TabIndex = 0;
            this.lbspid.Text = "Sample ID :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(385, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(43, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "GET";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Location ID :";
            // 
            // txtlocation
            // 
            this.txtlocation.Location = new System.Drawing.Point(116, 107);
            this.txtlocation.Name = "txtlocation";
            this.txtlocation.Size = new System.Drawing.Size(214, 20);
            this.txtlocation.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(336, 105);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "SELECT";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmsetlocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 189);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btexit);
            this.Controls.Add(this.btapply);
            this.Controls.Add(this.txtlocation);
            this.Controls.Add(this.txtrfid);
            this.Controls.Add(this.cmbstorage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbspid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmsetlocation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmsetlocation";
            this.Load += new System.EventHandler(this.frmsetlocation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbstorage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtrfid;
        private System.Windows.Forms.Button btapply;
        private System.Windows.Forms.Button btexit;
        private System.Windows.Forms.Label lbspid;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtlocation;
        private System.Windows.Forms.Button button2;
    }
}