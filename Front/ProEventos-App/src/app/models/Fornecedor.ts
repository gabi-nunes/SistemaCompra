import { FamiliaProduto } from './FamiliaProduto';

export class Fornecedor {
  id: number;
  cnpj: string;
  nome: string;
  cidade: string;
  endereco: number;
  bairro: string;
  numero: number;
  complemento: string;
  estado: string;
  cep: string;
  inscricaoMunicipal: number;
  inscricaoEstadual: number;
  email: string;
  telefone: string;
  senha: string;
  celular: string;
  posicao: number;
  pontuacaoRanking: number;
  familiaProdutoId: number;
  familiaProduto: FamiliaProduto;

}
