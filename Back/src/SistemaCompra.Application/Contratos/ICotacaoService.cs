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
        Task<Cotacao> CreatCotacao(int SolicitacaoId, CotacaoDto model);
        Task<Cotacao> AddCotacaoProduto(int CotacaoId);
        Task<Cotacao> UpdateCotacao(int CotacaoId, CotacaoDto model);
        Task<double> CalcQuantAsync(int id);
        Task<bool> EnviarEmail(int id);
        Task<bool> DeleteCotacao(int CotacaoId);

        Task<Cotacao[]> GetAllCotacaoAsync();
        Task<int> TheLastID();
        Task<Cotacao[]> GetCotacaobyFornecedorAsync(int FornecedorId);


        Task<Cotacao[]> GetAllCotacaobyDataAsync(DateTime DataCriacao);

        Task<Cotacao> GetCotacaobyIdAsync(int CotacaoId);

        Task<FornecedorIdealDto> fornecedorIdeal(int idsol);
        Task<FornecedorIdealDto> GetFornecedorMaioresRankingAsync(int id);
        Task<Cotacao> CotacaoVencedora(int idCot);
        Task<Cotacao[]> GetCotacaoPendenteAsync();
        Task<Cotacao[]> GetCotacaoEncerradaAsync();
        Task<ItemCotacao[]> GetAllCotacaobyItemAsync(int CotacaoId);
        Task<Cotacao[]> GetAllCotacaobySolicitacaoAsync(int SolicitacaoId);

        Task<Cotacao> EnviarOfetarAsync(int idCot, EnviarOfertaDto model);

        Task<ItemCotacao> EnviarPrecooAsync(int id, double value);
    }
}
