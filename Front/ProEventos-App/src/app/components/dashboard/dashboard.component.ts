import { CotacaoService } from './../../services/cotacao.service';
import { SolicitacaoService } from './../../services/solicitacao.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Cotacao } from 'src/app/models/Cotacao';
import { Pedido } from 'src/app/models/Pedido';
import { Solicitacao } from 'src/app/models/Solicitacao';
import { PedidoService } from 'src/app/services/pedido.service';
// import { MenuService } from 'src/app/services/menu.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

    modalRef = {} as BsModalRef;
    constructor(
      // private menuService: MenuService,
      private modalService: BsModalService,
      private toastr: ToastrService,
      private spinner: NgxSpinnerService,
      private router: Router,
      private pedidoService: PedidoService,
      private solicitacaoService: SolicitacaoService,
      private cotacaoService: CotacaoService,
    ) {}

    public cotacoes: Cotacao[] = [];
    public pedidos: Pedido[] = [];
    public solicitacoes: Solicitacao[] = [];
    opcao?: number;
    listagem: any[]=[];

    public imgWidth = 150;
    public imgMargin = 2;
    public imgIsVisible = false;

    public ngOnInit(): void {
      this.CarregarPedidos();
      this.GetSolicitacoes();
      this.opcao =0;
      this.load();
    }

    load() {
      console.log('sessionStorage', sessionStorage);
      (sessionStorage.refresh == 'true' || !sessionStorage.refresh)
          && location.reload();
      sessionStorage.refresh = false;
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
        resultTooltip = 'Reprovado';
        break;
      case 2:
        resultTooltip = 'Pendente';
        break;
    }
    return resultTooltip;
  }

    public AlteraVisibilidadeImg(): void{
      this.imgIsVisible = !this.imgIsVisible;
    }

    public CarregarPedidos(): void{
      // tslint:disable-next-line: deprecation
      this.pedidoService.getPendetes().subscribe(
        (pedidosResponse: Pedido[]) => {
          this.pedidos = pedidosResponse;
          this.spinner.hide()
        },
          // this.pedidosFiltrados = pedidosResponse;
        () => {
          this.spinner.hide(),
          this.toastr.error('Erro ao carregar os Pedidos', 'Erro');
        },
        () => this.spinner.hide()
      );
    }
    public CarregarCotacaoes(): void{
      // tslint:disable-next-line: deprecation
      this.cotacaoService.getCotacaoPedente().subscribe(
        (CotacaoResponse: Cotacao[]) => {
          this.cotacoes = CotacaoResponse

        },
        () => {
          this.spinner.hide(),
          this.toastr.error('Erro ao carregar os Cotacoes', 'Erro');
        },
        () => this.spinner.hide()
      );

    }
    public GetSolicitacoes(): void{
      // tslint:disable-next-line: deprecation
      this.solicitacaoService.getPendentes().subscribe({
        next: (solicitacoesResponse: Solicitacao[]) => {
          this.solicitacoes = solicitacoesResponse
          this.spinner.hide();
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
      this.toastr.success('', '');
    }

    decline(): void {
      this.modalRef.hide();
    }

    DetalharSolicitacao(id: number): void{
      this.router.navigate([`solicitações/detalhe/${id}`]);
    }
    DetalharPedido(id: number): void{
      this.router.navigate([`pedidos/detalhe/${id}`]);
    }
    DetalharCotacao(id: number): void{
      this.router.navigate([`solicitações/detalhe/${id}`]);
    }

}
