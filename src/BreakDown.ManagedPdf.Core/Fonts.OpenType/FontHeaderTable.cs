using System;
using BreakDown.ManagedPdf.Core.Fonts.OpenType.enums;
using BreakDown.ManagedPdf.Core.root;

namespace BreakDown.ManagedPdf.Core.Fonts.OpenType
{
    /// <summary>
    /// This table gives global information about the font. The bounding box values should be computed using 
    /// only glyphs that have contours. Glyphs with no contours should be ignored for the purposes of these calculations.
    /// </summary>
    internal class FontHeaderTable : OpenTypeFontTable
    {
        public const string Tag = TableTagNames.Head;

        public Int32 version; // 0x00010000 for Version 1.0.
        public Int32 fontRevision;
        public uint checkSumAdjustment;
        public uint magicNumber; // Set to 0x5F0F3CF5
        public ushort flags;
        public ushort unitsPerEm; // Valid range is from 16 to 16384. This value should be a power of 2 for fonts that have TrueType outlines.
        public long created;
        public long modified;
        public short xMin, yMin; // For all glyph bounding boxes.
        public short xMax, yMax; // For all glyph bounding boxes.
        public ushort macStyle;
        public ushort lowestRecPPEM;
        public short fontDirectionHint;
        public short indexToLocFormat; // 0 for short offsets, 1 for long
        public short glyphDataFormat; // 0 for current format

        public FontHeaderTable(OpenTypeFontface fontData)
            : base(fontData, Tag)
        {
            Read();
        }

        public void Read()
        {
            try
            {
                version = _fontData.ReadFixed();
                fontRevision = _fontData.ReadFixed();
                checkSumAdjustment = _fontData.ReadULong();
                magicNumber = _fontData.ReadULong();
                flags = _fontData.ReadUShort();
                unitsPerEm = _fontData.ReadUShort();
                created = _fontData.ReadLongDate();
                modified = _fontData.ReadLongDate();
                xMin = _fontData.ReadShort();
                yMin = _fontData.ReadShort();
                xMax = _fontData.ReadShort();
                yMax = _fontData.ReadShort();
                macStyle = _fontData.ReadUShort();
                lowestRecPPEM = _fontData.ReadUShort();
                fontDirectionHint = _fontData.ReadShort();
                indexToLocFormat = _fontData.ReadShort();
                glyphDataFormat = _fontData.ReadShort();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(PSSR.ErrorReadingFontData, ex);
            }
        }
    }
}
