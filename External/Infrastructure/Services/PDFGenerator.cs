using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using IronPdf;

namespace Infrastructure.Services
{
    public class PDFGenerator : IPDFGenerator
    {
        public async Task<bool> CreatePDFFromText(string text,string fileName)
        {
            var renderer = new HtmlToPdf();
            //renderer.RenderHtmlAsPdf("<h1>This is test file</h1>").SaveAs()
            var pdfDocument = await renderer.RenderHtmlAsPdfAsync(text);
            pdfDocument.SaveAs(fileName);
            return true;
        }
    }
}