using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IImageProcessor
    {
        Task<bool> SaveImage(string filePath,IFormFile file);
        bool DeleteImage(string filePath);
    }
}
