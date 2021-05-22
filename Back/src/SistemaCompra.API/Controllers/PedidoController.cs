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
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService PedidoService;
        public PedidoController(IPedidoService _pedidoService)
        {
            PedidoService = _pedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Cotacao = await PedidoService.GetAllPedidoAsync();
                if (Cotacao == null) return NotFound("Nenhum cotacao encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar cotacao. Erro: {ex.Message}");
            }
        }

        [HttpGet("Id/{PedidoId}")]
        public async Task<IActionResult> GetbyId(int PedidoId)
        {
            try
            {
                var Cotacao = await PedidoService.GetPedidobyIdAsync(PedidoId);
                if (Cotacao == null) return NotFound("Nenhum pedido encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar pedido. Erro: {ex.Message}");
            }
        }

        [HttpGet("DataAdmissao/{DataEmissao}")]
        public async Task<IActionResult> GetdataEmissao(string date)
        {
            try
            {
                var Cotacao = await PedidoService.GetPedidoByDataEmissaoPedidoAsync(date);
                if (Cotacao == null) return NotFound("Nenhum pedido encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar pedido. Erro: {ex.Message}");
            }
        }
        [HttpGet("DataAdmissao/{DataAdmissao}")]
        public async Task<IActionResult> GetdataAdimissao(string date)
        {
            try
            {
                var Cotacao = await PedidoService.GetPedidoByDataAdimicapPedidoAsync(date);
                if (Cotacao == null) return NotFound("Nenhum pedido encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar pedido. Erro: {ex.Message}");
            }
        }

        [HttpGet("Pendente")]
        public async Task<IActionResult> GetPendente()
        {
            try
            {
                var Cotacao = await PedidoService.GetPedidoByPendenteAsync();
                if (Cotacao == null) return NotFound("Nenhum pedido encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar pedido. Erro: {ex.Message}");
            }
        }

        [HttpGet("Aprovado")]
        public async Task<IActionResult> GetAprovador()
        {
            try
            {
                var Cotacao = await PedidoService.GetPedidoByAprovacaoAsync();
                if (Cotacao == null) return NotFound("Nenhum pedido encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar pedido. Erro: {ex.Message}");
            }
        }


        [HttpGet("FornecedorGanhador/{FamiliaProduto}")]
        public async Task<IActionResult> GetFornecedorGanhador(int FamiliaProduto)
        {
            try
            {
                var Cotacao = await PedidoService.GetFornecedorGanhadorAsync(FamiliaProduto);
                if (Cotacao == null) return NotFound("Nenhum pedido encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar pedido. Erro: {ex.Message}");
            }
        }
        [HttpGet("VisualizarRanking/{FamiliaProduto}")]
        public async Task<IActionResult> GetVisualizarRanking(int FamiliaProduto)
        {
            try
            {
                var Cotacao = await PedidoService.GetvisualizarRankingAsync(FamiliaProduto);
                if (Cotacao == null) return NotFound("Nenhum fornecedor encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Forncedor. Erro: {ex.Message}");
            }
        }


        [HttpGet("Fornecedor/{Fornecedorid}")]
        public async Task<IActionResult> GetFornecedor(int Fornecedorid)
        {
            try
            {
                var Cotacao = await PedidoService.GetFornecedorByIdAsync(Fornecedorid);
                if (Cotacao == null) return NotFound("Nenhum pedido encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar pedido. Erro: {ex.Message}");
            }
        }
        [HttpGet("Cotacao/{cotacaoid}")]
        public async Task<IActionResult> GetCot(int cotacaoid)
        {
            try
            {
                var Cotacao = await PedidoService.GetCotacaoByIdAsync(cotacaoid);
                if (Cotacao == null) return NotFound("Nenhum pedido encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar pedido. Erro: {ex.Message}");
            }
        }

        [HttpGet("Rejeitados")]
        public async Task<IActionResult> GetRejeitado()
        {
            try
            {
                var Cotacao = await PedidoService.GetPedidoByRejeitasAsync();
                if (Cotacao == null) return NotFound("Nenhum pedido encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar pedido. Erro: {ex.Message}");
            }
        }
        [HttpGet("ItemPedido/{Pedidoid}")]
        public async Task<IActionResult> GetItemPedido(int Pedidoid)
        {
            try
            {
                var Cotacao = await PedidoService.GetItemPedidoByIdAsync(Pedidoid);
                if (Cotacao == null) return NotFound("Nenhum pedido encontrado!");
                return Ok(Cotacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar pedido. Erro: {ex.Message}");
            }
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Post([FromBody] PedidoDto model)
        {
            try
            {
                var pedido = await PedidoService.CreatePedido(model);
                if (pedido == null) return BadRequest("Erro ao tentar Adicionar a cotacao.");
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar Cotacao. Erro: {ex.Message}");
            }
        }

        [HttpPost("ItemPedido/{Id}")]
        public async Task<IActionResult> PostItemPedido(int Id)
        {
            try
            {
                var pedido = await PedidoService.AddItemPedido(Id);
                if (pedido == null) return BadRequest("Erro ao tentar Adicionar a cotacao.");
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar Cotacao. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cotacao = await PedidoService.DeletePedido(id);
                if (cotacao == null) return NoContent();

                return Ok(new { messagem = "Deletado" });
                       
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPut("AlterarStatus/{id}")]
        public async Task<IActionResult> Putalterarstatus(int id, [FromBody] AprovarPedidoDTO model)
        {
            try
            {
                var solicitacao = await PedidoService.AprovaPedidoAsync(id, model);

                if (solicitacao == null) return BadRequest("Erro ao alterar status. Tente Novamente!");
                return Ok(solicitacao);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar efetuar a alteração de status. Erro: {ex.Message}");
            }
        }


        [HttpPut("ValorMaximo/{valor}")]
        public async Task<IActionResult> PutValor(double valor)
        {
            try
            {
                var pedido = await PedidoService.valorMaximo(valor);

                if (pedido == null) return BadRequest("Erro ao alterar status. Tente Novamente!");
                return Ok(pedido);
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
                int id = await PedidoService.TheLastID();
                if (id == null) return NoContent();

                return Ok(id);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar a última Solicitação . Erro: {ex.Message}");
            }
        }

        [HttpGet("PedidoPorFornecedorId/{fornecedorId}")]
        public async Task<IActionResult> Getid(int fornecedorId)
        {
            try
            {
                var pedido = await PedidoService.GetPedidoByfornecedorId(fornecedorId);
                if (pedido == null) return NoContent();

                return Ok(pedido);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar a última Solicitação . Erro: {ex.Message}");
            }
        }


    }
}
