using System.Diagnostics;

namespace BreakDown.ManagedPdf.Core.Pdf.Advanced
{
    /// <summary>
    /// The BitReader class is a helper to read bits from an in-memory bitmap file.
    /// </summary>
    class BitReader
    {
        readonly byte[] _imageBits;
        uint _bytesOffsetRead;
        readonly uint _bytesFileOffset;
        byte _buffer;
        uint _bitsInBuffer;
        readonly uint _bitsTotal; // Bits we may read (bits per image line)

        /// <summary>
        /// Initializes a new instance of the <see cref="BitReader"/> class.
        /// </summary>
        /// <param name="imageBits">The in-memory bitmap file.</param>
        /// <param name="bytesFileOffset">The offset of the line to read.</param>
        /// <param name="bits">The count of bits that may be read (i. e. the width of the image for normal usage).</param>
        internal BitReader(byte[] imageBits, uint bytesFileOffset, uint bits)
        {
            _imageBits = imageBits;
            _bytesFileOffset = bytesFileOffset;
            _bitsTotal = bits;
            _bytesOffsetRead = bytesFileOffset;
            _buffer = imageBits[_bytesOffsetRead];
            _bitsInBuffer = 8;
        }

        /// <summary>
        /// Sets the position within the line (needed for 2D encoding).
        /// </summary>
        /// <param name="position">The new position.</param>
        internal void SetPosition(uint position)
        {
            _bytesOffsetRead = _bytesFileOffset + (position >> 3);
            _buffer = _imageBits[_bytesOffsetRead];
            _bitsInBuffer = 8 - (position & 0x07);
        }

        /// <summary>
        /// Gets a single bit at the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>True if bit is set.</returns>
        internal bool GetBit(uint position)
        {
            if (position >= _bitsTotal)
            {
                return false;
            }

            SetPosition(position);
            return (PeekByte(out var dummy) & 0x80) > 0;
        }

        /// <summary>
        /// Returns the bits that are in the buffer (without changing the position).
        /// Data is MSB aligned.
        /// </summary>
        /// <param name="bits">The count of bits that were returned (1 through 8).</param>
        /// <returns>The MSB aligned bits from the buffer.</returns>
        internal byte PeekByte(out uint bits)
        {
            // TODO: try to make this faster!
            if (_bitsInBuffer == 8)
            {
                bits = 8;
                return _buffer;
            }

            bits = _bitsInBuffer;
            return (byte)(_buffer << (int)(8 - _bitsInBuffer));
        }

        /// <summary>
        /// Moves the buffer to the next byte.
        /// </summary>
        internal void NextByte()
        {
            _buffer = _imageBits[++_bytesOffsetRead];
            _bitsInBuffer = 8;
        }

        /// <summary>
        /// "Removes" (eats) bits from the buffer.
        /// </summary>
        /// <param name="bits">The count of bits that were processed.</param>
        internal void SkipBits(uint bits)
        {
            Debug.Assert(bits <= _bitsInBuffer, "Buffer underrun");
            if (bits == _bitsInBuffer)
            {
                NextByte();
                return;
            }

            _bitsInBuffer -= bits;
        }
    }
}
