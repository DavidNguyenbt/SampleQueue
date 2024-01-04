namespace SampleQueue
{
    partial class frmlocation
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
            this.btexit = new System.Windows.Forms.Button();
            this.btapply = new System.Windows.Forms.Button();
            this.cmblocation = new System.Windows.Forms.ComboBox();
            this.cmbstore = new System.Windows.Forms.ComboBox();
            this.lbspid = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btexit
            // 
            this.btexit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btexit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btexit.ForeColor = System.Drawing.Color.Teal;
            this.btexit.Location = new System.Drawing.Point(249, 115);
            this.btexit.Name = "btexit";
            this.btexit.Size = new System.Drawing.Size(75, 23);
            this.btexit.TabIndex = 14;
            this.btexit.Text = "EXIT";
            this.btexit.UseVisualStyleBackColor = false;
            this.btexit.Click += new System.EventHandler(this.btexit_Click);
            // 
            // btapply
            // 
            this.btapply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btapply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btapply.ForeColor = System.Drawing.Color.Blue;
            this.btapply.Location = new System.Drawing.Point(168, 115);
            this.btapply.Name = "btapply";
            this.btapply.Size = new System.Drawing.Size(75, 23);
            this.btapply.TabIndex = 15;
            this.btapply.Text = "APPLY";
            this.btapply.UseVisualStyleBackColor = false;
            this.btapply.Click += new System.EventHandler(this.btapply_Click);
            // 
            // cmblocation
            // 
            this.cmblocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmblocation.FormattingEnabled = true;
            this.cmblocation.Location = new System.Drawing.Point(342, 66);
            this.cmblocation.Name = "cmblocation";
            this.cmblocation.Size = new System.Drawing.Size(111, 21);
            this.cmblocation.TabIndex = 10;
            // 
            // cmbstore
            // 
            this.cmbstore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbstore.FormattingEnabled = true;
            this.cmbstore.Location = new System.Drawing.Point(133, 66);
            this.cmbstore.Name = "cmbstore";
            this.cmbstore.Size = new System.Drawing.Size(118, 21);
            this.cmbstore.TabIndex = 11;
            this.cmbstore.SelectedIndexChanged += new System.EventHandler(this.cmbstore_SelectedIndexChanged);
            // 
            // lbspid
            // 
            this.lbspid.AutoSize = true;
            this.lbspid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbspid.ForeColor = System.Drawing.Color.Blue;
            this.lbspid.Location = new System.Drawing.Point(12, 9);
            this.lbspid.Name = "lbspid";
            this.lbspid.Size = new System.Drawing.Size(73, 13);
            this.lbspid.TabIndex = 6;
            this.lbspid.Text = "Sample ID :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(268, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Location ID :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Storage Method :";
            // 
            // frmlocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(510, 177);
            this.Controls.Add(this.btexit);
            this.Controls.Add(this.btapply);
            this.Controls.Add(this.cmblocation);
            this.Controls.Add(this.cmbstore);
            this.Controls.Add(this.lbspid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmlocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmlocation";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmlocation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btexit;
        private System.Windows.Forms.Button btapply;
        private System.Windows.Forms.ComboBox cmblocation;
        private System.Windows.Forms.ComboBox cmbstore;
        private System.Windows.Forms.Label lbspid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}