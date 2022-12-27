import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from "@angular/forms";
import { ToastrService } from 'ngx-toastr';
import { RegisterUser } from '../_models/register';
import { AccountService } from '../_services/account.service';
import { passwordMatch } from '../_validators/passwordMatch';
import {MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};

  @Input() inputUsersFromHomeComponent: any;

// @Output() cancelRegisterUser = new EventEmitter<false>();

  // public means it can be accessed in Template also.
  constructor(private accountService: AccountService, private toastr: ToastrService, private fb: FormBuilder,
    private dialogRef : MatDialogRef<RegisterComponent>) { }

  // If we want to pass default value then mention that in "" below
  registerForm = this.fb.group({
    
    gender: ["male"],
    username: ["", [Validators.required]],
    knownAs: ["", [Validators.required]],
    dateOfBirth: ["", [Validators.required]],
    city: ["", [Validators.required]],
    country: ["", [Validators.required]],
    password: ["", [Validators.required, Validators.minLength(3), Validators.maxLength(10)]],
    confirmpassword: ["", [Validators.required, passwordMatch('password')]]

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
        // this.cancel();
        this.dialogRef.close();
      },
      error: error => this.toastr.error(error.error)
    });
  }

  cancel() {
    this.model = {};

    this.dialogRef.close();
    // this.cancelRegisterUser.emit(false);
  }
}
