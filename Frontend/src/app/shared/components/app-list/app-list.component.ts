import {Component, Input, OnInit} from '@angular/core';
import {BehaviorSubject, debounceTime, mergeMap, Observable, tap} from "rxjs";
import {HttpResponse} from "@angular/common/http";
import {IPaginationHeader} from "../../interfaces/paginationHeader";
import {IQueryParameter} from "../../interfaces/queryParameter";
import {DataService} from "../../services/data.service";
import {PaginationHeaderService} from "../../services/pagination-header.service";

@Component({
  selector: 'app-list',
  templateUrl: './app-list.component.html',
  styleUrls: ['./app-list.component.scss']
})
export class AppListComponent<T> implements OnInit {
  @Input()
  public title: string;
  @Input()
  public entityType: string;
  @Input()
  public orderBy: string = 'Title';
  @Input()
  public headerColumns: string[];

  public results$: Observable<HttpResponse<T[]>> = new Observable<HttpResponse<T[]>>();
  public _listFilterValue: string = '';

  public paginationHeader: IPaginationHeader;
  private behaviorSubject: BehaviorSubject<IQueryParameter>;
  private dueTime: number = 500;

  public constructor(private dataService: DataService,
                     private paginationHeaderService: PaginationHeaderService) {
  }

  ngOnInit(): void {
    this.behaviorSubject = new BehaviorSubject<IQueryParameter>({ orderBy: this.orderBy })
    this.results$ = this.behaviorSubject.pipe(
      debounceTime(this.dueTime),
      mergeMap(queryParameter => this.dataService.getDataList<T>(this.entityType, queryParameter)),
      tap(response => {

      }));
      this.paginationHeaderService.paginationHeader.subscribe(s => this.paginationHeader = s);
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
