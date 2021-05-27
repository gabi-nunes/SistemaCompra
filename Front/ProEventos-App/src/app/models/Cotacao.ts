import { Fornecedor } from './Fornecedor';
import { ItemCotacao } from './ItemCotacao';
import { Pedido } from './Pedido';
import { Solicitacao } from './Solicitacao';

export interface Cotacao {
  id: number;
  CotadorId: number;
  prazoCotacao: Date;
  solicitacaoId: number;
  solicitacao: Solicitacao;
  frete: number;
  status: number;
  frmPagamento: number;
  prazoOfertas: Date;
  parcelas: number;
  fornecedorGanhadorId: number;
  total: number;
  pedidoId: number;
  pedido: Pedido[];
  fornecedorId: number;
  fornecedor: Fornecedor;
  itensCotacao: ItemCotacao[];
  isCollapsed: boolean;
  dataEntrega: Date;
}
