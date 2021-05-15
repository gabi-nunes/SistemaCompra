using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.DTO.Request
{
    public class AprovaSolicitacaoDTO
    {
        public DateTime DataAprovacao { get; set; }
        public int StatusAprovacao { get; set; }
        public int? IdAprovador { get; set; }
        public string? ObservacaoRejeicao { get; set; }
    }
}
