using Microsoft.AspNetCore.Http;
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
    public class FamiliaProdutoController : ControllerBase
    {
        private readonly IFamiliaProdutoService FamiliaProdutoService;
        public FamiliaProdutoController(IFamiliaProdutoService FamprodutoService)
        {
            FamiliaProdutoService = FamprodutoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var produto = await FamiliaProdutoService.GetAllProdutoAsync();
                if (produto == null) return NotFound("Nenhum Produto encontrado!");
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar usuarios. Erro: {ex.Message}");
            }
        }

    }
}
