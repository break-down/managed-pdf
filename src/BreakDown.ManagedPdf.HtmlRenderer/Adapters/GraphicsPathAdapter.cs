// "Therefore those skilled at the unorthodox
// are infinite as heaven and earth,
// inexhaustible as the great rivers.
// When they come to an end,
// they begin again,
// like the days and months;
// they die and are reborn,
// like the four seasons."
// 
// - Sun Tsu,
// "The Art of War"

using System;
using BreakDown.ManagedPdf.Core.Drawing;
using BreakDown.ManagedPdf.Html.Adapters;
using BreakDown.ManagedPdf.Html.Adapters.Entities;

namespace BreakDown.ManagedPdf.HtmlRenderer.Adapters
{
    /// <summary>
    /// Adapter for WinForms graphics path object for core.
    /// </summary>
    internal sealed class GraphicsPathAdapter : RGraphicsPath
    {
        /// <summary>
        /// the last point added to the path to begin next segment from
        /// </summary>
        private RPoint lastPoint;

        /// <summary>
        /// The actual BreakDown.ManagedPdf.Core graphics path instance.
        /// </summary>
        public XGraphicsPath GraphicsPath { get; } = new();

        public override void Start(double x, double y)
        {
            lastPoint = new RPoint(x, y);
        }

        public override void LineTo(double x, double y)
        {
            GraphicsPath.AddLine((float)lastPoint.X, (float)lastPoint.Y, (float)x, (float)y);
            lastPoint = new RPoint(x, y);
        }

        public override void ArcTo(double x, double y, double size, Corner corner)
        {
            var left = (float)(Math.Min(x, lastPoint.X) - (corner == Corner.TopRight || corner == Corner.BottomRight ? size : 0));
            var top = (float)(Math.Min(y, lastPoint.Y) - (corner == Corner.BottomLeft || corner == Corner.BottomRight ? size : 0));
            GraphicsPath.AddArc(left, top, (float)size * 2, (float)size * 2, GetStartAngle(corner), 90);
            lastPoint = new RPoint(x, y);
        }

        public override void Dispose()
        {
        }

        /// <summary>
        /// Get arc start angle for the given corner.
        /// </summary>
        private static int GetStartAngle(Corner corner) => corner switch
        {
            Corner.TopLeft => 180,
            Corner.TopRight => 270,
            Corner.BottomLeft => 90,
            Corner.BottomRight => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(corner))
        };
    }
}
