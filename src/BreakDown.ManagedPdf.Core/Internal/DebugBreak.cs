using System.Diagnostics;

namespace BreakDown.ManagedPdf.Core.Internal
{
    /// <summary>
    /// Helper class around the Debugger class.
    /// </summary>
    public static class DebugBreak
    {
        /// <summary>
        /// Call Debugger.Break() if a debugger is attached.
        /// </summary>
        public static void Break()
        {
            Break(false);
        }

        /// <summary>
        /// Call Debugger.Break() if a debugger is attached or when always is set to true.
        /// </summary>
        public static void Break(bool always)
        {
#if DEBUG
            if (always || Debugger.IsAttached)
            {
                Debugger.Break();
            }
#endif
        }
    }
}
