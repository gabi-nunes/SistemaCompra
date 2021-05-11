import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Produto } from 'src/app/models/Produto';
import { ProdutoService } from 'src/app/services/produto.service';

@Component({
  selector: 'app-produto-lista',
  templateUrl: './produto-lista.component.html',
  styleUrls: ['./produto-lista.component.scss']
})
export class ProdutoListaComponent implements OnInit {

  modalRef = {} as BsModalRef;
  constructor(
    private produtoService: ProdutoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {}

  public produtos: Produto  [] = [];
  public produtosFiltrados: Produto  [] = [];
  public imgIsVisible = false;
  private gridFilter = '';

  public get GridFilter(): string{
    return this.gridFilter;
  }
  public set GridFilter(value: string){
    this.gridFilter = value;
    this.produtosFiltrados = this.gridFilter ? this.Filtrar(this.gridFilter) : this.produtos;
  }

  public Filtrar(filter: string): Produto  []{
    filter = filter.toLocaleLowerCase();
    return this.produtos.filter(
      (produto: Produto) => produto.descricao.toLocaleLowerCase().indexOf(filter) !== -1

      );
  }

    public ngOnInit(): void {
      this.spinner.show();
      this.CarregarProdutos();
    }

    public AlteraVisibilidadeImg(): void{
      this.imgIsVisible = !this.imgIsVisible;
    }
    public CarregarProdutos(): void{
      // tslint:disable-next-line: deprecation
      this.produtoService.getProdutos().subscribe({
        next: (produtosResponse: Produto []) => {
          this.produtos = produtosResponse,
          this.produtosFiltrados = produtosResponse;
        },
        error: () => {
          this.spinner.hide(),
          this.toastr.error('Erro ao carregar os Produtos', 'Erro');
        },
        complete: () => this.spinner.hide()
      });
  }




  decline(): void {
    this.modalRef.hide();
  }

}
