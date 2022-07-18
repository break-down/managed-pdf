using System.Diagnostics;
using BreakDown.ManagedPdf.Core.Pdf.Content.Objects.@enum;

namespace BreakDown.ManagedPdf.Core.Pdf.Content.Objects
{
    /// <summary>
    /// Represents an operator a PDF content stream.
    /// </summary>
    [DebuggerDisplay("({Name}, operands={Operands.Count})")]
    public class COperator : CObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="COperator"/> class.
        /// </summary>
        protected COperator()
        {
        }

        internal COperator(OpCode opcode)
        {
            _opcode = opcode;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        public new COperator Clone()
        {
            return (COperator)Copy();
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
        /// Gets or sets the name of the operator
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name
        {
            get { return _opcode.Name; }
        }

        /// <summary>
        /// Gets or sets the operands.
        /// </summary>
        /// <value>The operands.</value>
        public CSequence Operands
        {
            get { return _seqence ?? (_seqence = new CSequence()); }
        }

        CSequence _seqence;

        /// <summary>
        /// Gets the operator description for this instance.
        /// </summary>
        public OpCode OpCode
        {
            get { return _opcode; }
        }

        readonly OpCode _opcode;

        /// <summary>
        /// Returns a string that represents the current operator.
        /// </summary>
        public override string ToString()
        {
            if (_opcode.OpCodeName == OpCodeName.Dictionary)
            {
                return " ";
            }

            return Name;
        }

        internal override void WriteObject(ContentWriter writer)
        {
            var count = _seqence != null ? _seqence.Count : 0;
            for (var idx = 0; idx < count; idx++)
            {
                // ReSharper disable once PossibleNullReferenceException because the loop is not entered if _sequence is null
                _seqence[idx].WriteObject(writer);
            }

            writer.WriteLineRaw(ToString());
        }
    }
}
