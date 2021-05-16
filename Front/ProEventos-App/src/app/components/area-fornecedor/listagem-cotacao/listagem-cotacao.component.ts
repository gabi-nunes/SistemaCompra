import { CotacoesListaComponent } from './../../cotacoes/cotacoes-lista/cotacoes-lista.component';
import { Cotacao } from './../../../models/Cotacao';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FamiliaProduto } from 'src/app/models/FamiliaProduto';
import { Fornecedor } from 'src/app/models/Fornecedor';
import { CotacaoService } from 'src/app/services/cotacao.service';
import { FamiliaProdutoService } from 'src/app/services/familiaProduto.service';


@Component({
  selector: 'app-listagem-cotacao',
  templateUrl: './listagem-cotacao.component.html',
  styleUrls: ['./listagem-cotacao.component.scss']
})
export class ListagemCotacaoComponent implements OnInit {

  modalRef = {} as BsModalRef;
  constructor(
    private cotacaoService: CotacaoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router,
    private familiaProdService: FamiliaProdutoService,
  ) {}

  public cotacao: Cotacao[] = [];
  public fornecedor: Fornecedor;
  public cotacaoId = 0;
  public cotacaoFiltrados: Cotacao[] = [];
  public imgWidth = 150;
  public imgMargin = 2;
  public imgIsVisible = false;
  public familiaProdutoFornecedor = {} as FamiliaProduto;
  public familiaProdutos: FamiliaProduto[] = [];
  private familiaIdFiltro: number;
  private gridFilter = '';


  public get GridFilter(): string{
    return this.gridFilter;
  }
  public set GridFilter(value: string){
    this.gridFilter = value;
    this.cotacaoFiltrados = this.gridFilter ? this.Filtrar(this.gridFilter) : this.cotacao;
  }

  public Filtrar(filter: string): Cotacao[]{
    filter = filter.toLocaleLowerCase();
    return this.cotacao.filter(
      (cotacao: Cotacao) => cotacao.dataEmissaoCotacao.toDateString().indexOf(filter) !== -1
     );
  }


  public ngOnInit(): void {
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.fornecedor = JSON.parse(userJson);
    this.spinner.show();
    this.CarregarCotacaoes();

  }

  public AlteraVisibilidadeImg(): void{
    this.imgIsVisible = !this.imgIsVisible;
  }
  public CarregarCotacaoes(): void{
    // tslint:disable-next-line: deprecation
    this.cotacaoService.getCotacaoByFornecedor(this.fornecedor.id).subscribe(
      (CotacaoResponse: Cotacao[]) => {
        this.cotacao = CotacaoResponse
        console.log(this.cotacao);
      },
      () => {
        this.spinner.hide(),
        this.toastr.error('Erro ao carregar os Cotacoes', 'Erro');
      },
      () => this.spinner.hide()
    );

  }

  openModal(template: TemplateRef<any>, cotacaoId: number): void{
    this.cotacaoId= cotacaoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
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

  confirm(): void {
    this.modalRef.hide();
    this.spinner.show();
    debugger
    this.cotacaoService.DeleteCotacao(this.cotacaoId).subscribe(
      (result: any) => {
          console.log(result);
          this.toastr.success('Cotacao Deletada com Sucesso', 'Deletado');
          this.spinner.hide();
          this.CarregarCotacaoes();
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Falha ao tentar deletar o Fornecedor', 'Erro');
        this.spinner.hide();
      },
      () => {}
    );
  }

  decline(): void {
    this.modalRef.hide();
  }

  DetalharCotacao(id: number): void{
    this.router.navigate([`areaFornecedor/detalhe/${id}`]);
  }

}
