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
    public class PedidoPersist : IpedidoPersist
    {
        private readonly GoodPlaceContext Context;
        public PedidoPersist(GoodPlaceContext context)
        {
            this.Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<ItemPedido[]> GetAllItemPedidoByIdPedidosync(int id)
        {
            IQueryable<ItemPedido> query = Context.itensPedido;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.PedidoId == id);

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<Pedido[]> GetAllPedidoAsync()
        {
            IQueryable<Pedido> query = Context.Pedido
                .Include(e => e.itensPedidos);

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        
        }

        public async Task<Cotacao> GetCotacaooByIdAsync(int id)
        {
            IQueryable<Cotacao> query = Context.Cotacoes;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }
        public async Task<Solicitacao> GetSolicitacaoByIdAsync(int id)
        {
            IQueryable<Solicitacao> query = Context.Solicitacoes;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<Fornecedor> GetFornecedorByIdAsync(int Fornecedorid)
        {
            IQueryable<Fornecedor> query = Context.Fornecedores;
            query = query.Where(e => e.Id == Fornecedorid);

            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }
        public async Task<Fornecedor[]> GetvisualizarRankingAsync(int FamiliaProdutoid)
        {
            IQueryable<Fornecedor> query = Context.Fornecedores;
            query = query.Where(e => e.FamiliaProdutoId== FamiliaProdutoid);

            return await query.OrderByDescending(e => e.PontuacaoRanking).ToArrayAsync();
        }

        public async Task<Fornecedor[]> GetFornecedorGanhadorAsync(int famailiaid)
        {
            IQueryable<Fornecedor> query = Context.Fornecedores;
            query = query.Where(e => e.FamiliaProdutoId == famailiaid);

            return await query.OrderBy(e => e.PontuacaoRanking).ToArrayAsync();
        }

        public async Task<Pedido> GetIdLast()
        {
            IQueryable<Pedido> query = Context.Pedido;


            return await query.OrderByDescending(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<ItemCotacao[]> GetItemCotacaoByIdCotacaoAsync(int id)
        {
            IQueryable<ItemCotacao> query = Context.itensCotacao;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.cotacaoId== id);

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<ItemPedido> GetItemPedidoByIdAsync(int id)
        {
            IQueryable<ItemPedido> query = Context.itensPedido;
               
            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<Pedido[]> GetPedidoByAprovacaoAsync()
        {
            IQueryable<Pedido> query = Context.Pedido
                .Include(e => e.itensPedidos);

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.StatusAprov == 0);

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<Pedido[]> GetPedidoByDataAdimicapPedidoAsync(string DataAdicao)
        {
            IQueryable<Pedido> query = Context.Pedido
                .Include(e => e.itensPedidos);

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.DataEmissao == DataAdicao);

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public  async Task<Pedido[]> GetPedidoByDataEmissaoPedidoAsync(string DataEmissao)
        {
            IQueryable<Pedido> query = Context.Pedido
                .Include(e => e.itensPedidos);

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.DataEmissao== DataEmissao);

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public  async Task<Pedido> GetPedidoByIdAsync(int id)
        {
            IQueryable<Pedido> query = Context.Pedido
                 .Include(e => e.itensPedidos)
                 .Include(e=> e.cotacao);

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id== id);

            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<Pedido> GetPedidoByIdCotacaoAsync(int id)
        {
            IQueryable<Pedido> query = Context.Pedido
                 .Include(e => e.itensPedidos);

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }


        public async  Task<Pedido> GetPedidoByIdsemProdAsync(int id)
        {
            IQueryable<Pedido> query = Context.Pedido;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.Id == id);

            return await query.OrderBy(e => e.Id).FirstOrDefaultAsync();
        }

        public async Task<Pedido[]> GetPedidoByPendenteAsync()
        {
            IQueryable<Pedido> query = Context.Pedido;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.StatusAprov == 2);

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }


        public async Task<Pedido[]> GetPedidoByRejeitasAsync()
        {
            IQueryable<Pedido> query = Context.Pedido;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.StatusAprov == 1);

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }

        public async Task<Pedido[]> GetPedidoByfornecedorId(int fornecedorId)
        {
            IQueryable<Pedido> query = Context.Pedido;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                         .Where(e => e.cotacao.fornecedorId == fornecedorId  && e.StatusAprov==0);

            return await query.OrderBy(e => e.Id).ToArrayAsync();
        }
    }
}
