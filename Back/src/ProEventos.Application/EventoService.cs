using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Application.DTOs;
using ProEventos.Persistence.Contratos;
using ProEventos.Domain;
using AutoMapper;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist FGeralPersist;
        private readonly IEventoPersist FEventoPresist;
        private readonly IMapper FMapper;
        public EventoService(IEventoPersist evento, IGeralPersist geral, IMapper mapper)
        {
            FMapper = mapper;
            FEventoPresist = evento;
            FGeralPersist = geral;

        }
        public async Task<EventoDto> AddEvento(EventoDto model)
        {
            try
            {

                Evento evento = FMapper.Map<Evento>(model); 
                FGeralPersist.Add<Evento>(evento);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    Evento eventoResultado = await FEventoPresist.GetEventoByIdAsync(evento.Id, false);
                    return FMapper.Map<EventoDto>(eventoResultado);
                }
                return null;
            }   
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
        {
            try
            {
                var LEvento = await FEventoPresist.GetEventoByIdAsync(eventoId, false);
                if (LEvento == null) return null;

                model.Id = LEvento.Id;
                FMapper.Map(model, LEvento);
                FGeralPersist.Update<Evento>(LEvento);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    Evento eventoResult = await FEventoPresist.GetEventoByIdAsync(model.Id, false);
                    return FMapper.Map<EventoDto>(eventoResult);
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
            return false;
            // try
            // {
            //     var LEvento = await FEventoPresist.GetEventoByIdAsync(eventoId, false);
            //     if (LEvento == null) throw new Exception("Evento não foi Deletado pois não foi      encontrado");

            //     FGeralPersist.Delete<EventoDto>(LEvento);
            //     return await FGeralPersist.SaveChangesAsync();
            // }
            // catch (Exception ex)
            // {
            //     throw new Exception(ex.Message);
            // }

        }

        public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await FEventoPresist.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;
                var result = FMapper.Map<EventoDto[]>(eventos);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await FEventoPresist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;
                var result = FMapper.Map<EventoDto[]>(eventos);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await FEventoPresist.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (evento == null) return null;

                var result = FMapper.Map<EventoDto>(evento);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}