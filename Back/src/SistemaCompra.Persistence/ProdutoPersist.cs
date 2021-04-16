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
    public class ProdutoPersist : IprodutoPersist
    {
        private readonly ProEventosContext Context;

        public ProdutoPersist(ProEventosContext context)
        {
            this.Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
      
        public async Task<user[]> GetAllProdutoAsync()
        {
            IQueryable<user> query = Context.Users;

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<user> GetAllProdutoByIdAsync(int id)
        {

            IQueryable<user> query = Context.Users;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<user[]> GetProdutoByDescricaoAsync(string Name)
        {
            IQueryable<user> query = Context.Users;

            query = query.Where(e => e.Name.ToLower().Contains(Name.ToLower()));
            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<user[]> GetProdutoByFamilia(int idFamilia)
        {
            IQueryable<user> query = Context.Users;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == idFamilia);

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

    }
}
