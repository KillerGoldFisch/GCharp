using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GSharp.Threading;

namespace GSharp.Test.WF {
    static class Program {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main() {
            new GThread(() => {
                int nr = 0;
                while (true) {
                    nr += 1;
                    GSharp.Threading.Stressing.PrimeTool.IsPrime(nr);
                    //System.Threading.Thread.Sleep(1);
                }
            }).Start();

            Logging.Log.LoggingHandler.Add(new GSharp.Logging.Logger.FileLogger("test.log"));
            System.Threading.Thread.CurrentThread.Name = "GUI Thread";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
