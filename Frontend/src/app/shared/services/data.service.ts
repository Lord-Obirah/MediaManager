import { Injectable } from '@angular/core';
import {catchError, map, Observable, of, tap, throwError} from "rxjs";
import {HttpClient, HttpErrorResponse, HttpResponse} from "@angular/common/http";
import {IQueryParameter} from "../interfaces/queryParameter";

@Injectable({
  providedIn: 'root'
})
export class DataService {

  public apiUrl: string = 'http://localhost:5002/api'

  constructor(private http: HttpClient) { } //TODO Destroy object to unsubscibe

  public updateData<T>(entityType: string, id: string, data: T): Observable<HttpResponse<T>> {
    let url = this.getUrl(entityType, id);
    return id ? this.http.put<T>(url, data, { observe: 'response' })
      .pipe(
        tap(data => console.log('All: ', JSON.stringify(data, null, 4))),
        catchError(this.handleError)
      )
    :
    this.http.post<T>(url, data, { observe: 'response' })
      .pipe(
        tap(data => console.log('All: ', JSON.stringify(data, null, 4))),
        catchError(this.handleError)
      );
  }

  public getData<T>(entityType: string, id: string | null): Observable<HttpResponse<T>> {
    let url = this.getUrl(entityType, id);

    return this.http.get<T>(url, { observe: 'response' })
      .pipe(
        tap(data => console.log('All: ', JSON.stringify(data, null, 4))),
        catchError(this.handleError)
      );
  }

  public getDataList<T>(entityType: string, queryParameter: IQueryParameter | undefined = undefined): Observable<HttpResponse<T[]>> {
      let url = this.getUrl(entityType, null, queryParameter);

      return this.http.get<T[]>(url, { observe: 'response' })
        .pipe(
        tap(data => console.log('All: ', JSON.stringify(data, null, 4))),
          catchError(this.handleError)
      );
  }

  private getUrl(entityType: string, id: string | null, queryParameter: IQueryParameter | null = null): string {
    let getParams = '';

    if(queryParameter) {
      let searchQuery = queryParameter.searchQuery?.trim();
      let orderBy = queryParameter.orderBy?.trim();
      let page = queryParameter.page;
      let pageSize = queryParameter.pageSize;

      let queryParams: string[] = [];

      if (searchQuery && searchQuery.length > 0) {
        queryParams.push(`searchQuery=${searchQuery}`);
      }
      if (orderBy && orderBy.length > 0) {
        queryParams.push(`orderBy=${orderBy}`);
      }
      if (page) {
        queryParams.push(`page=${page}`);
      }
      if (pageSize) {
        queryParams.push(`pageSize=${pageSize}`);
      }
      getParams = queryParams.length > 0 ? `?${queryParams.join('&')}` : '';
    }

    return `${this.apiUrl}/${entityType}${(id ? `/${id}` : '')}${getParams}`;
  }

  private handleError<T>(error: HttpErrorResponse): Observable<never> {
    let errorMessage = '';
    if(error.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${error.error.message}`;
    } else {
      errorMessage = `Server returned code: ${error.status}, error message is: ${error.message}`
    }
    console.error(errorMessage);
    return throwError(() => new Error(errorMessage))
  }
}
