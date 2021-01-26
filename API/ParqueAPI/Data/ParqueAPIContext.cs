using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParqueAPI.Models;
using ParqueAPI.Data;

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

        public DbSet<ParqueAPI.Models.Fatura> Fatura { get; set; }

        public DbSet<ParqueAPI.Models.Reserva> Reserva { get; set; }

        public DbSet<ParqueAPI.Models.Lugar> Lugar { get; set; }

        public DbSet<ParqueAPI.Models.SubAluguer> SubAluguer { get; set; }
    }
}
