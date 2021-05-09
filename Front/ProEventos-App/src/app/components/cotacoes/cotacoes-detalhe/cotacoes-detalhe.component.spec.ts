import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CotacoesDetalheComponent } from './cotacoes-detalhe.component';

describe('CotacoesDetalheComponent', () => {
  let component: CotacoesDetalheComponent;
  let fixture: ComponentFixture<CotacoesDetalheComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CotacoesDetalheComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CotacoesDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
