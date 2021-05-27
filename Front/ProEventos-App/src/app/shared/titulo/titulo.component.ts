import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {
  constructor(private router: Router) { }
  @Input() titulo = 'Dashboard';
  @Input() subtitulo = '';
  @Input() icone = '';
  @Input() mostraBtnListar = false;

  ngOnInit(): void {
  }

  Listar(): void{
    debugger;
    this.router.navigate([`/${this.titulo.toLocaleLowerCase()}/lista`]);
  }
  // normalize('NFD').replace('/[\u0300-\u036f]/g', '')

  ShowMenu(): boolean{
    return this.router.url !== '/user/login' && this.router.url !== '/user/recuperar';
  }
}
