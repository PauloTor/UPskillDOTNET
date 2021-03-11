using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ParqueAPICentral.Authentication
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
   //  public DbSet<ParqueAPICentral.Entities.User> Users { get; set; }

        public DbSet<ParqueAPICentral.Models.SubAluguer> SubAluguer { get; set; }

        public DbSet<ParqueAPICentral.Models.Cliente> Cliente { get; set; }

        public DbSet<ParqueAPICentral.Models.Reserva> Reserva { get; set; }

        public DbSet<ParqueAPICentral.Models.Pagamento> Pagamento { get; set; }

        public DbSet<ParqueAPICentral.Models.Fatura> Fatura { get; set; }

        public DbSet<ParqueAPICentral.Models.Parque> Parque { get; set; }

        public DbSet<ParqueAPICentral.Models.Morada> Morada { get; set; }

    }

}