using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class ImageProcessor : IImageProcessor
    {
        private readonly IConfiguration _config;

        public ImageProcessor(IConfiguration config)
        {
            _config = config;
        }
        public async Task<bool> SaveImage(string filePath,IFormFile file)
        {
            bool output = false;
            new FileInfo(filePath).Directory?.Create();
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {   
                await file.CopyToAsync(stream);
                output = true;
            }
            return output;
        }
        public bool DeleteImage(string filePath)
        {
            bool output = false;
            FileInfo fileInfo = new(filePath);
            fileInfo.Delete();
            output = true;
            return output;
        }
    }
}
