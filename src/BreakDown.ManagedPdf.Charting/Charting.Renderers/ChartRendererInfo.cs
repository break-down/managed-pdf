using BreakDown.ManagedPdf.Core.Drawing;
using BreakDown.ManagedPdf.Core.Drawing.enums;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// A ChartRendererInfo stores information of all main parts of a chart like axis renderer info or
    /// plotarea renderer info.
    /// </summary>
    internal class ChartRendererInfo : AreaRendererInfo
    {
        internal Chart _chart;

        internal AxisRendererInfo xAxisRendererInfo;

        internal AxisRendererInfo yAxisRendererInfo;

        //internal AxisRendererInfo zAxisRendererInfo; // not yet used
        internal PlotAreaRendererInfo plotAreaRendererInfo;
        internal LegendRendererInfo legendRendererInfo;
        internal SeriesRendererInfo[] seriesRendererInfos;

        /// <summary>
        /// Gets the chart's default font for rendering.
        /// </summary>
        internal XFont DefaultFont
        {
            get
            {
                return _defaultFont ??
                       (_defaultFont = Converter.ToXFont(_chart._font, new XFont("Arial", 12, XFontStyle.Regular)));
            }
        }

        XFont _defaultFont;

        /// <summary>
        /// Gets the chart's default font for rendering data labels.
        /// </summary>
        internal XFont DefaultDataLabelFont
        {
            get
            {
                return _defaultDataLabelFont ??
                       (_defaultDataLabelFont = Converter.ToXFont(_chart._font, new XFont("Arial", 10, XFontStyle.Regular)));
            }
        }

        XFont _defaultDataLabelFont;
    }
}
