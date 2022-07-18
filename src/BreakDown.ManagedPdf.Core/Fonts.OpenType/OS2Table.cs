using System;
using BreakDown.ManagedPdf.Core.Fonts.OpenType.enums;
using BreakDown.ManagedPdf.Core.root;

namespace BreakDown.ManagedPdf.Core.Fonts.OpenType
{
    /// <summary>
    /// The OS/2 table consists of a set of Metrics that are required in OpenType fonts. 
    /// </summary>
    internal class OS2Table : OpenTypeFontTable
    {
        public const string Tag = TableTagNames.OS2;

        [Flags]
        public enum FontSelectionFlags : ushort
        {
            Italic = 1 << 0,
            Bold = 1 << 5,
            Regular = 1 << 6,
        }

        public ushort version;
        public short xAvgCharWidth;
        public ushort usWeightClass;
        public ushort usWidthClass;
        public ushort fsType;
        public short ySubscriptXSize;
        public short ySubscriptYSize;
        public short ySubscriptXOffset;
        public short ySubscriptYOffset;
        public short ySuperscriptXSize;
        public short ySuperscriptYSize;
        public short ySuperscriptXOffset;
        public short ySuperscriptYOffset;
        public short yStrikeoutSize;
        public short yStrikeoutPosition;
        public short sFamilyClass;
        public byte[] panose; // = new byte[10];
        public uint ulUnicodeRange1; // Bits 0-31
        public uint ulUnicodeRange2; // Bits 32-63
        public uint ulUnicodeRange3; // Bits 64-95
        public uint ulUnicodeRange4; // Bits 96-127
        public string achVendID; // = "";
        public ushort fsSelection;
        public ushort usFirstCharIndex;
        public ushort usLastCharIndex;
        public short sTypoAscender;
        public short sTypoDescender;
        public short sTypoLineGap;
        public ushort usWinAscent;

        public ushort usWinDescent;

        // Version >= 1
        public uint ulCodePageRange1; // Bits 0-31

        public uint ulCodePageRange2; // Bits 32-63

        // Version >= 2
        public short sxHeight;
        public short sCapHeight;
        public ushort usDefaultChar;
        public ushort usBreakChar;
        public ushort usMaxContext;

        public OS2Table(OpenTypeFontface fontData)
            : base(fontData, Tag)
        {
            Read();
        }

        public void Read()
        {
            try
            {
                version = _fontData.ReadUShort();
                xAvgCharWidth = _fontData.ReadShort();
                usWeightClass = _fontData.ReadUShort();
                usWidthClass = _fontData.ReadUShort();
                fsType = _fontData.ReadUShort();
                ySubscriptXSize = _fontData.ReadShort();
                ySubscriptYSize = _fontData.ReadShort();
                ySubscriptXOffset = _fontData.ReadShort();
                ySubscriptYOffset = _fontData.ReadShort();
                ySuperscriptXSize = _fontData.ReadShort();
                ySuperscriptYSize = _fontData.ReadShort();
                ySuperscriptXOffset = _fontData.ReadShort();
                ySuperscriptYOffset = _fontData.ReadShort();
                yStrikeoutSize = _fontData.ReadShort();
                yStrikeoutPosition = _fontData.ReadShort();
                sFamilyClass = _fontData.ReadShort();
                panose = _fontData.ReadBytes(10);
                ulUnicodeRange1 = _fontData.ReadULong();
                ulUnicodeRange2 = _fontData.ReadULong();
                ulUnicodeRange3 = _fontData.ReadULong();
                ulUnicodeRange4 = _fontData.ReadULong();
                achVendID = _fontData.ReadString(4);
                fsSelection = _fontData.ReadUShort();
                usFirstCharIndex = _fontData.ReadUShort();
                usLastCharIndex = _fontData.ReadUShort();
                sTypoAscender = _fontData.ReadShort();
                sTypoDescender = _fontData.ReadShort();
                sTypoLineGap = _fontData.ReadShort();
                usWinAscent = _fontData.ReadUShort();
                usWinDescent = _fontData.ReadUShort();

                if (version >= 1)
                {
                    ulCodePageRange1 = _fontData.ReadULong();
                    ulCodePageRange2 = _fontData.ReadULong();

                    if (version >= 2)
                    {
                        sxHeight = _fontData.ReadShort();
                        sCapHeight = _fontData.ReadShort();
                        usDefaultChar = _fontData.ReadUShort();
                        usBreakChar = _fontData.ReadUShort();
                        usMaxContext = _fontData.ReadUShort();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(PSSR.ErrorReadingFontData, ex);
            }
        }

        public bool IsBold
        {
            get { return (fsSelection & (ushort)FontSelectionFlags.Bold) != 0; }
        }

        public bool IsItalic
        {
            get { return (fsSelection & (ushort)FontSelectionFlags.Italic) != 0; }
        }
    }
}
