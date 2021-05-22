using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Persistence.Contratos
{
    public interface IpedidoPersist
    {
        Task<Pedido[]> GetAllPedidoAsync();
        Task<Pedido> GetPedidoByIdAsync(int id);
        Task<Cotacao> GetCotacaooByIdAsync(int id);
        Task<ItemPedido[]> GetAllItemPedidoByIdPedidosync(int id);
        Task<Pedido> GetIdLast();
        Task<Pedido[]> GetPedidoByRejeitasAsync();
        Task<Pedido[]> GetPedidoByPendenteAsync();
        Task<Pedido[]> GetPedidoByAprovacaoAsync();
        Task<Pedido[]> GetPedidoByfornecedorId(int fornecedorId);
        Task<Fornecedor[]> GetFornecedorGanhadorAsync(int famailiaid);
        Task<Fornecedor> GetFornecedorByIdAsync(int Fornecedorid);
        Task<Pedido> GetPedidoByIdCotacaoAsync(int id);
        Task<Pedido[]> GetPedidoByDataEmissaoPedidoAsync(string DataEmissao);
        Task<Pedido[]> GetPedidoByDataAdimicapPedidoAsync(string DataAdicao);
        Task<ItemPedido> GetItemPedidoByIdAsync(int id);
        Task<Fornecedor[]> GetvisualizarRankingAsync(int FamiliaProdutoid);
        Task<ItemCotacao[]> GetItemCotacaoByIdCotacaoAsync(int id);

        Task<Pedido> GetPedidoByIdsemProdAsync(int id);
        
    }
}
