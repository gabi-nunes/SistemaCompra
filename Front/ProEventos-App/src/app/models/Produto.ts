import { FamiliaProduto } from './FamiliaProduto';

export interface Produto {

  id: number;
  descricao: string;
  unidMedida: string;
  familiaProdId: number;
  familiaProduto: FamiliaProduto;
}
