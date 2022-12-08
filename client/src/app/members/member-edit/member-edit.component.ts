import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

  member: Member | undefined;
  user: User | null = null;

  constructor(private membersService: MembersService, private accountService: AccountService) {

    this.accountService.currentUser$.pipe(take(1)).subscribe({

      next: (user) => {
        this.user = user;
      },
      error: (error) => { console.error(error); }

    });
  }

  ngOnInit(): void {

    this.loadMember();
  }

  loadMember() {
    if (!this.user) return;

    this.membersService.getMember(this.user.userName).subscribe({
      next: (member) => {
        this.member = member;
      },
      error(err) {
          console.error(err);
      },
    })
  }

}
