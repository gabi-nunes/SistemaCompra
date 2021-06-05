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

        public UserPersist(GoodPlaceContext context)
        {
            this.Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<user[]> GetAllUserAsync()
        {
            IQueryable<user> query = Context.Users;
        
            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<user> GetAllUserByIdAsync(int id)
        {
           
            IQueryable<user> query =  Context.Users;
                
            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<user[]> GetUserByNameAsync(string Name)
       {
            IQueryable<user> query = Context.Users;

            query = query.Where(e => e.nome.ToLower().Contains(Name.ToLower()));
            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<user> GetUserByEmailAsync(string Email)
        {
            IQueryable<user> query = Context.Users;

            query = query.Where(e => e.email.ToLower().Contains(Email.ToLower()));
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<user> GetIdLast()
        {
            IQueryable<user> query = Context.Users;


            return await query.OrderByDescending(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<user> GetLogin(string email, string senha)
       {
            IQueryable<user> query = Context.Users;
            //atreção aqui
            query = query.Where(e => e.email.ToLower()==email.ToLower() && e.Senha==senha);
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

           public async Task<user> recuperarSenha(string email)
       {
            IQueryable<user> query = Context.Users;
            //atreção aqui
            query = query.Where(e => e.email.ToLower().Contains(email.ToLower()));
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }
    }
}