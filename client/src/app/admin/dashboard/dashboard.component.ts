
import { Component, OnInit, ViewChild, } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { RegisterComponent } from 'src/app/register/register.component';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  displayedColumns: string[] = ['name', 'age', 'gender', 'city', 'country', 'lastActive', 'action'];
  dataSource!: MatTableDataSource<any>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  constructor(private membersService: MembersService, public dialog: MatDialog) { }

  ngOnInit(): void {

    this.loadMembers();
  }

  loadMembers() {

    // this.membersService.getMembers().subscribe({
    //   next: members => {

    //     this.dataSource = new MatTableDataSource(members);
    //     this.dataSource.paginator = this.paginator;
    //     this.dataSource.sort = this.sort;

    //   },
    //   error: (error: any) => { console.log(error); }
    // })
  }

  editMember(row: any) {

      this.dialog.open(RegisterComponent, {
       width: '30%',
       data: row
      });
  

  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

}
