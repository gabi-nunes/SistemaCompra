import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FamiliaProduto } from 'src/app/models/FamiliaProduto';
import { Fornecedor } from 'src/app/models/Fornecedor';
import { FamiliaProdutoService } from 'src/app/services/familiaProduto.service';
import { FornecedorService } from 'src/app/services/fornecedor.service';

@Component({
  selector: 'app-area-fornecedor',
  templateUrl: './area-fornecedor.component.html',
  styleUrls: ['./area-fornecedor.component.scss']
})
export class AreaFornecedorComponent implements OnInit {
  modalRef = {} as BsModalRef;
  constructor(
    private fornecedorService: FornecedorService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router,
    private familiaProdService: FamiliaProdutoService,
  ) {}

  public fornecedores: Fornecedor[] = [];
  public fornecedorId = 0;
  public fornecedoresFiltrados: Fornecedor[] = [];
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
    this.fornecedoresFiltrados = this.gridFilter ? this.Filtrar(this.gridFilter) : this.fornecedores;
  }

  public get FamiliaIdFiltro(): number{
    return this.familiaIdFiltro;
  }
  public set FamiliaIdFiltro(value: number){
    this.familiaIdFiltro = value;
    this.fornecedoresFiltrados = this.familiaIdFiltro ? this.FiltrarByFamilia(this.familiaIdFiltro) : this.fornecedores;
  }

  public Filtrar(filter: string): Fornecedor[]{
    filter = filter.toLocaleLowerCase();
    return this.fornecedores.filter(
      (fornecedor: Fornecedor) => fornecedor.nome.toLocaleLowerCase().indexOf(filter) !== -1
     );
  }

  public FiltrarByFamilia(filter: number): Fornecedor[]{
    return this.fornecedores.filter(
      (fornecedor: Fornecedor) => fornecedor.familiaProdutoId == filter);
  }

  public CarregarFamiliaProdutos(): void{
    this.familiaProdService.getFamiliaProdutos().subscribe(
      (familias: FamiliaProduto[]) => {
        this.familiaProdutos = familias;
      },
      (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao tentar carregar as Famílias de Produtos', 'Erro');
        console.error(error);
      },
      () => {
        this.spinner.hide();
      }
    );
  }

  public ngOnInit(): void {
    this.spinner.show();
    this.CarregarFamiliaProdutos();

  }

  public AlteraVisibilidadeImg(): void{
    this.imgIsVisible = !this.imgIsVisible;
  }
  public CarregarCotacaoes(): void{
    // tslint:disable-next-line: deprecation
    this.fornecedorService.getFornecedores().subscribe(
      (fornecedoresResponse: Fornecedor[]) => {
        this.fornecedores = fornecedoresResponse,
        this.fornecedoresFiltrados = fornecedoresResponse;
        this.setFamiliaProduto();
      },
      () => {
        this.spinner.hide(),
        this.toastr.error('Erro ao carregar os Fornecedores', 'Erro');
      },
      () => this.spinner.hide()
    );
  }

  public setFamiliaProduto(): void{
    this.fornecedores.forEach(forn => {
      const famProds = this.familiaProdutos.filter(
        (fp: FamiliaProduto) => fp.id === forn.familiaProdutoId
        );
      forn.familiaProduto = famProds[0];
    });
  }

  openModal(template: TemplateRef<any>, fornecedorId: number): void{
    this.fornecedorId = fornecedorId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef.hide();
    this.spinner.show();
    this.fornecedorService.deleteFornecedor(this.fornecedorId).subscribe(
      (result: any) => {
          console.log(result);
          this.toastr.success('Fornecedor deletado com Sucesso', 'Deletado');
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

  DetalharFornecedor(id: number): void{
    this.router.navigate([`fornecedor/detalhe/${id}`]);
  }

}
