namespace BreakDown.ManagedPdf.Core.SharpZipLib.Zip
{
    /// <summary>
    /// Determines how entries are tested to see if they should use Zip64 extensions or not.
    /// </summary>
    public enum UseZip64
    {
        /// <summary>
        /// Zip64 will not be forced on entries during processing.
        /// </summary>
        /// <remarks>An entry can have this overridden if required ZipEntry.ForceZip64"</remarks>
        Off,

        /// <summary>
        /// Zip64 should always be used.
        /// </summary>
        On,

        /// <summary>
        /// #ZipLib will determine use based on entry values when added to archive.
        /// </summary>
        Dynamic,
    }
}
