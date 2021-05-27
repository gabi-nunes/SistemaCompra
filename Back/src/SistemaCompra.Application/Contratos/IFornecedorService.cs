using SistemaCompra.Application.DTO.Request;
using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.Contratos
{
    public interface IFornecedorService
    {
        Task<Fornecedor> AddFornecedor(FornecedorDto model);
        Task<Fornecedor> UpdateFornecedor(int FornecedorId, Fornecedor model);
        Task<bool> DeleteFornecedor(int FornecedorId);
        Task<Fornecedor> GetbyemailAsync(string email);
        Task<Fornecedor[]> GetAllFornecedorAsync();

        Task<Fornecedor> GetAllFornecedorbyemailAsync(string email);
        Task<Fornecedor[]> GetAllFornecedorbyNameAsync(string nome);

        Task<Fornecedor> GetFornecedorbyIdAsync(int FornecedorId);

        Task<Fornecedor> Login(Login login);

        Task<Fornecedor> RecuperarSenha(string email);

        bool EnviarEmail(string email);

        Task<Fornecedor> AlterarSenha(int id, string senha);
    }
}
