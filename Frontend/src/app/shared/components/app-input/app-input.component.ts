import {Component, Host, OnInit, Optional, SkipSelf} from '@angular/core';
import {AppAbstractValueComponent, getBaseProviders} from "../app-abstract-value.component";
import {ControlContainer} from "@angular/forms";

@Component({
  selector: 'app-input',
  templateUrl: './app-input.component.html',
  styleUrls: ['./app-input.component.scss'],
  providers: [...getBaseProviders(AppInputComponent)]
})
export class AppInputComponent extends AppAbstractValueComponent<string> implements OnInit {

  constructor(@Optional() @Host() @SkipSelf() controlContainer: ControlContainer) {
    super(controlContainer);
  }

  public override ngOnInit(): void {
    super.ngOnInit();
  }
}
