using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPDFGenerator
    {
        Task<bool> CreatePDFFromText(string text,string fileName);
    }
}