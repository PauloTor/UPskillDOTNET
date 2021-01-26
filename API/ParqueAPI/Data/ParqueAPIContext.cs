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

         

        public DbSet<Morada> Morada { get; set; }

        public DbSet<Parque> Parque { get; set; }

        
        public DbSet<Reserva> Reserva { get; set; }

        public DbSet<Lugar> Lugar { get; set; }

        
    }
}
