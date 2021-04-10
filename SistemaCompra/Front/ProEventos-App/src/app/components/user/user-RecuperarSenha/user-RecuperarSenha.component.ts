import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { login } from 'src/app/models/login';
import { user } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-RecuperarSenha',
  templateUrl: './user-RecuperarSenha.component.html',
  styleUrls: ['./user-RecuperarSenha.component.scss']
})
export class UserRecuperarSenhaComponent implements OnInit {
  log = {} as login;
  email: string;
  senha: string;
  id: string;
  recuperarFrom: FormGroup;
  user={} as user;
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

    this.user = {... this.recuperarFrom.value}

    this.userService.AlterarSenha(this.user.id, this.user.email, this.user).subscribe(

      () => this.toastr.success('Email enviado com sucesso!', 'sucesso'),

      () => this.spinner.hide()
    );
    this.router.navigate(['/user/login']);
}

}
