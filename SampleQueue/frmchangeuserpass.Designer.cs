
namespace SampleQueue
{
    partial class frmchangeuserpass
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
            this.lbname = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtconfirm = new System.Windows.Forms.TextBox();
            this.txtnewpass = new System.Windows.Forms.TextBox();
            this.txtoldpass = new System.Windows.Forms.TextBox();
            this.btchange = new System.Windows.Forms.Button();
            this.btexit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbname
            // 
            this.lbname.AutoSize = true;
            this.lbname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbname.ForeColor = System.Drawing.Color.Blue;
            this.lbname.Location = new System.Drawing.Point(12, 13);
            this.lbname.Name = "lbname";
            this.lbname.Size = new System.Drawing.Size(75, 20);
            this.lbname.TabIndex = 0;
            this.lbname.Text = "Name : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Old Password :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "New Password : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Confirm New Password :";
            // 
            // txtconfirm
            // 
            this.txtconfirm.Location = new System.Drawing.Point(178, 140);
            this.txtconfirm.Name = "txtconfirm";
            this.txtconfirm.PasswordChar = '*';
            this.txtconfirm.Size = new System.Drawing.Size(167, 22);
            this.txtconfirm.TabIndex = 1;
            // 
            // txtnewpass
            // 
            this.txtnewpass.Location = new System.Drawing.Point(178, 96);
            this.txtnewpass.Name = "txtnewpass";
            this.txtnewpass.PasswordChar = '*';
            this.txtnewpass.Size = new System.Drawing.Size(167, 22);
            this.txtnewpass.TabIndex = 1;
            // 
            // txtoldpass
            // 
            this.txtoldpass.Location = new System.Drawing.Point(178, 52);
            this.txtoldpass.Name = "txtoldpass";
            this.txtoldpass.PasswordChar = '*';
            this.txtoldpass.Size = new System.Drawing.Size(167, 22);
            this.txtoldpass.TabIndex = 1;
            // 
            // btchange
            // 
            this.btchange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btchange.Location = new System.Drawing.Point(79, 186);
            this.btchange.Name = "btchange";
            this.btchange.Size = new System.Drawing.Size(93, 23);
            this.btchange.TabIndex = 2;
            this.btchange.Text = "CHANGE";
            this.btchange.UseVisualStyleBackColor = false;
            this.btchange.Click += new System.EventHandler(this.btchange_Click);
            // 
            // btexit
            // 
            this.btexit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btexit.Location = new System.Drawing.Point(211, 186);
            this.btexit.Name = "btexit";
            this.btexit.Size = new System.Drawing.Size(93, 23);
            this.btexit.TabIndex = 2;
            this.btexit.Text = "EXIT";
            this.btexit.UseVisualStyleBackColor = false;
            this.btexit.Click += new System.EventHandler(this.btexit_Click);
            // 
            // frmchangeuserpass
            // 
            this.AcceptButton = this.btchange;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btexit;
            this.ClientSize = new System.Drawing.Size(411, 221);
            this.Controls.Add(this.btexit);
            this.Controls.Add(this.btchange);
            this.Controls.Add(this.txtoldpass);
            this.Controls.Add(this.txtnewpass);
            this.Controls.Add(this.txtconfirm);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbname);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmchangeuserpass";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmchangeuserpass";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmchangeuserpass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtconfirm;
        private System.Windows.Forms.TextBox txtnewpass;
        private System.Windows.Forms.TextBox txtoldpass;
        private System.Windows.Forms.Button btchange;
        private System.Windows.Forms.Button btexit;
    }
}