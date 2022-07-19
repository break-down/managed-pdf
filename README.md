# BreakDown.ManagedPdf

Open Source .NET Library to create & process PDF from HTML on the fly with support for HTML 4.01 and CSS 2

Features:
---------

* 100% managed code depends only on ManagedPdf (PDFSharp Fork) library
* Extensive HTML 4.01 and CSS level 2 specifications support
* Support separating CSS from HTML by loading stylesheet code separately
* Handles "real world" malformed HTML, it doesn't have to be XHTML
* High performance and low memory footprint
* Extendable and configurable
* Runs in Highly Concurrent Environment

Sample
-------

```csharp
    using System.IO;
    using BreakDown.ManagedPdf.Core.root.enums;
    using BreakDown.ManagedPdf.HtmlRenderer;
```

```csharp
    var html = "--html-template--";
    var pdf = PdfGenerator.GeneratePdf(html, new PdfGenerateConfig
    {
        PageSize = PageSize.Letter,
        PageOrientation = PageOrientation.Portrait,
        MarginTop = 25,
        MarginBottom = 25,
        MarginLeft = 25,
        MarginRight = 25
    });

    using var memoryStream = new MemoryStream();
    pdf.Save(memoryStream);
    return memoryStream.GetBuffer();
```

Forked From:
-----------

* [PDFSharp](https://github.com/LionelVallet/PDFsharp): https://github.com/LionelVallet/PDFsharp
* [HTML-Renderer](https://github.com/LionelVallet/HTML-Renderer): https://github.com/LionelVallet/HTML-Renderer