import { TestBed } from '@angular/core/testing';

import { QueryParameterService } from './query-parameter.service';

describe('QueryParameterService', () => {
  let service: QueryParameterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QueryParameterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
