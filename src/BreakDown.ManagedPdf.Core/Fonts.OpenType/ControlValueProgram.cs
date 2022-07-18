using System;
using BreakDown.ManagedPdf.Core.Fonts.OpenType.enums;
using BreakDown.ManagedPdf.Core.root;

namespace BreakDown.ManagedPdf.Core.Fonts.OpenType
{
    /// <summary>
    /// The Control Value Program consists of a set of TrueType instructions that will be executed whenever the font or 
    /// point size or transformation matrix change and before each glyph is interpreted. Any instruction is legal in the
    /// CVT Program but since no glyph is associated with it, instructions intended to move points within a particular
    /// glyph outline cannot be used in the CVT Program. The name 'prep' is anachronistic. 
    /// </summary>
    internal class ControlValueProgram : OpenTypeFontTable
    {
        public const string Tag = TableTagNames.Prep;

        byte[]
            bytes; // Set of instructions executed whenever point size or font or transformation change. n is the number of BYTE items that fit in the size of the table.

        public ControlValueProgram(OpenTypeFontface fontData)
            : base(fontData, Tag)
        {
            DirectoryEntry.Tag = TableTagNames.Prep;
            DirectoryEntry = fontData.TableDictionary[TableTagNames.Prep];
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
