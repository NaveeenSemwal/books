import { Component, OnInit } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { Observable, take } from 'rxjs';
import { Selector } from 'src/app/_customcontrols/select-input/select';
import { Member } from 'src/app/_models/member';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { UserParams } from 'src/app/_models/userParams';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  members$: Observable<Member[]> | undefined;
  members: Member[] | undefined;
  result: Observable<PaginatedResult<Member[]>> | undefined;
  pagination: Pagination | undefined;

  userParams: UserParams | undefined;

  genderList: Selector[] = [{ value: "male", display: "Male" }, { value: "female", display: "Female" }]


  constructor(private membersService: MembersService) {

    this.userParams = this.membersService.getUserParams();

  }

  ngOnInit(): void {
    this.loadmembers();

  }

  pageChanged(event: PageChangedEvent): void {

    if (this.userParams && this.userParams?.pageNumber !== event.page) {
      this.userParams.pageNumber = event.page;

      this.membersService.setUserParams(this.userParams);

      this.loadmembers();
    }
  }

  loadmembers() {

    if (this.userParams) {

      this.membersService.setUserParams(this.userParams);

      this.membersService.getMembers(this.userParams).subscribe({

        next: response => {
          if (response.result && response.pagination) {

            this.members = response.result;
            this.pagination = response.pagination;
          }
        }
      });
    }

  }

  resetFilters() {

      this.userParams = this.membersService.resetUserParams();
      this.loadmembers();
  }
}

