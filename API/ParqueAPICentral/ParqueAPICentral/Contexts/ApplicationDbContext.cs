using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<ParqueAPICentral.Models.SubAluguer> SubAluguer { get; set; }

        //public DbSet<ParqueAPICentral.Models.Cliente> Cliente { get; set; }

        public DbSet<ParqueAPICentral.Models.Reserva> Reserva { get; set; }

        public DbSet<ParqueAPICentral.Models.Pagamento> Pagamento { get; set; }

        public DbSet<ParqueAPICentral.Models.Fatura> Fatura { get; set; }

        public DbSet<ParqueAPICentral.Models.Parque> Parque { get; set; }

        public DbSet<ParqueAPICentral.Models.Morada> Morada { get; set; }
    }
}
