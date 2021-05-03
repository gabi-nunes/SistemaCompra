using SistemaCompra.Application.Contratos;
using SistemaCompra.Application.DTO.Request;
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
        public Solicitacao solicitacao;
        public List<SolicitacaoProduto> sps;


        public SolicitacaoService(ISolicitacaoPersist solicitacaoPresist, IGeralPersist geral)
        {
            _SolicitacaoPresist = solicitacaoPresist;
            FGeralPersist = geral;
        }
        public async Task<Solicitacao> CreateSolicitacao(int userId, SolicitacaoDTO model)
        {
            try
            {
                var user = await _SolicitacaoPresist.GetAllUserByIdAsync(userId);

                solicitacao = new Solicitacao();
                solicitacao.Observacao = model.Observacao;
                solicitacao.DataNecessidade = model.DataNecessidade;
                solicitacao.DataSolicitacao = model.DataSolicitacao;
                solicitacao.user_id = user.Id;
                solicitacao.StatusAprovacao = model.StatusAprovacao;
      

                FGeralPersist.Add<Solicitacao>(solicitacao);

                if (await FGeralPersist.SaveChangesAsync())
                {
                    var SolicitacaoRetorno = await _SolicitacaoPresist.GetAllSolicitacaoByIdAsync(solicitacao.Id);

                    return SolicitacaoRetorno;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<user> GetuserbyIdAsync(int userId)
        {
            try
            {
                var usuarios = await _SolicitacaoPresist.GetAllUserByIdAsync(userId);
                if (usuarios == null) return null;
                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<SolicitacaoProduto> AddSolicitacaoProduto(int solicitacaId, SolicitacaoProdutoDTO model)
        {
            try
            {
                var solicitacao = await _SolicitacaoPresist.GetAllSolicitacaoByIdAsync(solicitacaId);
                if (solicitacao == null) return null;

                sps = new List<SolicitacaoProduto>();

                
                    var produto = await _SolicitacaoPresist.GetAllProduByIdAsync(model.ProdutoId);
                    SolicitacaoProduto sp = new SolicitacaoProduto();
                    sp.Solicitacao_Id = solicitacao.Id;
                    sp.Produto_Id = produto.Id;
                    sp.Id = model.id;
                    sp.QtdeProduto = model.QtdeProduto;
                    FGeralPersist.Add<SolicitacaoProduto>(sp);
              
                

                //  FGeralPersist.Update<SolicitacaoProduto>(sps);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await _SolicitacaoPresist.GetAllSolicitacaoProdByIdAsync(solicitacaId);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Solicitacao> AprovaSolicitacaoAsync(int id, AprovaSolicitacaoDTO model)
        {
            try
            {
              
                var LESolicitacao = await _SolicitacaoPresist.GetAllSolicitacaoByIdsemProdAsync(id);
                if (LESolicitacao == null) return null;

                solicitacao = LESolicitacao;
                solicitacao.DataAprovacao = model.DataAprovacao;
                solicitacao.Aprovador = model.Aprovador;
                 solicitacao.StatusAprovacao = model.StatusAprovacao;

                FGeralPersist.Update<Solicitacao>(solicitacao);
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

        public async Task<Solicitacao[]> GetAllSolicitacaobyDataAsync(DateTime DataCriacao)
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

        public async Task<Solicitacao> UpdateSolicitacao(int SolicitacaoId, SolicitacaoDTO model)
        {
            try
            {
                var LESolicitacao = await _SolicitacaoPresist.GetAllSolicitacaoByIdsemProdAsync(SolicitacaoId);
                if (LESolicitacao == null) return null;
                //atenção aqui
                LESolicitacao.DataNecessidade = model.DataNecessidade;
                LESolicitacao.DataSolicitacao = model.DataSolicitacao;
                LESolicitacao.Observacao = model.Observacao;
                solicitacao = LESolicitacao;

                FGeralPersist.Update<Solicitacao>(solicitacao);
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

        public async Task<SolicitacaoProduto> UpdateSolicitacaoProduto(int Id, SolicitacaoProdutoDTO model)
        {
            try
            {
                var LESolicitacaoprod = await _SolicitacaoPresist.GetSolicitacaoProdByIdAsync(Id);
                if (LESolicitacaoprod == null) return null;
                //atenção aqui
                LESolicitacaoprod.QtdeProduto = model.QtdeProduto;

                FGeralPersist.Update<SolicitacaoProduto>(LESolicitacaoprod);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await _SolicitacaoPresist.GetAllSolicitacaoProdByIdAsync(Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteSolicitacaoProduto(int SolicitacaoPrdoId)
        {
            try
            {
                var solicitacaoProduto = await _SolicitacaoPresist.GetSolicitacaoProdByIdAsync(SolicitacaoPrdoId);
                if (solicitacaoProduto == null) throw new Exception("Solicitacao para delete não encontrado.");

                FGeralPersist.Delete<SolicitacaoProduto>(solicitacaoProduto);
                return await FGeralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int > TheLastID()
        {
            try
            {
                int idLast;
                var solicitacao = await _SolicitacaoPresist.GetIdLast();
                if (solicitacao == null) throw new Exception("Última Solicitacao não encontrada.");

                idLast = solicitacao.Id;
                return idLast;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
