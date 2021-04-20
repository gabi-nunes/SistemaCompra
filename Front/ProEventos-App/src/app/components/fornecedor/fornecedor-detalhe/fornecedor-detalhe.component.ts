import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FamiliaProduto } from 'src/app/models/FamiliaProduto';
import { Fornecedor } from 'src/app/models/Fornecedor';
import { FamiliaProdutoService } from 'src/app/services/familiaProduto.service';
import { FornecedorService } from 'src/app/services/fornecedor.service';

@Component({
  selector: 'app-fornecedor-detalhe',
  templateUrl: './fornecedor-detalhe.component.html',
  styleUrls: ['./fornecedor-detalhe.component.scss']
})
export class FornecedorDetalheComponent implements OnInit {
/**
 *
 */
constructor(private spinner: NgxSpinnerService,
            private toastr: ToastrService,
            private fornecedorService: FornecedorService,
            private familiaProdService: FamiliaProdutoService,
            private actRouter: ActivatedRoute,
            private modalService: BsModalService,
            private router: Router,
            ) {}

  form: FormGroup = new FormGroup({});
  fornecedor = {} as Fornecedor;
  fornecedorId = {} as any;
  modalRef = {} as BsModalRef;
  familiaProdutos = [{}] as FamiliaProduto[];
  familiaId: number;

  ngOnInit(): void{
    this.validation();
    this.CarregarFornecedor();
    // this.CarregarFamiliaProdutos();
  }

  public validation(): void{
    this.form = new FormGroup({
      nome: new FormControl('', Validators.required),
      cnpj: new FormControl('', Validators.required),
      familiaProdutoId: new FormControl('', Validators.required),
      cep: new FormControl('', Validators.required),
      estado: new FormControl('', Validators.required),
      cidade: new FormControl('', Validators.required),
      endereco: new FormControl('', Validators.required),
      numero: new FormControl('', Validators.required),
      bairro: new FormControl('', Validators.required),
      complemento: new FormControl(''),
      telefone: new FormControl('', Validators.required),
      celular: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      inscricaoMunicipal: new FormControl('', Validators.required),
      inscricaoEstadual: new FormControl('', Validators.required),
      pontuacaoRanking: new FormControl('')
      // tema: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]),
      // qtdePessoas: new FormControl('', [Validators.required, Validators.max(12000)]),
    });
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

  public setFamiliaProduto(famProdId: number): FamiliaProduto{
    const famProds = this.familiaProdutos.filter(
      (fp: FamiliaProduto) => fp.Id === famProdId
      );
    return famProds[0];
  }

  public CarregarFornecedor(): void{
    this.fornecedorId = this.actRouter.snapshot.paramMap.get('id');

    if (this.fornecedorId !== null){
      this.spinner.show();
      this.fornecedorService.getFornecedorById(+this.fornecedorId).subscribe(
        (e: Fornecedor) => {
          this.fornecedor = {...e},
          this.form.patchValue(e);
        },
        (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao tentar carregar o Fornecedor', 'Erro');
          console.error(error);
        },
        () => {
          this.spinner.hide();
        }
      );
    }
  }

  public SalvarFornecedor(): void{
    this.spinner.show();
    if (this.form.valid){
      debugger;
      // this.fornecedor.familiaProduto = this.setFamiliaProduto(this.familiaId);
      if (this.fornecedorId === null){

        this.fornecedor = {...this.form.value};
        this.fornecedorService.postFornecedor(this.fornecedor).subscribe(
          () => {this.toastr.success('Fornecedor salvo com Sucesso', 'Fornecedor Salvo'); },
          (error: any) => {
            console.log(error);
            this.toastr.error('Erro ao tentar salvar Fornecedor', 'Erro');
            this.spinner.hide();
          },
          () => {
            this.spinner.hide();
          }
        );
      }else{
        this.fornecedor = {id: this.fornecedor.id, ...this.form.value};
        this.fornecedorService.putFornecedor(this.fornecedor.id, this.fornecedor).subscribe(
          () => {this.toastr.success('Fornecedor salvo com Sucesso', 'Fornecedor Salvo'); },
          (error: any) => {
            console.log(error);
            this.toastr.error('Erro ao tentar salvar Fornecedor', 'Erro');
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
      this.router.navigate([`/fornecedor/lista`]);
      this.modalRef.hide();
    }

    public CancelarForm(): void{
    }
}

