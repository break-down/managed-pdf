using System;
using BreakDown.ManagedPdf.Core.Drawing;

namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// SeriesRendererInfo holds all data series specific rendering information.
    /// </summary>
    internal class SeriesRendererInfo : RendererInfo
    {
        internal Series _series;

        internal DataLabelRendererInfo _dataLabelRendererInfo;
        internal PointRendererInfo[] _pointRendererInfos;

        internal XPen LineFormat;
        internal XBrush FillFormat;

        // Used if ChartType is set to Line
        internal MarkerRendererInfo _markerRendererInfo;

        /// <summary>
        /// Gets the sum of all points in PointRendererInfo.
        /// </summary>
        internal double SumOfPoints
        {
            get
            {
                double sum = 0;
                foreach (var pri in _pointRendererInfos)
                {
                    if (!double.IsNaN(pri.Point._value))
                    {
                        sum += Math.Abs(pri.Point._value);
                    }
                }

                return sum;
            }
        }
    }
}
