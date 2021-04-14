
import { user } from './../models/user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { Login } from './../models/login';



@Injectable(
  // {providedIn: 'root'}
  )

export class UserService {
  private currentUserSubject: BehaviorSubject<user>;
  public currentUser: Observable<user>;
  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<user>(JSON.parse(localStorage.getItem('currentUser') || '{}'));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public baseURL = 'https://localhost:44358/User';

  // tslint:disable-next-line: typedef
 public RegisterUser(user:user) : Observable<user>{
   debugger
    return this.http.post<user>(`${this.baseURL}/Registrar`,user);
  }
  public AtualizaUser(id: number ,user:user) : Observable<user>{
    debugger
     return this.http.put<user>(`${this.baseURL}/Atualiza/${id}`,user);
   }

  public login(login: Login): Observable<user>{
    debugger

    return this.http.post<user>(`${this.baseURL}/Login/`, login)
    .pipe(map(user => {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        debugger
        return user;
    }));

   }



   public RecuperarSenha(login: Login): Observable<user>{
    debugger
     return this.http.post<user>(`${this.baseURL}/RecuperarSenha/`,login).pipe(take(1));
   }

   public AlterarSenha( login: Login): Observable<user>  {
    debugger
     return this.http.put<user>(`${this.baseURL}/AlterarSenha/`, login);
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

  getUserByEmail(email: string): Observable<user>{
    debugger
    return this.http.get<user>(`${this.baseURL}/email/${email}`);
  }
}






