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

getSolicitacoesbyId(): Observable<Solicitacao[]>{
  return this.http.get<Solicitacao[]>(`${this.baseURL}/{id}}`);
}


}
