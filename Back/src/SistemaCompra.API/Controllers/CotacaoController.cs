using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCompra.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CotacaoController : ControllerBase
    {
        private readonly ICotacaoService CotacaoService;
        public CotacaoController(ICotacaoService cotacaoService)
        {
            CotacaoService = cotacaoService;
        }
    }
}
