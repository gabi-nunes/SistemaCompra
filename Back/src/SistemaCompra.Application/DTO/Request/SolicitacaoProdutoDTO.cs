using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.DTO.Request
{
    public class SolicitacaoProdutoDTO
    {
        public int id { get; set; }
        public int QtdeProduto { get; set; }
        public int ProdutoId { get; set; }
    }
}
