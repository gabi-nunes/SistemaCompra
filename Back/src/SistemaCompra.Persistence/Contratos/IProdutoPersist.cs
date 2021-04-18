using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Persistence.Contratos
{
    public interface IProdutoPersist 
    {
        Task<Produto[]> GetAllProdutoAsync();
        Task<Produto> GetAllProdutoByIdAsync(int id);
        Task<Produto> GetProdutoByDescricaoAsync(string desc);
        Task<Produto[]> GetProdutobyFamilia(int FamiliaProdutoid);
        Task<FamiliaProduto> GetProdutobyDesFamiliaProduto(string Desc);
    }
}
