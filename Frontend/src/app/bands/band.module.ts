import {NgModule} from '@angular/core';
import {RouterModule} from "@angular/router";
import {BandListComponent} from "./components/band-list/band-list.component";
import {BandDetailComponent} from "./components/band-detail/band-detail.component";
import {SharedModule} from "../shared/shared.module";

@NgModule({
  declarations: [
    BandListComponent,
    BandDetailComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild([
      {path: 'bands', component: BandListComponent},
      {path: 'bands/:id', component: BandDetailComponent},
    ])
  ],
})
export class BandModule {
}
