using System;

namespace BreakDown.ManagedPdf.Core.Pdf
{
    /// <summary>
    /// Internal wrapper for PdfStringEncoding.
    /// </summary>
    [Flags]
    enum PdfStringFlags
    {
        // ReSharper disable InconsistentNaming
        RawEncoding = 0x00,
        StandardEncoding = 0x01, // not used by PDFsharp
        PDFDocEncoding = 0x02,
        WinAnsiEncoding = 0x03,
        MacRomanEncoding = 0x04, // not used by PDFsharp
        MacExpertEncoding = 0x05, // not used by PDFsharp
        Unicode = 0x06,
        EncodingMask = 0x0F,

        HexLiteral = 0x80,

        // ReSharper restore InconsistentNaming
    }
}
