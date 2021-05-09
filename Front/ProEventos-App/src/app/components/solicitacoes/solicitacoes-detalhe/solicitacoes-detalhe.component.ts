//#region "Imports"
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BsLocaleService, DateFormatter } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { FamiliaProduto } from 'src/app/models/FamiliaProduto';
import { Solicitacao } from 'src/app/models/Solicitacao';
import { SolicitacaoProduto } from 'src/app/models/SolicitacaoProduto';
import { Produto } from 'src/app/models/Produto';
import { user } from 'src/app/models/user';

import { SolicitacaoDTO } from 'src/app/models/SolicitacaoDTO';
import { SolicitacaoProdutoDTO } from 'src/app/models/SolicitacaoProdutoDTO';

import { FamiliaProdutoService } from 'src/app/services/familiaProduto.service';
import { ProdutoService } from 'src/app/services/produto.service';
import { SolicitacaoService } from 'src/app/services/solicitacao.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-solicitacoes-detalhe',
  templateUrl: './solicitacoes-detalhe.component.html',
  styleUrls: ['./solicitacoes-detalhe.component.scss']
})
//#endregion

export class SolicitacoesDetalheComponent implements OnInit {
//#region "Variáveis"
  user: user;
  produtoId: number;
  produtos: Produto[] = [];
  ProdSelecionado: Produto;
  qtdeProd: number;
  isvalid: boolean = false;

  form: FormGroup = new FormGroup({});
  solicitacao = {} as Solicitacao;
  solicitacaoId = {} as any;

  modalRefQtde = {} as BsModalRef;
  modalRefProd = {} as BsModalRef;
  modalRef = {} as BsModalRef;

  familiaProdutos: FamiliaProduto[] = [];
  familiaId: number;
  dataHoje = new Date();

  solProdIdExluidos: number[] = [];
  solicitacaoProdutos: SolicitacaoProduto[] = [];
  solicitacaoProdutosOriginal: SolicitacaoProduto[] = [];

  isCadastro: boolean;

  private gridFilter = '';
  produtosFiltrados: Produto[] = [];

  public get GridFilter(): string{
    return this.gridFilter;
  }
  public set GridFilter(value: string){
    this.gridFilter = value;
    this.produtosFiltrados = this.gridFilter ? this.Filtrar(this.gridFilter) : this.produtos;
  }

  get f(): any{
    return this.form.controls;
  }
//#endregion
//#region "Constructor"
  constructor(private spinner: NgxSpinnerService,
              private toastr: ToastrService,
              private actRouter: ActivatedRoute,
              private modalService: BsModalService,
              private router: Router,
              private fb: FormBuilder,
              private localeService: BsLocaleService,
              private familiaProdService: FamiliaProdutoService,
              private solicitacaoService: SolicitacaoService,
              private produtoService: ProdutoService,
              private userService: UserService,
  ) { this.localeService.use('pt-br'); }

  ngOnInit(): void{
    this.CarregarSolicitacao();
    this.CarregarFamiliaProdutos();
    this.validation();
  }
//#endregion
//#region "Helpers"
  public Filtrar(filter: string): Produto[]{
    filter = filter.toLocaleLowerCase();
    return this.produtos.filter(
      (produto: any) => produto.descricao.toLocaleLowerCase().indexOf(filter) !== -1

    );
  }

  public validation(): void{
    const df = new DateFormatter();
    this.form = this.fb.group({
      id: [this.solicitacaoId, Validators.required],
      user: [this.user?.name, Validators.required],
      familiaProdutoId: [this.solicitacaoProdutos[0]?.produto.familiaProdutoId, Validators.required],
      dataSolicitacao: [this.solicitacao.dataSolicitacao ?? this.dataHoje, Validators.required],
      dataNecessidade: [df.format(this.solicitacao?.dataNecessidade, 'dd/MM/yyyy', 'pt-br'), Validators.required],
      dataAprovacao: [this.solicitacao?.dataAprovacao],
      observacao: [this.solicitacao?.observacao],
    });
  }

  private PegarUltimoId(): void{
    this.solicitacaoService.getLastId().subscribe(
      (li: number) => {
        this.solicitacaoId = li + 1;
        this.validation();
      },
      (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao tentar carregar a Última Solicitacao', 'Erro');
        console.error(error);
      },
      () => {
        this.spinner.hide();
      }
      );
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
//#endregion
//#region "Carregar"
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
  this.spinner.show();
  this.solicitacaoId = this.actRouter.snapshot.paramMap.get('id');

  if (this.solicitacaoId === null){
    this.isCadastro = true;
    this.PegarUltimoId();
    this.CarregarUser();
    return;
  }

  this.isCadastro = false;

  this.solicitacaoService.getSolicitacaoById(+this.solicitacaoId).subscribe(
    (s: Solicitacao) => {
      this.solicitacao = {...s},
      this.familiaId = s.solicitacaoProdutos[0].produto.familiaProdutoId;
      this.solicitacaoProdutos = [];
      this.form.patchValue(s);
      this.CarregarUser(s.user_id);
      s.solicitacaoProdutos.forEach(item => {
        this.solicitacaoProdutos.push(item);
        this.solicitacaoProdutosOriginal.push(item);
      });
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

public CarregarProdutos(): void{
  this.produtoService.getProdutosByFamiliaProdId(this.familiaId).subscribe({
    next: (produtosResponse: Produto[]) => {
      this.produtos = produtosResponse;
      this.produtosFiltrados = produtosResponse;
    },
    error: () => {
      this.spinner.hide(),
      this.toastr.error('Erro ao carregar os Produtos', 'Erro');
    },
    complete: () => this.spinner.hide()
  });
}

public CarregarUser(userId: number = 0): void{
  const isRegistro = this.actRouter.snapshot.paramMap.get('id') === null;
  if (isRegistro) {
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.user = JSON.parse(userJson);
    return;
  }

  this.userService.getUserById(userId).subscribe({
    next: (userResponse: user) => {
      this.user = userResponse;
      this.form.value.user = this.user.name;
      this.validation();
      console.log(this.form);
      console.log(`Imprimindo o Usuário DENTRO do Carregar Usuário:`);
      console.log(userResponse);
      console.log(`Imprimindo o FORM DENTRO do Carregar Usuário:`);
      console.log(this.form);
      console.log(this.solicitacaoProdutos);
      // this.solicitacaoProdutos.forEach(item => {
      //   console.log(item.produto.descricao);
      // });
    },
    error: () => {
      this.spinner.hide(),
      this.toastr.error('Erro ao carregar os user', 'Erro');
    },
    complete: () => this.spinner.hide()
  });
}
//#endregion
//#region "Salvar"
  public SalvarSolicitacao(): void{
    this.spinner.show();
    const solicitacaoDto = {} as SolicitacaoDTO;
    if (this.solProdIdExluidos.length > 0){
      this.DeletarItens();
    }

    if (this.form.valid){
      if (this.isCadastro){
        this.solicitacao = {...this.form.value};
        debugger;
        solicitacaoDto.dataNecessidade = this.solicitacao.dataNecessidade;
        solicitacaoDto.dataSolicitacao = this.solicitacao.dataSolicitacao;
        solicitacaoDto.observacao = this.solicitacao.observacao;
        solicitacaoDto.statusAprovacao = 2;
        this.solicitacaoService.postSolicitacao(this.user.id, solicitacaoDto).subscribe(
          () => {
            this.toastr.success('Solicitação salva com Sucesso', 'Solicitação Salva');
            this.SalvarSolicitacaoProd();
          },
          (error: any) => {
            console.log(error);
            this.toastr.error('Erro ao tentar salvar Solicitação', 'Erro');
            this.spinner.hide();
          },
          () => {
            this.spinner.hide();
          }
        );
      }else{
        this.solicitacao = {id: this.solicitacao.id, ...this.form.value};
        solicitacaoDto.id = this.solicitacao.id;
        solicitacaoDto.dataNecessidade = this.solicitacao.dataNecessidade;
        solicitacaoDto.dataSolicitacao = this.solicitacao.dataSolicitacao;
        solicitacaoDto.observacao = this.solicitacao.observacao;
        solicitacaoDto.statusAprovacao = 2;
        this.solicitacaoService.putSolicitacao(this.solicitacao.id, solicitacaoDto).subscribe(
          () => {
            this.toastr.success('Solicitação Atualizada com Sucesso', 'Solicitação Atualizada');
            this.SalvarSolicitacaoProd();
          },
          (error: any) => {
            console.log(error);
            this.toastr.error('Erro ao tentar Atualizar Solicitação', 'Erro');
            this.spinner.hide();
          },
          () => {
            this.spinner.hide();
          }
        );
      }
    }
  }

  SalvarSolicitacaoProd(): void {
    let solicitacaoProdDto = {} as SolicitacaoProdutoDTO;
    solicitacaoProdDto = new SolicitacaoProdutoDTO();
    const solProdIdsOriginais: number[] = this.solicitacaoProdutosOriginal.map((s) => s.produto.id);

    this.solicitacaoProdutos.forEach(solProd => {
      if (solProdIdsOriginais.includes(solProd.produto.id)){
        solicitacaoProdDto.qtdeProduto = solProd.qtdeProduto;
        solicitacaoProdDto.produtoId = solProd.produto.id;
        this.solicitacaoService.putSolicitacaoProd(solProd.id, solicitacaoProdDto).subscribe(
          () => {},
          (error: any) => {
            console.log(error);
            this.toastr.error('Erro ao tentar salvar Item de Solicitação', 'Erro');
            this.spinner.hide();
          },
          () => {
            this.spinner.hide();
          }
        );
      }else{
        solicitacaoProdDto.qtdeProduto = solProd.qtdeProduto;
        solicitacaoProdDto.produtoId = solProd.produto.id;
        this.solicitacaoService.postSolicitacaoProd(this.solicitacaoId, solicitacaoProdDto).subscribe(
          () => {},
          (error: any) => {
            console.log(error);
            this.toastr.error('Erro ao tentar salvar Item de Solicitação', 'Erro');
            this.spinner.hide();
          },
          () => {
            this.spinner.hide();
          }
        );
      }
    });
    this.router.navigate([`/solicitações/lista`]);
  }
  private DeletarItens(): void{
    this.solProdIdExluidos.forEach(itemExcluido => {
      this.solicitacaoService.deleteSolicitacaoProduto(itemExcluido).subscribe(
        () => {
          this.spinner.hide();
        },
        (error: any) => {
          console.error(error);
          this.toastr.error('Falha ao tentar excluir o Item', 'Erro');
          this.spinner.hide();
        },
        () => { }
      );
    });
  }
//#endregion
//#region "Modal"
  public OpenModal(template: TemplateRef<any>, solProdId: number): void{
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
    this.solProdIdExluidos.push(solProdId);
  }

  decline(): void {this.modalRef.hide(); }

  confirm(): void {
    this.solicitacaoProdutos = this.solicitacaoProdutos.filter(s => !this.solProdIdExluidos.includes(s.id));
    this.modalRef.hide();
  }


  public CancelarForm(): void{
  }

  public IncluirItemSolic(): void{
    this.modalRefQtde.hide();
    this.modalRefProd.hide();

    const solItem = new SolicitacaoProduto();
    solItem.produto = this.ProdSelecionado;
    solItem.produtoid = this.produtoId;
    solItem.qtdeProduto = this.qtdeProd;
    this.solicitacaoProdutos.push(solItem);
    this.qtdeProd = 0;
  }

  public OpenProdutosModal(template: TemplateRef<any>): void{
    this.CarregarProdutos();
    this.modalRefProd = this.modalService.show(template, {class: 'modal-lg modal-dialog-centered'});
  }
  CloseModalProdutos(): void{
    this.modalRefProd.hide();
  }

  OpenQtdeModal(template: TemplateRef<any>, prodId: number): void{
    const prodEscolhido = this.produtos.filter((p: Produto) => p.id === prodId);
    this.ProdSelecionado = prodEscolhido[0];
    this.produtoId = prodId;
    this.modalRefQtde = this.modalService.show(template, {class: 'modal-sm modal-dialog-centered'});
  }
  CloseModalQtde(): void{
    this.modalRefQtde.hide();
  }

  GerarRelatrio(): void{
    window.print();
  }
//#endregion
}
