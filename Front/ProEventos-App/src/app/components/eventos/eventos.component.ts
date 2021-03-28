import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '../../models/Evento';
import { EventoService } from '../../services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {
  modalRef = {} as BsModalRef;
  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
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

}


