using System.Threading.Tasks;
using SistemaCompra.Domain;

namespace SistemaCompra.Application.Contratos
{
    public interface IuserService
    {
        Task<user> AddUser(user model);
       Task<user> UpdateUser(int userId, user model);
        Task<bool> DeleteUser(int userId);

        Task<user[]> GetAllUserAsync();

        Task<user> GetAllUserbyemailAsync(string email);
        Task<user[]> GetAllUserbyNameAsync(string nome);

        Task<user> GetuserbyIdAsync(int userId);

      Task<user> Login(Login login);

        Task<user> RecuperarSenha(string email);

        bool EnviarEmail(string email);

        Task<user> AlterarSenha(int id, string senha);
    }
}