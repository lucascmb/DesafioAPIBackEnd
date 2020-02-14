using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIBackEnd.Models
{
    public class Fundos
    {
        public int id { get; set; }
        public string nomeDoFundo { get; set; }
        public string cnpjDoFundo { get; set; }
        public string investimentoInicialMinimo { get; set; }
    }
}
