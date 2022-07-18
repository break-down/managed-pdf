#region PDFsharp - A .NET library for processing PDF

//
// Authors:
//   Thomas Hövel
//
// Copyright (c) 2005-2019 empira Software GmbH, Cologne Area (Germany)
//
// http://www.pdfsharp.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

#endregion

using System;
using BreakDown.ManagedPdf.Core.Pdf;

namespace BreakDown.ManagedPdf.Core.Drawing.Internal;

// $THHO THHO4THHO add support for PdfDocument.Options.
internal class ImageImporterBmp : ImageImporterRoot, IImageImporter
{
    public ImportedImage ImportImage(StreamReaderHelper stream, PdfDocument document)
    {
        try
        {
            stream.CurrentOffset = 0;
            if (TestBitmapFileHeader(stream, out var offsetImageData))
            {
                // Magic: TestBitmapFileHeader updates stream.CurrentOffset on success.

                var ipd = new ImagePrivateDataBitmap(stream.Data, stream.Length);
                ImportedImage ii = new ImportedImageBitmap(this, ipd, document);
                if (TestBitmapInfoHeader(stream, ii, offsetImageData))
                {
                    //stream.CurrentOffset = offsetImageData;
                    return ii;
                }
            }
        }

        // ReSharper disable once EmptyGeneralCatchClause
        catch (Exception)
        {
        }

        return null;
    }

    private bool TestBitmapFileHeader(StreamReaderHelper stream, out int offset)
    {
        offset = 0;

        // File must start with "BM".
        if (stream.GetWord(0, true) == 0x424d)
        {
            var filesize = (int)stream.GetDWord(2, false);

            // Integrity check: filesize set in BM header should match size of the stream.
            // We test "<" instead of "!=" to allow extra bytes at the end of the stream.
            if (filesize < stream.Length)
            {
                return false;
            }

            offset = (int)stream.GetDWord(10, false);
            stream.CurrentOffset += 14;
            return true;
        }

        return false;
    }

    private bool TestBitmapInfoHeader(StreamReaderHelper stream, ImportedImage ii, int offset)
    {
        var size = (int)stream.GetDWord(0, false);
        if (size == 40 || size == 108 || size == 124) // sizeof BITMAPINFOHEADER == 40, sizeof BITMAPV4HEADER == 108, sizeof BITMAPV5HEADER == 124
        {
            var width = stream.GetDWord(4, false);
            var height = (int)stream.GetDWord(8, false);
            int planes = stream.GetWord(12, false);
            int bitcount = stream.GetWord(14, false);
            var compression = (int)stream.GetDWord(16, false);
            var sizeImage = (int)stream.GetDWord(20, false);
            var xPelsPerMeter = (int)stream.GetDWord(24, false);
            var yPelsPerMeter = (int)stream.GetDWord(28, false);
            var colorsUsed = stream.GetDWord(32, false);
            var colorsImportant = stream.GetDWord(36, false);

            // TODO Integrity and plausibility checks.
            if (sizeImage != 0 && sizeImage + offset > stream.Length)
            {
                return false;
            }

            var privateData = (ImagePrivateDataBitmap)ii.Data;

            // Return true only for supported formats.
            if (compression == 0 || compression == 3) // BI_RGB == 0, BI_BITFIELDS == 3
            {
                ((ImagePrivateDataBitmap)ii.Data).Offset = offset;
                ((ImagePrivateDataBitmap)ii.Data).ColorPaletteOffset = stream.CurrentOffset + size;
                ii.Information.Width = width;
                ii.Information.Height = (uint)Math.Abs(height);
                ii.Information.HorizontalDPM = xPelsPerMeter;
                ii.Information.VerticalDPM = yPelsPerMeter;
                privateData.FlippedImage = height < 0;
                if (planes == 1 && bitcount == 24)
                {
                    // RGB24
                    ii.Information.ImageFormat = ImageInformation.ImageFormats.RGB24;

                    // TODO: Verify Mask if size >= 108 && compression == 3.
                    return true;
                }

                if (planes == 1 && bitcount == 32)
                {
                    // ARGB32
                    //ii.Information.ImageFormat = ImageInformation.ImageFormats.ARGB32;
                    ii.Information.ImageFormat = compression == 0 ? ImageInformation.ImageFormats.RGB24 : ImageInformation.ImageFormats.ARGB32;

                    // TODO: tell RGB from ARGB. Idea: assume RGB if alpha is always 0.

                    // TODO: Verify Mask if size >= 108 && compression == 3.
                    return true;
                }

                if (planes == 1 && bitcount == 8)
                {
                    // Palette8
                    ii.Information.ImageFormat = ImageInformation.ImageFormats.Palette8;
                    ii.Information.ColorsUsed = colorsUsed;

                    return true;
                }

                if (planes == 1 && bitcount == 4)
                {
                    // Palette8
                    ii.Information.ImageFormat = ImageInformation.ImageFormats.Palette4;
                    ii.Information.ColorsUsed = colorsUsed;

                    return true;
                }

                if (planes == 1 && bitcount == 1)
                {
                    // Palette8
                    ii.Information.ImageFormat = ImageInformation.ImageFormats.Palette1;
                    ii.Information.ColorsUsed = colorsUsed;

                    return true;
                }

                // TODO Implement more formats!
            }
        }

        return false;
    }

    public ImageData PrepareImage(ImagePrivateData data)
    {
        throw new NotImplementedException();
    }
}

// THHO4THHO Maybe there will be derived classes for direct bitmaps vs. palettized bitmaps or so. Time will tell.
