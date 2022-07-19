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

using BreakDown.ManagedPdf.Core.Drawing;
using BreakDown.ManagedPdf.Core.root.enums;

namespace BreakDown.ManagedPdf.HtmlRenderer
{
    /// <summary>
    /// The settings for generating PDF using <see cref="PdfGenerator"/>
    /// </summary>
    public sealed class PdfGenerateConfig
    {
        /// <summary>
        /// the top margin between the page start and the text
        /// </summary>
        private int marginTop;

        /// <summary>
        /// the bottom margin between the page end and the text
        /// </summary>
        private int marginBottom;

        /// <summary>
        /// the left margin between the page start and the text
        /// </summary>
        private int marginLeft;

        /// <summary>
        /// the right margin between the page end and the text
        /// </summary>
        private int marginRight;

        /// <summary>
        /// the page size to use for each page in the generated pdf
        /// </summary>
        public PageSize PageSize { get; set; }

        /// <summary>
        /// if the page size is undefined this allow you to set manually the page size
        /// </summary>
        public XSize ManualPageSize { get; set; }

        /// <summary>
        /// the orientation of each page of the generated pdf
        /// </summary>
        public PageOrientation PageOrientation { get; set; }

        /// <summary>
        /// the top margin between the page start and the text
        /// </summary>
        public int MarginTop
        {
            get => marginTop;
            set
            {
                if (value > -1)
                {
                    marginTop = value;
                }
            }
        }

        /// <summary>
        /// the bottom margin between the page end and the text
        /// </summary>
        public int MarginBottom
        {
            get => marginBottom;
            set
            {
                if (value > -1)
                {
                    marginBottom = value;
                }
            }
        }

        /// <summary>
        /// the left margin between the page start and the text
        /// </summary>
        public int MarginLeft
        {
            get => marginLeft;
            set
            {
                if (value > -1)
                {
                    marginLeft = value;
                }
            }
        }

        /// <summary>
        /// the right margin between the page end and the text
        /// </summary>
        public int MarginRight
        {
            get => marginRight;
            set
            {
                if (value > -1)
                {
                    marginRight = value;
                }
            }
        }

        /// <summary>
        /// Set all 4 margins to the given value.
        /// </summary>
        /// <param name="value"></param>
        public void SetMargins(int value)
        {
            if (value > -1)
            {
                marginBottom = marginLeft = marginTop = marginRight = value;
            }
        }

        // The international definitions are:
        //   1 inch == 25.4 mm
        //   1 inch == 72 point

        /// <summary>
        /// Convert the units passed in milimiters to the units used in BreakDown.ManagedPdf.Core
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static XSize MillimetersToUnits(double width, double height)
        {
            return new XSize(width / 25.4 * 72, height / 25.4 * 72);
        }

        /// <summary>
        /// Convert the units passed in inches to the units used in BreakDown.ManagedPdf.Core
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static XSize InchesToUnits(double width, double height)
        {
            return new XSize(width * 72, height * 72);
        }
    }
}
