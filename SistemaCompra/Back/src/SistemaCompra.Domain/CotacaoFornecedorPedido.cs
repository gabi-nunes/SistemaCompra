using System;
using System.Collections.Generic;

namespace SistemaCompra.Domain
{
    public class CotacaoFornecedorPedido
    {
        public int Id { get; set; }
        public double Frete { get; set; }
        public int FrmPagamento { get; set; }
        public DateTime PrazoOferta { get; set; }
        public int Parcelas { get; set; }
        public int FornecedorGanhadorId { get; set; }
        public double Total { get; set; }
        public int CotacaoId { get; set; }
        public Cotacao Cotacao { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        //public int IdFornecedor { get; set; }
        public IEnumerable<Fornecedor> Fornecedores { get; set; }


    }
}