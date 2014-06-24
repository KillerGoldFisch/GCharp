using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSharp.Threading {
    public partial class GThreadControl : UserControl {
        internal GThread _gThread;

        public GThreadControl(GThread gthread) {
            this._gThread = gthread;
            InitializeComponent();

            this.lblName.Text = gthread.Name;
        }

        public void UpdateCPUUsage() {
            if (!GThread.GThreadManager.Manager.CPUUsage.ContainsKey(this._gThread))
                return;

            double cpuUsage = GThread.GThreadManager.Manager.CPUUsage[this._gThread] * 100.0;
            if (double.IsNaN(cpuUsage)) cpuUsage = 0.0;
            this.pbCPU.Value = (int)cpuUsage;
            this.lblPercent.Text = cpuUsage.ToString("0.00") + "%";
        }
    }
}
