using System.Collections.Generic;

namespace SistemaCompra.Domain
{
    public class SolicitacaoProduto
    {
        public int Id { get; set; }
        //public int ProdutoId { get; set; }
        public int QtdeProduto { get; set; }
        public IEnumerable<Produto> Produtos { get; set; }
        public IEnumerable<Solicitacao> Solicitacoes { get; set; }
    }
}