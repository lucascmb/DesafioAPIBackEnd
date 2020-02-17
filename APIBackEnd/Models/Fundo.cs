using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIBackEnd.Models
{
    public class Fundo
    {
        public int Id { get; set; }
        public string NomeDoFundo { get; set; }
        public string CnpjDoFundo { get; set; }
        public string InvestimentoInicialMinimo { get; set; }
    }
}
