import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Fornecedor } from 'src/app/models/Fornecedor';
import { Pedido } from 'src/app/models/Pedido';
import { CotacaoService } from 'src/app/services/cotacao.service';
import { FamiliaProdutoService } from 'src/app/services/familiaProduto.service';
import { PedidoService } from 'src/app/services/pedido.service';

@Component({
  selector: 'app-listagem-pedido',
  templateUrl: './listagem-pedido.component.html',
  styleUrls: ['./listagem-pedido.component.scss']
})
export class ListagemPedidoComponent implements OnInit {


  modalRef = {} as BsModalRef;
  constructor( private pedidoService: PedidoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router,
    private familiaProdService: FamiliaProdutoService,) { }

    public pedido: Pedido[] = [];
    public fornecedor: Fornecedor;
    public PedidoId = 0;


  ngOnInit() {
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.fornecedor = JSON.parse(userJson);
    this.spinner.show();
    this.CarregarPedido();
  }

  DetalharPedido(id: number): void{
    this.router.navigate([`pedidos/detalhe/${id}`]);
  }

  GetColorByStatus(status: number): any{
    let resultColor: any;
    switch (status) {
      case 0:
        resultColor = '#f0ad4e';
        break;
      case 1:
        resultColor = '#5cb85c';
        break;
      case 2:
        resultColor = '#5bc0de';
        break;
        case 3:
        resultColor = '#d9534f';
        break;
    }
    return resultColor;
  }

  GetTooltipByStatus(status: number): string{
    let resultTooltip: any;
    switch (status) {
      case 0:
        resultTooltip = 'Pendente';
        break;
      case 1:
        resultTooltip = 'Aguardando Ofertas';
        break;
      case 2:
        resultTooltip = 'Ofertas Recebidas';
        break;
        case 3:
          resultTooltip = 'Cotação Encerrada';
          break;
    }
    return resultTooltip;
  }

  public CarregarPedido(): void{
    // tslint:disable-next-line: deprecation
    this.pedidoService.getPedidoByIdFornecedor(this.fornecedor.id).subscribe(
      (pedidoResponse: Pedido[]) => {
        this.pedido = pedidoResponse
        console.log(this.pedido);
      },
      () => {
        this.spinner.hide(),
        this.toastr.error('Erro ao carregar os Cotacoes', 'Erro');
      },
      () => this.spinner.hide()
    );

  }

  openModal(template: TemplateRef<any>, pedidoId: number): void{
    this.PedidoId= pedidoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef.hide();
    this.spinner.show();
    debugger
    this.pedidoService.deletePedido(this.PedidoId).subscribe(
      (result: any) => {
          console.log(result);
          this.toastr.success('Pedido Deletada com Sucesso', 'Deletado');
          this.spinner.hide();
          this.CarregarPedido();
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Falha ao tentar deletar o Pedido', 'Erro');
        this.spinner.hide();
      },
      () => {}
    );
  }

  decline(): void {
    this.modalRef.hide();
  }


}
