using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Data;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly DataContext FContext;
        public EventoController(DataContext context)
        {
            FContext = context;
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return FContext.Eventos;
        }

        [HttpGet("{AId}")]
        public Evento GetById(int AId)
        {
            return FContext.Eventos.FirstOrDefault(evento => evento.EventoId == AId);
        }


        [HttpPost]
        public string ExemploPost()
        {
            return "Exemplo de Post";
        }
    }
}
