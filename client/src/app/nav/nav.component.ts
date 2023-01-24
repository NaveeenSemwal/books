import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
  isAdmin : boolean = false;

  // public means it can be accessed in Template also.
  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {

  }

  login() {

    this.accountService.login(this.model).subscribe({

      next: (user) => {

        let loggedInuser = this.accountService.getUserByToken(user.token);

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

  logout() {
    this.accountService.logout();

    this.router.navigateByUrl('/')
  }

}
