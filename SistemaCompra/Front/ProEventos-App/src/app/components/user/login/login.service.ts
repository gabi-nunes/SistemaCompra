import { EventEmitter, Injectable } from '@angular/core';

import { Subject } from 'rxjs';
import { user } from 'src/app/models/user';

@Injectable({
  providedIn: 'root'
})
export class loginService {

  usuarioPerfil: user[]=[];

  passandoUser = new EventEmitter<user>()
  static pegandoUser = new EventEmitter<user>()

  constructor() { }

    // Observable string sources
    private userSource = new Subject<user>();


    user$ = this.userSource.asObservable();


    // Service message command
    addUser(usuario: user){
      debugger
      this.usuarioPerfil.push(usuario);
      //passar eventos
      loginService.pegandoUser.emit(usuario);
    }

  getUser(){

  }



}
