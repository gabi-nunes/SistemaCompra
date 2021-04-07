/* tslint:disable:no-unused-variable */

import { TestBed, async } from '@angular/core/testing';
import { DateTimeFormatterPipe } from './DateTimeFormatter.pipe';

describe('Pipe: DateTimeFormattere', () => {
  it('create an instance', () => {
    let pipe = new DateTimeFormatterPipe('pt-BR');
    expect(pipe).toBeTruthy();
  });
});
