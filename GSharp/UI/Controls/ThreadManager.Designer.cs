namespace GSharp.UI.Controls {
    partial class ThreadManager {
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
            this.components = new System.ComponentModel.Container();
            GSharp.UI.Controls.GracialList.GLColumn glColumn1 = new GSharp.UI.Controls.GracialList.GLColumn();
            GSharp.UI.Controls.GracialList.GLColumn glColumn2 = new GSharp.UI.Controls.GracialList.GLColumn();
            GSharp.UI.Controls.GracialList.GLColumn glColumn3 = new GSharp.UI.Controls.GracialList.GLColumn();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.glGTreadList = new GSharp.UI.Controls.GracialList.GlacialList();
            this.SuspendLayout();
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 500;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // glGTreadList
            // 
            this.glGTreadList.AllowColumnResize = true;
            this.glGTreadList.AllowMultiselect = false;
            this.glGTreadList.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.glGTreadList.AlternatingColors = false;
            this.glGTreadList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glGTreadList.AutoHeight = true;
            this.glGTreadList.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.glGTreadList.BackgroundStretchToFit = true;
            glColumn1.ActivatedEmbeddedType = GSharp.UI.Controls.GracialList.GLActivatedEmbeddedTypes.None;
            glColumn1.CheckBoxes = false;
            glColumn1.ImageIndex = -1;
            glColumn1.Name = "clName";
            glColumn1.NumericSort = false;
            glColumn1.Text = "Name";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 100;
            glColumn2.ActivatedEmbeddedType = GSharp.UI.Controls.GracialList.GLActivatedEmbeddedTypes.None;
            glColumn2.CheckBoxes = false;
            glColumn2.ImageIndex = -1;
            glColumn2.Name = "clUsage";
            glColumn2.NumericSort = false;
            glColumn2.Text = "Usage";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn2.Width = 100;
            glColumn3.ActivatedEmbeddedType = GSharp.UI.Controls.GracialList.GLActivatedEmbeddedTypes.None;
            glColumn3.CheckBoxes = false;
            glColumn3.ImageIndex = -1;
            glColumn3.Name = "clPecent";
            glColumn3.NumericSort = false;
            glColumn3.Text = "[%]";
            glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn3.Width = 100;
            this.glGTreadList.Columns.AddRange(new GSharp.UI.Controls.GracialList.GLColumn[] {
            glColumn1,
            glColumn2,
            glColumn3});
            this.glGTreadList.ControlStyle = GSharp.UI.Controls.GracialList.GLControlStyles.Normal;
            this.glGTreadList.FullRowSelect = true;
            this.glGTreadList.GridColor = System.Drawing.Color.LightGray;
            this.glGTreadList.GridLines = GSharp.UI.Controls.GracialList.GLGridLines.gridBoth;
            this.glGTreadList.GridLineStyle = GSharp.UI.Controls.GracialList.GLGridLineStyles.gridSolid;
            this.glGTreadList.GridTypes = GSharp.UI.Controls.GracialList.GLGridTypes.gridOnExists;
            this.glGTreadList.HeaderHeight = 22;
            this.glGTreadList.HeaderVisible = true;
            this.glGTreadList.HeaderWordWrap = false;
            this.glGTreadList.HotColumnTracking = false;
            this.glGTreadList.HotItemTracking = false;
            this.glGTreadList.HotTrackingColor = System.Drawing.Color.LightGray;
            this.glGTreadList.HoverEvents = false;
            this.glGTreadList.HoverTime = 1;
            this.glGTreadList.ImageList = null;
            this.glGTreadList.ItemHeight = 17;
            this.glGTreadList.ItemWordWrap = false;
            this.glGTreadList.Location = new System.Drawing.Point(0, 0);
            this.glGTreadList.Name = "glGTreadList";
            this.glGTreadList.Selectable = true;
            this.glGTreadList.SelectedTextColor = System.Drawing.Color.White;
            this.glGTreadList.SelectionColor = System.Drawing.Color.DarkBlue;
            this.glGTreadList.ShowBorder = true;
            this.glGTreadList.ShowFocusRect = false;
            this.glGTreadList.Size = new System.Drawing.Size(304, 477);
            this.glGTreadList.SortType = GSharp.UI.Controls.GracialList.SortTypes.InsertionSort;
            this.glGTreadList.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.glGTreadList.TabIndex = 0;
            this.glGTreadList.Text = "glacialList1";
            // 
            // ThreadManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.glGTreadList);
            this.Name = "ThreadManager";
            this.Size = new System.Drawing.Size(304, 477);
            this.ResumeLayout(false);

        }

        #endregion

        private GracialList.GlacialList glGTreadList;
        private System.Windows.Forms.Timer timerUpdate;
    }
}
