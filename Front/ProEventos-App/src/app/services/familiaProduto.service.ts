import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FamiliaProduto } from '../models/FamiliaProduto';

@Injectable({
  providedIn: 'root'
})
export class FamiliaProdutoService {

  constructor(private http: HttpClient) { }

  public baseURL = 'https://localhost:5001/FamiliaProduto';
<<<<<<< HEAD
  // public baseURL = 'https://localhost:44358/FamiliaProduto';
=======

>>>>>>> master
  getFamiliaProdutos(): Observable<FamiliaProduto[]>{
    return this.http.get<FamiliaProduto[]>(this.baseURL);
  }
  getFamiliaProdutoById(famProdId: number): Observable<FamiliaProduto>{
    return this.http.get<FamiliaProduto>(`${this.baseURL}/${famProdId}`);
  }
}
