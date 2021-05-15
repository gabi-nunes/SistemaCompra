using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.DTO.Request
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public DateTime DataEmissao { get; set; }
        public string Observacao { get; set; }
        public int cotacaoId { get; set; }

    }
}
