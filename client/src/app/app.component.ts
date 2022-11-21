import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = "The Dating App";
  users: any;

  constructor(private http: HttpClient, private accountService: AccountService) { }

  ngOnInit() {

    this.http.get("https://localhost:5001/api/users").subscribe({
      complete: () => { }, // completeHandler
      next: response => this.users = response,
      error: error => console.log(error)
    });


    this.setCurrentUser();

  }

  setCurrentUser() {

    const user: User = JSON.parse(localStorage.getItem('user') as string);

    this.accountService.setCurrentUser(user);
  }


}