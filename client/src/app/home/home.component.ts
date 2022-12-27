import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import { RegisterComponent } from '../register/register.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  users: any;

  constructor(private http: HttpClient, public dialog: MatDialog) { }

  ngOnInit(): void {

  }

  openDialog() {
    this.dialog.open(RegisterComponent, {
     width: '30%',
    });
  }

  // registerToggle() {
  //   this.registerMode = !this.registerMode;
  // }

  // cancelRegisterUserMode(event: any) {

  //   this.registerMode = event;

  // }

}
