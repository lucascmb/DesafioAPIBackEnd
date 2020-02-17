using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace APIBackEnd.Models
{
    public class Movimentacao
    {
        public Guid Id { get; set; }
        public TipoMovimentacao TipoDaMovimentacao { get; set; }
        public string CpfDoCliente { get; set; }
        public decimal ValorDaMovimentacao { get; set; }
        public DateTime DataDaMovimentacao { get; set; }

        public Guid IdDoFundo { get; set; }
        //public Fundo Fundo { get; set; }
    }
}
