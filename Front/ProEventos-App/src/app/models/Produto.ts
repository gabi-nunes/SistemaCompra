import { FamiliaProduto } from "./FamiliaProduto";
import { SolicitacaoProduto } from "./SolicitacaoProduto";

export class Produto {
   id: number;
   descricao: string;
   unidMedida: string;
   familiaProdutoId: number;
   familiaProduto: FamiliaProduto;
   solicitacaoProdutos: SolicitacaoProduto[];
}
