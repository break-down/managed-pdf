using BreakDown.ManagedPdf.Charting.Charting.enums;
using BreakDown.ManagedPdf.Core.Drawing;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    internal class AxisTitleRendererInfo : AreaRendererInfo
    {
        internal AxisTitle _axisTitle;

        internal string AxisTitleText;
        internal XFont AxisTitleFont;
        internal XBrush AxisTitleBrush;
        internal double AxisTitleOrientation;
        internal HorizontalAlignment AxisTitleAlignment;
        internal VerticalAlignment AxisTitleVerticalAlignment;
        internal XSize AxisTitleSize;
    }
}
