using System;
using System.Diagnostics;
using System.Text;
using BreakDown.ManagedPdf.Core.Fonts.OpenType.enums;
using BreakDown.ManagedPdf.Core.root;

namespace BreakDown.ManagedPdf.Core.Fonts.OpenType
{
    /// <summary>
    /// The naming table allows multilingual strings to be associated with the OpenTypeTM font file.
    /// These strings can represent copyright notices, font names, family names, style names, and so on.
    /// To keep this table short, the font manufacturer may wish to make a limited set of entries in some
    /// small set of languages; later, the font can be "localized" and the strings translated or added.
    /// Other parts of the OpenType font file that require these strings can then refer to them simply by
    /// their index number. Clients that need a particular string can look it up by its platform ID, character
    /// encoding ID, language ID and name ID. Note that some platforms may require single byte character
    /// strings, while others may require double byte strings. 
    ///
    /// For historical reasons, some applications which install fonts perform Version control using Macintosh
    /// platform (platform ID 1) strings from the 'name' table. Because of this, we strongly recommend that
    /// the 'name' table of all fonts include Macintosh platform strings and that the syntax of the Version
    /// number (name id 5) follows the guidelines given in this document.
    /// </summary>
    internal class NameTable : OpenTypeFontTable
    {
        public const string Tag = TableTagNames.Name;

        /// <summary>
        /// Get the font family name.
        /// </summary>
        public string Name = String.Empty;

        /// <summary>
        /// Get the font subfamily name.
        /// </summary>
        public string Style = String.Empty;

        /// <summary>
        /// Get the full font name.
        /// </summary>
        public string FullFontName = String.Empty;

        public ushort format;
        public ushort count;
        public ushort stringOffset;

        byte[] bytes;

        public NameTable(OpenTypeFontface fontData)
            : base(fontData, Tag)
        {
            Read();
        }

        public void Read()
        {
            try
            {
#if DEBUG
                _fontData.Position = DirectoryEntry.Offset;
#endif
                bytes = new byte[DirectoryEntry.PaddedLength];
                Buffer.BlockCopy(_fontData.FontSource.Bytes, DirectoryEntry.Offset, bytes, 0, DirectoryEntry.Length);

                format = _fontData.ReadUShort();
                count = _fontData.ReadUShort();
                stringOffset = _fontData.ReadUShort();

                for (var idx = 0; idx < count; idx++)
                {
                    var nrec = ReadNameRecord();
                    var value = new byte[nrec.length];
                    Buffer.BlockCopy(_fontData.FontSource.Bytes, DirectoryEntry.Offset + stringOffset + nrec.offset, value, 0, nrec.length);

                    //Debug.WriteLine(nrec.platformID.ToString());

                    // Read font name and style in US English.
                    if (nrec.platformID == 0 || nrec.platformID == 3)
                    {
                        // Font Family name. Up to four fonts can share the Font Family name, 
                        // forming a font style linking group (regular, italic, bold, bold italic - 
                        // as defined by OS/2.fsSelection bit settings).
                        if (nrec.nameID == 1 && nrec.languageID == 0x0409)
                        {
                            if (String.IsNullOrEmpty(Name))
                            {
                                Name = Encoding.BigEndianUnicode.GetString(value, 0, value.Length);
                            }
                        }

                        // Font Subfamily name. The Font Subfamily name distinguishes the font in a 
                        // group with the same Font Family name (name ID 1). This is assumed to
                        // address style (italic, oblique) and weight (light, bold, black, etc.).
                        // A font with no particular differences in weight or style (e.g. medium weight,
                        // not italic and fsSelection bit 6 set) should have the string “Regular” stored in 
                        // this position.
                        if (nrec.nameID == 2 && nrec.languageID == 0x0409)
                        {
                            if (String.IsNullOrEmpty(Style))
                            {
                                Style = Encoding.BigEndianUnicode.GetString(value, 0, value.Length);
                            }
                        }

                        // Full font name; a combination of strings 1 and 2, or a similar human-readable
                        // variant. If string 2 is "Regular", it is sometimes omitted from name ID 4.
                        if (nrec.nameID == 4 && nrec.languageID == 0x0409)
                        {
                            if (String.IsNullOrEmpty(FullFontName))
                            {
                                FullFontName = Encoding.BigEndianUnicode.GetString(value, 0, value.Length);
                            }
                        }
                    }
                }

                Debug.Assert(!String.IsNullOrEmpty(Name));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(PSSR.ErrorReadingFontData, ex);
            }
        }

        NameRecord ReadNameRecord()
        {
            var nrec = new NameRecord();
            nrec.platformID = _fontData.ReadUShort();
            nrec.encodingID = _fontData.ReadUShort();
            nrec.languageID = _fontData.ReadUShort();
            nrec.nameID = _fontData.ReadUShort();
            nrec.length = _fontData.ReadUShort();
            nrec.offset = _fontData.ReadUShort();
            return nrec;
        }

        class NameRecord
        {
            public ushort platformID;
            public ushort encodingID;
            public ushort languageID;
            public ushort nameID;
            public ushort length;
            public ushort offset;
        }
    }
}
