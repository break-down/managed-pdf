using BreakDown.ManagedPdf.Core.Drawing;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// Stores legend specific rendering information.
    /// </summary>
    internal class LegendRendererInfo : AreaRendererInfo
    {
        internal Legend _legend;

        internal XFont Font;
        internal XBrush FontColor;
        internal XPen BorderPen;
        internal LegendEntryRendererInfo[] Entries;
    }
}
