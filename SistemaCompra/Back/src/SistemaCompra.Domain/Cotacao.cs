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
        public IEnumerable<ItemCotacao> ItensCotacao { get; set; }
        public IEnumerable<CotacaoFornecedorPedido> CotacaoFornecedorPedido { get; set; }



    }
}