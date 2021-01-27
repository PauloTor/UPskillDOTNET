using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParqueAPIPrivado.Models;

namespace ParqueAPIPrivado.Data
{
    public class ParqueAPIPrivadoContext : DbContext
    {
        public ParqueAPIPrivadoContext (DbContextOptions<ParqueAPIPrivadoContext> options)
            : base(options)
        {
        }

        public DbSet<ParqueAPIPrivado.Models.Lugar> Lugar { get; set; }

        public DbSet<ParqueAPIPrivado.Models.Morada> Morada { get; set; }

        public DbSet<ParqueAPIPrivado.Models.Parque> Parque { get; set; }

        public DbSet<ParqueAPIPrivado.Models.Reserva> Reserva { get; set; }
    }
}
