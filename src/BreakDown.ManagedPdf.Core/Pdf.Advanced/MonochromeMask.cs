namespace BreakDown.ManagedPdf.Core.Pdf.Advanced
{
    /// <summary>
    /// Helper class for creating bitmap masks (8 pels per byte).
    /// </summary>
    class MonochromeMask
    {
        /// <summary>
        /// Returns the bitmap mask that will be written to PDF.
        /// </summary>
        public byte[] MaskData
        {
            get { return _maskData; }
        }

        private readonly byte[] _maskData;

        /// <summary>
        /// Creates a bitmap mask.
        /// </summary>
        public MonochromeMask(int sizeX, int sizeY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            var byteSize = ((sizeX + 7) / 8) * sizeY;
            _maskData = new byte[byteSize];
            StartLine(0);
        }

        /// <summary>
        /// Starts a new line.
        /// </summary>
        public void StartLine(int newCurrentLine)
        {
            _bitsWritten = 0;
            _byteBuffer = 0;
            _writeOffset = ((_sizeX + 7) / 8) * (_sizeY - 1 - newCurrentLine);
        }

        /// <summary>
        /// Adds a pel to the current line.
        /// </summary>
        /// <param name="isTransparent"></param>
        public void AddPel(bool isTransparent)
        {
            if (_bitsWritten < _sizeX)
            {
                // Mask: 0: opaque, 1: transparent (default mapping)
                if (isTransparent)
                {
                    _byteBuffer = (_byteBuffer << 1) + 1;
                }
                else
                {
                    _byteBuffer = _byteBuffer << 1;
                }

                ++_bitsWritten;
                if ((_bitsWritten & 7) == 0)
                {
                    _maskData[_writeOffset] = (byte)_byteBuffer;
                    ++_writeOffset;
                    _byteBuffer = 0;
                }
                else if (_bitsWritten == _sizeX)
                {
                    var n = 8 - (_bitsWritten & 7);
                    _byteBuffer = _byteBuffer << n;
                    _maskData[_writeOffset] = (byte)_byteBuffer;
                }
            }
        }

        /// <summary>
        /// Adds a pel from an alpha mask value.
        /// </summary>
        public void AddPel(int shade)
        {
            // NYI: dithering.
            AddPel(shade < 128);
        }

        private readonly int _sizeX;
        private readonly int _sizeY;
        private int _writeOffset;
        private int _byteBuffer;
        private int _bitsWritten;
    }
}
