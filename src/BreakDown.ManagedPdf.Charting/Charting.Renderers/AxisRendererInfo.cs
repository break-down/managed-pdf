using BreakDown.ManagedPdf.Charting.Charting.enums;
using BreakDown.ManagedPdf.Core.Drawing;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// An AxisRendererInfo holds all axis specific rendering information.
    /// </summary>
    internal class AxisRendererInfo : AreaRendererInfo
    {
        internal Axis _axis;

        internal double MinimumScale;
        internal double MaximumScale;
        internal double MajorTick;
        internal double MinorTick;
        internal TickMarkType MinorTickMark;
        internal TickMarkType MajorTickMark;
        internal double MajorTickMarkWidth;
        internal double MinorTickMarkWidth;
        internal XPen MajorTickMarkLineFormat;
        internal XPen MinorTickMarkLineFormat;

        //Gridlines
        internal XPen MajorGridlinesLineFormat;
        internal XPen MinorGridlinesLineFormat;

        //AxisTitle
        internal AxisTitleRendererInfo _axisTitleRendererInfo;

        //TickLabels
        internal string TickLabelsFormat;
        internal XFont TickLabelsFont;
        internal XBrush TickLabelsBrush;
        internal double TickLabelsHeight;

        //LineFormat
        internal XPen LineFormat;

        //Chart.XValues, used for X axis only.
        internal XValues XValues;

        /// <summary>
        /// Sets the x coordinate of the inner rectangle.
        /// </summary>
        internal override double X
        {
            set
            {
                base.X = value;
                InnerRect.X = value;
            }
        }

        /// <summary>
        /// Sets the y coordinate of the inner rectangle.
        /// </summary>
        internal override double Y
        {
            set
            {
                base.Y = value;
                InnerRect.Y = value + LabelSize.Height / 2;
            }
        }

        /// <summary>
        /// Sets the height of the inner rectangle.
        /// </summary>
        internal override double Height
        {
            set
            {
                base.Height = value;
                InnerRect.Height = value - (InnerRect.Y - Y);
            }
        }

        /// <summary>
        /// Sets the width of the inner rectangle.
        /// </summary>
        internal override double Width
        {
            set
            {
                base.Width = value;
                InnerRect.Width = value - LabelSize.Width / 2;
            }
        }

        internal XRect InnerRect;
        internal XSize LabelSize;
    }
}
