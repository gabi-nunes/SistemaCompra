import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Solicitacao } from '../models/Solicitacao';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(private http: HttpClient) {}
  public baseURL = 'https://localhost:5001/Dashboard';

 // getSolicitacaobyStatus(): Observable<Solicitacao[]>{
 //   return this.http.get<Solicitacao[]>(this.baseURL)/{StatusAprovacao};
 // }

}
