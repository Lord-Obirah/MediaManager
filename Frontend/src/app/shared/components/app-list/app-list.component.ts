import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {BehaviorSubject, debounceTime, mergeMap, Observable, tap} from "rxjs";
import {HttpResponse} from "@angular/common/http";
import {IMovieList} from "../../../movies/interfaces/movieList";
import {IPaginationHeader} from "../../interfaces/paginationHeader";
import {IQueryParameter} from "../../interfaces/queryParameter";
import {DataService} from "../../services/data.service";

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

  @Output()
  public listResponse: EventEmitter<HttpResponse<T[]>> = new EventEmitter<HttpResponse<T[]>>();

  public response: T[] | null;
  public results$: Observable<HttpResponse<T[]>> = new Observable<HttpResponse<T[]>>();
  public _listFilterValue: string = '';
  public paginationHeader!: IPaginationHeader;

  private behaviorSubject: BehaviorSubject<IQueryParameter>;
  private dueTime: number = 500;

  public constructor(private dataService: DataService) {
  }

  ngOnInit(): void {
    this.behaviorSubject = new BehaviorSubject<IQueryParameter>({ orderBy: this.orderBy })
    this.results$ = this.behaviorSubject.pipe(
      debounceTime(this.dueTime),
      mergeMap(queryParameter => this.dataService.getDataList<T>(this.entityType, queryParameter)),
      tap(response => {
        this.getPaginationHeader(response);
        this.listResponse.emit(response);
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

  public handleListResponse(listResponse: HttpResponse<T[]>){
    this.response = listResponse.body;
    this.getPaginationHeader(listResponse);
  }

  public getRowNumber(index: number): number {
    return (((this.paginationHeader.currentPage ? this.paginationHeader.currentPage : 1) - 1) * (this.paginationHeader.pageSize ? this.paginationHeader.pageSize : 50)) + (index + 1)
  }

  protected getPaginationHeader(response: HttpResponse<T[]>): void
  {
      const paginationHeader = response.headers.get('X-Pagination');
      if(typeof(paginationHeader) === 'string') {
        this.paginationHeader = JSON.parse(paginationHeader);
        console.log(this.paginationHeader);
      }
  }
}
