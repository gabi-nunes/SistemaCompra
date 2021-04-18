using Microsoft.EntityFrameworkCore;
using SistemaCompra.Domain;
using SistemaCompra.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Persistence
{
    public class ProdutoPersist : IProdutoPersist
    {
        private readonly GoodPlaceContext Context;
        public ProdutoPersist(GoodPlaceContext context)
        {
            this.Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Produto[]> GetAllProdutoAsync()
        {

            IQueryable<Produto> query = Context.Produtos
                  .Include(e => e.FamiliaProduto);

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<Produto> GetAllProdutoByIdAsync(int id)
        {
            IQueryable<Produto> query = Context.Produtos;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Produto> GetProdutoByDescricaoAsync(string desc)
        {
            IQueryable<Produto> query = Context.Produtos
                .Include(e => e.FamiliaProduto);

            query = query.Where(e => e.Descricao.ToLower().Contains(desc.ToLower()));
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

        public async  Task<Produto[]> GetProdutobyFamilia(int FamiliaProdutoid)
        {
            IQueryable<Produto> query = Context.Produtos
                .Include(e => e.FamiliaProduto);

            query = query.AsNoTracking().OrderBy(e => e.FamiliaProdId)
                         .Where(e => e.FamiliaProdId == FamiliaProdutoid);

            return await query.OrderBy(e => e.FamiliaProdId).ToArrayAsync();
        }

        public async Task<FamiliaProduto> GetProdutobyDesFamiliaProduto(string Desc)
        {
            IQueryable<FamiliaProduto> query = Context.FamiliaProdutos;

            query = query.Where(e => e.Descricao.ToLower().Contains(Desc.ToLower()));
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();

           
        }


    }
}
