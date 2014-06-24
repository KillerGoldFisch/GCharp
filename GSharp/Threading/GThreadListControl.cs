using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSharp.Threading {
    public partial class GThreadListControl : UserControl {
        List<GThreadControl> _controls = new List<GThreadControl>();

        public GThreadListControl(IEnumerable<GThread> threads) {
            InitializeComponent();

            this.timerUpdate.Enabled = true;

            foreach (GThread t in threads)
                this.AddThread(t);

            GThread.OnEndGThread += new GThread.OnEndGThreadHandler(GThread_OnEndGThread);
        }



        public GThreadListControl() {
            InitializeComponent();

            this.timerUpdate.Enabled = true;

            foreach (GThread t in GThread.AllGThreads)
                this.AddThread(t);

            GThread.OnEndGThread+=new GThread.OnEndGThreadHandler(GThread_OnEndGThread);
            GThread.OnNewGThread += new GThread.OnNewGThreadHandler(GThread_OnNewGThread);
        }

        public void AddThread(GThread thread) {
            if (this.ContainsThread(thread))
                return;


            Async.UI(() => {
                lock (this._controls) {
                    GThreadControl c = new GThreadControl(thread);
                    this.flowLayoutPanel.Controls.Add(c);
                    this._controls.Add(c);
                }
            }, this, true);

        }

        public bool ContainsThread(GThread thread) {
            lock (this._controls) {
                foreach (GThreadControl c in this._controls)
                    if (c._gThread == thread)
                        return true;
                return false;
            }
        }

        public bool RemoveThread(GThread thread) {

            GThreadControl c = this.getControlByThread(thread);
            if (c == null)
                return false;
            Async.UI(() => {
                lock (this._controls) {
                    this.flowLayoutPanel.Controls.Remove(c);
                    this._controls.Remove(c);
                }
            }, this, true);
            return true;

        }

        private GThreadControl getControlByThread(GThread thread) {
            lock (this._controls) {
                foreach (GThreadControl c in this._controls)
                    if (c._gThread == thread)
                        return c;
                return null;
            }
        }

        #region Eventhandler
        void GThread_OnNewGThread(GThread gThread) {
            this.AddThread(gThread);
        }

        void GThread_OnEndGThread(GThread gThread) {
            this.RemoveThread(gThread);
        }

        private void timerUpdate_Tick(object sender, EventArgs e) {
            /*if (this.flowLayoutPanel.VerticalScroll.Visible)
                this.Width = this.MaximumSize.Width;
            else
                this.Width = this.MinimumSize.Width;*/
            lock (this._controls) {
                foreach (GThreadControl c in this._controls)
                    c.UpdateCPUUsage();
            }
        }
        #endregion
    }
}
