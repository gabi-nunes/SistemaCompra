﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.DTO.Request
{
    public  class ProdutoDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string UnidMedida { get; set; }
        public int FamiliaProdutoId { get; set; }
    }
}
