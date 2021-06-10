using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Persistence.Contratos
{
    public interface IFornecedorPersist 
    {
        Task<Fornecedor[]> GetAllFornecedorAsync();
        Task<Fornecedor> GetAllFornecedorByIdAsync(int id);
        Task<Fornecedor> GetUserByEmailAsync(string Email);
        Task<Fornecedor[]> GetFornecedorByNameAsync(string nome);
        Task<Fornecedor> GetLogin(string email, string senha);
        Task<Fornecedor> GetFornecedorByEmailAsync(string Email);
        Task<Fornecedor> recuperarSenha(string email);
        Task<Fornecedor> GetIdLast(int Familiaid);
    }
}
