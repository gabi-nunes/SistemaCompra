using SistemaCompra.Application.DTO.Request;
using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.Contratos
{
    public interface IPedidoService
    {
        Task<Pedido> CreatePedido( PedidoDto model);
        Task<ItemPedido[]> AddItemPedido(int PedidoId);
        Task<bool> DeletePedido(int PedidoId);
        Task<Pedido[]> GetAllPedidoAsync();
        Task<int> TheLastID();
        Task<Pedido[]> GetPedidoByDataEmissaoPedidoAsync(DateTime DataEmissao);
        Task<Pedido[]> GetPedidoByDataAdimicapPedidoAsync(DateTime DataAdicao);
        Task<ItemPedido[]> GetItemPedidoByIdAsync(int id);
        Task<Pedido> GetPedidobyIdAsync(int Id);
        Task<Pedido[]> GetPedidoByRejeitasAsync();
        Task<Pedido[]> GetPedidoByPendenteAsync();
        Task<Pedido[]> GetPedidoByAprovacaoAsync();
        Task<Pedido> AprovaPedidoAsync(int id, AprovarPedidoDTO model);
        Task<Fornecedor[]> GetFornecedorGanhadorAsync(int famailiaid);
        Task<Fornecedor> GetFornecedorByIdAsync(int Fornecedorid);
        Task GerarRankingAsync(int cotacaoId);
        Task<Fornecedor[]> GetvisualizarRankingAsync(int FamiliaProdutoid);

    }
}
