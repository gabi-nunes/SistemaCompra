import { Login } from './../../../models/Login';
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
  id: string;
  loginForm: FormGroup;
  userO={} as user;
  usuario: any;
  perfil: any = 0;

  userperfil = new EventEmitter<user>()

constructor(private userService: UserService, private fb: FormBuilder,private router: Router,  private toastr: ToastrService,private spinner: NgxSpinnerService, private logService: loginService) 
{
  
}

  ngOnInit(): void {
    this.validation();
  }

  public validation(): void {
    this.loginForm= this.fb.group({
      email: ['', Validators.required],
      senha: ['', [Validators.required, Validators.max(120000)]],

    });
  }

//   myFormGroup: FormGroup = this.fb.group({
//     dob: ['', Validators.required]
// });

  public  salvarlogin(): void {
    if(this.loginForm.valid){

      this.usuario = {... this.loginForm.value}

debugger

    this.userService.login(this.usuario).subscribe(
        (result: any)=>{
            console.log(result);
          this.perfil = result;

          this.logService.passandoUser.subscribe(
            this.logService.addUser(this.perfil)
            );
          
          debugger
          this.userperfil.emit(this.perfil);
            this.toastr.success('Login aceito', 'OK');
            this.router.navigate(['/user/lista']);

            this.spinner.hide();
        },
        (error: any)=>{
          console.error(error);
          this.toastr.error('Erro ao tentar entrar, verifique seu email e sua senha!', 'Erro');
        },
        ()=>   this.spinner.hide(),
      );

  }


}


  }








