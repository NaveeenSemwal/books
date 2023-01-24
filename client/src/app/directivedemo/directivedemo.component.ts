import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-directivedemo',
  templateUrl: './directivedemo.component.html',
  styleUrls: ['./directivedemo.component.css']
})
export class DirectivedemoComponent implements OnInit {

  oddNumbers: number[] = [1, 3, 5]

  evenNumbers: number[] = [2, 4, 6]

  oddOnly = false;

  constructor() { }

  ngOnInit(): void {
  }

}
