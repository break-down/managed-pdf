#region PDFsharp Charting - A .NET charting library based on PDFsharp

//
// Authors:
//   Niklas Schneider
//
// Copyright (c) 2005-2019 empira Software GmbH, Cologne Area (Germany)
//
// http://www.pdfsharp.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

#endregion

using BreakDown.ManagedPdf.Core.Drawing;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// Represents gridlines used by bar charts, i. e. X axis grid will be rendered
    /// from left to right and Y axis grid will be rendered from top to bottom of the plot area.
    /// </summary>
    internal class BarGridlinesRenderer : GridlinesRenderer
    {
        /// <summary>
        /// Initializes a new instance of the BarGridlinesRenderer class with the
        /// specified renderer parameters.
        /// </summary>
        internal BarGridlinesRenderer(RendererParameters parms)
            : base(parms)
        {
        }

        /// <summary>
        /// Draws the gridlines into the plot area.
        /// </summary>
        internal override void Draw()
        {
            var cri = (ChartRendererInfo)_rendererParms.RendererInfo;

            var plotAreaRect = cri.plotAreaRendererInfo.Rect;
            if (plotAreaRect.IsEmpty)
            {
                return;
            }

            var xari = cri.xAxisRendererInfo;
            var yari = cri.yAxisRendererInfo;

            var xMin = xari.MinimumScale;
            var xMax = xari.MaximumScale;
            var yMin = yari.MinimumScale;
            var yMax = yari.MaximumScale;
            var xMajorTick = xari.MajorTick;
            var yMajorTick = yari.MajorTick;
            var xMinorTick = xari.MinorTick;
            var yMinorTick = yari.MinorTick;
            var xMaxExtension = xari.MajorTick;

            var matrix = cri.plotAreaRendererInfo._matrix;

            LineFormatRenderer lineFormatRenderer;
            var gfx = _rendererParms.Graphics;

            var points = new XPoint[2];
            if (xari.MinorGridlinesLineFormat != null)
            {
                lineFormatRenderer = new LineFormatRenderer(gfx, xari.MinorGridlinesLineFormat);
                for (var x = xMin + xMinorTick; x < xMax; x += xMinorTick)
                {
                    points[0].Y = x;
                    points[0].X = yMin;
                    points[1].Y = x;
                    points[1].X = yMax;
                    matrix.TransformPoints(points);
                    lineFormatRenderer.DrawLine(points[0], points[1]);
                }
            }

            if (xari.MajorGridlinesLineFormat != null)
            {
                lineFormatRenderer = new LineFormatRenderer(gfx, xari.MajorGridlinesLineFormat);
                for (var x = xMin; x <= xMax; x += xMajorTick)
                {
                    points[0].Y = x;
                    points[0].X = yMin;
                    points[1].Y = x;
                    points[1].X = yMax;
                    matrix.TransformPoints(points);
                    lineFormatRenderer.DrawLine(points[0], points[1]);
                }
            }

            if (yari.MinorGridlinesLineFormat != null)
            {
                lineFormatRenderer = new LineFormatRenderer(gfx, yari.MinorGridlinesLineFormat);
                for (var y = yMin + yMinorTick; y < yMax; y += yMinorTick)
                {
                    points[0].Y = xMin;
                    points[0].X = y;
                    points[1].Y = xMax;
                    points[1].X = y;
                    matrix.TransformPoints(points);
                    lineFormatRenderer.DrawLine(points[0], points[1]);
                }
            }

            if (yari.MajorGridlinesLineFormat != null)
            {
                lineFormatRenderer = new LineFormatRenderer(gfx, yari.MajorGridlinesLineFormat);
                for (var y = yMin; y <= yMax; y += yMajorTick)
                {
                    points[0].Y = xMin;
                    points[0].X = y;
                    points[1].Y = xMax;
                    points[1].X = y;
                    matrix.TransformPoints(points);
                    lineFormatRenderer.DrawLine(points[0], points[1]);
                }
            }
        }
    }
}
