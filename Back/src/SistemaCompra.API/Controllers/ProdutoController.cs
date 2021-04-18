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
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService ProdutoService;
        public ProdutoController(IProdutoService produtoService)
        {
            ProdutoService = produtoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var produto = await ProdutoService.GetAllProdutoAsync();
                if (produto == null) return NotFound("Nenhum Produto encontrado!");
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar usuarios. Erro: {ex.Message}");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var usuarios = await ProdutoService.GetAllProdutoByIdAsync(Id);
                if (usuarios == null) return NotFound("Nenhum Produto foi Encontrado com o Id informado.");
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o usuario pelo Id. Erro: {ex.Message}");
            }
        }

        [HttpGet("Descricao/{desc}")]
        public async Task<IActionResult> GetBynome(string desc)
        {
            try
            {
                var usuarios = await ProdutoService.GetByDescricaoAsync(desc);
                if (usuarios == null) return NotFound("Nenhum produto foi Encontrado com a Descricao informado.");
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o usuario pelo tema. Erro: {ex.Message}");
            }
        }

        [HttpGet("Familia/{desc}")]
        public async Task<IActionResult> GetByFamilia(string desc)
        {
            try
            {
                var usuarios = await ProdutoService.GetProdutobyFamilia(desc);
                if (usuarios == null) return NotFound("Nenhum Produto foi Encontrado com o Id informado.");
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o usuario pelo Id. Erro: {ex.Message}");
            }
        }
    }

}
