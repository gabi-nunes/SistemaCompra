using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCompra.Domain
{
    public class Cotacao
    {
        public int Id { get; set; }
        public DateTime DataEmissaoCotacao { get; set; }
        public int SolicitacaoId { get; set; }
        public Solicitacao Solicitacao { get; set; }
        public double Frete { get; set; }
        public int status { get; set; }
        public int FrmPagamento { get; set; }
        public DateTime DataEntrega { get; set; }
        public DateTime PrazoOfertas { get; set; }
        public int Parcelas { get; set; }
        public int FornecedorGanhadorId { get; set; }
        public double Total { get; set; }
        public int fornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public IEnumerable<ItemCotacao> ItensCotacao { get; set; }




    }
}

