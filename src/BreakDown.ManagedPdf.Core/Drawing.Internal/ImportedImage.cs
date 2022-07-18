using System;
using BreakDown.ManagedPdf.Core.Pdf;

namespace BreakDown.ManagedPdf.Core.Drawing.Internal
{
    /// <summary>
    /// The imported image.
    /// </summary>
    internal abstract class ImportedImage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportedImage"/> class.
        /// </summary>
        protected ImportedImage(IImageImporter importer, ImagePrivateData data, PdfDocument document)
        {
            Data = data;
            _document = document;
            data.Image = this;
            _importer = importer;
        }

        /// <summary>
        /// Gets information about the image.
        /// </summary>
        public ImageInformation Information
        {
            get { return _information; }
            private set { _information = value; }
        }

        private ImageInformation _information = new ImageInformation();

        /// <summary>
        /// Gets a value indicating whether image data for the PDF file was already prepared.
        /// </summary>
        public bool HasImageData
        {
            get { return _imageData != null; }
        }

        /// <summary>
        /// Gets the image data needed for the PDF file.
        /// </summary>
        public ImageData ImageData
        {
            get
            {
                if (!HasImageData)
                {
                    _imageData = PrepareImageData();
                }

                return _imageData;
            }
            private set { _imageData = value; }
        }

        private ImageData _imageData;

        internal virtual ImageData PrepareImageData()
        {
            throw new NotImplementedException();
        }

        private IImageImporter _importer;
        internal ImagePrivateData Data;
        internal readonly PdfDocument _document;
    }
}
