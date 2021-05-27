import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {

public baseURL = 'https://localhost:5001/Pedido';

constructor(private http: HttpClient) { }
  postRegistrarPedido(pedidoDto: any): Observable<any>{
    return this.http.post<any>(`${this.baseURL}/Registrar`, pedidoDto);
  }
}
