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
    return !this.router.url.includes('lista') && !this.router.url.includes('dashboard') && !this.router.url.includes('produtos');
  }

  ngOnInit(): void {
  }

  Listar(): void{
    debugger;
    this.router.url.includes('lista')
    this.router.navigate([`/${this.titulo.toLocaleLowerCase().replace('cotações', 'cotacoes')}/lista`]);
  }

  ShowMenu(): boolean{
    return this.router.url !== '/user/login' && this.router.url !== '/user/recuperar';
  }
}
