using BreakDown.ManagedPdf.Charting.Charting.enums;
using BreakDown.ManagedPdf.Core.Drawing;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// Represents a description of a marker for a line chart.
    /// </summary>
    internal class MarkerRendererInfo : RendererInfo
    {
        internal XUnit MarkerSize;
        internal MarkerStyle MarkerStyle;
        internal XColor MarkerForegroundColor;
        internal XColor MarkerBackgroundColor;
    }
}
