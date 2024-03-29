import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from "@angular/forms";
import { ToastrService } from 'ngx-toastr';
import { RegisterUser } from '../_models/register';
import { AccountService } from '../_services/account.service';
import { passwordMatch } from '../_validators/passwordMatch';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  actionBtn: string = "Register";
  isAdmin: boolean = false;

  @Input() inputUsersFromHomeComponent: any;

  // @Output() cancelRegisterUser = new EventEmitter<false>();

  maxDate: Date = new Date();

  // public means it can be accessed in Template also.
  constructor(private accountService: AccountService, private router: Router,
    private toastr: ToastrService, private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public editData: any,
    private dialogRef: MatDialogRef<RegisterComponent>) { }

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

    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);

    // Password :ConfirmPassword validation. check if value of password changes.  valueChanges returns Observable.
    // updateValueAndValidity : Recalculates the value and validation status of control.
    this.registerForm.controls['password'].valueChanges.subscribe({

      next: () => this.registerForm.controls['confirmpassword'].updateValueAndValidity()

    });

    if (this.editData) {

      console.log(this.editData);

      this.registerForm.controls['gender'].setValue(this.editData.gender),
        this.registerForm.controls['username'].setValue(this.editData.name),
        this.registerForm.controls['knownAs'].setValue(this.editData.knownAs),
        this.registerForm.controls['dateOfBirth'].setValue(this.editData.dateOfBirth),
        this.registerForm.controls['city'].setValue(this.editData.city),
        this.registerForm.controls['country'].setValue(this.editData.country),
        this.registerForm.controls['password'].setValue(this.editData.password)
      this.registerForm.controls['confirmpassword'].setValue(this.editData.password)

      this.actionBtn = "Update";

    }
  }

  regsiter() {
    console.log(this.registerForm.value);

    // Told the compiler that below properties will not be null as we are using strict mode of Angular14
    // const user: RegisterUser = {

    //   name: this.registerForm.value.username!,
    //   password: this.registerForm.value.password!,
    //   confirmpassword: this.registerForm.value.confirmpassword!,
    //   role: 'Admin',
    //   gender : this.registerForm.value.gender!,
    //   knownas : this.registerForm.value.knownAs!,
    //   dateofbirth : this.registerForm.value.dateOfBirth,



    // };

    if (this.editData) {

      // Update code

    } else {

      this.accountService.register(this.registerForm.value).subscribe({

        next: response => {
         
          // this.cancel();
          this.dialogRef.close();

          this.login(this.registerForm.value);

        },
        error: error => this.toastr.error(error.error)
      });

    }
  }

  cancel() {
    this.model = {};

    this.dialogRef.close();
    // this.cancelRegisterUser.emit(false);
  }

  login(requestModel: any) {

    let model = { "username": requestModel.username, "password":  requestModel.password}

    this.accountService.login(model).subscribe({

      next: (user) => {

        let loggedInuser = this.accountService.getDecodedToken(user.token);

        console.log(loggedInuser);

        if (loggedInuser.role === 'Admin') {

          this.isAdmin = true;
          this.router.navigateByUrl('/admin/dashboard')

        } else {
          this.router.navigateByUrl('/members')
        }
      },
      error: issue => {
        this.toastr.error(issue.error.errorMessages[0])
      }
    });
  }
}
