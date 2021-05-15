import { ItemCotacao } from './ItemCotacao';
import { Pedido } from './Pedido';

export interface ItemPedido {
   id: number;
   idPedido: number;
   idProduto: number;
   qtdeProduto: number;
   precoUnit: number;
   itemCotacaoId: number;
  //  itemCotacao: ItemCotacao;
   pedidoId: number;
   pedido: Pedido;
}
