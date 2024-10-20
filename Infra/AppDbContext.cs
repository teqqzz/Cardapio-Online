using Microsoft.EntityFrameworkCore;
using CardapioOnline.Models;

namespace CardapioOnline.Infra
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            var caminho = AppContext.BaseDirectory;
            CaminhoDb = Path.Combine(caminho, "cardapioonline.db");
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Item> Itens { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<DetalhePedido> DetalhesPedidos { get; set; }
        public virtual string CaminhoDb { get; }

        // M�todo para configurar o banco de dados SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={CaminhoDb}");

        // Configura��o das entidades e relacionamentos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura��o da entidade Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.CriadoEm).HasColumnType("DATETIME").IsRequired();
                entity.Property(e => e.AtualizadoEm).HasColumnType("DATETIME");

                // Relacionamento 1:N com Itens
                entity.HasMany(c => c.Itens)
                      .WithOne(i => i.Categoria)
                      .HasForeignKey(i => i.CategoriaId);
            });

            // Configura��o da entidade Item
            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.Preco).HasColumnType("DECIMAL(18, 2)").IsRequired();

                entity.HasOne(i => i.Categoria)
                      .WithMany(c => c.Itens)
                      .HasForeignKey(i => i.CategoriaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configura��o da entidade Pedido
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NomeCliente).IsRequired();
                entity.Property(e => e.DataPedido).HasColumnType("DATETIME").IsRequired();

                // Relacionamento 1:N com DetalhePedido
                entity.HasMany(p => p.DetalhesPedido)
                      .WithOne(dp => dp.Pedido)
                      .HasForeignKey(dp => dp.PedidoId);
            });

            // Configura��o da entidade DetalhePedido
            modelBuilder.Entity<DetalhePedido>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantidade).IsRequired();

                entity.HasOne(dp => dp.Pedido)
                      .WithMany(p => p.DetalhesPedido)
                      .HasForeignKey(dp => dp.PedidoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
