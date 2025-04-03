using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KTM_ASP.Models;

namespace KTM_ASP.Data
{
    public class KTM_ASPContext : DbContext
    {
        public KTM_ASPContext (DbContextOptions<KTM_ASPContext> options)
            : base(options)
        {
        }

        public DbSet<KTM_ASP.Models.Interiors> Interiors { get; set; } = default!;
    }
}
