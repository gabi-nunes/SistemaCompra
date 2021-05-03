using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application.Contratos;
using SistemaCompra.Application.DTO.Request;
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Cotacao = await CotacaoService.GetAllCotacaoAsync();
                if (Cotacao == null) return NotFound("Nenhum cotacao encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar cotacao. Erro: {ex.Message}");
            }
        }

        [HttpGet("Id/{CotacaoId}")]
        public async Task<IActionResult> GetbyId(int CotacaoId)
        {
            try
            {
                var Cotacao = await CotacaoService.GetCotacaobyIdAsync(CotacaoId);
                if (Cotacao == null) return NotFound("Nenhum cotacao encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar cotacao. Erro: {ex.Message}");
            }
        }

        [HttpGet("EnviarFornecedor/{CotacaoId}")]
        public async Task<IActionResult> GeFornecedortbyId(int CotacaoId)
        {
            try
            {
                var Cotacao = await CotacaoService.GetCotacaobyIdAsync(CotacaoId);
                if (Cotacao == null) return NotFound("Nenhum cotacao encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar cotacao. Erro: {ex.Message}");
            }
        }

        [HttpGet("CotacaoPorSolicitacao/{CotacaoId}")]
        public async Task<IActionResult> GetcotacaobyId(int CotacaoId)
        {
            try
            {
                var Cotacao = await CotacaoService.GetAllCotacaobySolicitacaoAsync(CotacaoId);
                if (Cotacao == null) return NotFound("Nenhum cotacao encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar cotacao. Erro: {ex.Message}");
            }
        }
        [HttpGet("CotacaoPorPrazo/{date}")]
        public async Task<IActionResult> GetcotacaobyId(DateTime date)
        {
            try
            {
                var Cotacao = await CotacaoService.GetAllCotacaobyDataAsync(date);
                if (Cotacao == null) return NotFound("Nenhum cotacao encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar cotacao. Erro: {ex.Message}");
            }
        }

        [HttpGet("CotacaoPendente")]
        public async Task<IActionResult> GetSolicitbyId()
        {
            try
            {
                var Cotacao = await CotacaoService.GetCotacaoPendenteAsync();
                if (Cotacao == null) return NotFound("Nenhum cotacao encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar cotacao. Erro: {ex.Message}");
            }
        }
        [HttpGet("CotacaoEncerrada")]
        public async Task<IActionResult> GetCotEncerrada()
        {
            try
            {
                var Cotacao = await CotacaoService.GetCotacaoEncerradaAsync();
                if (Cotacao == null) return NotFound("Nenhum Cotacao encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Cotacao. Erro: {ex.Message}");
            }
        }

        [HttpGet("CotacaoProduto/{CotacaoId}")]
        public async Task<IActionResult> GetItem(int CotacaoId)
        {
            try
            {
                var Cotacao = await CotacaoService.GetAllCotacaobyItemAsync(CotacaoId);
                if (Cotacao == null) return NotFound("Nenhum cotacao encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar cotacao. Erro: {ex.Message}");
            }
        }
        [HttpGet("FornecedorGanhador/{FamiliaId}")]
        public async Task<IActionResult> GetFornecedorMaiorRankigId(int FamiliaId)
        {
            try
            {
                var Cotacao = await CotacaoService.GetFornecedorMaioresRankingAsync(FamiliaId);
                if (Cotacao == null) return NotFound("Nenhum cotacao encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar cotacao. Erro: {ex.Message}");
            }
        }


        [HttpPost("Registrar/{CotacaoId}")]
        public async Task<IActionResult> Post(int CotacaoId, [FromBody] CotacaoDto model)
        {
            try
            {
                var cotacao = await CotacaoService.CreatCotacao(CotacaoId, model);
                if (cotacao == null) return BadRequest("Erro ao tentar Adicionar a cotacao.");
                return Ok(cotacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar Cotacao. Erro: {ex.Message}");
            }
        }

        [HttpPost("RegistrarItemCot/{CotacaoId}")]
        public async Task<IActionResult> PostItem(int CotacaoId)
        {
            try
            {
                var cotacao = await CotacaoService.AddCotacaoProduto(CotacaoId);
                if (cotacao == null) return BadRequest("Erro ao tentar Adicionar a cotacao.");
                return Ok(cotacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar Cotacao. Erro: {ex.Message}");
            }
        }

        [HttpPut("EnviaPrecoPorItem/{ItemCotacaoId}")]
        public async Task<IActionResult> PutItem(int ItemCotacaoId, double value)
        {
            try
            {
                var cotacao = await CotacaoService.EnviarPrecooAsync(ItemCotacaoId, value);
                if (cotacao == null) return BadRequest("Erro ao tentar Adicionar a cotacao.");
                return Ok(cotacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar Cotacao. Erro: {ex.Message}");
            }
        }

        [HttpGet("RetornarQuantidade/{ItemCotacaoId}")]
        public async Task<IActionResult> GetQuantidade(int ItemCotacaoId)
        {
            try
            {
                var Cotacao = await CotacaoService.CalcQuantAsync(ItemCotacaoId);
                if (Cotacao == null) return NotFound("Nenhum cotacao encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar cotacao. Erro: {ex.Message}");
            }
        }

        [HttpPut("EnviarOferta/{CotacaoId}")]
        public async Task<IActionResult> PutEnviarOfeta(int CotacaoId,[FromBody] EnviarOfertaDto model)
        {
            try
            {
                var cotacao = await CotacaoService.EnviarOfetarAsync(CotacaoId, model);
                if (cotacao == null) return BadRequest("Erro ao tentar Adicionar a cotacao.");
                return Ok(cotacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar Cotacao. Erro: {ex.Message}");
            }
        }


        [HttpPut("CotacaoGanhadore/{IdCot}")]
        public async Task<IActionResult> PutFornecedorVencedor(int IdCot)
        {
            try
            {
                var cotacao = await CotacaoService.CotacaoVencedora(IdCot);
                if (cotacao == null) return BadRequest("Erro ao tentar Definir Fornecedor Ganhador.");
                return Ok(cotacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Definir Fornecedor Ganhador. Erro: {ex.Message}");
            }
        }

        [HttpGet("FornecedorIdeal/{cotacaoId}")]
        public async Task<IActionResult> GetFornecedorIdeal(int cotacaoId)
        {
            try
            {
                var cotacao = await CotacaoService.fornecedorIdeal(cotacaoId);
                if (cotacao == null) return BadRequest("Erro ao tentar Definir Fornecedor Ganhador.");
                return Ok(cotacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Definir Fornecedor Ganhador. Erro: {ex.Message}");
            }
        }

        [HttpGet("FornecedorMaiorRnaking")]
        public async Task<IActionResult> GetFornecedorGanhador(int familiaId)
        {
            try
            {
                var cotacao = await CotacaoService.GetFornecedorMaioresRankingAsync(familiaId);
                if (cotacao == null) return BadRequest("Erro ao tentar Definir Fornecedor Ganhador.");
                return Ok(cotacao);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Definir Fornecedor Ganhador. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cotacao = await CotacaoService.DeleteCotacao(id);
                if (cotacao == null) return NoContent();

                return await CotacaoService.DeleteCotacao(id) ?
                       Ok(new { messagem = "Deletado" }) :
                       throw new Exception("Ocorreu um problem não específico ao tentar deletar cotacao.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPut("Atualiza/{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] CotacaoDto model)
        {
            try
            {
                var cotacao = await CotacaoService.UpdateCotacao(Id, model);
                if (cotacao == null) return BadRequest("Não foi possível encontrar cotacao ou ja encerrada!");
                return Ok(cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Atualizar cotacao. Erro: {ex.Message}");
            }
        }




    }
}
