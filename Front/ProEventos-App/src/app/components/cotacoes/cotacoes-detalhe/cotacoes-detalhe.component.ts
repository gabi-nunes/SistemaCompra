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
import { SolicitacaoService } from 'src/app/services/solicitacao.service';
import { UserService } from 'src/app/services/user.service';
import { Fornecedor } from 'src/app/models/Fornecedor';
import { CotacaoService } from 'src/app/services/cotacao.service';

@Component({
  selector: 'app-cotacoes-detalhe',
  templateUrl: './cotacoes-detalhe.component.html',
  styleUrls: ['./cotacoes-detalhe.component.scss']
})
//#endregion

export class CotacoesDetalheComponent implements OnInit {
//#region "Variáveis"
  user: user;
  aprovador: user;
  produtoId: number;
  produtos: Produto[] = [];
  ProdSelecionado: Produto;
  qtdeProd: number;

  form: FormGroup = new FormGroup({});
  solicitacao = {} as Solicitacao;
  solicitacaoId = {} as any;

  modalRefQtde = {} as BsModalRef;
  modalRefForn = {} as BsModalRef;
  modalRef = {} as BsModalRef;
  modalRefAprovacao = {} as BsModalRef;
  modalRefCancel = {} as BsModalRef;

  familiaProduto = {} as FamiliaProduto;
  familiaId: number;

  dataHoje = new Date();

  solicitacaoProdutos: SolicitacaoProduto[] = [];
  solicitacaoProdutosOriginal: SolicitacaoProduto[] = [];

  fornecedoresRanking: Fornecedor[] = [];
  fornecedoresEscolhidos: Fornecedor[] = [];
  FornIdSubstituir = 0;

  isCadastro: boolean;
  isCollapsed: boolean[] = [];
  private gridFilter = '';
  fornecedoresFiltrados: Fornecedor[] = [];
  cotador: any;

  public get GridFilter(): string{
    return this.gridFilter;
  }
  public set GridFilter(value: string){
    this.gridFilter = value;
    this.fornecedoresFiltrados = this.gridFilter ? this.Filtrar(this.gridFilter) : this.fornecedoresRanking;
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
              private userService: UserService,
              private cotacaoService: CotacaoService
  ) { this.localeService.use('pt-br'); }

  ngOnInit(): void{
    this.isCollapsed.fill(true);
    this.CarregarSolicitacao();
    this.CarregarUser(0, true);
    this.validation();
  }
//#endregion
//#region "Helpers"
  public Filtrar(filter: string): Fornecedor[]{
    filter = filter.toLocaleLowerCase();
    return this.fornecedoresRanking.filter(
      (forn: any) => forn.nome.toLocaleLowerCase().indexOf(filter) !== -1

    );
  }

  public validation(): void{
    const df = new DateFormatter();
    this.form = this.fb.group({
      id: [this.solicitacaoId],
      user: [this.user?.name],
      DesFamProd: [this.familiaProduto?.descricao],
      dataSolicitacao: [this.solicitacao.dataSolicitacao ?? this.dataHoje],
      dataNecessidade: [this.solicitacao?.dataNecessidade],
      observacaoRejeicao: [this.solicitacao?.observacaoRejeicao],
      prazoCotacao: ['', Validators.required],
      cotador: [this.cotador?.name],
      fornecedor: [],
      frete: [],
      total: []
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

  cssRotate(isCollapsed: boolean): any{
    if (isCollapsed) {
      return {'fas fa-angle-down': true};
    }
    return {'fas fa-angle-up': true};
  }

  cssStatusValidation(): any{
    if (this.solicitacao.statusAprovacao === 1) {return {'is-invalid': true}; }
    return {'is-valid': true};
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

public CarregarSolicitacao(): void{
  this.spinner.show();
  this.solicitacaoId = this.actRouter.snapshot.paramMap.get('id');

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
      // this.CarregarAprovador(s.idAprovador);
      this.CarregarFamiliaProduto();

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

public CarregarUser(userId: number = 0, isCotador = false): void{
  if (isCotador) {
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.cotador = JSON.parse(userJson);
    return;
  }

  this.userService.getUserById(userId).subscribe({
    next: (userResponse: user) => {
      this.user = userResponse;
      this.form.value.user = this.user.name;
      this.validation();
    },
    error: () => {
      this.spinner.hide(),
      this.toastr.error('Erro ao carregar os user', 'Erro');
    },
    complete: () => this.spinner.hide()
  });
}

public CarregarFamiliaProduto(): void{
    this.spinner.show();
    this.familiaProdService.getFamiliaProdutoById(this.familiaId).subscribe(
      (famProd: FamiliaProduto) => {
        this.familiaProduto = famProd;
        this.validation();
        this.CarregarFornecedoresRanking();
      },
      () => {
        this.spinner.hide(),
        this.toastr.error('Erro a família do produto', 'Erro');
      },
      () => this.spinner.hide()
    );
  }

  public CarregarFornecedoresRanking(): void{
    this.spinner.show();
    let count = 0;
    this.cotacaoService.getFornecedoresRankingByFamProdId(this.familiaProduto.id).subscribe(
      (fornecedores: Fornecedor[]) => {
        fornecedores.forEach(f => {
          this.fornecedoresRanking.push(f);
          if (count < 3){
            this.fornecedoresEscolhidos.push(f);
            count++;
          }

        });
      },
      () => {
        this.spinner.hide(),
        this.toastr.error('Erro a carregar os fornecedores', 'Erro');
      },
      () => this.spinner.hide()
    );
  }

//#endregion
//#region "Salvar"
  public EnviarCotacoes(): void{
    this.spinner.show();
    const solicitacaoDto = {} as SolicitacaoDTO;

    if (this.form.valid){
      if (this.isCadastro){
        this.solicitacao = {...this.form.value};
        debugger;
        solicitacaoDto.dataNecessidade = this.solicitacao.dataNecessidade;
        solicitacaoDto.dataSolicitacao = this.solicitacao.dataSolicitacao;
        solicitacaoDto.observacao = this.solicitacao.observacao;
        solicitacaoDto.statusAprovacao = 2;
        solicitacaoDto.observacao = '';
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

//#endregion
//#region "Modal"
  public OpenModal(template: TemplateRef<any>): void{
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  OpenModalAprovacao(template: TemplateRef<any>): void{
    this.modalRefAprovacao = this.modalService.show(template, {class: 'modal-md modal-dialog-centered'});
  }

  OpenModalFornecedores(template: TemplateRef<any>, fornId: number): void{
    this.modalRefForn = this.modalService.show(template, {class: 'modal-lg modal-dialog-centered'});
    this.FornIdSubstituir = fornId;
  }

  SubstituirFornecedor(novoFornId: number): void{
    this.fornecedoresEscolhidos = this.fornecedoresEscolhidos.filter(f => f.id !== this.FornIdSubstituir);
    const novoForn = this.fornecedoresRanking.find(f => f.id === novoFornId);
    this.fornecedoresEscolhidos.push(novoForn ?? new Fornecedor());
    this.modalRefForn.hide();
  }

  CloseModalAprovacao(): void{
    this.modalRefAprovacao.hide();
  }

  CloseModalFornecedores(): void{
    this.modalRefForn.hide();
  }

  decline(): void {this.modalRef.hide(); }
  declineCancel(): void {this.modalRefCancel.hide(); }

  confirm(): void {
    this.modalRef.hide();
  }

  public OpenModalCancel(template: TemplateRef<any>): void{
    this.modalRefCancel = this.modalService.show(template, {class: 'modal-sm'});
  }

  public CancelarForm(): void{
    this.modalRefCancel.hide()
    this.router.navigate([`/solicitações/lista`]);
  }

  public IncluirItemSolic(): void{
    this.modalRefQtde.hide();
    this.modalRefForn.hide();

    const solItem = new SolicitacaoProduto();
    solItem.produto = this.ProdSelecionado;
    solItem.produtoid = this.produtoId;
    solItem.qtdeProduto = this.qtdeProd;
    this.solicitacaoProdutos.push(solItem);
    this.qtdeProd = 0;
  }
}
//#endregion
