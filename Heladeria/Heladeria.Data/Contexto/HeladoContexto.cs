using Heladeria.Data.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Heladeria.Data.Contexto
{
    public class HeladoContexto : DbContext
    {
        public HeladoContexto(DbContextOptions<HeladoContexto> options) : base(options) { }

        public DbSet<Helado> Helados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Helado>(e =>
            {
                e.ToTable("helado");
                e.HasKey(h => h.Id);
                e.Property(h => h.Sabor).HasMaxLength(50).IsRequired().HasColumnName("sabor");
                e.Property(h => h.Tamanio).HasMaxLength(50).IsRequired().HasColumnName("tamanio");
                e.Property(h => h.Precio).HasPrecision(10, 2).IsRequired().HasColumnName("precio");
                e.HasQueryFilter(h => !h.Borrado);
                
            });
        }
    }
}
