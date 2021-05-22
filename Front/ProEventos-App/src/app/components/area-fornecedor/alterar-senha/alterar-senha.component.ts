import { FornecedorService } from 'src/app/services/fornecedor.service';
import { FornecedorListaComponent } from './../../fornecedor/fornecedor-lista/fornecedor-lista.component';
import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Fornecedor } from 'src/app/models/Fornecedor';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { Login } from 'src/app/models/login';
import { Validator } from 'src/app/helpers/Validator';


@Component({
  selector: 'app-alterar-senha',
  templateUrl: './alterar-senha.component.html',
  styleUrls: ['./alterar-senha.component.scss']
})
export class AlterarSenhaComponent implements OnInit {

  public user: Fornecedor;

  public userId = 0;
  form: FormGroup;
  public isvalid: boolean= false;
  fornecedor = {} as Fornecedor;
  fornecedorId = {} as any;
  alterarS: boolean = true;
  txtSenha: string;
  senha: string;
  confimarSenha: string;
  txtConfirmar: string;
  login:Login;



  constructor(
    private fornecedorService: FornecedorService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router,
    private fb: FormBuilder,
    private route: ActivatedRoute) { }

  ngOnInit() {
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.user = JSON.parse(userJson);
    this.validation();
    this.CarregarFornecedor();
  }

  public validation(): void{
    const formOptions: AbstractControlOptions = {
      validators: Validator.MustMatch('senhaPas', 'confirmarSenha')
    };
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
      pontuacaoRanking: new FormControl(this.fornecedor?.pontuacaoRanking ?? 0),
      senhaPas: new FormControl('', Validators.required),
      confirmarSenha: new FormControl('', Validators.required),
      },formOptions);
    }

    get f(): any{
      return this.form.controls;
    }

  public CarregarFornecedor(): void{
    this.fornecedorId = this.route.snapshot.paramMap.get('id');
    debugger
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

  Salvarsenha(){

    this.alterarS= false;

    this.login= {id: this.user.id,email: this.user.email, senha: this.form.value.senhaPas}
    debugger;

  this.fornecedorService.AlterarSenha(this.login).subscribe(

    () => this.toastr.success('Senha alterada com sucesso!', 'sucesso'),

    () => this.spinner.hide()
  );
  debugger
  this.router.navigate(['/areaFornecedor/ListaCotacao']);
}

alterarsenha(){
  this.alterarS= false;
}

Cancelar(){
  this.alterarS= true;
}
}
