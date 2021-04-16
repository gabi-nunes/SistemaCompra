using System.Threading.Tasks;
using SistemaCompra.Domain;

namespace SistemaCompra.Application.Contratos
{
    public interface IUserService
    {
        Task<User> AddUser(User model);
       Task<User> UpdateUser(int UserId, User model);
        Task<bool> DeleteUser(int UserId);

        Task<User[]> GetAllUserAsync();
       
        Task<User[]> GetAllUserbyNameAsync(string nome);

        Task<User> GetUserbyIdAsync(int UserId);

      Task<User> Login(Login login);

        Task<User> RecuperarSenha(string email);

        bool EnviarEmail(string email);

        Task<User> AlterarSenha(int id, string senha);
    }
}