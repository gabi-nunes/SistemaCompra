import { FamiliaProdutoService } from 'src/app/services/familiaProduto.service';
import { FamiliaProduto } from 'src/app/models/FamiliaProduto';
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
    private router: Router,
    private familiaProdutoService: FamiliaProdutoService
  ) {}

  public produtos: Produto  [] = [];
  familiaProdutos: FamiliaProduto[] = [];
  public produtosFiltrados: Produto  [] = [];
  public imgIsVisible = false;
  private gridFilter = '';
  desc: string
  familiaFiltrados: Produto[];
  familiaIdFiltro: number;
  Familia: string;

  public get GridFilter(): string{
    return this.gridFilter;
  }
  public set GridFilter(value: string){
    this.gridFilter = value;
    this.produtosFiltrados = this.gridFilter ? this.Filtrar(this.gridFilter) : this.produtos;
  }

  public get FamiliaIdFiltro(): number{
    return this.familiaIdFiltro;
  }

  public set FamiliaIdFiltro(value: number){
    this.familiaIdFiltro = value;
    this.produtosFiltrados = this.familiaIdFiltro ? this.FiltrarByFamilia(this.familiaIdFiltro) : this.produtos;
  }


  public Filtrar(filter: string): Produto  []{
    filter = filter.toLocaleLowerCase();
    return this.produtos.filter(
      (produto: Produto) => produto.descricao.toLocaleLowerCase().indexOf(filter) !== -1

      );
  }

    public ngOnInit(): void {
      this.spinner.show();
      this.CarregarFamiliaProdutos();
      this.CarregarProdutos();
      this.setFamiliaProduto();
    }


    public CarregarProdutos(): void{
      // tslint:disable-next-line: deprecation
      this.produtoService.getProdutos().subscribe(
         (produtosResponse: Produto []) => {
          this.produtos = produtosResponse,
          this.produtosFiltrados = produtosResponse;
          this.spinner.hide()
        },
       (error: any) => {
          this.spinner.hide(),
          this.toastr.error('Erro ao carregar os Produtos', 'Erro');
        }
      );
  }

  public FiltrarByFamilia(filter: number): Produto[]{
    return this.produtos.filter(
      (produto: Produto) => produto.familiaProdutoId == filter);
  }
  public setFamiliaProduto(): void{
    this.produtos.forEach(forn => {
      const famProds = this.familiaProdutos.filter(
        (fp: FamiliaProduto) => fp.id === forn.familiaProdutoId
        );
      forn.familiaProduto = famProds[0];
      this.spinner.hide()
    });
  }

  public CarregarFamiliaProdutos(): void{
    debugger
    this.familiaProdutoService.getFamiliaProdutos().subscribe(
      (familias: FamiliaProduto[]) => {
        this.familiaProdutos = familias;
        this.spinner.hide()
      },
      (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao tentar carregar as Famílias de Produtos', 'Erro');
        console.error(error);
      },
      () => {
      }
    );
  }

 public  getFamiliaDesc(FamiliaId: number): any {
      const familiaProd = this.familiaProdutos.find(x=> x.id == FamiliaId);
      return familiaProd?.descricao;
  }


  decline(): void {
    this.modalRef.hide();
  }

}
