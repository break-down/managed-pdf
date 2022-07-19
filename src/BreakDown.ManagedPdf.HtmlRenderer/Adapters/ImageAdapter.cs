// "Therefore those skilled at the unorthodox
// are infinite as heaven and earth,
// inexhaustible as the great rivers.
// When they come to an end,
// they begin again,
// like the days and months;
// they die and are reborn,
// like the four seasons."
// 
// - Sun Tsu,
// "The Art of War"

using BreakDown.ManagedPdf.Core.Drawing;
using BreakDown.ManagedPdf.Html.Adapters;

namespace BreakDown.ManagedPdf.HtmlRenderer.Adapters
{
    /// <summary>
    /// Adapter for WinForms Image object for core.
    /// </summary>
    internal sealed class ImageAdapter : RImage
    {
        public ImageAdapter(XImage image)
        {
            Image = image;
        }

        /// <summary>
        /// the underline win-forms image.
        /// </summary>
        public XImage Image { get; }

        public override double Width => Image.PixelWidth;

        public override double Height => Image.PixelHeight;

        public override void Dispose()
        {
            Image.Dispose();
        }
    }
}
