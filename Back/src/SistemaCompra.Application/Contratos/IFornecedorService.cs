using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.Contratos
{
    public interface IFornecedor
    {
        Task<user> AddFornecedor(user model);
        Task<user> UpdateFornecedor(int userId, user model);
        Task<bool> DeleteFornecedor(int userId);

        Task<user[]> GetAllFornecedorAsync();

        Task<user> GetAllFornecedorbyemailAsync(string email);
        Task<user[]> GetAllFornecedorbyNameAsync(string nome);

        Task<user> GetFornecedorbyIdAsync(int userId);

        Task<user> LoginFornecedor(Login loginFornecedor);

        Task<user> RecuperarSenhaFornecedor(string email);

        bool EnviarEmail(string email);

        Task<user> AlterarSenhaFornecedor(int id, string senha);

        Task<user> visualizarCotacao(user model); //modelde cotação com a lista
        Task<user> EnviarOferta(user model);
    }
}
