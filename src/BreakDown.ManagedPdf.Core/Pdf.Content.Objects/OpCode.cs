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

using BreakDown.ManagedPdf.Core.Pdf.Content.Objects.@enum;

namespace BreakDown.ManagedPdf.Core.Pdf.Content.Objects
{
    /// <summary>
    /// Represents a PDF content stream operator description.
    /// </summary>
    public sealed class OpCode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpCode"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="opcodeName">The enum value of the operator.</param>
        /// <param name="operands">The number of operands.</param>
        /// <param name="postscript">The postscript equivalent, or null, if no such operation exists.</param>
        /// <param name="flags">The flags.</param>
        /// <param name="description">The description from Adobe PDF Reference.</param>
        internal OpCode(string name, OpCodeName opcodeName, int operands, string postscript, OpCodeFlags flags, string description)
        {
            Name = name;
            OpCodeName = opcodeName;
            Operands = operands;
            Postscript = postscript;
            Flags = flags;
            Description = description;
        }

        /// <summary>
        /// The name of the operator.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The enum value of the operator.
        /// </summary>
        public readonly OpCodeName OpCodeName;

        /// <summary>
        /// The number of operands. -1 indicates a variable number of operands.
        /// </summary>
        public readonly int Operands;

        /// <summary>
        /// The flags.
        /// </summary>
        public readonly OpCodeFlags Flags;

        /// <summary>
        /// The postscript equivalent, or null, if no such operation exists.
        /// </summary>
        public readonly string Postscript;

        /// <summary>
        /// The description from Adobe PDF Reference.
        /// </summary>
        public readonly string Description;
    }
}
