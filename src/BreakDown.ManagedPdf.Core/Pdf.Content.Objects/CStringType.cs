namespace BreakDown.ManagedPdf.Core.Pdf.Content.Objects
{
    /// <summary>
    /// Type of the parsed string.
    /// </summary>
    public enum CStringType
    {
        /// <summary>
        /// The string has the format "(...)".
        /// </summary>
        String,

        /// <summary>
        /// The string has the format "&lt;...&gt;".
        /// </summary>
        HexString,

        /// <summary>
        /// The string... TODO.
        /// </summary>
        UnicodeString,

        /// <summary>
        /// The string... TODO.
        /// </summary>
        UnicodeHexString,

        /// <summary>
        /// HACK: The string is the content of a dictionary.
        /// Currently there is no parser for dictionaries in Content Streams.
        /// </summary>
        Dictionary,
    }
}
