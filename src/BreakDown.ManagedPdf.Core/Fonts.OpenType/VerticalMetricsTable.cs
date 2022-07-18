using System;
using System.Diagnostics;
using BreakDown.ManagedPdf.Core.Fonts.OpenType.enums;
using BreakDown.ManagedPdf.Core.root;

namespace BreakDown.ManagedPdf.Core.Fonts.OpenType
{
    /// <summary>
    /// The vertical Metrics table allows you to specify the vertical spacing for each glyph in a
    /// vertical font. This table consists of either one or two arrays that contain metric
    /// information (the advance heights and top sidebearings) for the vertical layout of each
    /// of the glyphs in the font.
    /// </summary>
    internal class VerticalMetricsTable : OpenTypeFontTable
    {
        // UNDONE
        public const string Tag = TableTagNames.VMtx;

        // code comes from HorizontalMetricsTable
        public HorizontalMetrics[] metrics;
        public Int16[] leftSideBearing;

        public VerticalMetricsTable(OpenTypeFontface fontData)
            : base(fontData, Tag)
        {
            Read();
            throw new NotImplementedException("VerticalMetricsTable");
        }

        public void Read()
        {
            try
            {
                var hhea = _fontData.hhea;
                var maxp = _fontData.maxp;
                if (hhea != null && maxp != null)
                {
                    int numMetrics = hhea.numberOfHMetrics; //->NumberOfHMetrics();
                    var numLsbs = maxp.numGlyphs - numMetrics;

                    Debug.Assert(numMetrics != 0);
                    Debug.Assert(numLsbs >= 0);

                    metrics = new HorizontalMetrics[numMetrics];
                    for (var idx = 0; idx < numMetrics; idx++)
                    {
                        metrics[idx] = new HorizontalMetrics(_fontData);
                    }

                    if (numLsbs > 0)
                    {
                        leftSideBearing = new Int16[numLsbs];
                        for (var idx = 0; idx < numLsbs; idx++)
                        {
                            leftSideBearing[idx] = _fontData.ReadFWord();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(PSSR.ErrorReadingFontData, ex);
            }
        }
    }
}
