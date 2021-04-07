import { user } from './../models/user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable(
  // {providedIn: 'root'}
  )
export class UserService {
  constructor(private http: HttpClient) {}

  public baseURL = 'https://localhost:5001/User';

  // tslint:disable-next-line: typedef
 public RegisterUser(user:user) : Observable<user>{
   debugger
    return this.http.post<user>(`${this.baseURL}/Registrar`,user);
  }

  public login(email: string, senha: string) {
    debugger
     return this.http.post<string>(`${this.baseURL}/login`,{email, senha});
   }

   public RecuperarSenha( id: number,email: string, user: user) {
    debugger
     return this.http.put<user>(`${this.baseURL}/login`,{id, email, user});
   }
   public AlterarSenha( id: number,email: string, user: user) {
    debugger
     return this.http.put<user>(`${this.baseURL}/login`,{id, email, user});
   }

   getUserById(id: number): Observable<user>{
    return this.http.get<user>(`${this.baseURL}/${id}/`);
  }
  getUser(){
    return this.http.get(`${this.baseURL}`);
 }



 
}
