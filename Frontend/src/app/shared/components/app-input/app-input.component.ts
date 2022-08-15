import {Component, EventEmitter, forwardRef, Input, OnInit, Output} from '@angular/core';
import {ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR} from "@angular/forms";

export function getBaseProviders(component: any) {
  return [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => component),
      multi: true
    }
  ];
}

@Component({
  selector: 'app-input',
  templateUrl: './app-input.component.html',
  styleUrls: ['./app-input.component.scss'],
  providers: [...getBaseProviders(AppInputComponent)],
})
export class AppInputComponent implements OnInit, ControlValueAccessor {
  private _value: string;
  private touchHandler: any[] = [];
  private changeHandlers: ((value: string) => any)[] = [];
  /**
   * Indicates if the element requires input from the user. Settings this flag changes the style of the element
   * to mark it as required. In addition, a validation rule is added.
   */
  @Input()
  public isRequired = false;

  @Input()
  public isDisabled = false;

  @Input()
  public formControl: FormControl;

  /**
   * A placeholder to show inside the input element if there's no text
   */
  @Input()
  public placeholder?: string;

  /** Sets a new value for the control and emits the @see valueChange event. */
  @Input()
  public set value(value: string) {
    if (!this._value && this._value === value) {
      return;
    }

    this._value = value;
    this.valueChange.emit(this._value);
  }

  /** This event is emitted whenever the value of the control is changed. */
  @Output()
  valueChange = new EventEmitter();

  /** Retrieves the current value of the control. */
  public get value(): string {
    return this._value;
  }

  constructor() { }

  ngOnInit(): void {
  }

  public writeValue(value: string) {
    this.value = value;
  }

  public registerOnChange(changeHandler: (value: string) => string) {
    this.changeHandlers.push(changeHandler);
  }

  public registerOnTouched(touchHandler: any) {
    this.touchHandler.push(touchHandler);
  }


}
