using SistemaCompra.Application.Contratos;
using SistemaCompra.Application.DTO.Request;
using SistemaCompra.Application.DTO.Response;
using SistemaCompra.Domain;
using SistemaCompra.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public async Task<Cotacao> AddCotacaoProduto(int CotacaoId, int SolicitacaoId)
        {
            try
            {
                bool salvar = false;
                sps = new List<ItemCotacaoDto>();

                var solicitacaoProdutos = await _CotacaoPresist.GetAllSolicitacaoProdutoByIdAsync(SolicitacaoId);

                foreach (SolicitacaoProduto prod in solicitacaoProdutos)
                {
                    ItemCotacao itemCot = new ItemCotacao();
                    itemCot.IdSolicitacaoProduto = prod.Id;
                    itemCot.IdProduto = prod.Produto_Id;
                    itemCot.QtdeProduto = prod.QtdeProduto;
                    itemCot.cotacaoId = Cotacao.Id;
                    itemCot.TotalItem= 0.0;
                    itemCot.PrecoUnit = 0.0;
                    FGeralPersist.Add<ItemCotacao>(itemCot);
                    salvar = await FGeralPersist.SaveChangesAsync();
                }

                if (salvar)
                {
                    return await _CotacaoPresist.GetCotacaoByIdAsync(CotacaoId);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cotacao> EnviarOfertaAsync(int idCot, EnviarOfertaDto model)
        {
            var cotacao = await _CotacaoPresist.GetCotacaoByIdAsync(idCot);
            var itensCots = await _CotacaoPresist.GetAllItemCotacaoByIdCotAsync(idCot);
            if (cotacao == null) return null;

            var frete = Convert.ToDouble(model.Frete);
            var data = model.DataEntrega.ToString("dd/MM/yyyy");
            cotacao.DataEntrega = data;
            cotacao.status = 3;
            cotacao.Total = CalcTotalAsync(itensCots);
            cotacao.Total += frete;
            cotacao.Frete =  "R$" + model.Frete;

            FGeralPersist.Update<Cotacao>(cotacao);


            
            if (await FGeralPersist.SaveChangesAsync())
            {
                return await _CotacaoPresist.GetCotacaoByIdAsync(idCot);
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

            var cotacaoVencedora = await _CotacaoPresist.GetCotacaoByIdAsync(id);
            cotacaoVencedora.FornecedorGanhadorId = cotacaoVencedora.fornecedorId;

            FGeralPersist.Update<Cotacao>(cotacaoVencedora);


         
            if (await FGeralPersist.SaveChangesAsync())
            {
                return await _CotacaoPresist.GetCotacaoByIdAsync(idsol);
            }
            return null;


        }


        public double CalcTotalAsync(ItemCotacao[] itensCots)
        {
            double Total = 0;
            foreach (ItemCotacao item in itensCots)
            {
                Total += item.QtdeProduto * item.PrecoUnit;
            }

            return Total;
        }


        public async Task<ItemCotacao> EnviarPrecooAsync(int id, Enviapreco model)
        {
            var Itemcotacao = await _CotacaoPresist.GetAllItemCotacaoByIdAsync(id);
            if (Itemcotacao == null) return null;

            Itemcotacao.PrecoUnit = model.preco;
            Itemcotacao.TotalItem = model.total;

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
        public async Task<Cotacao> CreateCotacao(int SolicitacaoId, CotacaoDto model)
        {
            try
            {
                var solicitacao = await _CotacaoPresist.GetAllSolicitacaoByIdAsync(SolicitacaoId);

                Cotacao = new Cotacao();
                Cotacao.Id = model.Id;
                var prazo = model.PrazoOfertas.ToString("dd/MM/yyyy");
                Cotacao.PrazoOfertas = prazo;
                var dataHoje= DateTime.Today;
                var DataTirar = model.PrazoOfertas.Subtract(dataHoje);
                Cotacao.prazoDias = Convert.ToInt32(DataTirar.TotalDays);
                var dataEmissao = model.DataEmissaoCotacao.ToString("dd/MM/yyyy");
                Cotacao.DataEmissaoCotacao = dataEmissao;
                Cotacao.SolicitacaoId = SolicitacaoId;
                Cotacao.status = model.status;
                Cotacao.fornecedorId = model.fornecedorId;
                Cotacao.CotadorId = model.CotadorId;
                Cotacao.FrmPagamento = model.FrmPagamento;
                Cotacao.Parcelas = model.Parcelas;


                FGeralPersist.Add<Cotacao>(Cotacao);

                if (await FGeralPersist.SaveChangesAsync())
                {
                    await AddCotacaoProduto(Cotacao.Id, SolicitacaoId);
                    return await _CotacaoPresist.GetCotacaoByIdAsync(Cotacao.Id);
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
                var cotacao = await _CotacaoPresist.GetCotacaoByIdAsync(CotacaoId);
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

        public async Task<Cotacao[]> GetAllCotacaobyDataAsync(string DataCriacao)
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
                var cotacaos = await _CotacaoPresist.GetCotacaoByIdAsync(CotacaoId);
                if (cotacaos == null) return null;
                return cotacaos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Cotacao[]> GetCotacaobyFornecedorAsync(int FornecedorId)
        {
            try
            {
                var cotacaos = await _CotacaoPresist.GetFornecedorPorCotacaoByIdAsync(FornecedorId);
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
                var dataE = model.DataEmissaoCotacao.ToString("dd/MM/yyyy");
                cotacao.DataEmissaoCotacao = dataE;
                var dataP = model.PrazoOfertas.ToString("dd/MM/yyyy");
                cotacao.PrazoOfertas = dataP;

                if (cotacao.status != 1)
                {
                    FGeralPersist.Update<Cotacao>(cotacao);
                    if (await FGeralPersist.SaveChangesAsync())
                    {
                        return await _CotacaoPresist.GetCotacaoByIdAsync(model.Id);
                    }
                }
                return null;
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

            bool isFirst = true;
            double menorPreco = 0;
            DateTime? menorData = null;
            int idPrice = 0;
            int idDate = 0;


            foreach (Cotacao cotacao in cotacoes)
            {
                if (isFirst)
                {
                    menorPreco = cotacao.Total;
                    idPrice = cotacao.Id;
                    menorData = DateTime.Parse(cotacao.DataEntrega);
                    idDate = cotacao.Id;
                    isFirst = false;
                }

                if (cotacao.Total < menorPreco)
                {
                    menorPreco = cotacao.Total;
                    idPrice = cotacao.Id;
                }

                if (DateTime.Parse(cotacao.DataEntrega) < menorData)
                {
                    menorData = DateTime.Parse(cotacao.DataEntrega); 
                    idDate = cotacao.Id;
                }
            }
            if (idDate == idPrice)
            {
                Cotacao CotacaoRetorno = cotacoes.Where((c) => c.Id == idDate).FirstOrDefault();
                model.FornecedorIdeal = CotacaoRetorno.fornecedorId;
            }
            else
            {
                var CotacaoMelhorData = cotacoes.Where((c) => c.Id == idDate).FirstOrDefault();
                model.FornecedorMenorData = CotacaoMelhorData.fornecedorId;
                var CotacaoMelhroPrice = cotacoes.Where((c) => c.Id == idPrice).FirstOrDefault();
                model.FornecedorMenorPreco = CotacaoMelhroPrice.fornecedorId;
            }

            return model;
        }

        public async Task<Cotacao> CotacaoVencedora(int idCot)
        {
            try
            {
                var CotacaoIdeal = await _CotacaoPresist.GetCotacaoByIdAsync(idCot);
                CotacaoIdeal.FornecedorGanhadorId = CotacaoIdeal.fornecedorId;
                CotacaoIdeal.status = 3;

                FGeralPersist.Update<Cotacao>(CotacaoIdeal);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await _CotacaoPresist.GetCotacaoByIdAsync(idCot);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fornecedor[]> FornecedorMaioresRankingAsync(int id)
        {
            var fornecedorMaiorRanking = await _CotacaoPresist.GetFornecedorGanhadorAsync(id);

            bool salvar = false;
            foreach (var LForn in fornecedorMaiorRanking)
            {
                LForn.Posicao= fornecedorMaiorRanking.ToList().IndexOf(LForn);
                LForn.Posicao += 1;
                FGeralPersist.Update<Fornecedor>(LForn);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    salvar = true;

                }
                else
                {
                    salvar = false;
                }
            }

            if (salvar)
            {
                return await _CotacaoPresist.GetFornecedorGanhadorAsync(id);
            }
           
            return null;
        }


    

        public async Task<bool> EnviarEmail(int id)
        {
            try
            {
                var fornecedor = await _CotacaoPresist.getEmailFornecedor(id);
                // Estancia da Classe de Mensagem
                MailMessage _mailMessage = new MailMessage();
                // Remetente
                _mailMessage.From = new MailAddress("goodplacecompras@gmail.com");

                // Destinatario seta no metodo abaixo

                //Contrói o MailMessage
                _mailMessage.CC.Add(fornecedor.Email);
                _mailMessage.Subject = "Good Place ";
                _mailMessage.IsBodyHtml = true;
                _mailMessage.Body = "<p>Bem vindo ao Good Place!</p><p>Parabens Forncedor!</p>Informamos que voce foi selecionado participar de uma cotação, entre no sistema Good Place e visualize a cotação enviada para você</p> <p>Esperamos que a nossa parceria seja duradoura e que nosso trabalho sempre corresponda com as expectativas! </p>";



                //CONFIGURAÇÃO COM PORTA
                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                //CONFIGURAÇÃO SEM PORTA
                // SmtpClient _smtpClient = new SmtpClient(UtilRsource.ConfigSmtp);

                // Credencial para envio por SMTP Seguro (Quando o servidor exige autenticação)
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential("goodplacecompras@gmail.com", "Tcc123456");

                _smtpClient.EnableSsl = true;

                _smtpClient.Send(_mailMessage);

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Cotacao[]> GetCotacaoPorFornecedorIDAsync()
        {
            throw new NotImplementedException();
        }
    }
}
