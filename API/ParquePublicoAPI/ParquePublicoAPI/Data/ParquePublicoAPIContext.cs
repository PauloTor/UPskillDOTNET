using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParquePublicoAPI.Models;

namespace ParquePublicoAPI.Data
{
    public class ParquePublicoAPIContext : DbContext
    {
        public ParquePublicoAPIContext (DbContextOptions<ParquePublicoAPIContext> options)
            : base(options)
        {
        }

        public DbSet<ParquePublicoAPI.Models.Rua> Rua { get; set; }
    }
}
