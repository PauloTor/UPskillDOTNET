using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParqueWEB.Models;
using ParqueWEB;

namespace ParqueWEB.Data
{
    public class ParqueWEBContext : DbContext
    {
        public ParqueWEBContext (DbContextOptions<ParqueWEBContext> options)
            : base(options)
        {
        }

        public DbSet<ParqueWEB.Models.Lugar> Lugar { get; set; }

        public DbSet<ParqueWEB.Models.Morada> Morada { get; set; }

        public DbSet<ParqueWEB.Parque> Parque { get; set; }

        public DbSet<ParqueWEB.Models.Reserva> Reserva { get; set; }
    }
}
