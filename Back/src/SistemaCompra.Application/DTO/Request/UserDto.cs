using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.DTO.Request
{
    public class UserDto
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string Setor { get; set; }
        public string Cargo { get; set; }

    }
}
