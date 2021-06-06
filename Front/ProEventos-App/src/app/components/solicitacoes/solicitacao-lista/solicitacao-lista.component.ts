import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Solicitacao } from 'src/app/models/Solicitacao';
import { user } from 'src/app/models/user';
import { SolicitacaoService } from 'src/app/services/solicitacao.service';

@Component({
  selector: 'app-solicitacao-lista',
  templateUrl: './solicitacao-lista.component.html',
  styleUrls: ['./solicitacao-lista.component.scss']
})
export class SolicitacaoListaComponent implements OnInit {

  modalRefAprovacao = {} as BsModalRef;
  modalRef = {} as BsModalRef;
  constructor(
    private solicitacaoService: SolicitacaoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {}

  public solicitacao = {} as Solicitacao;
  public solicitacoes: Solicitacao[] = [];
  public solicitacoesFiltradas: Solicitacao[] = [];
  public imgWidth = 150;
  public imgMargin = 2;
  public imgIsVisible = false;
  solicitacaoId = 0;
  user: user;
  podeAprovar: boolean= false;

  public ngOnInit(): void {
    this.spinner.show();
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.user = JSON.parse(userJson);
    this.isAprovar();
    this.GetSolicitacoes();

  }

  isAprovar(){
    debugger
    if(this.user?.cargo == "Comprador" || this.user?.cargo =="gerente" || this.user?.cargo =="comprador" || this.user.cargo =="Gerente"){
      this.podeAprovar= true;
    }
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

  openModalAprovacao(template: TemplateRef<any>, solicitacao: Solicitacao): void{
    this.solicitacao = solicitacao;
    this.modalRefAprovacao = this.modalService.show(template, {class: 'modal-md modal-dialog-centered'});
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
     case 3:
          resultColor = '#5cb85c';
          break;
      case 4:
            resultColor = '#5cb85c';
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
        resultTooltip = 'Reprovado';
        break;
      case 2:
        resultTooltip = 'Pendente';
        break;
      case 3:
          resultTooltip = 'Aprovado';
          break;
      case 4:
            resultTooltip = 'Aprovado';
            break;
    }
    return resultTooltip;
  }

  onMudouEvento(evento: any): void{
    console.log(evento);
  }
}
