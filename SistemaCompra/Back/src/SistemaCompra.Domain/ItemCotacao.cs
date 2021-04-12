namespace SistemaCompra.Domain
{
    public class ItemCotacao
    {
        public int Id { get; set; }
        public int IdCotacao { get; set; }
        public int IdSolicitacaoProduto { get; set; }
        public int IdProduto { get; set; }
        public int QtdeProduto { get; set; }
        public double PrecoUnit { get; set; }
    }
}