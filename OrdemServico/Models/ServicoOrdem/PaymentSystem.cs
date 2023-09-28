using OrdemServico.Models.Usuario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdemServico.Models.ServicoOrdem
{
    public class PaymentSystem
    {
        [Key]
        public int IdPayment { get; set; }

        public string Metadata { get; set; }

        public string Id { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }
    }
}
