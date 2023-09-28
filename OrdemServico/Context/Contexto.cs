using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrdemServico.Models.ServicoOrdem;
using OrdemServico.Models.Usuario;

namespace OrdemServico.Context
{
    public class Contexto : IdentityDbContext<ApplicationUser>
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<ClassOrdemServico> OrdemServico { get; set; }

        public DbSet<PaymentSystem> PaymentSystem { get; set; }
    }
}