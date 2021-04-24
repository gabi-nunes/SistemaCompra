import { Fornecedor } from './Fornecedor';
import { ItemCotacao } from './ItemCotacao';
import { Pedido } from './Pedido';
import { Solicitacao } from './Solicitacao';

export interface Cotacao {
  id: number;
  prazoCotacao: Date;
  solicitacaoId: number;
  solicitacao: Solicitacao;
  frete: number;
  status: number;
  frmPagamento: number;
  prazoOferta: Date;
  parcelas: number;
  fornecedorGanhadorId: number;
  total: number;
  pedidoId: number;
  statusCotacao: number;
  pedido: Pedido[];
  fornecedorId: number;
  fornecedor: Fornecedor;
  itensCotacaos: ItemCotacao[];
}
