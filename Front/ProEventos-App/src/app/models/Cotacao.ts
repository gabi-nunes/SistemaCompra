import { Pedido } from './Pedido';
import { Solicitacao } from './Solicitacao';

export interface Cotacao {
  Id: number;
  PrazoCotacao: Date;
  SolicitacaoId: number;
  Solicitacao: Solicitacao;
  Frete: number;
  status: number;
  FrmPagamento: number;
  PrazoOferta: Date;
  Parcelas: number;
  FornecedorGanhadorId: number;
  Total: number;
  PedidoId: number;
  StatusCotac: number;
   Pedido: Pedido[];
  // fornecedorId: number;
  // fornecedorid: Fornecedor;
 // ItensCotacao: IEnumerable<ItemCotacao>;
}
