namespace BreakDown.ManagedPdf.Core.Pdf
{
    /// <summary>
    /// Value creation flags. Specifies whether and how a value that does not exist is created.
    /// </summary>

// ReSharper disable InconsistentNaming
    public enum VCF

        // ReSharper restore InconsistentNaming
    {
        /// <summary>
        /// Don't create the value.
        /// </summary>
        None,

        /// <summary>
        /// Create the value as direct object.
        /// </summary>
        Create,

        /// <summary>
        /// Create the value as indirect object.
        /// </summary>
        CreateIndirect,
    }
}
