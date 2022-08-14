import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RouterModule} from "@angular/router";
import {ListPaginationComponent} from "./list-pagination/list-pagination.component";
import {ToastrModule} from "ngx-toastr";

@NgModule({
  declarations: [
    ListPaginationComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ToastrModule.forRoot(),
  ],
  providers: [
  ],
  exports: [
    ListPaginationComponent,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class SharedModule {
}
