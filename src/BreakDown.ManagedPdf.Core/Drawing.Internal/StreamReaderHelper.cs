using System;
using System.Diagnostics;
using System.IO;

namespace BreakDown.ManagedPdf.Core.Drawing.Internal
{
    /// <summary>
    /// Helper for dealing with Stream data.
    /// </summary>
    internal class StreamReaderHelper
    {
        internal StreamReaderHelper(Stream stream)
        {
#if GDI || WPF
            _stream = stream;
            MemoryStream ms = stream as MemoryStream;
            if (ms == null)
            {
                // THHO4STLA byte[] or MemoryStream?
                _ownedMemoryStream = ms = new MemoryStream();
                CopyStream(stream, ms);
                // For .NET 4: stream.CopyTo(ms);
            }
            _data = ms.GetBuffer();
            _length = (int)ms.Length;
#else

            // For WinRT there is no GetBuffer() => alternative implementation for WinRT.
            // TODO: Are there advantages of GetBuffer()? It should reduce LOH fragmentation.
            _stream = stream;
            _stream.Position = 0;
            if (_stream.Length > int.MaxValue)
            {
                throw new ArgumentException("Stream is too large.", "stream");
            }

            _length = (int)_stream.Length;
            _data = new byte[_length];
            _stream.Read(_data, 0, _length);
#endif
        }

        internal byte GetByte(int offset)
        {
            if (_currentOffset + offset >= _length)
            {
                Debug.Assert(false);
                return 0;
            }

            return _data[_currentOffset + offset];
        }

        internal ushort GetWord(int offset, bool bigEndian)
        {
            return (ushort)(bigEndian ? GetByte(offset) * 256 + GetByte(offset + 1) : GetByte(offset) + GetByte(offset + 1) * 256);
        }

        internal uint GetDWord(int offset, bool bigEndian)
        {
            return (uint)(bigEndian ? GetWord(offset, true) * 65536 + GetWord(offset + 2, true) : GetWord(offset, false) + GetWord(offset + 2, false) * 65536);
        }

        private static void CopyStream(Stream input, Stream output)
        {
            var buffer = new byte[65536];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            _currentOffset = 0;
        }

        /// <summary>
        /// Gets the original stream.
        /// </summary>
        public Stream OriginalStream
        {
            get { return _stream; }
        }

        private readonly Stream _stream;

        internal int CurrentOffset
        {
            get { return _currentOffset; }
            set { _currentOffset = value; }
        }

        private int _currentOffset;

        /// <summary>
        /// Gets the data as byte[].
        /// </summary>
        public byte[] Data
        {
            get { return _data; }
        }

        private readonly byte[] _data;

        /// <summary>
        /// Gets the length of Data.
        /// </summary>
        public int Length
        {
            get { return _length; }
        }

        private readonly int _length;

#if GDI || WPF
        /// <summary>
        /// Gets the owned memory stream. Can be null if no MemoryStream was created.
        /// </summary>
        public MemoryStream OwnedMemoryStream
        {
            get { return _ownedMemoryStream; }
        }
        private readonly MemoryStream _ownedMemoryStream;
#endif
    }
}
