using System.Diagnostics;
using System.Globalization;
using BreakDown.ManagedPdf.Core._internal;

namespace BreakDown.ManagedPdf.Core.Pdf.Content.Objects
{
    /// <summary>
    /// Represents a real value in a PDF content stream.
    /// </summary>
    [DebuggerDisplay("({Value})")]
    public class CReal : CNumber
    {
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        public new CReal Clone()
        {
            return (CReal)Copy();
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
        /// Gets or sets the value.
        /// </summary>
        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        double _value;

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        public override string ToString()
        {
            const string format = Config.SignificantFigures1Plus9;
            return _value.ToString(format, CultureInfo.InvariantCulture);
        }

        internal override void WriteObject(ContentWriter writer)
        {
            writer.WriteRaw(ToString() + " ");
        }
    }
}
