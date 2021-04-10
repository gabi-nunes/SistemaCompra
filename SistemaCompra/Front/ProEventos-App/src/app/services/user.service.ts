import { user } from './../models/user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';


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
  public AtualizaUser(id: number ,user:user) : Observable<user>{
    debugger
     return this.http.put<user>(`${this.baseURL}/Atualiza/${id}`,user);
   }

  public login(email: string, senha: string): Observable<any>  {
    debugger

     return this.http.post<any>(`${this.baseURL}/Login/`, {email, senha}).pipe(take(1));;
   }

   public RecuperarSenha(id: string,email: string, user: user) {
    debugger
     return this.http.put<user>(`${this.baseURL}/login/`,{id, email, user});
   }
   public AlterarSenha( id: number,email: string, user: user) {
    debugger
     return this.http.put<user>(`${this.baseURL}/login/`,{id, email, user});'    '
   }

   getUserById(id: number): Observable<user>{
    return this.http.get<user>(`${this.baseURL}/${id}/`);
  }
  public getUser(): Observable<user[]> {
    return this.http.get<user[]>(this.baseURL).pipe(take(1));
  }

  public deleteUser(id: number): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }
}





