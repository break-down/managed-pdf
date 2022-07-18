namespace BreakDown.ManagedPdf.Core.Events
{
    /// <summary>
    /// The action type of a PageGraphicsEvent.
    /// </summary>
    public enum PageGraphicsActionType
    {
        /// <summary>
        /// The XGraphics object for the page was created.
        /// </summary>
        GraphicsCreated = 1,

        /// <summary>
        /// DrawString() was called on the page's XGraphics object.
        /// </summary>
        DrawString,

        /// <summary>
        /// Another method drawing content was called on the page's XGraphics object.
        /// </summary>
        Draw
    }
}
