import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, Form, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})
export class TextInputComponent implements ControlValueAccessor {

  // Note : ControlValueAccessor is a interface and implement it if you want to create a custom form control.
  
  @Input() label = '';
  @Input() type = 'text';

  constructor(@Self() public ngControl: NgControl) {

    ngControl.valueAccessor = this;  // this refer to TextInputComponent

  }

  writeValue(obj: any): void {

  }

  registerOnChange(fn: any): void {

  }

  registerOnTouched(fn: any): void {

  }

  // Here control is property name.
  // Suppose in ts file we have property like ---- country = new FormControl()
  // so in HTML we will write  <input [formControl]="country">
  get control(): FormControl {
    return this.ngControl.control as FormControl;
  }

}
