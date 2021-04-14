using System.Threading.Tasks;
using SistemaCompra.Domain;

namespace SistemaCompra.Persistence.Contratos
{
    public interface IPalestrantePersist
    {
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos); 
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos); 
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos); 

    }
}