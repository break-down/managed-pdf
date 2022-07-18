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

#define VERBOSE_

using System;
using System.Diagnostics;
using BreakDown.ManagedPdf.Core.root;

// ReSharper disable InconsistentNaming

namespace BreakDown.ManagedPdf.Core.Fonts.OpenType
{
    /// <summary>
    /// CMap format 4: Segment mapping to delta values.
    /// The Windows standard format.
    /// </summary>
    internal class CMap4 : OpenTypeFontTable
    {
        public WinEncodingId encodingId; // Windows encoding ID.
        public ushort format; // Format number is set to 4.
        public ushort length; // This is the length in bytes of the subtable. 
        public ushort language; // This field must be set to zero for all cmap subtables whose platform IDs are other than Macintosh (platform ID 1). 
        public ushort segCountX2; // 2 x segCount.
        public ushort searchRange; // 2 x (2**floor(log2(segCount)))
        public ushort entrySelector; // log2(searchRange/2)
        public ushort rangeShift;
        public ushort[] endCount; // [segCount] / End characterCode for each segment, last=0xFFFF.
        public ushort[] startCount; // [segCount] / Start character code for each segment.
        public short[] idDelta; // [segCount] / Delta for all character codes in segment.
        public ushort[] idRangeOffs; // [segCount] / Offsets into glyphIdArray or 0
        public int glyphCount; // = (length - (16 + 4 * 2 * segCount)) / 2;
        public ushort[] glyphIdArray; // Glyph index array (arbitrary length)

        public CMap4(OpenTypeFontface fontData, WinEncodingId encodingId)
            : base(fontData, "----")
        {
            this.encodingId = encodingId;
            Read();
        }

        internal void Read()
        {
            try
            {
                // m_EncodingID = encID;
                format = _fontData.ReadUShort();
                Debug.Assert(format == 4, "Only format 4 expected.");
                length = _fontData.ReadUShort();
                language = _fontData.ReadUShort(); // Always null in Windows
                segCountX2 = _fontData.ReadUShort();
                searchRange = _fontData.ReadUShort();
                entrySelector = _fontData.ReadUShort();
                rangeShift = _fontData.ReadUShort();

                var segCount = segCountX2 / 2;
                glyphCount = (length - (16 + 8 * segCount)) / 2;

                //ASSERT_CONDITION(0 <= m_NumGlyphIds && m_NumGlyphIds < m_Length, "Invalid Index");

                endCount = new ushort[segCount];
                startCount = new ushort[segCount];
                idDelta = new short[segCount];
                idRangeOffs = new ushort[segCount];

                glyphIdArray = new ushort[glyphCount];

                for (var idx = 0; idx < segCount; idx++)
                {
                    endCount[idx] = _fontData.ReadUShort();
                }

                //ASSERT_CONDITION(m_EndCount[segs - 1] == 0xFFFF, "Out of Index");

                // Read reserved pad.
                _fontData.ReadUShort();

                for (var idx = 0; idx < segCount; idx++)
                {
                    startCount[idx] = _fontData.ReadUShort();
                }

                for (var idx = 0; idx < segCount; idx++)
                {
                    idDelta[idx] = _fontData.ReadShort();
                }

                for (var idx = 0; idx < segCount; idx++)
                {
                    idRangeOffs[idx] = _fontData.ReadUShort();
                }

                for (var idx = 0; idx < glyphCount; idx++)
                {
                    glyphIdArray[idx] = _fontData.ReadUShort();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(PSSR.ErrorReadingFontData, ex);
            }
        }
    }
}

// UNDONE
