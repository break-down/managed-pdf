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

using System;
using BreakDown.ManagedPdf.Charting.Charting.enums;
using BreakDown.ManagedPdf.Core.Drawing;
using BreakDown.ManagedPdf.Core.Drawing.enums;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// Represents a data label renderer for pie charts.
    /// </summary>
    internal class PieDataLabelRenderer : DataLabelRenderer
    {
        /// <summary>
        /// Initializes a new instance of the PieDataLabelRenderer class with the
        /// specified renderer parameters.
        /// </summary>
        internal PieDataLabelRenderer(RendererParameters parms)
            : base(parms)
        {
        }

        /// <summary>
        /// Calculates the space used by the data labels.
        /// </summary>
        internal override void Format()
        {
            var cri = (ChartRendererInfo)_rendererParms.RendererInfo;
            if (cri.seriesRendererInfos.Length == 0)
            {
                return;
            }

            var sri = cri.seriesRendererInfos[0];
            if (sri._dataLabelRendererInfo == null)
            {
                return;
            }

            var sumValues = sri.SumOfPoints;
            var gfx = _rendererParms.Graphics;

            sri._dataLabelRendererInfo.Entries = new DataLabelEntryRendererInfo[sri._pointRendererInfos.Length];
            var index = 0;
            foreach (SectorRendererInfo sector in sri._pointRendererInfos)
            {
                var dleri = new DataLabelEntryRendererInfo();
                if (sri._dataLabelRendererInfo.Type != DataLabelType.None)
                {
                    if (sri._dataLabelRendererInfo.Type == DataLabelType.Percent)
                    {
                        var percent = 100 / (sumValues / Math.Abs(sector.Point._value));
                        dleri.Text = percent.ToString(sri._dataLabelRendererInfo.Format) + "%";
                    }
                    else if (sri._dataLabelRendererInfo.Type == DataLabelType.Value)
                    {
                        dleri.Text = sector.Point._value.ToString(sri._dataLabelRendererInfo.Format);
                    }

                    if (dleri.Text.Length > 0)
                    {
                        dleri.Size = gfx.MeasureString(dleri.Text, sri._dataLabelRendererInfo.Font);
                    }
                }

                sri._dataLabelRendererInfo.Entries[index++] = dleri;
            }
        }

        /// <summary>
        /// Draws the data labels of the pie chart.
        /// </summary>
        internal override void Draw()
        {
            var cri = (ChartRendererInfo)_rendererParms.RendererInfo;
            if (cri.seriesRendererInfos.Length == 0)
            {
                return;
            }

            var sri = cri.seriesRendererInfos[0];
            if (sri == null || sri._dataLabelRendererInfo == null)
            {
                return;
            }

            var gfx = _rendererParms.Graphics;
            var font = sri._dataLabelRendererInfo.Font;
            var fontColor = sri._dataLabelRendererInfo.FontColor;
            var format = XStringFormats.Center;
            format.LineAlignment = XLineAlignment.Center;
            foreach (var dataLabel in sri._dataLabelRendererInfo.Entries)
            {
                if (dataLabel.Text != null)
                {
                    gfx.DrawString(dataLabel.Text, font, fontColor, dataLabel.Rect, format);
                }
            }
        }

        /// <summary>
        /// Calculates the data label positions specific for pie charts.
        /// </summary>
        internal override void CalcPositions()
        {
            var cri = (ChartRendererInfo)_rendererParms.RendererInfo;
            var gfx = _rendererParms.Graphics;

            if (cri.seriesRendererInfos.Length > 0)
            {
                var sri = cri.seriesRendererInfos[0];
                if (sri != null && sri._dataLabelRendererInfo != null)
                {
                    var sectorIndex = 0;
                    foreach (SectorRendererInfo sector in sri._pointRendererInfos)
                    {
                        // Determine output rectangle
                        var midAngle = sector.StartAngle + sector.SweepAngle / 2;
                        var radMidAngle = midAngle / 180 * Math.PI;
                        var origin = new XPoint(sector.Rect.X + sector.Rect.Width / 2,
                                                sector.Rect.Y + sector.Rect.Height / 2);
                        var radius = sector.Rect.Width / 2;
                        var halfradius = radius / 2;

                        var dleri = sri._dataLabelRendererInfo.Entries[sectorIndex++];
                        switch (sri._dataLabelRendererInfo.Position)
                        {
                            case DataLabelPosition.OutsideEnd:
                                // Outer border of the circle.
                                dleri.X = origin.X + (radius * Math.Cos(radMidAngle));
                                dleri.Y = origin.Y + (radius * Math.Sin(radMidAngle));
                                if (dleri.X < origin.X)
                                {
                                    dleri.X -= dleri.Width;
                                }

                                if (dleri.Y < origin.Y)
                                {
                                    dleri.Y -= dleri.Height;
                                }

                                break;

                            case DataLabelPosition.InsideEnd:
                                // Inner border of the circle.
                                dleri.X = origin.X + (radius * Math.Cos(radMidAngle));
                                dleri.Y = origin.Y + (radius * Math.Sin(radMidAngle));
                                if (dleri.X > origin.X)
                                {
                                    dleri.X -= dleri.Width;
                                }

                                if (dleri.Y > origin.Y)
                                {
                                    dleri.Y -= dleri.Height;
                                }

                                break;

                            case DataLabelPosition.Center:
                                // Centered
                                dleri.X = origin.X + (halfradius * Math.Cos(radMidAngle));
                                dleri.Y = origin.Y + (halfradius * Math.Sin(radMidAngle));
                                dleri.X -= dleri.Width / 2;
                                dleri.Y -= dleri.Height / 2;
                                break;

                            case DataLabelPosition.InsideBase:
                                // Aligned at the base/center of the circle
                                dleri.X = origin.X;
                                dleri.Y = origin.Y;
                                if (dleri.X < origin.X)
                                {
                                    dleri.X -= dleri.Width;
                                }

                                if (dleri.Y < origin.Y)
                                {
                                    dleri.Y -= dleri.Height;
                                }

                                break;
                        }
                    }
                }
            }
        }
    }
}
