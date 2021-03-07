using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        public IEnumerable<Evento> FEventos = new Evento[]{
             new Evento(){
                EventoId = 1,
                Tema = "Angular e .NET 5",
                Local = "São Paulo", 
                Lote = "1° Lote",
                QtdePessoas = 250,
                DataEvento = DateTime.Now.AddDays(15).ToString(),
                ImagemURL = "Puts"
            },
                new Evento(){
                EventoId = 2,
                Tema = "Outro Tema",
                Local = "Rio de Janeiro", 
                Lote = "2° Lote",
                QtdePessoas = 400,
                DataEvento = DateTime.Now.AddDays(4).ToString(),
                ImagemURL = "foto.png"}
        };
        public EventoController()
        {
  
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return FEventos;
        }
       
        [HttpGet("{AId}")]
        public IEnumerable<Evento> GetById(int AId)
        {
            return FEventos.Where(evento => evento.EventoId == AId);
        }
        

        [HttpPost]
        public string ExemploPost()
        {
            return "Exemplo de Post";
        }
    }
}
