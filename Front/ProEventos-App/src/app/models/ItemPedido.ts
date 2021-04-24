import { ItemCotacao } from './ItemCotacao';
import { Pedido } from './Pedido';

export class ItemPedido {
   Id: number;
   IdPedido: number;
   IdProduto: number;
   QtdeProduto: number;
   PrecoUnit: number;
   itemCotacaoId: number;
   itemCotacao: ItemCotacao;
   PedidoId: number;
   Pedido: Pedido;
}
