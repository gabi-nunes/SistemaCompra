using Microsoft.EntityFrameworkCore;
using SistemaCompra.Domain;
using SistemaCompra.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Persistence
{
    public class FornecedorPersist : IFornecedorPersist
    {
        private readonly ProEventosContext Context;

        public FornecedorPersist(ProEventosContext context)
        {
            this.Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
     
        public async Task<user[]> GetAllFornecedorAsync()
        {
            IQueryable<user> query = Context.Users;

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<user> GetAllFornecedorByIdAsync(int id)
        {

            IQueryable<user> query = Context.Users;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<user[]> GetFornecedorByNameAsync(string Name)
        {
            IQueryable<user> query = Context.Users;

            query = query.Where(e => e.Name.ToLower().Contains(Name.ToLower()));
            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<user> GetFornecedorByEmailAsync(string Email)
        {
            IQueryable<user> query = Context.Users;

            query = query.Where(e => e.email.ToLower().Contains(Email.ToLower()));
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<user> GetLogin(string email, string senha)
        {
            IQueryable<user> query = Context.Users;
            //atreção aqui
            query = query.Where(e => e.email.ToLower() == email.ToLower() && e.Senha == senha);
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<user> recuperarSenha(string email)
        {
            IQueryable<user> query = Context.Users;
            //atreção aqui
            query = query.Where(e => e.email.ToLower().Contains(email.ToLower()));
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<user> visualizarCotacao(user model) //passar a cotação
        {
            IQueryable<user> query = Context.Users;
            //atreção aqui
            query = query.Where(e => e.email.ToLower().Contains(model.email.ToLower()));
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }


    }
}
}
