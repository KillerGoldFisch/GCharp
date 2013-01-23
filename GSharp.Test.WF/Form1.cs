using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GSharp.UI.Controls.RuntimeProperty;
using GSharp.Logging;

namespace GSharp.Test.WF {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            
        }

        private void timer1_Tick(object sender, EventArgs e) {
            Log.Success("Test");
            Log.Info("Test");
            Log.Warn("Test");
            Log.Debug("Test", this);
            Log.Exception("Test", new Exception("Test"));
            Log.Error("Test");
            Log.Fatal("Test");
        }

        private void Form1_Load(object sender, EventArgs e) {
            //timer1_Tick(null, null);

            Log.TryLog(() => {
                int m;
                for(int n = 3; n >= 0; n--)
                    m = 3/n;
            });
        }
    }
}
