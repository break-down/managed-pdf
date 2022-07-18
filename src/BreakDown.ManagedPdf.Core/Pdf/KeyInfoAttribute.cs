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

namespace BreakDown.ManagedPdf.Core.Pdf
{
    /// <summary>
    /// Summary description for KeyInfo.
    /// </summary>
    internal class KeyInfoAttribute : Attribute
    {
        public KeyInfoAttribute()
        {
        }

        public KeyInfoAttribute(KeyType keyType)
        {
            //_version = version;
            KeyType = keyType;
        }

        public KeyInfoAttribute(string version, KeyType keyType)
        {
            _version = version;
            KeyType = keyType;
        }

        public KeyInfoAttribute(KeyType keyType, Type objectType)
        {
            //_version = version;
            KeyType = keyType;
            _objectType = objectType;
        }

        public KeyInfoAttribute(string version, KeyType keyType, Type objectType)
        {
            //_version = version;
            KeyType = keyType;
            _objectType = objectType;
        }

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        string _version = "1.0";

        public KeyType KeyType
        {
            get { return _entryType; }
            set { _entryType = value; }
        }

        KeyType _entryType;

        public Type ObjectType
        {
            get { return _objectType; }
            set { _objectType = value; }
        }

        Type _objectType;

        public string FixedValue
        {
            get { return _fixedValue; }
            set { _fixedValue = value; }
        }

        string _fixedValue;
    }
}
