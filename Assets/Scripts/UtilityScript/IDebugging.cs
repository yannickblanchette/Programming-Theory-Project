using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityScripts
{
    public interface IDebugging
    {
        public void TurnOnInfoLogging();
        public void TurnOffInfoLogging();

        public void TurnOnDebugLogging();
        public void TurnOffDebugLogging();

        public void TurnOnWarningLogging();
        public void TurnOffWarningLogging();

        public void TurnOnErrorLogging();
        public void TurnOffErrorLogging();

        public bool infoLoggingEnabled { get; }
        public bool debugLoggingEnabled { get; }
        public bool warningLoggingEnabled { get; }
        public bool errorLoggingEnabled { get; }

        public bool InfoLog(string log);
        public bool DebugLog(string log);
        public bool WarningLog(string log);
        public bool ErrorLog(string log);
    }
}
