using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.DTO.Response
{
    public class FornecedorIdealDto
    {
        public int? FornecedorIdeal { get; set; }
        public int? FornecedorMenorPreco { get; set; }
        public int? FornecedorMenorData { get; set; }
        public Fornecedor[] FornecedoresRanking { get; set; }
        public bool isForncedorMaior { get; set; }

    }
}
