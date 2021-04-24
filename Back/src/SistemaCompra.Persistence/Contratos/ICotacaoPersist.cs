using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Persistence.Contratos
{
    public interface ICotacaoPersist
    {
        Task<Cotacao[]> GetAllCotacaoAsync();
        Task<Cotacao> GetAllCotacaoByIdAsync(int id);
        Task<Solicitacao> GetAllSolicitacaoByIdAsync(int id);
        Task<ItemCotacao[]> GetAllItemCotacaoByIdCotAsync(int id);
        Task<Cotacao> GetIdLast();
        Task<Cotacao[]> GetCotacaoByDataCotacaoAsync(DateTime Data);
        Task<Cotacao[]> GetCotacaoByPendenteAsync();
        Task<ItemCotacao> GetAllItemCotacaoByIdAsync(int id);
        Task<Cotacao> GetAllCotacaoByIdsemProdAsync(int id);
        Task<SolicitacaoProduto> GetAllSolicitacaoProdutoByIdAsync(int id);
    }
}
