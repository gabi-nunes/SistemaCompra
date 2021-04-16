using System.Threading.Tasks;
using SistemaCompra.Domain;

namespace SistemaCompra.Persistence.Contratos
{
    public interface IUserPersist
    {
        Task<User[]> GetAllUserAsync(); 
        Task<User> GetAllUserByIdAsync(int id); 
        Task<User[]> GetUserByNameAsync(string Name);
        Task<User> GetLogin(string email, string senha);

        Task<User> GetUserByEmailAsync(string Email);
        Task<User> recuperarSenha(string email);
        
    }
}