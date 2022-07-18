namespace BreakDown.ManagedPdf.Charting.Charting.Renderers
{
    /// <summary>
    /// A CombinationRendererInfo stores information for rendering combination of charts.
    /// </summary>
    internal class CombinationRendererInfo : ChartRendererInfo
    {
        internal SeriesRendererInfo[] _commonSeriesRendererInfos;
        internal SeriesRendererInfo[] _areaSeriesRendererInfos;
        internal SeriesRendererInfo[] _columnSeriesRendererInfos;
        internal SeriesRendererInfo[] _lineSeriesRendererInfos;
    }
}
