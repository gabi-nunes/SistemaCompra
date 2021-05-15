import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ItemPedido } from 'src/app/models/ItemPedido';
import { Pedido } from 'src/app/models/Pedido';
import { user } from 'src/app/models/user';
import { FamiliaProdutoService } from 'src/app/services/familiaProduto.service';
import { PedidoService } from 'src/app/services/pedido.service';
import { UserService } from 'src/app/services/user.service';

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
    private userService: UserService
    ) {}

public user: user;
public UserFiltrados: user[] = [];
public userId = 0;
form: FormGroup = new FormGroup({});
pedido = {} as Pedido;
pedidoId = {} as any;
modalRef = {} as BsModalRef;
itensPedidos = [{}] as ItemPedido[];
itemPedId: number;

ngOnInit(): void{
this.validation();
this.CarregarPedido();
const userJson = localStorage.getItem('currentUser') || '{}';
this.user = JSON.parse(userJson);
// this.CarregarFamiliaProdutos();
}



public validation(): void{
this.form = new FormGroup({
dataEmissao: new FormControl(this.pedido.dataEmissao, Validators.required),
observacao: new FormControl('', Validators.required),
aprovador: new FormControl(this.user.name, Validators.required),
fornecedor: new FormControl('', Validators.required)
// cotacao: new FormControl(this.cotacao.id, Validators.required)
});
}

// public CarregarItensPedidos(): void{
// this.itemPedidosService.getItensPedidos().subscribe(
// (familias: FamiliaProduto[]) => {
// this.itensPedidos = familias;
// },
// (error: any) => {
// this.spinner.hide();
// this.toastr.error('Erro ao tentar carregar os Itens do Pedido', 'Erro');
// console.error(error);
// },
// () => {
// this.spinner.hide();
// }
// );
// }


// public setItensPedidos(itemPid: number): ItemPedido{
// const famProds = this.itensPedidos.filter(
// (itp: ItemPedido) => itp.id === itemPid
// );
// return famProds[0];
// }

public CarregarPedido(): void{
this.pedidoId = this.actRouter.snapshot.paramMap.get('id');

if (this.pedidoId !== null){
this.spinner.show();
this.pedidoService.getPedidoById(+this.pedidoId).subscribe(
(e: Pedido) => {
  this.pedido = {...e},
  this.form.patchValue(e);
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
}

public SalvarPedido(): void{
this.spinner.show();
if (this.form.valid){
    // this.pedido.itemPedido = this.setItemPedido(this.itemPedidoId);
    if (this.pedidoId === null){

        this.pedido = {...this.form.value};
        this.pedidoService.postPedido(this.pedido).subscribe(
          () => {this.toastr.success('Pedido aprovado com Sucesso', 'Pedido Aprovado'); },
          (error: any) => {
            console.log(error);
            this.toastr.error('Erro ao tentar aprovar Pedido', 'Erro');
            this.spinner.hide();
          },
          () => {
            this.spinner.hide();
          }
        );
        }
    }
}

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

}
