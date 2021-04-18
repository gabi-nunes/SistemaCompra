// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using SistemaCompra.Application.Contratos;
// using SistemaCompra.Domain;

// namespace SistemaCompra.API.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class SistemaCompraController : ControllerBase
// {
//     private readonly IEventoService eventoService;
//     public SistemaCompraController(IEventoService eventoService)
//     {
        
//         this.eventoService = eventoService;
//     }


//         [HttpGet]
//         public async Task<IActionResult> Get()
//         {
//             try
//             {
//             var eventos = await eventoService.GetAllEventosAsync(true);
//             if (eventos == null ) return NotFound("Nenhum evento Encontrado");
//             return Ok(eventos);
//             }
//             catch (Exception ex)
//             {
//                 return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
//             }
//         }

//         [HttpGet("{Id}")]
//         public async Task<IActionResult> GetById(int Id)
//         {
//             try
//             {
//             var evento = await eventoService.GetEventoByIdAsync(Id, true);
//             if (evento == null ) return NotFound("Nenhum evento foi Encontrado com o Id informado.");
//             return Ok(evento);
//             }
//             catch (Exception ex)
//             {
//                 return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento pelo Id. Erro: {ex.Message}");
//             }
//         }

//         [HttpGet("tema/{tema}")]
//         public async Task<IActionResult> GetByTema(string tema)
//         {
//             try
//             {
//             var eventos = await eventoService.GetAllEventosByTemaAsync(tema, true);
//             if (eventos == null ) return NotFound("Nenhum evento foi Encontrado com o tema informado.");
//             return Ok(eventos);
//             }
//             catch (Exception ex)
//             {
//                 return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento pelo tema. Erro: {ex.Message}");
//             }
//         }


//         [HttpPost]
//         public async Task<IActionResult> Post(Evento model)
//         {
//             try
//             {
//             var evento = await eventoService.AddEvento(model);
//             if (evento == null ) return BadRequest("Erro ao tentar Adicionar o Evento.");
//             return Ok(evento);
//             }
//             catch (Exception ex)
//             {
//                 return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar o evento. Erro: {ex.Message}");
//             }
//         }

//         [HttpPut("{Id}")]
//         public async Task<IActionResult> Put(int Id, Evento model)
//         {
//             try
//             {
//             var evento = await eventoService.UpdateEvento(Id, model);
//             if (evento == null ) return BadRequest("Não foi possível encontrar o Evento");
//             return Ok(evento);
//             }
//             catch (Exception ex)
//             {
//                 return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Atualizar o evento. Erro: {ex.Message}");
//             }
//         }

//         [HttpDelete("{Id}")]
//         public async Task<IActionResult> Delete(int Id)
//         {
//             try
//             {
//             if (await eventoService.DeleteEvento(Id))
//                 return Ok("Evento Deletado");
//             else
//                 return BadRequest("Erro ao Deletar o Evento");
//             }
//             catch (Exception ex)
//             {
//                 return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar o evento. Erro: {ex.Message}");
//             }
//         }
//     }
// }
