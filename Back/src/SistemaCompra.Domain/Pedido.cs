using System;
using System.Collections.Generic;

namespace SistemaCompra.Domain
{
    public class Pedido
    {
        public int Id { get; set; }
        public int StatusAprov { get; set; }
        public DateTime DataEmissao { get; set; }
        public int AprovadorId { get; set; }
        public DateTime DataAprovacao { get; set; }
        public string Observacao { get; set; }
        public string ObservacaoRejeicao { get; set; }
        public int cotacaoId { get; set; }
        public Cotacao cotacao { get; set; }
        public IEnumerable<ItemPedido> itensPedidos { get; set; }



    }
}