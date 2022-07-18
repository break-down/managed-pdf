using System;
using System.Diagnostics;
using System.Text;

namespace BreakDown.ManagedPdf.Core.Pdf.Content.Objects
{
    /// <summary>
    /// Represents a string value in a PDF content stream.
    /// </summary>
    [DebuggerDisplay("({Value})")]
    public class CString : CObject
    {
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        public new CString Clone()
        {
            return (CString)Copy();
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
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        string _value;

        /// <summary>
        /// Gets or sets the type of the content string.
        /// </summary>
        public CStringType CStringType
        {
            get { return _cStringType; }
            set { _cStringType = value; }
        }

        CStringType _cStringType;

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        public override string ToString()
        {
            var s = new StringBuilder();
            switch (CStringType)
            {
                case CStringType.String:
                    s.Append("(");
                    var length = _value.Length;
                    for (var ich = 0; ich < length; ich++)
                    {
                        var ch = _value[ich];
                        switch (ch)
                        {
                            case Chars.LF:
                                s.Append("\\n");
                                break;

                            case Chars.CR:
                                s.Append("\\r");
                                break;

                            case Chars.HT:
                                s.Append("\\t");
                                break;

                            case Chars.BS:
                                s.Append("\\b");
                                break;

                            case Chars.FF:
                                s.Append("\\f");
                                break;

                            case Chars.ParenLeft:
                                s.Append("\\(");
                                break;

                            case Chars.ParenRight:
                                s.Append("\\)");
                                break;

                            case Chars.BackSlash:
                                s.Append("\\\\");
                                break;

                            default:
#if true_
                                // not absolut necessary to use octal encoding for characters less than blank
                                if (ch < ' ')
                                {
                                    s.Append("\\");
                                    s.Append((char)(((ch >> 6) & 7) + '0'));
                                    s.Append((char)(((ch >> 3) & 7) + '0'));
                                    s.Append((char)((ch & 7) + '0'));
                                }
                                else
#endif
                                s.Append(ch);
                                break;
                        }
                    }

                    s.Append(')');
                    break;

                case CStringType.HexString:
                    throw new NotImplementedException();

                //break;

                case CStringType.UnicodeString:
                    throw new NotImplementedException();

                //break;

                case CStringType.UnicodeHexString:
                    throw new NotImplementedException();

                //break;

                case CStringType.Dictionary:
                    s.Append(_value);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return s.ToString();
        }

        internal override void WriteObject(ContentWriter writer)
        {
            writer.WriteRaw(ToString());
        }
    }
}
