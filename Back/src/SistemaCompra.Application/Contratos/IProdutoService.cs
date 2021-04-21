using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.Contratos
{
    public interface IProdutoService
    {
        Task<Produto[]> GetAllProdutoAsync();
        Task<Produto> GetAllProdutoByIdAsync(int id);
        Task<Produto[]> GetProdutobyFamiliaId(int idFamilia);
        Task<Produto> GetByDescricaoAsync(string desc);
        Task<Produto[]> GetProdutobyFamilia(string desc);
        
    }
}
