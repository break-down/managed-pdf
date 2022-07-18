using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BreakDown.ManagedPdf.Core.Pdf.Content.Objects
{
    /// <summary>
    /// Represents a sequence of objects in a PDF content stream.
    /// </summary>
    [DebuggerDisplay("(count={Count})")]
    public class CSequence : CObject, IList<CObject> // , ICollection<CObject>, IEnumerable<CObject>
    {
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        public new CSequence Clone()
        {
            return (CSequence)Copy();
        }

        /// <summary>
        /// Implements the copy mechanism of this class.
        /// </summary>
        protected override CObject Copy()
        {
            var obj = base.Copy();
            _items = new List<CObject>(_items);
            for (var idx = 0; idx < _items.Count; idx++)
            {
                _items[idx] = _items[idx].Clone();
            }

            return obj;
        }

        /// <summary>
        /// Adds the specified sequence.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        public void Add(CSequence sequence)
        {
            var count = sequence.Count;
            for (var idx = 0; idx < count; idx++)
            {
                _items.Add(sequence[idx]);
            }
        }

        #region IList Members

        /// <summary>
        /// Adds the specified value add the end of the sequence.
        /// </summary>
        public void Add(CObject value)
        {
            _items.Add(value);
        }

        /// <summary>
        /// Removes all elements from the sequence.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }

        //bool IList.Contains(object value)
        //{
        //  return items.Contains(value);
        //}

        /// <summary>
        /// Determines whether the specified value is in the sequence.
        /// </summary>
        public bool Contains(CObject value)
        {
            return _items.Contains(value);
        }

        /// <summary>
        /// Returns the index of the specified value in the sequence or -1, if no such value is in the sequence.
        /// </summary>
        public int IndexOf(CObject value)
        {
            return _items.IndexOf(value);
        }

        /// <summary>
        /// Inserts the specified value in the sequence.
        /// </summary>
        public void Insert(int index, CObject value)
        {
            _items.Insert(index, value);
        }

        /////// <summary>
        /////// Gets a value indicating whether the sequence has a fixed size.
        /////// </summary>
        ////public bool IsFixedSize
        ////{
        ////  get { return items.IsFixedSize; }
        ////}

        /////// <summary>
        /////// Gets a value indicating whether the sequence is read-only.
        /////// </summary>
        ////public bool IsReadOnly
        ////{
        ////  get { return items.IsReadOnly; }
        ////}

        /// <summary>
        /// Removes the specified value from the sequence.
        /// </summary>
        public bool Remove(CObject value)
        {
            return _items.Remove(value);
        }

        /// <summary>
        /// Removes the value at the specified index from the sequence.
        /// </summary>
        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        /// <summary>
        /// Gets or sets a CObject at the specified index.
        /// </summary>
        /// <value></value>
        public CObject this[int index]
        {
            get { return (CObject)_items[index]; }
            set { _items[index] = value; }
        }

        #endregion

        #region ICollection Members

        /// <summary>
        /// Copies the elements of the sequence to the specified array.
        /// </summary>
        public void CopyTo(CObject[] array, int index)
        {
            _items.CopyTo(array, index);
        }

        /// <summary>
        /// Gets the number of elements contained in the sequence.
        /// </summary>
        public int Count
        {
            get { return _items.Count; }
        }

        ///// <summary>
        ///// Gets a value indicating whether access to the sequence is synchronized (thread safe).
        ///// </summary>
        //public bool IsSynchronized
        //{
        //  get { return items.IsSynchronized; }
        //}

        ///// <summary>
        ///// Gets an object that can be used to synchronize access to the sequence.
        ///// </summary>
        //public object SyncRoot
        //{
        //  get { return items.SyncRoot; }
        //}

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through the sequence.
        /// </summary>
        public IEnumerator<CObject> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Converts the sequence to a PDF content stream.
        /// </summary>
        public byte[] ToContent()
        {
            Stream stream = new MemoryStream();
            var writer = new ContentWriter(stream);
            WriteObject(writer);
            writer.Close(false);

            stream.Position = 0;
            var count = (int)stream.Length;
            var bytes = new byte[count];
            stream.Read(bytes, 0, count);
#if !UWP
            stream.Close();
#else
            stream.Dispose();
#endif
            return bytes;
        }

        /// <summary>
        /// Returns a string containing all elements of the sequence.
        /// </summary>
        public override string ToString()
        {
            var s = new StringBuilder();

            for (var idx = 0; idx < _items.Count; idx++)
            {
                s.Append(_items[idx]);
            }

            return s.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal override void WriteObject(ContentWriter writer)
        {
            for (var idx = 0; idx < _items.Count; idx++)
            {
                _items[idx].WriteObject(writer);
            }
        }

        #region IList<CObject> Members

        int IList<CObject>.IndexOf(CObject item)
        {
            throw new NotImplementedException();
        }

        void IList<CObject>.Insert(int index, CObject item)
        {
            throw new NotImplementedException();
        }

        void IList<CObject>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        CObject IList<CObject>.this[int index]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #endregion

        #region ICollection<CObject> Members

        void ICollection<CObject>.Add(CObject item)
        {
            throw new NotImplementedException();
        }

        void ICollection<CObject>.Clear()
        {
            throw new NotImplementedException();
        }

        bool ICollection<CObject>.Contains(CObject item)
        {
            throw new NotImplementedException();
        }

        void ICollection<CObject>.CopyTo(CObject[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        int ICollection<CObject>.Count
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<CObject>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<CObject>.Remove(CObject item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<CObject> Members

        IEnumerator<CObject> IEnumerable<CObject>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        List<CObject> _items = new List<CObject>();
    }
}
