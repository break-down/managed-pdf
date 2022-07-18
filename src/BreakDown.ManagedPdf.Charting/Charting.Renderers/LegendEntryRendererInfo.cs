using BreakDown.ManagedPdf.Core.Drawing;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// Represents one description of a legend entry.
    /// </summary>
    internal class LegendEntryRendererInfo : AreaRendererInfo
    {
        internal SeriesRendererInfo _seriesRendererInfo;
        internal LegendRendererInfo _legendRendererInfo;

        internal string EntryText;

        /// <summary>
        /// Size for the marker only.
        /// </summary>
        internal XSize MarkerSize;

        internal XPen MarkerPen;
        internal XBrush MarkerBrush;

        /// <summary>
        /// Width for marker area. Extra spacing for line charts are considered.
        /// </summary>
        internal XSize MarkerArea;

        /// <summary>
        /// Size for text area.
        /// </summary>
        internal XSize TextSize;
    }
}
