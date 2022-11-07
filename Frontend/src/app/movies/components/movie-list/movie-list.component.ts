import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {IMovieList} from "../../interfaces/movieList";
import {AbstractAppListComponent} from "../../../shared/components/app-list/abstract-app-list.component";
import {PaginationHeaderService} from "../../../shared/services/pagination-header.service";
import {ListDataService} from "../../../shared/services/list-data.service";

@Component({
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.scss']
})
export class MovieListComponent extends AbstractAppListComponent<IMovieList> implements OnInit {
  constructor(paginationHeaderService: PaginationHeaderService,
              listDataService: ListDataService) {
    super(paginationHeaderService, listDataService);
  }

  override ngOnInit(): void {
   super.ngOnInit()
  }
}
