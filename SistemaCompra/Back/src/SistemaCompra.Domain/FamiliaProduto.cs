using System.Collections.Generic;

namespace SistemaCompra.Domain
{
    public class FamiliaProduto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<Produto> Produtos { get; set; }

    }
}