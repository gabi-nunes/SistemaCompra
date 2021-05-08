using Microsoft.EntityFrameworkCore;
using SistemaCompra.Domain;

namespace SistemaCompra.Persistence
{
    public class GoodPlaceContext : DbContext
    {
        public GoodPlaceContext(DbContextOptions<GoodPlaceContext> options) : base(options){}
        public DbSet<user> Users { get; set; }
        public DbSet<Solicitacao> Solicitacoes { get; set; }
        public DbSet<SolicitacaoProduto> solicitacaoProduto { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<FamiliaProduto> FamiliaProdutos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Cotacao> Cotacoes { get; set; }
        public DbSet<ItemCotacao> itensCotacao { get; set; }
        public DbSet<ItemPedido> itensPedido{ get; set; }
        public DbSet<Pedido> Pedido { get; set; }

        protected override void OnModelCreating(ModelBuilder mb){
        

             mb.Entity<user>().HasKey(e => e.Id);
                               
        }
    }
}