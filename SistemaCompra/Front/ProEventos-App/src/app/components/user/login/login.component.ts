import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { user } from 'src/app/models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Login } from 'src/app/models/login';

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
  user={} as user;
  usuario: any;

  constructor(private userService: UserService, private fb: FormBuilder,private router: Router,  private toastr: ToastrService,private spinner: NgxSpinnerService) { }

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
            this.toastr.success('Login aceito', 'OK');
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







