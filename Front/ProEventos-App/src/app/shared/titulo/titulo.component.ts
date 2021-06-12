
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { user } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {
  constructor(private router: Router, private actRouter: ActivatedRoute, private userService: UserService) { }
  @Input() titulo = 'Dashboard';
  @Input() subtitulo = '';
  @Input() icone = '';

  isUser = false;
  isFornecedor = false;
  usuario: user;

  public get mostraBtnListar(): boolean{

    return !this.router.url.includes('lista') && !this.router.url.includes('dashboard') && !this.router.url.includes('user') && !this.router.url.includes('produtos') && !this.router.url.includes('alterarSenha') ;
  }

  ngOnInit(): void {
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.usuario = JSON.parse(userJson);
    this.isUserByEmail();
  }

  Listar(){
    if(this.router.url.includes('areaFornecedor')){
      this.router.navigate([`/areaFornecedor/listaCotacao`]);
      return;
    }
    if(this.router.url.includes('pedidos/detalhe') && this.isFornecedor){
      this.router.navigate([`/areaFornecedor/listaPedido`]);
      return;
    }
    this.router.navigate([`/${this.titulo.toLocaleLowerCase().replace('cotações', 'cotacoes')}/lista`]);
  }

  ShowMenu(): boolean{
    return this.router.url !== '/user/login' && this.router.url !== '/user/recuperar';
  }

  isUserByEmail(){
    this.userService.getIsUserByEmail(this.usuario.email).subscribe(
      (result: any) => {
        this.isUser = result;
        this.isFornecedor = !result;
      }
    );
  }
}
