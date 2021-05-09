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
                var produto = await FamiliaProdutoService.GetAllFamiliaProdutoAsync();
                if (produto == null) return NotFound("Nenhum Produto encontrado!");
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Famílias de Produtos. Erro: {ex.Message}");
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var familiaProduto = await FamiliaProdutoService.GetFamiliaProdutoByIdAsync(id);
                if (familiaProduto == null) return NotFound("Nenhuma Família de Produto encontrada!");
                return Ok(familiaProduto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Família de Produto. Erro: {ex.Message}");
            }
        }

    }
}
