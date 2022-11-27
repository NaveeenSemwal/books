import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};

  @Input() inputUsersFromHomeComponent :any;

  @Output() cancelRegisterUser = new EventEmitter<false>();

  registerForm = new FormGroup({

    username: new FormControl("", [Validators.required, Validators.email, Validators.pattern('')]),
    password: new FormControl("", [Validators.required, Validators.minLength(3), Validators.maxLength(6)])

  });

  constructor() { }

  ngOnInit(): void {
  }

  regsiter() {
    console.log(this.registerForm.value);
  }

  cancel() {
    console.log("cancelled")
    this.model = {};

    this.cancelRegisterUser.emit(false);

  }

  get userValidation() {
    return this.registerForm.get("username");
  }

  get passwordValidation() {
    return this.registerForm.get("password");
  }

}
