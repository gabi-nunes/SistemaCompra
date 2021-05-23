import { EnviarOferta } from './../../../models/EnviarOferta';
import { ItemCotacao } from './../../../models/ItemCotacao';
import { Cotacao } from 'src/app/models/Cotacao';
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
import { SolicitacaoDTO } from 'src/app/models/SolicitacaoDTO';
import { SolicitacaoProduto } from 'src/app/models/SolicitacaoProduto';
import { SolicitacaoProdutoDTO } from 'src/app/models/SolicitacaoProdutoDTO';
import { user } from 'src/app/models/user';
import { CotacaoService } from 'src/app/services/cotacao.service';
import { FamiliaProdutoService } from 'src/app/services/familiaProduto.service';
import { ProdutoService } from 'src/app/services/produto.service';
import { SolicitacaoService } from 'src/app/services/solicitacao.service';
import { UserService } from 'src/app/services/user.service';
import { preco } from 'src/app/models/preco';




@Component({
  selector: 'app-detalhe-cotacao',
  templateUrl: './detalhe-cotacao.component.html',
  styleUrls: ['./detalhe-cotacao.component.scss']
})
export class DetalheCotacaoComponent implements OnInit {


  produtoId: number;
  frete: string;
dataEntrega: Date;
  produtos: Produto[] = [];
  ProdSelecionado: Produto;
  qtdeProd: number;
  iscolumn: boolean;
  isvalid: boolean = false;
  cotacaoid: number;
  DataEmissaoFormatada: string;
  itemCotacaoid: number;
  valor: string;
  precoUnit={} as preco;


  item: any;
  form: FormGroup = new FormGroup({});
  cotacao = {} as Cotacao;
  solicitacaoId = {} as any;

  modalRefQtde = {} as BsModalRef;
  modalRefProd = {} as BsModalRef;
  modalRef = {} as BsModalRef;
  modalRefAprovacao = {} as BsModalRef;
  modalRefCancel = {} as BsModalRef;

  familiaProdutos: FamiliaProduto[] = [];
  familiaId: number;
  produto: Produto[]=[];
  dataHoje = new Date();
  total: number =0;
  dateMaior: Boolean= false;
  valores:any[]=[];
  aparecer: boolean = false;
  totalItem: number;
  itemCotacaoEnvia: ItemCotacao[]=[]
  now = new Date;

  solProdIdExluidos: number[] = [];
  itensCotacoes: ItemCotacao[] = [];
  solicitacaoProdutosOriginal: SolicitacaoProduto[] = [];

  isCadastro: boolean;
  isExpirada: boolean;

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
              private produtoService: ProdutoService,
              private cotacaoService: CotacaoService,
  ) { this.localeService.use('pt-br');}

  ngOnInit(): void{
    this.actRouter.params.subscribe(params => this.cotacaoid = params['id']);
    this.CarregarCotacaoes();
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
      id: [this.cotacao?.id],
      dataEmissaoCotacao: [this.cotacao?.dataEmissaoCotacao],
      prazoOfertas: [this.cotacao?.prazoOfertas],
      dataEntrega:[this.cotacao?.dataEntrega,Validators.required],
      frete: [this.cotacao?.frete,Validators.required],
      total: [this.cotacao?.total.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })]
    });
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

public CarregarCotacaoes(): void{

  // tslint:disable-next-line: deprecation
  this.cotacaoService.getCotacaoById(this.cotacaoid).subscribe(
    (CotacaoResponse: Cotacao) => {
      this.cotacao = CotacaoResponse;
      this.form.patchValue({total: this.cotacao.total.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })});
      this.cotacao.itensCotacao.forEach(itemcotacao => {
        this.Carregarprodutos();
        this.itensCotacoes.push(itemcotacao);
      });
      debugger
      console.log(this.itensCotacoes);
      this.validation();
    },
    () => {
      this.spinner.hide(),
      this.toastr.error('Erro ao carregar os Cotacoes', 'Erro');
    },
    () => this.spinner.hide()
  );

}
public getDescricaoproduto(idProduto: number): string{
  let produtoSelecionado = this.produto.find(x=> x.id== idProduto);
  var descProduto= produtoSelecionado?.descricao ?? '';
  return descProduto;
}
public Carregarprodutos(): void{
  // tslint:disable-next-line: deprecation
  this.produtoService.getProdutos().subscribe(
    (produtoResponse: Produto[]) => {
      this.produto = produtoResponse;
    },
    () => {
      this.spinner.hide(),
      this.toastr.error('Erro ao carregar os Cotacoes', 'Erro');
    },
    () => this.spinner.hide()
  );


}

public get ShowAlert(): boolean{
  if (this.cotacao.prazoDias < 0 ) {
    this.isExpirada= true;
    return false; }
    this.isExpirada= false;
  return true;
}

//#endregion
//#region "Salvar"

//#endregion
//#region "Modal"
  public OpenModal(template: TemplateRef<any>, solProdId: number): void{
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
    this.solProdIdExluidos.push(solProdId);
  }

  cssValidation(control: FormControl): any{
    return {'is-invalid': control?.errors && control?.touched};
  }

  public OpenModalCancel(template: TemplateRef<any>): void{
    this.modalRefCancel = this.modalService.show(template, {class: 'modal-sm'});
  }

  public CancelarForm(): void{
    this.modalRefCancel.hide()
    this.router.navigate([`/solicitações/lista`]);
  }

  public EnviarOfertaFornecedor(): void{
    const enviaOf = new EnviarOferta();
    enviaOf.frete= this.frete;
    enviaOf.dataEntrega= this.dataEntrega;

    this.itensCotacoes.forEach(item=>{
      this.precoUnit.itemcotacao= item.id;
      this.precoUnit.preco	=item.precoUnit;
      this.precoUnit.total= item.totalItem;
      debugger
      this.cotacaoService.EnviarPreçoItem(this.precoUnit).subscribe(
        (result: any) => {
        },
        (error: any) => {
          console.error(error);
          this.toastr.error('Falha ao tentar adicionar', 'Erro');
          this.spinner.hide();
        },
        () => {
          this.spinner.hide()
        },
      );
    });

    this.cotacaoService.enviar(this.cotacaoid, enviaOf).subscribe(
      (result: any) => {
        this.toastr.success('Enviado com sucesso', 'Sucesso!');
      },
      () => {}
    );
    this.router.navigate([`/areaFornecedor/listaCotacao`]);

  }


  public IncluirPrecoItem(valor: string){
    debugger
    let item : any;
    let valorRecebido: number;
  valorRecebido = parseFloat(valor.replace(",", "."));
  debugger
    let itemValor= this.itemCotacaoEnvia.find(x=>x.id == this.itemCotacaoid);
    if( itemValor != undefined){
        this.cotacao.total-= itemValor.totalItem;
        this.itemCotacaoEnvia.splice(this.itemCotacaoEnvia.indexOf(itemValor), 1);
    }

    item= this.itensCotacoes.find(x=>x.id == this.itemCotacaoid);
    this.qtdeProd= item.qtdeProduto;
    item.precoUnit=  valorRecebido;
    item.totalItem=  valorRecebido * this.qtdeProd;
    console.log(valorRecebido)
    this.itemCotacaoEnvia.push(item);
    console.log(this.itemCotacaoEnvia)
    let principal= this.itensCotacoes.find(x=>x.id == this.itemCotacaoid);
    this.cotacao.total += item.totalItem;
    this.form.patchValue({total: this.cotacao.total.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })});

    console.log(this.cotacao.total);
    this.valor='';
    this.CloseModalQtde();
  }

  onKey(frete: string) { // without type info
    var freteFormatado = parseFloat(frete);
    this.cotacao.total += freteFormatado ;
    this.form.patchValue({total: this.cotacao.total.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })});
    debugger
  }



  public OpenProdutosModal(itemcotacaoid:number, template: TemplateRef<any>): void{
    this.itemCotacaoid= itemcotacaoid;
    this.modalRefProd = this.modalService.show(template, {class: 'modal-lg modal-dialog-centered'});
  }


  OpenQtdeModal(template: TemplateRef<any>, prodId: number): void{
    const prodEscolhido = this.produtos.filter((p: Produto) => p.id === prodId);
    this.ProdSelecionado = prodEscolhido[0];
    this.produtoId = prodId;
  }
  CloseModalQtde(): void{
    this.modalRefProd.hide();
  }


  onMudouEvento(evento: any): void{
    console.log(evento);
  }



  GerarRelatrio(): void{

    window.print();

  }
//#endregion
}


