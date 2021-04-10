import { Component, OnInit} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from 'src/app/models/Evento';
import { EventoService } from 'src/app/services/evento.service';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {
  form = {} as FormGroup;
  evento = {} as Evento;

  constructor(private fb: FormBuilder,
              private localeService: BsLocaleService,
              private actRouter: ActivatedRoute,
              private eventoService: EventoService,
              private spinner: NgxSpinnerService,
              private tostr: ToastrService
              ) {
    this.localeService.use('pt-br');
  }

  public CarregarEvento(): void{
    const eventoId = this.actRouter.snapshot.paramMap.get('id');

    if (eventoId !== null){
      this.spinner.show();
      this.eventoService.getEventoById(+eventoId).subscribe(
        (e: Evento) => {
          this.evento = {...e},
          this.form.patchValue(e);
        },
        (error: any) => {
          this.spinner.hide()
          this.tostr.error('Erro ao tentar carregar o evento', 'Erro');
          console.error(error);
        },
        () => {
          this.spinner.hide();
        }
      );
    }
  }

  ngOnInit(): void{
    this.CarregarEvento();
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
    return {'is-invalid': control?.errors && control?.touched};
  }
}
