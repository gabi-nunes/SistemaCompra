using System;
using System.Collections.Generic;

namespace SistemaCompra.Domain
{
    public class Solicitacao
    {
        public int Id { get; set; }
        //public int SolicitacaoProdutoId { get; set; }
        public int UserId { get; set; }
        public user User { get; set; }
        public string Observacao { get; set; }
        public DateTime DataNecessidade { get; set; }
        public DateTime DataAprovacao { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public int StatusAprovacao { get; set; }
        public string Aprovador { get; set; }
        public int CotacaoId { get; set; }
        public Cotacao Cotacao { get; set; }
        public IEnumerable<SolicitacaoProduto> SolicitaoProdutos { get; set; }




    }
}