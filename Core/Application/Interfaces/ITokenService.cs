using Application.Dtos;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user,IList<string> Roles );
        bool ValidateCurrentToken(string token);
    }
}
