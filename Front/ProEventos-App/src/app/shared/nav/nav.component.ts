import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { user } from 'src/app/models/user';
import { FornecedorService } from 'src/app/services/fornecedor.service';

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
  constructor(private router: Router, private userService: UserService, private fornecedorService: FornecedorService) { }


  ngOnInit(): void {
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.usuario = JSON.parse(userJson);
    this.userService.loginMudou.subscribe(user => {
      debugger;
      this.usuario = user;
      this.isUserByEmail();
      this.Validar();
    });

    this.fornecedorService.loginMudou.subscribe(forn => {
      debugger;
      this.usuario = forn;
      this.isUserByEmail();
      this.Validar();
    });
    this.isUserByEmail();
    this.Validar();

  }

  isUserByEmail(): void{
    this.userService.getIsUserByEmail(this.usuario.email).subscribe(
      (result: any) => {
        this.isUser = result;
        this.isFornecedor = !result;
      }
    );
  }

  Validar(){
    this.isUsuario = this.usuario.cargo === 'Solicitante';
    this.isComprador = this.usuario.cargo === 'Comprador';
    this.isGerente = this.usuario.cargo === 'Gerente';
  }

  showMenu(): boolean{
    return (this.router.url !== '/user/login') && (this.router.url !== '/user/recuperar');
  }

  reload(): void{
    debugger;
    this.ngOnInit();
  }

  public showHome(): void{
    if(this.isUser){
      this.router.navigate([`/dashboard`]);
    }
  }
}
