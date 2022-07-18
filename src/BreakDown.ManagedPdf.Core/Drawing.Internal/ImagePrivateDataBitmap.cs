using System;
using BreakDown.ManagedPdf.Core.Pdf.Advanced;

namespace BreakDown.ManagedPdf.Core.Drawing.Internal
{
    /// <summary>
    /// Image data needed for PDF bitmap images.
    /// </summary>
    internal class ImagePrivateDataBitmap : ImagePrivateData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePrivateDataBitmap"/> class.
        /// </summary>
        public ImagePrivateDataBitmap(byte[] data, int length)
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

        /// <summary>
        /// True if first line is the top line, false if first line is the bottom line of the image. When needed, lines will be reversed while converting data into PDF format.
        /// </summary>
        internal bool FlippedImage;

        /// <summary>
        /// The offset of the image data in Data.
        /// </summary>
        internal int Offset;

        /// <summary>
        /// The offset of the color palette in Data.
        /// </summary>
        internal int ColorPaletteOffset;

        internal void CopyBitmap(ImageDataBitmap dest)
        {
            switch (Image.Information.ImageFormat)
            {
                case ImageInformation.ImageFormats.ARGB32:
                    CopyTrueColorMemoryBitmap(3, 8, true, dest);
                    break;

                case ImageInformation.ImageFormats.RGB24:
                    CopyTrueColorMemoryBitmap(4, 8, false, dest);
                    break;

                case ImageInformation.ImageFormats.Palette8:
                    CopyIndexedMemoryBitmap(8, dest);
                    break;

                case ImageInformation.ImageFormats.Palette4:
                    CopyIndexedMemoryBitmap(4, dest);
                    break;

                case ImageInformation.ImageFormats.Palette1:
                    CopyIndexedMemoryBitmap(1, dest);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Copies images without color palette.
        /// </summary>
        /// <param name="components">4 (32bpp RGB), 3 (24bpp RGB, 32bpp ARGB)</param>
        /// <param name="bits">8</param>
        /// <param name="hasAlpha">true (ARGB), false (RGB)</param>
        /// <param name="dest">Destination </param>
        private void CopyTrueColorMemoryBitmap(int components, int bits, bool hasAlpha, ImageDataBitmap dest)
        {
            var width = (int)Image.Information.Width;
            var height = (int)Image.Information.Height;

            var logicalComponents = components;
            if (components == 4)
            {
                logicalComponents = 3;
            }

            var imageData = new byte[components * width * height];

            var hasMask = false;
            var hasAlphaMask = false;
            var alphaMask = hasAlpha ? new byte[width * height] : null;
            var mask = hasAlpha ? new MonochromeMask(width, height) : null;

            var nFileOffset = Offset;
            var nOffsetRead = 0;
            if (logicalComponents == 3)
            {
                for (var y = 0; y < height; ++y)
                {
                    // TODO Handle Flipped.
                    var nOffsetWrite = 3 * (height - 1 - y) * width;
                    var nOffsetWriteAlpha = 0;
                    if (hasAlpha)
                    {
                        mask.StartLine(y);
                        nOffsetWriteAlpha = (height - 1 - y) * width;
                    }

                    for (var x = 0; x < width; ++x)
                    {
                        imageData[nOffsetWrite] = Data[nFileOffset + nOffsetRead + 2];
                        imageData[nOffsetWrite + 1] = Data[nFileOffset + nOffsetRead + 1];
                        imageData[nOffsetWrite + 2] = Data[nFileOffset + nOffsetRead];
                        if (hasAlpha)
                        {
                            mask.AddPel(Data[nFileOffset + nOffsetRead + 3]);
                            alphaMask[nOffsetWriteAlpha] = Data[nFileOffset + nOffsetRead + 3];
                            if (!hasMask || !hasAlphaMask)
                            {
                                if (Data[nFileOffset + nOffsetRead + 3] != 255)
                                {
                                    hasMask = true;
                                    if (Data[nFileOffset + nOffsetRead + 3] != 0)
                                    {
                                        hasAlphaMask = true;
                                    }
                                }
                            }

                            ++nOffsetWriteAlpha;
                        }

                        nOffsetRead += hasAlpha ? 4 : components;
                        nOffsetWrite += 3;
                    }

                    nOffsetRead = 4 * ((nOffsetRead + 3) / 4); // Align to 32 bit boundary
                }
            }
            else if (components == 1)
            {
                // Grayscale
                throw new NotImplementedException("Image format not supported (grayscales).");
            }

            dest.Data = imageData;
            dest.Length = imageData.Length;

            if (alphaMask != null)
            {
                dest.AlphaMask = alphaMask;
                dest.AlphaMaskLength = alphaMask.Length;
            }

            if (mask != null)
            {
                dest.BitmapMask = mask.MaskData;
                dest.BitmapMaskLength = mask.MaskData.Length;
            }
        }

        private void CopyIndexedMemoryBitmap(int bits /*, ref bool hasAlpha*/, ImageDataBitmap dest)
        {
            int firstMaskColor = -1, lastMaskColor = -1;
            var segmentedColorMask = false;

            var bytesColorPaletteOffset =
                ((ImagePrivateDataBitmap)Image.Data)
                .ColorPaletteOffset; // GDI+ always returns Windows bitmaps: sizeof BITMAPFILEHEADER + sizeof BITMAPINFOHEADER

            var bytesFileOffset = ((ImagePrivateDataBitmap)Image.Data).Offset;
            var paletteColors = Image.Information.ColorsUsed;
            var width = (int)Image.Information.Width;
            var height = (int)Image.Information.Height;

            var mask = new MonochromeMask(width, height);

            var isGray = bits == 8 && (paletteColors == 256 || paletteColors == 0);
            var isBitonal = 0; // 0: false; >0: true; <0: true (inverted)
            var paletteData = new byte[3 * paletteColors];
            for (var color = 0; color < paletteColors; ++color)
            {
                paletteData[3 * color] = Data[bytesColorPaletteOffset + 4 * color + 2];
                paletteData[3 * color + 1] = Data[bytesColorPaletteOffset + 4 * color + 1];
                paletteData[3 * color + 2] = Data[bytesColorPaletteOffset + 4 * color + 0];
                if (isGray)
                {
                    isGray = paletteData[3 * color] == paletteData[3 * color + 1] &&
                             paletteData[3 * color] == paletteData[3 * color + 2];
                }

                if (Data[bytesColorPaletteOffset + 4 * color + 3] < 128)
                {
                    // We treat this as transparency:
                    if (firstMaskColor == -1)
                    {
                        firstMaskColor = color;
                    }

                    if (lastMaskColor == -1 || lastMaskColor == color - 1)
                    {
                        lastMaskColor = color;
                    }

                    if (lastMaskColor != color)
                    {
                        segmentedColorMask = true;
                    }
                }

                //else
                //{
                //  // We treat this as opacity:
                //}
            }

            if (bits == 1)
            {
                if (paletteColors == 0)
                {
                    isBitonal = 1;
                }

                if (paletteColors == 2)
                {
                    if (paletteData[0] == 0 &&
                        paletteData[1] == 0 &&
                        paletteData[2] == 0 &&
                        paletteData[3] == 255 &&
                        paletteData[4] == 255 &&
                        paletteData[5] == 255)
                    {
                        isBitonal = 1; // Black on white
                    }

                    if (paletteData[5] == 0 &&
                        paletteData[4] == 0 &&
                        paletteData[3] == 0 &&
                        paletteData[2] == 255 &&
                        paletteData[1] == 255 &&
                        paletteData[0] == 255)
                    {
                        isBitonal = -1; // White on black
                    }
                }
            }

            // NYI: (no sample found where this was required) 
            // if (segmentedColorMask = true)
            // { ... }

            var isFaxEncoding = false;
            var imageData = new byte[((width * bits + 7) / 8) * height];
            byte[] imageDataFax = null;
            var k = 0;

            if (bits == 1 && dest._document.Options.EnableCcittCompressionForBilevelImages)
            {
                // TODO: flag/option?
                // We try Group 3 1D and Group 4 (2D) encoding here and keep the smaller byte array.
                //byte[] temp = new byte[imageData.Length];
                //int ccittSize = DoFaxEncoding(ref temp, imageBits, (uint)bytesFileOffset, (uint)width, (uint)height);

                // It seems that Group 3 2D encoding never beats both other encodings, therefore we don't call it here.
                //byte[] temp2D = new byte[imageData.Length];
                //uint dpiY = (uint)image.VerticalResolution;
                //uint kTmp = 0;
                //int ccittSize2D = DoFaxEncoding2D((uint)bytesFileOffset, ref temp2D, imageBits, (uint)width, (uint)height, dpiY, out kTmp);
                //k = (int) kTmp;

                var tempG4 = new byte[imageData.Length];
                var ccittSizeG4 = PdfImage.DoFaxEncodingGroup4(ref tempG4, Data, (uint)bytesFileOffset, (uint)width, (uint)height);

                isFaxEncoding = /*ccittSize > 0 ||*/ ccittSizeG4 > 0;
                if (isFaxEncoding)
                {
                    //if (ccittSize == 0)
                    //  ccittSize = 0x7fffffff;
                    if (ccittSizeG4 == 0)
                    {
                        ccittSizeG4 = 0x7fffffff;
                    }

                    //if (ccittSize <= ccittSizeG4)
                    //{
                    //  Array.Resize(ref temp, ccittSize);
                    //  imageDataFax = temp;
                    //  k = 0;
                    //}
                    //else
                    {
                        Array.Resize(ref tempG4, ccittSizeG4);
                        imageDataFax = tempG4;
                        k = -1;
                    }
                }
            }

            //if (!isFaxEncoding)
            {
                var bytesOffsetRead = 0;
                if (bits == 8 || bits == 4 || bits == 1)
                {
                    var bytesPerLine = (width * bits + 7) / 8;
                    for (var y = 0; y < height; ++y)
                    {
                        mask.StartLine(y);
                        var bytesOffsetWrite = (height - 1 - y) * ((width * bits + 7) / 8);
                        for (var x = 0; x < bytesPerLine; ++x)
                        {
                            if (isGray)
                            {
                                // Lookup the gray value from the palette:
                                imageData[bytesOffsetWrite] = paletteData[3 * Data[bytesFileOffset + bytesOffsetRead]];
                            }
                            else
                            {
                                // Store the palette index.
                                imageData[bytesOffsetWrite] = Data[bytesFileOffset + bytesOffsetRead];
                            }

                            if (firstMaskColor != -1)
                            {
                                int n = Data[bytesFileOffset + bytesOffsetRead];
                                if (bits == 8)
                                {
                                    // TODO???: segmentedColorMask == true => bad mask NYI
                                    mask.AddPel((n >= firstMaskColor) && (n <= lastMaskColor));
                                }
                                else if (bits == 4)
                                {
                                    // TODO???: segmentedColorMask == true => bad mask NYI
                                    var n1 = (n & 0xf0) / 16;
                                    var n2 = (n & 0x0f);
                                    mask.AddPel((n1 >= firstMaskColor) && (n1 <= lastMaskColor));
                                    mask.AddPel((n2 >= firstMaskColor) && (n2 <= lastMaskColor));
                                }
                                else if (bits == 1)
                                {
                                    // TODO???: segmentedColorMask == true => bad mask NYI
                                    for (var bit = 1; bit <= 8; ++bit)
                                    {
                                        var n1 = (n & 0x80) / 128;
                                        mask.AddPel((n1 >= firstMaskColor) && (n1 <= lastMaskColor));
                                        n *= 2;
                                    }
                                }
                            }

                            bytesOffsetRead += 1;
                            bytesOffsetWrite += 1;
                        }

                        bytesOffsetRead = 4 * ((bytesOffsetRead + 3) / 4); // Align to 32 bit boundary
                    }
                }
                else
                {
                    throw new NotImplementedException("ReadIndexedMemoryBitmap: unsupported format #3");
                }
            }

            dest.Data = imageData;
            dest.Length = imageData.Length;

            if (imageDataFax != null)
            {
                dest.DataFax = imageDataFax;
                dest.LengthFax = imageDataFax.Length;
            }

            dest.IsGray = isGray;
            dest.K = k;
            dest.IsBitonal = isBitonal;

            dest.PaletteData = paletteData;
            dest.PaletteDataLength = paletteData.Length;
            dest.SegmentedColorMask = segmentedColorMask;

            //if (alphaMask != null)
            //{
            //    dest.AlphaMask = alphaMask;
            //    dest.AlphaMaskLength = alphaMask.Length;
            //}

            if (mask != null && firstMaskColor != -1)
            {
                dest.BitmapMask = mask.MaskData;
                dest.BitmapMaskLength = mask.MaskData.Length;
            }
        }
    }
}
