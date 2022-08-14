import {NgModule} from '@angular/core';
import {RouterModule} from "@angular/router";
import {MovieListComponent} from "./components/movie-list/movie-list.component";
import {MovieDetailComponent} from "./components/movie-detail/movie-detail.component";
import {SharedModule} from "../shared/shared.module";

@NgModule({
  declarations: [
    MovieListComponent,
    MovieDetailComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild([
      {path: 'movies', component: MovieListComponent},
      {path: 'movies/:id', component: MovieDetailComponent},
    ])
  ],
})
export class MovieModule {
}
