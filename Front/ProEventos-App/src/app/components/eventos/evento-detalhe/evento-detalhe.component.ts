import { Component, OnInit} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {
  form: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder,
              private localeService: BsLocaleService
              ) {
                this.localeService.use('pt-br')
              }

  ngOnInit(): void{
    this.validation();
  }

  get f(): any{
    return this.form.controls;
  }

  get bsConfig(): any{
    return {
      dateInputFormat: 'MM/DD/YYYY - h:mm',
      adaptivePosition: true,
      showWeekNumbers: false,

    };
  }


  public validation(): void{
    this.form = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdePessoas: ['', [Validators.required, Validators.max(12000), Validators.pattern('^[0-9]*$')]],
      imagemURL: [''],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  public ResetForm(): void{
    this.form.reset();
  }

  cssValidation(control: FormControl): any{
    return {'is-invalid': control?.errors && control?.touched}
  }
}
