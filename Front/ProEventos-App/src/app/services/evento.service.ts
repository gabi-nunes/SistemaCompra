import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';

@Injectable(
  // {providedIn: 'root'}
  )
export class EventoService {
  constructor(private http: HttpClient) {
  }
  public baseURL = 'https://localhost:5001/Eventos';

  getEventos(): Observable<Evento[]>{
    return this.http.get<Evento[]>(this.baseURL);
  }
  getEventosByTema(tema: string): Observable<Evento[]>{
    return this.http.get<Evento[]>(`${this.baseURL}/tema/${tema}`);
  }
  getEventoById(id: number): Observable<Evento>{
    return this.http.get<Evento>(`${this.baseURL}/${id}`);
  }
  postEvento(evento: Evento): Observable<Evento>{
    return this.http.post<Evento>(this.baseURL, evento);
  }
  putEvento(id: number, evento: Evento): Observable<Evento>{
    return this.http.put<Evento>(`${this.baseURL}/${id}`, evento.id);
  }
  deleteEvento(id: number): Observable<any>{
    return this.http.delete(`${this.baseURL}/${id}`);
  }
 }

