import { user } from './../../../models/user';
import { Component, NgModule, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
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
  estadoSalvar = 'post';
  senha: boolean = true;
  alterarS: boolean = true;

  constructor(private service: UserService,  private route: ActivatedRoute, private router: Router,private fb: FormBuilder,  private toastr: ToastrService,private spinner: NgxSpinnerService) { }


  ngOnInit(): void {
    this.validation();
  
  }


  public validation(): void {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
     email: ['', Validators.required],
      setor: ['', Validators.required],
      senha: ['', [Validators.required, Validators.max(120000)]],
      cargo: ['', Validators.required],

    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

public salvarUser(): void {
  debugger
  if(this.form.valid){

    if(this.estadoSalvar ==='post'){

      this.usuario = {... this.form.value}

  this.service.RegisterUser(this.usuario).subscribe(

    () => this.toastr.success('Evento salvo com sucesso!', 'sucesso'),

    () => this.spinner.hide()


  );
   this.spinner.hide()
   //colocar um treco de carregar
    this.router.navigate(['/user/lista']);
}

}
}



}



