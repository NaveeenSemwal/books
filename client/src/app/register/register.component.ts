import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from "@angular/forms";
import { ToastrService } from 'ngx-toastr';
import { RegisterUser } from '../_models/register';
import { AccountService } from '../_services/account.service';
import { passwordMatch } from '../_validators/passwordMatch';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};

  @Input() inputUsersFromHomeComponent: any;

  @Output() cancelRegisterUser = new EventEmitter<false>();

  // public means it can be accessed in Template also.
  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  // If we want to pass default value then mention that in "" below
  registerForm = new FormGroup({

    // username: new FormControl("", [Validators.required, Validators.email, Validators.pattern('')]),
    username: new FormControl("", [Validators.required]),
    password: new FormControl("", [Validators.required, Validators.minLength(3), Validators.maxLength(10)]),
    confirmpassword: new FormControl("", [Validators.required, passwordMatch('password')])

  });

  ngOnInit(): void {

    // Password :ConfirmPassword validation. check if value of password changes.  valueChanges returns Observable.
    // updateValueAndValidity : Recalculates the value and validation status of control.
    this.registerForm.controls['password'].valueChanges.subscribe({

      next: () => this.registerForm.controls['confirmpassword'].updateValueAndValidity()

    });
  }

  regsiter() {
    console.log(this.registerForm.value);

    // Told the compiler that below properties will not be null as we are using strict mode of Angular14
    const user: RegisterUser = {

      name: this.registerForm.value.username!,
      password: this.registerForm.value.password!,
      confirmpassword: this.registerForm.value.confirmpassword!,
      role: 'Admin'

    };

    this.accountService.register(user).subscribe({

      next: response => {
        console.log(response);
        this.cancel();
      },
      error: error => this.toastr.error(error.error)
    });
  }

  cancel() {
    console.log("cancelled")
    this.model = {};

    this.cancelRegisterUser.emit(false);

  }
}
