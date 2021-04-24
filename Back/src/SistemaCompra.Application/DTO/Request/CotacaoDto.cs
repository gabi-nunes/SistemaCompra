using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.DTO.Request
{
    public class CotacaoDto
    {
        public int Id { get; set; }
        public DateTime PrazoCotacao { get; set; }
        public int SolicitacaoId { get; set; }
        public int status { get; set; }
        public DateTime PrazoOferta { get; set; }
        

    }
}
