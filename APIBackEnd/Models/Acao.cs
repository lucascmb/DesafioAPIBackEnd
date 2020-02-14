using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIBackEnd.Models
{
    public class Acao
    {
        public int id { get; set; }
        public enum tipoDaMovimentacao { Aplicação = 0, Resgate = 1 }
        public int idDoFundo { get; set; }
        public string cpfDoCliente { get; set; }
        public decimal valorDaMovimentacao { get; set; }
        public DateTime dataDaMovimentacao { get; set; }
    }
}
