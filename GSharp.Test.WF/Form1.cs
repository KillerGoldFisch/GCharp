using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GSharp.UI.Controls.RuntimeProperty;

namespace GSharp.Test.WF {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            RuntimeEditor re = new RuntimeEditor();

            re.SelectedObject = this;
            re.Show();
        }
    }
}
