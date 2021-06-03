import { SolicitacaoProduto } from './SolicitacaoProduto';
import { user } from './user';

export interface Solicitacao {
  id: number;
  user_id: number;
  user: user;
  observacao: string;
  observacaoRejeicao: string;
  dataNecessidade: any;
  dataAprovacao: any;
  dataSolicitacao: any;
  statusAprovacao: number;
  idAprovador: number;
  solicitacaoProdutos: SolicitacaoProduto[];
}

