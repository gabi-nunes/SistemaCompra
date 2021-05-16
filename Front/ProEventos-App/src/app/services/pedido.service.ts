import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pedido } from '../models/Pedido';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {

  constructor(private http: HttpClient) {
  }
  public baseURL = 'https://localhost:5001/Pedido';

  getPedidos(): Observable<Pedido[]>{
    return this.http.get<Pedido[]>(this.baseURL);
  }
  getPedidoById(id: number): Observable<Pedido>{
    return this.http.get<Pedido>(`${this.baseURL}/${id}`);
  }
  postPedido(pedido: Pedido): Observable<Pedido>{
    return this.http.post<Pedido>(`${this.baseURL}/Registrar`, pedido);
  }
  deletePedido(id: number): Observable<any>{
    return this.http.delete(`${this.baseURL}/${id}`);
  }

}
