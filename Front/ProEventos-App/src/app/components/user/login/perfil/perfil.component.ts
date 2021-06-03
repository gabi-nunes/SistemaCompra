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
      validators: Validator.MustMatch('senhaPas', 'confirmarSenha')
    };
    this.form = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
     email: ['', Validators.required],
      setor: ['', Validators.required],
      senhaPas: ['', Validators.required],
      confirmarSenha: ['', Validators.required],
      cargo: ['', Validators.required],
    },formOptions);
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

  public atualiza(){

      this.user = {id: this.user.id , ...this.form.value}
      debugger
      this.userService.AtualizaUser(this.user.id,this.user).subscribe(

        () => this.toastr.success('Evento salvo com sucesso!', 'sucesso'),

        () => this.spinner.hide()


      );
       this.spinner.hide()

        this.router.navigate(['/user/lista']);

}
Salvarsenha(){

    this.alterarS = false;

    this.login= {id: this.user.id,email: this.user.email, senha: this.form.value.senhaPas}
    debugger;

  this.userService.AlterarSenha(this.login).subscribe(

    () => this.toastr.success('Senha alterada com sucesso!', 'sucesso'),

    () => this.spinner.hide()
  );
  debugger
  this.router.navigate(['/user/lista']);
}
alterarsenha(){
  this.alterarS= false;
}

Cancelar(){
  this.alterarS= true;
}


}
