import { ProdutoService } from './../../../services/produto.service';
import { Fornecedor } from './../../../models/Fornecedor';
import { Pedido } from './../../../models/Pedido';
import { preco } from './../../../models/preco';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ItemPedido } from 'src/app/models/ItemPedido';
import { user } from 'src/app/models/user';
import { FamiliaProdutoService } from 'src/app/services/familiaProduto.service';
import { PedidoService } from 'src/app/services/pedido.service';
import { UserService } from 'src/app/services/user.service';
import { Cotacao } from 'src/app/models/Cotacao';
import { Produto } from 'src/app/models/Produto';

@Component({
  selector: 'app-pedido-detalhe',
  templateUrl: './pedido-detalhe.component.html',
  styleUrls: ['./pedido-detalhe.component.scss']
})
export class PedidoDetalheComponent implements OnInit {

  constructor(
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private pedidoService: PedidoService,
    private familiaProdService: FamiliaProdutoService,
    private actRouter: ActivatedRoute,
    private modalService: BsModalService,
    private router: Router,
    private userService: UserService,
    private produtoService: ProdutoService
    ) {}

public user: user;
aprovador: user;
cotacao: Cotacao;
public UserFiltrados: user[] = [];
public userId = 0;
form: FormGroup = new FormGroup({});
pedido = {} as Pedido;
pedidoId = 0;
itensPedidos: ItemPedido[] = [];
itemPedId: number;
fornecedor: Fornecedor;
produto: Produto[]=[];
podeAprovar : boolean = false;

modalRef = {} as BsModalRef;
modalRefAprovacao = {} as BsModalRef;

ngOnInit(): void{
this.validation();
this.actRouter.params.subscribe(params => this.pedidoId = params['id']);
const userJson = localStorage.getItem('currentUser') || '{}';
this.user = JSON.parse(userJson);
this.isAprovador();
this.CarregarPedidos();
this.Carregarprodutos();
// this.CarregarFamiliaProdutos();
}



public validation(): void{
this.form = new FormGroup({
id: new FormControl(this.pedido?.id),
id2: new FormControl(this.pedido?.id),
cotacaoId: new FormControl(this.pedido?.cotacaoId),
dataEmissao: new FormControl(this.pedido?.dataEmissao),
observacao: new FormControl(this.pedido?.cotacao?.solicitacao?.observacao),
observacaoRejeicao: new FormControl(this.pedido?.observacaoRejeicao),
nome: new FormControl(this.fornecedor?.nome),
frete: new FormControl(this.pedido?.cotacao?.frete),
formaPagamento: new FormControl(this.pedido?.cotacao?.frmPagamento),
total: new FormControl(this.pedido?.cotacao?.total.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })),
cnpj: new FormControl(this.fornecedor?.cnpj),
endereco: new FormControl(this.fornecedor?.endereco),
bairro: new FormControl(this.fornecedor?.bairro),
numero: new FormControl(this.fornecedor?.numero),
telefone: new FormControl(this.fornecedor?.numero),
estado: new FormControl(this.fornecedor?.estado),
cep: new FormControl(this.fornecedor?.cep),
inscricaoEstadual: new FormControl(this.fornecedor?.inscricaoEstadual),
inscricaoMunicipal: new FormControl(this.fornecedor?.inscricaoMunicipal),
dataEntrega: new FormControl(this.pedido?.cotacao?.dataEntrega),
});
}

public CarregarPedidos(): void{

this.pedidoService.getPedidoById(this.pedidoId).subscribe(
(pedidoresponse: Pedido) => {
this.pedido = pedidoresponse;
this.pedido.itensPedidos.forEach((item: ItemPedido) => {
  this.itensPedidos.push(item);
});
this.CarregarFornecedor();
},
(error: any) => {
this.spinner.hide();
this.toastr.error('Erro ao tentar carregar o Pedido', 'Erro');
console.error(error);
},
() => {
this.spinner.hide();
}
);
}

public getDescricaoproduto(idProduto: number): string{
  let produtoSelecionado = this.produto.find(x=> x.id== idProduto);
  var descProduto= produtoSelecionado?.descricao ?? '';
  return descProduto;
}

public Carregarprodutos(): void{
  // tslint:disable-next-line: deprecation
  this.produtoService.getProdutos().subscribe(
    (produtoResponse: Produto[]) => {
      this.produto = produtoResponse;
      console.log(this.produto)
    },
    () => {
      this.spinner.hide(),
      this.toastr.error('Erro ao carregar os Cotações', 'Erro');
    },
    () => this.spinner.hide()
  );


}
public CarregarFornecedor(): void{
    this.pedidoService.getFornecedorById(this.pedido.cotacao.fornecedorId).subscribe(
    (fornecedorresponse: any) => {
    this.fornecedor = fornecedorresponse;
    this.validation();
    },
    (error: any) => {
    this.spinner.hide();
    this.toastr.error('Erro ao tentar carregar o Pedido', 'Erro');
    console.error(error);
    },
    () => {
    this.spinner.hide();
    }
    );

    }


  isAprovador(){
      if( this.user?.cargo =="gerente" ||  this.user.cargo =="Gerente" || ( this.user?.cargo == "comprador" || this.user?.cargo == "comprador" && this.pedido.valorMaximo < this.pedido.cotacao.total)){
        this.podeAprovar= true;
      }
    }


// public setItensPedidos(itemPid: number): ItemPedido{
// const famProds = this.itensPedidos.filter(
// (itp: ItemPedido) => itp.id === itemPid
// );
// return famProds[0];
// }

public OpenModal(template: TemplateRef<any>): void{
this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
}

decline(): void {this.modalRef.hide(); }

confirm(): void {
this.router.navigate([`/pedido/lista`]);
this.modalRef.hide();
}

public CancelarForm(): void{
}

OpenModalAprovacao(template: TemplateRef<any>): void{
  this.modalRefAprovacao = this.modalService.show(template, {class: 'modal-md modal-dialog-centered'});
}

CloseModalAprovacao(): void{
  this.modalRefAprovacao.hide();
}

onMudouEvento(evento: any): void{
  console.log(evento);
}

cssStatusValidation(): any{
  if (this.pedido.statusAprov === 1) {return {'is-invalid': true}; }
  return {'is-valid': true};
}

// AprovStatusModal(): void{
//   this.pedido.statusAprov = 2;
//   this.router.navigate([`/pedido/lista`]);;
// }

// DesaprovStatusModal(): void{
//   this.pedido.statusAprov = 1;
//   this.router.navigate([`/pedido/lista`]);
// }

GerarRelatrio(): void{

  window.print();

}

}
