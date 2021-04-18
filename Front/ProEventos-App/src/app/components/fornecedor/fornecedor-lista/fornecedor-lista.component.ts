import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Fornecedor } from 'src/app/models/Fornecedor';
import { FornecedorService } from 'src/app/services/fornecedor.service';

@Component({
  selector: 'app-fornecedor-lista',
  templateUrl: './fornecedor-lista.component.html',
  styleUrls: ['./fornecedor-lista.component.scss']
})
export class FornecedorListaComponent implements OnInit {

  modalRef = {} as BsModalRef;
  constructor(
    private fornecedorService: FornecedorService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {}

  public fornecedores: Fornecedor[] = [];
  public fornecedorId = 0;
  public fornecedoresFiltrados: Fornecedor[] = [];
  public imgWidth = 150;
  public imgMargin = 2;
  public imgIsVisible = false;
  private gridFilter = '';

  public get GridFilter(): string{
    return this.gridFilter;
  }
  public set GridFilter(value: string){
    this.gridFilter = value;
    this.fornecedoresFiltrados = this.gridFilter ? this.Filtrar(this.gridFilter) : this.fornecedores;
  }

  public Filtrar(filter: string): Fornecedor[]{
    filter = filter.toLocaleLowerCase();
    return this.fornecedores.filter(
      (fornecedor: any) => fornecedor.nome.toLocaleLowerCase().indexOf(filter) !== -1

      );
  }

  public ngOnInit(): void {
    // this.spinner.show();
    // this.CarregarFornecedores();
  }

  public AlteraVisibilidadeImg(): void{
    this.imgIsVisible = !this.imgIsVisible;
  }
  public CarregarFornecedores(): void{
    // tslint:disable-next-line: deprecation
    this.fornecedorService.getFornecedores().subscribe(
      (fornecedoresResponse: Fornecedor[]) => {
        this.fornecedores = fornecedoresResponse,
        this.fornecedoresFiltrados = fornecedoresResponse;
      },
      () => {
        this.spinner.hide(),
        this.toastr.error('Erro ao carregar os Fornecedores', 'Erro');
      },
      () => this.spinner.hide()
    );
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
          this.CarregarFornecedores();
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
