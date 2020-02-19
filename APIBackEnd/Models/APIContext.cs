using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace APIBackEnd.Models
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }

        public DbSet<Fundo> Fundos { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder build)
        {
            base.OnModelCreating(build);

            build.Entity<Fundo>().ToTable("Fundos");
            build.Entity<Fundo>().HasKey(p => p.Id);
            build.Entity<Fundo>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            //var jsonString = File.ReadAllText("Models/DataFundos.json");
            //var fund = JsonSerializer.Deserialize<List<Fundo>>(jsonString);

            //build.Entity<Fundo>().HasData(
            //    fund
            //    );

            build.Entity<Movimentacao>().ToTable("Movimentacoes");
            build.Entity<Movimentacao>().HasKey(p => p.Id);
            build.Entity<Movimentacao>().Property(p => p.DataDaMovimentacao).ValueGeneratedOnAdd();
        }
    }
}
