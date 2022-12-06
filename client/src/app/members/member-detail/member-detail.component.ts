import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {

  member: Member | undefined;

  constructor(private membersService: MembersService, private route: ActivatedRoute) { }

  ngOnInit(): void {

    this.loadMember();
  }

  loadMember() {
    const username = this.route.snapshot.params["username"];

    if (!username) return;

    this.membersService.getMember(username).subscribe({
      next: member => {
        this.member = member;

        console.log(member);
      },
      error: (error: any) => { console.log(error); }
    })
  }

}
