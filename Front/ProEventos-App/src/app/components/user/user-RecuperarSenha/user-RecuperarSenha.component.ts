import { Login } from './../../../models/login';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr'
import { user } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { FornecedorService } from 'src/app/services/fornecedor.service';
import { Fornecedor } from 'src/app/models/Fornecedor';

@Component({
  selector: 'app-user-RecuperarSenha',
  templateUrl: './user-RecuperarSenha.component.html',
  styleUrls: ['./user-RecuperarSenha.component.scss']
})
export class UserRecuperarSenhaComponent implements OnInit {
  log = {} as Login;
  email: string;
  senha: string;
  id: string;
  isUser: boolean = false;
  recuperarFrom: FormGroup;
  user={} as user;
  fornecedor={} as Fornecedor;
  usuario: any;
  constructor(private userService: UserService,
              private fb: FormBuilder,
              private router: Router,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService,
              private fornecedorService: FornecedorService) { }

  ngOnInit(): void {
    this.validation();
  }

  public validation(): void {
    this.recuperarFrom= this.fb.group({
      email: ['', Validators.required],
    });
  }

RecuperarSenha(){
  this.spinner.show();
  if (this.email  !== null) {
    this.userService.getIsUserByEmail(this.email).subscribe(
      (result: any) => {
        this.isUser= result;

        if(this.isUser){
        this.userService.getUserByEmail(this.email ).subscribe(
          (usuario: user) => {
            this.user= {...usuario}
            this.recuperarFrom.patchValue(this.user);
            this.log= {id: this.user.id,email: this.user.email,senha: this.user.senha}
            this.userService.RecuperarSenha(this.log).subscribe(
              () => {this.toastr.success('Email enviado com sucesso!', 'sucesso'),
                    this.spinner.hide(),
                    this.router.navigate(['/user/login']);
                    },
              () => this.spinner.hide(),
              () => this.spinner.hide()
            );
          },
          (error: any) => {
            this.spinner.hide();
            this.toastr.error('Erro ao tentar carregar Usuario.', 'Erro!');
            console.error(error);
          },
          () => this.spinner.hide(),
        );
        }else{
          this.fornecedorService.getByEmail(this.email ).subscribe(
            (fornecedor: Fornecedor) => {
              this.fornecedor= {...fornecedor}
              this.recuperarFrom.patchValue(this.fornecedor);
              this.log= {id: this.fornecedor.id,email: this.fornecedor.email,senha: this.fornecedor.senha}
              this.fornecedorService.RecuperarSenha(this.log).subscribe(
                () => {this.toastr.success('Email enviado com sucesso!', 'sucesso'),
                      this.spinner.hide(),
                      this.router.navigate(['/user/login']);
                      },
                () => this.spinner.hide(),
                () => this.spinner.hide()
              );
            },
            (error: any) => {
              this.spinner.hide();
              this.toastr.error('Usuário não Encontrado!', 'Erro!');
              console.error(error);
            },
            () => this.spinner.hide(),
          );
      }
}
  );
  }
  }
}
