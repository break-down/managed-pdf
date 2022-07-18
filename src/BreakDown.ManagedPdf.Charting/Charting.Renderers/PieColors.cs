using BreakDown.ManagedPdf.Core.Drawing;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// Represents the predefined pie chart colors.
    /// </summary>
    internal sealed class PieColors
    {
        /// <summary>
        /// Gets the color for pie charts from the specified index.
        /// </summary>
        public static XColor Item(int index)
        {
            return XColor.FromArgb((int)_sectorColors[index]);
        }

        /// <summary>
        /// Colors for pie charts taken from Excel.
        /// </summary>
        static readonly uint[] _sectorColors = new uint[]
        {
            0xFF9999FF, 0xFF993366, 0xFFFFFFCC, 0xFFCCFFFF, 0xFF660066, 0xFFFF8080,
            0xFF0066CC, 0xFFCCCCFF, 0xFF000080, 0xFFFF00FF, 0xFFFFFF00, 0xFF00FFFF,
            0xFF800080, 0xFF800000, 0xFF008080, 0xFF0000FF, 0xFF00CCFF, 0xFFCCFFFF,
            0xFFCCFFCC, 0xFFFFFF99, 0xFF99CCFF, 0xFFFF99CC, 0xFFCC99FF, 0xFFFFCC99,
            0xFF3366FF, 0xFF33CCCC, 0xFF99CC00, 0xFFFFCC00, 0xFFFF9900, 0xFFFF6600,
            0xFF666699, 0xFF969696, 0xFF003366, 0xFF339966, 0xFF003300, 0xFF333300,
            0xFF993300, 0xFF993366, 0xFF333399, 0xFF333333, 0xFF000000, 0xFFFFFFFF,
            0xFFFF0000, 0xFF00FF00, 0xFF0000FF, 0xFFFFFF00, 0xFFFF00FF, 0xFF00FFFF,
            0xFF800000, 0xFF008000, 0xFF000080, 0xFF808000, 0xFF800080, 0xFF008080,
            0xFFC0C0C0, 0xFF808080
        };
    }
}
