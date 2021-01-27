using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ParquePrivateAPI.Data
{
    public class ParquePrivateAPIContext : DbContext
    {
        public ParquePrivateAPIContext (DbContextOptions<ParquePrivateAPIContext> options)
            : base(options)
        {
        }

        public DbSet<ParquePrivateAPI.Models.Morada> Morada { get; set; }

        public DbSet<ParquePrivateAPI.Models.Lugar> Lugar { get; set; }

        public DbSet<ParquePrivateAPI.Models.Reserva> Reserva { get; set; }

        public DbSet<ParquePrivateAPI.Models.Parque> Parque { get; set; }
    }
}
