using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCompra.Domain
{
    public class SolicitacaoProduto
    {
        [Key]public int Id { get; set; }
        //public int ProdutoId { get; set; }
        public int QtdeProduto { get; set; }
        [ForeignKey("Solicitacao")]
        public int Solicitacao_Id { get; set; }
        public Solicitacao Solicitacao { get; set; }

        [ForeignKey("Produto")]
        public int Produto_Id { get; set; }
        public Produto Produto { get; set; }


    }
}