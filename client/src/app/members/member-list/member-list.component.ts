import { Component, OnInit } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { Observable, take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
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

  constructor(private membersService: MembersService, private accountService: AccountService) {

    // take(1) : It is from rxjs. Any Http call we subscribe,  we need to unsubscribe also. But using take(1) we are completeing this call. 
    // So no need of unsubscribe
    this.accountService.currentUser$.pipe(take(1)).subscribe({

      next: user => {
        if (user) {
          this.userParams = new UserParams(user);
        }
      }
    });

  }

  ngOnInit(): void {
    this.loadmembers();

  }

  pageChanged(event: PageChangedEvent): void {

    if (this.userParams && this.userParams?.pageNumber !== event.page) {
      this.userParams.pageNumber = event.page;

      this.loadmembers();
    }
  }

  loadmembers() {

    if (!this.userParams) return;

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

