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
        Task<user[]> GetAllFornecedorAsync();
        Task<user> GetAllFornecedorByIdAsync(int id);
        Task<user[]> GetFornecedorByNameAsync(string Name);
        Task<user> GetLogin(string email, string senha);

        Task<user> GetFornecedorByEmailAsync(string Email);
        Task<user> recuperarSenha(string email);
    }
}
