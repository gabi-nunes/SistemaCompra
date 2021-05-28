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
    public class PedidoService : IPedidoService
    {

        private readonly IGeralPersist FGeralPersist;
        private readonly IpedidoPersist _pedidoPresist;

        public PedidoService(IpedidoPersist pedidoPresist, IGeralPersist geral)
        {
            _pedidoPresist = pedidoPresist;
            FGeralPersist = geral;
        }

        public async Task<ItemPedido[]> AddItemPedido(int PedidoId)
        {
            try
            {
                var pedido = await _pedidoPresist.GetPedidoByIdAsync(PedidoId);
                if (pedido == null) return null;


                var itensCotacao = await _pedidoPresist.GetItemCotacaoByIdCotacaoAsync(pedido.cotacaoId);

                bool salvar = false;

                foreach (ItemCotacao prod in itensCotacao)
                {
                    ItemPedido itemped = new ItemPedido();
                    itemped.PedidoId = pedido.Id;
                    itemped.IdProduto = prod.IdProduto;
                    itemped.QtdeProduto = prod.QtdeProduto;
                    itemped.PrecoUnit = prod.PrecoUnit;
                    itemped.TotalItem= prod.TotalItem;
                    itemped.itemCotacaoId = prod.Id;
                    

                    FGeralPersist.Add<ItemPedido>(itemped);
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
                    return await _pedidoPresist.GetAllItemPedidoByIdPedidosync(PedidoId);
                }
                return null;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw new Exception(ex.Message);
            } 
        }


        public async Task<Pedido> AprovaPedidoAsync(int id, AprovarPedidoDTO model)
        {
            try
            {

                var pedido = await _pedidoPresist.GetPedidoByIdAsync(id);
                if (pedido == null) return null;

                pedido.AprovadorId = model.AprovadorId;
                var data = model.DataAprovacao.ToString("dd/MM/yyyy");
                pedido.DataAprovacao = data;
                pedido.StatusAprov = model.StatusAprov;
                pedido.ObservacaoRejeicao = model.ObservacaoRejeicao;

                if(pedido.StatusAprov == 0)
                {
                    await GerarRankingAsync(pedido.cotacaoId);
                }

                FGeralPersist.Update<Pedido>(pedido);
                if (await FGeralPersist.SaveChangesAsync())
                {
                    return await _pedidoPresist.GetPedidoByIdAsync(id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> CreatePedido(PedidoDto model)
        {

            try
            {
                var cotacao = await _pedidoPresist.GetCotacaooByIdAsync(model.cotacaoId);
                cotacao.status = 3;
                FGeralPersist.Update<Cotacao>(cotacao);
                await FGeralPersist.SaveChangesAsync();

                Pedido pedido = new Pedido();

                var data = model.DataEmissao.ToString("dd/MM/yyyy");
                pedido.DataEmissao = data;
                pedido.Observacao = model.Observacao;
                pedido.cotacaoId = model.cotacaoId;
                pedido.StatusAprov=2;

                FGeralPersist.Add<Pedido>(pedido);

                if (await FGeralPersist.SaveChangesAsync())
                {
                    await AddItemPedido(pedido.Id);
                    var pedidoRetorno = await _pedidoPresist.GetPedidoByIdAsync(pedido.Id);

                    return pedidoRetorno;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletePedido(int PedidoId)
        {

            try
            {
                var pedido = await _pedidoPresist.GetPedidoByIdAsync(PedidoId);
                if (pedido == null) throw new Exception("Pedido para delete não encontrado.");

                FGeralPersist.Delete<Pedido>(pedido);
                return await FGeralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task GerarRankingAsync(int cotacaoId)
        {
            try
            {
                var cotacao = await _pedidoPresist.GetCotacaooByIdAsync(cotacaoId);
                var fornecedor = await _pedidoPresist.GetFornecedorByIdAsync(cotacao.FornecedorGanhadorId);
                int pontuacao = Convert.ToInt32(cotacao.Total * 0.01);
                fornecedor.PontuacaoRanking = pontuacao;

                FGeralPersist.Update<Fornecedor>(fornecedor);
                await FGeralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<Pedido[]> GetAllPedidoAsync()
        {

            try
            {
                var pedido = await _pedidoPresist.GetAllPedidoAsync();
                if (pedido == null) throw new Exception("Pedido não encontrado.");

                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fornecedor> GetFornecedorByIdAsync(int Fornecedorid)
        {
            try
            {
                var fornecedor = await _pedidoPresist.GetFornecedorByIdAsync(Fornecedorid);
                if (fornecedor == null) throw new Exception("Pedido não encontrado.");

                return fornecedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Cotacao> GetCotacaoByIdAsync(int cotacaoid)
        {
            try
            {
                var fornecedor = await _pedidoPresist.GetCotacaooByIdAsync(cotacaoid);
                if (fornecedor == null) throw new Exception("Pedido não encontrado.");

                return fornecedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Fornecedor[]> GetFornecedorGanhadorAsync(int famailiaid)
        {
            try
            {
                var fornecedor = await _pedidoPresist.GetFornecedorGanhadorAsync(famailiaid);
                if (fornecedor == null) throw new Exception("Pedido não encontrado.");

                return fornecedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ItemPedido[]> GetItemPedidoByIdAsync(int id)
        {
            try
            {
                var fornecedor = await _pedidoPresist.GetAllItemPedidoByIdPedidosync(id);
                if (fornecedor == null) throw new Exception("Pedido não encontrado.");

                return fornecedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido[]> GetPedidoByAprovacaoAsync()
        {
            try
            {
                var pedido = await _pedidoPresist.GetPedidoByAprovacaoAsync();
                if (pedido == null) throw new Exception("Pedido não encontrado.");

                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido[]> GetPedidoByDataAdimicapPedidoAsync(string DataAdicao)
        {
            try
            {
                var pedido = await _pedidoPresist.GetPedidoByDataAdimicapPedidoAsync(DataAdicao);
                if (pedido == null) throw new Exception("Pedido não encontrado.");

                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async  Task<Pedido[]> GetPedidoByDataEmissaoPedidoAsync(string DataEmissao)
        {
            try
            {
                var pedido = await _pedidoPresist.GetPedidoByDataEmissaoPedidoAsync(DataEmissao);
                if (pedido == null) throw new Exception("Pedido não encontrado.");

                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido[]> GetPedidoByfornecedorId(int fornecedorId)
        {
            try
            {
                var pedido = await _pedidoPresist.GetPedidoByfornecedorId(fornecedorId);
                if (pedido == null) throw new Exception("Pedido não encontrado.");

                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> GetPedidobyIdAsync(int Id)
        {
            try
            {
                var pedido = await _pedidoPresist.GetPedidoByIdAsync(Id);
                if (pedido == null) throw new Exception("Pedido não encontrado.");

                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido[]> GetPedidoByPendenteAsync()
        {
            try
            {
                var pedido = await _pedidoPresist.GetPedidoByPendenteAsync();
                if (pedido == null) throw new Exception("Pedido não encontrado.");

                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido[]> GetPedidoByRejeitasAsync()
        {
            try
            {
                var pedido = await _pedidoPresist.GetPedidoByRejeitasAsync();
                if (pedido == null) throw new Exception("Pedido não encontrado.");

                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fornecedor[]> GetvisualizarRankingAsync(int FamiliaProdutoid)
        {
            try
            {
                var fornecedores= await _pedidoPresist.GetvisualizarRankingAsync(FamiliaProdutoid);
                if (fornecedores == null) throw new Exception("Fornecedor não encontrado.");

                return fornecedores;
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
                int idLast;
                var solicitacao = await _pedidoPresist.GetIdLast();
                if (solicitacao == null) throw new Exception("Última Solicitacao não encontrada.");

                idLast = solicitacao.Id;
                return idLast;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<double> valorMaximo(double valor)
        {
            var pedido = await _pedidoPresist.GetAllPedidoAsync();

            foreach(Pedido item in pedido)
            {
                item.valorMaximo = valor;

                FGeralPersist.Update<Pedido>(item);
                await FGeralPersist.SaveChangesAsync();
            }
            return valor;

        }

        }
    }

    

