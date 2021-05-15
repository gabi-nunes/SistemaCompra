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
        private readonly GoodPlaceContext Context;
        public FornecedorPersist(GoodPlaceContext context)
        {
            this.Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async  Task<Fornecedor[]> GetAllFornecedorAsync()
        {
            IQueryable<Fornecedor> query = Context.Fornecedores;

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<Fornecedor> GetAllFornecedorByIdAsync(int id)
        {
            IQueryable<Fornecedor> query = Context.Fornecedores;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Fornecedor> GetUserByEmailAsync(string Email)
        {
            IQueryable<Fornecedor> query = Context.Fornecedores;

            query = query.Where(e => e.Email.ToLower().Contains(Email.ToLower()));
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<Fornecedor[]> GetFornecedorByNameAsync(string nome)
        {
            IQueryable<Fornecedor> query = Context.Fornecedores;

            query = query.Where(e => e.Nome.ToLower().Contains(nome.ToLower()));
            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<Fornecedor> GetFornecedorByEmailAsync(string Email)
        {
            IQueryable<Fornecedor> query = Context.Fornecedores;

            query = query.Where(e => e.Email.ToLower().Contains(Email.ToLower()));
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<Fornecedor> GetLogin(string email, string senha)
        {
            IQueryable<Fornecedor> query = Context.Fornecedores;
            //atreção aqui
            query = query.Where(e => e.Email.ToLower() == email.ToLower() && e.Senha == senha);
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<Fornecedor> recuperarSenha(string email)
        {
            IQueryable<Fornecedor> query = Context.Fornecedores;
            //atreção aqui
            query = query.Where(e => e.Email.ToLower().Contains(email.ToLower()));
            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }
    }
}
