import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {
  constructor(private router: Router, private actRouter: ActivatedRoute) { }
  @Input() titulo = 'Dashboard';
  @Input() subtitulo = '';
  @Input() icone = '';

  public get mostraBtnListar(): boolean{
    return !this.router.url.includes('lista') && !this.router.url.includes('dashboard') && !this.router.url.includes('user') && !this.router.url.includes('produtos') && !this.router.url.includes('alterarSenha');
  }

  ngOnInit(): void {
  }

  Listar(): void{
    if(this.router.url.includes('areaFornecedor')){
      this.router.navigate([`/areaFornecedor/listaCotacao`]);
      return;
    }
    this.router.navigate([`/${this.titulo.toLocaleLowerCase().replace('cotações', 'cotacoes')}/lista`]);
  }

  ShowMenu(): boolean{
    return this.router.url !== '/user/login' && this.router.url !== '/user/recuperar';
  }
}
