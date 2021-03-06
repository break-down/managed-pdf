#region PDFsharp - A .NET library for processing PDF

//
// Authors:
//   Stefan Lange
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
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using BreakDown.ManagedPdf.Core.Fonts;
using BreakDown.ManagedPdf.Core.Pdf.Filters;

namespace BreakDown.ManagedPdf.Core.Pdf.Advanced
{
    /// <summary>
    /// Represents a ToUnicode map for composite font.
    /// </summary>
    internal sealed class PdfToUnicodeMap : PdfDictionary
    {
        public PdfToUnicodeMap(PdfDocument document)
            : base(document)
        {
        }

        public PdfToUnicodeMap(PdfDocument document, CMapInfo cmapInfo)
            : base(document)
        {
            _cmapInfo = cmapInfo;
        }

        /// <summary>
        /// Gets or sets the CMap info.
        /// </summary>
        public CMapInfo CMapInfo
        {
            get { return _cmapInfo; }
            set { _cmapInfo = value; }
        }

        CMapInfo _cmapInfo;

        /// <summary>
        /// Creates the ToUnicode map from the CMapInfo.
        /// </summary>
        internal override void PrepareForSave()
        {
            base.PrepareForSave();

            // This code comes literally from PDF Reference
            var prefix =
                "/CIDInit /ProcSet findresource begin\n" +
                "12 dict begin\n" +
                "begincmap\n" +
                "/CIDSystemInfo << /Registry (Adobe)/Ordering (UCS)/Supplement 0>> def\n" +
                "/CMapName /Adobe-Identity-UCS def /CMapType 2 def\n";
            var suffix = "endcmap CMapName currentdict /CMap defineresource pop end end";

            var glyphIndexToCharacter = new ConcurrentDictionary<int, char>();
            int lowIndex = 65536, hiIndex = -1;
            foreach (var entry in _cmapInfo.CharacterToGlyphIndex)
            {
                var index = (int)entry.Value;
                lowIndex = Math.Min(lowIndex, index);
                hiIndex = Math.Max(hiIndex, index);

                //glyphIndexToCharacter.Add(index, entry.Key);
                glyphIndexToCharacter[index] = entry.Key;
            }

            var ms = new MemoryStream();
#if !SILVERLIGHT && !NETFX_CORE
            var wrt = new StreamWriter(ms, Encoding.ASCII);
#else
            StreamWriter wrt = new StreamWriter(ms, Encoding.UTF8);
#endif
            wrt.Write(prefix);

            wrt.WriteLine("1 begincodespacerange");
            wrt.WriteLine(String.Format("<{0:X4}><{1:X4}>", lowIndex, hiIndex));
            wrt.WriteLine("endcodespacerange");

            // Sorting seems not necessary. The limit is 100 entries, we will see.
            wrt.WriteLine(String.Format("{0} beginbfrange", glyphIndexToCharacter.Count));
            foreach (var entry in glyphIndexToCharacter)
            {
                wrt.WriteLine(String.Format("<{0:X4}><{0:X4}><{1:X4}>", entry.Key, (int)entry.Value));
            }

            wrt.WriteLine("endbfrange");

            wrt.Write(suffix);
#if !UWP
            wrt.Close();
#else
            wrt.Dispose();
#endif

            // Compress like content streams
            var bytes = ms.ToArray();
#if !UWP
            ms.Close();
#else
            ms.Dispose();
#endif
            if (Owner.Options.CompressContentStreams)
            {
                Elements.SetName("/Filter", "/FlateDecode");
                bytes = Filtering.FlateDecode.Encode(bytes, _document.Options.FlateEncodeMode);
            }

            //PdfStream stream = CreateStream(bytes);
            else
            {
                Elements.Remove("/Filter");
            }

            if (Stream == null)
            {
                CreateStream(bytes);
            }
            else
            {
                Stream.Value = bytes;
                Elements.SetInteger(PdfStream.Keys.Length, Stream.Length);
            }
        }

        public sealed class Keys : PdfStream.Keys
        {
            // No new keys.
        }
    }
}
