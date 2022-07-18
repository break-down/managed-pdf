﻿using System;
using System.IO;

namespace BreakDown.ManagedPdf.Core.SharpZipLib.Zip.Compression.Streams
{
    /// <summary>
    /// An input buffer customised for use by <see cref="InflaterInputStream"/>
    /// </summary>
    /// <remarks>
    /// The buffer supports decryption of incoming data.
    /// </remarks>
    internal class InflaterInputBuffer
    {
        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="InflaterInputBuffer"/> with a default buffer size
        /// </summary>
        /// <param name="stream">The stream to buffer.</param>
        public InflaterInputBuffer(Stream stream)
            : this(stream, 4096)
        {
        }

        /// <summary>
        /// Initialise a new instance of <see cref="InflaterInputBuffer"/>
        /// </summary>
        /// <param name="stream">The stream to buffer.</param>
        /// <param name="bufferSize">The size to use for the buffer</param>
        /// <remarks>A minimum buffer size of 1KB is permitted.  Lower sizes are treated as 1KB.</remarks>
        public InflaterInputBuffer(Stream stream, int bufferSize)
        {
            inputStream = stream;
            if (bufferSize < 1024)
            {
                bufferSize = 1024;
            }

            rawData = new byte[bufferSize];
            clearText = rawData;
        }

        #endregion

        /// <summary>
        /// Get the length of bytes bytes in the <see cref="RawData"/>
        /// </summary>
        public int RawLength
        {
            get { return rawLength; }
        }

        /// <summary>
        /// Get the contents of the raw data buffer.
        /// </summary>
        /// <remarks>This may contain encrypted data.</remarks>
        public byte[] RawData
        {
            get { return rawData; }
        }

        /// <summary>
        /// Get the number of useable bytes in <see cref="ClearText"/>
        /// </summary>
        public int ClearTextLength
        {
            get { return clearTextLength; }
        }

        /// <summary>
        /// Get the contents of the clear text buffer.
        /// </summary>
        public byte[] ClearText
        {
            get { return clearText; }
        }

        /// <summary>
        /// Get/set the number of bytes available
        /// </summary>
        public int Available
        {
            get { return available; }
            set { available = value; }
        }

        /// <summary>
        /// Call <see cref="Inflater.SetInput(byte[], int, int)"/> passing the current clear text buffer contents.
        /// </summary>
        /// <param name="inflater">The inflater to set input for.</param>
        public void SetInflaterInput(Inflater inflater)
        {
            if (available > 0)
            {
                inflater.SetInput(clearText, clearTextLength - available, available);
                available = 0;
            }
        }

        /// <summary>
        /// Fill the buffer from the underlying input stream.
        /// </summary>
        public void Fill()
        {
            rawLength = 0;
            var toRead = rawData.Length;

            while (toRead > 0)
            {
                var count = inputStream.Read(rawData, rawLength, toRead);
                if (count <= 0)
                {
                    break;
                }

                rawLength += count;
                toRead -= count;
            }

#if false //!NETCF_1_0
			if ( cryptoTransform != null ) {
				clearTextLength = cryptoTransform.TransformBlock(rawData, 0, rawLength, clearText, 0);
			}
			else
#endif
            {
                clearTextLength = rawLength;
            }

            available = clearTextLength;
        }

        /// <summary>
        /// Read a buffer directly from the input stream
        /// </summary>
        /// <param name="buffer">The buffer to fill</param>
        /// <returns>Returns the number of bytes read.</returns>
        public int ReadRawBuffer(byte[] buffer)
        {
            return ReadRawBuffer(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Read a buffer directly from the input stream
        /// </summary>
        /// <param name="outBuffer">The buffer to read into</param>
        /// <param name="offset">The offset to start reading data into.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>Returns the number of bytes read.</returns>
        public int ReadRawBuffer(byte[] outBuffer, int offset, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }

            var currentOffset = offset;
            var currentLength = length;

            while (currentLength > 0)
            {
                if (available <= 0)
                {
                    Fill();
                    if (available <= 0)
                    {
                        return 0;
                    }
                }

                var toCopy = Math.Min(currentLength, available);
                System.Array.Copy(rawData, rawLength - (int)available, outBuffer, currentOffset, toCopy);
                currentOffset += toCopy;
                currentLength -= toCopy;
                available -= toCopy;
            }

            return length;
        }

        /// <summary>
        /// Read clear text data from the input stream.
        /// </summary>
        /// <param name="outBuffer">The buffer to add data to.</param>
        /// <param name="offset">The offset to start adding data at.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>Returns the number of bytes actually read.</returns>
        public int ReadClearTextBuffer(byte[] outBuffer, int offset, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }

            var currentOffset = offset;
            var currentLength = length;

            while (currentLength > 0)
            {
                if (available <= 0)
                {
                    Fill();
                    if (available <= 0)
                    {
                        return 0;
                    }
                }

                var toCopy = Math.Min(currentLength, available);
                Array.Copy(clearText, clearTextLength - (int)available, outBuffer, currentOffset, toCopy);
                currentOffset += toCopy;
                currentLength -= toCopy;
                available -= toCopy;
            }

            return length;
        }

        /// <summary>
        /// Read a <see cref="byte"/> from the input stream.
        /// </summary>
        /// <returns>Returns the byte read.</returns>
        public int ReadLeByte()
        {
            if (available <= 0)
            {
                Fill();
                if (available <= 0)
                {
                    throw new ZipException("EOF in header");
                }
            }

            var result = rawData[rawLength - available];
            available -= 1;
            return result;
        }

        /// <summary>
        /// Read an <see cref="short"/> in little endian byte order.
        /// </summary>
        /// <returns>The short value read case to an int.</returns>
        public int ReadLeShort()
        {
            return ReadLeByte() | (ReadLeByte() << 8);
        }

        /// <summary>
        /// Read an <see cref="int"/> in little endian byte order.
        /// </summary>
        /// <returns>The int value read.</returns>
        public int ReadLeInt()
        {
            return ReadLeShort() | (ReadLeShort() << 16);
        }

        /// <summary>
        /// Read a <see cref="long"/> in little endian byte order.
        /// </summary>
        /// <returns>The long value read.</returns>
        public long ReadLeLong()
        {
            return (uint)ReadLeInt() | ((long)ReadLeInt() << 32);
        }

#if false //!NETCF_1_0
		/// <summary>
		/// Get/set the <see cref="ICryptoTransform"/> to apply to any data.
		/// </summary>
		/// <remarks>Set this value to null to have no transform applied.</remarks>
		public ICryptoTransform CryptoTransform
		{
			set { 
				cryptoTransform = value;
				if ( cryptoTransform != null ) {
					if ( rawData == clearText ) {
						if ( internalClearText == null ) {
							internalClearText = new byte[rawData.Length];
						}
						clearText = internalClearText;
					}
					clearTextLength = rawLength;
					if ( available > 0 ) {
						cryptoTransform.TransformBlock(rawData, rawLength - available, available, clearText, rawLength - available);
					}
				} else {
					clearText = rawData;
					clearTextLength = rawLength;
				}
			}
		}
#endif

        #region Instance Fields

        int rawLength;
        byte[] rawData;

        int clearTextLength;
        byte[] clearText;
#if false //!NETCF_1_0		
		byte[] internalClearText;
#endif

        int available;

#if false //!NETCF_1_0
		ICryptoTransform cryptoTransform;
#endif
        Stream inputStream;

        #endregion
    }
}
