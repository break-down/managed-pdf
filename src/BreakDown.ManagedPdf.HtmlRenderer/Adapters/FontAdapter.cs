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

using BreakDown.ManagedPdf.Core.Drawing;
using BreakDown.ManagedPdf.Html.Adapters;

namespace BreakDown.ManagedPdf.HtmlRenderer.Adapters
{
    /// <summary>
    /// Adapter for WinForms Font object for core.
    /// </summary>
    internal sealed class FontAdapter : RFont
    {
        #region Fields and Consts

        /// <summary>
        /// the vertical offset of the font underline location from the top of the font.
        /// </summary>
        private double underlineOffset = -1;

        /// <summary>
        /// Cached font height.
        /// </summary>
        private double height = -1;

        /// <summary>
        /// Cached font whitespace width.
        /// </summary>
        private double whitespaceWidth = -1;

        #endregion

        /// <summary>
        /// Init.
        /// </summary>
        public FontAdapter(XFont font)
        {
            Font = font;
        }

        /// <summary>
        /// the underline win-forms font.
        /// </summary>
        public XFont Font { get; }

        public override double Size => Font.Size;

        public override double UnderlineOffset => underlineOffset;

        public override double Height => height;

        public override double LeftPadding => height / 6f;

        public override double GetWhitespaceWidth(RGraphics graphics)
        {
            if (whitespaceWidth < 0)
            {
                whitespaceWidth = graphics.MeasureString(" ", this).Width;
            }

            return whitespaceWidth;
        }

        /// <summary>
        /// Set font metrics to be cached for the font for future use.
        /// </summary>
        /// <param name="newHeight">the full height of the font</param>
        /// <param name="newUnderlineOffset">the vertical offset of the font underline location from the top of the font.</param>
        internal void SetMetrics(int newHeight, int newUnderlineOffset)
        {
            height = newHeight;
            underlineOffset = newUnderlineOffset;
        }
    }
}
