import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Produto } from '../models/Produto';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {

  constructor(private http: HttpClient) {}
  public baseURL = 'https://localhost:5001/Produto';

  getProdutos(): Observable<Produto[]>{
    return this.http.get<Produto[]>(this.baseURL);
  }

  getProdutoById(ProdutoId: number): Observable<Produto>{
    return this.http.get<Produto>(`${this.baseURL}/${ProdutoId}`);
  }

}
