using System.Threading.Tasks;
using SistemaCompra.Domain;

namespace SistemaCompra.Persistence.Contratos
{
    public interface IUserPersist
    {
        Task<user[]> GetAllUserAsync(); 
        Task<user> GetAllUserByIdAsync(int id); 
        Task<user[]> GetUserByNameAsync(string Name);
        Task<user> GetLogin(string email, string senha);

        Task<user> GetUserByEmailAsync(string Email);
        Task<user> recuperarSenha(string email);
        Task<user> GetIdLast();


    }
}