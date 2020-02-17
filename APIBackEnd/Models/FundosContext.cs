using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace APIBackEnd.Models
{
    public class FundosContext : DbContext
    {
        public FundosContext (DbContextOptions<FundosContext> options) : base(options) {
          
        }

        public DbSet<Fundo> Fundos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Fundo>().ToTable("Fundos");
            modelBuilder.Entity<Fundo>().HasKey(p => p.Id);
            modelBuilder.Entity<Fundo>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        }
    }
}
