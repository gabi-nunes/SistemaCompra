using SistemaCompra.Application.DTO.Request;
using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application.Contratos
{
    
    
    public interface ISolicitacaoService
    {
        Task<Solicitacao> CreateSolicitacao(int userId, SolicitacaoDTO model);
        Task<SolicitacaoProduto> AddSolicitacaoProduto(int solicitacaId, SolicitacaoProdutoDTO model);
        Task<Solicitacao> UpdateSolicitacao(int SolicitacaoId, SolicitacaoDTO model);

        Task<SolicitacaoProduto> UpdateSolicitacaoProduto(int Id, SolicitacaoProdutoDTO model);
        Task<bool> DeleteSolicitacao(int SolicitacaoId);

        Task<Solicitacao[]> GetAllSolicitacaoAsync();
        Task<int> TheLastID();

         Task<user> GetuserbyIdAsync(int userId);
     
        Task<Solicitacao[]> GetAllSolicitacaobyDataAsync(DateTime DataCriacao);

        Task<Solicitacao> GetSolicitacaobyIdAsync(int SolicitacaoId);

        Task<Solicitacao[]> GetSolicitacaoPendenteAsync();

        Task<Solicitacao> AprovaSolicitacaoAsync(int id, AprovaSolicitacaoDTO model);

        Task<bool> DeleteSolicitacaoProduto(int SolicitacaoPrdoId);

    }
}

