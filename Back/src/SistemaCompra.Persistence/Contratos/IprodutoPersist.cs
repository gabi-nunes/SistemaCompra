using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Persistence.Contratos
{
    public interface IprodutoPersist
    {
     Task<user[]> GetAllProdutoAsync();
       

       Task<user> GetAllProdutoByIdAsync(int id);
       
       Task<user[]> GetProdutoByDescricaoAsync(string Name);
        

        Task<user[]> GetProdutoByFamilia(int idFamilia);
        

    }
}
}
