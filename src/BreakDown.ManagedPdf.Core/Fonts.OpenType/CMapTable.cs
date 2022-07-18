using System;
using BreakDown.ManagedPdf.Core.Fonts.OpenType.enums;
using BreakDown.ManagedPdf.Core.root;

namespace BreakDown.ManagedPdf.Core.Fonts.OpenType
{
    /// <summary>
    /// This table defines the mapping of character codes to the glyph index values used in the font.
    /// It may contain more than one subtable, in order to support more than one character encoding scheme.
    /// </summary>
    internal class CMapTable : OpenTypeFontTable
    {
        public const string Tag = TableTagNames.CMap;

        public ushort version;
        public ushort numTables;

        /// <summary>
        /// Is true for symbol font encoding.
        /// </summary>
        public bool symbol;

        public CMap4 cmap4;

        /// <summary>
        /// Initializes a new instance of the <see cref="CMapTable"/> class.
        /// </summary>
        public CMapTable(OpenTypeFontface fontData)
            : base(fontData, Tag)
        {
            Read();
        }

        internal void Read()
        {
            try
            {
                var tableOffset = _fontData.Position;

                version = _fontData.ReadUShort();
                numTables = _fontData.ReadUShort();
#if DEBUG_
                if (_fontData.Name == "Cambria")
                    Debug-Break.Break();
#endif

                var success = false;
                for (var idx = 0; idx < numTables; idx++)
                {
                    var platformId = (PlatformId)_fontData.ReadUShort();
                    var encodingId = (WinEncodingId)_fontData.ReadUShort();
                    var offset = _fontData.ReadLong();

                    var currentPosition = _fontData.Position;

                    // Just read Windows stuff.
                    if (platformId == PlatformId.Win && (encodingId == WinEncodingId.Symbol || encodingId == WinEncodingId.Unicode))
                    {
                        symbol = encodingId == WinEncodingId.Symbol;

                        _fontData.Position = tableOffset + offset;
                        cmap4 = new CMap4(_fontData, encodingId);
                        _fontData.Position = currentPosition;

                        // We have found what we are looking for, so break.
                        success = true;
                        break;
                    }
                }

                if (!success)
                {
                    throw new InvalidOperationException("Font has no usable platform or encoding ID. It cannot be used with PDFsharp.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(PSSR.ErrorReadingFontData, ex);
            }
        }
    }
}
