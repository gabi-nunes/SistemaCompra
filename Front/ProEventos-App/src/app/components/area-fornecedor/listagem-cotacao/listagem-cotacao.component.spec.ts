import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListagemCotacaoComponent } from './listagem-cotacao.component';

describe('ListagemCotacaoComponent', () => {
  let component: ListagemCotacaoComponent;
  let fixture: ComponentFixture<ListagemCotacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListagemCotacaoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListagemCotacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
