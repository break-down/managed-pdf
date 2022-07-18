namespace BreakDown.ManagedPdf.Core.Drawing.Internal
{
    /// <summary>
    /// Contains data needed for PDF. Will be prepared when needed.
    /// </summary>
    internal class ImageDataDct : ImageData
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        public byte[] Data
        {
            get { return _data; }
            internal set { _data = value; }
        }

        private byte[] _data;

        /// <summary>
        /// Gets the length.
        /// </summary>
        public int Length
        {
            get { return _length; }
            internal set { _length = value; }
        }

        private int _length;
    }
}
