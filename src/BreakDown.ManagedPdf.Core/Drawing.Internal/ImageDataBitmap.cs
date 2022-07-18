using BreakDown.ManagedPdf.Core.Pdf;

namespace BreakDown.ManagedPdf.Core.Drawing.Internal
{
    /// <summary>
    /// Contains data needed for PDF. Will be prepared when needed.
    /// Bitmap refers to the format used in PDF. Will be used for BMP, PNG, TIFF, GIF and others.
    /// </summary>
    internal class ImageDataBitmap : ImageData
    {
        private ImageDataBitmap()
        {
        }

        internal ImageDataBitmap(PdfDocument document)
        {
            _document = document;
        }

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

        /// <summary>
        /// Gets the data.
        /// </summary>
        public byte[] DataFax
        {
            get { return _dataFax; }
            internal set { _dataFax = value; }
        }

        private byte[] _dataFax;

        /// <summary>
        /// Gets the length.
        /// </summary>
        public int LengthFax
        {
            get { return _lengthFax; }
            internal set { _lengthFax = value; }
        }

        private int _lengthFax;

        public byte[] AlphaMask
        {
            get { return _alphaMask; }
            internal set { _alphaMask = value; }
        }

        private byte[] _alphaMask;

        public int AlphaMaskLength
        {
            get { return _alphaMaskLength; }
            internal set { _alphaMaskLength = value; }
        }

        private int _alphaMaskLength;

        public byte[] BitmapMask
        {
            get { return _bitmapMask; }
            internal set { _bitmapMask = value; }
        }

        private byte[] _bitmapMask;

        public int BitmapMaskLength
        {
            get { return _bitmapMaskLength; }
            internal set { _bitmapMaskLength = value; }
        }

        private int _bitmapMaskLength;

        public byte[] PaletteData
        {
            get { return _paletteData; }
            set { _paletteData = value; }
        }

        private byte[] _paletteData;

        public int PaletteDataLength
        {
            get { return _paletteDataLength; }
            set { _paletteDataLength = value; }
        }

        private int _paletteDataLength;

        public bool SegmentedColorMask;

        public int IsBitonal;

        public int K;

        public bool IsGray;

        internal readonly PdfDocument _document;
    }
}
