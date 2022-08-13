import {Component, OnInit} from '@angular/core';
import {DataService} from "../../services/data.service";
import {IMovieList} from "../../interfaces/movieList";
import {BehaviorSubject, debounceTime, mergeMap, Observable, tap} from "rxjs";
import {HttpResponse} from "@angular/common/http";
import {IPaginationHeader} from "../../interfaces/paginationHeader";
import {IQueryParameter} from "../../interfaces/queryParameter";
import {QueryParameterService} from "../../services/query-parameter.service";

@Component({
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.scss']
})
export class MovieListComponent implements OnInit {
  public title: string = 'Filme';
  public results$: Observable<HttpResponse<IMovieList[]>> = new Observable<HttpResponse<IMovieList[]>>();
  public _listFilterValue: string = '';
  public paginationHeader!: IPaginationHeader;

  private behaviorSubject = new BehaviorSubject<IQueryParameter>({ orderBy: 'Title' })
  private dueTime: number = 500;

  public get listFilterValue(): string {
    return this._listFilterValue;
  }

  public set listFilterValue(input: string)
  {
    this._listFilterValue = input;
    this.performFiltering(input);
  }

  constructor(private dataService: DataService) {
  }

  ngOnInit(): void {
    this.results$ = this.behaviorSubject.pipe(
      debounceTime(this.dueTime),
      mergeMap(queryParameter => this.dataService.getDataList<IMovieList>('movies', queryParameter)),
      tap(response => {
        const paginationHeader = response.headers.get('X-Pagination');
        if(typeof(paginationHeader) === 'string') {
          this.paginationHeader = JSON.parse(paginationHeader);
          console.log(this.paginationHeader);
        }
      }));
  }

  private performFiltering(filterValue: string): void {
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
