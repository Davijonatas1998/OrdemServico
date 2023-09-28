using System.ComponentModel.DataAnnotations;

namespace OrdemServico.Models.ServicoOrdem
{
    public enum TipoServico
    {
        [Display(Name = "Reparo")]
        Reparo,

        [Display(Name = "Manutenção")]
        Manutencao,

        [Display(Name = "Vistoria")]
        Vistoria,

        [Display(Name = "Eletrico")]
        Eletrico,

        [Display(Name = "Software")]
        Software
    }
}