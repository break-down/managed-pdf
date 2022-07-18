using System;
using BreakDown.ManagedPdf.Core.Fonts.OpenType.enums;
using BreakDown.ManagedPdf.Core.root;

namespace BreakDown.ManagedPdf.Core.Fonts.OpenType
{
    /// <summary>
    /// This table contains additional information needed to use TrueType or OpenTypeTM fonts
    /// on PostScript printers. 
    /// </summary>
    internal class PostScriptTable : OpenTypeFontTable
    {
        public const string Tag = TableTagNames.Post;

        public Int32 formatType;
        public float italicAngle;
        public Int16 underlinePosition;
        public Int16 underlineThickness;
        public ulong isFixedPitch;
        public ulong minMemType42;
        public ulong maxMemType42;
        public ulong minMemType1;
        public ulong maxMemType1;

        public PostScriptTable(OpenTypeFontface fontData)
            : base(fontData, Tag)
        {
            Read();
        }

        public void Read()
        {
            try
            {
                formatType = _fontData.ReadFixed();
                italicAngle = _fontData.ReadFixed() / 65536f;
                underlinePosition = _fontData.ReadFWord();
                underlineThickness = _fontData.ReadFWord();
                isFixedPitch = _fontData.ReadULong();
                minMemType42 = _fontData.ReadULong();
                maxMemType42 = _fontData.ReadULong();
                minMemType1 = _fontData.ReadULong();
                maxMemType1 = _fontData.ReadULong();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(PSSR.ErrorReadingFontData, ex);
            }
        }
    }
}
