﻿using OrdemServico.Models.Usuario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdemServico.Models.ServicoOrdem
{
    public class ClassOrdemServico
    {
        [Key]
        public int IdServico { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MinLength(10, ErrorMessage = "O tamanho da descrição deve ser de 10 ou mais caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MinLength(15, ErrorMessage = "O tamanho da descrição deve ser de 15 ou mais caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public TipoServico Tipo { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public DateTime DataAbertura { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public DateTime DataConclusao { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public Status Status { get; set; }

        public string NumeroSerie { get; set; }

        public string Preco { get; set; }

        public string Desconto { get; set; }

        public bool Pagamento { get; set; }

        public string Id { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }
    }
}