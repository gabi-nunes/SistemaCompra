using System.Collections.Generic;

namespace SistemaCompra.Domain
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public int InscricaoMunicipal { get; set; }
        public int InscricaoEstadual { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public int PontuacaoRanking { get; set; }
        public int FamiliaProdutoId { get; set; }
        public string Senha { get; set; }
        public FamiliaProduto FamiliaProduto { get; set; }
        public IEnumerable<Cotacao> Cotacoes { get; set; }

    }
}