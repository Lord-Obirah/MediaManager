import {
  Component,
  Directive,
  EventEmitter,
  forwardRef,
  Host,
  Input,
  OnInit,
  Optional,
  Output,
  SkipSelf
} from '@angular/core';
import {ControlContainer, ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR} from "@angular/forms";

export function getBaseProviders(component: any) {
  return [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => component),
      multi: true
    }
  ];
}

@Directive()
export abstract class AppAbstractValueComponent<T> implements OnInit, ControlValueAccessor {
  private _value: T;
  private touchHandler: any[] = [];
  private changeHandlers: ((value: T) => any)[] = [];
  /**
   * Indicates if the element requires input from the user. Settings this flag changes the style of the element
   * to mark it as required. In addition, a validation rule is added.
   */
  @Input()
  public isRequired = false;

  @Input()
  public isDisabled = false;

  @Input()
  public formControlName: string;

  @Input()
  public formControl: FormControl;

  /**
   * A placeholder to show inside the input element if there's no text
   */
  @Input()
  public placeholder?: string;

  /** Sets a new value for the control and emits the @see valueChange event. */
  @Input()
  public set value(value: T) {
    if (!this._value && this._value === value) {
      return;
    }

    this._value = value;
    //this.formControl?.setValue(this._value);
    this.valueChange.emit(this._value);
  }

  /** This event is emitted whenever the value of the control is changed. */
  @Output()
  valueChange = new EventEmitter();

  /**
   * Emitted only when value is changed from formControl.
   * Value will be emitted on `control.setValue(value)` or `control.patchValue(value)`
   */
  @Output()
  public writeValueChange = new EventEmitter<T>();

  /** Retrieves the current value of the control. */
  public get value(): T {
    return this._value;
  }

  constructor(@Optional() @Host() @SkipSelf() protected controlContainer: ControlContainer) {
  }

  public ngOnInit(): void {
    if(this.controlContainer) {
      this.formControl = this.controlContainer?.control?.get(
        this.formControlName
      ) as FormControl;
    }
  }

  public touch(): void {
    this.formControl?.updateValueAndValidity({ emitEvent: true });
    this.formControl.setValue(this._value);
    this.touchHandler.forEach(h => h());
  }

  public writeValue(value: T) {
    this.value = value;
    this.writeValueChange.emit(value);
  }

  public registerOnChange(changeHandler: (value: T) => string) {
    this.changeHandlers.push(changeHandler);
  }

  public registerOnTouched(touchHandler: any) {
    this.touchHandler.push(touchHandler);
  }


}
