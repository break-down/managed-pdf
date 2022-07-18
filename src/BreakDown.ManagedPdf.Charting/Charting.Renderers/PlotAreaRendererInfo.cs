using BreakDown.ManagedPdf.Core.Drawing;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// Stores rendering information common to all plot area renderers.
    /// </summary>
    internal class PlotAreaRendererInfo : AreaRendererInfo
    {
        internal PlotArea _plotArea;

        /// <summary>
        /// Saves the plot area's matrix.
        /// </summary>
        internal XMatrix _matrix;

        internal XPen LineFormat;
        internal XBrush FillFormat;
    }
}
