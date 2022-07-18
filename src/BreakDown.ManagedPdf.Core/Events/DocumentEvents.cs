#region PDFsharp - A .NET library for processing PDF

//
// Authors:
//   Stefan Lange
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

namespace BreakDown.ManagedPdf.Core.Events;

/// <summary>
/// A class encapsulating all events of a PdfDocument.
/// </summary>
public class DocumentEvents
{
    /// <summary>
    /// An event raised if a page was added.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="args">The PageEventArgs of the event.</param>
    public void OnPageAdded(object sender, PageEventArgs args)
    {
        if (PageAdded != null)
        {
            PageAdded(sender, args);
        }
    }

    /// <summary>
    /// EventHandler for OnPageAdded.
    /// </summary>
    public event PageAddedOrRemovedEventHandler PageAdded;

    /// <summary>
    /// An event raised if a page was removes.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="args">The PageEventArgs of the event.</param>
    public void OnPageRemoved(object sender, PageEventArgs args)
    {
        if (PageRemoved != null)
        {
            PageRemoved(sender, args);
        }
    }

    /// <summary>
    /// EventHandler for OnPageRemoved.
    /// </summary>
    public event PageAddedOrRemovedEventHandler PageRemoved;

    /// <summary>
    /// An event raised if the XGraphics object of a page is created.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="args">The PageGraphicsEventArgs of the event.</param>
    public void OnPageGraphicsCreated(object sender, PageGraphicsEventArgs args)
    {
        if (PageGraphicsCreated != null)
        {
            PageGraphicsCreated(sender, args);
        }
    }

    /// <summary>
    /// EventHandler for OnPageGraphicsCreated.
    /// </summary>
    public event PageGraphicsEventHandler PageGraphicsCreated;

    /// <summary>
    /// An event raised if something is drawn on a page's XGraphics object.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="args">The PageGraphicsEventArgs of the event.</param>
    public void OnPageGraphicsAction(object sender, PageGraphicsEventArgs args)
    {
        if (PageGraphicsAction != null)
        {
            PageGraphicsAction(sender, args);
        }
    }

    /// <summary>
    /// EventHandler for OnPageGraphicsAction.
    /// </summary>
    public event PageGraphicsEventHandler PageGraphicsAction;
}
