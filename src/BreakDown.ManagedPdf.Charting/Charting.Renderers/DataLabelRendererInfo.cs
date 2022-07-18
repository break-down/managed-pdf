using BreakDown.ManagedPdf.Charting.Charting.enums;
using BreakDown.ManagedPdf.Core.Drawing;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// Stores data label specific rendering information.
    /// </summary>
    internal class DataLabelRendererInfo : RendererInfo
    {
        internal DataLabelEntryRendererInfo[] Entries;

        internal string Format;
        internal XFont Font;
        internal XBrush FontColor;
        internal DataLabelPosition Position;
        internal DataLabelType Type;
    }
}
