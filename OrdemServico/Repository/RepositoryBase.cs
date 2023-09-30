using MercadoPago.Resource.Payment;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrdemServico.Context;
using OrdemServico.Models.ServicoOrdem;
using OrdemServico.Models.Usuario;
using OrdemServico.Repository.Interface;
using OrdemServico.Repository.Interfaces;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace OrdemServico.Repository
{
    public class RepositoryBase : IManagerService, IProfileUser, IPayment
    {
        private readonly Contexto _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RepositoryBase(Contexto contexto, UserManager<ApplicationUser> userManager)
        {
            _context = contexto;
            _userManager = userManager;
        }

        public async Task<IList<string>> GetRole(ApplicationUser user)
        {
            var Profile = await _userManager.GetRolesAsync(user);
            return Profile;
        }

        public async Task<ApplicationUser> GetProfileAsync(ClaimsPrincipal userClaimsPrincipal)
        {
            return await _userManager.GetUserAsync(userClaimsPrincipal);
        }

        public async Task<List<ApplicationUser>> ListProfilesAsync()
        {
            var Users = await _userManager.Users.ToListAsync();
            var Profiles = new List<ApplicationUser>();

            foreach (var User in Users)
            {
                var roles = await _userManager.GetRolesAsync(User);
                if (!roles.Contains("Admin"))
                {
                    Profiles.Add(User);
                }
            }

            return Profiles;
        }

        public async Task<ClassOrdemServico> CreateServico(ClassOrdemServico Servico)
        {
            _context.Add(Servico);
            await _context.SaveChangesAsync();
            return Servico;
        }

        public async Task<ClassOrdemServico> DeleteServico(int? id)
        {
            var Servico = await _context.OrdemServico.FindAsync(id);
            if (Servico != null || Servico.Pagamento)
            {
                _context.OrdemServico.Remove(Servico);
                await _context.SaveChangesAsync();
            }
            return Servico;
        }

        public async Task<ClassOrdemServico> DetailsServico(int? id)
        {
            return await _context.OrdemServico.FirstOrDefaultAsync(c => c.IdServico == id);
        }

        public async Task<ClassOrdemServico> GetServico(int? id)
        {
            var Servico = await _context.OrdemServico.FindAsync(id);
            return Servico;
        }

        public async Task<List<ClassOrdemServico>> ListServico()
        {
            return await _context.OrdemServico.ToListAsync();
        }

        public IQueryable<ClassOrdemServico> QuerybleServico()
        {
           var ListServico = _context.OrdemServico.AsQueryable(); 
           return ListServico;
        }

        public async Task<ClassOrdemServico> UpdateServico(ClassOrdemServico Servico)
        {
            if(Servico.Preco != null || Servico.Pagamento)
            {
                _context.Update(Servico);
                await _context.SaveChangesAsync();
            }
            return Servico;
        }

        public async Task<PaymentSystem> GetPayment(string Id, string Metadata)
        {
            return await _context.PaymentSystem.Where(c => c.Id == Id).FirstOrDefaultAsync(c => c.Metadata == Metadata);
        }

        public async Task<PaymentSystem> CreatePayment(string Metadata, string id)
        {
            var Payment = new PaymentSystem
            {
                Metadata = Metadata,
                Id = id
            };

            _context.Add(Payment);
            await _context.SaveChangesAsync();
            return Payment;
        }

        public async Task<PaymentSystem> UpdatePayment(PaymentSystem payment, string Metadata, string id)
        {
            payment.Metadata = Metadata;
            payment.Id = id;

            if (payment != null)
            {
                _context.Update(payment);
                await _context.SaveChangesAsync();
            }
            return payment;
        }

        public async Task<PaymentSystem> DeletePayment(string Id, string Metadata)
        {
            var Payment = await _context.PaymentSystem.Where(c => c.Id == Id).FirstOrDefaultAsync(c => c.Metadata == Metadata);
            if (Payment != null)
            {
                _context.PaymentSystem.Remove(Payment);
                await _context.SaveChangesAsync();
            }
            return Payment;
        }
    }
}