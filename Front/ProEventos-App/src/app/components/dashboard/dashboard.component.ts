import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Cotacao } from 'src/app/models/Cotacao';
import { Pedido } from 'src/app/models/Pedido';
import { Solicitacao } from 'src/app/models/Solicitacao';
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
      private router: Router
    ) {}

    public cotacoes: Cotacao[] = [];
    public pedidos: Pedido[] = [];
    public solicitacoes: Solicitacao[] = [];
    opcao?: number;

    public imgWidth = 150;
    public imgMargin = 2;
    public imgIsVisible = false;

    public ngOnInit(): void {
    }

    public AlteraVisibilidadeImg(): void{
      this.imgIsVisible = !this.imgIsVisible;
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

    DetalharEvento(id: number): void{
      this.router.navigate([`solicitações/detalhe/${id}`]);
    }

}
