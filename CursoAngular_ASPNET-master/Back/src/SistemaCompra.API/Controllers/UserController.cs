using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaCompra.Application.Contratos;
using SistemaCompra.Domain;
using SistemaCompra.Persistence;

namespace SistemaCompra.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
             private readonly IuserService UserService;
        public SistemaCompraController(IEventoService userService)
        {
            this.UserService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
            var eventos = await eventoService.GetAllEventosAsync(true);
            if (eventos == null ) return NotFound("Nenhum evento Encontrado");
            return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

    }

}
