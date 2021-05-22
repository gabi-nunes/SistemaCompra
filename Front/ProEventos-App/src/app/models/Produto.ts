import { FamiliaProduto } from "./FamiliaProduto";
import { SolicitacaoProduto } from "./SolicitacaoProduto";

export class Produto {
   id: number;
   descricao: string;
   unidMedida: string;
   familiaProdutoId: number;
   descricaoFamilia: string;
   familiaProduto: FamiliaProduto;
   solicitacaoProdutos: SolicitacaoProduto[];
}
