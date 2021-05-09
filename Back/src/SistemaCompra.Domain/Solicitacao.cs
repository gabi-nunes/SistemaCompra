using System;
using System.Collections.Generic;

namespace SistemaCompra.Domain
{
    public class Solicitacao
    {
        public int Id { get; set; }
        //public int SolicitacaoProdutoId { get; set; }
        public int user_id { get; set; }
        public user User { get; set; }
        public string Observacao { get; set; }
        public DateTime DataNecessidade { get; set; }
        public DateTime? DataAprovacao { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public int StatusAprovacao { get; set; }
        public int? IdAprovador { get; set; }
        public string? ObservacaoRejeicao { get; set; }
        public IEnumerable<Cotacao> Cotacoes { get; set; }
        public IEnumerable<SolicitacaoProduto> SolicitacaoProdutos { get; set; }
    }
}