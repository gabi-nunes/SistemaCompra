import { UserService } from './../../services/user.service';
import { user } from './../../models/user';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {
  constructor(private router: Router, private actRouter: ActivatedRoute,private userService: UserService) { }
  @Input() titulo = 'Dashboard';
  @Input() subtitulo = '';
  @Input() icone = '';

  isUser: Boolean = false;
  isFornecedor : Boolean= false;
  usuario : any;

  public get mostraBtnListar(): boolean{

    return !this.router.url.includes('lista') && !this.router.url.includes('dashboard') && !this.router.url.includes('user') && !this.router.url.includes('produtos') && !this.router.url.includes('alterarSenha') ;
  }

  ngOnInit(): void {
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.usuario = JSON.parse(userJson);
  }

  Listar(){
    if(this.router.url.includes('areaFornecedor')){
      this.router.navigate([`/areaFornecedor/listaCotacao`]);
      return;
    }
    this.router.navigate([`/${this.titulo.toLocaleLowerCase().replace('cotações', 'cotacoes')}/lista`]);
  }

  isUserByEmail(){
    this.userService.getIsUserByEmail(this.usuario.email).subscribe(
      (result: any) => {
        this.isUser= result;
        this.isFornecedor= !result;
      }
    );
  }

  ShowMenu(): boolean{
    return this.router.url !== '/user/login' && this.router.url !== '/user/recuperar';
  }
}
