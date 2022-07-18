using System;
using BreakDown.ManagedPdf.Core.Fonts.OpenType.enums;
using BreakDown.ManagedPdf.Core.root;

namespace BreakDown.ManagedPdf.Core.Fonts.OpenType
{
    /// <summary>
    /// This table contains a list of values that can be referenced by instructions.
    /// They can be used, among other things, to control characteristics for different glyphs.
    /// The length of the table must be an integral number of FWORD units. 
    /// </summary>
    internal class ControlValueTable : OpenTypeFontTable
    {
        public const string Tag = TableTagNames.Cvt;

        Int16[] array; // List of n values referenceable by instructions. n is the number of FWORD items that fit in the size of the table.

        public ControlValueTable(OpenTypeFontface fontData)
            : base(fontData, Tag)
        {
            DirectoryEntry.Tag = TableTagNames.Cvt;
            DirectoryEntry = fontData.TableDictionary[TableTagNames.Cvt];
            Read();
        }

        public void Read()
        {
            try
            {
                var length = DirectoryEntry.Length / 2;
                array = new Int16[length];
                for (var idx = 0; idx < length; idx++)
                {
                    array[idx] = _fontData.ReadFWord();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(PSSR.ErrorReadingFontData, ex);
            }
        }
    }
}
