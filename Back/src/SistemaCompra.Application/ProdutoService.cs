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
    public class ProdutoService : IProdutoService
    {
        private readonly IGeralPersist FGeralPersist;
        private readonly IProdutoPersist _produtoPresist;
        
        public ProdutoService(IProdutoPersist produtoPresist, IGeralPersist geral)
        {
            _produtoPresist = produtoPresist;
            FGeralPersist = geral;
            
        }

        public async  Task<Produto[]> GetAllProdutoAsync()
        {
            try
            {
                var produto = await _produtoPresist.GetAllProdutoAsync();
                if (produto == null) return null;
                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Produto> GetAllProdutoByIdAsync(int id)
        {
            try
            {
                var produto = await _produtoPresist.GetAllProdutoByIdAsync(id);
                if (produto == null) return null;
                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Produto> GetByDescricaoAsync(string desc)
        {
            try
            {
                var produto = await _produtoPresist.GetProdutoByDescricaoAsync(desc);
                if (produto == null) return null;
                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Produto[]> GetProdutobyFamilia(string desc)
        {
            try
            {
                var Fami = await _produtoPresist.GetProdutobyDesFamiliaProduto(desc);
                if (Fami == null) return null;
                var produto = await _produtoPresist.GetProdutobyFamilia(Fami.Id);
                if (produto == null) return null;
                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Produto[]> GetProdutobyFamiliaId(int idFamilia)
        {
            try
            {
                var produto = await _produtoPresist.GetProdutobyFamilia(idFamilia);
                if (produto == null) return null;
                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
