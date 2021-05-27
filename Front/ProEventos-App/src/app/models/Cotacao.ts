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
<<<<<<< HEAD
  prazoOfertas: Date;
=======
  prazoOfertas: any;
>>>>>>> master
  parcelas: number;
  fornecedorGanhadorId: number;
  total: number;
  pedidoId: number;
  pedido: Pedido[];
  prazodiaBool: boolean;
  fornecedorId: number;
  fornecedor: Fornecedor;
  itensCotacao: ItemCotacao[];
<<<<<<< HEAD
  isCollapsed: boolean;
  dataEntrega: Date;
=======











>>>>>>> master
}
