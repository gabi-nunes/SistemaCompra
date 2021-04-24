import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsLocaleService, DateFormatter } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FamiliaProduto } from 'src/app/models/FamiliaProduto';
import { Produto } from 'src/app/models/Produto';
import { Solicitacao } from 'src/app/models/Solicitacao';
import { SolicitacaoProduto } from 'src/app/models/SolicitacaoProduto';
import { user } from 'src/app/models/user';
import { FamiliaProdutoService } from 'src/app/services/familiaProduto.service';
import { ProdutoService } from 'src/app/services/produto.service';
import { SolicitacaoService } from 'src/app/services/solicitacao.service';

@Component({
  selector: 'app-solicitacoes-detalhe',
  templateUrl: './solicitacoes-detalhe.component.html',
  styleUrls: ['./solicitacoes-detalhe.component.scss']
})
export class SolicitacoesDetalheComponent implements OnInit {
  constructor(private spinner: NgxSpinnerService,
              private toastr: ToastrService,
              private familiaProdService: FamiliaProdutoService,
              private actRouter: ActivatedRoute,
              private modalService: BsModalService,
              private router: Router,
              private fb: FormBuilder,
              private solicitacaoService: SolicitacaoService,
              private produtoService: ProdutoService,
              private localeService: BsLocaleService
  ) { this.localeService.use('pt-br'); }

  user: user;
  produtoId: number;
  produtos: Produto[];
  ProdSelecionado: Produto;
  qtdeProd: number;

  form: FormGroup = new FormGroup({});
  solicitacao = {} as Solicitacao;
  solicitacaoId = {} as any;

  modalRefQtde = {} as BsModalRef;
  modalRefProd = {} as BsModalRef;
  modalRef = {} as BsModalRef;

  familiaProdutos = [{}] as FamiliaProduto[];
  familiaId: number;
  dataHoje = new Date();
  solicitacaoProdutos: SolicitacaoProduto[] = [];

  ngOnInit(): void{
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.user = JSON.parse(userJson);
    this.validation();
    this.CarregarSolicitacao();
    this.CarregarProdutos();

  // this.CarregarFamiliaProdutos();
  }


  get f(): any{
    return this.form.controls;
  }

  public validation(): void{
    this.form = this.fb.group({
      id: ['312', Validators.required],
      user: [this.user.name, Validators.required],
      familiaProdutoId: ['', Validators.required],
      dataSolicitacao: [this.dataHoje, Validators.required],
      dataNecessidade: ['', Validators.required],
      dataAprovacao: [''],
      observacao: [''],
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

  public CarregarSolicitacao(): void{
    this.solicitacaoId = this.actRouter.snapshot.paramMap.get('id');

    if (this.solicitacaoId !== null){
    this.spinner.show();
    this.solicitacaoService.getSolicitacaoById(+this.solicitacaoId).subscribe(
      (e: Solicitacao) => {
        this.solicitacao = {...e},
        this.form.patchValue(e);
      },
      (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao tentar carregar a Solicitacao', 'Erro');
        console.error(error);
      },
      () => {
        this.spinner.hide();
      }
      );
    }
  }

  public CarregarProdutos(): void{
    this.produtoService.getProdutos().subscribe({
      next: (produtosResponse: Produto[]) => {
        this.produtos = produtosResponse;
      },
      error: () => {
        this.spinner.hide(),
        this.toastr.error('Erro ao carregar os Produtos', 'Erro');
      },
      complete: () => this.spinner.hide()
    });
  }

  cssValidation(control: FormControl): any{
    return {'is-invalid': control?.errors && control?.touched};
  }

  get bsConfig(): any{
    return {
      dateInputFormat: 'DD/MM/YYYY',
      adaptivePosition: true,
      showWeekNumbers: false,
    };
  }

  public setFamiliaProduto(famProdId: number): FamiliaProduto{
    const famProds = this.familiaProdutos.filter(
    (fp: FamiliaProduto) => fp.Id === famProdId
    );
    return famProds[0];
  }

  public SalvarSolicitacao(): void{
    this.spinner.show();
    if (this.form.valid){
      if (this.solicitacaoId === null){
        this.solicitacao = {...this.form.value};
        this.solicitacaoService.postSolicitacao(this.solicitacao).subscribe(
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
        this.solicitacao = {id: this.solicitacao.id, ...this.form.value};
        this.solicitacaoService.putSolicitacao(this.solicitacao.id, this.solicitacao).subscribe(
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
    this.router.navigate([`/solicitacao/lista`]);
    this.modalRef.hide();
  }

  public CancelarForm(): void{
  }

  public IncluirItemSolic(): void{
    debugger;
    this.modalRefQtde.hide();
    this.modalRefProd.hide();
    const prodEscolhido = this.produtos.filter((p: Produto) => p.id === this.produtoId);
    this.ProdSelecionado = prodEscolhido[0];
    const solItem = new SolicitacaoProduto();
    solItem.produto = this.ProdSelecionado;
    solItem.produtoid = this.produtoId;
    solItem.qtdeProduto = this.qtdeProd;
    this.solicitacaoProdutos.push(solItem);
    this.qtdeProd = 0;
  }

  public OpenProdutosModal(template: TemplateRef<any>): void{
    this.modalRefProd = this.modalService.show(template, {class: 'modal-lg modal-dialog-centered'});
  }
  CloseModalProdutos(): void{
    this.modalRefProd.hide();
  }

  OpenQtdeModal(template: TemplateRef<any>, prodId: number): void{
    this.produtoId = prodId;
    this.modalRefQtde = this.modalService.show(template, {class: 'modal-sm modal-dialog-centered'});
  }
  CloseModalQtde(): void{
    this.modalRefQtde.hide();
  }
}
