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
            IQueryable<Solicitacao> query = Context.Solcitacoes;

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<Solicitacao> GetAllSolicitacaoByIdAsync(int id)
        {
            IQueryable<Solicitacao> query = Context.Solcitacoes;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public Task<Solicitacao[]> GetSolicitacaoByDataSolicitacaoAsync(DateTime Data)
        {
            throw new NotImplementedException();
        }
    }
}
