import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {ToastrModule} from "ngx-toastr";
import {DxSelectBoxModule, DxTextBoxModule} from 'devextreme-angular';
import {RouterModule} from "@angular/router";
import {ListPaginationComponent} from "./components/list-pagination/list-pagination.component";
import {AppSingleSelectComponent} from './components/app-single-select/app-single-select.component';
import {AppInputComponent} from './components/app-input/app-input.component';
import {AppListComponent} from "./components/app-list/app-list.component";
import {AppDetailComponent} from './components/app-detail/app-detail.component';

@NgModule({
  declarations: [
    ListPaginationComponent,
    AppListComponent,
    AppInputComponent,
    AppSingleSelectComponent,
    AppDetailComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ToastrModule.forRoot(),
    DxTextBoxModule,
    DxSelectBoxModule,
    RouterModule,
    ReactiveFormsModule
  ],
  providers: [],
  exports: [
    ListPaginationComponent,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AppInputComponent,
    AppSingleSelectComponent,
    AppListComponent,
    AppDetailComponent
  ]
})

export class SharedModule {
}
