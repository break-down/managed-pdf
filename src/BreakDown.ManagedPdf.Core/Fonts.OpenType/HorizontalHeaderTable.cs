using System;
using BreakDown.ManagedPdf.Core.Fonts.OpenType.enums;
using BreakDown.ManagedPdf.Core.root;

namespace BreakDown.ManagedPdf.Core.Fonts.OpenType
{
    /// <summary>
    /// This table contains information for horizontal layout. The values in the minRightSidebearing, 
    /// MinLeftSideBearing and xMaxExtent should be computed using only glyphs that have contours.
    /// Glyphs with no contours should be ignored for the purposes of these calculations.
    /// All reserved areas must be set to 0. 
    /// </summary>
    internal class HorizontalHeaderTable : OpenTypeFontTable
    {
        public const string Tag = TableTagNames.HHea;

        public Int32 version; // 0x00010000 for Version 1.0.
        public Int16 ascender; // Typographic ascent. (Distance from baseline of highest Ascender) 
        public Int16 descender; // Typographic descent. (Distance from baseline of lowest Descender) 
        public Int16 lineGap; // Typographic line gap. Negative LineGap values are treated as zero in Windows 3.1, System 6, and System 7.
        public UInt16 advanceWidthMax;
        public Int16 minLeftSideBearing;
        public Int16 minRightSideBearing;
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

        public HorizontalHeaderTable(OpenTypeFontface fontData)
            : base(fontData, Tag)
        {
            Read();
        }

        public void Read()
        {
            try
            {
                version = _fontData.ReadFixed();
                ascender = _fontData.ReadFWord();
                descender = _fontData.ReadFWord();
                lineGap = _fontData.ReadFWord();
                advanceWidthMax = _fontData.ReadUFWord();
                minLeftSideBearing = _fontData.ReadFWord();
                minRightSideBearing = _fontData.ReadFWord();
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
