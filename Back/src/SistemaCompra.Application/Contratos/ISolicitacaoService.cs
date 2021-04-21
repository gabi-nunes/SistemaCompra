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
        Task<Solicitacao> AddSolicitacao(Solicitacao model);
        Task<SolicitacaoProduto> AddSolicitacaoProduto(SolicitacaoProduto model);
        Task<Solicitacao> UpdateSolicitacao(int SolicitacaoId, Solicitacao model);
        Task<bool> DeleteSolicitacao(int SolicitacaoId);

        Task<Solicitacao[]> GetAllSolicitacaoAsync();

        Task<Solicitacao[]> GetAllSolicitacaobyDataAsync(string DataCriacao);

        Task<Solicitacao> GetSolicitacaobyIdAsync(int SolicitacaoId);

        Task<Solicitacao[]> GetSolicitacaoPendenteAsync();

        Task<Solicitacao> AprovaSolicitacaoAsync(int id, int statusAprovacao, string nomeAprovador);

    }
}

