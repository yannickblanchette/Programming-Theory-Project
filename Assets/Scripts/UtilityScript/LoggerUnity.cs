using System;
using UnityEngine;


namespace UtilityScripts
{
    public class LoggerUnity : MonoBehaviour, IDebugLogger
    {
        public static LoggerUnity instance { get; private set; }
        private bool _testModeEnabled;
        

        void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            // end of new code

            instance = this;
            _testModeEnabled = false;
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
