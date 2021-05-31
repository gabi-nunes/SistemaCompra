import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Pedido } from 'src/app/models/Pedido';
import { user } from 'src/app/models/user';
import { PedidoService } from 'src/app/services/pedido.service';

@Component({
  selector: 'app-aprovar-pedido',
  templateUrl: './aprovar-pedido.component.html',
  styleUrls: ['./aprovar-pedido.component.scss']
})
export class AprovarPedidoComponent implements OnInit {

  constructor(private pedidoService: PedidoService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService,
              private router: Router
    ) {}
@Input() modal: BsModalRef;
@Input() pedido: Pedido;

@Output() carregaEventos = new EventEmitter();

objPedidoAprov = {} as any;
user = {} as user;
observacaoRejeicao: string;
ngOnInit(): void {
const userJson = localStorage.getItem('currentUser') || '{}';
this.user = JSON.parse(userJson);

this.objPedidoAprov = {
dataAprovacao: new Date(),
statusAprov: null,
aprovadorId: this.user.id,
observacaoRejeicao: null
};

}

public CloseModal(): void{
this.modal.hide();
}

public AprovarPedido(): void{
  this.spinner.show();
  this.objPedidoAprov.statusAprov = 0;
  this.objPedidoAprov.observacaoRejeicao = this.observacaoRejeicao;
  debugger;
  this.router.navigate([`/pedidos/lista`]);
  this.pedidoService.putAlteraStatusPedido(+this.pedido.id, this.objPedidoAprov).subscribe(
  () => {
  this.spinner.hide();
  this.modal.hide();
  this.toastr.success('Pedido Aprovado com Sucesso', 'Aprovado');
  debugger;

  },
  (error: any) => {
    this.spinner.hide();
    this.toastr.error('Erro ao tentar Aprovar a Pedido', 'Erro');
    console.error(error);
  },
  () => {
  this.spinner.hide();
  }

  );
}
public ReprovarPedido(): void{
this.spinner.show();
this.objPedidoAprov.statusAprov = 1;
this.objPedidoAprov.observacaoRejeicao = this.observacaoRejeicao;
debugger;
this.pedidoService.putAlteraStatusPedido(+this.pedido.id, this.objPedidoAprov).subscribe(
() => {
this.spinner.hide();
this.modal.hide();
this.toastr.success('Pedido reprovado com Sucesso', 'Reprovado');
this.carregaEventos.emit({RecarregaPedido: true});
this.router.navigate([`/pedidos/lista`]);
},
(error: any) => {
this.spinner.hide();
this.toastr.error('Erro ao tentar Reprovar a Pedido', 'Erro');
console.error(error);
},
() => {
this.spinner.hide();
}
);
}

}
