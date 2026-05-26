using javier_api.Models;
using Microsoft.EntityFrameworkCore;
namespace javier_api.Data
{
    public class LibreriaContext : DbContext
    {
        public LibreriaContext(DbContextOptions<LibreriaContext> options)
            : base(options)
        {
        }
        public DbSet<Libro> Libros => Set<Libro>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Pedido> Pedidos => Set<Pedido>();
        public DbSet<PedidoItem> PedidoItems => Set<PedidoItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Libro>()
                .HasIndex(l => l.Isbn)
                .IsUnique();
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Username)
                .IsUnique();
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId);

            modelBuilder.Entity<PedidoItem>()
                .HasOne(pi => pi.Pedido)
                .WithMany(p => p.Items)
                .HasForeignKey(pi => pi.PedidoId);

            modelBuilder.Entity<PedidoItem>()
                .HasOne(pi => pi.Libro)
                .WithMany()
                .HasForeignKey(pi => pi.LibroId);
        }
    }
}