import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {ToastrModule} from "ngx-toastr";

import {DxSelectBoxModule, DxTextBoxModule} from 'devextreme-angular';
import {ListPaginationComponent} from "./list-pagination/list-pagination.component";
import {AppSingleSelectComponent} from './components/app-single-select/app-single-select.component';
import {AppInputComponent} from './components/app-input/app-input.component';

@NgModule({
  declarations: [
    ListPaginationComponent,
    AppInputComponent,
    AppSingleSelectComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ToastrModule.forRoot(),
    DxTextBoxModule,
    DxSelectBoxModule
  ],
  providers: [],
  exports: [
    ListPaginationComponent,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AppInputComponent,
    AppSingleSelectComponent
  ]
})

export class SharedModule {
}
