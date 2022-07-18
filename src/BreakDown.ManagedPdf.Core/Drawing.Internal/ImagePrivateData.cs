namespace BreakDown.ManagedPdf.Core.Drawing.Internal
{
    /// <summary>
    /// Contains internal data. This includes a reference to the Stream if data for PDF was not yet prepared.
    /// </summary>
    internal abstract class ImagePrivateData
    {
        internal ImagePrivateData()
        {
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        public ImportedImage Image
        {
            get { return _image; }
            internal set { _image = value; }
        }

        private ImportedImage _image;
    }
}
