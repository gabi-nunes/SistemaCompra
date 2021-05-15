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
    public class FamiliaProdutoService : IFamiliaProdutoService
    {
        private readonly IGeralPersist FGeralPersist;
        private readonly IFamiliaProdutoPersist _FamiliaProdutoPersist;
        public FamiliaProdutoService(IFamiliaProdutoPersist familiaProdutoPersist, IGeralPersist geral)
        {
            _FamiliaProdutoPersist = familiaProdutoPersist;
            FGeralPersist = geral;
        }
        public async Task<FamiliaProduto[]> GetAllFamiliaProdutoAsync()
        {
            try
            {
                var famProduto = await _FamiliaProdutoPersist.GetAllFamiliaProdutoAsync();
                if (famProduto == null) return null;
                return famProduto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FamiliaProduto> GetFamiliaProdutoByIdAsync(int FamiliaProdId)
        {
            try
            {
                var famProduto = await _FamiliaProdutoPersist.GetFamiliaProdutoByIdAsync(FamiliaProdId);
                if (famProduto == null) return null;
                return famProduto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
