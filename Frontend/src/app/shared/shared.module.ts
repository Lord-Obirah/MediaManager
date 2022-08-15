import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {ListPaginationComponent} from "./list-pagination/list-pagination.component";
import {ToastrModule} from "ngx-toastr";
import { AppInputComponent } from './components/app-input/app-input.component';

import { DxTextBoxModule } from 'devextreme-angular';

@NgModule({
  declarations: [
    ListPaginationComponent,
    AppInputComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ToastrModule.forRoot(),
    DxTextBoxModule
  ],
  providers: [
  ],
  exports: [
    ListPaginationComponent,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AppInputComponent
  ]
})
export class SharedModule {
}
