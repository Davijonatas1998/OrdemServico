using Microsoft.AspNetCore.Identity;

namespace OrdemServico.Models.Usuario
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
    }
}