import { Cotacao } from './Cotacao';

export interface Solicitacao {
   Id: number;
     // SolicitacaoProdutoId: number;
   UserId: number;
   // User: user;
   Observacao: string;
   DataNecessidade: Date;
   DataAprovacao: Date;
   DataSolicitacao: Date;
   StatusAprovacao: number;
   Aprovador: string;
   CotacaoId: number;
   Cotacao: Cotacao;
   // SolicitaoProdutos: IEnumerable<SolicitacaoProduto>;
}
