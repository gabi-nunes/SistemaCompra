import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  constructor(private http: HttpClient) { }

  public eventos: any = [];
  public eventosFiltrados: any = [];
  public imgWidth = 150;
  public imgMargin = 2;
  public imgIsVisible = false;
  private gridFilter = '';

  public get GridFilter(): string{
    return this.gridFilter;
  }
  public set GridFilter(value: string){
    this.gridFilter = value;
    this.eventosFiltrados = this.gridFilter ? this.Filtrar(this.gridFilter) : this.eventos;
  }

public Filtrar(filter: string): any{
  filter = filter.toLocaleLowerCase();
  return this.eventos.filter(
    (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filter) !== -1 || evento.local.toLocaleLowerCase().indexOf(filter) !== -1

    );
}

  ngOnInit(): void {
    this.GetEventos();
  }

  public AlteraVisibilidadeImg(): void{
    this.imgIsVisible = !this.imgIsVisible;
  }
  public GetEventos(): void{
    // tslint:disable-next-line: deprecation
    this.http.get('https://localhost:5001/Eventos').subscribe(
      response => {
        this.eventos = response,
        this.eventosFiltrados = response;
      },
      error => console.log(error)
    );
  }
}
