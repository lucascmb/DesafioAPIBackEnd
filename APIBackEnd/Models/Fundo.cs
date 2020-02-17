using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIBackEnd.Models
{
    public class Fundo
    {
        public Guid Id { get; set; }
        public string NomeDoFundo { get; set; }
        public string CnpjDoFundo { get; set; }
        public decimal InvestimentoInicialMinimo { get; set; }
    }
}
