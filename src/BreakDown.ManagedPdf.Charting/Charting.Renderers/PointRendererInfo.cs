using BreakDown.ManagedPdf.Core.Drawing;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// PointRendererInfo is used to render one single data point which is part of a data series.
    /// </summary>
    internal class PointRendererInfo : RendererInfo
    {
        internal Point Point;

        internal XPen LineFormat;
        internal XBrush FillFormat;
    }
}
