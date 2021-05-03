using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application.Contratos;
using SistemaCompra.Application.DTO.Request;
using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaCompra.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolicitacaoController : ControllerBase
    {
        private readonly ISolicitacaoService SolicitacaoService;
        public SolicitacaoController(ISolicitacaoService solicitacaoservice)
        {
            SolicitacaoService = solicitacaoservice;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var solicitacaos = await SolicitacaoService.GetAllSolicitacaoAsync();
                if (solicitacaos == null) return NotFound("Nenhuma Solicitacao encontrado!");
                return Ok(solicitacaos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Solicitacao. Erro: {ex.Message}");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var solicitacaos = await SolicitacaoService.GetSolicitacaobyIdAsync(Id);
                if (solicitacaos == null) return NotFound("Nenhuma Solicitacao foi Encontrado com o Id informado.");
                return Ok(solicitacaos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o Solicitacao pelo Id. Erro: {ex.Message}");
            }
        }

        
        [HttpGet("Data/{dataCriacao}")]
        public async Task<IActionResult> GetBynome(DateTime dataCriacao)
        {
            try
            {
                var solicitacaos = await SolicitacaoService.GetAllSolicitacaobyDataAsync(dataCriacao);
                if (solicitacaos == null) return NotFound("Nenhuma Solicitacao foi Encontrado com data informado.");
                return Ok(solicitacaos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o Solicitacao pelo data. Erro: {ex.Message}");
            }
        }

        [HttpGet("AprovacaoPendente")]
        public async Task<IActionResult> GetAprovacao()
        {
            try
            {
                var solicitacaos = await SolicitacaoService.GetSolicitacaoPendenteAsync();
                if (solicitacaos == null) return NotFound("Nenhuma Solicitacao foi Encontrado com pendente informado.");
                return Ok(solicitacaos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o Solicitacao pendente. Erro: {ex.Message}");
            }
        }

        [HttpPost("Registrar/{userId}")]
        public async Task<IActionResult> Post(int userId, [FromBody] SolicitacaoDTO model)
        {
            try
            {
                var solicitacao = await SolicitacaoService.CreateSolicitacao(userId, model);
                if (solicitacao == null) return BadRequest("Erro ao tentar Adicionar o Solicitacao.");
                return Ok(solicitacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar o Solicitacao Produto. Erro: {ex.Message}");
            }
        }
        [HttpPost("SolicitacaoProd/{solicitacaoId}")]
        public async Task<IActionResult> PutSolicitacaoProduto(int solicitacaoId, [FromBody] SolicitacaoProdutoDTO model)
        {
            try
            {
                var solicitacao = await SolicitacaoService.AddSolicitacaoProduto(solicitacaoId, model);
                if (solicitacao == null) return BadRequest("Erro ao tentar Adicionar o Solicitacao produto.");
                return Ok(solicitacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar o Solicitacao Produto. Erro: {ex.Message}");
            }
        }
        
        [HttpPut("Atualiza/{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] SolicitacaoDTO model)
        {
            try
            {
                var solicitacao = await SolicitacaoService.UpdateSolicitacao(Id, model);
                if (solicitacao == null) return BadRequest("Não foi possível encontrar o Solicitante");
                return Ok(solicitacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Atualizar solicitacao. Erro: {ex.Message}");
            }
        }


        [HttpPut("AtualizaSolicitacaoProduto/{Id}")]
        public async Task<IActionResult> PutSolicitacaoProd(int Id, [FromBody] SolicitacaoProdutoDTO model)
        {
            try
            {
                var solicitacao = await SolicitacaoService.UpdateSolicitacaoProduto(Id, model);
                if (solicitacao == null) return BadRequest("Não foi possível encontrar o Solicitacao Produto");
                return Ok(solicitacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Atualizar solicitacao. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var solicitacao = await SolicitacaoService.GetSolicitacaobyIdAsync(id);
                if (solicitacao == null) return NoContent();

                return await SolicitacaoService.DeleteSolicitacao(id) ?
                       Ok(new { messagem = "Deletado" }) :
                       throw new Exception("Ocorreu um problem não específico ao tentar deletar Solicitacao.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
        }

        [HttpDelete("DeleteSolicitacaoProduto/{id}")]
        public async Task<IActionResult> DeleteSoliProd(int id)
        {
            try
            {
                var solicitacao = await SolicitacaoService.DeleteSolicitacaoProduto(id);
                if (!solicitacao) return NoContent();

                return Ok(new { messagem = "Deletado" });
                       
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPut("AlterarStatus/{id}")]
        public async Task<IActionResult> Putalterarstatus(int id, [FromBody] AprovaSolicitacaoDTO model)
        {
            try
            {
                var solicitacao = await SolicitacaoService.AprovaSolicitacaoAsync(id, model);

                if (solicitacao == null) return BadRequest("Erro ao alterar status. Tente Novamente!");
                return Ok(solicitacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar efetuar a alteração de status. Erro: {ex.Message}");
            }
        }

        [HttpGet("UltimoId")]
        public async Task<IActionResult> GetidLast()
        {
            try
            {
                int id = await SolicitacaoService.TheLastID();
                if (id == null) return NoContent();

                return Ok(id);
                     
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar a última Solicitação . Erro: {ex.Message}");
            }
        }

    }

}
