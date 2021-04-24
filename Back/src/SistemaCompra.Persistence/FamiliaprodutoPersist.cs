﻿using Microsoft.EntityFrameworkCore;
using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Persistence.Contratos
{
    public class FamiliaprodutoPersist : IFamiliaProdutoPersist
    {
        private readonly GoodPlaceContext Context;

        public FamiliaprodutoPersist(GoodPlaceContext context)
        {
            this.Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
     

        public async Task<FamiliaProduto[]> GetAllprodutoAsync()
        {
            IQueryable<FamiliaProduto> query = Context.FamiliaProdutos;

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

    }
}
