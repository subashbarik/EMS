using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Statics;

namespace Application.Helpers
{
    public static class AccountHelper
    {
        public static bool IsAdmin(UserDto user)
        {
            bool output = false;
            foreach(var role in user.Roles)
            {
                if (role == Role.Admin)
                {
                    output = true;
                    break;
                }
            }
            return output;
        }
    }
}
