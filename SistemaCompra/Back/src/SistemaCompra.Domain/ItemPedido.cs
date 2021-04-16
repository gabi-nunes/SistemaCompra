namespace SistemaCompra.Domain
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProduto { get; set; }
        public int QtdeProduto { get; set; }
        public double PrecoUnit { get; set; }
         public int itemCotacaoId { get; set; }
         public ItemCotacao itemCotacao { get; set; }
        public int PedidoId { get; set; }
         public Pedido Pedido { get; set; }
    }
}