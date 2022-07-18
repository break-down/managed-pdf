using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using BreakDown.ManagedPdf.Core.Internal;

namespace BreakDown.ManagedPdf.Core.Drawing
{
    internal sealed class XPngBitmapEncoder : XBitmapEncoder
    {
        internal XPngBitmapEncoder()
        {
        }

        /// <summary>
        /// Saves the image on the specified stream in PNG format.
        /// </summary>
        public override void Save(Stream stream)
        {
            if (Source == null)
            {
                throw new InvalidOperationException("No image source.");
            }
#if CORE_WITH_GDI || GDI
            if (Source.AssociatedGraphics != null)
            {
                Source.DisassociateWithGraphics();
                Debug.Assert(Source.AssociatedGraphics == null);
            }

            try
            {
                Lock.EnterGdiPlus();
                Source._gdiImage.Save(stream, ImageFormat.Png);
            }
            finally
            {
                Lock.ExitGdiPlus();
            }
#endif
#if WPF
            DiagnosticsHelper.ThrowNotImplementedException("Save...");
#endif
        }
    }
}
