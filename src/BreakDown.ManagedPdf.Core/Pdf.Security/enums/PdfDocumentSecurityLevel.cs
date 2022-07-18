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

namespace BreakDown.ManagedPdf.Core.Pdf.Security.enums
{
    /// <summary>
    /// Specifies the security level of the PDF document.
    /// </summary>
    public enum PdfDocumentSecurityLevel
    {
        /// <summary>
        /// Document is not protected.
        /// </summary>
        None,

        /// <summary>
        /// Document is protected with 40-bit security. This option is for compatibility with 
        /// Acrobat 3 and 4 only. Use Encrypted128Bit whenever possible.
        /// </summary>
        Encrypted40Bit,

        /// <summary>
        /// Document is protected with 128-bit security.
        /// </summary>
        Encrypted128Bit,
    }
}
