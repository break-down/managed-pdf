using BreakDown.ManagedPdf.Core.Drawing;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// Base class for all renderer infos which defines an area.
    /// </summary>
    internal abstract class AreaRendererInfo : RendererInfo
    {
        /// <summary>
        /// Gets or sets the x coordinate of this rectangle.
        /// </summary>
        internal virtual double X
        {
            get { return _rect.X; }
            set { _rect.X = value; }
        }

        /// <summary>
        /// Gets or sets the y coordinate of this rectangle.
        /// </summary>
        internal virtual double Y
        {
            get { return _rect.Y; }
            set { _rect.Y = value; }
        }

        /// <summary>
        /// Gets or sets the width of this rectangle.
        /// </summary>
        internal virtual double Width
        {
            get { return _rect.Width; }
            set { _rect.Width = value; }
        }

        /// <summary>
        /// Gets or sets the height of this rectangle.
        /// </summary>
        internal virtual double Height
        {
            get { return _rect.Height; }
            set { _rect.Height = value; }
        }

        /// <summary>
        /// Gets the area's size.
        /// </summary>
        internal XSize Size
        {
            get { return _rect.Size; }
            set { _rect.Size = value; }
        }

        /// <summary>
        /// Gets the area's rectangle.
        /// </summary>
        internal XRect Rect
        {
            get { return _rect; }
            set { _rect = value; }
        }

        XRect _rect;
    }
}
