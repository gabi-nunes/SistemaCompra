/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CotacaoService } from './cotacao.service';

describe('Service: Cotacao', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CotacaoService]
    });
  });

  it('should ...', inject([CotacaoService], (service: CotacaoService) => {
    expect(service).toBeTruthy();
  }));
});
