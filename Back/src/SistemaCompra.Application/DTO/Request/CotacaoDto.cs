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
        public DateTime DataEmissaoCotacao { get; set; }
        public int status { get; set; }
        public int Parcelas { get; set; }
        public int FrmPagamento { get; set; }
        public DateTime PrazoOfertas { get; set; }
        public int fornecedorId { get; set; }

    }
}
