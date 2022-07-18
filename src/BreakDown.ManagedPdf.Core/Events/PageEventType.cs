namespace BreakDown.ManagedPdf.Core.Events
{
    /// <summary>
    /// The event type of a PageEvent.
    /// </summary>
    public enum PageEventType
    {
        /// <summary>
        /// A new page was created.
        /// </summary>
        Created,

        /// <summary>
        /// A page was moved.
        /// </summary>
        Moved,

        /// <summary>
        /// A page was imported from another document.
        /// </summary>
        Imported,

        /// <summary>
        /// A page was removed.
        /// </summary>
        Removed
    }
}
