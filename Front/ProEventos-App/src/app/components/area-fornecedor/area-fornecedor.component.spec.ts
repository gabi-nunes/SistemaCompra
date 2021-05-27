import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AreaFornecedorComponent } from './area-fornecedor.component';

describe('AreaFornecedorComponent', () => {
  let component: AreaFornecedorComponent;
  let fixture: ComponentFixture<AreaFornecedorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AreaFornecedorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AreaFornecedorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
