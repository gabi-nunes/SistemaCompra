import { EnviarOferta } from './../models/EnviarOferta';
import { HttpClient } from '@angular/common/http';
import { identifierModuleUrl } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cotacao } from '../models/Cotacao';
import { Fornecedor } from '../models/Fornecedor';
import { preco } from '../models/preco';

@Injectable({
  providedIn: 'root'
})
export class CotacaoService {

  constructor(private http: HttpClient) {}
  public baseURL = 'https://localhost:5001/Cotacao';

  getCotacaoById(cotacaoId: number): Observable<Cotacao>{
    return this.http.get<Cotacao>(`${this.baseURL}/Id/${cotacaoId}`);
  }
  EnviarPreçoItem(precoUnit: preco): Observable<Cotacao>{
    return this.http.put<any>(`${this.baseURL}/EnviaPrecoPorItem/${precoUnit.itemcotacao}/`, precoUnit);
  }

  totalPorItem(famProdId: number): Observable<Fornecedor[]>{
    return this.http.get<Fornecedor[]>(`${this.baseURL}/FornecedorMaiorRanking/${famProdId}`);
  }

  getTotalitem(itemCotacao: number): Observable<any>{
    return this.http.get<any>(`${this.baseURL}/RetornarQuantidadeporItem/${itemCotacao}`);
  }
  getTotal(itemCotacao: number): Observable<any>{
    return this.http.get<any>(`${this.baseURL}/RetornarQuantidade/${itemCotacao}`);
  }

  getCotacaoByFornecedor(id:number) : Observable<Cotacao[]>{
    return this.http.get<Cotacao[]>(`${this.baseURL}/CotacaoPorFornecedor/${id}`);
  }
  DeleteCotacao(id:number): Observable<any>{
    return this.http.delete<any>(`${this.baseURL}/${id}`);
  }
  enviar(id: number, enviar: EnviarOferta): Observable<any>{
    return this.http.put<any>(`${this.baseURL}/EnviarOferta/${id}`,enviar );
  }

  // postcotacao(userId: number, cotacaoDto: cotacaoDTO): Observable<cotacaoDTO>{
  //   return this.http.post<cotacaoDTO>(`${this.baseURL}/Registrar/${userId}`, cotacaoDto);
  // }

  // putcotacao(id: number, cotacaoDto: cotacaoDTO): Observable<cotacaoDTO>{
  //   return this.http.put<cotacaoDTO>(`${this.baseURL}/Atualiza/${id}`, cotacaoDto);
  // }
}
