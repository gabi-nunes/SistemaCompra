import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Produto } from '../models/Produto';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {

constructor(private http: HttpClient) { }
  public baseURL = 'https://localhost:5001/Produto';
  //public baseURL = 'https://localhost:44358/Produto';

  getProdutos(): Observable<Produto[]>{
    return this.http.get<Produto[]>(this.baseURL);
  }
  getProdutoById(id: number): Observable<Produto>{
    return this.http.get<Produto>(`${this.baseURL}/${id}`);
  }
  getProdutosByFamiliaProdId(familiaId: number): Observable<Produto[]>{
    return this.http.get<Produto[]>(`${this.baseURL}/FamiProduto/${familiaId}`);
  }


}
