using Application.Interfaces;
using Application.Helpers;
using IronPdf;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Services
{
    public class PDFGenerator : IPDFGenerator
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PDFGenerator(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<bool> CreatePDFFromText(string text,string fileName)
        {
            string path;
            fileName = fileName.ToLower();
            string dir = _hostingEnvironment.WebRootPath + "\\PDF";
            bool exists = FileHelper.DirExists(dir);
            if (exists is false)
            {
                FileHelper.CreateDir(dir);
            }
            bool containsExtension = FileHelper.ContainsFileExtension(fileName.ToLower(), ".pdf");
            if(containsExtension is false) 
            {
                path = Path.Combine(dir, fileName+".pdf");
            }
            else
            {
                path = Path.Combine(dir, fileName);
            }
            var renderer = new HtmlToPdf();
            //renderer.RenderHtmlAsPdf("<h1>This is test file</h1>").SaveAs()
            var pdfDocument = await renderer.RenderHtmlAsPdfAsync(text);
            pdfDocument.SaveAs(path);
            return true;
        }
        
    }
}