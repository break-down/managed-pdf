using BreakDown.ManagedPdf.Core.Pdf;

namespace BreakDown.ManagedPdf.Core.Events
{
    /// <summary>
    /// EventArgs for changes in the PdfPages of adocument.
    /// </summary>
    public class PageEventArgs : PdfSharpEventArgs
    {
        /// <summary>
        /// Gets or sets the affected page.
        /// </summary>
        public PdfPage Page { get; set; }

        /// <summary>
        /// Gets or sets the page index of the affected page.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// The event type of PageEvent.
        /// </summary>
        public PageEventType EventType { get; internal set; }
    }
}
