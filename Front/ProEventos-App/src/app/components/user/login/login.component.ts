import { FornecedorService } from 'src/app/services/fornecedor.service';
import { Fornecedor } from 'src/app/models/Fornecedor';
import { Login } from './../../../models/login';
import { UserService } from 'src/app/services/user.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { user } from 'src/app/models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  log = {} as Login;
  email: string;
  senha: string;
  isUser: boolean =false;
  id: string;
  loginForm: FormGroup;
  userO = {} as user;
  usuario: any;
  Fornecedor: any;
  perfil: any = 0;

  userperfil = new EventEmitter<user>();
  userFornecedor = new EventEmitter<Fornecedor>();

  constructor(private userService: UserService,
              private fb: FormBuilder,
              private router: Router,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService,
              private forncedorService: FornecedorService
              ){}

  ngOnInit(): void {
    this.validation();
  }

  public validation(): void {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      senha: ['', [Validators.required, Validators.max(120000)]],
    });
  }

  public  salvarlogin(): void {
    if (this.loginForm.valid){
      debugger;
      this.userService.getIsUserByEmail(this.email).subscribe(
        (result: any) => {
          this.isUser= result;
          debugger;
          if(this.isUser === true){
            debugger;
            this.usuario = {... this.loginForm.value}
            this.userService.login(this.usuario).subscribe(
              (result: any) => {
                this.toastr.success('Login aceito', 'OK');
                this.router.navigate(['/dashboard']);
                this.spinner.hide();
              },
              (error: any) => {
                console.error(error);
                this.toastr.error('Erro ao tentar entrar, verifique seu email e sua senha!', 'Erro');
              },
              () => this.spinner.hide()
            );
          }
          else{
            debugger;
            this.Fornecedor = {... this.loginForm.value}
            this.forncedorService.login(this.Fornecedor).subscribe(
              () => {
                this.toastr.success('Login aceito', 'OK');
                this.router.navigate(['/areaFornecedor']);
                this.spinner.hide();
              },
              (error: any) => {
                console.error(error);
                this.toastr.error('Erro ao tentar entrar, verifique seu email e sua senha!', 'Erro');
              },
              () => this.spinner.hide()
            );
          }
        },
        (error: any) => {
          console.error(error);
          this.toastr.error('Erro ao tentar entrar, verifique seu email e sua senha!', 'Erro');
        },
        () => this.spinner.hide()
      );
      }
    }
  }
