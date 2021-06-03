using SistemaCompra.Application.DTO.Request;
using SistemaCompra.Application.DTO.Response;
using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.Contratos
{
    public interface ICotacaoService
    {
        Task<Cotacao> CreateCotacao(int SolicitacaoId, CotacaoDto model);
        Task<Cotacao> AddCotacaoProduto(int CotacaoId, int SolicitacaoId);
        Task<Cotacao> UpdateCotacao(int CotacaoId, CotacaoDto model);
        double CalcTotalAsync(ItemCotacao[] itensCots);
        Task<bool> EnviarEmail(int id);
        Task<bool> DeleteCotacao(int CotacaoId);

        Task<Cotacao[]> GetAllCotacaoAsync();
        Task<int> TheLastID();
        Task<Cotacao> EscolherFornecedorGanhador(int idsol);
        Task<Cotacao[]> GetCotacaobyFornecedorAsync(int FornecedorId);

        Task<Cotacao[]> GetAllCotacaobyDataAsync(string DataCriacao);

        Task<Cotacao> GetCotacaobyIdAsync(int CotacaoId);

        Task<FornecedorIdealDto> fornecedorIdeal(int idsol);
        Task<double> CalcQuantporItemAsync(int id);
        Task<Fornecedor[]> FornecedorMaioresRankingAsync(int id);
        Task<Cotacao[]> GetCotacaoPendenteAsync();
        Task<Cotacao[]> GetCotacaoEncerradaAsync();
        Task<Cotacao[]> GetCotacaoPorFornecedorIDAsync();
        Task<ItemCotacao[]> GetAllCotacaobyItemAsync(int CotacaoId);
        Task<Cotacao[]> GetAllCotacaobySolicitacaoAsync(int SolicitacaoId);

        Task<Cotacao> EnviarOfertaAsync(int idCot, EnviarOfertaDto model);

        Task<ItemCotacao> EnviarPrecooAsync(int id, Enviapreco model);
    }
}
