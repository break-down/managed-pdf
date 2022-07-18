using System;
using BreakDown.ManagedPdf.Core.Fonts.OpenType.enums;
using BreakDown.ManagedPdf.Core.root;

namespace BreakDown.ManagedPdf.Core.Fonts.OpenType
{
    /// <summary>
    /// This table is similar to the CVT Program, except that it is only run once, when the font is first used.
    /// It is used only for FDEFs and IDEFs. Thus the CVT Program need not contain function definitions.
    /// However, the CVT Program may redefine existing FDEFs or IDEFs. 
    /// </summary>
    internal class FontProgram : OpenTypeFontTable
    {
        public const string Tag = TableTagNames.Fpgm;

        byte[] bytes; // Instructions. n is the number of BYTE items that fit in the size of the table.

        public FontProgram(OpenTypeFontface fontData)
            : base(fontData, Tag)
        {
            DirectoryEntry.Tag = TableTagNames.Fpgm;
            DirectoryEntry = fontData.TableDictionary[TableTagNames.Fpgm];
            Read();
        }

        public void Read()
        {
            try
            {
                var length = DirectoryEntry.Length;
                bytes = new byte[length];
                for (var idx = 0; idx < length; idx++)
                {
                    bytes[idx] = _fontData.ReadByte();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(PSSR.ErrorReadingFontData, ex);
            }
        }
    }
}
