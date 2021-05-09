import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Solicitacao } from 'src/app/models/Solicitacao';
import { user } from 'src/app/models/user';
import { SolicitacaoService } from 'src/app/services/solicitacao.service';

@Component({
  selector: 'app-aprovar-solicitacao',
  templateUrl: './aprovar-solicitacao.component.html',
  styleUrls: ['./aprovar-solicitacao.component.scss']
})
export class AprovarSolicitacaoComponent implements OnInit {

  constructor(private solicitacaoService: SolicitacaoService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService,
              private router: Router
              ) {}
  @Input() modal: BsModalRef;
  @Input() solicitacao: Solicitacao;

  @Output() carregaEventos = new EventEmitter();

  objSolicitacaoAprov = {} as any;
  user = {} as user;
  observacaoRejeicao: string;
  ngOnInit(): void {
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.user = JSON.parse(userJson);

    this.objSolicitacaoAprov = {
      dataAprovacao: new Date(),
      statusAprovacao: null,
      idAprovador: this.user.id,
      observacaoRejeicao: null
    };

  }

  public CloseModal(): void{
    this.modal.hide();
  }

  public AprovarSolicitacao(): void{
    this.spinner.show();
    this.objSolicitacaoAprov.statusAprovacao = 0;
    this.objSolicitacaoAprov.observacaoRejeicao = this.observacaoRejeicao;
    debugger;
    this.solicitacaoService.putAlteraStatusSolicitacao(+this.solicitacao.id, this.objSolicitacaoAprov).subscribe(
      () => {
        this.spinner.hide();
        this.modal.hide();
        this.toastr.success('Solicitação Aprovada com Sucesso', 'Aprovada');
        debugger;
        this.router.navigate([`/cotacoes/detalhe/${this.solicitacao.id}`]);
      },
      (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao tentar Aprovar a Solicitacao', 'Erro');
        console.error(error);
      },
      () => {
        this.spinner.hide();
      }
      );
  }
  public ReprovarSolicitacao(): void{
    this.spinner.show();
    this.objSolicitacaoAprov.statusAprovacao = 1;
    this.objSolicitacaoAprov.observacaoRejeicao = this.observacaoRejeicao;
    debugger;
    this.solicitacaoService.putAlteraStatusSolicitacao(+this.solicitacao.id, this.objSolicitacaoAprov).subscribe(
      () => {
        this.spinner.hide();
        this.modal.hide();
        this.toastr.success('Solicitação Reprovada com Sucesso', 'Reprovada');
        this.carregaEventos.emit({RecarregaSolicitacoes: true});
        this.router.navigate([`/solicitações/lista`]);
      },
      (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao tentar Aprovar a Solicitacao', 'Erro');
        console.error(error);
      },
      () => {
        this.spinner.hide();
      }
      );
  }

}
