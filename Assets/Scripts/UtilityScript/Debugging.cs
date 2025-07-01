using UnityEngine;
using System;

namespace UtilityScripts
{
    public class Debugging : MonoBehaviour, IDebugging
    {
        private static Debugging instance;

        private bool _infoLoggingEnabled;
        private bool _debugLoggingEnabled;
        private bool _warningLoggingEnabled;
        private bool _errorLoggingEnabled;

        [SerializeField] private LoggerUnity logger;
        private Guid guid;


        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                this._infoLoggingEnabled = false;
                this._debugLoggingEnabled = false;
                this._warningLoggingEnabled = false;
                this._errorLoggingEnabled = false;
                this.guid = Guid.NewGuid();
            }
        }



        public static Debugging GetInstance()
        {
            return instance;
        }


        public bool infoLoggingEnabled => _infoLoggingEnabled;


        public bool debugLoggingEnabled => _debugLoggingEnabled;


        public bool warningLoggingEnabled => _warningLoggingEnabled;


        public bool errorLoggingEnabled => _errorLoggingEnabled;


        public void TurnOffErrorLogging()
        {
            _errorLoggingEnabled = false;
        }


        public void TurnOffDebugLogging()
        {
            _debugLoggingEnabled = false;
        }


        public void TurnOffInfoLogging()
        {
            _infoLoggingEnabled = false;
        }


        public void TurnOffWarningLogging()
        {
            _warningLoggingEnabled = false;
        }


        public void TurnOnDebugLogging()
        {
            _debugLoggingEnabled = true;
        }


        public void TurnOnErrorLogging()
        {
            _errorLoggingEnabled = true;
        }


        public void TurnOnInfoLogging()
        {
            _infoLoggingEnabled = true;
        }


        public void TurnOnWarningLogging()
        {
            _warningLoggingEnabled = true;
        }


        public bool DebugLog(string log)
        {
            if (_debugLoggingEnabled)
            {
                logger.WriteLine(log);
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool InfoLog(string log)
        {
            if (_infoLoggingEnabled)
            {
                logger.WriteLine(log);
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool WarningLog(string log)
        {
            if (_warningLoggingEnabled)
            {
                logger.WriteLine(log);
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool ErrorLog(string log)
        {
            if (_errorLoggingEnabled)
            {
                logger.WriteLine(log);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
