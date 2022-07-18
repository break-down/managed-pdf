using BreakDown.ManagedPdf.Core.Drawing;
using BreakDown.ManagedPdf.Core.Pdf;

namespace BreakDown.ManagedPdf.Core.Events
{
    /// <summary>
    /// EventArgs for actions on a page's XGraphics object.
    /// </summary>
    public class PageGraphicsEventArgs : PdfSharpEventArgs
    {
        /// <summary>
        /// Gets the page xxxxx.
        /// </summary>
        public PdfPage Page { get; internal set; }

        /// <summary>
        /// Gets the created XGraphics object.
        /// </summary>
        public XGraphics Graphics { get; internal set; }

        /// <summary>
        /// The action type of PageGraphicsEvent.
        /// </summary>
        public PageGraphicsActionType ActionType { get; internal set; }
    }
}
