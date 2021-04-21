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
        public async Task<FamiliaProduto[]> GetAllProdutoAsync()
        {
            try
            {
                var produto = await _FamiliaProdutoPersist.GetAllprodutoAsync();
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
