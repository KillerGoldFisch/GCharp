namespace GSharp.Threading {
    partial class GThreadForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.gThreadListControl1 = new GSharp.Threading.GThreadListControl();
            this.SuspendLayout();
            // 
            // gThreadListControl1
            // 
            this.gThreadListControl1.Location = new System.Drawing.Point(0, 0);
            this.gThreadListControl1.MaximumSize = new System.Drawing.Size(344, 1000);
            this.gThreadListControl1.MinimumSize = new System.Drawing.Size(314, 0);
            this.gThreadListControl1.Name = "gThreadListControl1";
            this.gThreadListControl1.Size = new System.Drawing.Size(344, 628);
            this.gThreadListControl1.TabIndex = 0;
            // 
            // GThreadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 640);
            this.Controls.Add(this.gThreadListControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GThreadForm";
            this.ShowIcon = false;
            this.Text = "GThreadForm";
            this.Load += new System.EventHandler(this.GThreadForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private GThreadListControl gThreadListControl1;

    }
}