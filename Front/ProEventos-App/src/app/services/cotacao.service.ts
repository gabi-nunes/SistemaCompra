import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cotacao } from '../models/Cotacao';
import { CotacaoDTO } from '../models/CotacaoDTO';
import { Fornecedor } from '../models/Fornecedor';

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

}
