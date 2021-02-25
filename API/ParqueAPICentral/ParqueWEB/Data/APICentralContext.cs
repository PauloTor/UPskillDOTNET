using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParqueAPICentral.Models;
using Microsoft.AspNetCore.Mvc;
using ParqueAPICentral.DTO;

namespace ParqueAPICentral.Data
{
    public class APICentralContext : DbContext
    {
        public APICentralContext(DbContextOptions<APICentralContext> options)
            : base(options)
        {
        }

        public DbSet<ParqueAPICentral.Entities.User> Users { get; set; }

        public DbSet<ParqueAPICentral.Models.SubAluguer> SubAluguer { get; set; }

        public DbSet<ParqueAPICentral.Models.Cliente> Cliente { get; set; }   

        public DbSet<ParqueAPICentral.Models.Reserva> Reserva { get; set; }

        public DbSet<ParqueAPICentral.Models.Pagamento> Pagamento { get; set; }

        public DbSet<ParqueAPICentral.Models.Fatura> Fatura { get; set; }

        public DbSet<ParqueAPICentral.Models.Parque> Parque { get; set; }

        public DbSet<ParqueAPICentral.Models.Morada> Morada { get; set; }

    }
}
