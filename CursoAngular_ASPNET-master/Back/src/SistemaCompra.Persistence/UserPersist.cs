using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaCompra.Domain;
using SistemaCompra.Persistence;
using SistemaCompra.Persistence.Contratos;

namespace SistemaCompra.Persistence
{
    public class UserPersist : IUserPersist
    {
        
        private readonly ProEventosContext Context;

        public UserPersist(ProEventosContext context)
        {
            this.Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<user[]> GetAllUserAsync()
        {
            IQueryable<user> query = Context.Users;
        
            return await query.OrderBy(e => e.CodigoSolicitante).ToArrayAsync();
        }

        public async Task<user> GetAllUserByIdAsync(int id)
        {
            IQueryable<user> query = Context.Users;

            return await query.OrderBy(e => e.CodigoSolicitante).FirstOrDefaultAsync();
        }

        public async Task<user[]> GetUserByNameAsync(string Name)
       {
            IQueryable<user> query = Context.Users;

            query = query.Where(e => e.Name.ToLower().Contains(Name.ToLower()));
            return await query.OrderBy(e => e.CodigoSolicitante).ToArrayAsync();
        }

         public async Task<user> GetLogin(string email, string senha)
       {
            IQueryable<user> query = Context.Users;
            //atreção aqui
            query = query.Where(e => e.email.ToLower().Contains(email.ToLower()) && e.Senha==senha);
            return await query.OrderBy(e => e.CodigoSolicitante).FirstOrDefaultAsync();
        }

           public async Task<user> recuperarSenha(string email)
       {
            IQueryable<user> query = Context.Users;
            //atreção aqui
            query = query.Where(e => e.email.ToLower().Contains(email.ToLower()));
            return await query.OrderBy(e => e.CodigoSolicitante).FirstOrDefaultAsync();
        }
    }
}