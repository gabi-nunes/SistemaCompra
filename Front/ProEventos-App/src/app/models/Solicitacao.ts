import { SolicitacaoProduto } from './SolicitacaoProduto';
import { user } from './user';

export interface Solicitacao {
  id: number;
  user_id: number;
  user: user;
  observacao: string;
  observacaoRejeicao: string;
  dataNecessidade: Date;
  dataAprovacao: Date;
  dataSolicitacao: Date;
  statusAprovacao: number;
  idAprovador: number;
  solicitacaoProdutos: SolicitacaoProduto[];
}

