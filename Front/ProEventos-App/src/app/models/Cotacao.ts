import { Fornecedor } from './Fornecedor';
import { ItemCotacao } from './ItemCotacao';
import { Pedido } from './Pedido';
import { Solicitacao } from './Solicitacao';

export class Cotacao {
  id: number;
  CotadorId: number;
  dataEmissaoCotacao: Date;
  solicitacaoId: number;
  solicitacao: Solicitacao;
  frete: number;
  dataEntrega: Date;
  prazoDias: number;
  status: number;
  frmPagamento: number;
  prazoOfertas: any;
  parcelas: number;
  fornecedorGanhadorId: number;
  total: number;
  pedidoId: number;
  statusCotacao: number;
  pedido: Pedido[];
  prazodiaBool: boolean;
  fornecedorId: number;
  fornecedor: Fornecedor;
  itensCotacao: ItemCotacao[];











}
