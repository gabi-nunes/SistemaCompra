import { Fornecedor } from 'src/app/models/Fornecedor';
import { HttpClient } from '@angular/common/http';
import { Injectable, EventEmitter } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Login } from '../models/login';
import { map, take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class FornecedorService {

    loginMudou = new EventEmitter<Fornecedor>();
    private currentUserSubject: BehaviorSubject<Fornecedor>;
    public currentUser: Observable<Fornecedor>;
    constructor(private http: HttpClient) {
      this.currentUserSubject = new BehaviorSubject<Fornecedor>(JSON.parse(localStorage.getItem('currentUser') || '{}'));
      this.currentUser = this.currentUserSubject.asObservable();
    }

  public baseURL = 'https://localhost:5001/Fornecedor';

  getFornecedores(): Observable<Fornecedor[]>{
    return this.http.get<Fornecedor[]>(this.baseURL);
  }
  getFornecedorById(id: number): Observable<Fornecedor>{
    return this.http.get<Fornecedor>(`${this.baseURL}/${id}`);
  }
  getByEmail(email: string): Observable<Fornecedor>{
    return this.http.get<Fornecedor>(`${this.baseURL}/email/${email}`);
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
  public login(login: Login): Observable<Fornecedor>{
    return this.http.post<Fornecedor>(`${this.baseURL}/Login/`, login)
      .pipe(map(fornecedor => {
          this.loginMudou.emit(fornecedor);
          localStorage.setItem('currentUser', JSON.stringify(fornecedor));
          this.currentUserSubject.next(fornecedor);
          return fornecedor;
    }));
  }

  public RecuperarSenha(login: Login): Observable<Fornecedor>{
    return this.http.post<Fornecedor>(`${this.baseURL}/RecuperarSenha/`, login).pipe(take(1));
  }

  public AlterarSenha( login: Login): Observable<Fornecedor>  {
    return this.http.put<Fornecedor>(`${this.baseURL}/AlterarSenha/`, login);
  }

}
