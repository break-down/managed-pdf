using System;

namespace BreakDown.ManagedPdf.Core.Pdf.Advanced
{
    /// <summary>
    /// The PDF font descriptor flags.
    /// </summary>
    [Flags]
    enum PdfFontDescriptorFlags
    {
        /// <summary>
        /// All glyphs have the same width (as opposed to proportional or variable-pitch
        /// fonts, which have different widths).
        /// </summary>
        FixedPitch = 1 << 0,

        /// <summary>
        /// Glyphs have serifs, which are short strokes drawn at an angle on the top and
        /// bottom of glyph stems. (Sans serif fonts do not have serifs.)
        /// </summary>
        Serif = 1 << 1,

        /// <summary>
        /// Font contains glyphs outside the Adobe standard Latin character set. This
        /// flag and the Nonsymbolic flag cannot both be set or both be clear.
        /// </summary>
        Symbolic = 1 << 2,

        /// <summary>
        /// Glyphs resemble cursive handwriting.
        /// </summary>
        Script = 1 << 3,

        /// <summary>
        /// Font uses the Adobe standard Latin character set or a subset of it.
        /// </summary>
        Nonsymbolic = 1 << 5,

        /// <summary>
        /// Glyphs have dominant vertical strokes that are slanted.
        /// </summary>
        Italic = 1 << 6,

        /// <summary>
        /// Font contains no lowercase letters; typically used for display purposes,
        /// such as for titles or headlines.
        /// </summary>
        AllCap = 1 << 16,

        /// <summary>
        /// Font contains both uppercase and lowercase letters. The uppercase letters are
        /// similar to those in the regular version of the same typeface family. The glyphs
        /// for the lowercase letters have the same shapes as the corresponding uppercase
        /// letters, but they are sized and their proportions adjusted so that they have the
        /// same size and stroke weight as lowercase glyphs in the same typeface family.
        /// </summary>
        SmallCap = 1 << 17,

        /// <summary>
        /// Determines whether bold glyphs are painted with extra pixels even at very small
        /// text sizes.
        /// </summary>
        ForceBold = 1 << 18,
    }
}
