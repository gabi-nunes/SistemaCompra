import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { user } from 'src/app/models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss'],
})
export class NavComponent implements OnInit {
  isCollapsed = true;
  usuario: any;
  isComprador: boolean = false;
  isGerente:boolean = false;
  isUsuario: boolean = false;
  isUser: boolean = false;
  isFornecedor: boolean= false;
  constructor(private router: Router, private userService: UserService) { }


  ngOnInit(): void {
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.usuario= JSON.parse(userJson);
    this.isUserByEmail();
    this.Validar();
  }

  isUserByEmail(){
    this.userService.getIsUserByEmail(this.usuario.email).subscribe(
      (result: any) => {
        this.isUser= result;
        this.isFornecedor= !result;
      }
    );
  }


  Validar(){
    this.isUsuario = this.usuario.cargo === '0';
    this.isComprador = this.usuario.cargo === '1';
    this.isGerente = this.usuario.cargo === '2';
  }

  showMenu(): boolean{
    return (this.router.url !== '/user/login') && (this.router.url !== '/user/recuperar');
  }
}
