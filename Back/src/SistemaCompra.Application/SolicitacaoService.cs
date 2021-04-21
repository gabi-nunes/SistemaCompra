using SistemaCompra.Application.Contratos;
using SistemaCompra.Domain;
using SistemaCompra.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Application
{
    public class SolicitacaoService : ISolicitacaoService
    {

        private readonly IGeralPersist FGeralPersist;
        private readonly ISolicitacaoPersist _SolicitacaoPresist;
        public SolicitacaoService(ISolicitacaoPersist solicitacaoPresist, IGeralPersist geral)
        {
            _SolicitacaoPresist = solicitacaoPresist;
            FGeralPersist = geral;
        }
        public async Task<Solicitacao> AddSolicitacao(Solicitacao model)
        {
            try
            {
                FGeralPersist.Add<Solicitacao>(model);

                if (await FGeralPersist.SaveChangesAsync())
                {
                    var SolicitacaoRetorno = await _SolicitacaoPresist.GetAllSolicitacaoByIdAsync(model.Id);

                    return SolicitacaoRetorno;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SolicitacaoProduto> AddSolicitacaoProduto(SolicitacaoProduto model)
        {
            try
            {
                FGeralPersist.Add<SolicitacaoProduto>(model);

                if (await FGeralPersist.SaveChangesAsync())
                {
                    var SolicitacaoRetorno = await _SolicitacaoPresist.GetAllSolicitacaoProdByIdAsync(model.Id);

                    return SolicitacaoRetorno;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Solicitacao> AprovaSolicitacaoAsync(int id ,int statusAprovacao, string nomeAprovador)
        {
            try
            {
                var LESolicitacao = await _SolicitacaoPresist.GetAllSolicitacaoByIdAsync(id);
                if (LESolicitacao == null) return null;
                //atenção aqui
                LESolicitacao.StatusAprovacao = statusAprovacao;
                LESolicitacao.Aprovador = nomeAprovador;

                FGeralPersist.Update<Solicitacao>(LESolicitacao);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await _SolicitacaoPresist.GetAllSolicitacaoByIdAsync(LESolicitacao.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteSolicitacao(int SolicitacaoId)
        {
            try
            {
                var solicitacao = await _SolicitacaoPresist.GetAllSolicitacaoByIdAsync(SolicitacaoId);
                if (solicitacao == null) throw new Exception("Solicitacao para delete não encontrado.");

                FGeralPersist.Delete<Solicitacao>(solicitacao);
                return await FGeralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Solicitacao[]> GetAllSolicitacaoAsync()
        {
            try
            {
                var solicitacao = await _SolicitacaoPresist.GetAllSolicitacaoAsync();
                if (solicitacao == null) return null;
                return solicitacao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Solicitacao[]> GetAllSolicitacaobyDataAsync(string DataCriacao)
        {
            try
            {
                var solicitacao = await _SolicitacaoPresist.GetSolicitacaoByDataSolicitacaoAsync(DataCriacao);
                if (solicitacao == null) return null;
                return solicitacao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Solicitacao> GetSolicitacaobyIdAsync(int SolicitacaoId)
        {
            try
            {
                var solicitacao = await _SolicitacaoPresist.GetAllSolicitacaoByIdAsync(SolicitacaoId);
                if (solicitacao == null) return null;
                return solicitacao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Solicitacao[]> GetSolicitacaoPendenteAsync()
        {
            try
            {
                var solicitacao = await _SolicitacaoPresist.GetSolicitacaoByPendenteAsync();
                if (solicitacao == null) return null;
                return solicitacao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Solicitacao> UpdateSolicitacao(int SolicitacaoId, Solicitacao model)
        {
            try
            {
                var LEuser = await _SolicitacaoPresist.GetAllSolicitacaoByIdAsync(SolicitacaoId);
                if (LEuser == null) return null;
                //atenção aqui
                model.Id = LEuser.Id;

                FGeralPersist.Update<Solicitacao>(model);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await _SolicitacaoPresist.GetAllSolicitacaoByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
