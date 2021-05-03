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
    public class CotacaoPersist : ICotacaoPersist
    {
        private readonly GoodPlaceContext Context;
        public CotacaoPersist(GoodPlaceContext context)
        {
            this.Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<SolicitacaoProduto> GetAllSolicitacaoProdutoByIdAsync(int id)
        {
            IQueryable<SolicitacaoProduto> query = Context.solicitacaoProduto;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Solicitacao_Id == id);

            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<Solicitacao> GetAllSolicitacaoByIdAsync(int id)
        {
            IQueryable<Solicitacao> query = Context.Solicitacoes
                .Include(e => e.SolicitacaoProdutos)
                .ThenInclude(e => e.Produto);

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cotacao[]> GetAllCotacaoAsync()
        {
            IQueryable<Cotacao> query = Context.Cotacoes
                .Include(e => e.ItensCotacao);

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<Cotacao> GetAllCotacaoByIdAsync(int id)
        {
            IQueryable<Cotacao> query = Context.Cotacoes
                 .Include(e => e.ItensCotacao);

            return await query.OrderByDescending(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<Cotacao> GetAllCotacaoByIdsemProdAsync(int id)
        {
            IQueryable<Cotacao> query = Context.Cotacoes;
                 

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public Task<ItemCotacao> GetAllItemCotacaoByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ItemCotacao[]> GetAllItemCotacaoByIdCotAsync(int id)
        {
            IQueryable<ItemCotacao> query = Context.itensCotacao;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.cotacaoId == id);

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<Cotacao[]> GetCotacaoByDataCotacaoAsync(DateTime Data)
        {
            IQueryable<Cotacao> query = Context.Cotacoes
                .Include(e => e.ItensCotacao);


            query = query.Where(e => e.PrazoOferta == Data);
            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public  async Task<Cotacao[]> GetCotacaoByPendenteAsync()
        {
            IQueryable<Cotacao> query = Context.Cotacoes;

            return await query.Where(e => e.status == 0).ToArrayAsync();
        }

        public async Task<Cotacao> GetIdLast()
        {
            IQueryable<Cotacao> query = Context.Cotacoes;


            return await query.OrderByDescending(e => e.Id).FirstOrDefaultAsync();
        }

      
    }
}
