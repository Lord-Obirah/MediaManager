import {Component, OnInit} from '@angular/core';
import {HttpResponse} from "@angular/common/http";
import {BehaviorSubject, debounceTime, mergeMap, Observable, tap} from "rxjs";
import {IPaginationHeader} from "../../../shared/interfaces/paginationHeader";
import {IQueryParameter} from "../../../shared/interfaces/queryParameter";
import {DataService} from "../../../shared/services/data.service";
import {IRecordList} from "../../interfaces/recordList";

@Component({
  templateUrl: './record-list.component.html',
  styleUrls: ['./record-list.component.scss']
})
export class RecordListComponent implements OnInit {
  public title: string = 'CDs';
  public results$: Observable<HttpResponse<IRecordList[]>> = new Observable<HttpResponse<IRecordList[]>>();
  public _listFilterValue: string = '';
  public paginationHeader!: IPaginationHeader;

  private behaviorSubject = new BehaviorSubject<IQueryParameter>({ orderBy: 'Title' })
  private dueTime: number = 500;

  constructor(private dataService: DataService) {
  }

  ngOnInit(): void {
    this.results$ = this.behaviorSubject.pipe(
      debounceTime(this.dueTime),
      mergeMap(queryParameter => this.dataService.getDataList<IRecordList>('records', queryParameter)),
      tap(response => {
        const paginationHeader = response.headers.get('X-Pagination');
        if(typeof(paginationHeader) === 'string') {
          this.paginationHeader = JSON.parse(paginationHeader);
          console.log(this.paginationHeader);
        }
      }));
  }

  public performFiltering(filterValue: string): void {
    filterValue = filterValue.toLowerCase();
    const queryParameter = this.paginationHeader.currentPageLink;
    queryParameter.searchQuery = filterValue;
    queryParameter.page = undefined;
    this.behaviorSubject.next(queryParameter);
  }

  public handleNavigationEvent(event: IQueryParameter): void {
    this.behaviorSubject.next(event);
  }
}
