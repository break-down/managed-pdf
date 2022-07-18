namespace BreakDown.ManagedPdf.Core.Events
{
    /// <summary>
    /// EventHandler for OnPageAdded and OnPageRemoved.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The PageEventArgs of the event.</param>
    public delegate void PageAddedOrRemovedEventHandler(object sender, PageEventArgs e);
}
