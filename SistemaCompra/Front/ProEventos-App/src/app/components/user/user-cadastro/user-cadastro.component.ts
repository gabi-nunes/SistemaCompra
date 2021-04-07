import { user } from './../../../models/user';
import { Component, NgModule, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { CompileTemplateMetadata } from '@angular/compiler';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
;

@Component({
  selector: 'app-user-cadastro',
  templateUrl: './user-cadastro.component.html',
  styleUrls: ['./user-cadastro.component.scss']
})
export class UserCadastroComponent implements OnInit {

  usuario = {} as user;
  form: FormGroup;
  order=0;

  constructor(private service: UserService, private router: Router,private fb: FormBuilder,  private toastr: ToastrService,private spinner: NgxSpinnerService) { }


  ngOnInit(): void {
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      Name: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      email: ['', Validators.required],
      setor: ['', Validators.required],
      senha: ['', [Validators.required, Validators.max(120000)]],
      cargo: ['', Validators.required],

    });
  }



public salvarUser(): void {
  if(this.form.valid){

    this.usuario = {... this.form.value}

  this.service.RegisterUser(this.usuario).subscribe(

    () => this.toastr.success('Evento salvo com sucesso!', 'sucesso'),

    () => this.spinner.hide()


  );

}
}
}
