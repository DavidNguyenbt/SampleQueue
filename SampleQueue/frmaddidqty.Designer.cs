namespace SampleQueue
{
    partial class frmaddidqty
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
            this.nmqty = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btapply = new System.Windows.Forms.Button();
            this.btclose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nmqty)).BeginInit();
            this.SuspendLayout();
            // 
            // nmqty
            // 
            this.nmqty.Location = new System.Drawing.Point(15, 35);
            this.nmqty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmqty.Name = "nmqty";
            this.nmqty.Size = new System.Drawing.Size(120, 20);
            this.nmqty.TabIndex = 0;
            this.nmqty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "The qty of barcode need to print : ";
            // 
            // btapply
            // 
            this.btapply.Location = new System.Drawing.Point(152, 35);
            this.btapply.Name = "btapply";
            this.btapply.Size = new System.Drawing.Size(75, 23);
            this.btapply.TabIndex = 2;
            this.btapply.Text = "APPLY";
            this.btapply.UseVisualStyleBackColor = true;
            this.btapply.Click += new System.EventHandler(this.btapply_Click);
            // 
            // btclose
            // 
            this.btclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btclose.Location = new System.Drawing.Point(244, 35);
            this.btclose.Name = "btclose";
            this.btclose.Size = new System.Drawing.Size(75, 23);
            this.btclose.TabIndex = 2;
            this.btclose.Text = "CLOSE";
            this.btclose.UseVisualStyleBackColor = true;
            this.btclose.Click += new System.EventHandler(this.btclose_Click);
            // 
            // frmaddidqty
            // 
            this.AcceptButton = this.btapply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btclose;
            this.ClientSize = new System.Drawing.Size(348, 77);
            this.Controls.Add(this.btclose);
            this.Controls.Add(this.btapply);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nmqty);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmaddidqty";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "THIS SAMPLE UNIT : SET";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.nmqty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nmqty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btapply;
        private System.Windows.Forms.Button btclose;
    }
}