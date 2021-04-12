namespace SistemaCompra.Domain
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public int IdFamiliaProduto { get; set; }
        public string Cidade { get; set; }
        public int Endereco { get; set; }
        public string Bairro { get; set; }
        public int Numero { get; set; }
        public string? Complemento { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public int InscricaoMunicipal { get; set; }
        public int InscricaoEstadual { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public int PontuacaoRanking { get; set; }
    }
}