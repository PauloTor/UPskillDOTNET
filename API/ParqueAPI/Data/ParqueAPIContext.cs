using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParqueAPI.Models;

namespace ParqueAPI.Data
{
    public class ParqueAPIContext : DbContext
    {
        public ParqueAPIContext (DbContextOptions<ParqueAPIContext> options)
            : base(options)
        {
        }

        public DbSet<ParqueAPI.Models.Morada> Morada { get; set; }

        public DbSet<ParqueAPI.Models.Parque> Parque { get; set; }

        public DbSet<ParqueAPI.Models.Cliente> Cliente { get; set; }
    }
}
