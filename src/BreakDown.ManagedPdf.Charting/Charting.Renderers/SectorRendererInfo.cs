using BreakDown.ManagedPdf.Core.Drawing;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// Represents one sector of a series used by a pie chart.
    /// </summary>
    internal class SectorRendererInfo : PointRendererInfo
    {
        internal XRect Rect;
        internal double StartAngle;
        internal double SweepAngle;
    }
}
