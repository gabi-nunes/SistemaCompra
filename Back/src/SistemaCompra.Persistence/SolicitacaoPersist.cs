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
    public class SolicitacaoPersist : ISolicitacaoPersist
    {
        private readonly GoodPlaceContext Context;

        public SolicitacaoPersist(GoodPlaceContext context)
        {
            this.Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Solicitacao[]> GetAllSolicitacaoAsync()
        {
            IQueryable<Solicitacao> query = Context.Solcitacoes
                .Include(e => e.SolicitacaoProdutos);

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<Solicitacao> GetAllSolicitacaoByIdAsync(int id)
        {
            IQueryable<Solicitacao> query = Context.Solcitacoes
                .Include(e => e.SolicitacaoProdutos);

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async  Task<SolicitacaoProduto> GetAllSolicitacaoProdByIdAsync(int id)
        {
            IQueryable<SolicitacaoProduto> query = Context.solicitacaoProduto;
          

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Solicitacao[]> GetSolicitacaoByDataSolicitacaoAsync(string DataCriacao)
        {
            IQueryable<Solicitacao> query = Context.Solcitacoes
               .Include(e => e.SolicitacaoProdutos);

            query = query.Where(e => e.DataSolicitacao == DataCriacao);
            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public  async  Task<Solicitacao[]> GetSolicitacaoByPendenteAsync()
        {
            IQueryable<Solicitacao> query = Context.Solcitacoes
                .Include(e => e.SolicitacaoProdutos).Where(e=> e.StatusAprovacao == 2);

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }
    }
}
