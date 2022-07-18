namespace BreakDown.ManagedPdf.Core.Pdf.IO
{
    /// <summary>
    /// A delegated used by the PdfReader.Open function to retrieve a password if the document is protected.
    /// </summary>
    public delegate void PdfPasswordProvider(PdfPasswordProviderArgs args);
}
