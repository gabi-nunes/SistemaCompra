using System;
using System.Collections.Generic;

namespace SistemaCompra.Domain
{
    public class Cotacao
    {
        public int Id { get; set; }
        public DateTime PrazoCotacao { get; set; }
        public int SolicitacaoId { get; set; }
        public Solicitacao Solicitacao { get; set; }
        public double Frete { get; set; }
        public int FrmPagamento { get; set; }
        public DateTime PrazoOferta { get; set; }
        public int Parcelas { get; set; }
        public int FornecedorGanhadorId { get; set; }
        public double Total { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public int fornecedorId { get; set; }
        public Fornecedor fornecedorid { get; set; }
        public IEnumerable<ItemCotacao> ItensCotacao { get; set; }




    }
}