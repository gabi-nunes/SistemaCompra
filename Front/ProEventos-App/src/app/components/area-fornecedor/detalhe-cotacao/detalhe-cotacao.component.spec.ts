import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalheCotacaoComponent } from './detalhe-cotacao.component';

describe('DetalheCotacaoComponent', () => {
  let component: DetalheCotacaoComponent;
  let fixture: ComponentFixture<DetalheCotacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetalheCotacaoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DetalheCotacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
