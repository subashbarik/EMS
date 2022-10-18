using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class EMSIdentityContext:IdentityDbContext
    {
        public EMSIdentityContext(DbContextOptions<EMSIdentityContext> options):base(options)
        {

        }
    }
}
