using System;
using System.ComponentModel.DataAnnotations;

namespace APIBackEnd.Models
{
    public class Movimentacao
    {
        public Guid Id { get; set; }
        public TipoMovimentacao TipoDaMovimentacao { get; set; }
        [Required]
        public string CpfDoCliente { get; set; }
        [Required]
        public decimal ValorDaMovimentacao { get; set; }
        [Required]
        public DateTime DataDaMovimentacao { get; set; }

        [Required]
        public Guid IdDoFundo { get; set; }
        //public Fundo Fundo { get; set; }
    }
}
