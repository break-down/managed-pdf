using System;
using System.Diagnostics;
using BreakDown.ManagedPdf.Core.root;

namespace BreakDown.ManagedPdf.Core.Pdf.Content.Objects
{
    /// <summary>
    /// Represents a name in a PDF content stream.
    /// </summary>
    [DebuggerDisplay("({Name})")]
    public class CName : CObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CName"/> class.
        /// </summary>
        public CName()
        {
            _name = "/";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CName"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public CName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        public new CName Clone()
        {
            return (CName)Copy();
        }

        /// <summary>
        /// Implements the copy mechanism of this class.
        /// </summary>
        protected override CObject Copy()
        {
            var obj = base.Copy();
            return obj;
        }

        /// <summary>
        /// Gets or sets the name. Names must start with a slash.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrEmpty(_name))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (_name[0] != '/')
                {
                    throw new ArgumentException(PSSR.NameMustStartWithSlash);
                }

                _name = value;
            }
        }

        string _name;

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        public override string ToString()
        {
            return _name;
        }

        internal override void WriteObject(ContentWriter writer)
        {
            writer.WriteRaw(ToString() + " ");
        }
    }
}
