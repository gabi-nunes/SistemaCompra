import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { user } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-User-detalhe',
  templateUrl: './User-detalhe.component.html',
  styleUrls: ['./User-detalhe.component.scss']
})
export class UserDetalheComponent implements OnInit {
  form: FormGroup = new FormGroup({});
  usuario = {} as user;
  constructor(private service: UserService, private router: Router,private fb: FormBuilder,  private toastr: ToastrService,private spinner: NgxSpinnerService) { }

  ngOnInit() {
  }
  public validation(): void{
    this.form = new FormGroup({
      nome: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]),
      email: new FormControl('', [Validators.required, Validators.email]),
      Setor: new FormControl('', Validators.required),
      Cargo: new FormControl('', Validators.required),

    });
  }
  public salvarUser(): void {
      if(this.form.valid){

        this.usuario = {... this.form.value}

      this.service.AtualizaUser(this.usuario.id,this.usuario).subscribe(

        () => this.toastr.success('Evento salvo com sucesso!', 'sucesso'),

        () => this.spinner.hide()


      );
       this.spinner.hide()
       //colocar um treco de carregar
        this.router.navigate(['/user/lista']);
    }
    }

}




