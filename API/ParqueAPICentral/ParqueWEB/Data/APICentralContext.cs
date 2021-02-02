﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParqueAPICentral.Models;

namespace ParqueAPICentral.Data
{
    public class APICentralContext : DbContext
    {
        public APICentralContext (DbContextOptions<APICentralContext> options)
            : base(options)
        {
        }

        public DbSet<ParqueAPICentral.Models.SubAluguer> SubAluguer { get; set; }

        public DbSet<ParqueAPICentral.Models.Cliente> Cliente { get; set; }

        public DbSet<ParqueAPICentral.Models.Morada> Morada { get; set; }

        public DbSet<ParqueAPICentral.Models.Parque> Parque { get; set; }

        public DbSet<ParqueAPICentral.Models.Reserva> Reserva { get; set; }

        public DbSet<ParqueAPICentral.Models.Rua> Rua { get; set; }
        public DbSet<ParqueAPICentral.Models.Pagamento> Pagamento { get; set; }

        public DbSet<ParqueAPICentral.Models.Lugar> Lugar { get; set; }

        public DbSet<ParqueAPICentral.Models.Fatura> Fatura { get; set; }
    }
}
