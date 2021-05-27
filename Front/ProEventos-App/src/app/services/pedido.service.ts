import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
<<<<<<< HEAD
=======
import { Cotacao } from '../models/Cotacao';
import { Fornecedor } from '../models/Fornecedor';
import { ItemPedido } from '../models/ItemPedido';
import { Pedido } from '../models/Pedido';
>>>>>>> master

@Injectable({
  providedIn: 'root'
})
export class PedidoService {

<<<<<<< HEAD
public baseURL = 'https://localhost:5001/Pedido';

constructor(private http: HttpClient) { }
  postRegistrarPedido(pedidoDto: any): Observable<any>{
    return this.http.post<any>(`${this.baseURL}/Registrar`, pedidoDto);
  }
=======
  constructor(private http: HttpClient) {
  }
  public baseURL = 'https://localhost:5001/Pedido';

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

>>>>>>> master
}
