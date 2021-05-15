using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Persistence.Contratos
{
    public interface IFamiliaProdutoPersist
    {
        Task<FamiliaProduto[]> GetAllFamiliaProdutoAsync();
        Task<FamiliaProduto> GetFamiliaProdutoByIdAsync(int FamiliaProdId);
    }
}
