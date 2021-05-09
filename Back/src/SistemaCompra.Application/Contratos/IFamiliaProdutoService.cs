using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.Contratos
{
    public interface IFamiliaProdutoService
    {
        Task<FamiliaProduto[]> GetAllFamiliaProdutoAsync();
        Task<FamiliaProduto> GetFamiliaProdutoByIdAsync(int FamiliaProdId);
        
        }
}
