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

namespace BreakDown.ManagedPdf.HtmlRenderer.Adapters
{
    /// <summary>
    /// Because BreakDown.ManagedPdf.Core doesn't support texture brush we need to implement it ourselves.
    /// </summary>
    internal sealed class XTextureBrush
    {
        /// <summary>
        /// The image to draw in the brush
        /// </summary>
        private readonly XImage image;

        private readonly XRect destinationRect;

        /// <summary>
        /// the transform the location of the image to handle center alignment
        /// </summary>
        private readonly XPoint translateTransformPoint;

        /// <summary>
        /// Init.
        /// </summary>
        public XTextureBrush(XImage image, XRect destinationRect, XPoint translateTransformPoint)
        {
            this.image = image;
            this.destinationRect = destinationRect;
            this.translateTransformPoint = translateTransformPoint;
        }

        /// <summary>
        /// Draw the texture image in the given graphics at the given location.
        /// </summary>
        public void DrawRectangle(XGraphics graphics, double x, double y, double width, double height)
        {
            var prevState = graphics.Save();
            graphics.IntersectClip(new XRect(x, y, width, height));

            var rx = translateTransformPoint.X;
            double w = image.PixelWidth, h = image.PixelHeight;
            while (rx < x + width)
            {
                var ry = translateTransformPoint.Y;
                while (ry < y + height)
                {
                    graphics.DrawImage(image, rx, ry, w, h);
                    ry += h;
                }

                rx += w;
            }

            graphics.Restore(prevState);
        }
    }
}
