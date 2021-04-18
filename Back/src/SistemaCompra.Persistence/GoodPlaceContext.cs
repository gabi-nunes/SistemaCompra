using Microsoft.EntityFrameworkCore;
using SistemaCompra.Domain;

namespace SistemaCompra.Persistence
{
    public class GoodPlaceContext : DbContext
    {
        public GoodPlaceContext(DbContextOptions<GoodPlaceContext> options) : base(options){}
        public DbSet<user> Users { get; set; }
        public DbSet<Solicitacao> Solcitacoes { get; set; }
        public DbSet<SolicitacaoProduto> solicitacaoProduto { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<FamiliaProduto> FamiliaProdutos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Cotacao> Cotacoes { get; set; }
        public DbSet<ItemCotacao> itensCotacao { get; set; }
        public DbSet<ItemPedido> itensPedido{ get; set; }
        public DbSet<Pedido> Pedido { get; set; }

        protected override void OnModelCreating(ModelBuilder mb){
            mb.Entity<SolicitacaoProduto>().HasKey(PE => new {PE.Solicitacaoid, PE.Produtoid});

            mb.Entity<Solicitacao>().HasMany(e => e.SolicitaoProdutos)
                               .WithOne(rs => rs.Solicitacao)
                               .OnDelete(DeleteBehavior.Cascade);

             mb.Entity<FamiliaProduto>().HasMany(p => p.Fornecedores)
                               .WithOne(rs => rs.FamiliaProduto)
                               .OnDelete(DeleteBehavior.Cascade);

             mb.Entity<FamiliaProduto>().HasMany(p => p.Produtos)
                               .WithOne(rs => rs.FamiliaProduto)
                               .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<Pedido>()
            .HasOne(a => a.cotacao)
            .WithOne(a => a.Pedido)
            .HasForeignKey<Cotacao>(c => c.Id);

            mb.Entity<Solicitacao>()
            .HasOne(a => a.Cotacao)
            .WithOne(a => a.Solicitacao)
            .HasForeignKey<Cotacao>(c => c.Id);

            mb.Entity<ItemCotacao>()
            .HasOne(a => a.itemPedido)
            .WithOne(a => a.itemCotacao)
            .HasForeignKey<ItemPedido>(c => c.Id);

           
        

             mb.Entity<user>().HasKey(e => e.Id);
                               
        }
    }
}