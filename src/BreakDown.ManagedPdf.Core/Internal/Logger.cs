using System.Diagnostics;

namespace BreakDown.ManagedPdf.Core.Internal
{
    internal static class Logger
    {
        public static void Log(string format, params object[] args)
        {
            Debug.WriteLine("Log...");
        }
    }
}
