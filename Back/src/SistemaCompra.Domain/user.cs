using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCompra.Domain
{
    public class user
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
    
        public string Name { get; set; }
        public string email { get; set; }
        public string Setor { get; set; }
        public string Senha { get; set; }
        public string Cargo { get; set; }
        
        
        
        
        
        
    }
}