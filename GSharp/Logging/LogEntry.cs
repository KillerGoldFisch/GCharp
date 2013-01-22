using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSharp.Logging {
    public class LogEntry {
        public enum LogTypes {
            Success,
            Info,
            Warn,
            Debug,
            Exaption,
            Error,
            Fatal
        }

        private LogTypes _logType;
        private DateTime _timeStamp;
        private string _message;
        private string _stacktrace;
        private object[] _params;
        private Exception _exception;
    }
}
