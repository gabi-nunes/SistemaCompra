import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolicitacoesDetalheComponent } from './solicitacoes-detalhe.component';

describe('SolicitacoesDetalheComponent', () => {
  let component: SolicitacoesDetalheComponent;
  let fixture: ComponentFixture<SolicitacoesDetalheComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SolicitacoesDetalheComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SolicitacoesDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
