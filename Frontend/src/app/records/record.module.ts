import {NgModule} from '@angular/core';
import {RouterModule} from "@angular/router";
import {RecordListComponent} from "./components/record-list/record-list.component";
import {RecordDetailComponent} from "./components/record-detail/record-detail.component";
import {SharedModule} from "../shared/shared.module";

@NgModule({
  declarations: [
    RecordListComponent,
    RecordDetailComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild([
      {path: 'movies', component: RecordListComponent},
      {path: 'movies/:id', component: RecordDetailComponent},
    ])
  ],
})
export class RecordModule {
}
