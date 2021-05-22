import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Solicitacao } from '../models/Solicitacao';
import { SolicitacaoDTO } from '../models/SolicitacaoDTO';
import { SolicitacaoProdutoDTO } from '../models/SolicitacaoProdutoDTO';

@Injectable(
  // providedIn: 'root'
)
export class SolicitacaoService {

  constructor(private http: HttpClient) {}
  public baseURL = 'https://localhost:5001/Solicitacao';

  getSolicitacoes(): Observable<Solicitacao[]>{
    return this.http.get<Solicitacao[]>(this.baseURL);
  }
  getPendentes(): Observable<Solicitacao[]>{
    return this.http.get<Solicitacao[]>(`${this.baseURL}/AprovacaoPendente`);
  }
  getSolicitacaoById(solicitacaoId: number): Observable<Solicitacao>{
    return this.http.get<Solicitacao>(`${this.baseURL}/${solicitacaoId}`);
  }

  postSolicitacao(userId: number, solicitacaoDto: SolicitacaoDTO): Observable<SolicitacaoDTO>{
    return this.http.post<SolicitacaoDTO>(`${this.baseURL}/Registrar/${userId}`, solicitacaoDto);
  }

  postSolicitacaoProd(solicitacaoId: number, solicitacaoProdDto: SolicitacaoProdutoDTO): Observable<SolicitacaoProdutoDTO>{
    return this.http.post<SolicitacaoProdutoDTO>(`${this.baseURL}/SolicitacaoProd/${solicitacaoId}`, solicitacaoProdDto);
  }

  putSolicitacao(id: number, solicitacaoDto: SolicitacaoDTO): Observable<SolicitacaoDTO>{
    return this.http.put<SolicitacaoDTO>(`${this.baseURL}/Atualiza/${id}`, solicitacaoDto);
  }
  putAlteraStatusSolicitacao(id: number, solicitacaoDto: any): Observable<SolicitacaoDTO>{
    return this.http.put<any>(`${this.baseURL}/AlterarStatus/${id}`, solicitacaoDto);
  }

  putSolicitacaoProd(solicitacaoId: number, solicitacaoProdDto: SolicitacaoProdutoDTO): Observable<SolicitacaoProdutoDTO>{
    return this.http.put<SolicitacaoProdutoDTO>(`${this.baseURL}/AtualizaSolicitacaoProduto/${solicitacaoId}`, solicitacaoProdDto);
  }

  deleteSolicitacao(id: number): Observable<any>{
    return this.http.delete(`${this.baseURL}/${id}`);
  }

  deleteSolicitacaoProduto(solProdId: number): Observable<any>{
    return this.http.delete(`${this.baseURL}/DeleteSolicitacaoProduto/${solProdId}`);
  }

  getLastId(): Observable<any>{
    return this.http.get<number>(`${this.baseURL}/UltimoId`);
  }

}
