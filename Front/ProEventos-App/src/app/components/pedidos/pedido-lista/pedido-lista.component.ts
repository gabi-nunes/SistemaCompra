import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Pedido } from 'src/app/models/Pedido';
import { user } from 'src/app/models/user';
import { PedidoService } from 'src/app/services/pedido.service';


@Component({
  selector: 'app-pedido-lista',
  templateUrl: './pedido-lista.component.html',
  styleUrls: ['./pedido-lista.component.scss']
})
export class PedidoListaComponent implements OnInit {

  modalRef = {} as BsModalRef;
  constructor(
    private pedidoService: PedidoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {}

  public pedidos: Pedido[] = [];
  public pedidoId = 0;
  public pedidosFiltrados: Pedido[] = [];
  public imgWidth = 150;
  public imgMargin = 2;
  public imgIsVisible = false;
  private gridFilter = '';
  valor: string;
  user: user;

  podeAprovar : boolean = false;

  public get GridFilter(): string{
    return this.gridFilter;
  }
  public set GridFilter(value: string){
    this.gridFilter = value;
    this.pedidosFiltrados = this.gridFilter ? this.Filtrar(this.gridFilter) : this.pedidos;
  }

  public Filtrar(filter: string): Pedido[]{
    filter = filter.toLocaleLowerCase();
    return this.pedidos.filter(
      (pedidos: Pedido) => pedidos.dataEmissao.toLocaleLowerCase().indexOf(filter) !== -1

      );
  }

  public ngOnInit(): void {
    this.spinner.show();
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.user = JSON.parse(userJson);
    this.isAprovar();
    this.CarregarPedidos();
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
  public CarregarPedidos(): void{
    // tslint:disable-next-line: deprecation
    this.pedidoService.getPedidos().subscribe(
      (pedidosResponse: Pedido[]) => {
        this.pedidos = pedidosResponse;
      },
        // this.pedidosFiltrados = pedidosResponse;
      () => {
        this.spinner.hide(),
        this.toastr.error('Erro ao carregar os Pedidos', 'Erro');
      },
      () => this.spinner.hide()
    );
  }

  incluirValor(){
    let valorReal;
    this.valor.replace(",",".");
    valorReal= parseFloat(this.valor);
    debugger
    this.pedidoService.passarValorMaximo(valorReal).subscribe(
      (result: any) => {
          console.log(result);
          this.toastr.success('Valor salvo com sucesso', 'Sucesso');
          this.spinner.hide();
          this.CarregarPedidos();
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Erro ao salvar o valor', 'Erro');
        this.spinner.hide();
      },
      () => {}
    );

  }


  openModal(template: TemplateRef<any>, pedidoId: number): void{
    this.pedidoId = pedidoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  openModalValor(template: TemplateRef<any>): void{
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }


  confirm(): void {
    this.modalRef.hide();
    this.spinner.show();
    this.pedidoService.deletePedido(this.pedidoId).subscribe(
      (result: any) => {
          console.log(result);
          this.toastr.success('Pedido deletado com Sucesso', 'Deletado');
          this.spinner.hide();
          this.CarregarPedidos();
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Falha ao tentar deletar o Pedido', 'Erro');
        this.spinner.hide();
      },
      () => {}
    );
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


  decline(): void {
    this.modalRef.hide();
  }

  DetalharPedido(id: number): void{
    this.router.navigate([`pedidos/detalhe/${id}`]);
  }


}
