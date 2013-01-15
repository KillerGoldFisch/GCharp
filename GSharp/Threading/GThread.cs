using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace GSharp.Threading {
    /// <summary>
    /// Description of XThread.
    /// </summary>
    public class GThread {
        public delegate void OnErrorHandler(GThread sender, Exception ex);
        public event OnErrorHandler OnError;

        private Thread _thread;
        private ProcessThread _processThread;
        private ThreadStart _threadStart;

        public Thread Thread { get { return _thread; } }
        public ProcessThread ProcessThread { get { return _processThread; } }

        private long _oldThreadCPUTime = 0;
        private long _oldProcessCPUTime = 0;

        public string Name { get; set; }

        public GThread(ThreadStart threadStart) {
            _threadStart = threadStart;
            _thread = new Thread(_thread_start);
        }

        public void Start() {
            _thread.Start();
        }

        private void _thread_start() {
            _processThread = GetCurrentProcessThread();
            try {
                _threadStart();
            } catch (Exception ex) {
                if (OnError != null)
                    OnError(this, ex);
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
