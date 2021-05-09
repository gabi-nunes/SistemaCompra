import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AprovarSolicitacaoComponent } from './aprovar-solicitacao.component';

describe('AprovarSolicitacaoComponent', () => {
  let component: AprovarSolicitacaoComponent;
  let fixture: ComponentFixture<AprovarSolicitacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AprovarSolicitacaoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AprovarSolicitacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
