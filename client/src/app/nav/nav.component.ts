import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};

 // public means it can be accessed in Template also.
  constructor(public accountService: AccountService) { }

  ngOnInit(): void {

  }

  login() {

    this.accountService.login(this.model).subscribe({

      next: resposne => {
          console.log(resposne);
      },
      error: error => console.log(error)
    });
  }

  logout() {
    this.accountService.logout();
  }

}
