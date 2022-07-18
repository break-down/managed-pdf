namespace BreakDown.ManagedPdf.Core.Drawing.Internal
{
    /// <summary>
    /// Public information about the image, filled immediately.
    /// Note: The stream will be read and decoded on the first call to PrepareImageData().
    /// ImageInformation can be filled for corrupted images that will throw an expection on PrepareImageData().
    /// </summary>
    internal class ImageInformation
    {
        internal enum ImageFormats
        {
            /// <summary>
            /// Standard JPEG format (RGB).
            /// </summary>
            JPEG,

            /// <summary>
            /// Grayscale JPEG format.
            /// </summary>
            JPEGGRAY,

            /// <summary>
            /// JPEG file with inverted CMYK, thus RGBW.
            /// </summary>
            JPEGRGBW,

            /// <summary>
            /// JPEG file with CMYK.
            /// </summary>
            JPEGCMYK,
            Palette1,
            Palette4,
            Palette8,
            RGB24,
            ARGB32
        }

        internal ImageFormats ImageFormat;

        internal uint Width;
        internal uint Height;

        /// <summary>
        /// The horizontal DPI (dots per inch). Can be 0 if not supported by the image format.
        /// Note: JFIF (JPEG) files may contain either DPI or DPM or just the aspect ratio. Windows BMP files will contain DPM. Other formats may support any combination, including none at all.
        /// </summary>
        internal decimal HorizontalDPI;

        /// <summary>
        /// The vertical DPI (dots per inch). Can be 0 if not supported by the image format.
        /// </summary>
        internal decimal VerticalDPI;

        /// <summary>
        /// The horizontal DPM (dots per meter). Can be 0 if not supported by the image format.
        /// </summary>
        internal decimal HorizontalDPM;

        /// <summary>
        /// The vertical DPM (dots per meter). Can be 0 if not supported by the image format.
        /// </summary>
        internal decimal VerticalDPM;

        /// <summary>
        /// The horizontal component of the aspect ratio. Can be 0 if not supported by the image format.
        /// Note: Aspect ratio will be set if either DPI or DPM was set, but may also be available in the absence of both DPI and DPM.
        /// </summary>
        internal decimal HorizontalAspectRatio;

        /// <summary>
        /// The vertical component of the aspect ratio. Can be 0 if not supported by the image format.
        /// </summary>
        internal decimal VerticalAspectRatio;

        /// <summary>
        /// The colors used. Only valid for images with palettes, will be 0 otherwise.
        /// </summary>
        internal uint ColorsUsed;
    }
}
