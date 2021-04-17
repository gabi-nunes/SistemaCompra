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
        
        private readonly GoodPlaceContext Context;

        public UserPersist(GoodPlaceContext  context)
        {
            this.Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<User[]> GetAllUserAsync()
        {
            IQueryable<User> query = Context.Users;
        
            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<User> GetAllUserByIdAsync(int id)
        {
           
            IQueryable<User> query =  Context.Users;
                
            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<User[]> GetUserByNameAsync(string Name)
       {
            IQueryable<User> query = Context.Users;

            query = query.Where(e => e.Name.ToLower().Contains(Name.ToLower()));
            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<User> GetUserByEmailAsync(string Email)
        {
            IQueryable<User> query = Context.Users;

            query = query.Where(e => e.email.ToLower().Contains(Email.ToLower()));
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<User> GetLogin(string email, string senha)
       {
            IQueryable<User> query = Context.Users;
            //atreção aqui
            query = query.Where(e => e.email.ToLower()==email.ToLower() && e.Senha==senha);
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

           public async Task<User> recuperarSenha(string email)
       {
            IQueryable<User> query = Context.Users;
            //atreção aqui
            query = query.Where(e => e.email.ToLower().Contains(email.ToLower()));
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

    }
}