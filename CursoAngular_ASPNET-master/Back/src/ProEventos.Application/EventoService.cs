using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist FGeralPersist;
        private readonly IEventoPersist FEventoPresist;
        public EventoService(IEventoPersist evento, IGeralPersist geral)
        {
            FEventoPresist = evento;
            FGeralPersist = geral;

        }
        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                FGeralPersist.Add<Evento>(model);
                if (await FGeralPersist.SaveChangesAsync())        
                {
                    return await FEventoPresist.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var LEvento = await FEventoPresist.GetEventoByIdAsync(eventoId, false);
                if (LEvento == null) return null;
                 
                model.Id = LEvento.Id;

                FGeralPersist.Update<Evento>(model);
                if (await FGeralPersist.SaveChangesAsync())        
                {
                    return await FEventoPresist.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var LEvento = await FEventoPresist.GetEventoByIdAsync(eventoId, false);
                if (LEvento == null) throw new Exception("Evento não foi Deletado pois não foi      encontrado");
                 
                FGeralPersist.Delete<Evento>(LEvento);
                return await FGeralPersist.SaveChangesAsync();        
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await FEventoPresist.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;
                return eventos; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await FEventoPresist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;
                return eventos; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await FEventoPresist.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (evento == null) return null;
                return evento; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}