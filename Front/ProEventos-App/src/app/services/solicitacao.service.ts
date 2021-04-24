import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Solicitacao } from '../models/Solicitacao';

@Injectable(
  // providedIn: 'root'
)
export class SolicitacaoService {

constructor(private http: HttpClient) {}
public baseURL = 'https://localhost:5001/Solicitacoes';

getSolicitacoes(): Observable<Solicitacao[]>{
  return this.http.get<Solicitacao[]>(this.baseURL);
}

getSolicitacaoById(solicitacaoId: number): Observable<Solicitacao>{
  return this.http.get<Solicitacao>(`${this.baseURL}/${solicitacaoId}`);
}

postSolicitacao(solicitacao: Solicitacao): Observable<Solicitacao>{
  return this.http.post<Solicitacao>(`${this.baseURL}/Registrar`, solicitacao);
}
putSolicitacao(id: number, solicitacao: Solicitacao): Observable<Solicitacao>{
  return this.http.put<Solicitacao>(`${this.baseURL}/Atualiza/${id}`, solicitacao);
}
deleteSolicitacao(id: number): Observable<any>{
  return this.http.delete(`${this.baseURL}/${id}`);
}


}
