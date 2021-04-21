import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Fornecedor } from '../models/Fornecedor';

@Injectable({
  providedIn: 'root'
})
export class FornecedorService {

  constructor(private http: HttpClient) {
  }
  public baseURL = 'https://localhost:5001/Fornecedor';

  getFornecedores(): Observable<Fornecedor[]>{
    return this.http.get<Fornecedor[]>(this.baseURL);
  }
  getFornecedorById(id: number): Observable<Fornecedor>{
    return this.http.get<Fornecedor>(`${this.baseURL}/${id}`);
  }

  postFornecedor(fornecedor: Fornecedor): Observable<Fornecedor>{
    return this.http.post<Fornecedor>(`${this.baseURL}/Registrar`, fornecedor);
  }
  putFornecedor(id: number, fornecedor: Fornecedor): Observable<Fornecedor>{
    return this.http.put<Fornecedor>(`${this.baseURL}/Atualiza/${id}`, fornecedor);
  }
  deleteFornecedor(id: number): Observable<any>{
    return this.http.delete(`${this.baseURL}/${id}`);
  }

}
