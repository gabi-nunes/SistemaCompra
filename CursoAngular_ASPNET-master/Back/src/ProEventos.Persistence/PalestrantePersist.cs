using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext Context;
        public PalestrantePersist(ProEventosContext context)
        {
            this.Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }
        
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = Context.Palestrantes;
            query = query.Include(p => p.RedesSociais);
            if(includeEventos){
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
            return await query.OrderBy(p => p.Id).ToArrayAsync();
        }
        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, 
                                                                       bool includeEventos)
        {
            IQueryable<Palestrante> query = Context.Palestrantes;
            query = query.Include(p => p.RedesSociais);
            if(includeEventos){
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
            query = query.Where(p => p.Nome.ToLower().Contains(nome.ToLower()));
            return await query.OrderBy(p => p.Nome).ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, 
                                                               bool includeEventos)
        {
            IQueryable<Palestrante> query = Context.Palestrantes;
            query = query.Include(p => p.RedesSociais);
            if(includeEventos){
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
            query = query.Where(p => p.Id == palestranteId);
            return await query.FirstOrDefaultAsync();
        }


    }
}