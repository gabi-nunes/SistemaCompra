import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Validator } from 'src/app/helpers/Validator';

@Component({
  selector: 'app-user-cadastro',
  templateUrl: './user-cadastro.component.html',
  styleUrls: ['./user-cadastro.component.scss']
})
export class UserCadastroComponent implements OnInit {
  form: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void{
    this.validation();
  }

  get f(): any{
    return this.form.controls;
  }

  public validation(): void{
    const formOptions: AbstractControlOptions = {
      validators: Validator.MustMatch('password', 'pswdConfirm')
    };

    this.form = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      userName: ['', Validators.required],
      password: ['', Validators.required],
      pswdConfirm: ['', Validators.required],
    }, formOptions);
  }


}
