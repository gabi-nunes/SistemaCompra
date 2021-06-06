import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Cotacao } from 'src/app/models/Cotacao';
import { Solicitacao } from 'src/app/models/Solicitacao';
import { CotacaoService } from 'src/app/services/cotacao.service';
import { SolicitacaoService } from 'src/app/services/solicitacao.service';

@Component({
  selector: 'app-cotacoes-lista',
  templateUrl: './cotacoes-lista.component.html',
  styleUrls: ['./cotacoes-lista.component.scss']
})
export class CotacoesListaComponent implements OnInit {

  modalRefAprovacao = {} as BsModalRef;
  modalRef = {} as BsModalRef;
  status: number;
  constructor(
    private cotacaoService: CotacaoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router,
    private solicitacaoService: SolicitacaoService
  ) {}

  // public solicitacao = {} as Solicitacao;
  public solicitacoes: Solicitacao[] = [];
  public cotacoes: Cotacao[] = [];
  public cotacoesFiltradas: Cotacao[] = [];
  cotacaoId = 0;
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
    this.GetCotacoes();
    this.GetSolicitacoesAprovadas();
  }

  public GetCotacoes(): void{
    // tslint:disable-next-line: deprecation
    this.cotacaoService.getCotacoes().subscribe({
      next: (CotacaoResponse: Cotacao[]) => {
        this.cotacoes = CotacaoResponse
        // this.CotacaoFiltradas = CotacaoResponse;
      },
      error: () => {
        this.spinner.hide(),
        this.toastr.error('Erro ao carregar as Solicitações', 'Erro');
      },
      complete: () => this.spinner.hide()
    });
  }

  FilterCotacoesById(solId: number): Cotacao[]{
    debugger;
    const cotacoesSolicitacao = this.cotacoes.filter(c => c.solicitacaoId == solId);
    return cotacoesSolicitacao;
  }

  public GetSolicitacoesAprovadas(): void{
    // tslint:disable-next-line: deprecation
    this.solicitacaoService.getSolicitacoes().subscribe({
      next: (solicitacoesResponse: Solicitacao[]) => {
        this.solicitacoes = solicitacoesResponse;
        this.solicitacoes = this.solicitacoes.filter(s => s.statusAprovacao == 0 || s.statusAprovacao == 3 || s.statusAprovacao == 4);
        // this.solicitacoesFiltradas = solicitacoesResponse;
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

  // openModalAprovacao(template: TemplateRef<any>, solicitacao: Solicitacao): void{
  //   this.solicitacao = solicitacao;
  //   this.modalRefAprovacao = this.modalService.show(template, {class: 'modal-md modal-dialog-centered'});
  // }

  confirm(): void {
    this.modalRef.hide();
    this.spinner.show();
    this.solicitacaoService.deleteSolicitacao(this.solicitacaoId).subscribe(
      () => {
          this.toastr.success('Solicitação deletada com Sucesso', 'Deletada');
          this.spinner.hide();
          this.GetSolicitacoesAprovadas();
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

  DetalharCotacoes(id: number): void{
    this.router.navigate([`cotacoes/detalhe/${id}`]);
  }

  GetColorByStatus(solId: number): any{
    debugger;
    const status = this.GetStatus(solId);
    let statusObj = {color: '', tooltip: ''};
    switch (status) {
      case 1:
        statusObj.color = '#5bc0de';
        statusObj.tooltip = 'Aguardando Ofertas';
        break;
      case 2:
        statusObj.color = '#5cb85c';
        statusObj.tooltip = 'Ofertas Recebidas';
        break;
      case 3:
        statusObj.color = '#d9534f';
        statusObj.tooltip = 'Cotação Encerrada';
        break;
      default:
        statusObj.color = '#f0ad4e';
        statusObj.tooltip  = 'Pendente';
        break;
    }
    return statusObj;
  }

  public GetStatus(solId: number): number{
    debugger;
    const cots = this.FilterCotacoesById(solId);
    if (!cots.length){return -1; }
    if (cots.some(c => c.status === 3)) { return 3; }
    else if (cots.every(c => c.status === 2)) { return 2; }
    else if (cots.some(c => c.status === 1)) { return 1; }
    else if (cots.some(c => c.status === 3)) { return 3; }
    else if (cots.some(c => c.status === 4)) { return 3; }
    return 0;
  }

  getPrazoCotacao(solID: number): string | undefined{
    return this.cotacoes?.find(c => c.id === solID)?.prazoOfertas.toString() ?? "__/__/____";
  }

  onMudouEvento(evento: any): void{
    console.log(evento);
  }

}
