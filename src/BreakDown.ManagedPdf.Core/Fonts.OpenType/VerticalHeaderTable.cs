using System;
using BreakDown.ManagedPdf.Core.Fonts.OpenType.enums;
using BreakDown.ManagedPdf.Core.root;

namespace BreakDown.ManagedPdf.Core.Fonts.OpenType
{
    internal class VerticalHeaderTable : OpenTypeFontTable
    {
        public const string Tag = TableTagNames.VHea;

        // code comes from HorizontalHeaderTable
        public Int32 Version; // 0x00010000 for Version 1.0.
        public Int16 Ascender; // Typographic ascent. (Distance from baseline of highest Ascender) 
        public Int16 Descender; // Typographic descent. (Distance from baseline of lowest Descender) 
        public Int16 LineGap; // Typographic line gap. Negative LineGap values are treated as zero in Windows 3.1, System 6, and System 7.
        public UInt16 AdvanceWidthMax;
        public Int16 MinLeftSideBearing;
        public Int16 MinRightSideBearing;
        public Int16 xMaxExtent;
        public short caretSlopeRise;
        public short caretSlopeRun;
        public short reserved1;
        public short reserved2;
        public short reserved3;
        public short reserved4;
        public short reserved5;
        public short metricDataFormat;
        public ushort numberOfHMetrics;

        public VerticalHeaderTable(OpenTypeFontface fontData)
            : base(fontData, Tag)
        {
            Read();
        }

        public void Read()
        {
            try
            {
                Version = _fontData.ReadFixed();
                Ascender = _fontData.ReadFWord();
                Descender = _fontData.ReadFWord();
                LineGap = _fontData.ReadFWord();
                AdvanceWidthMax = _fontData.ReadUFWord();
                MinLeftSideBearing = _fontData.ReadFWord();
                MinRightSideBearing = _fontData.ReadFWord();
                xMaxExtent = _fontData.ReadFWord();
                caretSlopeRise = _fontData.ReadShort();
                caretSlopeRun = _fontData.ReadShort();
                reserved1 = _fontData.ReadShort();
                reserved2 = _fontData.ReadShort();
                reserved3 = _fontData.ReadShort();
                reserved4 = _fontData.ReadShort();
                reserved5 = _fontData.ReadShort();
                metricDataFormat = _fontData.ReadShort();
                numberOfHMetrics = _fontData.ReadUShort();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(PSSR.ErrorReadingFontData, ex);
            }
        }
    }
}
