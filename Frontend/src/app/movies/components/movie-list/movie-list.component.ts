import {Component, OnInit} from '@angular/core';
import {IMovieList} from "../../interfaces/movieList";
import {DataService} from "../../../shared/services/data.service";
import {AppListComponent} from "../../../shared/components/app-list/app-list.component";

@Component({
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.scss']
})
export class MovieListComponent extends AppListComponent<IMovieList> implements OnInit {
  constructor(dataService: DataService) {
    super(dataService);
  }

  override ngOnInit(): void {
   super.ngOnInit()
  }
}
