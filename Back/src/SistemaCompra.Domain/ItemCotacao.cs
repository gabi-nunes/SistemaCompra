using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCompra.Domain
{
    public class ItemCotacao
    {
        public int Id { get; set; }
        public int IdCotacao { get; set; }
        public int IdSolicitacaoProduto { get; set; }
        public SolicitacaoProduto SolicitacaoProduto { get; set; }
        public int IdProduto { get; set; }
        public int QtdeProduto { get; set; }
        public double PrecoUnit { get; set; }
         public double TotalItem { get; set; }
        public int cotacaoId { get; set; }
        public Cotacao Cotacao { get; set; }
         [ForeignKey("ItemPedido")]public int itemPedidoId { get; set; }
        public ItemPedido itemPedido { get; set; }

    }
}