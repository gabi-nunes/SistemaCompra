using System.Collections.Generic;

namespace SistemaCompra.Domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string UnidMedida { get; set; }
        public int FamiliaProdId { get; set; }
        public FamiliaProduto FamiliaProduto { get; set; }
        public IEnumerable<SolicitacaoProduto> SolicitacaoProdutos { get; set; }
    }
}