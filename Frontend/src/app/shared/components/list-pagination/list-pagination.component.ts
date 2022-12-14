import {ChangeDetectorRef, Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {debounceTime, map, Subject} from "rxjs";
import {IPaginationHeader} from "../../interfaces/paginationHeader";
import {IQueryParameter} from "../../interfaces/queryParameter";
import {PaginationHeaderService} from "../../services/pagination-header.service";

@Component({
  selector: 'app-list-pagination',
  templateUrl: './list-pagination.component.html',
  styleUrls: ['./list-pagination.component.scss']
})
export class ListPaginationComponent implements OnInit, OnDestroy {
  public subject: Subject<string> = new Subject<string>();
  public paginationHeader: IPaginationHeader;

  /**
   * Allows you to register a click handler that will be invoked when the user clicks on the button.
   */
  @Output()
  public onNavigationButtonClick: EventEmitter<IQueryParameter> = new EventEmitter<IQueryParameter>();

  constructor(private paginationHeaderService: PaginationHeaderService) { }

  ngOnInit(): void {
    this.subject.pipe(
      debounceTime(500),
      map((value) => {
        const page = Number(value);
        if(page && page <= this.paginationHeader.totalPages! && page > 0) {
          const currentPageLink = this.paginationHeader.currentPageLink;
          currentPageLink.page = page;
          this.onClick(currentPageLink);
        }
      })).subscribe();

    this.paginationHeaderService.paginationHeader.subscribe(s => this.paginationHeader = s);
  }

  public onClick(event: IQueryParameter | undefined): void
  {
    this.onNavigationButtonClick.emit(event);
  }

  ngOnDestroy(): void {
    this.subject.unsubscribe();
  }
}
