using System.Diagnostics;

namespace BreakDown.ManagedPdf.Core.Pdf.Content.Objects
{
    /// <summary>
    /// Represents a comment in a PDF content stream.
    /// </summary>
    [DebuggerDisplay("({Text})")]
    public class CComment : CObject
    {
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        public new CComment Clone()
        {
            return (CComment)Copy();
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
        /// Gets or sets the comment text.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        string _text;

        /// <summary>
        /// Returns a string that represents the current comment.
        /// </summary>
        public override string ToString()
        {
            return "% " + _text;
        }

        internal override void WriteObject(ContentWriter writer)
        {
            writer.WriteLineRaw(ToString());
        }
    }
}
