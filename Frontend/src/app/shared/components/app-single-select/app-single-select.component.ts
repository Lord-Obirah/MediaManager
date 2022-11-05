import {Component, EventEmitter, Host, Input, OnInit, Optional, Output, SkipSelf} from '@angular/core';
import {AppAbstractValueComponent, getBaseProviders} from "../app-abstract-value.component";
import {AppSingleSelectData} from "./app-single-select.data";
import {Observable, tap} from "rxjs";
import {HttpResponse} from "@angular/common/http";
import {ControlContainer} from "@angular/forms";

@Component({
  selector: 'app-singleselect',
  templateUrl: './app-single-select.component.html',
  styleUrls: ['./app-single-select.component.scss'],
  providers: [...getBaseProviders(AppSingleSelectComponent)]
})
export class AppSingleSelectComponent extends AppAbstractValueComponent<any> implements OnInit {
  public dataSource: any;

@Input()
  public dataSource$: Observable<HttpResponse<any[]>>;

  @Input()
  public valueColumn: string;

  @Input()
  public displayColumn: string;

  /**
   * Emits value on row clicked.
   */
  @Output()
  public rowClicked: EventEmitter<any> = new EventEmitter();

  constructor(@Optional() @Host() @SkipSelf() controlContainer: ControlContainer) {
    super(controlContainer);
  }

  public override ngOnInit(): void {
    super.ngOnInit();

    this.dataSource$.pipe(tap(response => {
      this.dataSource = new Array<AppSingleSelectData>();
      response?.body?.forEach((item, index) => {
        this.dataSource.push({
                  dxGuid: index,
                  displayExpr: item[this.displayColumn],
                  valueExpr: this.valueColumn && item ? item[this.valueColumn] : item.toString(),
                  icon: item ? item.icon : null,
                  color: item ? item.color : null,
                  object: item
              });
            });
      return this.dataSource;
    })).subscribe();
  }

}
