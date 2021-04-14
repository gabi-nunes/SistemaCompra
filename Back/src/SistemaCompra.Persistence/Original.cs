// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using ProEventos.Domain;
// using ProEventos.Persistence.Contratos;

// namespace ProEventos.Persistence
// {
//     public class ProEventosContext : IProEventosContext
//     {
//         private readonly ProEventosContext Context;
//         public ProEventosContext(ProEventosContext context)
//         {
//             this.Context = context;

//         }
//         //GERAL
//         public void Add<T>(T entity) where T : class
//         {
//             Context.Add(entity);
//         }
//         public void Update<T>(T entity) where T : class
//         {
//             Context.Update(entity);
//         }

//         public void Delete<T>(T entity) where T : class
//         {
//             Context.Remove(entity);
//         }

//         public void DeleteRange<T>(T entityArray) where T : class
//         {
//             Context.RemoveRange(entityArray);
//         }

//         public async Task<bool> SaveChangesAsync()
//         {
//             return (await Context.SaveChangesAsync()) > 0; 
//         }

//         //EVENTOS
//         public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
//         {
//             IQueryable<Evento> query = Context.Eventos;
//             query = query.Include(e => e.Lotes)
//                          .Include(e => e.RedesSociais);
//             if (includePalestrantes){
//                 query = query.Include(e => e.PalestrantesEventos)
//                              .ThenInclude(pe => pe.Palestrante);
//             }  

//             return await query.OrderBy(e => e.Id).ToArrayAsync();
//         }
//         public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, 
//                                                              bool includePalestrantes = false)
//         {
//             IQueryable<Evento> query = Context.Eventos;
//             query = query.Include(e => e.Lotes)
//                          .Include(e => e.RedesSociais);
//             if (includePalestrantes){
//                 query = query.Include(e => e.PalestrantesEventos)
//                              .ThenInclude(pe => pe.Palestrante);
//             }  

//             query = query.Where(e => e.Tema.ToLower().Contains(tema.ToLower()));
//             return await query.OrderBy(e => e.Id).ToArrayAsync();
//         }
//         public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
//         {
//             IQueryable<Evento> query = Context.Eventos;
//             query = query.Include(e => e.Lotes)
//                          .Include(e => e.RedesSociais);
//             if (includePalestrantes){
//                 query = query.Include(e => e.PalestrantesEventos)
//                              .ThenInclude(pe => pe.Palestrante);
//             }  

//             query = query.Where(e => e.Id == eventoId);
//             return await query.FirstOrDefaultAsync();
//         }


//         //PALESTRANTES
//         public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
//         {
//             IQueryable<Palestrante> query = Context.Palestrantes;
//             query = query.Include(p => p.RedesSociais);
//             if(includeEventos){
//                 query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
//             }
//             return await query.OrderBy(p => p.Id).ToArrayAsync();
//         }
//         public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, 
//                                                                        bool includeEventos)
//         {
//             IQueryable<Palestrante> query = Context.Palestrantes;
//             query = query.Include(p => p.RedesSociais);
//             if(includeEventos){
//                 query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
//             }
//             query = query.Where(p => p.Nome.ToLower().Contains(nome.ToLower()));
//             return await query.OrderBy(p => p.Nome).ToArrayAsync();
//         }

//         public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, 
//                                                                bool includeEventos)
//         {
//             IQueryable<Palestrante> query = Context.Palestrantes;
//             query = query.Include(p => p.RedesSociais);
//             if(includeEventos){
//                 query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
//             }
//             query = query.Where(p => p.Id == palestranteId);
//             return await query.FirstOrDefaultAsync();
//         }


//     }
// }