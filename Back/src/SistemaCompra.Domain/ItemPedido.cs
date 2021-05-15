using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCompra.Domain
{
    public class ItemPedido
    {
        [Key]public int Id { get; set; }
        public int IdProduto { get; set; }
        public int QtdeProduto { get; set; }
        public double PrecoUnit { get; set; }
        public int itemCotacaoId { get; set; }
        [ForeignKey("ItemCotacao")] public ItemCotacao itemCotacao { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }
}