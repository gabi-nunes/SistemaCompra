import { Cotacao } from './Cotacao';
import { ItemPedido } from './ItemPedido';

export interface Pedido {
 id: number;
 statusAprov: number;
 dataEmissao: string;
 aprovador: string;
 dataAprovacao: string;
 observacao: string;
 cotacaoId: number;
 cotacao: Cotacao;
 itensPedidos: ItemPedido[];
}
