using System;
using BreakDown.ManagedPdf.Core.Fonts.OpenType.enums;
using BreakDown.ManagedPdf.Core.root;

namespace BreakDown.ManagedPdf.Core.Fonts.OpenType
{
    /// <summary>
    /// This table establishes the memory requirements for this font.
    /// Fonts with CFF data must use Version 0.5 of this table, specifying only the numGlyphs field.
    /// Fonts with TrueType outlines must use Version 1.0 of this table, where all data is required.
    /// Both formats of OpenType require a 'maxp' table because a number of applications call the 
    /// Windows GetFontData() API on the 'maxp' table to determine the number of glyphs in the font.
    /// </summary>
    internal class MaximumProfileTable : OpenTypeFontTable
    {
        public const string Tag = TableTagNames.MaxP;

        public Int32 version;
        public ushort numGlyphs;
        public ushort maxPoints;
        public ushort maxContours;
        public ushort maxCompositePoints;
        public ushort maxCompositeContours;
        public ushort maxZones;
        public ushort maxTwilightPoints;
        public ushort maxStorage;
        public ushort maxFunctionDefs;
        public ushort maxInstructionDefs;
        public ushort maxStackElements;
        public ushort maxSizeOfInstructions;
        public ushort maxComponentElements;
        public ushort maxComponentDepth;

        public MaximumProfileTable(OpenTypeFontface fontData)
            : base(fontData, Tag)
        {
            Read();
        }

        public void Read()
        {
            try
            {
                version = _fontData.ReadFixed();
                numGlyphs = _fontData.ReadUShort();
                maxPoints = _fontData.ReadUShort();
                maxContours = _fontData.ReadUShort();
                maxCompositePoints = _fontData.ReadUShort();
                maxCompositeContours = _fontData.ReadUShort();
                maxZones = _fontData.ReadUShort();
                maxTwilightPoints = _fontData.ReadUShort();
                maxStorage = _fontData.ReadUShort();
                maxFunctionDefs = _fontData.ReadUShort();
                maxInstructionDefs = _fontData.ReadUShort();
                maxStackElements = _fontData.ReadUShort();
                maxSizeOfInstructions = _fontData.ReadUShort();
                maxComponentElements = _fontData.ReadUShort();
                maxComponentDepth = _fontData.ReadUShort();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(PSSR.ErrorReadingFontData, ex);
            }
        }
    }
}
