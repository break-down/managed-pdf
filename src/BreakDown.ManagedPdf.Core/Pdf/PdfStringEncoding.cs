using System;

namespace BreakDown.ManagedPdf.Core.Pdf
{
    /// <summary>
    /// Determines the encoding of a PdfString or PdfStringObject.
    /// </summary>
    [Flags]
    public enum PdfStringEncoding
    {
        /// <summary>
        /// The characters of the string are actually bytes with an unknown or context specific meaning or encoding.
        /// With this encoding the 8 high bits of each character is zero.
        /// </summary>
        RawEncoding = PdfStringFlags.RawEncoding,

        /// <summary>
        /// Not yet used by PDFsharp.
        /// </summary>
        StandardEncoding = PdfStringFlags.StandardEncoding,

        /// <summary>
        /// The characters of the string are actually bytes with PDF document encoding.
        /// With this encoding the 8 high bits of each character is zero.
        /// </summary>

        // ReSharper disable InconsistentNaming because the name is spelled as in the Adobe reference.
        PDFDocEncoding = PdfStringFlags.PDFDocEncoding,

        // ReSharper restore InconsistentNaming

        /// <summary>
        /// The characters of the string are actually bytes with Windows ANSI encoding.
        /// With this encoding the 8 high bits of each character is zero.
        /// </summary>
        WinAnsiEncoding = PdfStringFlags.WinAnsiEncoding,

        /// <summary>
        /// Not yet used by PDFsharp.
        /// </summary>
        MacRomanEncoding = PdfStringFlags.MacExpertEncoding,

        /// <summary>
        /// Not yet used by PDFsharp.
        /// </summary>
        MacExpertEncoding = PdfStringFlags.MacExpertEncoding,

        /// <summary>
        /// The characters of the string are Unicode characters.
        /// </summary>
        Unicode = PdfStringFlags.Unicode,
    }
}
