using System;

namespace BreakDown.ManagedPdf.Core.Pdf.Advanced
{
    /// <summary>
    /// A helper class for writing groups of bits into an array of bytes.
    /// </summary>
    class BitWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BitWriter"/> class.
        /// </summary>
        /// <param name="imageData">The byte array to be written to.</param>
        internal BitWriter(ref byte[] imageData)
        {
            _imageData = imageData;
        }

        /// <summary>
        /// Writes the buffered bits into the byte array.
        /// </summary>
        internal void FlushBuffer()
        {
            if (_bitsInBuffer > 0)
            {
                var bits = 8 - _bitsInBuffer;
                WriteBits(0, bits);
            }
        }

        /// <summary>
        /// Masks for n bits in a byte (with n = 0 through 8).
        /// </summary>
        static readonly uint[] masks = { 0, 1, 3, 7, 15, 31, 63, 127, 255 };

        /// <summary>
        /// Writes bits to the byte array.
        /// </summary>
        /// <param name="value">The bits to be written (LSB aligned).</param>
        /// <param name="bits">The count of bits.</param>
        internal void WriteBits(uint value, uint bits)
        {
#if true

            // TODO: Try to make this faster!

            // If we have to write more bits than fit into the buffer, we fill
            // the buffer and call the same routine recursively for the rest.
#if USE_GOTO
            // Use GOTO instead of end recursion: (is this faster?)
            SimulateRecursion:
#endif
            if (bits + _bitsInBuffer > 8)
            {
                // We can't add all bits this time.
                var bitsNow = 8 - _bitsInBuffer;
                var bitsRemainder = bits - bitsNow;
                WriteBits(value >> (int)(bitsRemainder), bitsNow); // that fits
#if USE_GOTO
                bits = bitsRemainder;
                goto SimulateRecursion;
#else
                WriteBits(value, bitsRemainder);
                return;
#endif
            }

            _buffer = (_buffer << (int)bits) + (value & masks[bits]);
            _bitsInBuffer += bits;

            if (_bitsInBuffer == 8)
            {
                // The line below will sometimes throw a System.IndexOutOfRangeException while PDFsharp tries different formats for monochrome bitmaps (exception occurs if CCITT encoding requires more space than an uncompressed bitmap).
                _imageData[_bytesOffsetWrite] = (byte)_buffer;
                _bitsInBuffer = 0;
                ++_bytesOffsetWrite;
            }
#else
            // Simple implementation writing bit by bit:
            int mask = 1 << (int)(bits - 1);
            for (int b = 0; b < bits; ++b)
            {
                if ((value & mask) != 0)
                    buffer = (buffer << 1) + 1;
                else
                    buffer = buffer << 1;
                ++bitsInBuffer;
                mask /= 2;
                if (bitsInBuffer == 8)
                {
                    imageData[bytesOffsetWrite] = (byte)buffer;
                    bitsInBuffer = 0;
                    ++bytesOffsetWrite;
                }
            }
#endif
        }

        /// <summary>
        /// Writes a line from a look-up table.
        /// A "line" in the table are two integers, one containing the values, one containing the bit count.
        /// </summary>
        internal void WriteTableLine(uint[] table, uint line)
        {
            var value = table[line * 2];
            var bits = table[line * 2 + 1];
            WriteBits(value, bits);
        }

        [Obsolete]
        internal void WriteEOL()
        {
            // Not needed for PDF.
            WriteTableLine(PdfImage.WhiteMakeUpCodes, 40);
        }

        /// <summary>
        /// Flushes the buffer and returns the count of bytes written to the array.
        /// </summary>
        internal int BytesWritten()
        {
            FlushBuffer();
            return _bytesOffsetWrite;
        }

        int _bytesOffsetWrite;
        readonly byte[] _imageData;
        uint _buffer;
        uint _bitsInBuffer;
    }
}
