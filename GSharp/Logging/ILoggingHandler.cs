using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSharp.Logging {
    public interface ILoggingHandler {
        void Success(DateTime timestamp, string Message, string Stacktrace, params object[] list);
        void Info(DateTime timestamp, string Message, string Stacktrace, params object[] list);
        void Warn(DateTime timestamp, string Message, string Stacktrace, params object[] list);
        void Debug(DateTime timestamp, string Message, string Stacktrace, params object[] list);
        void Exaption(DateTime timestamp, string Message, Exception exception, string Stacktrace, params object[] list);
        void Error(DateTime timestamp, string Message, string Stacktrace, params object[] list);
        void Fatal(DateTime timestamp, string Message, string Stacktrace, params object[] list);
    }
}
