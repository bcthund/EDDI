using System.Diagnostics;
using System.Linq;

namespace Utilities
{
    public class Processes
    {
        private const string eliteDangerousProcessName = "EliteDangerous";

        public static bool IsProcessRunning(string processName)
        {
            return Process.GetProcesses().Any( p => p.ProcessName.Contains( processName ) );
        }

        public static bool IsEliteRunning()
        {
            return IsProcessRunning(eliteDangerousProcessName);
        }
    }
}
