using System;
using BreakDown.ManagedPdf.Core.root;

namespace BreakDown.ManagedPdf.Core.Fonts.OpenType
{
    internal class HorizontalMetrics : OpenTypeFontTable
    {
        public const string Tag = "----";

        public ushort advanceWidth;
        public short lsb;

        public HorizontalMetrics(OpenTypeFontface fontData)
            : base(fontData, Tag)
        {
            Read();
        }

        public void Read()
        {
            try
            {
                advanceWidth = _fontData.ReadUFWord();
                lsb = _fontData.ReadFWord();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(PSSR.ErrorReadingFontData, ex);
            }
        }
    }
}
