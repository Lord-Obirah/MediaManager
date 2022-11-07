import { Injectable } from '@angular/core';
import {BehaviorSubject} from "rxjs";
import {HttpResponse} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ListDataService {
  private listDataBehaviorSubject: BehaviorSubject<any[]> = new BehaviorSubject<any[]>([]);

  public listData = this.listDataBehaviorSubject.asObservable()

  constructor() { }

  public setListData(response: HttpResponse<any>): void {
      this.listDataBehaviorSubject.next(response.body)
  }
}
