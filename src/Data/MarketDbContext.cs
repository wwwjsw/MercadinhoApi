using Microsoft.EntityFrameworkCore;
using MercadinhoApi.Models;

namespace MercadinhoApi.Data
{
    public class MarketDbContext : DbContext
    {
        public MarketDbContext(DbContextOptions<MarketDbContext> options) : base(options)
        {
        }

        public DbSet<ItemMercado> ItensMercado { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemMercado>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Categoria).HasMaxLength(50);
                entity.Property(e => e.Preco).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Quantidade).IsRequired();
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}