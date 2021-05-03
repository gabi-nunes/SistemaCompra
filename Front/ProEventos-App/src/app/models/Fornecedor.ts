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
  celular: string;
  pontuacaoRanking: number;
  familiaProdutoId: number;
  familiaProduto: FamiliaProduto;

}
