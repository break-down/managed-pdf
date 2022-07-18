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
using System.Diagnostics;
using BreakDown.ManagedPdf.Core.Pdf.Advanced;

namespace BreakDown.ManagedPdf.Core.Pdf
{
    /// <summary>
    /// Holds information about the value of a key in a dictionary. This information is used to create
    /// and interpret this value.
    /// </summary>
    internal sealed class KeyDescriptor
    {
        /// <summary>
        /// Initializes a new instance of KeyDescriptor from the specified attribute during a KeysMeta
        /// initializes itself using reflection.
        /// </summary>
        public KeyDescriptor(KeyInfoAttribute attribute)
        {
            _version = attribute.Version;
            _keyType = attribute.KeyType;
            _fixedValue = attribute.FixedValue;
            _objectType = attribute.ObjectType;

            if (_version == "")
            {
                _version = "1.0";
            }
        }

        /// <summary>
        /// Gets or sets the PDF version starting with the availability of the described key.
        /// </summary>
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        string _version;

        public KeyType KeyType
        {
            get { return _keyType; }
            set { _keyType = value; }
        }

        KeyType _keyType;

        public string KeyValue
        {
            get { return _keyValue; }
            set { _keyValue = value; }
        }

        string _keyValue;

        public string FixedValue
        {
            get { return _fixedValue; }
        }

        readonly string _fixedValue;

        public Type ObjectType
        {
            get { return _objectType; }
            set { _objectType = value; }
        }

        Type _objectType;

        public bool CanBeIndirect
        {
            get { return (_keyType & KeyType.MustNotBeIndirect) == 0; }
        }

        /// <summary>
        /// Returns the type of the object to be created as value for the described key.
        /// </summary>
        public Type GetValueType()
        {
            var type = _objectType;
            if (type == null)
            {
                // If we have no ObjectType specified, use the KeyType enumeration.
                switch (_keyType & KeyType.TypeMask)
                {
                    case KeyType.Name:
                        type = typeof(PdfName);
                        break;

                    case KeyType.String:
                        type = typeof(PdfString);
                        break;

                    case KeyType.Boolean:
                        type = typeof(PdfBoolean);
                        break;

                    case KeyType.Integer:
                        type = typeof(PdfInteger);
                        break;

                    case KeyType.Real:
                        type = typeof(PdfReal);
                        break;

                    case KeyType.Date:
                        type = typeof(PdfDate);
                        break;

                    case KeyType.Rectangle:
                        type = typeof(PdfRectangle);
                        break;

                    case KeyType.Array:
                        type = typeof(PdfArray);
                        break;

                    case KeyType.Dictionary:
                        type = typeof(PdfDictionary);
                        break;

                    case KeyType.Stream:
                        type = typeof(PdfDictionary);
                        break;

                    case KeyType.NumberTree:
                        type = typeof(PdfNumberTreeNode);
                        break;

                    case KeyType.NameTree:
                        type = typeof(PdfNameTreeNode);
                        break;

                    case KeyType.FileSpecification:
                        type = typeof(PdfFileSpecification);
                        break;

                    // The following types are not yet used

                    case KeyType.NameOrArray:
                        throw new NotImplementedException("KeyType.NameOrArray");

                    case KeyType.ArrayOrDictionary:
                        throw new NotImplementedException("KeyType.ArrayOrDictionary");

                    case KeyType.StreamOrArray:
                        throw new NotImplementedException("KeyType.StreamOrArray");

                    case KeyType.ArrayOrNameOrString:
                        return null; // HACK: Make PdfOutline work

                    //throw new NotImplementedException("KeyType.ArrayOrNameOrString");

                    default:
                        Debug.Assert(false, "Invalid KeyType: " + _keyType);
                        break;
                }
            }

            return type;
        }
    }
}
