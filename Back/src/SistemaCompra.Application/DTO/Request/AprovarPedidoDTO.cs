using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.DTO.Request
{
    public class AprovarPedidoDTO
    {
        public int AprovadorId { get; set; }
        public DateTime DataAprovacao { get; set; }
        public int StatusAprov { get; set; }
        public string ObservacaoRejeicao { get; set; }
    }
}
