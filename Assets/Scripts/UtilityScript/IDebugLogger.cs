using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityScripts
{
    public interface IDebugLogger
    {
        public void WriteLine(string message);

        public void EnableTestMode();
        public void DisableTestMode();

        public bool testModeEnabled { get; }
    }
}
