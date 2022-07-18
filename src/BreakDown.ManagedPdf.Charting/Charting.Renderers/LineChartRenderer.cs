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

using BreakDown.ManagedPdf.Charting.Charting.enums;
using BreakDown.ManagedPdf.Core.Drawing;
using BreakDown.ManagedPdf.Core.Drawing.enums;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// Represents a line chart renderer.
    /// </summary>
    internal class LineChartRenderer : ColumnLikeChartRenderer
    {
        /// <summary>
        /// Initializes a new instance of the LineChartRenderer class with the specified renderer parameters.
        /// </summary>
        internal LineChartRenderer(RendererParameters parms)
            : base(parms)
        {
        }

        /// <summary>
        /// Returns an initialized and renderer specific rendererInfo.
        /// </summary>
        internal override RendererInfo Init()
        {
            var cri = new ChartRendererInfo();
            cri._chart = (Chart)_rendererParms.DrawingItem;
            _rendererParms.RendererInfo = cri;

            InitSeriesRendererInfo();

            LegendRenderer lr = new ColumnLikeLegendRenderer(_rendererParms);
            cri.legendRendererInfo = (LegendRendererInfo)lr.Init();

            AxisRenderer xar = new HorizontalXAxisRenderer(_rendererParms);
            cri.xAxisRendererInfo = (AxisRendererInfo)xar.Init();

            AxisRenderer yar = new VerticalYAxisRenderer(_rendererParms);
            cri.yAxisRendererInfo = (AxisRendererInfo)yar.Init();

            var plotArea = cri._chart.PlotArea;
            var lpar = new LinePlotAreaRenderer(_rendererParms);
            cri.plotAreaRendererInfo = (PlotAreaRendererInfo)lpar.Init();

            return cri;
        }

        /// <summary>
        /// Layouts and calculates the space used by the line chart.
        /// </summary>
        internal override void Format()
        {
            var cri = (ChartRendererInfo)_rendererParms.RendererInfo;

            LegendRenderer lr = new ColumnLikeLegendRenderer(_rendererParms);
            lr.Format();

            // axes
            AxisRenderer xar = new HorizontalXAxisRenderer(_rendererParms);
            xar.Format();

            AxisRenderer yar = new VerticalYAxisRenderer(_rendererParms);
            yar.Format();

            // Calculate rects and positions.
            CalcLayout();

            // Calculated remaining plot area, now it's safe to format.
            var lpar = new LinePlotAreaRenderer(_rendererParms);
            lpar.Format();
        }

        /// <summary>
        /// Draws the line chart.
        /// </summary>
        internal override void Draw()
        {
            var cri = (ChartRendererInfo)_rendererParms.RendererInfo;

            LegendRenderer lr = new ColumnLikeLegendRenderer(_rendererParms);
            lr.Draw();

            // Draw wall.
            var wr = new WallRenderer(_rendererParms);
            wr.Draw();

            // Draw gridlines.
            GridlinesRenderer glr = new ColumnLikeGridlinesRenderer(_rendererParms);
            glr.Draw();

            var pabr = new PlotAreaBorderRenderer(_rendererParms);
            pabr.Draw();

            // Draw line chart's plot area.
            var lpar = new LinePlotAreaRenderer(_rendererParms);
            lpar.Draw();

            // Draw x- and y-axis.
            if (cri.xAxisRendererInfo._axis != null)
            {
                AxisRenderer xar = new HorizontalXAxisRenderer(_rendererParms);
                xar.Draw();
            }

            if (cri.yAxisRendererInfo._axis != null)
            {
                AxisRenderer yar = new VerticalYAxisRenderer(_rendererParms);
                yar.Draw();
            }
        }

        /// <summary>
        /// Initializes all necessary data to draw a series for a line chart.
        /// </summary>
        private void InitSeriesRendererInfo()
        {
            var cri = (ChartRendererInfo)_rendererParms.RendererInfo;

            var seriesColl = cri._chart.SeriesCollection;
            cri.seriesRendererInfos = new SeriesRendererInfo[seriesColl.Count];
            for (var idx = 0; idx < seriesColl.Count; ++idx)
            {
                var sri = new SeriesRendererInfo();
                sri._series = seriesColl[idx];
                cri.seriesRendererInfos[idx] = sri;
            }

            InitSeries();
        }

        /// <summary>
        /// Initializes all necessary data to draw a series for a line chart.
        /// </summary>
        internal void InitSeries()
        {
            var cri = (ChartRendererInfo)_rendererParms.RendererInfo;

            var seriesIndex = 0;
            foreach (var sri in cri.seriesRendererInfos)
            {
                if (sri._series._markerBackgroundColor.IsEmpty)
                {
                    sri.LineFormat = Converter.ToXPen(sri._series._lineFormat, LineColors.Item(seriesIndex), ChartRenderer.DefaultSeriesLineWidth);
                }
                else
                {
                    sri.LineFormat = Converter.ToXPen(sri._series._lineFormat, sri._series._markerBackgroundColor, ChartRenderer.DefaultSeriesLineWidth);
                }

                sri.LineFormat.LineJoin = XLineJoin.Bevel;

                var mri = new MarkerRendererInfo();
                sri._markerRendererInfo = mri;

                mri.MarkerForegroundColor = sri._series._markerForegroundColor;
                if (mri.MarkerForegroundColor.IsEmpty)
                {
                    mri.MarkerForegroundColor = XColors.Black;
                }

                mri.MarkerBackgroundColor = sri._series._markerBackgroundColor;
                if (mri.MarkerBackgroundColor.IsEmpty)
                {
                    mri.MarkerBackgroundColor = sri.LineFormat.Color;
                }

                mri.MarkerSize = sri._series._markerSize;
                if (mri.MarkerSize == 0)
                {
                    mri.MarkerSize = 7;
                }

                if (!sri._series._markerStyleInitialized)

                    //mri.MarkerStyle = (MarkerStyle)(seriesIndex % (Enum.GetNames(typeof(MarkerStyle)).Length - 1) + 1);
                {
                    mri.MarkerStyle = (MarkerStyle)(seriesIndex % (10 - 1) + 1);
                }
                else
                {
                    mri.MarkerStyle = sri._series._markerStyle;
                }

                ++seriesIndex;
            }
        }
    }
}
