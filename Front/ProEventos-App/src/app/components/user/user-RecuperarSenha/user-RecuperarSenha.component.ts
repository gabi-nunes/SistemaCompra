import { Login } from './../../../models/login';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr'
import { user } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

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
  recuperarFrom: FormGroup;
  user={} as user;
  usuario: any;
  constructor(private userService: UserService, private fb: FormBuilder,private router: Router,  private toastr: ToastrService,private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.validation();
  }

  public validation(): void {
    this.recuperarFrom= this.fb.group({
      email: ['', Validators.required],
    });
  }

  RecuperarSenha(){
debugger
if (this.email  !== null) {
  // this.spinner.show();
   this.userService.getUserByEmail(this.email ).subscribe(
     (usuario: user) => {
       this.user= {...usuario}
       this.recuperarFrom.patchValue(this.user);
       this.log= {id: this.user.id,email: this.user.email,senha: this.user.senha}
       debugger
         this.userService.RecuperarSenha(this.log).subscribe(

           () => this.toastr.success('Email enviado com sucesso!', 'sucesso'),

           () => this.spinner.hide()
         );
         this.router.navigate(['/user/login']);
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
}
