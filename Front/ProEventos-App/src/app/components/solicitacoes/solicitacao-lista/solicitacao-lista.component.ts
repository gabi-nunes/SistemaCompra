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
  solicitacaoId = 0;
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

  openModal(template: TemplateRef<any>, solId: number): void{
    this.solicitacaoId = solId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef.hide();
    this.spinner.show();
    this.solicitacaoService.deleteSolicitacao(this.solicitacaoId).subscribe(
      () => {
          this.toastr.success('Solicitação deletada com Sucesso', 'Deletada');
          this.spinner.hide();
          this.GetSolicitacoes();
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Falha ao tentar deletar a Solicitação', 'Erro');
        this.spinner.hide();
      },
      () => {}
    );
  }

  decline(): void {
    this.modalRef.hide();
  }

  DetalharSolicitacao(id: number): void{
    this.router.navigate([`solicitações/detalhe/${id}`]);
  }

  GetColorByStatus(status: number): any{
    let resultColor: any;
    switch (status) {
      case 0:
        resultColor = '#5cb85c';
        break;
      case 1:
        resultColor = '#d9534f';
        break;
      case 2:
        resultColor = '#f0ad4e';
        break;
    }
    return resultColor;
  }

  GetTooltipByStatus(status: number): string{
    let resultTooltip: any;
    switch (status) {
      case 0:
        resultTooltip = 'Aprovado';
        break;
      case 1:
        resultTooltip = 'Recusado';
        break;
      case 2:
        resultTooltip = 'Pendente';
        break;
    }
    return resultTooltip;
  }
}
