﻿using SistemaCompra.Application.Contratos;
using SistemaCompra.Application.DTO.Request;
using SistemaCompra.Application.DTO.Response;
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

        public async Task<Cotacao> AddCotacaoProduto(int CotacaoId)
        {
            try
            {
                bool salvar = false;
                var Cotacao = await _CotacaoPresist.GetAllCotacaoByIdAsync(CotacaoId);
                if (Cotacao == null) return null;

                sps = new List<ItemCotacaoDto>();


                var solicitacaoProdutos = await _CotacaoPresist.GetAllSolicitacaoProdutoByIdAsync(Cotacao.SolicitacaoId);


                foreach (SolicitacaoProduto prod in solicitacaoProdutos)
                {
                    ItemCotacao itemCot = new ItemCotacao();
                    itemCot.IdSolicitacaoProduto = prod.Id;
                    itemCot.IdProduto = prod.Produto_Id;
                    itemCot.QtdeProduto = prod.QtdeProduto;
                    itemCot.cotacaoId = Cotacao.Id;
                    itemCot.PrecoUnit = 0.0;
                    FGeralPersist.Add<ItemCotacao>(itemCot);
                    if (await FGeralPersist.SaveChangesAsync())
                    {
                        salvar = true;
                    }
                    else
                    {
                        salvar = false;
                    }

                }

                if (salvar == true)
                {
                    return await _CotacaoPresist.GetAllCotacaoByIdAsync(CotacaoId);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cotacao> EnviarOfetarAsync(int idCot, EnviarOfertaDto model)
        {
            var cotacao = await _CotacaoPresist.GetAllCotacaoByIdAsync(idCot);
            var itemCot = await _CotacaoPresist.GetAllItemCotacaoByIdCotAsync(idCot);
            if (cotacao == null) return null;

            cotacao.Frete = model.Frete;
            cotacao.DataEntrega = model.DataEntrega;
            cotacao.status = model.status;
            cotacao.FrmPagamento = model.FrmPagamento;
            cotacao.Parcelas = model.Parcelas;
            cotacao.Total = await CalcQuantAsync(cotacao.Id);

            FGeralPersist.Update<Cotacao>(cotacao);


            
            if (await FGeralPersist.SaveChangesAsync())
            {
                return await _CotacaoPresist.GetAllCotacaoByIdAsync(idCot);
            }
            return null;


        }


        public async Task<Cotacao> EscolherFornecedorGanhador(int idsol)
        {

            var cotacoes = await _CotacaoPresist.GetCotByCotacaoMenorPreco(idsol);
            if (cotacoes == null) return null;

            bool isFirst = true;
            double total = 0;
            int id = 0;

            foreach (Cotacao item in cotacoes)
            {
                if (isFirst)
                {
                    total = item.Total;
                    isFirst = false;
                    id = item.Id;
                }

                if (item.Total < total)
                {
                    total = item.Total;
                    id = item.Id;
                }
            }

            var cotacaoVencedora = await _CotacaoPresist.GetAllCotacaoByIdAsync(id);
            cotacaoVencedora.FornecedorGanhadorId = cotacaoVencedora.fornecedorId;

            FGeralPersist.Update<Cotacao>(cotacaoVencedora);


         
            if (await FGeralPersist.SaveChangesAsync())
            {
                return await _CotacaoPresist.GetAllCotacaoByIdAsync(idsol);
            }
            return null;


        }


        public async Task<double> CalcQuantAsync(int id)
        {
            var Itemcotacao = await _CotacaoPresist.GetAllItemCotacaoByIdCotAsync(id);
            if (Itemcotacao == null) return 0;

            double Total = 0;
            foreach (ItemCotacao item in Itemcotacao)
            {
                Total = item.QtdeProduto * item.PrecoUnit;
            }


            return Total;

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
        public async Task<double> CalcQuantporItemAsync(int id)
        {
            var Itemcotacao = await _CotacaoPresist.GetAllItemCotacaoByIdAsync(id);
            if (Itemcotacao == null) return 0;

            double precoPorItem = Itemcotacao.PrecoUnit * Itemcotacao.QtdeProduto;


            return precoPorItem;


        }
        public async Task<Cotacao> CreatCotacao(int SolicitacaoId, CotacaoDto model)
        {

            try
            {
                var solicitacao = await _CotacaoPresist.GetAllSolicitacaoByIdAsync(SolicitacaoId);

                Cotacao = new Cotacao();
                Cotacao.Id = model.Id;
                Cotacao.DataEmissaoCotacao = model.DataEmissaoCotacao;
                Cotacao.SolicitacaoId = SolicitacaoId;
                Cotacao.status = model.status;
                Cotacao.PrazoOfertas = model.PrazoOfertas;
                Cotacao.fornecedorId = model.fornecedorId;
                Cotacao.CotadorId = model.CotadorId;


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

        public async Task<bool> DeleteCotacao(int CotacaoId)
        {
            try
            {
                var cotacao = await _CotacaoPresist.GetAllCotacaoByIdAsync(CotacaoId);
                if (cotacao == null) throw new Exception("Cotacao para delete não encontrado.");

                if (cotacao.status != 1)
                {
                    FGeralPersist.Delete<Cotacao>(cotacao);
                    return await FGeralPersist.SaveChangesAsync();
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       

        public async Task<Cotacao[]> GetAllCotacaoAsync()
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

        public async Task<Cotacao[]> GetAllCotacaobyDataAsync(DateTime DataCriacao)
        {
            try
            {
                var cotacaos = await _CotacaoPresist.GetCotacaoByDataCotacaoAsync(DataCriacao);
                if (cotacaos == null) return null;
                return cotacaos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cotacao[]> GetAllCotacaobySolicitacaoAsync(int SolicitacaoId)
        {
            try
            {
                var cotacaos = await _CotacaoPresist.GetCotByIdSolicitacaoAsync(SolicitacaoId);
                if (cotacaos == null) return null;
                return cotacaos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ItemCotacao[]> GetAllCotacaobyItemAsync(int CotacaoId)
        {
            try
            {
                var cotacaos = await _CotacaoPresist.GetAllItemCotacaoByIdCotAsync(CotacaoId);
                if (cotacaos == null) return null;
                return cotacaos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cotacao> GetCotacaobyIdAsync(int CotacaoId)
        {
            try
            {
                var cotacaos = await _CotacaoPresist.GetAllCotacaoByIdAsync(CotacaoId);
                if (cotacaos == null) return null;
                return cotacaos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public  async Task<Cotacao[]> GetCotacaoPendenteAsync()
        {
            try
            {
                var cotacaos = await _CotacaoPresist.GetCotacaoByPendenteAsync();
                if (cotacaos == null) return null;
                return cotacaos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cotacao[]> GetCotacaoEncerradaAsync()
        {
            try
            {
                var cotacaos = await _CotacaoPresist.GetCotacaoByEncerradasAsync();
                if (cotacaos == null) return null;
                return cotacaos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<int> TheLastID()
        {
            try
            {
                int idLats;
                var Cotacao = await _CotacaoPresist.GetIdLast();
                if (Cotacao== null) throw new Exception("Cotacao não encontrado.");

                idLats = Cotacao.Id;
                return idLats;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cotacao> UpdateCotacao(int CotacaoId, CotacaoDto model)
        {
            try
            {
                var cotacao = await _CotacaoPresist.GetAllCotacaoByIdsemProdAsync(CotacaoId);
                if (cotacao == null) return null;
                cotacao.DataEmissaoCotacao = model.DataEmissaoCotacao;
                cotacao.PrazoOfertas = model.PrazoOfertas;

                if (cotacao.status != 1)
                {
                    FGeralPersist.Update<Cotacao>(cotacao);
                    if (await FGeralPersist.SaveChangesAsync())
                    {
                        return await _CotacaoPresist.GetAllCotacaoByIdAsync(model.Id);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fornecedor[]> GetFornecedorMaiorRankingAsync(int id)
        {
            try
            {
                var Fornecedor = await _CotacaoPresist.GetFornecedorGanhadorAsync(id);
                if (Fornecedor == null) return null;

                return Fornecedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FornecedorIdealDto> fornecedorIdeal(int idsol)
        {

            FornecedorIdealDto model = new FornecedorIdealDto();
          

            var cotacoes = await _CotacaoPresist.GetCotByCotacaoMenorPreco(idsol);
            if (cotacoes == null) return null;

            bool isFirstPrice = true;
            bool isFirstDate = true;
            double menorPreco = 0;
            DateTime? menorData = null;
            int idPrice = 0;
            int idDate = 0;


            foreach (Cotacao item in cotacoes)
            {
                if (isFirstPrice)
                {
                    menorPreco = item.Total;
                    isFirstPrice = false;
                    idPrice = item.Id;
                }

                if (item.Total < menorPreco)
                {
                    menorPreco = item.Total;
                    idPrice = item.Id;
                }

            }

            foreach (Cotacao item in cotacoes)
            {
                if (isFirstDate)
                {
                    menorData = item.DataEntrega;
                    isFirstDate = false;
                    idDate = item.Id;
                }

                if (item.DataEntrega < menorData)
                {
                    menorData = item.DataEntrega;
                    idDate = item.Id;
                }

            }
            if (idDate == idPrice)
            {
                var CotacaoRetorno = await _CotacaoPresist.GetAllCotacaoByIdAsync(idPrice);
                model.FornecedorIdeal = CotacaoRetorno.fornecedorId;
                
                    model.isForncedorMaior = true;
                    return model;
               
            }
            else
            {
                var CotacaoMelhorData = await _CotacaoPresist.GetAllCotacaoByIdAsync(idDate);
                model.FornecedorMenorData = CotacaoMelhorData.fornecedorId;
                var CotacaoMelhroPrice = await _CotacaoPresist.GetAllCotacaoByIdAsync(idPrice);
                model.FornecedorMenorPreco = CotacaoMelhroPrice.fornecedorId;
                return model;
            }

        }

        public async Task<Cotacao> CotacaoVencedora(int idCot)
        {
            try
            {
                var CotacaoIdeal = await _CotacaoPresist.GetAllCotacaoByIdAsync(idCot);
                CotacaoIdeal.FornecedorGanhadorId = CotacaoIdeal.fornecedorId;
                CotacaoIdeal.status = 1;

                FGeralPersist.Update<Cotacao>(CotacaoIdeal);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await _CotacaoPresist.GetAllCotacaoByIdAsync(idCot);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FornecedorIdealDto> GetFornecedorMaioresRankingAsync(int id)
        {
            var fornecedorMaiorRanking = await _CotacaoPresist.GetFornecedorGanhadorAsync(id);
            FornecedorIdealDto model = new FornecedorIdealDto();

            model.FornecedoresRanking = new Fornecedor[3];

            if (fornecedorMaiorRanking.Length > 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    model.FornecedoresRanking[i] = fornecedorMaiorRanking[i];
                }
            }
            else
            {
                for (int i = 0; i < fornecedorMaiorRanking.Length; i++)
                {
                    model.FornecedoresRanking[i] = fornecedorMaiorRanking[i];
                }
            }

            return model;
        }
    }
}
