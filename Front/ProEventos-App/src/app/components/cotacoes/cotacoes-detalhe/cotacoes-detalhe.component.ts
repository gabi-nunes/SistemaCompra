//#region "Imports"
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
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

import { FamiliaProdutoService } from 'src/app/services/familiaProduto.service';
import { SolicitacaoService } from 'src/app/services/solicitacao.service';
import { UserService } from 'src/app/services/user.service';
import { Fornecedor } from 'src/app/models/Fornecedor';
import { CotacaoService } from 'src/app/services/cotacao.service';
import { CotacaoDTO } from 'src/app/models/CotacaoDTO';
import { Cotacao } from 'src/app/models/Cotacao';
import { ItemCotacao } from 'src/app/models/ItemCotacao';
import { PedidoService } from 'src/app/services/pedido.service';
import { ThisReceiver } from '@angular/compiler';

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

  cotacoes: Cotacao[] = [];
  solProdItem: any;
  fornIdeaisObj: any;

  get cotacoesFormArray(): FormArray{
    return this.form.get('cotacoes') as FormArray;
  }
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
  cotador: user;

  public get GridFilter(): string{
    return this.gridFilter;
  }
  public set GridFilter(value: string){
    this.gridFilter = value;
    this.fornecedoresFiltrados = this.gridFilter ? this.Filtrar(this.gridFilter) : this.fornecedoresRanking;
  }

  public get ShowAlert(): boolean{
    if (!this.cotacoes.length) {return false; }
    return this.cotacoes.every(c => c.status == 1);
  }

  public get IsCotacaoEncerrada(): boolean{
    return this.cotacoes.some(c => c.status == 3);
  }

  public get ShowCotacaoes(): boolean{
    if (!this.cotacoes.length) {return false; }
    return this.cotacoes.some(c => c.status == 2);
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
              private cotacaoService: CotacaoService,
              private pedidoService: PedidoService,

  ) { this.localeService.use('pt-br'); }

  ngOnInit(): void{
    this.isCollapsed.fill(true);
    this.CarregarSolicitacao();
    this.CarregarUser(0, true);
    this.getFornIdeais();
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
      user: [this.user?.nome],
      DesFamProd: [this.familiaProduto?.descricao],
      dataSolicitacao: [this.solicitacao.dataSolicitacao ?? this.dataHoje],
      dataNecessidade: [this.solicitacao?.dataNecessidade],
      observacaoRejeicao: [this.solicitacao?.observacaoRejeicao],
      prazoCotacao: ['', Validators.required],
      cotador: [this.cotador?.name],
      frmPagamento: [{value: '', disabled: this.IsCotacaoEncerrada}],
      cotacoes: this.fb.array([])
    });
  }

  // AdicionarCotacaoForm()

  criarCotacao(cotacao: Cotacao): FormGroup{
    let fornName = this.fornecedoresEscolhidos?.find(f => f.id === cotacao.fornecedorId)?.nome;
    return this.fb.group({
      fornecedor: [fornName ?? cotacao.fornecedorId],
      total: [this.getTotalCotacao(cotacao)],
      dataEntrega: [cotacao.dataEntrega],
      frete: [cotacao.frete.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })]
    });
  }

  cssValidation(control: FormControl): any{
    return {'is-invalid': control?.errors && control?.touched};
  }

  cssRotate(isCollapsed: boolean): any{
    return isCollapsed ? {'fas fa-angle-down': true} : {'fas fa-angle-up': true};
  }

  cssStatusValidation(): any{
    return this.solicitacao.statusAprovacao === 1 ? {'is-invalid': true} : {'is-valid': true};
  }

  cssGetIdeal(fornId: number): any{
    return {'border border-success rounded ': this.IsFornIdeal(fornId)};
  }
  public cssTextIdeal(fornId: number): any{
    return this.IsFornIdeal(fornId) ? {'text-success': true} : {'text-muted': true};
  }

  private getFornIdeais(): void{
    if (this.cotacoes.length){
      this.cotacaoService.getFornecedoresIdeais(this.solicitacaoId).subscribe(
        (response: any) => {
          this.fornIdeaisObj = response;
        },
        () => {
          this.spinner.hide(),
            this.toastr.error('Erro a carregar o fornecedor ideal', 'Erro');
        },
        () => this.spinner.hide()
      );
    }
  }

  public IsFornIdeal(fornId: number = 0): boolean{
    if (fornId == 0) {return false; }
    if (this.cotacoes.some(c => c.status == 1)) {return false; }
    let isIdeal = false;
    if (!this.fornIdeaisObj) {return false; }
    Object.keys(this.fornIdeaisObj).forEach(item => {
      if (this.fornIdeaisObj[item] == fornId){
        isIdeal = true;
        return;
      }
    });
    return isIdeal;
  }

  public GetTypeIdeal(fornId: number = 0): number{
    if (this.cotacoes.some(c => c.status == 1)) {return -1}
    if (!this.fornIdeaisObj) {return -1; }
    // if (!this.fornIdeaisObj.fornecedorIdeal){
      if (this.fornIdeaisObj?.fornecedorIdeal == fornId) {return 0; }
    // }
    if (this.fornIdeaisObj?.fornecedorMenorPreco == fornId) {return 1; }
    if (this.fornIdeaisObj?.fornecedorMenorData == fornId) {return 2; }
    return -1;
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
      this.CarregarCotacoesBySolicitacao();

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

public setSolProdItem(solProdItemId: number): number{
  this.solProdItem = this.solicitacaoProdutos.find(sp => sp.id == solProdItemId);
  return this.solProdItem.produto.id;
}

public CarregarCotacoesBySolicitacao(): void{
  this.spinner.show();
  this.cotacaoService.getCotacoesBySolicitacao(+this.solicitacaoId).subscribe(
    (cotacoes: Cotacao[]) => {
      this.cotacoes = cotacoes;
      this.form.patchValue({prazoCotacao: cotacoes[0]?.prazoOfertas,
                            frmPagamento: cotacoes[0]?.frmPagamento});
      if (this.IsCotacaoEncerrada) { this.form.get('frmPagamento')?.disable(); }
      cotacoes.forEach(c => {
        this.cotacoesFormArray.push(this.criarCotacao(c));
      });
      this.spinner.hide();
      this.getFornIdeais();
    },
    (error: any) => {
      this.spinner.hide();
      this.toastr.error('Erro ao tentar carregar as Cotações', 'Erro');
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
      this.form.value.user = this.user.nome;
      this.validation();
    },
    error: () => {
      this.spinner.hide(),
      this.toastr.error('Erro ao carregar os user', 'Erro');
    },
    complete: () => this.spinner.hide()
  });
}

public GerarPedido(cotacaoId: number): void{
  this.spinner.show();
  const pedidoObj = {'cotacaoId': cotacaoId, 'dataEmissao': new Date()};
  this.pedidoService.postRegistrarPedido(pedidoObj).subscribe(
    () => {
      this.toastr.success('Pedido gerado com sucesso', 'Pedido Gerado');
      this.spinner.hide();
      this.router.navigate([`/cotacoes/lista`]);
    },
    (error: any) => {
      console.log(error);
      this.toastr.error('Erro ao tentar Gerar o Pedido', 'Erro');
      this.spinner.hide();
    },
    () => {
      this.spinner.hide();
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
        this.toastr.error('Erro ao carregar família do produto', 'Erro');
      },
      () => this.spinner.hide()
    );
  }

 public NavigateToFornecedor(): void{this.router.navigate([`/fornecedor/detalhe`]); }

public CarregarFornecedoresRanking(): void{

  this.spinner.show();
  let count = 0;
  this.cotacaoService.getFornecedoresRankingByFamProdId(this.familiaProduto.id).subscribe(
    (fornecedores: Fornecedor[]) => {
      fornecedores?.forEach(f => {
        this.fornecedoresRanking.push(f);
        if (count < 3){
          this.fornecedoresEscolhidos.push(f);
          this
          count++;
        }
      });
      this.cotacoesFormArray.controls.forEach(c => {
        c.patchValue({fornecedor: this.fornecedoresRanking.find(f => f.id == c.value.fornecedor)?.nome})
      });
    },
    (error) => {
      console.log(error);
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
    const cotacaoDto = {} as CotacaoDTO;

    if (this.form.valid){
      this.fornecedoresEscolhidos.forEach(f => {
          cotacaoDto.CotadorId = this.cotador.id;
          cotacaoDto.DataEmissaoCotacao = new Date();
          cotacaoDto.fornecedorId = f.id;
          cotacaoDto.status = 1;
          cotacaoDto.prazoOfertas = this.form.value.prazoCotacao;
          cotacaoDto.FrmPagamento = this.form.value.frmPagamento;
          cotacaoDto.Parcelas = 0;
          this.cotacaoService.postRegistrarCotacao(this.solicitacaoId, cotacaoDto).subscribe(
            () => {
              this.toastr.success('Cotações Enviadas com sucesso', 'Cotações Enviadas');
              this.spinner.hide();
              this.router.navigate([`/cotacoes/lista`]);
            },
            (error: any) => {
              console.log(error);
              this.toastr.error('Erro ao tentar Enviar Cotações', 'Erro');
              this.spinner.hide();
            },
            () => {
              this.spinner.hide();
            }
          );
      });
    }
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
    if(!this.fornecedoresEscolhidos.some(f => f.id == novoFornId)){
      this.fornecedoresEscolhidos = this.fornecedoresEscolhidos.filter(f => f.id !== this.FornIdSubstituir);
      const novoForn = this.fornecedoresRanking.find(f => f.id === novoFornId);
      this.fornecedoresEscolhidos.push(novoForn ?? new Fornecedor());
    }
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
    this.router.navigate([`/cotacoes/lista`]);
  }

  public getTotalCotacao(cotacao: Cotacao): string{
    let total: any = 0;
    cotacao.itensCotacao.forEach(ic => {
      total += ic.precoUnit * ic.qtdeProduto;
    });
    total += cotacao.frete;
    return total.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  }

  public GetStatus(): number{
    if (this.cotacoes?.some(c => c.status === 3)) { return 3; }
    else if (this.cotacoes?.every(c => c.itensCotacao?.length > 0)) { return 2; }
    else if (this.cotacoes?.some(c => c.status === 1)) { return 1; }
    return 0;
  }
}


//#endregion
