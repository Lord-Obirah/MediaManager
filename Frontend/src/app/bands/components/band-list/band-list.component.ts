import {Component, OnInit} from '@angular/core';
import {HttpResponse} from "@angular/common/http";
import {BehaviorSubject, debounceTime, mergeMap, Observable, tap} from "rxjs";
import {IPaginationHeader} from "../../../shared/interfaces/paginationHeader";
import {IQueryParameter} from "../../../shared/interfaces/queryParameter";
import {DataService} from "../../../shared/services/data.service";
import {IBandList} from "../../interfaces/bandList";

@Component({
  templateUrl: './band-list.component.html',
  styleUrls: ['./band-list.component.scss']
})
export class BandListComponent implements OnInit {
  public title: string = 'Bands';
  public results$: Observable<HttpResponse<IBandList[]>> = new Observable<HttpResponse<IBandList[]>>();
  public _listFilterValue: string = '';
  public paginationHeader!: IPaginationHeader;

  private behaviorSubject = new BehaviorSubject<IQueryParameter>({ orderBy: 'Name' })
  private dueTime: number = 500;

  constructor(private dataService: DataService) {
  }

  ngOnInit(): void {
    this.results$ = this.behaviorSubject.pipe(
      debounceTime(this.dueTime),
      mergeMap(queryParameter => this.dataService.getDataList<IBandList>('bands', queryParameter)),
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
