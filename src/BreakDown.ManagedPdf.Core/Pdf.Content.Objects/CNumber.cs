namespace BreakDown.ManagedPdf.Core.Pdf.Content.Objects
{
    /// <summary>
    /// Represents the base class for numerical objects in a PDF content stream.
    /// </summary>
    public abstract class CNumber : CObject
    {
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        public new CNumber Clone()
        {
            return (CNumber)Copy();
        }

        /// <summary>
        /// Implements the copy mechanism of this class.
        /// </summary>
        protected override CObject Copy()
        {
            var obj = base.Copy();
            return obj;
        }

        //internal override void WriteObject(ContentWriter writer)
        //{
        //  throw new Exception("Must not come here.");
        //}
    }
}
