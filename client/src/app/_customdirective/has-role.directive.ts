import { Directive, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { map, take } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit {

  @Input() appHasRole: string[] = []; // Property name and selector should be same
  user: User | undefined;

  constructor(private template: TemplateRef<any>, private container: ViewContainerRef, private accountService: AccountService) { }


  ngOnInit(): void {

    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {

      if (user) {
        
        if (user.roles.some(x => this.appHasRole.includes(x))) {

          this.container.createEmbeddedView(this.template);

        } else {

          this.container.clear();
        }

      }

    })
  }

}
