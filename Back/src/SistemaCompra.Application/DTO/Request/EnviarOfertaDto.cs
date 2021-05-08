using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.DTO.Request
{
    public class EnviarOfertaDto
    {
        public double Frete { get; set; }
        public int status { get; set; }
        public int FrmPagamento { get; set; }
        public int Parcelas { get; set; }
        public DateTime DataEntrega { get; set; }
        public int fornecedorId { get; set; }



    }
}
