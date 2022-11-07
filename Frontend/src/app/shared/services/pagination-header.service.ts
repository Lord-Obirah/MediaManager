import { Injectable } from '@angular/core';
import {IPaginationHeader} from "../interfaces/paginationHeader";
import {BehaviorSubject, Observable, Subject} from "rxjs";
import {HttpResponse} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PaginationHeaderService {
  private paginationHeaderBehaviorSubject: BehaviorSubject<IPaginationHeader> = new BehaviorSubject<IPaginationHeader>({} as IPaginationHeader);

  public paginationHeader = this.paginationHeaderBehaviorSubject.asObservable()

  constructor() { }

  public setPaginationHeader(response: HttpResponse<any>): void {
    const paginationHeader = response.headers.get('X-Pagination');
    if(typeof(paginationHeader) === 'string') {
      const paginationHeaderObject = JSON.parse(paginationHeader);
      console.log(paginationHeaderObject);
      this.paginationHeaderBehaviorSubject.next(paginationHeaderObject)
    }
  }
}
