using BreakDown.ManagedPdf.Core.Pdf;

namespace BreakDown.ManagedPdf.Core.Drawing.Internal
{
    /// <summary>
    /// Bitmap refers to the format used in PDF. Will be used for BMP, PNG, TIFF, GIF and others.
    /// </summary>
    internal class ImportedImageBitmap : ImportedImage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportedImageBitmap"/> class.
        /// </summary>
        public ImportedImageBitmap(IImageImporter importer, ImagePrivateDataBitmap data, PdfDocument document)
            : base(importer, data, document)
        {
        }

        internal override ImageData PrepareImageData()
        {
            var data = (ImagePrivateDataBitmap)Data;
            var imageData = new ImageDataBitmap(_document);

            //imageData.Data = data.Data;
            //imageData.Length = data.Length;

            data.CopyBitmap(imageData);

            return imageData;
        }
    }
}
