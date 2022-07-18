using System;
using BreakDown.ManagedPdf.Core.Pdf;

namespace BreakDown.ManagedPdf.Core.Events
{
    /// <summary>
    /// Base class for EventArgs in PDFsharp.
    /// </summary>
    public abstract class PdfSharpEventArgs : EventArgs
    {
        /// <summary>
        /// The source of the event.
        /// </summary>
        public PdfObject Source { get; set; }
    }
}
