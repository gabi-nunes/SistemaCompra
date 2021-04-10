import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Validator } from 'src/app/helpers/Validator';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {
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
      titulo: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      telefone: ['', Validators.required],
      funcao: ['', Validators.required],
      descricao: [''],
      password: ['', Validators.required],
      pswdConfirm: ['', Validators.required],
    }, formOptions);
  }

}
