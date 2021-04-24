using System.Collections.Generic;

namespace SistemaCompra.Domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string UnidMedida { get; set; }
        public int FamiliaProdutoId { get; set; }
        public FamiliaProduto FamiliaProduto { get;  }
        public IEnumerable<SolicitacaoProduto> SolicitacaoProdutos { get;}
    }
}