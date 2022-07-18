using BreakDown.ManagedPdf.Core.Pdf;

namespace BreakDown.ManagedPdf.Core.Drawing.Internal
{
    /// <summary>
    /// Imported JPEG image.
    /// </summary>
    internal class ImportedImageJpeg : ImportedImage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportedImageJpeg"/> class.
        /// </summary>
        public ImportedImageJpeg(IImageImporter importer, ImagePrivateDataDct data, PdfDocument document)
            : base(importer, data, document)
        {
        }

        internal override ImageData PrepareImageData()
        {
            var data = (ImagePrivateDataDct)Data;
            var imageData = new ImageDataDct();
            imageData.Data = data.Data;
            imageData.Length = data.Length;

            return imageData;
        }
    }
}
