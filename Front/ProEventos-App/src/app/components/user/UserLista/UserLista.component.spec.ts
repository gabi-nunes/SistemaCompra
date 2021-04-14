/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UserListaComponent } from './UserLista.component';

describe('UserListaComponent', () => {
  let component: UserListaComponent;
  let fixture: ComponentFixture<UserListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
