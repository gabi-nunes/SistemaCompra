import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { EventoService } from './evento.service';
import { UserService } from './user.service';

describe('Service: user', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserService]
    });
  });

  it('should ...', inject([UserService], (service: EventoService) => {
    expect(service).toBeTruthy();
  }));
});
