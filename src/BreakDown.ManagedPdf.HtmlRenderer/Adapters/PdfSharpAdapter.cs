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

using System.Drawing;
using System.Drawing.Text;
using System.IO;
using BreakDown.ManagedPdf.Core.Drawing;
using BreakDown.ManagedPdf.Core.Drawing.enums;
using BreakDown.ManagedPdf.Core.Pdf.enums;
using BreakDown.ManagedPdf.Html.Adapters;
using BreakDown.ManagedPdf.Html.Adapters.Entities;
using BreakDown.ManagedPdf.HtmlRenderer.Utilities;

namespace BreakDown.ManagedPdf.HtmlRenderer.Adapters
{
    internal sealed class PdfSharpAdapter : RAdapter
    {
        private PdfSharpAdapter()
        {
            AddFontFamilyMapping("monospace", "Courier New");
            AddFontFamilyMapping("Helvetica", "Arial");

            var families = new InstalledFontCollection();

            foreach (var family in families.Families)
            {
                AddFontFamily(new FontFamilyAdapter(new XFontFamily(family.Name)));
            }
        }

        /// <summary>
        /// Singleton instance of global adapter.
        /// </summary>
        public static PdfSharpAdapter Instance { get; } = new();

        protected override RColor GetColorInt(string colorName)
        {
            try
            {
                var color = Color.FromKnownColor((KnownColor)System.Enum.Parse(typeof(KnownColor), colorName, true));
                return Utils.Convert(color);
            }
            catch
            {
                return RColor.Empty;
            }
        }

        protected override RPen CreatePen(RColor color)
        {
            return new PenAdapter(new XPen(Utils.Convert(color)));
        }

        protected override RBrush CreateSolidBrush(RColor color)
        {
            XBrush solidBrush;
            if (color == RColor.White)
            {
                solidBrush = XBrushes.White;
            }
            else if (color == RColor.Black)
            {
                solidBrush = XBrushes.Black;
            }
            else if (color.A < 1)
            {
                solidBrush = XBrushes.Transparent;
            }
            else
            {
                solidBrush = new XSolidBrush(Utils.Convert(color));
            }

            return new BrushAdapter(solidBrush);
        }

        protected override RBrush CreateLinearGradientBrush(RRect rect, RColor color1, RColor color2, double angle) =>
            new BrushAdapter(new XLinearGradientBrush(Utils.Convert(rect), Utils.Convert(color1), Utils.Convert(color2), GetLinearGradientMode(angle)));

        private static XLinearGradientMode GetLinearGradientMode(double angle) => angle switch
        {
            < 45 => XLinearGradientMode.ForwardDiagonal,
            < 90 => XLinearGradientMode.Vertical,
            < 135 => XLinearGradientMode.BackwardDiagonal,
            _ => XLinearGradientMode.Horizontal
        };

        protected override RImage ConvertImageInt(object image)
        {
            return image != null ? new ImageAdapter((XImage)image) : null;
        }

        protected override RImage ImageFromStreamInt(Stream memoryStream)
        {
            return new ImageAdapter(XImage.FromStream(memoryStream));
        }

        protected override RFont CreateFontInt(string family, double size, RFontStyle style)
        {
            var fontStyle = (XFontStyle)((int)style);
            var xFont = new XFont(family, size, fontStyle, new XPdfFontOptions(PdfFontEncoding.Unicode));
            return new FontAdapter(xFont);
        }

        protected override RFont CreateFontInt(RFontFamily family, double size, RFontStyle style)
        {
            var fontStyle = (XFontStyle)((int)style);
            var xFont = new XFont(((FontFamilyAdapter)family).FontFamily.Name, size, fontStyle, new XPdfFontOptions(PdfFontEncoding.Unicode));
            return new FontAdapter(xFont);
        }
    }
}
