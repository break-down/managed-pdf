using System;
using System.Diagnostics;
using BreakDown.ManagedPdf.Core.Fonts.OpenType.enums;
using BreakDown.ManagedPdf.Core.root;

namespace BreakDown.ManagedPdf.Core.Fonts.OpenType
{
    /// <summary>
    /// The type longHorMetric is defined as an array where each element has two parts:
    /// the advance width, which is of type USHORT, and the left side bearing, which is of type SHORT.
    /// These fields are in font design units.
    /// </summary>
    internal class HorizontalMetricsTable : OpenTypeFontTable
    {
        public const string Tag = TableTagNames.HMtx;

        public HorizontalMetrics[] Metrics;
        public Int16[] LeftSideBearing;

        public HorizontalMetricsTable(OpenTypeFontface fontData)
            : base(fontData, Tag)
        {
            Read();
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

                    Metrics = new HorizontalMetrics[numMetrics];
                    for (var idx = 0; idx < numMetrics; idx++)
                    {
                        Metrics[idx] = new HorizontalMetrics(_fontData);
                    }

                    if (numLsbs > 0)
                    {
                        LeftSideBearing = new Int16[numLsbs];
                        for (var idx = 0; idx < numLsbs; idx++)
                        {
                            LeftSideBearing[idx] = _fontData.ReadFWord();
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
