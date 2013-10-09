using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSharp.Extensions.TimerEx {
    public static class TimerExtensions {
        public static void Reset(this Timer @thisX) {
            @thisX.Stop();
            @thisX.Start();
        }
    }
}
