using SistemaCompra.Application.DTO.Request;
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
        Task<ItemCotacao[]> AddCotacaoProduto(int SolicitacaoId, ItemCotacaoDto model);
        Task<Cotacao> UpdateCotacao(int CotacaoId, CotacaoDto model);

        Task<ItemCotacao> UpdateCotacaoProduto(int Id, ItemCotacaoDto model);
        Task<bool> DeleteCotacao(int CotacaoId);

        Task<Cotacao[]> GetAllCotacaoAsync();
        Task<int> TheLastID();

        Task<user> GetuserbyIdAsync(int userId);

        Task<Cotacao[]> GetAllCotacaobyDataAsync(DateTime DataCriacao);

        Task<Cotacao> GetCotacaobyIdAsync(int CotacaoId);

        Task<Cotacao[]> GetCotacaoPendenteAsync();

        Task<Cotacao> EnviarCotacaoAsync(int id, EnviarOfertaDto model);

        Task<bool> DeleteCotacaoProduto(int CotacaoPrdoId);

        Task<ItemCotacao> EnviarPrecooAsync(int id, double value);
    }
}
