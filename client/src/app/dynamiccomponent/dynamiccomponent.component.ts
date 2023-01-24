import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-dynamiccomponent',
  templateUrl: './dynamiccomponent.component.html',
  styleUrls: ['./dynamiccomponent.component.css']
})
export class DynamiccomponentComponent implements OnInit {


  model: any = {};
  isAdmin: boolean = false;

  errorMessage = "";

  // public means it can be accessed in Template also.
  constructor(public accountService: AccountService) { }

  ngOnInit(): void {

  }

  login() {

    this.accountService.login(this.model).subscribe({

      next: (user) => {
        alert("sucess");

      },
      error: issue => {
        this.errorMessage = (issue.error.errorMessages[0]);
      }
    });
  }

  onHandleError()
  {
    this.errorMessage = "";
    this.model = {};
  }

}
