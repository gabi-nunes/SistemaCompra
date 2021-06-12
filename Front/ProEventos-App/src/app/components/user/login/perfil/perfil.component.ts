import { Component, Input, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { user } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Login } from 'src/app/models/login';
import { Validator } from 'src/app/helpers/Validator';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {
  public user: user;
  public UserFiltrados: user[] = [];
  public userId = 0;
  form: FormGroup;
  formSenha: FormGroup;
  public isvalid: boolean= false;
  senha: string;
  alterarS: boolean = true;
  txtSenha: string;
  txtConfirmar: string;
  login:Login;


  constructor(  private userService: UserService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router,
    private fb: FormBuilder,
    private route: ActivatedRoute) { }

  ngOnInit() {
    const userJson = localStorage.getItem('currentUser') || '{}';
    this.user = JSON.parse(userJson);
    this.validation();
    this.carregarUser();
  }

  public validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: Validator.MustMatch('senha', 'confirmarSenha')
    };
    this.form = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      email: ['', Validators.required],
      setor: ['', Validators.required],
      cargo: [''],
      senha: [this.alterarS ? this.user?.senha : '', Validators.required],
      confirmarSenha: [this.alterarS ? this.user?.senha : '', Validators.required],
    }, formOptions);
  }

  get f(): any{
    return this.form.controls;
  }

  public carregarUser(): void {

    const userIdParam = this.user.id;
    if (userIdParam  !== null) {
     // this.spinner.show();
      this.userService.getUserById(userIdParam).subscribe(
        (usuario: user) => {
          this.user= {...usuario};
          debugger
          this.form.patchValue(this.user);
        },

        (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao tentar carregar Evento.', 'Erro!');
          console.error(error);
        },
        () => this.spinner.hide(),
      );
    }
  }

  public AtualizaUser(){

      this.user = {id: this.user.id , ...this.form.value}
      this.user.cargo = this.form.value.cargo;
      this.userService.AtualizaUser(this.user.id,this.user).subscribe(
        () => {
          debugger;
          this.toastr.success('Usuário salvo com sucesso!', 'Sucesso');
          if (!this.alterarS){
            this.Salvarsenha();
          }else{  this.router.navigate(['/dashboard']);}
        },
        () => {
          this.toastr.error('Erro ao salvar Usuário!', 'Erro'),
          this.spinner.hide();
        },
        () => this.spinner.hide()
      );


}
  Salvarsenha(): void{
    this.alterarS = false;
    debugger;
    this.login = {id: this.user.id,email: this.user.email, senha: this.form.value.senha};
    this.userService.AlterarSenha(this.login).subscribe(
      () => {
        this.toastr.success('Senha alterada com sucesso!', 'Sucesso');
        this.router.navigate(['/dashboard']);
      },
      () => {
        this.toastr.error('Erro ao alterar a senha!', 'Erro');
        this.spinner.hide();
      },
      () => this.spinner.hide()
    );
  }
alterarsenha(){
  this.alterarS= false;
  debugger;
  this.form.patchValue({senha: '', confirmarSenha: ''});
}

Cancelar(){
  this.router.navigate(['/user/lista']);
}


}
