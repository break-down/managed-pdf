// "Therefore those skilled at the unorthodox
// are infinite as heaven and earth,
// inexhaustible as the great rivers.
// When they come to an end,
// they begin again,
// like the days and months;
// they die and are reborn,
// like the four seasons."
// 
// - Sun Tsu,
// "The Art of War"

using BreakDown.ManagedPdf.Html.Adapters;

namespace BreakDown.ManagedPdf.HtmlRenderer.Adapters
{
    internal sealed class BrushAdapter : RBrush
    {
        public BrushAdapter(object brush)
        {
            Brush = brush;
        }

        /// <summary>
        /// The actual WinForms brush instance.
        /// </summary>
        public object Brush { get; }

        public override void Dispose()
        {
        }
    }
}
