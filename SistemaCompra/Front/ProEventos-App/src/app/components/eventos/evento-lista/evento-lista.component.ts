import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from 'src/app/models/Evento';
import { EventoService } from 'src/app/services/evento.service';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {
  modalRef = {} as BsModalRef;
  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {}

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
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

  public Filtrar(filter: string): Evento[]{
    filter = filter.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filter) !== -1 || evento.local.toLocaleLowerCase().indexOf(filter) !== -1

      );
  }

  public ngOnInit(): void {
    this.spinner.show();
    this.GetEventos();
  }

  public AlteraVisibilidadeImg(): void{
    this.imgIsVisible = !this.imgIsVisible;
  }
  public GetEventos(): void{
    // tslint:disable-next-line: deprecation
    this.eventoService.getEventos().subscribe({
      next: (eventosResponse: Evento[]) => {
        this.eventos = eventosResponse,
        this.eventosFiltrados = eventosResponse;
      },
      error: () => {
        this.spinner.hide(),
        this.toastr.error('Erro ao carregar os Eventos', 'Erro');
      },
      complete: () => this.spinner.hide()
    });
  }

  openModal(template: TemplateRef<any>): void{
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef.hide();
    this.toastr.success('Evento deletado com Sucesso', 'Deletado');
  }

  decline(): void {
    this.modalRef.hide();
  }

  DetalharEvento(id: number): void{
    this.router.navigate([`eventos/detalhe/${id}`]);
  }

}
