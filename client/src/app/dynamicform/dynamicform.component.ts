import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { FieldConfig } from '../shared/field-config';


@Component({
  selector: 'app-dynamicform',
  templateUrl: './dynamicform.component.html',
  styleUrls: ['./dynamicform.component.css']
})
export class DynamicFormComponent implements OnInit {


  @Input() fields: FieldConfig[] = [];

  @Output() submit: EventEmitter<any> = new EventEmitter<any>();
  // @Output() submit: EventEmitter<any> = new EventEmitter<any>();

  form: FormGroup;

  constructor(private fb: FormBuilder) {

    this.form = this.createControl();

   }

  ngOnInit(): void {
    
  }

  onSubmit(event: Event) {

    alert("Dynamic Form component Submit");
  }

  createControl() {

    const group = this.fb.group({});

    this.fields.forEach(field => {
      if (field.type === "button") return;

      const control = this.fb.control(field.value);

      group.addControl(field.name as string, control);

    })

    return group;

  }

}
