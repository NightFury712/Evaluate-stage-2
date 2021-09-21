import { TestBed } from '@angular/core/testing';

import { ImportfileService } from './importfile.service';

describe('ImportfileService', () => {
  let service: ImportfileService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ImportfileService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
