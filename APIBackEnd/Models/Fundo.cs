using System;
using System.ComponentModel.DataAnnotations;

namespace APIBackEnd.Models
{
    public class Fundo
    {
        public Guid Id { get; set; }
        [Required]
        public string NomeDoFundo { get; set; }
        [Required]
        public string CnpjDoFundo { get; set; }
        [Required]
        public decimal InvestimentoInicialMinimo { get; set; }
    }
}
