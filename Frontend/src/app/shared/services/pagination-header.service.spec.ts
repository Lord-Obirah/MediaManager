import { TestBed } from '@angular/core/testing';

import { PaginationHeaderService } from './pagination-header.service';

describe('PaginationHeaderService', () => {
  let service: PaginationHeaderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PaginationHeaderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
