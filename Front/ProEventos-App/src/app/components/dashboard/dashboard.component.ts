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
import { user } from 'src/app/models/user';
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

    public Solicitacaocotacoes: Solicitacao[] = [];
    public SolicitacaoRastreabilidade: Solicitacao[] = [];
    public pedidos: Pedido[] = [];
    pedidosPendentes : Pedido[] = [];
    cotacoes: Cotacao[];
    public solicitacoes: Solicitacao[] = [];
    opcao?: number;
    isvalid: boolean = false;
    listagem: any[]=[];

    public imgWidth = 150;
    public imgMargin = 2;
    public imgIsVisible = false;
    user:user;

    public ngOnInit(): void {
      this.reload();
      this.GetSolicitacoesRatereabilidade();
      this.CarregarPedidos();
      this.GetSolicitacoes();
      this.GetCotacoes();
      this.GetSolicitacoesAprovadas();
      this.opcao =0;
      const userJson = localStorage.getItem('currentUser') || '{}';
      this.user = JSON.parse(userJson);
    }

    reload() {
      if (this.solicitacaoService.reload) {
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigate(['dashboard']);
          this.solicitacaoService.reload = false;
        });
      }
    }

    FilterCotacoesById(solId: number): Cotacao[]{

      const cotacoesSolicitacao = this.cotacoes?.filter(c => c.solicitacaoId == solId);
      return cotacoesSolicitacao;
    }



    public GetStatus(solId: number): number{
      const cots = this.FilterCotacoesById(solId);
      if (!cots?.length){return -1; }
      if (cots.some(c => c.status === 3)) { return 3; }
      else if (cots.every(c => c.status === 2)) { return 2; }
      else if (cots.some(c => c.status === 1)) { return 1; }
      return 0;
    }

    GetColorByStatus(status?: number): any{
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


  GetTooltipByStatus(status?: number): string{
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



  GetColorByStatusCotacao(solId: number): any{

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

  GetStatusRastreabilidade(solid: number, statusAprov: number){

    let statusObj = {color: '', tooltip: ''};

    if(statusAprov <= 2){
      statusObj.color= this.GetColorByStatus(statusAprov);
      statusObj.tooltip= this.GetTooltipByStatus(statusAprov);
      return statusObj;
    }
    if(statusAprov == 3){
       return statusObj = this.GetColorByStatusCotacao(solid);
    }
    if(statusAprov == 4){
      const cotacoesSol = this.cotacoes?.filter(c => c.solicitacaoId == solid);
      const cotacaoId = cotacoesSol.find(c => c.fornecedorGanhadorId > 0)?.id;
      const pedidoStatus = this.pedidos?.find(p => p.cotacaoId == cotacaoId)?.statusAprov;

      statusObj.color= this.GetColorByStatus(pedidoStatus);
      statusObj.tooltip= this.GetTooltipByStatus(pedidoStatus);

      return statusObj;
    }
    return statusObj;
  }



    public AlteraVisibilidadeImg(): void{
      this.imgIsVisible = !this.imgIsVisible;
    }

    public CarregarPedidos(): void{
      // tslint:disable-next-line: deprecation
      this.pedidoService.getPedidos().subscribe(
        (pedidosResponse: Pedido[]) => {
          this.pedidos = pedidosResponse;
          this.pedidosPendentes = this.pedidos.filter(p => p.statusAprov == 2);
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


  public GetCotacoes(): void{
    // tslint:disable-next-line: deprecation
    this.cotacaoService.getCotacoes().subscribe({
      next: (CotacaoResponse: Cotacao[]) => {
        this.cotacoes = CotacaoResponse;
        this.cotacoes.forEach(item => {
          if(item.status > 0){
             this.isvalid= true;
          }
        })
        console.log(this.isvalid)
        // this.CotacaoFiltradas = CotacaoResponse;
      },
      error: () => {
        this.spinner.hide(),
        this.toastr.error('Erro ao carregar as Solicitações', 'Erro');
      },
      complete: () => this.spinner.hide()
    });
  }

    public GetSolicitacoesAprovadas(): void{
      // tslint:disable-next-line: deprecation
      this.solicitacaoService.getSolicitacoes().subscribe({
        next: (solicitacoesResponse: any[]) => {
          this.Solicitacaocotacoes = solicitacoesResponse;
          this.Solicitacaocotacoes = this.Solicitacaocotacoes?.filter(s => s.statusAprovacao === 0);
        },
        error: () => {
          this.spinner.hide(),
          this.toastr.error('Erro ao carregar as Solicitações', 'Erro');
        },
        complete: () => this.spinner.hide()
      });
    }

    public GetSolicitacoesRatereabilidade(): void{
      // tslint:disable-next-line: deprecation
      this.solicitacaoService.getSolicitacoes().subscribe({
        next: (solicitacoesResponse: any[]) => {
          this.SolicitacaoRastreabilidade= solicitacoesResponse;
          debugger
          console.log(this.SolicitacaoRastreabilidade)
        },
        error: () => {
          this.spinner.hide(),
          this.toastr.error('Erro ao carregar as Solicitações', 'Erro');
        },
        complete: () => this.spinner.hide()
      });
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

    GetEstagio(status: number) : string{
      let texto = "oi";
      switch(status){
      case 0:
        texto = "Em Solicitação"
        break;
      case 1:
        texto = "Em Solicitação"
        break;
      case 2:
        texto = "Em Solicitação"
        break;
      case 3:
        texto = "Em Cotação"
        break;
        case 4:
        texto = "Em Pedido";
        break;
      }

      return texto;
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
      if(this.user.cargo!="0"){
      this.router.navigate([`pedidos/detalhe/${id}`]);
      }
      else{

      }
    }
    DetalharCotacao(id: number): void{
      if(this.user.cargo!=="0"){
          this.router.navigate([`cotacoes/detalhe/${id}`]);
      }
      else{}
    }

    DetalhaRastreabilidade(id: number, statusSoli : number): void{
        if(statusSoli <= 2){
          this.router.navigate([`solicitações/detalhe/${id}`]);
        }
        if(statusSoli == 3){
          this.router.navigate([`cotacoes/detalhe/${id}`]);
        }
        if(statusSoli == 4){
          const cotacoesSol = this.cotacoes.filter(c => c.solicitacaoId == id);
          const cotacaoId = cotacoesSol.find(c => c.fornecedorGanhadorId > 0)?.id;
          const pedidoId = this.pedidos.find(p => p.cotacaoId == cotacaoId)?.id;
          this.router.navigate([`pedidos/detalhe/${pedidoId}`]);
        }
    }

    novasoli(): void{
      this.router.navigate([`solicitações/detalhe/`]);
    }

}
