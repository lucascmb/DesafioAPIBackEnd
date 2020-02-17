using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace APIBackEnd.Models
{
    public class MovimentacoesContext : DbContext
    {
        public MovimentacoesContext (DbContextOptions<MovimentacoesContext> options) : base(options) { }

        public DbSet<Movimentacao> Movimentacoes { get; set; }
    }
}
