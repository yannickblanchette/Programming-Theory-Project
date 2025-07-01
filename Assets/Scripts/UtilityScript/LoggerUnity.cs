using System;
using UnityEngine;


namespace UtilityScripts
{
    public class LoggerUnity : MonoBehaviour, IDebugLogger
    {
        private static LoggerUnity instance;
        private bool _testModeEnabled;
        Guid guid;
        

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                _testModeEnabled = false;
                guid = Guid.NewGuid();
            }
        }


        public static LoggerUnity GetInstance()
        {
            return instance;
        }


        public void DisableTestMode()
        {
            _testModeEnabled = false;
        }


        public void EnableTestMode()
        {
            _testModeEnabled = true;
        }


        public bool testModeEnabled => _testModeEnabled;


        public void WriteLine(string message)
        {
            if (!_testModeEnabled)
            {
                Debug.Log(message);
            }
        }
    }
}
