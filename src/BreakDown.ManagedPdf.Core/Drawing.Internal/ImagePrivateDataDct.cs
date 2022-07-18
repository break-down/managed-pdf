namespace BreakDown.ManagedPdf.Core.Drawing.Internal
{
    /// <summary>
    /// Private data for JPEG images.
    /// </summary>
    internal class ImagePrivateDataDct : ImagePrivateData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePrivateDataDct"/> class.
        /// </summary>
        public ImagePrivateDataDct(byte[] data, int length)
        {
            _data = data;
            _length = length;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public byte[] Data
        {
            get { return _data; }

            //internal set { _data = value; }
        }

        private readonly byte[] _data;

        /// <summary>
        /// Gets the length.
        /// </summary>
        public int Length
        {
            get { return _length; }

            //internal set { _length = value; }
        }

        private readonly int _length;
    }
}
