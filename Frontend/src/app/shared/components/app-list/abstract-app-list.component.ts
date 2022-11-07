import {ChangeDetectorRef, Injectable, Input, OnInit} from '@angular/core';
import {Observable, tap} from "rxjs";
import {HttpResponse} from "@angular/common/http";
import {IPaginationHeader} from "../../interfaces/paginationHeader";
import {PaginationHeaderService} from "../../services/pagination-header.service";
import {ListDataService} from "../../services/list-data.service";

@Injectable()
export abstract class AbstractAppListComponent<T> implements OnInit {
  public response: T[] | null;
  public results$: Observable<HttpResponse<T[]>> = new Observable<HttpResponse<T[]>>();
  public _listFilterValue: string = '';
  public paginationHeader: IPaginationHeader;

  protected constructor(private paginationHeaderService: PaginationHeaderService,
                        private listDataService: ListDataService) {
  }

  ngOnInit(): void {
    this.paginationHeaderService.paginationHeader.subscribe(s => this.paginationHeader = s);
    this.listDataService.listData.subscribe(s => this.response = s);
  }

  public getRowNumber(index: number): number {
    return (((this.paginationHeader.currentPage ? this.paginationHeader.currentPage : 1) - 1) * (this.paginationHeader.pageSize ? this.paginationHeader.pageSize : 50)) + (index + 1)
  }
}
