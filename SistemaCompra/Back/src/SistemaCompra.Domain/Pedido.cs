using System;

namespace SistemaCompra.Domain
{
    public class Pedido
    {
        public int Id { get; set; }
        public int CotacaoFornecedorPedidoId { get; set; }
        public CotacaoFornecedorPedido CotacaoFornecedorPedido { get; set; }
        public int StatusAprov { get; set; }
        public DateTime DataEmissao { get; set; }
        public string Aprovador { get; set; }
        public string? Observacao { get; set; }

        

    }
}