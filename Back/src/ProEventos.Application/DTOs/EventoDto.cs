using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.DTOs
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório"),
          StringLength(50, MinimumLength=3, ErrorMessage = "O nome do tema deve conter entre 3 e 50 caracteres")]
        public string Tema { get; set; }

        [Display(Name = "Quantidade de Pessoas"),
            Range(1, 120000, ErrorMessage = "O campo {0} está fora do range")]
        public int QtdePessoas { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|png|bmp)$", ErrorMessage = "Tipo de imagem in   válida")]
        public string ImagemURL { get; set; }

        [Phone(ErrorMessage = "O campo {0} está com caracteres inválidos")]
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "O campo {0} necessita ser um e-mail válido")]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}