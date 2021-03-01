using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using PseudoCompanyFront.Models;

namespace PseudoCompanyFront.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PseudoCompanyFront.Models.Reserva> Reserva { get; set; }
        public DbSet<PseudoCompanyFront.Models.Cliente> Clientes { get; set; }
        public DbSet<PseudoCompanyFront.Models.ParqueDTO> ParquesDTO { get; set; }
        public DbSet<PseudoCompanyFront.Models.MoradaDTO> MoradasDTO { get; set; }

        public DbSet<PseudoCompanyFront.Models.RuaDTO> RuasDTO { get; set; }
    }
}
