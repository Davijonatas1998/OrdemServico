using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OrdemServico.Models.ServicoOrdem
{
    public enum Status
    {
        [Display(Name = "Aberta")]
        Aberta,

        [Display(Name = "Em Andamento")]
        EmAndamento,

        [Display(Name = "Concluída")]
        Concluida,

        [Display(Name = "Cancelada")]
        Cancelada
    }
}