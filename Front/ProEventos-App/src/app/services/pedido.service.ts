import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cotacao } from '../models/Cotacao';
import { Fornecedor } from '../models/Fornecedor';
import { ItemPedido } from '../models/ItemPedido';
import { Pedido } from '../models/Pedido';
import { PedidoDTO } from '../models/PedidoDTO';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {

public baseURL = 'https://localhost:5001/Pedido';

constructor(private http: HttpClient) { }
  postRegistrarPedido(pedidoDto: any): Observable<any>{
    return this.http.post<any>(`${this.baseURL}/Registrar`, pedidoDto);
  }
  getPedidos(): Observable<Pedido[]>{
    return this.http.get<Pedido[]>(this.baseURL);
  }
  getPendetes(): Observable<Pedido[]>{
    return this.http.get<Pedido[]>(`${this.baseURL}/Pendente`);
  }
  getPedidoById(id: number): Observable<Pedido>{
    return this.http.get<Pedido>(`${this.baseURL}/Id/${id}`);
  }

  getCotacaoById(id: number): Observable<Cotacao>{
    return this.http.get<Cotacao>(`${this.baseURL}/Cotacao/${id}`);
  }

  putAlteraStatusPedido(id: number, pedidoDto: any): Observable<PedidoDTO>{
    return this.http.put<any>(`${this.baseURL}/AlterarStatus/${id}`, pedidoDto);
  }

  getFornecedorById(id: number): Observable<Fornecedor>{
    return this.http.get<Fornecedor>(`${this.baseURL}/Fornecedor/${id}`);
  }
  getPedidoByIdFornecedor(fornecedorid: number): Observable<Pedido[]>{
    return this.http.get<Pedido[]>(`${this.baseURL}/PedidoPorFornecedorId/${fornecedorid}`);
  }
  getItemPedidoById(id: number): Observable<ItemPedido[]>{
    return this.http.get<ItemPedido[]>(`${this.baseURL}/ItemPedido/${id}`);
  }
  postPedido(pedido: Pedido): Observable<Pedido>{
    return this.http.post<Pedido>(`${this.baseURL}/Registrar`, pedido);
  }
  deletePedido(id: number): Observable<any>{
    return this.http.delete(`${this.baseURL}/${id}`);
  }

 passarValorMaximo(valor: number): Observable<any>{
    return this.http.get(`${this.baseURL}/ValorMaximo/${valor}`);
  }

}
