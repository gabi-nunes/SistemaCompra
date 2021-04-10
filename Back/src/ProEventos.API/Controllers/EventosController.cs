using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Application.DTOs;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService eventoService;
        public EventosController(IEventoService eventoService)
        {
            this.eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
            var eventos = await eventoService.GetAllEventosAsync(true);
            if (eventos == null ) return NoContent();
            return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
            var evento = await eventoService.GetEventoByIdAsync(Id, true);
            if (evento == null ) return NoContent();
            return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento pelo Id. Erro: {ex.Message}");
            }
        }

        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
            var eventos = await eventoService.GetAllEventosByTemaAsync(tema, true);
            if (eventos == null ) return NoContent();
            return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento pelo tema. Erro: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
            var evento = await eventoService.AddEvento(model);
            if (evento == null ) return NoContent();
            return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar o evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, EventoDto model)
        {
            try
            {
            var evento = await eventoService.UpdateEvento(Id, model);
            if (evento == null ) return NoContent();
            return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Atualizar o evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
            if (await eventoService.DeleteEvento(Id))
                return Ok(new {message = "Evento Deletado"});
            else
                return BadRequest("Erro ao Deletar o Evento");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar o evento. Erro: {ex.Message}");
            }
        }
    }
}
