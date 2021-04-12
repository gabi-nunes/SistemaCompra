namespace SistemaCompra.Domain
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProduto { get; set; }
        public int QtdeProduto { get; set; }
        public double PrecoUnit { get; set; }
    }
}