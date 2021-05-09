import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CotacoesListaComponent } from './cotacoes-lista.component';

describe('CotacoesListaComponent', () => {
  let component: CotacoesListaComponent;
  let fixture: ComponentFixture<CotacoesListaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CotacoesListaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CotacoesListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
