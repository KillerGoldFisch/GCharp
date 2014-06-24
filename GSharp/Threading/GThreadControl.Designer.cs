namespace GSharp.Threading {
    partial class GThreadControl {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.pbCPU = new System.Windows.Forms.ProgressBar();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPercent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbCPU
            // 
            this.pbCPU.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbCPU.Location = new System.Drawing.Point(0, 21);
            this.pbCPU.Name = "pbCPU";
            this.pbCPU.Size = new System.Drawing.Size(310, 17);
            this.pbCPU.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbCPU.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(68, 17);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "NONAME";
            // 
            // lblPercent
            // 
            this.lblPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(262, -1);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(40, 17);
            this.lblPercent.TabIndex = 2;
            this.lblPercent.Text = "0,0%";
            this.lblPercent.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // GThreadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.pbCPU);
            this.Name = "GThreadControl";
            this.Size = new System.Drawing.Size(310, 38);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbCPU;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPercent;
    }
}
