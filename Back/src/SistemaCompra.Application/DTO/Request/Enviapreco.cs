using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.DTO.Request
{
    public class Enviapreco
    {
        public int itemcotacao { get; set; }
        public double preco { get; set; }
        public  double total{ get; set; }
    }
}
