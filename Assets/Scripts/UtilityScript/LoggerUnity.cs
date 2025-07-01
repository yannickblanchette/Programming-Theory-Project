using System;
using UnityEngine;


namespace UtilityScripts
{
    public class LoggerUnity : MonoBehaviour, IDebugLogger
    {
        public static LoggerUnity instance { get; private set; }
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
