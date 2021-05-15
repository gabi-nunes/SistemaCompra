import { Cotacao } from './Cotacao';
import { ItemPedido } from './ItemPedido';
import { SolicitacaoProduto } from './SolicitacaoProduto';

export class ItemCotacao {
   id: number;
   idCotacao: number;
   idSolicitacaoProduto: number;
   solicitacaoProduto: SolicitacaoProduto;
   idProduto: number;
   qtdeProduto: number;
   precoUnit: number;
   cotacaoId: number;
   cotacao: Cotacao;
   itemPedidoId: number;
   itemPedido: ItemPedido;
}
