import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cotacao } from '../models/Cotacao';
import { Fornecedor } from '../models/Fornecedor';

@Injectable({
  providedIn: 'root'
})
export class CotacaoService {

  constructor(private http: HttpClient) {}
  public baseURL = 'https://localhost:5001/Cotacao';

  getCotacaoById(cotacaoId: number): Observable<Cotacao>{
    return this.http.get<Cotacao>(`${this.baseURL}/${cotacaoId}`);
  }

  getFornecedoresRankingByFamProdId(famProdId: number): Observable<Fornecedor[]>{
    return this.http.get<Fornecedor[]>(`${this.baseURL}/FornecedorMaiorRanking/${famProdId}`);
  }

  // postcotacao(userId: number, cotacaoDto: cotacaoDTO): Observable<cotacaoDTO>{
  //   return this.http.post<cotacaoDTO>(`${this.baseURL}/Registrar/${userId}`, cotacaoDto);
  // }

  // putcotacao(id: number, cotacaoDto: cotacaoDTO): Observable<cotacaoDTO>{
  //   return this.http.put<cotacaoDTO>(`${this.baseURL}/Atualiza/${id}`, cotacaoDto);
  // }
}
