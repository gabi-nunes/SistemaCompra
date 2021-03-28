using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventoPersist : IEventoPersist
    {
        private readonly ProEventosContext Context;

        public EventoPersist(ProEventosContext context)
        {
            this.Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = Context.Eventos;
            query = query.Include(e => e.Lotes)
                         .Include(e => e.RedesSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos)
                             .ThenInclude(pe => pe.Palestrante);
            }

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }
        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema,
                                                             bool includePalestrantes = false)
        {
            IQueryable<Evento> query = Context.Eventos;
            query = query.Include(e => e.Lotes)
                         .Include(e => e.RedesSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos)
                             .ThenInclude(pe => pe.Palestrante);
            }

            query = query.Where(e => e.Tema.ToLower().Contains(tema.ToLower()));
            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }
        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = Context.Eventos;
            query = query.Include(e => e.Lotes)
                         .Include(e => e.RedesSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos)
                             .ThenInclude(pe => pe.Palestrante);
            }

            query = query.Where(e => e.Id == eventoId);
            return await query.FirstOrDefaultAsync();
        }
    }
}