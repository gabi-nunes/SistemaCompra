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
    public class CotacaoService : ICotacaoService
    {

        private readonly IGeralPersist FGeralPersist;
        private readonly ICotacaoPersist _CotacaoPresist;
        public Cotacao Cotacao;
        public List<ItemCotacaoDto> sps;

        public CotacaoService(ICotacaoPersist cotacaoPresist, IGeralPersist geral)
        {
            _CotacaoPresist = cotacaoPresist;
            FGeralPersist = geral;
        }

        public async Task<ItemCotacao[]> AddCotacaoProduto(int SolicitacaoId, ItemCotacaoDto model)
        {
            try
            {
                var Cotacao = await _CotacaoPresist.GetAllCotacaoByIdAsync(model.Id);
                if (Cotacao == null) return null;

                sps = new List<ItemCotacaoDto>();


                var itemCotacaoPro = await _CotacaoPresist.GetAllSolicitacaoProdutoByIdAsync(SolicitacaoId);

                   
                    ItemCotacao sp = new ItemCotacao();
                    sp.Id= model.Id;
                    sp.IdSolicitacaoProduto= itemCotacaoPro.Id;
                    sp.IdProduto = itemCotacaoPro.Produto_Id;
                    sp.QtdeProduto = model.QtdeProduto;
                    sp.cotacaoId = model.cotacaoId;

                    FGeralPersist.Add<ItemCotacao>(sp);
                

                //  FGeralPersist.Update<SolicitacaoProduto>(sps);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await _CotacaoPresist.GetAllItemCotacaoByIdCotAsync(model.cotacaoId);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cotacao> EnviarCotacaoAsync(int idCot, EnviarOfertaDto model)
        {
            var cotacao = await _CotacaoPresist.GetAllCotacaoByIdAsync(idCot);
            var itemCot = await _CotacaoPresist.GetAllItemCotacaoByIdCotAsync(idCot);
            if (cotacao == null) return null;
           

            cotacao.Id = model.Id;
            cotacao.Frete = model.Frete;
            Cotacao.status = model.status;
            cotacao.FrmPagamento = model.FrmPagamento;
            cotacao.Parcelas = model.Parcelas;
            cotacao.Total = model.Total;
            cotacao.fornecedorId = model.fornecedorId;
            cotacao.ItensCotacao = (IEnumerable<ItemCotacao>)itemCot;



            FGeralPersist.Update<Cotacao>(cotacao);


            //  FGeralPersist.Update<SolicitacaoProduto>(sps);
            if (await FGeralPersist.SaveChangesAsync())
            {
                return await _CotacaoPresist.GetAllCotacaoByIdAsync(model.Id);
            }
            return null;


        }


        public async Task<ItemCotacao> EnviarPrecooAsync(int id, double value)
        {
            var Itemcotacao = await _CotacaoPresist.GetAllItemCotacaoByIdAsync(id);
            if (Itemcotacao == null) return null;

            Itemcotacao.PrecoUnit = value;

            FGeralPersist.Update<ItemCotacao>(Itemcotacao);


            //  FGeralPersist.Update<SolicitacaoProduto>(sps);
            if (await FGeralPersist.SaveChangesAsync())
            {
                return await _CotacaoPresist.GetAllItemCotacaoByIdAsync(id);
            }
            return null;


        }

        public async Task<Cotacao> CreatCotacao(int SolicitacaoId, CotacaoDto model)
        {

            try
            {
                var solicitacao = await _CotacaoPresist.GetAllSolicitacaoByIdAsync(SolicitacaoId);

                Cotacao = new Cotacao();
                Cotacao.Id = model.Id;
                Cotacao.PrazoCotacao = model.PrazoCotacao;
                Cotacao.SolicitacaoId = model.SolicitacaoId;
                Cotacao.status = model.status;
                Cotacao.PrazoOferta = model.PrazoOferta;


                FGeralPersist.Add<Cotacao>(Cotacao);

                if (await FGeralPersist.SaveChangesAsync())
                {
                    var SolicitacaoRetorno = await _CotacaoPresist.GetAllCotacaoByIdAsync(model.Id);

                    return SolicitacaoRetorno;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> DeleteCotacao(int CotacaoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCotacaoProduto(int CotacaoPrdoId)
        {
            throw new NotImplementedException();
        }

        public  async Task<Cotacao[]> GetAllCotacaoAsync()
        {
            try
            {
                var cotacaos = await _CotacaoPresist.GetAllCotacaoAsync();
                if (cotacaos == null) return null;
                return cotacaos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Cotacao[]> GetAllCotacaobyDataAsync(DateTime DataCriacao)
        {
            throw new NotImplementedException();
        }

        public Task<Cotacao> GetCotacaobyIdAsync(int CotacaoId)
        {
            throw new NotImplementedException();
        }

        public Task<Cotacao[]> GetCotacaoPendenteAsync()
        {
            throw new NotImplementedException();
        }

        public Task<user> GetuserbyIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> TheLastID()
        {
            throw new NotImplementedException();
        }

        public Task<Cotacao> UpdateCotacao(int CotacaoId, CotacaoDto model)
        {
            throw new NotImplementedException();
        }

        public Task<ItemCotacao> UpdateCotacaoProduto(int Id, ItemCotacaoDto model)
        {
            throw new NotImplementedException();
        }
    }
}
