using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.DTO.Request
{
    public class ItemCotacaoDto
    {
        public int Id { get; set; }
        public int IdSolicitacaoProduto { get; set; }
        public int IdProduto { get; set; }
        public int QtdeProduto { get; set; }
        public double PrecoUnit { get; set; }
        public int cotacaoId { get; set; }


    }
}
