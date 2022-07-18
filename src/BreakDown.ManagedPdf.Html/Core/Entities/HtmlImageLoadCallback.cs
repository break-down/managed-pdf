using System;
using BreakDown.ManagedPdf.Html.Adapters.Entities;

namespace BreakDown.ManagedPdf.Html.Core.Entities
{
    /// <summary>
    /// Callback used in <see cref="HtmlImageLoadEventArgs"/> to allow setting image externally and async.<br/>
    /// The callback can provide path to image file path, URL or the actual image to use.<br/>
    /// If <paramref name="imageRectangle"/> is given (not <see cref="RRect.Empty"/>) then only the specified rectangle will
    /// be used from the loaded image and not all of it, also the rectangle will be used for size and not the actual image size.<br/> 
    /// </summary>
    /// <param name="path">the path to the image to load (file path or URL)</param>
    /// <param name="image">the image to use</param>
    /// <param name="imageRectangle">optional: limit to specific rectangle in the loaded image</param>
    public delegate void HtmlImageLoadCallback(string path, Object image, RRect imageRectangle);
}
