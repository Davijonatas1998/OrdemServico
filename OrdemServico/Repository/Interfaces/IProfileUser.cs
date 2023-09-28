using OrdemServico.Models.Usuario;
using System.Security.Claims;

namespace OrdemServico.Repository.Interface
{
    public interface IProfileUser
    {
        public Task<ApplicationUser> GetProfileAsync(ClaimsPrincipal userClaimsPrincipal);

        public Task<List<ApplicationUser>> ListProfilesAsync();

        public Task<IList<string>> GetRole(ApplicationUser user);
    }
}
