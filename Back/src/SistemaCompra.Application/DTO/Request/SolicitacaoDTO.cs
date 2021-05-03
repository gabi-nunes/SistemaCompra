using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.DTO.Request
{
    public class SolicitacaoDTO
    {
        public int Id { get; set; }
        public string Observacao { get; set; }
        public DateTime DataNecessidade { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public int StatusAprovacao { get; set; }
    }
}
