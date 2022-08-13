import { Injectable } from '@angular/core';
import {IQueryParameter} from "../interfaces/queryParameter";
import {IPaginationHeader} from "../interfaces/paginationHeader";
import {query} from "@angular/animations";

@Injectable({
  providedIn: 'root'
})
export class QueryParameterService {

  constructor() { }

  public GetQueryParameterFromUrl(event: string, paginationHeader: IPaginationHeader, defaultOrder: string = 'Name'): IQueryParameter
  {
    const queryParameter = event == 'current' ? paginationHeader.currentPageLink : (event == 'next' ? paginationHeader.nextPageLink : paginationHeader.previousPageLink);

    return queryParameter ? queryParameter : { orderBy: defaultOrder};
  }
}
