using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaCompra.Domain;
using SistemaCompra.Persistence.Contratos;

namespace SistemaCompra.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        private readonly GoodPlaceContext Context;
        public GeralPersist(GoodPlaceContext context)
        {
            Context = context;
        }
        //GERAL
        public void Add<T>(T entity) where T : class
        {
            Context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            Context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            Context.Remove(entity);
        }

        public void DeleteRange<T>(T entityArray) where T : class
        {
            Context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await Context.SaveChangesAsync()) > 0; 
        }

        

       
    }
}