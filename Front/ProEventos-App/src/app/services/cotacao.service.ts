import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cotacao } from '../models/Cotacao';
import { CotacaoDTO } from '../models/CotacaoDTO';
import { Fornecedor } from '../models/Fornecedor';
import { EnviarOferta } from './../models/EnviarOferta';
import { identifierModuleUrl } from '@angular/compiler';
import { preco } from '../models/preco';

@Injectable({
  providedIn: 'root'
})
export class CotacaoService {

  constructor(private http: HttpClient) {}
  public baseURL = 'https://localhost:5001/Cotacao';
  // public baseURL = 'https://localhost:44358/Cotacao';

  getCotacoes(): Observable<Cotacao[]>{
    return this.http.get<Cotacao[]>(this.baseURL);
  }

  getCotacoesBySolicitacao(solicitacaoId: number): Observable<Cotacao[]>{
    return this.http.get<Cotacao[]>(`${this.baseURL}/CotacaoPorSolicitacao/${solicitacaoId}`);
  }

  getCotacaoById(cotacaoId: number): Observable<Cotacao>{
    return this.http.get<Cotacao>(`${this.baseURL}/${cotacaoId}`);
  }

  getFornecedoresRankingByFamProdId(famProdId: number): Observable<Fornecedor[]>{
    return this.http.get<Fornecedor[]>(`${this.baseURL}/FornecedorMaiorRanking/${famProdId}`);
  }

  postRegistrarCotacao(solicitId: number, cotacaoDto: CotacaoDTO): Observable<CotacaoDTO>{
    return this.http.post<CotacaoDTO>(`${this.baseURL}/Registrar/${solicitId}`, cotacaoDto);
  }

  getEscolherCotacaoGanhadora(cotacaoId: number): Observable<any>{
    return this.http.get<any>(`${this.baseURL}/CotacaoGanhadore/${cotacaoId}`);
  }

  getFornecedoresIdeais(solicitId: number): Observable<any>{
    return this.http.get<any>(`${this.baseURL}/FornecedorIdeal/${solicitId}`);
  }

  EnviarPreçoItem(precoUnit: preco): Observable<Cotacao>{
    return this.http.put<any>(`${this.baseURL}/EnviaPrecoPorItem/${precoUnit.itemcotacao}/`, precoUnit);
  }

  totalPorItem(famProdId: number): Observable<Fornecedor[]>{
    return this.http.get<Fornecedor[]>(`${this.baseURL}/FornecedorMaiorRanking/${famProdId}`);
  }

  getTotal(itemCotacao: number): Observable<any>{
    return this.http.get<any>(`${this.baseURL}/RetornarQuantidade/${itemCotacao}`);
  }

  getCotacaoByFornecedor(id:number) : Observable<Cotacao[]>{
    return this.http.get<Cotacao[]>(`${this.baseURL}/CotacaoPorFornecedor/${id}`);
  }
  DeleteCotacao(id:number): Observable<any>{
    return this.http.delete<any>(`${this.baseURL}/${id}`);
  }
  enviar(id: number, enviar: EnviarOferta): Observable<any>{
    return this.http.put<any>(`${this.baseURL}/EnviarOferta/${id}`,enviar );
  }

  getCotacaoPedente() : Observable<Cotacao[]>{
    return this.http.get<Cotacao[]>(`${this.baseURL}/CotacaoPendente`);
  }

  // postcotacao(userId: number, cotacaoDto: cotacaoDTO): Observable<cotacaoDTO>{
  //   return this.http.post<cotacaoDTO>(`${this.baseURL}/Registrar/${userId}`, cotacaoDto);
  // }

  // putcotacao(id: number, cotacaoDto: cotacaoDTO): Observable<cotacaoDTO>{
  //   return this.http.put<cotacaoDTO>(`${this.baseURL}/Atualiza/${id}`, cotacaoDto);
  // }
}
