using System;
using System.Globalization;
using BreakDown.ManagedPdf.Core.Pdf.Content;

namespace BreakDown.ManagedPdf.Core.Internal
{
    internal static class ContentReaderDiagnostics
    {
        public static void ThrowContentReaderException(string message)
        {
            throw new ContentReaderException(message);
        }

        public static void ThrowContentReaderException(string message, Exception innerException)
        {
            throw new ContentReaderException(message, innerException);
        }

        public static void ThrowNumberOutOfIntegerRange(long value)
        {
            var message = string.Format(CultureInfo.InvariantCulture, "Number '{0}' out of integer range.", value);
            ThrowContentReaderException(message);
        }

        public static void HandleUnexpectedCharacter(char ch)
        {
            var message = string.Format(CultureInfo.InvariantCulture,
                                        "Unexpected character '0x{0:x4}' in content stream. The stream may be corrupted or the feature is not implemented. " +
                                        "If you think this is a bug in PDFsharp, please send us your PDF file.", (int)ch);
            ThrowContentReaderException(message);
        }
    }
}
