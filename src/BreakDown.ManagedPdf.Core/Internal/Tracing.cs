using System.Diagnostics;

namespace BreakDown.ManagedPdf.Core.Internal
{
    class Tracing
    {
        [Conditional("DEBUG")]
        public void Foo()
        {
        }
    }
}
