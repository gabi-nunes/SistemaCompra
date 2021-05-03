import { SolicitacaoProduto } from './SolicitacaoProduto';
import { user } from './user';

export interface Solicitacao {
  id: number;
  user_id: number;
  user: user;
  observacao: string;
  dataNecessidade: Date;
  dataAprovacao: Date;
  dataSolicitacao: Date;
  statusAprovacao: number;
  aprovador: string;
  solicitacaoProdutos: SolicitacaoProduto[];
}

