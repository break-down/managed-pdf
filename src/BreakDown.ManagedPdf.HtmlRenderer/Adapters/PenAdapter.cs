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
using BreakDown.ManagedPdf.Core.Drawing.enums;
using BreakDown.ManagedPdf.Html.Adapters;
using BreakDown.ManagedPdf.Html.Adapters.Entities;

namespace BreakDown.ManagedPdf.HtmlRenderer.Adapters
{
    internal sealed class PenAdapter : RPen
    {
        public PenAdapter(XPen pen)
        {
            Pen = pen;
        }

        /// <summary>
        /// The actual WinForms brush instance.
        /// </summary>
        public XPen Pen { get; }

        public override double PenWidth
        {
            get => Pen.Width;
            set => Pen.Width = value;
        }

        public override RDashStyle DashStyle
        {
            set
            {
                SetPenDashStyle(value);
                SetSmallDashBetterLookingPattern();
            }
        }

        private void SetPenDashStyle(RDashStyle dashStyle) => Pen.DashStyle = dashStyle switch
        {
            RDashStyle.Solid => XDashStyle.Solid,
            RDashStyle.Dash => XDashStyle.Dash,
            RDashStyle.Dot => XDashStyle.Dot,
            RDashStyle.DashDot => XDashStyle.DashDot,
            RDashStyle.DashDotDot => XDashStyle.DashDotDot,
            RDashStyle.Custom => XDashStyle.Custom,
            _ => XDashStyle.Solid
        };

        private void SetSmallDashBetterLookingPattern()
        {
            if (Pen.DashStyle is not XDashStyle.Dash)
            {
                return;
            }

            if (PenWidth >= 2)
            {
                return;
            }

            Pen.DashPattern = new[] { 4, 4d };
        }
    }
}
