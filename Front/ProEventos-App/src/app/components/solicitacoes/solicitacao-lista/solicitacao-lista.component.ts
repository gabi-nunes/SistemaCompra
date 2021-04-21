import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Solicitacao } from 'src/app/models/Solicitacao';
import { SolicitacaoService } from 'src/app/services/solicitacao.service';

@Component({
  selector: 'app-solicitacao-lista',
  templateUrl: './solicitacao-lista.component.html',
  styleUrls: ['./solicitacao-lista.component.scss']
})
export class SolicitacaoListaComponent implements OnInit {


  modalRef = {} as BsModalRef;
  constructor(
    private solicitacaoService: SolicitacaoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {}

  public solicitacoes: Solicitacao[] = [];
  public solicitacoesFiltradas: Solicitacao[] = [];
  public imgWidth = 150;
  public imgMargin = 2;
  public imgIsVisible = false;
 /* private gridFilter = 0;

  public get GridFilter(): number{
    return this.gridFilter;
  }
  public set GridFilter(value: number){
    this.gridFilter = value;
    this.solicitacoesFiltradas = this.gridFilter ? this.Filtrar(this.gridFilter) : this.solicitacoes;
  }

  public Filtrar(filter: number): Solicitacao[]{
    filter = filter;
    return this.solicitacoes.filter(
      (solicitacao: any) => solicitacao.id.indexOf(filter) !== -1
      );
  } */

  public ngOnInit(): void {
    this.spinner.show();
    this.GetSolicitacoes();
  }

  public AlteraVisibilidadeImg(): void{
    this.imgIsVisible = !this.imgIsVisible;
  }
  public GetSolicitacoes(): void{
    // tslint:disable-next-line: deprecation
    this.solicitacaoService.getSolicitacoes().subscribe({
      next: (solicitacoesResponse: Solicitacao[]) => {
        this.solicitacoes = solicitacoesResponse,
        this.solicitacoesFiltradas = solicitacoesResponse;
      },
      error: () => {
        this.spinner.hide(),
        this.toastr.error('Erro ao carregar as Solicitações', 'Erro');
      },
      complete: () => this.spinner.hide()
    });
  }

  openModal(template: TemplateRef<any>): void{
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef.hide();
    this.toastr.success('Solicitação deletada com sucesso', 'Deletada');
  }

  decline(): void {
    this.modalRef.hide();
  }

  DetalharSolicitacao(id: number): void{
    this.router.navigate([`solicitacoes/detalhe/${id}`]);
  }
}
