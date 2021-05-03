import {Component, ElementRef, Input, OnInit, Self, ViewChild} from '@angular/core';
import {ControlValueAccessor, NgControl} from '@angular/forms';

@Component({
  selector: 'app-date-input',
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.scss']
})
export class DateInputComponent implements OnInit, ControlValueAccessor {
  @ViewChild('dateInput', {static: true}) input: ElementRef;
  @Input() label;
  maxDate: Date;
  constructor(@Self() public controlDir: NgControl) {
    this.maxDate = new Date();
    this.maxDate.setDate(this.maxDate.getDate());
    this.controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
    const control = this.controlDir.control;
    const validators = control.validator ? [control.validator] : [];
    control.setValidators(validators);
    control.updateValueAndValidity();
  }

  get ngControlInvalid(): boolean {
    return this.controlDir && this.controlDir.control && !this.controlDir.control.valid && this.controlDir.control.touched;
  }

  onChange(event): void {
  }

  onTouched(): void {
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  writeValue(obj: any): void {
    this.input.nativeElement.value = obj || '';
  }

}
