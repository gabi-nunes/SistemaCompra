/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FamiliaProdutoService } from './familiaProduto.service';

describe('Service: FamiliaProduto', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FamiliaProdutoService]
    });
  });

  it('should ...', inject([FamiliaProdutoService], (service: FamiliaProdutoService) => {
    expect(service).toBeTruthy();
  }));
});
