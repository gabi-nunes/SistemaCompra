using System.Collections.Generic;

namespace SistemaCompra.Domain
{
    public class SolicitacaoProduto
    {
        public int Id { get; set; }
        //public int ProdutoId { get; set; }
        public int QtdeProduto { get; set; }
        public int Solicitacaoid { get; set; }
        public Solicitacao Solicitacao { get; set; }
        public int Produtoid { get; set; }
        public Produto Produto { get; set; }


    }
}