using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Collections.Generic;

namespace GSharp.Threading {
    /// <summary>
    /// Description of XThread.
    /// </summary>
    public class GThread {
        public delegate void OnNewGThreadHandler(GThread gThread);
        public static event OnNewGThreadHandler OnNewGThread;

        public delegate void OnEndGThreadHandler(GThread gThread);
        public static event OnEndGThreadHandler OnEndGThread;

        public static List<GThread> AllGThreads = new List<GThread>();

        public static GThread GetCurrentGThread() {
            Thread currentThread = Thread.CurrentThread;
            foreach (GThread gThread in AllGThreads)
                if (gThread.Thread == currentThread)
                    return gThread;

            GThread gthread = new GThread();
            gthread._thread = currentThread;
            gthread._processThread = GThread.GetCurrentProcessThread();
            gthread.Name = GSharp.Sys.Process.Utils.GetLastMethodName();
            AllGThreads.Add(gthread);
            return gthread;
        }
        

        public delegate void OnErrorHandler(GThread sender, Exception ex);
        public event OnErrorHandler OnError;


        internal Thread _thread;
        internal ProcessThread _processThread;
        private ThreadStart _threadStart;

        public Thread Thread { get { return _thread; } }
        public ProcessThread ProcessThread { get { return _processThread; } }

        private long _oldThreadCPUTime = 0;
        private long _oldProcessCPUTime = 0;

        public string Name { get; set; }

        internal GThread() { }

        public GThread(ThreadStart threadStart, string name = null, bool isBackround = false) {
            if (name == null) this.Name = GSharp.Sys.Process.Utils.GetLastMethodName();
            else this.Name = name;
            _threadStart = threadStart;
            _thread = new Thread(_thread_start);
            _thread.IsBackground = isBackround;
        }
      

        public void Start() {
            _thread.Start();
        }

        private void _thread_start() {
            _processThread = GetCurrentProcessThread();
            AllGThreads.Add(this);
            if (OnNewGThread != null) OnNewGThread(this);
            try {
                _threadStart();
            } catch (Exception ex) {
                Logging.Log.Exception("Unhandled GThread Excaption", ex, this);
                if (OnError != null)
                    OnError(this, ex);
            } finally {
                AllGThreads.Remove(this);
                if (OnEndGThread != null) OnEndGThread(this);
            }
        }

        public double GetCPUUsageRelative() {
            Process p = Process.GetCurrentProcess();
            long threadCpuTime = _processThread.TotalProcessorTime.Ticks;
            long processCpuTime = p.TotalProcessorTime.Ticks;

            double usage = ((double)(threadCpuTime - _oldThreadCPUTime)) / ((double)(processCpuTime - _oldProcessCPUTime));
            //Console.WriteLine(string.Format("threadCpuTime={0} : threadCpuTime={1}\nprocessCpuTime={2} : _oldProcessCPUTime{3}",
            //                                threadCpuTime,_oldThreadCPUTime,
            //                                processCpuTime,_oldProcessCPUTime));

            _oldThreadCPUTime = threadCpuTime;
            _oldProcessCPUTime = processCpuTime;

            return usage;
        }

        public void Join() {
            _thread.Join();
        }

        public void Resume() { _thread.Resume(); }

        public void Abort() { _thread.Abort(); }

        public static ProcessThread GetCurrentProcessThread() {
            int threadID = AppDomain.GetCurrentThreadId();
            foreach (ProcessThread pt in Process.GetCurrentProcess().Threads) {
                if (pt.Id == threadID) {
                    return pt;
                }
            }
            return null;
            //ProcessThread pthread = thread;
        }


    }
}
