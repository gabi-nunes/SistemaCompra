import { login } from './../../../models/login';
import { UserService } from 'src/app/services/user.service';
import { Component, OnInit } from '@angular/core';
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
  log = {} as login;
  email: string;
  senha: string;
  loginForm: FormGroup;

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

      this.log = {... this.loginForm.value}

debugger

    this.userService.login(this.log).subscribe(

      () => this.toastr.success('login sucesso!', 'sucesso'),

      () => this.spinner.hide()


    );

  }
  }






}
