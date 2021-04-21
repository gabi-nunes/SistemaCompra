import { Cotacao } from './Cotacao';

export interface Pedido {
 Id: number;
 StatusAprov: number;
 DataEmissao: Date;
 Aprovador: string;
 DataAprovacao: Date;
 Observacao: string;
 cotacaoId: number;
 cotacao: Cotacao;
 // itensPedidos: IEnumerable<ItemPedido>;
}
