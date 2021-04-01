using System.Threading.Tasks;
using SistemaCompra.Domain;

namespace SistemaCompra.Persistence.Contratos
{
    public interface IEventoPersist
    {
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes); 
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes); 
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes); 
    }
}