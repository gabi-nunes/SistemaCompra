namespace SistemaCompra.Persistence.Contratos
{
    public interface IUserPersist
    {
          Task<Evento[]> GetAllEventosAsync(bool includePalestrantes); 
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes); 
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes);  
    }
}