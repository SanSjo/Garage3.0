using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GarageMVC3.Models;

namespace GarageMVC3.Data
{
    public class GarageMVC3Context : DbContext
    {
        public GarageMVC3Context (DbContextOptions<GarageMVC3Context> options)
            : base(options)
        {
        }

        public DbSet<GarageMVC3.Models.Vehicle> Vehicle { get; set; }
    }
}
