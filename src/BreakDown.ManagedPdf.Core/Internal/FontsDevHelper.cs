using BreakDown.ManagedPdf.Core.Drawing;
using BreakDown.ManagedPdf.Core.Drawing.enums;
using BreakDown.ManagedPdf.Core.Fonts;

namespace BreakDown.ManagedPdf.Core.Internal
{
    /// <summary>
    /// Internal stuff for development of PDFsharp.
    /// </summary>
    public static class FontsDevHelper
    {
        /// <summary>
        /// Creates font and enforces bold/italic simulation.
        /// </summary>
        public static XFont CreateSpecialFont(string familyName,
                                              double emSize,
                                              XFontStyle style,
                                              XPdfFontOptions pdfOptions,
                                              XStyleSimulations styleSimulations)
        {
            return new XFont(familyName, emSize, style, pdfOptions, styleSimulations);
        }

        /// <summary>
        /// Dumps the font caches to a string.
        /// </summary>
        public static string GetFontCachesState()
        {
            return FontFactory.GetFontCachesState();
        }
    }
}
